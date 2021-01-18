using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI_v2.Model
{
    //TODO Code review&&make it shorter
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
        public bool IsRunning()
        {
            return LoopActive;
        }
        public void StartStream(NewFrameCallback callback,  Notifyier connectionErrorCb, double fps = 45)
        {
            connectionDownCb = connectionErrorCb;
            if (stream != null && LoopActive == false)
            {
                x = new Thread(() => Loop(callback, fps));
                x.Start();
            }
            else
                SwapCallbacks(callback, connectionErrorCb);
        }
        public void SwapCallbacks(NewFrameCallback callback, Notifyier connectionErrorCb)
        {
            cb = callback;
            connectionDownCb = connectionErrorCb;
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

        NewFrameCallback cb;
        private void Loop(NewFrameCallback callback, double fps)
        {
            cb = callback;
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
                            cb(_frame);
                            
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
        void ReadAllFromStream(NetworkStream stream, byte[] buffer, int len)
        {
            int current = 0;
            while (current < buffer.Length&&current<len)
                current += stream.Read(buffer, current, len - current > buffer.Length ? buffer.Length : len - current);
        }
        private void ReceiveFrame()
        {
            if (stream.CanRead&&stream.CanWrite)
            {
                stream.Write(ack, 0, 1);
                ReadAllFromStream(stream, buffer, 1);
                if (buffer[0] != ack[0])
                    return;
                    ReadAllFromStream(stream, buffer, 4);
                    int lenght = BitConverter.ToInt32(buffer, 0);
                    _frame = new byte[lenght + chunkSize];// - lenght % chunkSize];
                    ReadAllFromStream(stream, _frame, lenght);
                }
        }
    }
}
