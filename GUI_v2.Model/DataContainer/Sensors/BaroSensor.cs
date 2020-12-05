using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.DataContainer.Sensors
{
    public class BaroSensor
    {
        private double value = 0;
        public delegate void NewDataCallback(double value);
        public NewDataCallback newDataCallback;

        public void UpdateData(double data)
        {
            value = data;
            newDataCallback?.Invoke(value);
        }
    }
}
