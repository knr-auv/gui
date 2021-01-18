using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows;

namespace GUI_v2.Model.JetsonClient
{
    public class Parser
    {
        private Callbacks cb;
        public Parser(Callbacks callbacks)
        {
            cb = callbacks;
        }
        public void Handle_data(byte[] data)
        {
            if (data.Length==0)
                return;
            switch (data[0])
            {
                case FROM_JETSON.AUTONOMY:
                    HandleAutonomy(data);
                    break;
                case FROM_JETSON.TELEMETRY:
                    HandleTelemetry(data);
                    break;
                case FROM_JETSON.REQUEST_RESPONCE:
                    HandleRequestResponse(data);
                    break;
                case FROM_JETSON.STATUS:
                    HandleStatus(data);
                    break;

                default:
                    Console.WriteLine(data[0].ToString() + "is not a valid protocol stuff");
                    break;
            }
        }

        //here i should decode and call callbacks
        private void HandleAutonomy(byte[] data)
        {
            switch ((FROM_JETSON.AUTONOMY_MSG)data[1])
            {
                case FROM_JETSON.AUTONOMY_MSG.DETECTION:
                    string result = Encoding.UTF8.GetString(data, 2, data.Length - 2);
                    cb.DetectionCallback(result);
                    break;
                case FROM_JETSON.AUTONOMY_MSG.AUTONOMY_STARTED:
                    cb.AutonomyStarted?.Invoke();
                    break;
                case FROM_JETSON.AUTONOMY_MSG.AUTONOMY_STOPED:
                    cb.AutonomyStoped?.Invoke();
                    break;
                case FROM_JETSON.AUTONOMY_MSG.DETECTOR_STARTED:
                    cb.DetectorStarted?.Invoke();
                    break;
                case FROM_JETSON.AUTONOMY_MSG.DETECTOR_STOPED:
                    cb.DetectorStoped?.Invoke();
                    break;
            }



        }
        private void HandleTelemetry(byte[] data)
        {
            switch ((FROM_JETSON.TELEMETRY_MSG)data[1])
            {
                case FROM_JETSON.TELEMETRY_MSG.IMU:
                    {
                        int size = (data.Length - 2) / sizeof(float);
                        float[] val = new float[size];
                        for (int i = 0; i < size; i++)
                        {
                            val[i] = BitConverter.ToSingle(data, 2 + i * sizeof(float));
                        }

                        //attitude, acc, gyro, mag, depth
                        cb.IMUDataCallback?.Invoke(val);
                    }
                    break;
                case FROM_JETSON.TELEMETRY_MSG.MOVEMENT_INFO:
                    {
                        int size = (data.Length - 2) / sizeof(float);
                        float[] val = new float[size];
                        for (int i = 0; i < size; i++)
                        {
                            val[i] = BitConverter.ToSingle(data, 2 + i * sizeof(float));
                        }
                            cb?.MovementInfoCallback(val);
                        
                        
                        
                    }
                    break;
                case FROM_JETSON.TELEMETRY_MSG.BATTERY:
                    {
                        int size = (data.Length - 2) / sizeof(float);
                        float[] val = new float[size];
                        for (int i = 0; i < size; i++)
                        {
                            val[i] = BitConverter.ToSingle(data, 2 + i * sizeof(float));
                        }
                        cb?.BatteryCallback(val);
                    }
                    break;
                case FROM_JETSON.TELEMETRY_MSG.MOTORS:
                    {
                        int size = (data.Length - 2) / sizeof(float);
                        float[] val = new float[size];
                        for (int i = 0; i < size; i++)
                        {
                            val[i] = BitConverter.ToSingle(data, 2 + i * sizeof(float));
                        }
                        cb?.MotorsDataCallback?.Invoke(val);
                    }
                    break;
            }
        }
        private void HandleRequestResponse(byte[] data)
        {
            byte key = data[1];
            switch (key)
            {
                case (byte)FROM_JETSON.REQUEST_RESPONCE_MSG.PID:
                    int size = (data.Length - 2) / sizeof(float);
                    float[] val = new float[size];
                    for (int i = 0; i < size; i++)
                    {
                        val[i] = BitConverter.ToSingle(data, 2 + i * sizeof(float));
                        val[i] = (float)Math.Round(val[i] * 100) / 100;
                    }
                    cb.PIDCallback?.Invoke(val);
                    Console.WriteLine("received pids");
                    break;
                case (byte)FROM_JETSON.REQUEST_RESPONCE_MSG.ARMED:
                    cb.ArmConfirmation?.Invoke();
                    break;
                case (byte)FROM_JETSON.REQUEST_RESPONCE_MSG.DISARMED:
                    cb.DisarmConfirmation?.Invoke();
                    break;
                default:
                    Console.WriteLine(key.ToString() + " is not valid request responce message");
                    break;
            }
        }

        private void HandleStatus(byte[] data)
        {
            string result;
            switch ((FROM_JETSON.STATUS_MSG)data[1])
            {
                
                case FROM_JETSON.STATUS_MSG.LOGGER:
                    result = Encoding.UTF8.GetString(data, 2, data.Length - 2);
                    cb.loggerMsgCallback?.Invoke(result);
                    break;
                case FROM_JETSON.STATUS_MSG.TASK_MANAGER:
                    result = Encoding.UTF8.GetString(data, 2, data.Length - 2);
                    cb.taskManagerMsgCallback?.Invoke(result);
                    break;

            }
        }

    }
}