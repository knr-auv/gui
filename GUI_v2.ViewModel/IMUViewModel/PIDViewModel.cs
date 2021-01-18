using GUI_v2.Tools;
using GUI_v2.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_v2.ViewModel.IMUViewModels
{
    public class PIDViewModel:BaseViewModel
    { 
        public ICommand ResetBtnClicked { get; set; }
        public ICommand SetBtnClicked { get; set; }
        private void SetBtnAction(object p)
        {
            var argList = new List<double>();
            argList.AddRange(PID_roll.GetPID());
            argList.AddRange(PID_pitch.GetPID());
            argList.AddRange(PID_yaw.GetPID());
            argList.AddRange(PID_depth.GetPID());
            ModelContainer.jetsonClient.SendPIDs(argList.ToArray());
        }
        private void ResetBtnAction(object p)
        {   
            ModelContainer.jetsonClient.GetPIDs();
        }

        private ModelContainer ModelContainer;
        public void UpdatePIDS(float[] data)
        {
            if (data.Length != 16) {
                Console.WriteLine("PID data error");
                return;
            }
                
            PID_roll.Update(data, 0);
            PID_pitch.Update(data, 4);
            PID_yaw.Update(data, 8);
            PID_depth.Update(data, 12);
        }
        public PIDViewModel(ModelContainer modelContainer)
        {
            ModelContainer = modelContainer;
            SetBtnClicked = new RelayCommand(SetBtnAction, (object e) => true);
            ResetBtnClicked = new RelayCommand(ResetBtnAction, (object e) => true);
        }
    private PIDClass _PID_roll = new PIDClass();
    private PIDClass _PID_pitch = new PIDClass();
    private PIDClass _PID_yaw = new PIDClass();
    private PIDClass _PID_depth = new PIDClass();

    public PIDClass PID_roll
    {
        get { return _PID_roll; }
        set { SetProperty(ref _PID_roll, value); }
    }
    public PIDClass PID_pitch
    {
        get { return _PID_pitch; }
        set { SetProperty(ref _PID_pitch, value); }
    }
    public PIDClass PID_yaw
    {
        get { return _PID_yaw; }
        set { SetProperty(ref _PID_yaw, value); }
    }
    public PIDClass PID_depth
    {
        get { return _PID_depth; }
        set { SetProperty(ref _PID_depth, value); }
    }
    
    }
}
