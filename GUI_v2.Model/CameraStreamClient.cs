using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI_v2.Model
{
    public class CameraStreamClient
    { 
        public delegate void NewFrameCallback(Byte[] frame);
        public delegate void Confirm(bool val);
        public Confirm ConnectionStatusChanged;
        public delegate void Notifyier();
        Notifyier connectionDownCb;
        private NetworkStream stream;
        private string address;
        private int port;
        private byte[] _frame;
        private bool LoopActive = false;
        private Thread x;
        private bool isReceiving = false;

        public bool ConnectToServer(string Address, int Port)
        {
            TcpClient client = new TcpClient();
            port = Port;
            address = Address;
            try
            {
                client.Connect(address, port);
                stream = client.GetStream();

                ConnectionStatusChanged.Invoke(true);
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                connectionDownCb?.Invoke();
                ConnectionStatusChanged.Invoke(false);
                return false;
            }

        }

        ~CameraStreamClient()
        {
            Disconnect();
        }
        public void Disconnect()
        {
            while (isReceiving) ;
            if (stream!=null)
            {
                stream.Flush();
                stream.Close();
            }
            connectionDownCb?.Invoke();
            ConnectionStatusChanged.Invoke(false);
            stream = null;
            StopStream();
        }
        public void StartStream(NewFrameCallback callback,  Notifyier connectionErrorCb, double fps = 45)
        {
            connectionDownCb = connectionErrorCb;
            if (stream!=null&&LoopActive ==false)
            {
                x = new Thread(() => Loop(callback, fps));
                x.Start();
            }
        }

        public void StopStream()
        {
            if (x!=null&&x.IsAlive)
            {
                LoopActive = false;
                x.Join(100);
                x.Abort();
            }
        }


        private void Loop(NewFrameCallback callback, double fps)
        {
            try 
            {
                LoopActive = true;
                DateTime lastFrame = DateTime.Now;
                int freq = (int)Math.Round(1000 / fps);
                while (LoopActive)
                {
                    if ((DateTime.Now - lastFrame).Milliseconds >= freq)
                    {
                        try
                        {
                            isReceiving = true;
                            ReceiveFrame();
                            isReceiving = false;
                            callback(_frame);
                            
                        }
                        catch (TaskCanceledException)
                        {
                            isReceiving = false;
                            Disconnect();
                            return;
                        }
                        catch(IOException)
                        {
                             isReceiving = false;
                            Disconnect();
                            return;
                        }
                        lastFrame = DateTime.Now;
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }
            catch(ThreadAbortException)
            {
                //ConnectionStatusChanged.Invoke(false);
                Disconnect();
                
                return;
            }
           
        }

        private static readonly int chunkSize = 4096;
        private readonly byte[] buffer = new Byte[chunkSize];
        private readonly byte[] ack = { 0x69 };

        private void ReceiveFrame()
        {
            if (stream.CanRead&&stream.CanWrite)
            {
                stream.Write(ack, 0, 1);
                if (stream.ReadByte() != ack[0])
                    return;
                var l = stream.Read(buffer, 0, 4);
                int lenght = BitConverter.ToInt32(buffer, 0);
                _frame = new byte[lenght + chunkSize];// - lenght % chunkSize];
                int receivedBytes = 0;
                int chunk;
                while (receivedBytes < lenght)
                {
                    chunk = stream.Read(_frame, receivedBytes, chunkSize);
                    receivedBytes += chunk;
                }
            }
        }
    }
}
