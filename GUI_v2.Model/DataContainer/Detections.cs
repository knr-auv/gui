using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PC_GUI_v2.Model.DataContainer
{
    public class Detection
    {
        private string _type = "None";
        private int _index = 0;
        private float _accuracy = 0;
        private float[] _boundingBox;
        private float[] _position;
        private float _angle = 0;
        private float _distance = 0;
        private float _width = 0;
        private float _height = 0;
        public string type
        {
            get => _type;
            set => _type = value;
        }
        public int index
        {
            get => _index;
            set => _index = value;
        }
        public float accuracy
        {
            get => _accuracy;
            set => _accuracy = value;
        }
        public float[] boundingBox
        {
            get => _boundingBox;
            set => _boundingBox = value;
        }
        public float[] position
        {
            get => _position;
            set => _position = value;
        }
        public float angle
        {
            get => _angle;
            set => _angle = value;
        }

        public float distance
        {
            get => _distance;
            set => _distance = value;
        }

        public float width
        {
            get => _width;
            set => _width = value;
        }
        public float height
        {
            get => _height;
            set => _height = value;
        }
    }

    public class Detections
    {
        public float FPS = 0;
        public List<Detection> ObjectList = new List<Detection>();

        public delegate void NewDetectionsCallback();
        public NewDetectionsCallback newDetectionsCallback;
        public delegate void BoundingBoxCallback(List<float[]> boxList);
        public BoundingBoxCallback boundingBoxCallback;

        private class helper
        {
            public float fps;
            public List<Detection> ObjectsList;
        }
        public void HandleNewDetections(string detection)
        {
            helper a = JsonConvert.DeserializeObject<helper>(detection);
            ObjectList = a.ObjectsList;
            FPS = a.fps;
            newDetectionsCallback?.Invoke();
            boundingBoxCallback?.Invoke(PrepareBoundingBoxes());
        }
        private List<float[]> PrepareBoundingBoxes()
        {
            List<float[]> boxList = new List<float[]>();

            foreach (var obj in ObjectList)
            {
                if (obj.boundingBox != null)
                    boxList.Add(obj.boundingBox);
            }

            return boxList;
        }



    }
}

