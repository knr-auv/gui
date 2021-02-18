using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;


namespace GUI_v2.Model.JetsonClient
{
    public class Sender
    {
        public Callbacks.ConfirmValue ConnectionStatusChnaged;
        private NetworkStream stream;
        public Sender(NetworkStream stream, Callbacks.ConfirmValue cs )
        {
            ConnectionStatusChnaged = cs;
            this.stream = stream;
        }
        private void Send_msg(byte[] data, byte TYPE)
        {
            try
            {
                if (stream.CanWrite)
                {
                    byte[] l = BitConverter.GetBytes(data.Length + 1);
                    byte[] header = new byte[5];
                    Buffer.BlockCopy(l, 0, header, 0, 4);
                    header[4] = TYPE;
                    stream.Write(header, 0, 5);
                    stream.Write(data, 0, data.Length);
                }
                else
                {
                    //throw new System.InvalidOperationException("cant write to socket");
                }
            }
            catch (System.IO.IOException)
            {
                ConnectionStatusChnaged.Invoke(false);
                stream.Close();
            }
        }
        public void SendSteering(TO_JETSON.STEERING_MSG msg, byte[] data)
        {
            byte[] b;
            if (msg == TO_JETSON.STEERING_MSG.PAD)
            {
                 b = new byte[data.Length + 1];
                b[0] = (byte)TO_JETSON.STEERING_MSG.PAD;
                Buffer.BlockCopy(data, 0, b, 1, data.Length);
            }
            else
            {  b = new byte[1];
                b[0] = (byte)msg;
            }

            Send_msg(b, TO_JETSON.STEERING);
            
        }
        public void SendControl(TO_JETSON.CONTROL_MSG msg)
        {
            byte[] data;
            switch (msg)
            {
                case TO_JETSON.CONTROL_MSG.ARM:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.CONTROL_MSG.ARM;
                    break;
                case TO_JETSON.CONTROL_MSG.DISARM:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.CONTROL_MSG.DISARM;
                    break;
                case TO_JETSON.CONTROL_MSG.START_AUTONOMY:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.CONTROL_MSG.START_AUTONOMY;
                    break;
                case TO_JETSON.CONTROL_MSG.STOP_AUTONOMY:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.CONTROL_MSG.STOP_AUTONOMY;
                    break;
                case TO_JETSON.CONTROL_MSG.START_TELEMETRY:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.CONTROL_MSG.START_TELEMETRY;
                    break;
                case TO_JETSON.CONTROL_MSG.START_DETECTOR:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.CONTROL_MSG.START_DETECTOR;
                    break;
                case TO_JETSON.CONTROL_MSG.STOP_DETECTOR:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.CONTROL_MSG.STOP_DETECTOR;
                    break;

                default:
                    data = null;
                    break;
            }
            Send_msg(data, TO_JETSON.CONTROL);

        }
        public void SendRequest(TO_JETSON.REQUEST_MSG msg)
        {
            byte[] data;
            switch (msg)
            {
                case TO_JETSON.REQUEST_MSG.PID:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.REQUEST_MSG.PID;
                    break;
                case TO_JETSON.REQUEST_MSG.CONFIG:
                    data = new byte[1];
                    data[0] = (byte)TO_JETSON.REQUEST_MSG.CONFIG;
                    break;
                default:
                    data = null;
                    break;
            }

            Send_msg(data, TO_JETSON.REQUEST);
        }
        public void SendSettings(TO_JETSON.SETTINGS_MSG msg, byte[] data)
        {
            var buffer = new List<byte>();
            switch (msg)
            {
                case TO_JETSON.SETTINGS_MSG.PID:
                    buffer.Add((byte)msg);
                    break;
                default:
                    break;
            }
            buffer.AddRange(data);
            Send_msg(buffer.ToArray(), TO_JETSON.SETTINGS);

        }
    }
}
