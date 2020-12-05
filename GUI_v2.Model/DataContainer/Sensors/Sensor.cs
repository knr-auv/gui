using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.DataContainer.Sensors
{
    public class Sensor
    {
        private double _x =0;
        private double _y = 0;
        private double _z = 0;
        private double _norm = 0;

        public double norm
        {
            get { return _norm; }
            set { _norm = value; }
        }
        public double x
        {
            get { return _x; }
            set { _x = value; }
        }

        public double y
        {
            get { return _y; }
            set { _y = value; }
        }
        public double z
        {
            get { return _z; }
            set { _z = value; }
        }

        
        public delegate void NewDataCallback(double x, double y, double z);
        public NewDataCallback newDataCallback;
        public delegate void Cb(double x);
        public Cb newNormCallback;
        public void UpdateData(double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
            norm = Math.Sqrt(x * x + y * y + z * z);
            newDataCallback?.Invoke(x, y, z);
            newNormCallback?.Invoke(norm);
        }


    }
}
