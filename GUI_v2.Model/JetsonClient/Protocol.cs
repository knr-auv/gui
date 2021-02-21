using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.Model.JetsonClient
{
    public class TO_JETSON
    {
        public const byte REQUEST = 0x01;
        public const byte STEERING = 0x02;
        public const byte CONTROL = 0x03;
        public const byte SETTINGS = 0x04;
        public enum REQUEST_MSG : byte
        {
            PID = 0x01,
            CONFIG = 0x02
        }
        public  enum STEERING_MSG : byte
        {
            PAD = 0x01,
            MODE_ACRO = 0x02,
            MODE_STABLE =0x03
        }
        public enum CONTROL_MSG : byte
        {
            ARM = 0x01,
            DISARM = 0x02,
            START_AUTONOMY = 0x03,
            STOP_AUTONOMY =0x04,
            START_TELEMETRY = 0x05,
            START_DETECTOR = 0x06,
            STOP_DETECTOR = 0x07,
            SET_MOTORS = 0x08,
            DISABLE_MOTORS = 0x09
        }
        public enum SETTINGS_MSG : byte
        {
            PID = 0x01
        }

    }

    public class FROM_JETSON 
    {
        public const byte TELEMETRY = 0x01;
        public const byte REQUEST_RESPONCE = 0x02;
        public const byte AUTONOMY = 0x03;
        public const byte STATUS = 0x04;
        public const byte SETTINGS = 0x05;
        public enum TELEMETRY_MSG : byte
        {
            MOTORS = 0x01,
            IMU = 0x02,
            MOVEMENT_INFO = 0x03,
            BATTERY = 0x04
        }
        public enum REQUEST_RESPONCE_MSG : byte
        {
            PID = 0x01,
            ARMED = 0x02,
            DISARMED =0x03
        }
        public enum AUTONOMY_MSG : byte
        {
            DETECTION = 0x01,
            AUTONOMY_STARTED = 0x02,
            AUTONOMY_STOPED = 0x03,
            DETECTOR_STARTED = 0x04,
            DETECTOR_STOPED = 0x05
        }
        public enum STATUS_MSG : byte
        {
            LOGGER = 0x01,
            SENSOR_STATUS = 0x02,
            TASK_MANAGER = 0x03,
            MODE_PC_SIMULATION = 0x04,
            MODE_JETSON_STM = 0x05,
            MODE_JETSON_SIMULATION = 0x06
        }
        public enum SETTINGS_MSG : byte
        {

        }
    }

}
