using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;

namespace GUI_v2.View.IMUView.Plots
{
    /// <summary>
    /// Interaction logic for Plot.xaml
    /// </summary>
    public partial class Plot : UserControl
    {
        public Plot()
        {
            InitializeComponent();
            this.IsVisibleChanged += Plot_IsVisibleChanged;
        }

        private void Plot_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var obj = (UserControl)sender;
            if(obj.IsVisible==true)
                Plott.Visibility = Visibility.Visible;
            else
                Plott.Visibility = Visibility.Collapsed;
        }
    }
}
