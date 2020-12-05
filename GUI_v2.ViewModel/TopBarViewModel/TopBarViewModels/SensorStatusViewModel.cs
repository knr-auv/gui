using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel.TopBarViewModels
{
    public class SensorStatusViewModel:BaseViewModel
    {

        public SensorStatusViewModel(ModelContainer modelContainer)
        {
            this.modelContainer = modelContainer;
            AccStatus = new SensorStatus();
            GyroStatus = new SensorStatus();
            MagStatus = new SensorStatus();
            BaroStatus = new SensorStatus();
            AccChangedCallback(modelContainer.modelStatus.sensorStatus.AccStatus);
            GyroChangedCallback(modelContainer.modelStatus.sensorStatus.GyroStatus);
            BaroChangedCallback(modelContainer.modelStatus.sensorStatus.BaroStatus);
            MagChangedCallback(modelContainer.modelStatus.sensorStatus.MagStatus);

            modelContainer.modelStatus.networkStatus.ConnectedToJetsonCallback += (bool val) => { JetsonConnected = val; };
            modelContainer.modelStatus.sensorStatus.AccStatusChanged += AccChangedCallback;
            modelContainer.modelStatus.sensorStatus.GyroStatusChanged += GyroChangedCallback;
            modelContainer.modelStatus.sensorStatus.MagStatusChanged += MagChangedCallback;
            modelContainer.modelStatus.sensorStatus.BaroStatusChanged += BaroChangedCallback;
        }



        public class SensorStatus: BaseViewModel
        {
            private bool _off = true;
            private bool _on = false;
            private bool _error = false;
            public bool off
            {
                get { return _off; }
                set { SetProperty(ref _off, value); }
            }
            public bool on
            {
                get { return _on; }
                set { SetProperty(ref _on, value); }
            }
            public bool error
            {
                get { return _error; }
                set { SetProperty(ref _error, value); }
            }
        }
        public bool JetsonConnected { get; set; }
        private double _hummidity = 10;
        public double Hummidity
        {
            get { return _hummidity; }
            set { SetProperty(ref _hummidity, value); }
        }
        private ModelContainer modelContainer;

        public SensorStatus AccStatus { get; set; }
        public SensorStatus GyroStatus { get; set; }
        public SensorStatus MagStatus { get; set; }
        public SensorStatus BaroStatus { get; set; }

        private void  AccChangedCallback(string value)
        {
            switch (value)
            {
                case "off":
                    AccStatus.off = true;
                    AccStatus.on = false;
                    AccStatus.error = false;
                    break;
                case "on":
                    AccStatus.off = false;
                    AccStatus.on = true;
                    AccStatus.error = false;
                    break;
                case "error":
                    AccStatus.off = false;
                    AccStatus.on = false;
                    AccStatus.error = true;
                    break;
                default:
                    Console.WriteLine(value + "Is not valid callback type in sensor status");
                    break;
            }

        }
        private void MagChangedCallback(string value)
        {
            switch (value)
            {
                case "off":
                    MagStatus.off = true;
                    MagStatus.on = false;
                    MagStatus.error = false;
                    break;
                case "on":
                    MagStatus.off = false;
                    MagStatus.on = true;
                    MagStatus.error = false;
                    break;
                case "error":
                    MagStatus.off = false;
                    MagStatus.on = false;
                    MagStatus.error = true;
                    break;
                default:
                    Console.WriteLine(value + "Is not valid callback type in sensor status");
                    break;
            }

        }
        private void BaroChangedCallback(string value)
        {
            switch (value)
            {
                case "off":
                    BaroStatus.off = true;
                    BaroStatus.on = false;
                    BaroStatus.error = false;
                    break;
                case "on":
                    BaroStatus.off = false;
                    BaroStatus.on = true;
                    BaroStatus.error = false;
                    break;
                case "error":
                    BaroStatus.off = false;
                    BaroStatus.on = false;
                    BaroStatus.error = true;
                    break;
                default:
                    Console.WriteLine(value + "Is not valid callback type in sensor status");
                    break;
            }

        }
        private void GyroChangedCallback(string value)
        {
            switch (value)
            {
                case "off":
                    GyroStatus.off = true;
                    GyroStatus.on = false;
                    GyroStatus.error = false;
                    break;
                case "on":
                    GyroStatus.off = false;
                    GyroStatus.on = true;
                    GyroStatus.error = false;
                    break;
                case "error":
                    GyroStatus.off = false;
                    GyroStatus.on = false;
                    GyroStatus.error = true;
                    break;
                default:
                    Console.WriteLine(value + "Is not valid callback type in sensor status");
                    break;
            }

        }

    }
}
