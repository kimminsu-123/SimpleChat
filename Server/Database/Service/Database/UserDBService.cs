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
INSERT INTO T_FRIEND_REQUEST (REQ_ID, TARGET_ID, FLAG)
VALUES (@REQ_ID, @TARGET_ID, @FLAG)
";

            var parameters = new Params()
            {
                new Param("@REQ_ID", request.MyId),
                new Param("@TARGET_ID", request.FriendId),
                new Param("@FLAG", 1),
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
    SET FLAG = '2',
        MOD_DT = datetime('now', 'localtime'),
  WHERE REQ_ID = @REQ_ID
    AND TARGET_ID = @TARGET_ID";

            var insertSql = @"
 INSERT INTO T_FRIEND (USER_ID, TARGET_ID, FLAG)
 VALUES (@USER_ID, @FRIEND_ID, '1')";

            var parameters = new Params()
            {
                new Param("@REQ_ID", request.MyId),
                new Param("@TARGET_ID", request.FriendId),
            };

            var parameters2 = new Params()
            {
                new Param("@USER_ID", request.MyId),
                new Param("@FRIEND_ID", request.FriendId),
            };

            var parameters3 = new Params()
            {
                new Param("@USER_ID", request.FriendId),
                new Param("@FRIEND_ID", request.MyId),
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
    SET FLAG = '3',
        MOD_DT = datetime('now', 'localtime'),
  WHERE REQ_ID = @REQ_ID
    AND TARGET_ID = @TARGET_ID";

            var parameters = new Params()
            {
                new Param("@REQ_ID", request.MyId),
                new Param("@TARGET_ID", request.FriendId),
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
    AND TARGET_ID = @TARGET_ID";

            var parameters = new Params()
            {
                new Param("@USER_ID", delFriend.MyId),
                new Param("@TARGET_ID", delFriend.FriendId),
            };

            var parameters2 = new Params()
            {
                new Param("@USER_ID", delFriend.FriendId),
                new Param("@TARGET_ID", delFriend.MyId),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameters);
                ret += Handler.ExecuteNonQuery(sql, parameters);
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
SELECT  B.USER_ID, B.NICKNAME
  FROM  T_FRIEND AS A
 WHERE  A.USER_ID = @USER_ID
 INNER JOIN USER_M AS B
    ON  A.USER_ID = B.USER_ID";

            var parameters = new Params()
            {
                new Param("@USER_ID", user.Id),
            };

            var list = new List<FriendRequest>();

            try
            {
                var reader = Handler.ExecuteReader(sql, parameters);

                while (reader.Read())
                {
                    list.Add(new FriendRequest(
                        (string) reader["REQ_ID"],
                        (string) reader["TARGET_ID"]
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
            throw new NotImplementedException();
        }
    }
}
