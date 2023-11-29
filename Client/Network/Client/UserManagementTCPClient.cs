
using Chungkang.GameNetwork.Network.Handler;

namespace Chungkang.GameNetwork.Network.Client
{
    public class UserManagementTCPClient : TCPClient
    {
        public UserManagementTCPClient(string ip, int port) : base(ip, port)
        {
            InitHandler(new UserManagementMessageHandler());
        }
    }
}
