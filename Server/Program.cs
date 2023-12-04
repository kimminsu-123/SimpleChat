using Chungkang.GameNetwork.Database.Connections;
using Chungkang.GameNetwork.Database.Handler;
using Chungkang.GameNetwork.Utils;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Network.Server;

namespace Chungkang.GameNetwork.Server
{
    public static class Program
    {
        private static List<TCPServer> servers;

        private static void Main()
        {
            try
            {
                Initialize();

                Booting();
            }
            catch(Exception err)
            {
                Console.WriteLine($"Program Main Exception : {err}");
                return;
            }

            while (true) { }
        }

        private static void Booting()
        {
            try
            {
                servers.Add(new UserManagementTCPServer(ServerInfo.userManagePort));
                servers.Add(new ChattingTCPServer(ServerInfo.chatPort));

                foreach (var s in servers)
                {
                    s.Start();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void Initialize()
        {
            servers = new List<TCPServer> ();

            try
            {
                var connection = new DBSQLiteConnection(DatabaseInfo.connectionString);
                connection.Open();
                DBUtils.Initialize(new DBHandler(connection));

                CreateTables();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CreateTables()
        {
            var sql = new string[6];

            sql[0] = @"
CREATE TABLE IF NOT EXISTS USER_M 
(
ID TEXT PRIMARY KEY, 
PW TEXT NOT NULL DEFAULT '', 
NICKNAME TEXT NOT NULL DEFAULT '', 
INS_DT TEXT DEFAULT (datetime('now', 'localtime')), 
INT_US TEXT DEFAULT 'system', 
MOD_DT TEXT DEFAULT (datetime('now', 'localtime')), 
MOD_US TEXT DEFAULT 'system'
)";

            // 1: 요청, 2: 수락, 3: 거절
            sql[1] = @"
CREATE TABLE IF NOT EXISTS T_FRIEND_REQUEST 
(
REQ_ID TEXT NOT NULL, 
TARGET_ID TEXT NOT NULL,
REQ_FLAG TEXT DEFAULT '0',
INS_DT TEXT DEFAULT (datetime('now', 'localtime')), 
INT_US TEXT DEFAULT 'system', 
MOD_DT TEXT DEFAULT (datetime('now', 'localtime')), 
MOD_US TEXT DEFAULT 'system',
PRIMARY KEY (REQ_ID, TARGET_ID)
) ";
            
            sql[2] = @"
CREATE TABLE IF NOT EXISTS T_FRIEND 
(
USER_ID TEXT NOT NULL, 
FRIEND_ID TEXT NOT NULL,
FLAG TEXT NOT NULL DEFAULT '1',
INS_DT TEXT DEFAULT (datetime('now', 'localtime')), 
INT_US TEXT DEFAULT 'system', 
MOD_DT TEXT DEFAULT (datetime('now', 'localtime')), 
MOD_US TEXT DEFAULT 'system',
PRIMARY KEY (USER_ID, FRIEND_ID, FLAG)
) ";

            sql[3] = @"
CREATE TABLE IF NOT EXISTS T_CHATROOM 
(
ID          TEXT NOT NULL DEFAULT '', 
NAME        TEXT NOT NULL DEFAULT '',
CREATER     TEXT NOT NULL DEFAULT '',
INS_DT      TEXT DEFAULT (datetime('now', 'localtime')), 
INT_US      TEXT DEFAULT 'system', 
MOD_DT      TEXT DEFAULT (datetime('now', 'localtime')), 
MOD_US      TEXT DEFAULT 'system',
PRIMARY KEY (ID)
) ";

            sql[4] = @"
CREATE TABLE IF NOT EXISTS T_CHATROOM_USERS 
(
CHATROOM_ID TEXT NOT NULL DEFAULT '', 
USER_ID     TEXT NOT NULL DEFAULT '',
FLAG        TEXT NOT NULL DEFAULT '0',
INS_DT      TEXT DEFAULT (datetime('now', 'localtime')), 
INT_US      TEXT DEFAULT 'system', 
MOD_DT      TEXT DEFAULT (datetime('now', 'localtime')), 
MOD_US      TEXT DEFAULT 'system',
PRIMARY KEY (CHATROOM_ID, USER_ID)
) ";

            sql[5] = @"
CREATE TABLE IF NOT EXISTS T_CHAT 
(
CHATROOM_ID TEXT NOT NULL DEFAULT '', 
SEQ         INTEGER PRIMARY KEY AUTOINCREMENT,
SENDER      TEXT NOT NULL DEFAULT '',
SEND_DTTM   TEXT NOT NULL DEFAULT (datetime('now', 'localtime')),
MESSAGE     TEXT NOT NULL DEFAULT '',
INS_DT      TEXT DEFAULT (datetime('now', 'localtime')), 
INT_US      TEXT DEFAULT 'system', 
MOD_DT      TEXT DEFAULT (datetime('now', 'localtime')), 
MOD_US      TEXT DEFAULT 'system',
UNIQUE (CHATROOM_ID, SEQ) 
)  ";

            try
            {
                DBUtils.Handler?.BeginTransaction();
                for(int i = 0; i < sql.Length; i++)
                {
                    DBUtils.Handler?.ExecuteNonQuery(sql[i], null);
                }
                DBUtils.Handler?.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DBUtils.Handler?.RollbackTransaction();
            }
        }
    }
}