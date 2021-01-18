using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Services.DialogServices;
using Services.DialogServices.SettingsDialog;


namespace GUI_v2.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        
       
        public ICommand DisconnectFromJetsonCommand { get; private set; }
        public ICommand ConnectToJetsonCommand { get; private set; }
        public ICommand ChangeViewCommand { get; private set; }
        public ICommand ExpandMenuCommand { get; private set; }
        public ICommand OpenSettingsCommand { get; private set; }


        private void OpenSettingsAction(object parameter)
        {
            modelContainer.jetsonClient.Disarm();
            modelContainer.keyboardController.StopController();
            DialogBaseViewModel vm = new SettingsDialogViewModel(modelContainer);
            DialogService.OpenDialog(vm);
        }

        private void ConnectToJetsonAction(object parameter)
        {
            Thread x = new Thread(() =>
          {
              Cursor = Cursors.Wait;
              string ip = modelContainer.userSettings.networkSettings.JetsonIP;
              int controlPort = modelContainer.userSettings.networkSettings.ControlPort;
              int videoPort = modelContainer.userSettings.networkSettings.VideoStreamPort;
              if (modelContainer.jetsonClient.ConnectToJetson(ip, controlPort))
              {
                  modelContainer.jetsonClient.StartReceiving();
                  modelContainer.modelStatus.networkStatus.ConnectedToJetson = true;
                  if (modelContainer.cameraStreamClient.ConnectToServer(ip, videoPort))
                  {
                      modelContainer.modelStatus.networkStatus.CameraStreamConnected = true;
                      if (SelectedViewModel == _IMUViewModel)
                          modelContainer.cameraStreamClient.StartStream(_IMUViewModel.CameraViewModel.SwapImage,_IMUViewModel.CameraViewModel.SetLogo);
                      else if (SelectedViewModel == _ControlViewModel)
                          modelContainer.cameraStreamClient.StartStream(_ControlViewModel.CameraViewModel.SwapImage, _IMUViewModel.CameraViewModel.SetLogo);
                      else if (SelectedViewModel == _AutonomyViewModel)
                          modelContainer.cameraStreamClient.StartStream(_AutonomyViewModel.CameraViewModel.SwapImage, _AutonomyViewModel.CameraViewModel.SetLogo);
                      //add other view if they are using stream
                  }
                  else
                      modelContainer.modelStatus.networkStatus.CameraStreamConnected = false;
              }
              else
              {
                  modelContainer.modelStatus.networkStatus.ConnectedToJetson = false;
                  modelContainer.modelStatus.networkStatus.CameraStreamConnected = false;
              }
              Cursor = null;
          });
            x.IsBackground = true;
            x.Start();
           
        }

        private void DisconnectFromJetsonAction(object parameter)
        {
            if (modelContainer.jetsonClient.Disconnect())
            {
                modelContainer.modelStatus.networkStatus.ConnectedToJetson = false;
            }
            modelContainer.cameraStreamClient.Disconnect();
            modelContainer.modelStatus.networkStatus.CameraStreamConnected = false;
        }

        private bool AlwaysAllow(object parameter)
        {
            return true;
        }

     

        private void ExpandMenuAction(object parameter)
        {
            this.ShouldMenuExpand = !this.ShouldMenuExpand;
        }

        private void ChangeViewAction(object parameter)
        {
            //TODO Consider passing here callback for camera view model
            if (parameter.ToString() == "IMU")
            {
                if (this.SelectedViewModel != this._IMUViewModel)
                {
                    this.SelectedViewModel = this._IMUViewModel;
                }
            }
            else if (parameter.ToString() == "Status")
            {
                if (this.SelectedViewModel != this._StatusViewModel)
                {
                    this.SelectedViewModel = this._StatusViewModel;
                }
            }
            else if (parameter.ToString() == "Control")
            {
                if (this.SelectedViewModel != this._ControlViewModel)
                {
                    this.SelectedViewModel = this._ControlViewModel;
                }
                }
            else if (parameter.ToString() == "Autonomy")
            {
                if (this.SelectedViewModel != this._AutonomyViewModel)
                {
                    this.SelectedViewModel = this._AutonomyViewModel;
                }
            }
            this.ShouldMenuExpand = !this.ShouldMenuExpand;
        }

    }
}
