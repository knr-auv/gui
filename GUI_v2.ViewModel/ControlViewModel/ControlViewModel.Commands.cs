using GUI_v2.Model.JetsonClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_v2.ViewModel
{
    public partial class ControlViewModel : BaseViewModel
    {


        public ICommand ArmCommand { get; private set; }
        public ICommand DisarmCommand { get; private set; }
        public ICommand DetectionBtnClickedCommand { get; private set; }
        private string selectedController = null;
        private Controller.ControllerBase  currentController;
        private void ArmedCallback()
        {
            if (selectedController == "Keyboard")
                currentController = modelContainer.keyboardController;
            currentController.StartController(modelContainer.jetsonClient.SendSteering);
            modelContainer.jetsonClient.callbacks.ArmConfirmation -= ArmedCallback;
        }

        private void DetectionBtnClickedAction(object parameter)
        {
            if (!(bool)parameter)
            {
                // modelContainer.jetsonClient.callbacks.IMUDataCallback += (double[] data) => Velocity.UpdateInfo(data[9], data[10], data[11]);
                modelContainer.dataContainer.Position.newDataCallback += Position.UpdateInfo;

                modelContainer.dataContainer.detections.newDetectionsCallback += DetectionListViewModel.HandleNewDetection;
                modelContainer.jetsonClient.StartDetections();
            }
            else
            {
                modelContainer.jetsonClient.StopDetections();
                //   modelContainer.jetsonClient.callbacks.IMUDataCallback -= (double[] data) => Velocity.UpdateInfo(data[9], data[10], data[11]);
                modelContainer.dataContainer.Position.newDataCallback -= Position.UpdateInfo;
                modelContainer.dataContainer.detections.newDetectionsCallback -= DetectionListViewModel.HandleNewDetection;
            }
        }

        private void ArmAction(object parameter)
        {
            selectedController = (string)parameter;
            modelContainer.jetsonClient.Arm();
            modelContainer.jetsonClient.callbacks.ArmConfirmation += ArmedCallback;
        }
        private void DisarmAction(object parameter)
        {
            currentController.StopController();
            modelContainer.jetsonClient.Disarm();
        }
        private bool CanArmAction(object parameter)
        {
            return true;
        }
        private bool CanDisarmAction(object parameter)
        {
            return true;
        }
    }
}
