using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Database.Service;
using System.Diagnostics.Tracing;

namespace Chungkang.GameNetwork.Service
{
    public class UserService
    {
        private UserDBService _service;

        public UserService()
        {
            _service = new UserDBService();
        }

        public bool Login(ref UserInfo? user)
        {
            if (user == null) throw new ArgumentException("login: user is null");

            try
            {
                return _service.ValidateUser(ref user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckDuplicatedId(UserInfo? user)
        {
            if (user == null) throw new ArgumentException("duplicatedId: user is null");

            try
            {
                return _service.CheckDuplicatedId(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Resigister(UserInfo? user)
        {
            if (user == null) throw new ArgumentException("register: user is null");

            try
            {
                return _service.RegisterUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserInfo? FindUser(UserInfo? user)
        {
            if (user == null) throw new ArgumentNullException("find user: user is null");

            try
            {
                _service.ValidateUser(ref user);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool FriendRequest(FriendRequest? request)
        {
            if (request == null) throw new ArgumentException("friend request: request is null");

            try
            {
                return _service.FriendRequest(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AcceptFriendRequest(FriendRequest? request)
        {
            if (request == null) throw new ArgumentException("accept friend request: request is null");

            try
            {
                return _service.AcceptFriendRequest(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RefuseFriendRequest(FriendRequest? request)
        {
            if (request == null) throw new ArgumentException("refuse friend request: request is null");

            try
            {
                return _service.RefuseFriendRequest(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteFriend(Friend? friend)
        {
            if (friend == null) throw new ArgumentException("delete friend: friend is null");

            try
            {
                return _service.DeleteFriend(friend);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<FriendRequest> GetFriendRequests(UserInfo? user)
        {
            if (user == null) throw new ArgumentException("delete friend: friend is null");

            try
            {
                return _service.GetFriendRequests(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Friend> GetFriends(UserInfo? user)
        {
            if (user == null) throw new ArgumentException("delete friend: friend is null");

            try
            {
                return _service.GetFriends(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
