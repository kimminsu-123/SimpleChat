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
                new Param("@REQ_ID", request.sourceId),
                new Param("@TARGET_ID", request.targetId),
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

            var insertSql1 = @"
 INSERT INTO T_FRIEND (USER_ID, TARGET_ID)
 VALUES (@USER_ID, @FRIEND_ID)";

            var insertSql2 = @"
 INSERT INTO T_FRIEND (USER_ID, TARGET_ID)
 VALUES (@FRIEND_ID, @USER_ID)";

            var parameters = new Params()
            {
                new Param("@REQ_ID", request.sourceId),
                new Param("@TARGET_ID", request.targetId),
            };

            var parameters2 = new Params()
            {
                new Param("@USER_ID", request.sourceId),
                new Param("@FRIEND_ID", request.targetId),
            };

            var parameters3 = new Params()
            {
                new Param("@USER_ID", request.targetId),
                new Param("@FRIEND_ID", request.sourceId),
            };

            try
            {
                Handler.BeginTransaction();
                var ret = Handler.ExecuteNonQuery(sql, parameters);
                ret += Handler.ExecuteNonQuery(insertSql1, parameters2);
                ret += Handler.ExecuteNonQuery(insertSql2, parameters3);
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
                new Param("@REQ_ID", request.sourceId),
                new Param("@TARGET_ID", request.targetId),
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
    }
}
