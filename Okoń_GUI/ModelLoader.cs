using GUI_v2.Model;
using GUI_v2.Model.UserSettings;
using GUI_v2.ViewModel;
using GUI_v2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_v2.View
{
    public class ModelLoader
    {
        private readonly ModelContainer modelContainer;
        private readonly LoadingWindow loadingWindow;
        public ModelLoader(LoadingWindow LoadingWindow)
        {
            loadingWindow = LoadingWindow;
            modelContainer = new ModelContainer();
            RegisterCallbacks();
        }
        //here you should connect info between model and modelStatus. Entire UI is binded to model status property. Only imu data are binded to server
        private void RegisterCallbacks()
        {
            modelContainer.jetsonClient.ConnectionStatusChnaged += (bool val) => { modelContainer.modelStatus.networkStatus.ConnectedToJetson = val; };
            modelContainer.jetsonClient.ConnectionStatusChnaged += (bool val) => {if(val==false) modelContainer.modelStatus.Armed = false; };
            modelContainer.jetsonClient.callbacks.ArmConfirmation += () => { modelContainer.modelStatus.Armed = true; };
            modelContainer.jetsonClient.callbacks.DisarmConfirmation += () => { modelContainer.modelStatus.Armed = false; };
            modelContainer.cameraStreamClient.ConnectionStatusChanged += (bool val)=>{ modelContainer.modelStatus.networkStatus.CameraStreamConnected = val; };
            modelContainer.jetsonClient.callbacks.IMUDataCallback += (double[] data) =>
             {
                //attitude, acc, gyro, mag, depth};
                modelContainer.dataContainer.Attitude.UpdateData(data[0], data[1], data[2]);
                 modelContainer.dataContainer.Acc.UpdateData(data[3], data[4], data[5]);
                 modelContainer.dataContainer.Gyro.UpdateData(data[6], data[7], data[8]);
                 modelContainer.dataContainer.Mag.UpdateData(data[9], data[10], data[11]);
                 modelContainer.dataContainer.Baro.UpdateData(data[12]);
             };
            modelContainer.jetsonClient.callbacks.MovementInfoCallback += (double[] data)=> 
             {
                modelContainer.dataContainer.Position.UpdateData(data[0], data[1], data[2]);
                modelContainer.dataContainer.Velocity.UpdateData(data[3], data[4], data[5]);
                modelContainer.dataContainer.Acceleration.UpdateData(data[6], data[7], data[8]);
             };
            modelContainer.jetsonClient.callbacks.BatteryCallback += (double[] val) => { modelContainer.dataContainer.Battery.UpdateData(val); };
            modelContainer.jetsonClient.callbacks.loggerMsgCallback += (string msg) => { modelContainer.logger.HandleNewMsg(msg); };
            modelContainer.jetsonClient.callbacks.AutonomyStarted += () => modelContainer.modelStatus.AutonomyRunning = true;
            modelContainer.jetsonClient.callbacks.AutonomyStoped += () => modelContainer.modelStatus.AutonomyRunning = false;
            modelContainer.jetsonClient.callbacks.taskManagerMsgCallback += modelContainer.modelStatus.taskManagerStatus.HandleNewData;
            modelContainer.jetsonClient.callbacks.DetectionCallback += modelContainer.dataContainer.detections.HandleNewDetections;
            modelContainer.dataContainer.Position.newDataCallback += modelContainer.dataContainer.detections.HandleNewPosition;
        }
        public ModelContainer GetModelConatiner()
        {
            return modelContainer;
        }

        private void UpdateText(string text)
        {
            loadingWindow.Dispatcher.Invoke(() => { loadingWindow.LoadingState = text; });
        }
        public void LoadUserSettings()
        {
            modelContainer.userSettings = UserSettings.Read("UserSettings.xaml");
            if (modelContainer.userSettings == null)
            {
                UpdateText("User settings not found or corupted, loading default settings");
                modelContainer.userSettings = new UserSettings();
            }
            modelContainer.SetUserSettings();
            UpdateText( "User settings loaded");
        }
        public void TryToConnect()
        {
            UpdateText ("Connecting to Jetson");
           if (modelContainer.jetsonClient.ConnectToJetson(modelContainer.userSettings.networkSettings.JetsonIP, modelContainer.userSettings.networkSettings.ControlPort))
            {
                modelContainer.modelStatus.networkStatus.ConnectedToJetson = true;
                modelContainer.jetsonClient.StartReceiving();
              UpdateText("Connected to Jetson");
                UpdateText("Connecting to stream");
                if (modelContainer.cameraStreamClient.ConnectToServer(modelContainer.userSettings.networkSettings.JetsonIP, modelContainer.userSettings.networkSettings.VideoStreamPort))
                {
                    modelContainer.modelStatus.networkStatus.CameraStreamConnected = true;
                    UpdateText("Connected to stream");
                }
                else
                {
                    modelContainer.modelStatus.networkStatus.CameraStreamConnected = false;
                    UpdateText("Could not connect to stream");
                }
            }
            else
            {
                modelContainer.modelStatus.networkStatus.ConnectedToJetson = false;
                UpdateText("Could not connect to jetson");
            }

           
        }
    }
}