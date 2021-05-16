using GUI_v2.ViewModel.TopBarViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel
{
    public class TopBarViewModel:BaseViewModel
    {
        public SensorStatusViewModel SensorStatusViewModel { get; set; }
        private ModelContainer modelContainer;

        public bool RealMode
        {
            get { bool ret = (modelContainer.modelStatus.Mode == "jetson_stm");
                return ret; }
            set {; }
        }
        public TopBarViewModel(ModelContainer modelContainer)
        {
            this.modelContainer = modelContainer;
            SensorStatusViewModel = new SensorStatusViewModel(modelContainer);
            modelContainer.modelStatus.ModeChangedCallback += (string v) => { RaisePropertyChanged("RealMode"); };
        }
    }
}
