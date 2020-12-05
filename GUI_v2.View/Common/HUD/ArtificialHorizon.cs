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
    class ArtificialHorizon :  HUDElementBase
    {
         
        private Line HorizonLine;
        private Line LeftVerticalLine;
        private Line RightVerticalLine;

        public ArtificialHorizon(int x, int y, int width, int height, Canvas parent):base(x,y,width,height,parent)
        {
            Show();
        }
        public void Show()
        {
            HorizonLine = new Line();
            LeftVerticalLine = new Line();
            RightVerticalLine = new Line();
            HorizonLine.StrokeThickness = 2;
            LeftVerticalLine.StrokeThickness = 2;
            RightVerticalLine.StrokeThickness = 2;

            HorizonLine.Stroke = Brushes.Green;
            LeftVerticalLine.Stroke = Brushes.Red;
            RightVerticalLine.Stroke = Brushes.Green;

            parent.Children.Add(LeftVerticalLine);
            parent.Children.Add(RightVerticalLine);
            parent.Children.Add(HorizonLine);
            redraw();
        }
        public void redraw()
        {
            double[] center = GridToCnv();
            double LeftX = center[0] - center[2] / 2;
            double TopY = center[1] - center[3] / 2;
            double RightX = center[0] + center[2] / 2;
            double BottomY = center[1] + center[3] / 2;
            HorizonLine.X1 = RightX;
            HorizonLine.Y1 = center[1];
            HorizonLine.X2 = LeftX;
            HorizonLine.Y2 = center[1];


            LeftVerticalLine.X1 = LeftX;
            LeftVerticalLine.Y1 = TopY;
            LeftVerticalLine.X2 = LeftX;
            LeftVerticalLine.Y2 = BottomY;

            RightVerticalLine.X1 = RightX;
            RightVerticalLine.Y1 = TopY;
            RightVerticalLine.X2 = RightX;
            RightVerticalLine.Y2 = BottomY;
        }
    }
}

