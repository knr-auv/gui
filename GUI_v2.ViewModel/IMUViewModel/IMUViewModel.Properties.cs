using GUI_v2.ViewModel.Common;
using GUI_v2.ViewModel.IMUViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel
{
    public partial class IMUViewModel : BaseViewModel
    {
        public MovementInfoViewModel MovementViewModel { get; set; }
        public PIDViewModel PIDViewModel { get; set; }
        public CameraStreamViewModel CameraViewModel { get; set; }
        public PlotViewModel AttitudePlotViewModel { get; set; }
        public PlotViewModel GyroPlotViewModel { get; set; }
        public PlotViewModel MagPlotViewModel { get; set; }
        public PlotViewModel AccPlotViewModel { get; set; }
        public PlotViewModel DepthPlotViewModel { get; set; }
    }
}
