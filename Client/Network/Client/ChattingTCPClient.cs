using Chungkang.GameNetwork.Network.Handler;

namespace Chungkang.GameNetwork.Network.Client
{
    public class ChattingTCPClient : TCPClient
    {
        public ChattingTCPClient(string ip, int port) : base(ip, port)
        {
            InitHandler(new ChattingMessageHandler());
        }
    }
}
