using GUI_v2.Tools;
using GUI_v2.View.Common.HUD;
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

namespace GUI_v2.View.Common.HUD
{
    /// <summary>
    /// Interaction logic for HUD.xaml
    /// </summary>
    public partial class HUD : UserControl
    {
        public ArtificialHorizon horizon;
        public Heading heading;


        public HUD()
        {
            InitializeComponent();
            horizon = new ArtificialHorizon(50, 50, 30, 50, cnv);
            heading = new Heading(50, 5, 20, 3, cnv);
            
        }

        public MovementInfoClass Attitude
        {
            get { return (MovementInfoClass)GetValue(AttitudeProperty); }
            set { SetValue(AttitudeProperty, value); }
        }
        public static readonly DependencyProperty AttitudeProperty =

      DependencyProperty.Register("Attitude",
          typeof(MovementInfoClass),
          typeof(HUD),
          new UIPropertyMetadata(null, AttitudeChanged));


        public static void AttitudeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HUD obj = (HUD)d;
            obj.horizon.Update(obj.Attitude.x, obj.Attitude.y);
        }
       

        private void cnv_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var c = GetValue(AttitudeProperty);
            horizon.redraw();
            heading.redraw();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var b = new Binding("Attitude");
            b.Source = DataContext;
         
            SetBinding(AttitudeProperty, b);
        }
    }
}
