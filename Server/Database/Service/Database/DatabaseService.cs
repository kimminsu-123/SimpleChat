using Chungkang.GameNetwork.Database.Handler;
using Chungkang.GameNetwork.Utils;

namespace Chungkang.GameNetwork.Database.Service
{
    public class DatabaseService
    {
        protected DBHandler Handler => DBUtils.Handler;
    }
}
