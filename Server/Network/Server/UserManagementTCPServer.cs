using Chungkang.GameNetwork.Network.Handler;
using Chungkang.GameNetwork.Network.Sender;

namespace Chungkang.GameNetwork.Network.Server
{
    public class UserManagementTCPServer : TCPServer
    {
        public UserManagementTCPServer(int port) : base(port) 
        {
            var sender = new UserManagementMessageSender(this);
            _messageHandler = new UserManagementMessageHandler(sender);
        }
    }
}
