using GUI_v2.Model.DataContainer.Sensors;
using PC_GUI_v2.Model.DataContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.DataContainer
{
    
    public class DataContainer
    {
        public BaroSensor Baro = new BaroSensor();
        public Sensor Attitude = new Sensor();
        public Sensor Acc = new Sensor();
        public Sensor Gyro = new Sensor();
        public Sensor Mag = new Sensor();
        public Sensor Position = new Sensor();
        public Sensor Velocity = new Sensor();
        public Sensor Acceleration = new Sensor();
        public BatterySensor Battery = new BatterySensor();
        public Detections detections = new Detections();
        public MotorsData motorsData = new MotorsData();
    }
}
