using Chungkang.GameNetwork.Database.Handler;
using Chungkang.GameNetwork.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Database.Service
{
    public class DatabaseService
    {
        protected DBHandler Handler => DBUtils.Handler;
    }
}
