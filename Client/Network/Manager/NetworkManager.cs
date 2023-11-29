using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Network.Manager
{
    public class NetworkManager
    {
        private static NetworkManager _instance;
        public static NetworkManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new NetworkManager();
                return _instance;
            }
        }

        public UserManagementTCPClient UserManageServer { get; private set; }

        public void Initialize()
        {
            try
            {
                UserManageServer = new UserManagementTCPClient(ServerInfo.serverIp, ServerInfo.userManagePort);

                UserManageServer.Initialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Start()
        {
            try
            {
                UserManageServer.Connect();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Release()
        {
            UserManageServer.Dispose();
        }
    }
}
