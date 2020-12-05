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

namespace GUI_v2.View.TopBar
{
    /// <summary>
    /// Interaction logic for HummidityIcon.xaml
    /// </summary>
    public partial class HummidityIcon : UserControl
    {
        public HummidityIcon()
        {
            InitializeComponent();
        }

        public double HummidityLevel
        {
            get { return (double)GetValue(HummidityLevelProperty); }
            set { SetValue(HummidityLevelProperty, value); }
        }
        public static readonly DependencyProperty HummidityLevelProperty =
          DependencyProperty.Register("HummidityLevel",
              typeof(double),
              typeof(HummidityIcon),
              new UIPropertyMetadata((double)0, HummidityLevelChanged));

        private static void HummidityLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HummidityIcon obj = (HummidityIcon)d;
            double val = (1 - obj.HummidityLevel/100);
            obj.grad1.Offset = obj.grad2.Offset = val;
            obj.HumLabel.Content = obj.HummidityLevel;
        }
    }
}
