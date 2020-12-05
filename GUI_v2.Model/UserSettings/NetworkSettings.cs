using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.UserSettings
{
    public class NetworkSettings
    {
        public string JetsonIP { get; set; }
        public int VideoStreamPort { get; set; }
        public int ControlPort { get; set; }

        public NetworkSettings()
        {
            JetsonIP = "localhost";
            ControlPort = 8080;
            VideoStreamPort = 8090;
        }
    }
}
