using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Network.Handler;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Chungkang.GameNetwork.Network.Server
{
    public abstract class TCPServer
    {
        protected Socket _listenSocket;
        protected readonly int lingerTimeout = 10;
        protected IPEndPoint _addr;

        protected MessageHandler _messageHandler;
        protected Dictionary<IPEndPoint, Socket> _clientSockets;
        protected object _lockClients;

        public Dictionary<IPEndPoint, Socket> ClientSockets => _clientSockets;
        public bool IsRun { get; private set; }

        public TCPServer(int port)
        {
            _lockClients = new object();
            _addr = new IPEndPoint(IPAddress.Any, port);
            IsRun = false;
            _clientSockets = new Dictionary<IPEndPoint, Socket>();

            try
            {
                _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Bind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetSocketOptions()
        {
            try
            {
                _listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            }
            catch(Exception) 
            {
                throw;
            }
        }

        private void Bind()
        {
            try
            {
                _listenSocket.Bind(_addr);
                SetSocketOptions();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Accept()
        {
            while (true)
            {
                Thread.Sleep(100);

                Socket clientSocket;

                try
                {
                    clientSocket = _listenSocket.Accept();
                }
                catch (Exception)
                {
                    break;
                }

                var clientThread = new Thread(ClientProcess)
                {
                    IsBackground = true
                };
                clientThread.Start(clientSocket);

                var clientAddr = clientSocket.RemoteEndPoint as IPEndPoint;
                lock (_lockClients) _clientSockets.Add(clientAddr, clientSocket);
                Console.WriteLine($"[{GetType().Name}] : Client 접속 ({clientAddr})");
            }

            _clientSockets.Clear();
            _listenSocket.Close();
        }

        protected void ClientProcess(object? sock)
        {
            var clientSocket = sock as Socket;
            if (clientSocket == null) throw new NullReferenceException("sock as Socket is Null");

            var sizeBuf = new byte[4];
            var addr = clientSocket.RemoteEndPoint as IPEndPoint;

            while (true)
            {
                try
                {
                    var ret = clientSocket.Receive(sizeBuf);
                    if (ret <= 0) break;
                    
                    var size = int.Parse(Encoding.UTF8.GetString(sizeBuf));
                    var dataBuf = new byte[size];

                    ret = clientSocket.Receive(dataBuf);
                    if (ret <= 0) break;

                    var str = Encoding.UTF8.GetString(dataBuf);
                    var wrapperMsg = JsonSerializer.Deserialize<WrapperMessage>(str);
                    if (wrapperMsg == null) continue;

                    _messageHandler.EnqueueMessage(wrapperMsg);

                    Console.WriteLine($"[{GetType().Name}] : Receive ({addr}) : {str}");
                }
                catch (Exception)
                {
                    break;
                }
            }

            lock (_lockClients)
            {
                if(addr != null)
                    _clientSockets.Remove(addr);
            }

            Console.WriteLine($"[{GetType().Name}] : Client 접속 종료 ({addr})");
            clientSocket.Close();
        }

        public void Start()
        {
            try
            {
                _listenSocket.Listen(int.MaxValue);
            }
            catch (Exception)
            {
                throw;
            }

            var t = new Thread(Accept);
            t.IsBackground = true;
            t.Start();

            IsRun = true;

            Console.WriteLine($"[{GetType().Name}] : 서버가 정상 동작합니다.");
        }

        public virtual void SendTo(IPEndPoint address, WrapperMessage msg)
        {
            Socket clientSocket;

            lock (_lockClients)
            {
                if (!_clientSockets.ContainsKey(address)) return;
                clientSocket = _clientSockets[address];
            }

            var sendStr = JsonSerializer.Serialize(msg);
            var sendBuf = Encoding.UTF8.GetBytes(sendStr);
            var sizeStr = $"{sendBuf.Length:D4}";
            var sizeBuf = Encoding.UTF8.GetBytes(sizeStr);

            try
            {
                var ret = clientSocket.Send(sizeBuf);
                if (ret <= 0) throw new Exception();

                ret = clientSocket.Send(sendBuf);
                if (ret <= 0) throw new Exception();

                Console.WriteLine($"[{GetType().Name}] : Send ({address}) -> ({msg.FromIP}:{msg.FromPort}) : {sendStr}");
            }
            catch (Exception)
            {
                clientSocket.Close();
                lock(_lockClients)
                    _clientSockets.Remove(address);
            }
        }
    }
}
