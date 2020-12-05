using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.Status
{
    public class SensorStatus
    {

        public delegate void StatusChangedCallback(string values);
        public StatusChangedCallback AccStatusChanged;
        public StatusChangedCallback MagStatusChanged;
        public StatusChangedCallback BaroStatusChanged;
        public StatusChangedCallback GyroStatusChanged;

        private string _AccStatus = "off";
        private string _MagStatus = "off";
        private string _GyroStatus = "off";
        private string _BaroStatus = "off";


        public string AccStatus
        {
            get { return _AccStatus; }
            set
            {
                _AccStatus = value;
                AccStatusChanged?.Invoke(value);
            }
        }
        public string MagStatus
        {
            get { return _MagStatus; }
            set
            {
                _MagStatus = value;
                MagStatusChanged?.Invoke(value);
            }
        }

        public string GyroStatus
        {
            get { return _GyroStatus; }
            set
            {
                _GyroStatus = value;
                GyroStatusChanged?.Invoke(value);
            }
        }
        public string BaroStatus
        {
            get { return _BaroStatus; }
            set
            {
                _BaroStatus = value;
                BaroStatusChanged?.Invoke(value);
            }
        }
    }
}
