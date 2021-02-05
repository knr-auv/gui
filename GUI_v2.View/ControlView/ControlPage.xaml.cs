using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_v2.View
{
    /// <summary>
    /// Interaction logic for ControlPage.xaml
    /// </summary>
    public partial class ControlPage : UserControl
    {
        public ControlPage()
        {
            InitializeComponent();
        }
        private Window FullscreenWindow;
        private Grid StreamParent;
        private MainWindow mainWindow;
        private Grid Wrapper;
        private void FullscreenBtn_Clicked(object sender, RoutedEventArgs e)
        {

            mainWindow = (MainWindow)Application.Current.MainWindow;
            FullscreenWindow = new Window() { Owner = mainWindow };
            FullscreenWindow.AllowsTransparency = true;
            FullscreenWindow.WindowStyle = WindowStyle.None;
            FullscreenWindow.WindowState = WindowState.Maximized;
            FullscreenWindow.Background = Brushes.Black;
            StreamParent = (Grid)CameraStream.Parent;

            StreamParent.Children.Remove(CameraStream);
            StreamParent.Children.Remove(HUD);

            Wrapper = new Grid();

            Wrapper.Children.Add(CameraStream);
            Wrapper.Children.Add(HUD);

            FullscreenWindow.Content = Wrapper;
            FullscreenWindow.DataContext = StreamParent.DataContext;
            
            FullscreenWindow.Closing += FullscreenWindow_Closing;
            FullscreenWindow.KeyDown += FullscreenWindow_KeyDown;
            FullscreenWindow.KeyUp += FullscreenWindow_KeyUp;
            FullscreenWindow.ShowInTaskbar = false;
            FullscreenWindow.Icon = mainWindow.Icon;
            FullscreenWindow.Title = mainWindow.Title;
            //FullscreenWindow
            FullscreenWindow.Loaded += FullscreenWindow_Loaded;
            mainWindow.Closing += MainWindow_Closing;

            FullscreenWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            FullscreenWindow.Show();
            Mouse.OverrideCursor = Cursors.None;



        }


        private void FullscreenWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0,1,new TimeSpan(0,0,0,0,300));
            animation.Completed +=(s,ev)=> {
                mainWindow.Hide();
                FullscreenWindow.ShowInTaskbar = true;

            };
            
            FullscreenWindow.BeginAnimation(OpacityProperty, animation);
            
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FullscreenWindow.Close();
        }

        private void FullscreenWindow_KeyUp(object sender, KeyEventArgs e)
        {
        
        }

        private void FullscreenWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) {
                DoubleAnimation animation = new DoubleAnimation(1, 0, new TimeSpan(0, 0, 0, 0, 300));
                animation.Completed += (s, ev) => {

                    FullscreenWindow.Close();
                    Mouse.OverrideCursor = null;
                };
                mainWindow.Show();
                FullscreenWindow.BeginAnimation(OpacityProperty, animation);
            }
                
        }

        private void FullscreenWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FullscreenWindow.Content = null;
            Wrapper.Children.Clear();
            StreamParent.Children.Add(CameraStream);
            StreamParent.Children.Add(HUD);
            mainWindow.Show();
           
        }

    }
}
