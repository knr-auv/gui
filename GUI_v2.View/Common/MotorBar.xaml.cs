using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_v2.View.Common
{
    /// <summary>
    /// Interaction logic for MotorBar.xaml
    /// </summary>
    public partial class MotorBar : UserControl
    {
        public MotorBar()
        {
            InitializeComponent();
        }

        public double MotorValue
        {
            get { return (double)GetValue(MotorValueProperty); }
            set { SetValue(MotorValueProperty, value); }
        }
        public static readonly DependencyProperty MotorValueProperty =
           DependencyProperty.Register("MotorValue",
               typeof(double),
               typeof(MotorBar),
               new UIPropertyMetadata((double)0, MotorValueChanged));

        private static void MotorValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MotorBar)d;
            double val = (double)e.NewValue;
            if (val > 0)
            {
                obj.up1.Offset = obj.up2.Offset = val / 1000;
                obj.down1.Offset = obj.down2.Offset = 0;
            }else if (val < 0)
            {
                obj.up1.Offset = obj.up2.Offset = 0;
                obj.down1.Offset = obj.down2.Offset = -val / 1000;
            }
            else
            {
                obj.up1.Offset = obj.up2.Offset = 0;
                obj.down1.Offset = obj.down2.Offset = 0;
            }
        }
    }
}
