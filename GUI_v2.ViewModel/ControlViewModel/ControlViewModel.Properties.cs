using GUI_v2.Tools;
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
        private bool _Armed;
        private bool showHUD = true;
        private bool _detectionState = false;
        private double _acceleration = 0;
        private double _velocity = 0;



        public bool Armed
        {
            get { return _Armed; }
            set { SetProperty(ref _Armed, value); }
        }

        public bool JetsonConnected
        {
            get { return modelContainer.modelStatus.networkStatus.ConnectedToJetson; }
            set { RaisePropertyChanged("JetsonConnected"); }
        }


        public bool DetectionState
        {
            get { return _detectionState; }
            set { SetProperty(ref _detectionState, value); }
        }


        public bool ShowHUD
        {
            get { return showHUD; }
            set { showHUD = value; }
        }


        public double Acceleration
        {
            get { return _acceleration; }
            set { SetProperty(ref _acceleration, value); }
        }
        public double Velocity
        {
            get { return _velocity; }
            set { SetProperty(ref _velocity, value); }
        }

        public MovementInfoClass Position { get; set; }

        public HUDViewModel HUDViewModel { get; set; }
        public CameraStreamViewModel CameraViewModel { get; set; }
        public DetectionListViewModel DetectionListViewModel { get; set; }
    }
}
