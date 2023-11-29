using Chungkang.GameNetwork.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chungkang.GameNetwork.Network.Handler
{
    public abstract class MessageHandler
    {
        private Queue<WrapperMessage> _messageQueue;

        protected object _lockQueue;
        protected int Count => _messageQueue.Count;

        private void HandleLoop()
        {
            while (true)
            {
                Thread.Sleep(100);

                HandleMessage();
            }
        }

        public MessageHandler()
        {
            _messageQueue = new Queue<WrapperMessage>();
            _lockQueue = new object();

            var t = new Thread(HandleLoop);
            t.IsBackground = true;
            t.Start();
        }

        public void EnqueueMessage(WrapperMessage message)
        {
            lock(_lockQueue)
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

        public abstract void HandleMessage();
    }
}
