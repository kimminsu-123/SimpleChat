using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Network.Server;

namespace Chungkang.GameNetwork.Network.Sender
{
    public class ChattingMessageSender : MessageSender
    {
        public ChattingMessageSender(TCPServer server) : base(server) { }

        protected override void SendMessage()
        {
            if (Count <= 0) return;

            WrapperMessage msg = DequeueMessage();
            if (msg == null) return;

            try
            {
                SendAll(msg);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SendAll(WrapperMessage msg)
        {
            foreach(var key in _server.ClientSockets.Keys)
            {
                _server.SendTo(key, msg);
            }
        }
    }
}
