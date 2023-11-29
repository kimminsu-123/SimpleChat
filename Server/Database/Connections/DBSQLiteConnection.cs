using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Database.Connections
{
    public class DBSQLiteConnection : SqliteConnection
    {
        public bool IsConnected { get; private set; }

        public DBSQLiteConnection(string connectionString) : base(connectionString) { }

        public override void Open()
        {
            try
            {
                base.Open();
                IsConnected = true;
            }
            catch (SqliteException)
            {
                IsConnected = false;
                throw;
            }
        }

        public override void Close()
        {
            IsConnected = false;
            base.Close();
        }
    }
}
