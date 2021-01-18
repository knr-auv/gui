using PC_GUI_v2.Model.DataContainer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;


namespace GUI_v2.ViewModel.Common
{

    public class DetectionListViewModel:BaseViewModel
    {
        private ModelContainer modelContainer;
        public double FPS
        {
            get { return modelContainer.dataContainer.detections.FPS; }
            set {; }
        }
        private ObservableCollection<Detection> _ObjectList;
        public ObservableCollection<Detection> ObjectList { 
            get 
            {
                PrepareToView();
                return _ObjectList;
                } 
            set { } }


        public void PrepareToView()
        {
            _ObjectList.Clear();
            foreach (var i in modelContainer.dataContainer.detections.ObjectList)
            {
                _ObjectList.Add(i);
            }
        }

        public void HandleNewDetection()
        {
            RaisePropertyChanged("FPS");
            RaisePropertyChanged("ObjectList");
        }

       


        public DetectionListViewModel(ModelContainer modelContainer)
        {
            this.modelContainer = modelContainer;
            _ObjectList = new ObservableCollection<Detection>();

        }

    }
}
