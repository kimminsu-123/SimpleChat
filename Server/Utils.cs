using Chungkang.GameNetwork.Database.Handler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Utils
{
    public struct DatabaseInfo
    {
        public const string connectionString = @"Data Source=.\database.db;Pooling=true;";
    }

    public static class DBUtils
    {
        private static DBHandler _handler;
        public static DBHandler Handler => _handler;

        public static void Initialize(DBHandler handler)
        {
            _handler = handler;
        }

        public static void Dispose()
        {
            _handler?.Dispose();
        }
    }

    public class Param
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public Param(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }

    public class Params : ICollection<Param>, IDisposable
    {
        private List<Param> list;

        public int Count => throw new NotImplementedException();
        public bool IsReadOnly => false;

        public Params()
        {
            list = new List<Param>();
        }

        public Param this[int idx]
        {
            get { return list[idx]; }
            set { list[idx] = value; }
        }

        public void Add(Param item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Param item)
        {
            return list.Contains(item);
        }

        public void CopyTo(Param[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public void Dispose()
        {
            Clear();
        }

        public IEnumerator<Param> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public bool Remove(Param item)
        {
            return list.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
