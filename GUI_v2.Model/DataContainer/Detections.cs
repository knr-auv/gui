using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_GUI_v2.Model.DataContainer
{
    public class Detection
    {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _distance = 0;
        private double _accuracy = 0;
        private string _type = "None";

        public double x
        {
            get => _x;
            set => _x=value;
        }
        public double y
        {
            get => _y;
            set => _y = value;
        }
        public double z
        {
            get => _z;
            set => _z = value;
        }
        public double distance
        {
            get => _distance;
            set => _distance = value;
        }
        public double accuracy
        {
            get => _accuracy;
            set => _accuracy = value;
        }
        public string type
        {
            get => _type;
            set => _type = value;
        }


        public void UpdateDistance(double x, double y, double z)
        {
            double tx = this.x - x;
            double ty = this.y - y;
            double tz = this.z - z;
            distance = Math.Sqrt(tx * tx + ty * ty + tz * tz);
        }
    }

    public class Detections
    {
        public double FPS = 0;
        public List<Detection> ObjectList =new List<Detection>();
        public List<Detection> LastDetectionList = new List<Detection>();
        public delegate void NewDetectionsCallback();
        public NewDetectionsCallback newDetectionsCallback;
        
        private class helper
        {
            public double fps;
            public List<Detection> ObjectsList;
            public List<Detection> LastDetections;
        }
        public void HandleNewDetections(string detection)
        {
            helper a = JsonConvert.DeserializeObject<helper>(detection);
            ObjectList = a.ObjectsList;
            LastDetectionList = a.LastDetections;
            FPS = a.fps;
            newDetectionsCallback?.Invoke();
        }
        public void HandleNewPosition(double x, double y, double z)
        {
            if(ObjectList!=null)
            foreach (var i in ObjectList)
                i.UpdateDistance(x,y,z);
            if(LastDetectionList!=null)
            foreach (var i in LastDetectionList)
                i.UpdateDistance(x,y,z);
            newDetectionsCallback?.Invoke();
        }
    }
}
