using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Network.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Network.Handler
{
    public abstract class MessageHandler
    {
        private Queue<WrapperMessage> _messageQueue;

        protected MessageSender _sender;
        protected object _lockQueue;
        protected int Count => _messageQueue.Count;

        public MessageHandler(MessageSender sender)
        {
            _messageQueue = new Queue<WrapperMessage>();
            _sender = sender;

            _lockQueue = new object();

            var thread = new Thread(Update);
            thread.IsBackground = true;
            thread.Start();
        }

        public void EnqueueMessage(WrapperMessage message)
        {
            lock (_lockQueue)
            {
                _messageQueue.Enqueue(message);
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
                HandleMessage();
            }
        }

        protected abstract void HandleMessage();
    }
}
