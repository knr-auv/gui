
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GUI_v2.ViewModel.IMUViewModels
{
    public class PlotViewModel:BaseViewModel
    {
        public PlotModel PlotModel { get; private set; }

        private DateTime time;
        private int maxDataPoints;
        private OxyPlot.Wpf.BarSeries b;
        private void initSeries(int n)
        {
            if (n == 3)
            {
                var a = new LineSeries { LineStyle = LineStyle.Solid, Title = "Roll", Color = OxyColors.Red };
                var b = new LineSeries { LineStyle = LineStyle.Solid, Title = "Pitch", Color = OxyColors.Orange };
                var c = new LineSeries { LineStyle = LineStyle.Solid, Title = "Yaw", Color = OxyColors.Blue };
                a.TrackerKey = "InvisibleTracker";
                b.TrackerKey = "InvisibleTracker";
                c.TrackerKey = "InvisibleTracker";
                PlotModel.Series.Add(a);
                PlotModel.Series.Add(b);
                PlotModel.Series.Add(c);
            }
            if (n == 1)
            {
                var a = new LineSeries { LineStyle = LineStyle.Solid, Title = "Depth", Color = OxyColors.Orange };
                a.TrackerKey = "InvisibleTracker";
                PlotModel.Series.Add(a);
            }
            
            
        }
        private void initPlot(string t, int n)
        {
            PlotModel = new PlotModel();
            
            PlotController plotController = new PlotController();
            PlotModel.Title = t;
            PlotModel.TitleFontWeight = OxyPlot.FontWeights.Normal;
            PlotModel.TitleFontSize = 21;
            var x = (Color)Application.Current.FindResource("OnSurfaceColor");
            var c = OxyColor.FromArgb(x.A, x.R, x.G, x.B);

            PlotModel.TitleColor = c;
            if (t == "Attitude")
            {
                PlotModel.LegendPlacement = LegendPlacement.Outside;
                PlotModel.LegendPosition = LegendPosition.TopCenter;
                PlotModel.LegendOrientation = LegendOrientation.Horizontal;
                PlotModel.LegendBorderThickness = 0;
                
            }
            else
                PlotModel.IsLegendVisible = false;
            PlotModel.PlotAreaBorderColor = OxyColors.Transparent;
            initSeries(n);
        }
        private void initAxis(bool set, double minValue, double maxValue)
        {
            var xAxis = new LinearAxis { Position = AxisPosition.Bottom, AxislineStyle = LineStyle.Solid };
            var yAxis = new LinearAxis { Position = AxisPosition.Left, AxislineStyle = LineStyle.Solid };
            xAxis.IsZoomEnabled = false;
            xAxis.IsPanEnabled = false;
            yAxis.IsZoomEnabled = false;
            yAxis.IsPanEnabled = false;

            yAxis.AxislineColor = OxyColors.Transparent;
            yAxis.MajorGridlineStyle = LineStyle.Solid;
            yAxis.MajorGridlineThickness = 2;

            yAxis.MinorTickSize = 0;
       
            var x = (Color)Application.Current.FindResource("OnSurfaceColor");
            var y = (double)Application.Current.FindResource("SurfaceOpacity");
            var c= OxyColor.FromArgb(x.A, x.R, x.G, x.B);
            x.A = (byte)((byte)255*y);
            var d = OxyColor.FromArgb(x.A, x.R, x.G, x.B); 
            yAxis.TicklineColor =OxyColors.Transparent;
            yAxis.MajorGridlineColor = d;
            yAxis.TextColor = c;
            
            xAxis.IsAxisVisible = false;
            if (set)
            {
                yAxis.Maximum = -minValue;
                yAxis.Minimum = maxValue;
                yAxis.StringFormat = "0m";
                yAxis.MajorStep = 1;
                yAxis.MinorTickSize = 0;
                yAxis.StartPosition = 1;
                yAxis.EndPosition = 0;
            }
            else
                yAxis.StringFormat = "0°";
            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);
        }

        public PlotViewModel(string Title, int NumberOfSeries, int MaxDataPoints, double minValue, double maxValue)
        {
            maxDataPoints = MaxDataPoints;
            initPlot(Title, NumberOfSeries);
            initAxis(true, minValue, maxValue);
            time = DateTime.Now;
        }


        public PlotViewModel(string Title, int NumberOfSeries, int MaxDataPoints)
        {
            maxDataPoints = MaxDataPoints;
            initPlot(Title, NumberOfSeries);
            initAxis(false, 0, 0);
        
            time = DateTime.Now;
        }
        private int c = 0;
        public void UpdatePlotData(double[] data)
        {
            double max = 0;
            double min = 0;
            for (var i = 0; i < PlotModel.Series.Count; i++)
            {

                double y = (DateTime.Now - time).TotalSeconds;
                var series = (LineSeries)PlotModel.Series[i];
                
                if (series.MaxY > max)
                    max = series.MaxY;
                if (series.MinY < min)
                    min = series.MinY;
               
                series.Points.Add(new DataPoint(y, data[i]));
                if (series.Points.Count >= maxDataPoints)
                    series.Points.RemoveAt(0);
                //time = DateTime.Now; ;
            }
            /*
            double range = Math.Abs(max - min);
            if (range > 150)
                PlotModel.Axes[1].MajorStep = 50;
            else if (range > 100)
                PlotModel.Axes[1].MajorStep = 30;
            else if (range > 50)
                PlotModel.Axes[1].MajorStep = 20;
            else if (range > 25)
                PlotModel.Axes[1].MajorStep = 10;
            else if (range > 10)
                PlotModel.Axes[1].MajorStep = 5;
            else if (range > 5)
                PlotModel.Axes[1].MajorStep = 2;
            else if (range > 3)
                PlotModel.Axes[1].MajorStep = 1;
            else if (range > 2)
                PlotModel.Axes[1].MajorStep = 0.5;
            else
                PlotModel.Axes[1].MajorStep = 0.2;*/
            if (c % 2 == 0)
            {
                PlotModel.InvalidatePlot(true);
                c=0;
            }
            else { c++; }

        }
        
    }

}
