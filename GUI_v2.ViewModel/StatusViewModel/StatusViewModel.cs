using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel
{
    public partial class StatusViewModel : BaseViewModel
    {
        public override void Show()
        {

        }
        public override void Hide()
        {

        }
        public StatusViewModel(ModelContainer modelContainer)
        { 
           
        }
        private void handleMotorSliderChange()
        {

            Console.WriteLine(M1.ToString() + M2.ToString());
        }
    }
}
