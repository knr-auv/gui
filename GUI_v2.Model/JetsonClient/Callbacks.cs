using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.JetsonClient
{
    public class Callbacks
    {
        public delegate void Confirm();
        public delegate void TextData(string var);
        public delegate void ConfirmValue(bool val);
        public delegate void Data(float[] data);
        public delegate void Detection(string detection);

        public Confirm ArmConfirmation;
        public Confirm DisarmConfirmation;
        public Confirm AutonomyStarted;
        public Confirm AutonomyStoped;
        public Confirm DetectorStarted;
        public Confirm DetectorStoped;
        public Data MotorsDataCallback;
        //attitude, gyro
        public Data  IMUDataCallback;
        public Data MovementInfoCallback;
        public Detection DetectionCallback;
        public Data PIDCallback;
        public Data BatteryCallback;
        public TextData loggerMsgCallback;
        public TextData taskManagerMsgCallback;
        public TextData modeMsgCallback;
     
    }
}
