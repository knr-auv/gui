using GUI_v2.ViewModel;
using GUI_v2.WindowControl;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace GUI_v2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            DoOnStartup();
        }

        public void keyUp(object sender, KeyEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            if (vm.modelContainer.keyboardController.keyboard_disabled)
                e.Handled = true;
        
    }
        public void keyDown(object sender, KeyEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            if (vm.modelContainer.keyboardController.keyboard_disabled)
                e.Handled = true;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            WindowControl.RefreshMaximizeRestoreButton();
        }
        private void DoOnStartup()
        {
            WindowControl.RefreshMaximizeRestoreButton();
        }



    }
}
