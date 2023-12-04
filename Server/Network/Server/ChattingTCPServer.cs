using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Network.Handler;
using Chungkang.GameNetwork.Network.Sender;
using System.Net;

namespace Chungkang.GameNetwork.Network.Server
{
    public class ChattingTCPServer : TCPServer
    {
        public ChattingTCPServer(int port) : base(port)
        {
            var sender = new ChattingMessageSender(this);
            _messageHandler = new ChattingMessageHandler(sender);
        }
    }
}
