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
        public ObservableCollection<Detection> ObjectList { 
            get 
            { 
                if (modelContainer.dataContainer.detections.ObjectList != null)
                    return new ObservableCollection<Detection>(modelContainer.dataContainer.detections.ObjectList);
                else 
                    return new ObservableCollection<Detection>();
                } 
            set { } }
        public ObservableCollection<Detection> CurrentDetections { get {
                if (modelContainer.dataContainer.detections.LastDetectionList != null)
                    return new ObservableCollection<Detection>(modelContainer.dataContainer.detections.LastDetectionList);
                else
                    return new ObservableCollection<Detection>();
            }
            set { }
        }

        public void HandleNewDetection()
        {
            RaisePropertyChanged("FPS");
            RaisePropertyChanged("ObjectList");
            RaisePropertyChanged("CurrentDetections");
        }



        public DetectionListViewModel(ModelContainer modelContainer)
        {
            this.modelContainer = modelContainer;

        }

    }
}
