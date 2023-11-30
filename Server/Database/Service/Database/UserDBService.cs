using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Utils;

namespace Chungkang.GameNetwork.Database.Service
{
    public class UserDBService : DatabaseService
    {
        public bool ValidateUser(ref UserInfo user)
        {
            var sql = @"
SELECT  ID, PW, NICKNAME
  FROM  USER_M    
 WHERE  ID = @ID
   AND  PW = @PW
";

            var parameters = new Params()
            {
                new Param("@ID", user.Id.Trim()),
                new Param("@PW", user.Password.Trim()),
            };

            try
            {
                var reader = Handler.ExecuteReader(sql, parameters);

                if (!reader.HasRows) return false;

                reader.Read();
                user.Id = (string)reader["ID"];
                user.Password = (string)reader["PW"];
                user.NickName = (string)reader["NICKNAME"];

                return reader.HasRows;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                parameters.Dispose();
            }
        }

        public bool CheckDuplicatedId(UserInfo user)
        {
            var sql = @"
SELECT  1
  FROM  USER_M    
 WHERE  ID = @ID
";

            var parameters = new Params()
            {
                new Param("@ID", user.Id.Trim()),
            };

            try
            {
                return Handler.ExecuteReader(sql, parameters).HasRows;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parameters.Dispose();
            }
        }

        public bool RegisterUser(UserInfo user)
        {
            var sql = @"
INSERT INTO USER_M (ID, PW, NICKNAME)
VALUES (@ID, @PW, @NICKNAME)
";

            var parameters = new Params()
            {
                new Param("@ID", user.Id.Trim()),
                new Param("@PW", user.Password.Trim()),
                new Param("@NICKNAME", user.NickName.Trim()),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameters) > 0 ? true : false;
                Handler.CommitTransaction();

                return ret;
            }
            catch (Exception)
            {
                Handler.RollbackTransaction();

                throw;
            }
            finally
            {
                parameters.Dispose();
            }
        }

