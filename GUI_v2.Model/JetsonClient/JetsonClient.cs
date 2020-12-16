using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI_v2.Model.JetsonClient
{
    public class JetsonClient
    {

        public Callbacks.ConfirmValue ConnectionStatusChnaged;
        private TcpClient client;
        private NetworkStream stream;
        private Receiver receiver;
        private Sender sender;
        private string addres;
        private int port;
        public Callbacks callbacks = new Callbacks();

        ~JetsonClient(){
            Disconnect();
        }

        public bool Disconnect()
        {
            if (client != null&&client.Connected)
            {
                stream.Flush();
                stream.Close();
            }
            ConnectionStatusChnaged.Invoke(false);
            return true;
        }
        public void StartReceiving()
        {
           receiver.StartReceiving();
        }
        public void StopReceiving()
        {
            receiver.StopReceiving();
        }
        public bool ConnectToJetson(string Address, int Port)
        {
            //if (_callbacks == null)
                //return false;
            addres = Address; port = Port;
            try
            {
                client = new TcpClient(addres, port);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                ConnectionStatusChnaged.Invoke(false);
                return false;
            }
            stream = client.GetStream();
            receiver = new Receiver(stream,callbacks, ConnectionStatusChnaged);
            sender = new Sender(stream, ConnectionStatusChnaged);
            ConnectionStatusChnaged.Invoke(true);
            return true;
        }
        //simulation, normal, graspers etc.
        public void GetConfig()
        {
            sender?.SendRequest(TO_JETSON.REQUEST_MSG.CONFIG);
        }

        public void Arm()
        {
            sender?.SendControl(TO_JETSON.CONTROL_MSG.ARM);
        }

        public void Disarm()
        {
            sender?.SendControl(TO_JETSON.CONTROL_MSG.DISARM);
        }
    
        public void SetSteeringMode(string mode)
        {
            if (mode == "acro")
                sender?.SendSteering(TO_JETSON.STEERING_MSG.MODE_ACRO, null);
            else if (mode == "stable")
                sender?.SendSteering(TO_JETSON.STEERING_MSG.MODE_STABLE, null);
        }

        public void SendPIDs(double[] data)
        {
            byte[] msg = new byte[data.Length * sizeof(double)];
            for(var i =0; i < data.Length; i++)
            {
                byte[] b = BitConverter.GetBytes(data[i]);
                Buffer.BlockCopy(b, 0, msg, i * sizeof(double), b.Length);
            }
            sender?.SendSettings(TO_JETSON.SETTINGS_MSG.PID, msg);
        }

        public void GetPIDs()
        {
            sender?.SendRequest(TO_JETSON.REQUEST_MSG.PID);
        }

        public void SendSteering(int[] data)
        {
            byte[] msg = new byte[sizeof(int) * data.Length];
            for (int i=0;i<data.Length;i++)
            {
                
                byte[] b =BitConverter.GetBytes(data[i]);
                Buffer.BlockCopy(b, 0, msg, i * sizeof(int), b.Length);
                    
            }
            
            
            sender?.SendSteering(TO_JETSON.STEERING_MSG.PAD, msg);
        }

        public void StartTelemetry()
        {
            sender?.SendControl(TO_JETSON.CONTROL_MSG.START_TELEMETRY);
        }
        public void StopAutonomy()
        {
            sender?.SendControl(TO_JETSON.CONTROL_MSG.STOP_AUTONOMY);
        }

        public void StartAutonomy()
        {
            sender?.SendControl(TO_JETSON.CONTROL_MSG.START_AUTONOMY);
        }
        public void StartDetections()
        {
            sender?.SendControl(TO_JETSON.CONTROL_MSG.START_DETECTOR);
        }
        public void StopDetections()
        {
            sender?.SendControl(TO_JETSON.CONTROL_MSG.STOP_DETECTOR);
        }
    }
}


