using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GUI_v2.View.Common
{
    /// <summary>
    /// Interaction logic for BatteryWidget.xaml
    /// </summary>
    public partial class BatteryWidget : UserControl
    {
        private Color color1;
        private Color color2;
        private Color color3;
        private bool IsBatteryLow = false;
        private Storyboard animation;
        public BatteryWidget()
        {

            InitializeComponent();
            color1 = (Color)ColorConverter.ConvertFromString("#FF00FF17");
            color2 = (Color)ColorConverter.ConvertFromString("#FFFFB900");
            color3 = (Color)ColorConverter.ConvertFromString("#FFD80000");

            animation = (Storyboard)FindResource("BlinkAnimation");
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.AutoReverse = true;
        }

        private static void BatteryCriticalLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            BatteryLevelChanged(d, e);
        }
        private double prev_diff=0;
        public static void BatteryLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
            BatteryWidget obj = (BatteryWidget)d;
            double value = 1 - (double)obj.BatteryLevel / 100;
            obj.grad1.Offset = value;
            obj.grad2.Offset = value + 0.01;
            obj.BatPath.Fill = new SolidColorBrush(GradientPick(value, obj.color1, obj.color2, obj.color3));
            
            if (obj.BatteryLevel <= obj.BatteryCritical&&!obj.IsBatteryLow)
            {
                obj.animation.Duration = new Duration(TimeSpan.FromSeconds(3));
                //obj.animation.AutoReverse = true;
              //  obj.animation.RepeatBehavior = RepeatBehavior.Forever;
                obj.IsBatteryLow = true;
                obj.animation.Begin();
            }
            else if (obj.BatteryLevel > obj.BatteryCritical && obj.IsBatteryLow)
            {
                obj.animation.Stop();
                obj.IsBatteryLow = false;
            }
            if (obj.IsBatteryLow)
            {
                double dif = 1 - (obj.BatteryCritical - obj.BatteryLevel) / obj.BatteryCritical;

                if (Math.Abs(dif - obj.prev_diff) < 0.1)
                    return;
                obj.prev_diff = dif;
                obj.animation.Stop();
                obj.animation.Duration = new Duration(TimeSpan.FromSeconds(dif*1));
               // obj.animation.AutoReverse = false;
            //    obj.animation.FillBehavior = FillBehavior.Stop;
               // obj.animation.RepeatBehavior = RepeatBehavior.Forever;

                obj.animation.Begin();

            }

        }



        private static int LinearInterp(int start, int end, double percentage) => start + (int)Math.Round(percentage * (end - start));

        private static Color ColorInterp(Color start, Color end, double percentage) =>
            Color.FromArgb((byte)LinearInterp(start.A, end.A, percentage),
                           (byte)LinearInterp(start.R, end.R, percentage),
                           (byte)LinearInterp(start.G, end.G, percentage),
                           (byte)LinearInterp(start.B, end.B, percentage));

        private static Color GradientPick(double percentage, Color Start, Color Center, Color End)
        {
            if (percentage < 0.5)
                return ColorInterp(Start, Center, percentage / 0.5);
            else if (percentage == 0.5)
                return Center;
            else
                return ColorInterp(Center, End, (percentage - 0.5) / 0.5);
        }

        public double BatteryLevel
        {
            get { return (double)GetValue(BatteryLevelProperty); }
            set { SetValue(BatteryLevelProperty, value);
                
            }
        }
        public double BatteryCritical
        {
            get { return (double)GetValue(BatteryCriticalProperty); }
            set { SetValue(BatteryCriticalProperty, value); }
        }
    

        public static readonly DependencyProperty BatteryCriticalProperty =
           DependencyProperty.Register("BatteryCritical",
               typeof(double),
               typeof(BatteryWidget),
               new UIPropertyMetadata((double)0, BatteryCriticalLevelChanged));

 

        public static readonly DependencyProperty BatteryLevelProperty =
           DependencyProperty.Register("BatteryLevel",
               typeof(double), 
               typeof(BatteryWidget),
               new UIPropertyMetadata((double)0, BatteryLevelChanged));
    }
}
