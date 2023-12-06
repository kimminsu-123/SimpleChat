using Chungkang.GameNetwork.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Common.Message
{
    public enum MessageFlag
    {
        // User Management
        Login = 0,
        Register,
        ValidateFriend,
        FriendRequest,
        AcceptFriendRequest,
        RefuseFriendRequest,
        DeleteFriend,
        FriendList,
        FriendRequestList,

        // Chat
        InqChatRooms,
        CreateChatRoom,
        LeaveChatRoom,
        SendChat,
        InqChatsInRoom,

        // Server
        Success = 200,
        Fail = -1
    }

    [Serializable]
    public class WrapperMessage
    {
        [JsonPropertyName("flag")]
        public MessageFlag Flag { get; set; }
        [JsonPropertyName("fromIp")]
        public string FromIP { get; set; }
        [JsonPropertyName("fromPort")]
        public int FromPort { get; set; }
        [JsonPropertyName("message")]
        public string JsonMessage { get; set; }
    }
}
