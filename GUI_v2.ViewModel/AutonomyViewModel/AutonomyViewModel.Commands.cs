using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_v2.ViewModel
{
    public partial class AutonomyViewModel : BaseViewModel
    {
        public ICommand StartAutonomyCommand { get; private set; }
        public ICommand StopAutonomyCommand { get; private set; }

        private void StartAutonomyAction(object p)
        {
            modelContainer.jetsonClient.StartAutonomy();
        }

        private void StopAutonomyAction(object p)
        {
            modelContainer.jetsonClient.StopAutonomy();
        }
        private bool Allow(object p)
        {
            return true;
        }
            
    }
}
