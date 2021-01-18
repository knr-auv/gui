
using GUI_v2.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using GUI_v2.ViewModel.IMUViewModels;
using System.Windows.Media.Media3D;
using GUI_v2.ViewModel.Common;
using GUI_v2.Model.JetsonClient;

namespace GUI_v2.ViewModel
{
    public partial class IMUViewModel : BaseViewModel
    {
        public override void Hide()
        {
            modelContainer.dataContainer.Velocity.newDataCallback -= MovementViewModel.Velocity.UpdateInfo;
            modelContainer.dataContainer.Acceleration.newDataCallback -= MovementViewModel.Acceleration.UpdateInfo;
            modelContainer.dataContainer.Position.newDataCallback -= MovementViewModel.Position.UpdateInfo;
        }
        public override void Show()
        {
            modelContainer.cameraStreamClient.StartStream(CameraViewModel.SwapImage, CameraViewModel.SetLogo);
            modelContainer.dataContainer.Velocity.newDataCallback += MovementViewModel.Velocity.UpdateInfo;
            modelContainer.dataContainer.Acceleration.newDataCallback += MovementViewModel.Acceleration.UpdateInfo;
            modelContainer.dataContainer.Position.newDataCallback += MovementViewModel.Position.UpdateInfo;

        }


      
        private readonly ModelContainer modelContainer;

        public IMUViewModel(ModelContainer modelContainer)
        {
            this.modelContainer = modelContainer;

            AttitudePlotViewModel = new PlotViewModel("Attitude",3,100);//, true, v => v + "°", "roll", "pitch", "yaw");
            GyroPlotViewModel = new PlotViewModel("Gyro",3, 20);//, false, v => v + "°", "x", "y", "z");
            MagPlotViewModel = new PlotViewModel("Mag",3, 20);//, false, v => v + "°", "x", "y", "z");
            AccPlotViewModel = new PlotViewModel("Acc",3, 20);//, false, v => v + "°", "x", "y", "z");
            DepthPlotViewModel = new PlotViewModel("Depth",1, 20,-4,0);//, false, v => v * -1 + "m", "Depth");

            MovementViewModel = new MovementInfoViewModel();
            CameraViewModel = new CameraStreamViewModel();
            PIDViewModel = new PIDViewModel(modelContainer);

            modelContainer.jetsonClient.callbacks.PIDCallback += PIDViewModel.UpdatePIDS;
            modelContainer.modelStatus.networkStatus.CameraStreamConnectedCallback += CameraStreamStatusChangedCallback;
            modelContainer.dataContainer.Attitude.newDataCallback += (float x, float y, float z) => { float[] a = { x, y, z }; AttitudePlotViewModel.UpdatePlotData(a); };
            modelContainer.dataContainer.Acc.newDataCallback += (float x, float y, float z) => { float[] a = { x, y, z }; AccPlotViewModel.UpdatePlotData(a); };
            modelContainer.dataContainer.Gyro.newDataCallback += (float x, float y, float z) => { float[] a = { x, y, z }; GyroPlotViewModel.UpdatePlotData(a); };
            modelContainer.dataContainer.Mag.newDataCallback += (float x, float y, float z) => { float[] a = { x, y, z }; MagPlotViewModel.UpdatePlotData(a); };
            modelContainer.dataContainer.Baro.newDataCallback += (float value) => { float[] a = { value }; DepthPlotViewModel.UpdatePlotData(a); };

        }
        private void CameraStreamStatusChangedCallback(bool val)
        {
            if (val == false)
            {
                CameraViewModel.SetLogo();
            }
        }
    }
}
        