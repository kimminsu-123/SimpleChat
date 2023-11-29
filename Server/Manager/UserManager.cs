﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Manager
{
    public class UserManager
    {
        private static UserManager _instance;
        public static UserManager Instance 
        {
            get
            {
                if(_instance == null)
                    _instance = new UserManager();

                return _instance;
            }
        }

    }
}
