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

namespace GUI_v2.WindowControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WindowControl : UserControl
    {
        public WindowControl()
        {
            InitializeComponent();

            //RefreshMaximizeRestoreButton();
        }
        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {

            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void OnMaximizeRestoreButtonClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
               Application.Current.MainWindow.WindowState = WindowState.Maximized;

            }
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
        public void RefreshMaximizeRestoreButton()
        {

            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                this.maximizeButton.Visibility = Visibility.Collapsed;
                this.restoreButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.maximizeButton.Visibility = Visibility.Visible;
                this.restoreButton.Visibility = Visibility.Collapsed;
            }
        }

    }
}
