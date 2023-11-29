using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Network.Server;

namespace Chungkang.GameNetwork.Network.Sender
{
    public abstract class MessageSender
    {
        private Queue<WrapperMessage> _messageQueue;

        protected TCPServer _server;
        protected object _lockQueue;
        protected int Count => _messageQueue.Count;

        public MessageSender(TCPServer server)
        {
            _lockQueue = new object();
            _messageQueue = new Queue<WrapperMessage>();
            _server = server;

            var t = new Thread(Update);
            t.IsBackground = true;
            t.Start();
        }

        public void EnqueueMessage(WrapperMessage msg)
        {
            lock(_lockQueue) 
            {
                _messageQueue.Enqueue(msg);
            }
        }

        public WrapperMessage DequeueMessage()
        {
            lock (_lockQueue)
            {
                return _messageQueue.Dequeue();
            }
        }

        private void Update()
        {
            while (true)
            {
                Thread.Sleep(100);

                SendMessage();
            }
        }

        protected abstract void SendMessage();
    }
}
