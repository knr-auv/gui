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
        ArtificialHorizon horizon;
        Heading heading;
        
        public HUD()
        {
            InitializeComponent();
            horizon = new ArtificialHorizon(50, 50, 50, 50, cnv);
            heading = new Heading(50, 5, 20, 3, cnv);
           
        }



        private void cnv_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            horizon.redraw();
            heading.redraw();
        }

  


    }
}
