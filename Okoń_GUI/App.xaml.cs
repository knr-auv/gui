using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GUI_v2.View;
using GUI_v2.ViewModel;
namespace Okoń_GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ModelContainer modelContainer;

        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            base.OnStartup(e);
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            ModelLoader loader = new ModelLoader(loadingWindow);
            Task.Factory.StartNew(() =>
            {
                loader.LoadUserSettings();
                loader.TryToConnect();
                modelContainer = loader.GetModelConatiner();
                this.Dispatcher.Invoke(() =>
                {
                    MainWindow window = new MainWindow();
                    MainViewModel MainViewModel = new MainViewModel(modelContainer);
                    window.DataContext = MainViewModel;
                    this.MainWindow = window;
                    loadingWindow.Close();
                    window.Show();
                    //modelContainer.jetsonClient.StartTelemetry(2);

                });
            });

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            modelContainer.keyboardController.StopController();
            modelContainer.jetsonClient.Disconnect();
            modelContainer.cameraStreamClient.Disconnect();

        }
    }

}
