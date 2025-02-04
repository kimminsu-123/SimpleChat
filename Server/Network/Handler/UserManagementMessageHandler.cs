﻿using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Network.Sender;
using Chungkang.GameNetwork.Service;
using System.Text.Json;

namespace Chungkang.GameNetwork.Network.Handler
{
    public class UserManagementMessageHandler : MessageHandler
    {
        private UserService _userService;

        public UserManagementMessageHandler(MessageSender sender) : base(sender)
        {
            _userService = new UserService();
        }

        protected override void HandleMessage()
        {
            if (Count <= 0) return;

            WrapperMessage msg;

            msg = DequeueMessage();

            if (string.IsNullOrEmpty(msg.JsonMessage.Trim())) return;

            var sendMsg = new WrapperMessage()
            {
                FromIP = msg.FromIP,
                FromPort = msg.FromPort,
            };
            var serverMsg = new ServerMessage();
            string detailMsg;
            bool retValue;

            try
            {
                switch (msg.Flag)
                {
                    case MessageFlag.Login:
                        var user = JsonSerializer.Deserialize<UserInfo>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleLogin(ref user, out detailMsg, out retValue);
                        break;
                    case MessageFlag.Register:
                        serverMsg.ReturnFlag = HandleRegister(JsonSerializer.Deserialize<UserInfo>(msg.JsonMessage), out detailMsg, out retValue);
                        break;
                    case MessageFlag.ValidateFriend:
                        user = JsonSerializer.Deserialize<UserInfo>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleValidateFriend(user, out detailMsg, out retValue);
                        break;
                    case MessageFlag.FriendRequest:
                        var req = JsonSerializer.Deserialize<FriendRequest>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleFriendRequest(req, out detailMsg, out retValue);
                        break;
                    case MessageFlag.AcceptFriendRequest:
                        req = JsonSerializer.Deserialize<FriendRequest>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleAcceptFriendRequest(req, out detailMsg, out retValue);
                        break;
                    case MessageFlag.RefuseFriendRequest:
                        req = JsonSerializer.Deserialize<FriendRequest>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleRefuseFriendRequest(req, out detailMsg, out retValue);
                        break;
                    case MessageFlag.DeleteFriend:
                        var friend = JsonSerializer.Deserialize<Friend>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleDeleteFriend(friend, out detailMsg, out retValue);
                        break;
                    case MessageFlag.FriendList:
                        user = JsonSerializer.Deserialize<UserInfo>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleFriendList(user, out detailMsg, out retValue);
                        break;
                    case MessageFlag.FriendRequestList:
                        user = JsonSerializer.Deserialize<UserInfo>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleFriendRequestList(user, out detailMsg, out retValue);
                        break;
                    default:
                        detailMsg = string.Empty;
                        retValue = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                detailMsg = ex.Message;
                retValue = false;
                Console.WriteLine(detailMsg);
            }

            serverMsg.Message = detailMsg;
            serverMsg.ReturnValue = retValue;
            serverMsg.RequesterIP = msg.FromIP;
            serverMsg.RequesterPort = msg.FromPort;

            sendMsg.Flag = retValue ? MessageFlag.Success : MessageFlag.Fail;
            sendMsg.JsonMessage = JsonSerializer.Serialize(serverMsg);
            
            _sender.EnqueueMessage(sendMsg);
        }

        public ServerMessageFlag HandleLogin(ref UserInfo? user, out string msg, out bool retValue)
        {
            retValue = true;

            try
            {
                if (!_userService.Login(ref user))
                {
                    msg = "존재하지 않는 유저입니다.";
                    retValue = false;
                }
                else
                {
                    msg = JsonSerializer.Serialize(user);
                }
            }
            catch(Exception err)
            {
                msg = err.Message;
                throw;
            }

            return ServerMessageFlag.Login;
        }

        public ServerMessageFlag HandleRegister(UserInfo? user, out string msg, out bool retValue)
        {
            retValue = true;
            msg = "회원가입에 성공하였습니다.";

            try
            {
                if (_userService.CheckDuplicatedId(user))
                {
                    msg = "중복된 아이디입니다.";
                    retValue = false;
                }

                if (!_userService.Resigister(user))
                {
                    msg = "회원가입에 실패했습니다.";
                    retValue = false;
                }
            }
            catch (Exception err)
            {
                msg = err.Message;
                retValue = false;
                throw;
            }

            return ServerMessageFlag.Register;
        }

        private ServerMessageFlag HandleValidateFriend(UserInfo? user, out string msg, out bool retValue)
        {
            msg = "유저를 찾았습니다.";
            retValue = true;

            try
            {
                user = _userService.FindUser(user);
                if(user == null)
                {
                    msg = "유저를 찾을 수 없습니다.";
                    retValue = false;
                }
            }
            catch (Exception err)
            {
                msg = err.Message;
                retValue = false;
                throw;
            }

            return ServerMessageFlag.ValidateFriend;
        }
        private ServerMessageFlag HandleFriendRequest(FriendRequest? req, out string msg, out bool retValue)
        {
            try
            {
                _userService.FriendRequest(req);
                msg = JsonSerializer.Serialize(req);
                retValue = true;
            }
            catch (Exception err)
            {
                msg = err.Message;
                retValue = false;
                throw;
            }

            return ServerMessageFlag.FriendRequest;
        }
        private ServerMessageFlag HandleAcceptFriendRequest(FriendRequest? req, out string msg, out bool retValue)
        {
            try
            {
                _userService.AcceptFriendRequest(req);
                msg = JsonSerializer.Serialize(req);
                retValue = true;
            }
            catch (Exception err)
            {
                msg = err.Message;
                retValue = false;
                throw;
            }

            return ServerMessageFlag.AcceptFriendRequest;
        }

        private ServerMessageFlag HandleRefuseFriendRequest(FriendRequest? req, out string msg, out bool retValue)
        {
            try
            {
                _userService.RefuseFriendRequest(req);
                msg = JsonSerializer.Serialize(req);
                retValue = true;
            }
            catch (Exception err)
            {
                msg = err.Message;
                retValue = false;
                throw;
            }

            return ServerMessageFlag.RefuseFriendRequest;
        }

        private ServerMessageFlag HandleDeleteFriend(Friend? friend, out string msg, out bool retValue)
        {
            try
            {
                _userService.DeleteFriend(friend);
                msg = JsonSerializer.Serialize(friend);
                retValue = true;
            }
            catch (Exception err)
            {
                msg = err.Message;
                retValue = false;
                throw;
            }

            return ServerMessageFlag.DeleteFriend;
        }

        private ServerMessageFlag HandleFriendList(UserInfo? user, out string msg, out bool retValue)
        {
            try
            {
                msg = JsonSerializer.Serialize(_userService.GetFriends(user));
                retValue = true;
            }
            catch (Exception err)
            {
                msg = err.Message;
                retValue = false;
                throw;
            }

            return ServerMessageFlag.FriendList;
        }

        private ServerMessageFlag HandleFriendRequestList(UserInfo? user, out string msg, out bool retValue)
        {
            retValue = true;

            try
            {
                msg = JsonSerializer.Serialize(_userService.GetFriendRequests(user));
            }
            catch (Exception err)
            {
                msg = err.Message;
                retValue = false;
                throw;
            }

            return ServerMessageFlag.FriendRequestList;
        }
    }
}
