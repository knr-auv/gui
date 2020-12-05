using PC_GUI_v2.Model.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.Status
{
    public class ModelStatus
    {
        public SensorStatus sensorStatus = new SensorStatus();
        public NetworkStatus networkStatus = new NetworkStatus();
        public TaskManagerStatus taskManagerStatus = new TaskManagerStatus();
        public delegate void Callback(bool value);
        public Callback ArmCallback;
        public Callback AutonomyStatusCallback;
        private bool _autonomyRunning = false;
        public bool AutonomyRunning
        {
            get { return _autonomyRunning; }
            set { _autonomyRunning = value;
                AutonomyStatusCallback?.Invoke(value);
            }
        }
        private bool _armed = false;
        public bool Armed
        {
            get { return _armed; }
            set {
                _armed = value; 
                ArmCallback?.Invoke(value); }
        }


    }
}
