using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Utils;

namespace Chungkang.GameNetwork.Database.Service
{
    public class ChattingDBService : DatabaseService
    {
        public List<ChatRoomInfo> GetAllRooms()
        {
            var list = new List<ChatRoomInfo>();
            var sql = @"
SELECT T_CHATROOM.ID, T_CHATROOM.NAME, T_CHATROOM.CREATER,
		T_CHATROOM_USERS.FLAG, T_CHATROOM_USERS.USER_ID, USER_M.NICKNAME
FROM T_CHATROOM
INNER JOIN T_CHATROOM_USERS 
ON T_CHATROOM.ID = T_CHATROOM_USERS.CHATROOM_ID
INNER JOIN USER_M
ON USER_M.ID = T_CHATROOM_USERS.USER_ID
";

            try
            {
                var reader = Handler.ExecuteReader(sql);
                while (reader.Read())
                {
                    var id = int.Parse((string)reader["ID"]);
                    var room = list.Find(x => x.Id.Equals(id));

                    if (room != null)
                    {
                        var user = new UserInfo((string)reader["USER_ID"], "", (string)reader["NICKNAME"]);
                        var chatUser = new ChatRoomUser { User = user, Flag = (ChatRoomUserFlag)int.Parse((string)reader["FLAG"]) };
                        room.Users.Add(chatUser);
                    }
                    else
                    {
                        room = new ChatRoomInfo()
                        {
                            Id = id,
                            Name = (string)reader["NAME"],
                            Creater = (string)reader["CREATER"]
                        };

                        var user = new UserInfo((string)reader["USER_ID"], "", (string)reader["NICKNAME"]);
                        var chatUser = new ChatRoomUser { User = user, Flag = (ChatRoomUserFlag)int.Parse((string)reader["FLAG"]) };
                        room.Users.Add(chatUser);

                        list.Add(room);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public void CreateChatRoom(ChatRoomInfo roomInfo)
        {
            var sql = @"
INSERT INTO T_CHATROOM (ID, NAME, CREATER)
VALUES (@ID, @NAME, @CREATER)
";

            var parameter = new Params()
            {
                new Param("@ID", roomInfo.Id),
                new Param("@NAME", roomInfo.Name),
                new Param("@CREATER", roomInfo.Creater),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameter);
                if(ret <= 0) throw new Exception("방이 만들어지지 않았습니다");
                JoinUsersInChatRoom(roomInfo);
                Handler.CommitTransaction();
            }
            catch (Exception)
            {
                Handler.RollbackTransaction();
                throw;
            }
            finally
            {
                parameter.Dispose();
            }
        }

        public void JoinUsersInChatRoom(ChatRoomInfo chatRoomInfo)
        {
            var sql = @"
INSERT INTO T_CHATROOM_USERS (CHATROOM_ID, USER_ID, FLAG)
VALUES (@CHATROOM_ID, @USER_ID, '1')
";

            var chatRoomId = new Param("@CHATROOM_ID", chatRoomInfo.Id);
            var userId = new Param("@USER_ID", "");

            try
            {
                foreach (var u in chatRoomInfo.Users)
                {
                    userId.Value = u.User.Id;
                    
                    var parameter = new Params
                    {
                        chatRoomId, userId
                    };

                    var ret = Handler.ExecuteNonQuery(sql, parameter);
                    parameter.Dispose();
                    if (ret <= 0) throw new Exception("유저가 참여되지 않았습니다.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LeaveUserInChatRoom(ChatRoomInfo chatRoomInfo, UserInfo user)
        {
            var sql = @"
 UPDATE T_CHATROOM_USERS 
    SET FLAG = '2'
  WHERE CHATROOM_ID = @CHATROOM_ID
    AND USER_ID = @USER_ID
";

            var parameter = new Params()
            {
                new Param("@CHATROOM_ID", chatRoomInfo.Id),
                new Param("@USER_ID", user.Id),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameter);
                if (ret <= 0) throw new Exception("방에서 나가기에 실패했습니다.");
                Handler.CommitTransaction();
            }
            catch (Exception)
            {
                Handler.RollbackTransaction();
                throw;
            }
            finally
            {
                parameter.Dispose();
            }
        }

        public void SendChat(Chat chat)
        {
            var sql = @"
INSERT INTO T_CHAT (CHATROOM_ID, SENDER, SEND_DTTM, MESSAGE)
VALUES (@CHATROOM_ID, @SENDER, @SEND_DTTM, @MESSAGE)
";

            var parameter = new Params()
            {
                new Param("@CHATROOM_ID", chat.ChatRoom.Id),
                new Param("@SENDER", chat.Sender.Id),
                new Param("@SEND_DTTM", chat.SendDttm.ToString()),
                new Param("@MESSAGE", chat.Message),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameter);
                if (ret <= 0) throw new Exception("채팅 보내기에 실패하였습니다.");
                Handler.CommitTransaction();
            }
            catch (Exception)
            {
                Handler.RollbackTransaction();
                throw;
            }
            finally
            {
                parameter.Dispose();
            }
        }
    }
}
