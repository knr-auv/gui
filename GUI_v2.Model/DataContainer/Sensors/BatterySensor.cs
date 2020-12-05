using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.DataContainer.Sensors
{
    public class BatterySensor
    {
        private double bat1 = 0;
        private double bat2 = 0;

        public delegate void NewDataCallback(double bat1, double bat2);
        public NewDataCallback newDataCallback;
        public void UpdateData(double[] data)
        {
            bat1 = data[0];
            bat2 = data[1];
            newDataCallback?.Invoke(bat1, bat2);
        }
    }
}
