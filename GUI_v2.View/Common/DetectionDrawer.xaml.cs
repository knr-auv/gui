using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DetectionDrawer.xaml
    /// </summary>
    public partial class DetectionDrawer : UserControl
    {
        public DetectionDrawer()
        {
            InitializeComponent();
        }
        private void drawRectangle(float [] box)
        {
            Line top = new Line();
            Line bottom = new Line();
            Line left = new Line();
            Line right = new Line();
            if (box[0] >= 1)
                box[0] = 0.99f;
            if (box[1] >= 1)
                box[1] = 0.99f;
            if (box[2] >= 1)
                box[2] = 0.99f;
            if (box[3] >= 1)
                box[3] = 0.99f;
            if (box[0] <= 0)
                box[0] = 0.01f;
            if (box[1] <= 0)
                box[1] = 0.01f;
            if (box[2] <= 0)
                box[2] = 0.01f;
            if (box[3] <= 0)
                box[3] = 0.01f;

            //minX
            top.X1 = left.X1 = left.X2 = bottom.X1 = box[0] * cnv.ActualWidth;
            //maxX
            top.X2 = right.X1 = right.X2 = bottom.X2 = box[2] * cnv.ActualWidth;
            //minY
            left.Y1 = bottom.Y1 = bottom.Y2 = right.Y1 =(1- box[1]) * cnv.ActualHeight;
            //maxY
            left.Y2 = top.Y1 = top.Y2 = right.Y2 = (1-box[3]) * cnv.ActualHeight;

            top.Stroke =  left.Stroke=right.Stroke= bottom.Stroke= Brushes.LightGreen;
            top.StrokeThickness = left.StrokeThickness = right.StrokeThickness = bottom.StrokeThickness = 2;


            cnv.Children.Add(top);
            cnv.Children.Add(bottom);
            cnv.Children.Add(left);
            cnv.Children.Add(right);
        }

        public void drawBoundingBoxes()
        {
            cnv.Children.Clear();
            if (BoundingBoxes == null)
                return;
            foreach (float[] i in BoundingBoxes)
            {
                drawRectangle(i);
            }
        }

        public ObservableCollection<float[]> BoundingBoxes
        {
            get { return (ObservableCollection<float[]>)GetValue(BoundingBoxesProperty); }
            set { SetValue(BoundingBoxesProperty, value); }
        }

        public static readonly DependencyProperty BoundingBoxesProperty =
          DependencyProperty.Register("BoundingBoxes",
              typeof(ObservableCollection<float[]>),
              typeof(DetectionDrawer),
              new UIPropertyMetadata(null, HandleNewBoxes));


        private static void HandleNewBoxes(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var o = (DetectionDrawer)d;
            o.drawBoundingBoxes();
        }

        private void cnv_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(BoundingBoxes!=null)
            drawBoundingBoxes();
        }
    }
}
