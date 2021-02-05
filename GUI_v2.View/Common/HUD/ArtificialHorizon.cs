using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace GUI_v2.View.Common.HUD
{
    public class ArtificialHorizon :  HUDElementBase
    {
        private List<Line> lines;
        private List<TextBlock> marks;
        private Line ref_line;
        private double roll;
        private double pitch;
        private int range = 26;
        public void Update(double roll, double pitch)
        {
            this.roll = roll;
            this.pitch = pitch;
            redraw();
        }

        public ArtificialHorizon(int x, int y, int width, int height, Canvas parent):base(x,y,width,height,parent)
        {
            Show();
        }
        public void Show()
        {
            lines = new List<Line>();
            marks = new List<TextBlock>();
            ref_line = new Line();
       
            
            Update(0, 0);
            redraw();
        }
        public void redraw()
        {
            double[] center = GridToCnv();
            double centerX = center[0];
            double centerY = center[1];
            double width = center[2];
            double height = center[3];
           
            foreach (var l in marks)
                parent.Children.Remove(l);
            foreach (var l in lines)
                parent.Children.Remove(l);

            lines.Clear();
            marks.Clear();

            double len = width/2;
            double span = range / 2 + Math.Abs((int)pitch % 10);
            span *= 2;

            double tanRoll = Math.Tan(roll * Math.PI / 180);
            double A = -tanRoll;
            double B = centerY;
            Func<double, double> fun = (double x) => A * (x - centerX) + B;
            double magic_num =  Math.Sqrt(1 + A * A);
            double dist = 10;
            double upscaler = 0;
            if (width != 0)
                upscaler = 10;// dist / _height * height;
            double pitch_dec = pitch - Math.Truncate(pitch);
            for(int i =-range/2; i<range/2; i++)
            {
                
                int mod_pitch = (int)pitch % 10;
                int v =  i - mod_pitch;
                if (v % 10 == 0 && Math.Abs(v)<range/2)
                {
                    //odsunięcie o moduł z pitch
                    double magic_number2 = mod_pitch+pitch_dec+dist*Math.Floor((v)/dist);
                    magic_number2 *= upscaler;
                    double cos = Math.Cos(roll * Math.PI / 180);
                    double sin = tanRoll * cos;
                    Line l = new Line();
                   
                    l.X1 = centerX - cos * len + magic_number2 * sin;
                    l.X2 = centerX + cos * len + magic_number2 * sin;
                    
                    l.Y1 = fun(l.X1) + magic_number2 * magic_num;
                    l.Y2 = fun(l.X2) + magic_number2 * magic_num;
                    l.Stroke = Brushes.GreenYellow;
                    l.StrokeThickness = 3;
                    lines.Add(l);
                    TextBlock text = new TextBlock();
                    text.Text = (((int)Math.Round((pitch - mod_pitch + i) / 10)) * 10).ToString() + "°";
                    text.Foreground = Brushes.GreenYellow;
                    Canvas.SetLeft(text, l.X1);
                    Canvas.SetTop(text, l.Y1);
                    marks.Add(text);
                    parent.Children.Add(text);
                    parent.Children.Add(l);

                }
               else if (v % 5 == 0 && Math.Abs(v) <= range / 2)
                {
                    //odsunięcie o moduł z pitch
                    double magic_number2 = mod_pitch+pitch_dec + dist/2 * Math.Floor(v / (dist/2));
                    magic_number2 *= upscaler;
                    double cos = Math.Cos(roll * Math.PI / 180);
                    double sin = tanRoll * cos;
                    Line l = new Line();

                    l.X1 = centerX - cos * len/2 + magic_number2 * sin;
                    l.X2 = centerX + cos * len/2 + magic_number2 * sin;

                    l.Y1 = fun(l.X1) + magic_number2 * magic_num;
                    l.Y2 = fun(l.X2) + magic_number2 * magic_num;
                    l.Stroke = Brushes.GreenYellow;
                    l.StrokeThickness = 3;
                    lines.Add(l);
                    parent.Children.Add(l);

                }
            }


            parent.Children.Remove(ref_line);
            ref_line.X1 = center[0] - center[2] / 3;
            ref_line.X2 = center[0] + center[2] / 3;
            ref_line.Y1 = center[1];
            ref_line.Y2 = center[1];
            ref_line.StrokeThickness = 4;
            ref_line.Stroke = Brushes.Yellow;
            parent.Children.Add(ref_line);
        }
    }
}

