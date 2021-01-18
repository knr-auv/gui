using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_GUI_v2.Model.DataContainer
{
    public class MotorsData
    {
        private float[] data;
        public delegate void NewDataCallback();
        public NewDataCallback newDataCallback;
        public float[] Data 
        {
            get { return data; }
            set { data = value;
                newDataCallback?.Invoke();
            }
        }
        public void UpdateData(float[] data)
        {
            Data = data;
        }
    }
}
