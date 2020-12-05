using GUI_v2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Tools
{
    public class PIDClass:BaseViewModel
    {
        private double _Kp = 0;
        public double[] GetPID()
        {
            double[] ret = new double[4];
            ret[0] = Kp;
            ret[1] = Ki;
            ret[2] = Kd;
            ret[3] = L;
            return ret;
        }
        public void Update(double[] data, int offset)
        {
            Kp = data[offset];
            Ki = data[offset + 1];
            Kd = data[offset + 2];
            L = data[offset + 3];
        }
        public double Kp
        {
            get { return _Kp; }
            set { SetProperty(ref _Kp, value); }
        }
        private double _Ki = 0;
        public double Ki
        {
            get { return _Ki; }
            set { SetProperty(ref _Ki, value); }
        }
        private double _Kd = 0;
        public double Kd
        {
            get { return _Kd; }
            set { SetProperty(ref _Kd, value); }
        }
        private double _L = 0;
        public double L
        {
            get { return _L; }
            set { SetProperty(ref _L, value); }
        }
    }
}
