using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GUI_v2.View.Common.HUD
{
    public class Heading:HUDElementBase
    {
        private double value;
        private List<Line> lines;
        public Heading(int x, int y, int width, int height, Canvas parent) : base(x, y, width, height, parent)
        {
            lines = new List<Line>();
            value = 0;
            Show();
        }
        public void Show()
        {
            lines.Clear();
            int noLines = 40;
            for(int i = 0; i < noLines; i++)
            {
                Line t = new Line();
                t.Stroke = Brushes.LightGreen;
                t.StrokeThickness = 2;
                lines.Add(t);
                parent.Children.Add(t);
            }
            redraw();
        }


        public void redraw(params double[] heading)
        {
            if (heading.Count() != 0)
                value = heading[0];
            double[] center = base.GridToCnv();
            double lX = center[0] - center[2]/2;
            double tY = center[1] - center[3]/2;
            double width = center[2];
            double heigh = center[3];

            for (int i =0; i < lines.Count; i++)
            {
                lines[i].X1 = lines[i].X2 = lX + width / lines.Count * i;
                lines[i].Y1 = tY;
                lines[i].Y2 = tY + heigh;
            }

        }
    }
}
