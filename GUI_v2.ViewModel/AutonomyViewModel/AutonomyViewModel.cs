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
            modelContainer.cameraStreamClient.StartStream(CameraViewModel.SwapImage, CameraViewModel.SetLogo);
        }
        public override void Hide()
        {

        }
        

        public AutonomyViewModel(ModelContainer ModelContainer)
        {
            modelContainer = ModelContainer;
            LoggerViewModel = new LoggerViewModel(modelContainer.logger);
            CameraViewModel = new CameraStreamViewModel();
            DetectionListViewModel = new DetectionListViewModel(modelContainer);


            StartAutonomyCommand = new RelayCommand(StartAutonomyAction, Allow);
            StopAutonomyCommand = new RelayCommand(StopAutonomyAction, Allow);
            modelContainer.dataContainer.Position.newDataCallback += (float x, float y, float z) => { RaisePropertyChanged("posX"); RaisePropertyChanged("posY"); RaisePropertyChanged("posZ"); };
            modelContainer.dataContainer.Velocity.newNormCallback += (float x) => RaisePropertyChanged("Velocity");
            modelContainer.dataContainer.Acceleration.newNormCallback += (float x) => RaisePropertyChanged("Acceleration");
            modelContainer.modelStatus.taskManagerStatus.NewData += () => RaisePropertyChanged("TaskManagerStatus");
            modelContainer.modelStatus.networkStatus.ConnectedToJetsonCallback += (bool val) => JetsonConnected = val;
            modelContainer.modelStatus.AutonomyStatusCallback += (bool val) => AutonomyStatus = val;
            modelContainer.dataContainer.Attitude.newDataCallback += (float x, float y, float z) => { Roll = x; Pitch = y; Heading = z; };
            modelContainer.dataContainer.motorsData.newDataCallback += () => { RaisePropertyChanged("MotorsData"); };
        }
    }
}
