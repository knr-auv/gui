using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GUI_v2.Model.JetsonClient
{
    public class Receiver
    {
        private readonly Callbacks.ConfirmValue connectionChanged;
        bool should_receive=true;
        private readonly NetworkStream stream;
        private readonly Callbacks cb;
        private readonly Parser parser;

        public Receiver(NetworkStream stream, Callbacks callbacks, Callbacks.ConfirmValue c)
        {
            connectionChanged += c;
            this.stream = stream;
            parser = new Parser(callbacks);
            cb = callbacks;
        }

        public void StartReceiving()
        {
            should_receive = true;
            Thread x = new Thread(Receive);
            x.Start();
            connectionChanged.Invoke(true);
        }
        public void StopReceiving()
        {
            connectionChanged.Invoke(false);
            should_receive = false;
        }
        void ReadAllFromStream(NetworkStream stream, byte[] buffer, int len)
        {
            int current = 0;
            while (current < buffer.Length && current < len)
                current += stream.Read(buffer, current, len - current > buffer.Length ? buffer.Length : len - current);
        }
        private readonly byte[] header_buffer =new byte[4];
        void Receive()
        {

            int msg_length = 0;
            int HEADER = 0;
            int DATA = 1;
            int rx_state = HEADER;
            while (should_receive)
            {
                try
                {
                    
                        if (rx_state == HEADER)
                        {
                            ReadAllFromStream(stream, header_buffer, 4);
                            msg_length = BitConverter.ToInt32(header_buffer, 0);                        
                            rx_state = DATA;
                        }else if (rx_state == DATA)
                        {
                            byte[] data = new byte[msg_length];
                            ReadAllFromStream(stream, data, msg_length);
                            parser.Handle_data(data);
                            rx_state = HEADER;
                        }
                        
                    
                }
                catch (ObjectDisposedException)
                {
                    connectionChanged.Invoke(false);
                    break;
                }
                catch (IOException)
                {
                    connectionChanged.Invoke(false);
                    break;
                }
            }
        }
    }
}
