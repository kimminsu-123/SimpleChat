using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Network.Handler;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Chungkang.GameNetwork.Network.Client
{
    public class TCPClient : IDisposable
    {
        private MessageHandler _handler;
        
        protected Socket socket;
        protected IPEndPoint serverAddr;
        protected readonly int lingerTimeout = 5;
        protected IPEndPoint localEndPoint;

        public IPEndPoint LocalEndPoint => localEndPoint;

        public TCPClient(string ip, int port)
        {
            serverAddr = new IPEndPoint(IPAddress.Parse(ip), port);    
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        protected void SetSocketOptions()
        {
            try
            {
                var lingerOption = new LingerOption(true, lingerTimeout);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, lingerOption);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void InitHandler(MessageHandler handler) 
        {
            _handler = handler;
        }

        public void Initialize()
        {
            try
            {
                SetSocketOptions();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Connect()
        {
            try
            {
                socket.Connect(serverAddr);

                var t = new Thread(Receive);
                t.IsBackground = true;
                t.Start();

                localEndPoint = (IPEndPoint)socket.LocalEndPoint;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Receive()
        {
            if (!socket.Connected) throw new Exception("Socket is not connected");

            var sizeBuf = new byte[10];
            var ret = 0;

            while (true)
            {
                try
                {
                    Thread.Sleep(100);

                    ret = socket.Receive(sizeBuf);
                    if (ret <= 0) break;

                    var size = int.Parse(Encoding.UTF8.GetString(sizeBuf));
                    var ms = new MemoryStream();

                    while (size > 0)
                    {
                        byte[] dataBuf;
                        if (size < socket.ReceiveBufferSize)
                            dataBuf = new byte[size];
                        else
                            dataBuf = new byte[socket.ReceiveBufferSize];

                        ret = socket.Receive(dataBuf);
                        if (ret <= 0) break;

                        size -= ret;

                        ms.Write(dataBuf, 0, dataBuf.Length);
                    }

                    ms.Dispose();

                    var json = Encoding.UTF8.GetString(ms.ToArray());
                    var wrapperMsg = JsonSerializer.Deserialize<WrapperMessage>(json);

                    if (wrapperMsg == null) continue;

                    _handler.EnqueueMessage(wrapperMsg);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int Send(WrapperMessage message)
        {
            if (!socket.Connected) throw new Exception("Socket is not connected");

            message.FromIP = localEndPoint.Address.ToString();
            message.FromPort = localEndPoint.Port;

            var str = JsonSerializer.Serialize(message);
            var sendBuf = Encoding.UTF8.GetBytes(str);
            var size = $"{sendBuf.Length:D10}";
            var sizeBuf = Encoding.UTF8.GetBytes(size);

            try
            {
                var ret = socket.Send(sizeBuf);
                if (ret <= 0) throw new Exception("send size buffer 0");

                ret = socket.Send(sendBuf);
                if (ret <= 0) throw new Exception("send message buffer 0");
                    
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Disconnect()
        {
            socket.Disconnect(true);
        }

        public void Close()
        {
            Disconnect();
            socket.Close();
        }

        public void Dispose()
        {
            Close();
            socket.Dispose();
        }
    }
}
