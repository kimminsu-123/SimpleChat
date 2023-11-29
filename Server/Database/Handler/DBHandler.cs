using Chungkang.GameNetwork.Utils;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Database.Handler
{
    public class DBHandler
    {
        private DbConnection _connection;
        private DbCommand _command;

        public DBHandler(DbConnection dbConnection)
        { 
            _connection = dbConnection;
        }

        public void BeginTransaction()
        {
            try
            {
                _command = _connection.CreateCommand();
                _command.Transaction = _connection.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CommitTransaction()
        {
            if (_command == null) throw new NullReferenceException("commit transaction before begin transaction");
            if (_command.Transaction == null) throw new NullReferenceException("commit transaction before begin transaction");

            try
            {
                _command.Transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

            _command.Dispose();
        }
        
        public void RollbackTransaction()
        {
            if (_command == null) throw new NullReferenceException("commit transaction before begin transaction");
            if (_command.Transaction == null) throw new NullReferenceException("commit transaction before begin transaction");

            try
            {
                _command.Transaction.Rollback();
            }
            catch (Exception)
            {
                throw;
            }

            _command.Dispose();
        }

        public DbDataReader ExecuteReader(string sql, Params? parameters = null)
        {
            if (string.IsNullOrEmpty(sql.Trim())) throw new ArgumentNullException("sql is not null or empty");

            _command = _connection.CreateCommand();
            _command.CommandText = sql;

            if(parameters != null)
            {
                foreach(var item in parameters)
                {
                    _command.Parameters.Add(new SqliteParameter(item.Name, item.Value));
                }
            }

            try
            {
                return _command.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ExecuteNonQuery(string sql, Params? parameters)
        {
            if (string.IsNullOrEmpty(sql.Trim())) throw new ArgumentNullException("sql is not null or empty");

            _command = _connection.CreateCommand();
            _command.CommandText = sql;

            if(parameters != null)
            {
                foreach(var item in parameters)
                {
                    _command.Parameters.Add(new SqliteParameter(item.Name, item.Value));
                }
            }

            try
            {
                return _command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;       
            }
            finally
            {
                _command.Dispose();
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _command?.Dispose();
        }
    }
}
