using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel
{
    public partial class StatusViewModel : BaseViewModel
    {
        private double m1, m2, m3, m4, m5, m6;

        public double M1 {
            get { return m1; }
            set { m1 = value; handleMotorSliderChange(); }
        }
        public double M2
        {
            get { return m2; }
            set { m2 = value; handleMotorSliderChange(); }
        }
        public double M3
        {
            get { handleMotorSliderChange(); return m3; }
            set { m3 = value; handleMotorSliderChange(); }
        }
        public double M4
        {
            get { return m4; }
            set { m4 = value; handleMotorSliderChange(); }
        }
        public double M5
        {
            get { return m5; }
            set { m5 = value; handleMotorSliderChange(); }
        }
        public double M6
        {
            get { return m6; }
            set { m6 = value; handleMotorSliderChange(); }
        }
    }
}
