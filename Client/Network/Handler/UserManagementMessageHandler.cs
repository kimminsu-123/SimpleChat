using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Manager;
using System.Text.Json;

namespace Chungkang.GameNetwork.Network.Handler
{
    public class UserManagementMessageHandler : MessageHandler
    {
        public override void HandleMessage()
        {
            if(Count <= 0) return;
            WrapperMessage wrapperMsg;

            lock (_lockQueue)
            {
                wrapperMsg = DequeueMessage();
            }

            if (wrapperMsg == null) return;

            var serverMsg = JsonSerializer.Deserialize<ServerMessage>(wrapperMsg.JsonMessage);
            if (serverMsg == null) return;

            var type = EventType.OnLogin;

            switch (serverMsg.ReturnFlag)
            {
                case ServerMessageFlag.Login:
                    type = EventType.OnLogin;
                    break;
                case ServerMessageFlag.Register:
                    type = EventType.OnRegister;
                    break;
                case ServerMessageFlag.FriendList:
                    type = EventType.OnInqFriendList;
                    break;
                case ServerMessageFlag.FriendRequest:
                    type = EventType.OnFriendRequest;
                    break;
                case ServerMessageFlag.FriendRequestList:
                    type = EventType.OnInqFriendRequestList;
                    break;
                case ServerMessageFlag.AcceptFriendRequest:
                    type = EventType.OnAcceptFriendRequest;
                    break;
                case ServerMessageFlag.RefuseFriendRequest:
                    type = EventType.OnRefuseFriendRequest;
                    break;
                case ServerMessageFlag.DeleteFriend:
                    type = EventType.OnDeleteFriend;
                    break;
            }

            EventManager.Instance.PostNotification(type, this, serverMsg);
        }
    }
}
