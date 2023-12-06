using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Common.Util
{
    public struct ServerInfo
    {
        public const string serverIp = "127.0.0.1";
        public const int userManagePort = 9000;
        public const int chatPort = 9001;
    }

    [Serializable]
    public class UserInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("nickName")]
        public string NickName { get; set; }

        public UserInfo(string id, string password, string nickName = "")
        {
            Id = id;
            Password = password;
            NickName = nickName;
        }

        public override string ToString()
        {
            return $"id: {Id}, pw: {Password}, nickName: {NickName}";
        }
    }

    public enum FriendRequestFlag
    {
        Request,
        Accept,
        Refuse
    }

    [Serializable]
    public class FriendRequest
    {
        [JsonPropertyName("myInfo")]
        public UserInfo MyInfo { get; set; }
        [JsonPropertyName("friendInfo")]
        public UserInfo FriendInfo { get; set; }
        [JsonPropertyName("flag")]
        public FriendRequestFlag Flag { get; set; }

        public FriendRequest(UserInfo myInfo, UserInfo friendInfo, FriendRequestFlag flag = FriendRequestFlag.Request)
        {
            MyInfo = myInfo;
            FriendInfo = friendInfo;
            Flag = flag;
        }

        public override string ToString()
        {
            return $"myInfo: {MyInfo}, friendInfo: {FriendInfo}, flag: {Flag}";
        }
    }

    public enum FriendFlag
    {
        Nomal = 1,
        Deleted
    }

    [Serializable]
    public class Friend
    {
        [JsonPropertyName("myInfo")]
        public UserInfo MyInfo { get; set; }
        [JsonPropertyName ("userInfo")]
        public UserInfo FriendInfo { get; set; }
        [JsonPropertyName("flag")]
        public FriendFlag Flag { get; set; }

        public Friend(UserInfo myInfo, UserInfo friendInfo, FriendFlag flag)
        {
            MyInfo = myInfo;
            FriendInfo = friendInfo;
            Flag = flag;
        }

        public override string ToString()
        {
            return $"myInfo: {MyInfo}, friendInfo: {FriendInfo}, flag: {Flag}";
        }
    }

    public enum ServerMessageFlag
    {
        Login,
        Register,
        ValidateFriend,
        FriendRequest,
        AcceptFriendRequest,
        RefuseFriendRequest,
        DeleteFriend,
        FriendList,
        FriendRequestList,

        InqChatRooms,
        CreateChatRoom,
        LeaveChatRoom,
        SendChat,
        InqChatsInRoom
    }

    [Serializable]
    public class ServerMessage
    {
        [JsonPropertyName("ret")]
        public ServerMessageFlag ReturnFlag { get; set; }
        [JsonPropertyName("retValue")]
        public bool ReturnValue { get; set; }
        [JsonPropertyName("retMessage")]
        public string Message { get; set; }
        [JsonPropertyName("requesterIp")]
        public string RequesterIP { get; set; }
        [JsonPropertyName("requesterPort")]
        public int RequesterPort { get; set; }
    }

    [Serializable]
    public class ChatRoomInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("creater")]
        public string Creater { get; set; }
        [JsonPropertyName("users")]
        public List<ChatRoomUser> Users { get; set; }

        public ChatRoomInfo()
        {
            Users = new List<ChatRoomUser>();
        }
    }

    [Serializable]
    public class Chat
    {
        [JsonPropertyName("chatroom")]
        public ChatRoomInfo ChatRoom { get; set; }
        [JsonPropertyName("sender")]
        public UserInfo Sender { get; set; }
        [JsonPropertyName("sendDttm")]
        public DateTime SendDttm { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public enum ChatRoomUserFlag
    {
        Normal = 1,
        Deleted
    }

    [Serializable] 
    public class ChatRoomUser
    {
        [JsonPropertyName("user")]
        public UserInfo User { get; set; }
        [JsonPropertyName("flag")]
        public ChatRoomUserFlag Flag { get; set; }
    }
}
