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
        public void UpdateInfo(float X, float Y, float Z)
        {
            x = X;y = Y;z = Z;
        }
        private float _x = 0;
        public float x
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }
        private float _y = 0;
        public float y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }
        private float _z = 0;
        public float z
        {
            get { return _z; }
            set { SetProperty(ref _z, value); }
        }
    }
}
