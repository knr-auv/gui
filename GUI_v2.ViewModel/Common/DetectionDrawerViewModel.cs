using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUI_v2.ViewModel.Common
{
    public class DetectionDrawerViewModel:BaseViewModel
    {
        public class Detection
        {
            public double minX;
            public double minY;
            public double maxX;
            public double maxY;
            public SolidColorBrush color;
        }


        private ModelContainer modelContainer;
        private ObservableCollection<float[]> boxList;
        public ObservableCollection<float[]> Boxes
        {
            get { return boxList; }
            set { SetProperty(ref boxList, value); }
        }
        public DetectionDrawerViewModel(ModelContainer modelContainer)
        {
            this.modelContainer = modelContainer;
            modelContainer.dataContainer.detections.boundingBoxCallback += HandleNewDetections;
        }
        public void HandleNewDetections(List<float[]> list)
        {

            boxList = new ObservableCollection<float[]>(list);
            RaisePropertyChanged("Boxes");
        }
    }
}
