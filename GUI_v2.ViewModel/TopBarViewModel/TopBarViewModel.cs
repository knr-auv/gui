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
        private bool _test = false;
        public bool TestMode
        {
            get { return _test; }
            set { SetProperty(ref _test, value); }
        }
        public TopBarViewModel(ModelContainer modelContainer)
        {
            this.modelContainer = modelContainer;
            SensorStatusViewModel = new SensorStatusViewModel(modelContainer);
            TestMode = false;
        }
    }
}
