using GUI_v2.ViewModel.Commands;
using GUI_v2.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel
{
    public partial class AutonomyViewModel : BaseViewModel
    {
        private ModelContainer modelContainer;

        public override void Show()
        {
            modelContainer.cameraStreamClient.StartStream(CameraStreamViewModel.SwapImage, CameraStreamViewModel.SetLogo);
        }
        public override void Hide()
        {
            modelContainer.cameraStreamClient.StopStream();
        }
        

        public AutonomyViewModel(ModelContainer ModelContainer)
        {
            modelContainer = ModelContainer;
            LoggerViewModel = new LoggerViewModel(modelContainer.logger);
            CameraStreamViewModel = new CameraStreamViewModel();
            DetectionListViewModel = new DetectionListViewModel(modelContainer);


            StartAutonomyCommand = new RelayCommand(StartAutonomyAction, Allow);
            StopAutonomyCommand = new RelayCommand(StopAutonomyAction, Allow);
            modelContainer.dataContainer.Position.newDataCallback += (double x, double y, double z) => { RaisePropertyChanged("posX"); RaisePropertyChanged("posY"); RaisePropertyChanged("posZ"); };
            modelContainer.dataContainer.Velocity.newNormCallback += (double x) => RaisePropertyChanged("Velocity");
            modelContainer.dataContainer.Acceleration.newNormCallback += (double x) => RaisePropertyChanged("Acceleration");
            modelContainer.modelStatus.taskManagerStatus.NewData += () => RaisePropertyChanged("TaskManagerStatus");
            modelContainer.modelStatus.networkStatus.ConnectedToJetsonCallback += (bool val) => JetsonConnected = val;
            modelContainer.modelStatus.AutonomyStatusCallback += (bool val) => AutonomyStatus = val;
            modelContainer.dataContainer.detections.newDetectionsCallback += DetectionListViewModel.HandleNewDetection;
        }
    }
}