        public bool FriendRequest(FriendRequest request)
        {
            var sql = @"
INSERT INTO T_FRIEND_REQUEST (REQ_ID, TARGET_ID, REQ_FLAG)
VALUES (@REQ_ID, @TARGET_ID, @REQ_FLAG)
";

            var parameters = new Params()
            {
                new Param("@REQ_ID", request.MyInfo),
                new Param("@TARGET_ID", request.FriendInfo),
                new Param("@REQ_FLAG", "1"),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameters) > 0 ? true : false;
                Handler.CommitTransaction();
                return ret;
            }
            catch (Exception)
            {
                Handler.RollbackTransaction();
                throw;
            }
            finally
            {
                parameters.Dispose();
            }
        }

        public bool AcceptFriendRequest(FriendRequest request)
        {
            var sql = @"
 UPDATE T_FRIEND_REQUEST
    SET REQ_FLAG = '2',
        MOD_DT = datetime('now', 'localtime'),
  WHERE REQ_ID = @REQ_ID
    AND TARGET_ID = @TARGET_ID";

            var insertSql = @"
 INSERT INTO T_FRIEND (USER_ID, FRIEND_ID, FLAG)
 VALUES (@USER_ID, @FRIEND_ID, '1')";

            var parameters = new Params()
            {
                new Param("@REQ_ID", request.MyInfo),
                new Param("@TARGET_ID", request.FriendInfo),
            };

            var parameters2 = new Params()
            {
                new Param("@USER_ID", request.MyInfo),
                new Param("@FRIEND_ID", request.FriendInfo),
            };

            var parameters3 = new Params()
            {
                new Param("@USER_ID", request.FriendInfo),
                new Param("@FRIEND_ID", request.MyInfo),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameters);
                ret += Handler.ExecuteNonQuery(insertSql, parameters2);
                ret += Handler.ExecuteNonQuery(insertSql, parameters3);
                Handler.CommitTransaction();

                return ret > 0;
            }
            catch (Exception)
            {
                Handler.RollbackTransaction();
                throw;
            }
            finally 
            { 
                parameters.Dispose();
                parameters2.Dispose();
                parameters3.Dispose();
            }
        }

        public bool RefuseFriendRequest(FriendRequest request)
        {
            var sql = @"
 UPDATE T_FRIEND_REQUEST
    SET REQ_FLAG = '3',
        MOD_DT = datetime('now', 'localtime'),
  WHERE REQ_ID = @REQ_ID
    AND TARGET_ID = @TARGET_ID";

            var parameters = new Params()
            {
                new Param("@REQ_ID", request.MyInfo),
                new Param("@TARGET_ID", request.FriendInfo),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameters) > 0 ? true : false;
                Handler.CommitTransaction();
                return ret;
            }
            catch (Exception)
            {
                Handler.RollbackTransaction();
                throw;
            }
            finally
            {
                parameters.Dispose();
            }
        }

        public bool DeleteFriend(Friend delFriend)
        {
            var sql = @"
 UPDATE T_FRIEND
    SET FLAG = '2',
        MOD_DT = datetime('now', 'localtime'),
  WHERE USER_ID = @USER_ID
    AND FRIEND_ID = @FRIEND_ID";

            var parameters = new Params()
            {
                new Param("@USER_ID", delFriend.MyInfo.Id),
                new Param("@FRIEND_ID", delFriend.FriendInfo.Id),
            };

            var parameters2 = new Params()
            {
                new Param("@USER_ID", delFriend.FriendInfo.Id),
                new Param("@FRIEND_ID", delFriend.MyInfo.Id),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameters);
                ret += Handler.ExecuteNonQuery(sql, parameters2);
                Handler.CommitTransaction();
                return ret > 0;
            }
            catch (Exception)
            {
                Handler.RollbackTransaction();
                throw;
            }
            finally
            {
                parameters.Dispose();
            }
        }

        public List<FriendRequest> GetFriendRequests(UserInfo user)
        {
            var sql = @"
SELECT  USER_M.ID, USER_M.NICKNAME, T_FRIEND_REQUEST.REQ_FLAG
  FROM  T_FRIEND_REQUEST 
 INNER JOIN USER_M
    ON  T_FRIEND_REQUEST.TARGET_ID = USER_M.ID
WHERE  T_FRIEND_REQUEST.REQ_ID = @REQ_ID";

            var parameters = new Params()
            {
                new Param("@REQ_ID", user.Id),
            };

            var list = new List<FriendRequest>();

            try
            {
                var reader = Handler.ExecuteReader(sql, parameters);

                while (reader.Read())
                {
                    var friendInfo = new UserInfo((string)reader["ID"], "", (string)reader["NICKNAME"]);
                    var flag = (FriendRequestFlag)Enum.Parse(typeof(FriendRequestFlag), (string)reader["REQ_FLAG"]);
                    list.Add(new FriendRequest(
                        user,
                        friendInfo,
                        flag
                    ));
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parameters.Dispose();
            }
        }

        public List<Friend> GetFriends(UserInfo user)
        {
            var sql = @"
SELECT  USER_M.ID, USER_M.NICKNAME, T_FRIEND.FLAG
  FROM  T_FRIEND 
INNER JOIN USER_M 
    ON  T_FRIEND.FRIEND_ID = USER_M.ID
 WHERE  T_FRIEND.USER_ID = @USER_ID";

            var parameters = new Params()
            {
                new Param("@USER_ID", user.Id),
            };

            var list = new List<Friend>();

            try
            {
                var reader = Handler.ExecuteReader(sql, parameters);

                while (reader.Read())
                {
                    var friend = new UserInfo((string)reader["ID"], "", (string)reader["NICKNAME"]);
                    var flag = (FriendFlag)Enum.Parse(typeof(FriendFlag), (string)reader["FLAG"]);
                    list.Add(new Friend(user, friend, flag));
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parameters.Dispose();
            }
        }
    }
}
