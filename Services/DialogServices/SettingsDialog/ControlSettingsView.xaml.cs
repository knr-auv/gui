using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Services.DialogServices.SettingsDialog
{
    /// <summary>
    /// Interaction logic for ControlSettings.xaml
    /// </summary>
    public partial class ControlSettingsView : UserControl
    {
        ControlSettingsViewModel vm;


        public ControlSettingsView()
        {
            InitializeComponent();
            vm = (ControlSettingsViewModel)DataContext;

        }


        string prevKey;
        Label currentLabel;
        ListView lv;
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            lv = (ListView)sender;
            if (lv.SelectedItem != null)
            {
                object o = lv.SelectedItem;
                ListViewItem lvi = (ListViewItem)lv.ItemContainerGenerator.ContainerFromItem(o);
                currentLabel = FindByName("ValueLabel", lvi) as Label;
                if (currentLabel != null)
                {
                    KeyValuePair<string, Key> temp = (KeyValuePair<string, Key>)lvi.DataContext;
                    prevKey = temp.Key;
                    currentLabel.Content = "...";
                    PreviewKeyDown += ControlSettingsView_KeyDown;
                }
            }
        }

        private void ControlSettingsView_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (vm == null)
                vm = (ControlSettingsViewModel)DataContext;
            if (currentLabel == null)
                return;
            //Binding b = new Binding("Value");
            //b.Source = vm.KeyboardAssignment[prevContent.ToString()];
            currentLabel.Content = e.Key;
            vm.AssignKeyAction(prevKey, e.Key);
            PreviewKeyDown -= ControlSettingsView_KeyDown;
            lv.UnselectAll();
            // BindingOperations.GetBindingExpressionBase(currentLabel, Label.ContentProperty).UpdateTarget();
        }

        private FrameworkElement FindByName(string name, FrameworkElement root)
        {
            Stack<FrameworkElement> tree = new Stack<FrameworkElement>();
            tree.Push(root);

            while (tree.Count > 0)
            {
                FrameworkElement current = tree.Pop();
                if (current.Name == name)
                    return current;

                int count = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < count; ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(current, i);
                    if (child is FrameworkElement)
                        tree.Push((FrameworkElement)child);
                }
            }

            return null;
        }

        private void ListView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
