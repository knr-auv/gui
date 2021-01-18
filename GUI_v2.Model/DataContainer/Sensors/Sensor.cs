using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.DataContainer.Sensors
{
    public class Sensor
    {
        private float _x =0;
        private float _y = 0;
        private float _z = 0;
        private float _norm = 0;

        public float norm
        {
            get { return _norm; }
            set { _norm = value; }
        }
        public float x
        {
            get { return _x; }
            set { _x = value; }
        }

        public float y
        {
            get { return _y; }
            set { _y = value; }
        }
        public float z
        {
            get { return _z; }
            set { _z = value; }
        }

        
        public delegate void NewDataCallback(float x, float y, float z);
        public NewDataCallback newDataCallback;
        public delegate void Cb(float x);
        public Cb newNormCallback;
        public void UpdateData(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
            norm = (float)Math.Sqrt(x * x + y * y + z * z);
            newDataCallback?.Invoke(x, y, z);
            newNormCallback?.Invoke(norm);
        }


    }
}
