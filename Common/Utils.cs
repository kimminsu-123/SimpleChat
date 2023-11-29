using System;
using System.Collections.Generic;
using System.Linq;
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

    [Serializable]
    public class FriendRequest
    {
        [JsonPropertyName("sourceId")]
        public string MyId { get; set; }
        [JsonPropertyName("targetId")]
        public string FriendId { get; set; }

        public FriendRequest(string myId, string friendId)
        {
            MyId = myId;
            FriendId = friendId;
        }

        public override string ToString()
        {
            return $"myId: {MyId}, friendId: {FriendId}";
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
        [JsonPropertyName("myId")]
        public string MyId { get; set; }
        [JsonPropertyName("friendId")]
        public string FriendId { get; set; }
        [JsonPropertyName("Flag")]
        public FriendFlag Flag { get; set; }

        public Friend(string myId, string friendId, FriendFlag flag)
        {
            MyId = myId;
            FriendId = friendId;
            Flag = flag;
        }

        public override string ToString()
        {
            return $"myId: {MyId}, friendId: {FriendId}, flag: {Flag}";
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
        DeleteFriend
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
    }
}
