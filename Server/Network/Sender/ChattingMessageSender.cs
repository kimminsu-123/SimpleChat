using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Network.Server;

namespace Chungkang.GameNetwork.Network.Sender
{
    public class ChattingMessageSender : MessageSender
    {
        private object _lockSockets;

        public ChattingMessageSender(TCPServer server) : base(server) 
        {
            _lockSockets = new object();    
        }

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
            lock (_lockSockets)
            {
                foreach (var key in _server.ClientSockets.Keys)
                {
                    _server.SendTo(key, msg);
                }
            }
        }
    }
}
