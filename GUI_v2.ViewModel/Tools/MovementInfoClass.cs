using GUI_v2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Tools
{
    public class MovementInfoClass:BaseViewModel
    {
        public void UpdateInfo(double X, double Y, double Z)
        {
            x = X;y = Y;z = Z;
        }
        private double _x = 0;
        public double x
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }
        private double _y = 0;
        public double y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }
        private double _z = 0;
        public double z
        {
            get { return _z; }
            set { SetProperty(ref _z, value); }
        }
    }
}
