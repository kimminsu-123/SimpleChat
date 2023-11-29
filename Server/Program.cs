using Chungkang.GameNetwork.Database.Connections;
using Chungkang.GameNetwork.Database.Handler;
using Chungkang.GameNetwork.Utils;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Network.Server;
using System.Reflection.Metadata.Ecma335;

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
            var sql = new string[3];

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
REQ_FLAG TEXT DEFAULT '1',
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