using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.DataContainer.Sensors
{
    public class BatterySensor
    {
        private float bat1 = 0;
        private float bat2 = 0;

        public delegate void NewDataCallback(float bat1, float bat2);
        public NewDataCallback newDataCallback;
        public void UpdateData(float[] data)
        {
            bat1 = data[0];
            bat2 = data[1];
            newDataCallback?.Invoke(bat1, bat2);
        }
    }
}
