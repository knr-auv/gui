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

namespace GUI_v2.View.StatusView
{
    /// <summary>
    /// Interaction logic for MotorSlider.xaml
    /// </summary>
    public partial class MotorSlider : UserControl
    {
        public MotorSlider()
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
               typeof(MotorSlider));/*,
               new UIPropertyMetadata((double)0, MotorValueChanged));

        private static void MotorValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MotorSlider)d;
            double val = (double)e.NewValue;
            
        }
        */
    }
}
