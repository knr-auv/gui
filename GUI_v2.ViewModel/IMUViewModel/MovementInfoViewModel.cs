using GUI_v2.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel.IMUViewModels
{
    public class MovementInfoViewModel:BaseViewModel
    {
        private MovementInfoClass _Acceleration = new MovementInfoClass();
        private MovementInfoClass _Velocity = new MovementInfoClass();
        private MovementInfoClass _Position = new MovementInfoClass();

        public MovementInfoClass Position
        {
            get { return _Position; }
            set { SetProperty(ref _Position, value); }
        }
        public MovementInfoClass Velocity
        {
            get { return _Velocity; }
            set { SetProperty(ref _Velocity, value); }
        }
        public MovementInfoClass Acceleration
        {
            get { return _Acceleration; }
            set { SetProperty(ref _Acceleration, value); }
        }
    }
}
