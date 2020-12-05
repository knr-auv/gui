using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.Status
{
    public class NetworkStatus
    {
        public delegate void Callback(bool value);
        public Callback CameraStreamConnectedCallback;
        public Callback ConnectedToJetsonCallback;

        private bool _CameraStreamConnected = false;
        private bool _ConnectedToJetson = false;

        public bool CameraStreamConnected
        {
            get { return _CameraStreamConnected; }
            set { _CameraStreamConnected = value;
                CameraStreamConnectedCallback?.Invoke(value);
            }
        }

        public bool ConnectedToJetson
        {
            get { return _ConnectedToJetson; }
            set { _ConnectedToJetson = value;
                ConnectedToJetsonCallback?.Invoke(value);
            }
        }
        public NetworkStatus()
        {
            
        }
    }
}
