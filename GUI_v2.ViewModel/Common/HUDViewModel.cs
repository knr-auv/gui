using GUI_v2.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel.Common
{
    public class HUDViewModel:BaseViewModel
    {
        private MovementInfoClass _attitude;
        public MovementInfoClass Attitude
        {
            get { return _attitude; }
            set { SetProperty(ref _attitude, value); }
        }
        public void UpdateAttitude(float x, float y , float z)
        {
            Attitude = new MovementInfoClass();
            Attitude.UpdateInfo(x, y, z);
           // RaisePropertyChanged("Attitude");
        }
        public HUDViewModel()
        {
            Attitude = new MovementInfoClass();
        }
    }
}
