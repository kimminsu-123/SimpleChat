using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Network.Server;
using System.Net;

namespace Chungkang.GameNetwork.Network.Sender
{
    public class UserManagementMessageSender : MessageSender
    {
        public UserManagementMessageSender(TCPServer server) : base(server) { }

        protected override void SendMessage()
        {
            if (Count <= 0) return;

            WrapperMessage msg;

            msg = DequeueMessage();

            var addr = new IPEndPoint(IPAddress.Parse(msg.FromIP), msg.FromPort);

            msg.FromIP = ServerInfo.serverIp;
            msg.FromPort = ServerInfo.userManagePort;

            try
            {
                _server.SendTo(addr, msg);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
