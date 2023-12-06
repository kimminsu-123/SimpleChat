using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Database.Service;
using System;

namespace Chungkang.GameNetwork.Service
{
    public class ChattingService
    {
        private ChattingDBService _service;
        private List<ChatRoomInfo> _rooms;

        public ChattingService()
        {
            _service = new ChattingDBService();
            _rooms = new List<ChatRoomInfo>();

            Initialize();
        }

        private void Initialize()
        {
            try
            {
                _rooms = _service.GetAllRooms();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ChattingService Initialize: {ex.Message}");
            }
        }

        private int GetNewRoomId()
        {
            if (_rooms.Count <= 0) return 0;

            return _rooms.Max(x => x.Id) + 1;
        }

        public ChatRoomInfo CreateChatRoom(ChatRoomInfo roomInfo)
        {
            try
            {
                roomInfo.Id = GetNewRoomId();
                _service.CreateChatRoom(roomInfo);
            }
            catch (Exception)
            {
                throw;
            }

            _rooms.Add(roomInfo);

            return roomInfo;
        }

        public ChatRoomInfo LeaveChatRoom(ChatRoomInfo roomInfo, UserInfo user)
        {
            try
            {
                _service.LeaveUserInChatRoom(roomInfo, user);
                _rooms
                    .First(x => x.Id.Equals(roomInfo.Id))
                    .Users
                    .First(x => x.User.Id.Equals(user.Id))
                    .Flag = ChatRoomUserFlag.Deleted;
            }
            catch (Exception)
            {
                throw;
            }

            return roomInfo;
        }

        public List<ChatRoomInfo> InqChatRooms(UserInfo user)
        {
            if (_rooms.Count <= 0) return _rooms;

            return _rooms.Where(r => r.Users.Any(u => u.User.Id.Equals(user.Id) && u.Flag == ChatRoomUserFlag.Normal)).ToList();
        }

        public void SendChat(Chat chat)
        {
            try
            {
                _service.SendChat(chat);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Chat> InqAllChatsInRoom(ChatRoomInfo room)
        {
            try
            {
                return _service.InqAllChatsInRoom(room);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
