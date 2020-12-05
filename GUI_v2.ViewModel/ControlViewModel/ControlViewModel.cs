using GUI_v2.Tools;
using GUI_v2.ViewModel.Commands;
using GUI_v2.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel
{
    public partial class ControlViewModel : BaseViewModel
    {
        public override void Hide()
        {
            modelContainer.cameraStreamClient.StopStream();
            modelContainer.dataContainer.Position.newDataCallback -= Position.UpdateInfo;
            modelContainer.dataContainer.Velocity.newNormCallback -= UpdateVelocity;
            modelContainer.dataContainer.Acceleration.newNormCallback -= UpdateAcceleration;
        }
        public override void Show()
        {
            modelContainer.cameraStreamClient.StartStream(CameraViewModel.SwapImage, CameraViewModel.SetLogo);
            modelContainer.dataContainer.Position.newDataCallback += Position.UpdateInfo;
            modelContainer.dataContainer.Velocity.newNormCallback += UpdateVelocity;
            modelContainer.dataContainer.Acceleration.newNormCallback += UpdateAcceleration;
        }

        
        public ModelContainer modelContainer;
        
        public ControlViewModel(ModelContainer modelContainer)
        {
            Position = new MovementInfoClass();
            this.modelContainer = modelContainer;
            CameraViewModel = new CameraStreamViewModel();
            HUDViewModel = new HUDViewModel();
            DetectionListViewModel = new DetectionListViewModel(modelContainer);
            ArmCommand = new RelayCommand(ArmAction, CanArmAction);
            DisarmCommand = new RelayCommand(DisarmAction, CanDisarmAction);
            DetectionBtnClickedCommand = new RelayCommand(DetectionBtnClickedAction, (object p) => { return true; });
            modelContainer.modelStatus.ArmCallback += (bool value) => { Armed = value; };
            modelContainer.modelStatus.networkStatus.ConnectedToJetsonCallback += (bool value) => { JetsonConnected = value; };
            modelContainer.modelStatus.networkStatus.CameraStreamConnectedCallback += CameraStreamStatusChangedCallback;

        }

        private void UpdateVelocity(double x) {Velocity = x;}
        private void UpdateAcceleration(double x) { Acceleration = x; }

        private void CameraStreamStatusChangedCallback(bool val)
        {
            if (val == false)
            {
                CameraViewModel.SetLogo();
            }
        }
    }
}
