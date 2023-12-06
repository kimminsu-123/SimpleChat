using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Manager;
using System.Text.Json;

namespace Chungkang.GameNetwork.Network.Handler
{
    public class ChattingMessageHandler : MessageHandler
    {
        public override void HandleMessage()
        {
            if (Count <= 0) return;
            WrapperMessage wrapperMsg;

            lock (_lockQueue)
            {
                wrapperMsg = DequeueMessage();
            }

            if (wrapperMsg == null) return;

            var serverMsg = JsonSerializer.Deserialize<ServerMessage>(wrapperMsg.JsonMessage);
            if (serverMsg == null) return;

            var type = EventType.OnCreateChatRoom;

            switch (serverMsg.ReturnFlag)
            {
                case ServerMessageFlag.InqChatRooms:
                    type = EventType.OnInqChatRooms;
                    break;
                case ServerMessageFlag.CreateChatRoom:
                    type = EventType.OnCreateChatRoom;
                    break;
                case ServerMessageFlag.LeaveChatRoom:
                    type = EventType.OnLeaveChatRoom;
                    break;
                case ServerMessageFlag.SendChat:
                    type = EventType.OnReceiveChat;
                    break;
                case ServerMessageFlag.InqChatsInRoom:
                    type = EventType.OnInqChatsInRoom;
                    break;
            }

            EventManager.Instance.PostNotification(type, this, serverMsg);
        }
    }
}
