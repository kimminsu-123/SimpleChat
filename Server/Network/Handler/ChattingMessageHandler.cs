using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Network.Sender;
using Chungkang.GameNetwork.Service;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace Chungkang.GameNetwork.Network.Handler
{
    public class ChattingMessageHandler : MessageHandler
    {
        private ChattingService _chattingService;

        public ChattingMessageHandler(MessageSender sender) : base(sender)
        {
            _chattingService = new ChattingService();
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
                    case MessageFlag.InqChatRooms:
                        var user = JsonSerializer.Deserialize<UserInfo>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleInqChatRooms(user, out detailMsg, out retValue);
                        break;
                    case MessageFlag.CreateChatRoom:
                        var roomInfo = JsonSerializer.Deserialize<ChatRoomInfo>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleCreateChatRoom(ref roomInfo, out detailMsg, out retValue);
                        break;
                    case MessageFlag.LeaveChatRoom:
                        roomInfo = JsonSerializer.Deserialize<ChatRoomInfo>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleLeaveChatRoom(roomInfo, out detailMsg, out retValue);
                        break;
                    case MessageFlag.SendChat:
                        var chat = JsonSerializer.Deserialize<Chat>(msg.JsonMessage);
                        serverMsg.ReturnFlag = HandleSendChat(chat, out detailMsg, out retValue);
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

            sendMsg.Flag = retValue ? MessageFlag.Success : MessageFlag.Fail;
            sendMsg.JsonMessage = JsonSerializer.Serialize(serverMsg);
            _sender.EnqueueMessage(sendMsg);
        }

        private ServerMessageFlag HandleInqChatRooms(UserInfo user, out string msg, out bool retValue)
        {
            try
            {
                msg = JsonSerializer.Serialize(_chattingService.InqChatRooms(user));
                retValue = true;
            }
            catch (Exception)
            {
                throw;
            }

            return ServerMessageFlag.InqChatRooms;
        }

        private ServerMessageFlag HandleCreateChatRoom(ref ChatRoomInfo roomInfo, out string msg, out bool retValue)
        {
            try
            {
                roomInfo = _chattingService.CreateChatRoom(roomInfo);

                msg = JsonSerializer.Serialize(roomInfo);
                retValue = true;
            }
            catch (Exception)
            {
                throw;
            }

            return ServerMessageFlag.CreateChatRoom;
        }

        private ServerMessageFlag HandleLeaveChatRoom(ChatRoomInfo roomInfo, out string msg, out bool retValue) 
        {
            try
            {
                var user = roomInfo.Users[0].User;
                var chatRoom = _chattingService.LeaveChatRoom(roomInfo, user);

                msg = JsonSerializer.Serialize(chatRoom);
                retValue = true;
            }
            catch (Exception)
            {
                throw;
            }

            return ServerMessageFlag.LeaveChatRoom;
        }

        private ServerMessageFlag HandleSendChat(Chat chat, out string msg, out bool retValue)
        {
            try
            {
                _chattingService.SendChat(chat);

                msg = JsonSerializer.Serialize(chat);
                retValue = true;
            }
            catch (Exception)
            {
                throw;
            }

            return ServerMessageFlag.LeaveChatRoom;
        }
    }
}
