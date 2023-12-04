using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Manager;
using Chungkang.GameNetwork.Network.Manager;
using Chungkang.GameNetwork.Utils;
using System.Text.Json;

namespace Client
{
    public partial class Frm_FriendList : Form
    {
        private Friend? _selectedFriend;
        private FriendRequest? _selectedFriendRequest;

        private List<Friend> _friends;
        private List<FriendRequest> _friendRequests;

        public Frm_FriendList()
        {
            InitializeComponent();

            _friends = new List<Friend>();
            _friendRequests = new List<FriendRequest>();
        }

        private void btnReqFriend_Click(object sender, EventArgs e)
        {
            var id = txtUserId.Text.Trim();
            if(string.IsNullOrEmpty(id))
            {
                MessageBox.Show("추가하고 싶은 친구의 아이디를 입력하세요", "입력", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var request = new FriendRequest(Util.User, new UserInfo(id, "", ""));
            var reqFriendMessage = new WrapperMessage
            {
                Flag = MessageFlag.FriendRequest,
                JsonMessage = JsonSerializer.Serialize(request)
            };

            NetworkManager.Instance.UserManageServer.Send(reqFriendMessage);
        }

        private void btnDelFriend_Click(object sender, EventArgs e)
        {
            if(listFriendList.Items.Count <= 0)
            {
                MessageBox.Show("삭제할 친구가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listFriendList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("삭제할 친구를 선택하여 주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var id = listFriendList.SelectedItems[0].SubItems[1].Text.Trim();
            _selectedFriend = _friends.Find(f => f.FriendInfo.Id.Equals(id));

            var reqDelFriendMessage = new WrapperMessage
            {
                Flag = MessageFlag.DeleteFriend,
                JsonMessage = JsonSerializer.Serialize(_selectedFriend)
            };

            NetworkManager.Instance.UserManageServer.Send(reqDelFriendMessage);
        }

        private void btnAcceptFriend_Click(object sender, EventArgs e)
        {
            if (listReqList.Items.Count <= 0)
            {
                MessageBox.Show("수락할 요청이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listReqList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("수락할 요청을 선택하여 주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var id = listReqList.SelectedItems[0].SubItems[1].Text.Trim();
            _selectedFriendRequest = _friendRequests.Find(f => f.FriendInfo.Id.Equals(id));

            if (_selectedFriendRequest?.Flag != FriendRequestFlag.Request)
            {
                MessageBox.Show("이미 처리된 요청입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var reqAcceptFriendMessage = new WrapperMessage
            {
                Flag = MessageFlag.AcceptFriendRequest,
                JsonMessage = JsonSerializer.Serialize(_selectedFriendRequest)
            };

            NetworkManager.Instance.UserManageServer.Send(reqAcceptFriendMessage);
        }

        private void btnRefuseFriend_Click(object sender, EventArgs e)
        {
            if (listReqList.Items.Count <= 0)
            {
                MessageBox.Show("거절할 요청이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listReqList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("거절할 요청을 선택하여 주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var id = listReqList.SelectedItems[0].SubItems[1].Text.Trim();
            _selectedFriendRequest = _friendRequests.Find(f => f.FriendInfo.Id.Equals(id));

            if (_selectedFriendRequest?.Flag != FriendRequestFlag.Request)
            {
                MessageBox.Show("이미 처리된 요청입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var reqRefuseFriendMessage = new WrapperMessage
            {
                Flag = MessageFlag.RefuseFriendRequest,
                JsonMessage = JsonSerializer.Serialize(_selectedFriendRequest)
            };

            NetworkManager.Instance.UserManageServer.Send(reqRefuseFriendMessage);
        }

        private void Frm_FriendList_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            EventManager.Instance.AddListener(EventType.OnInqFriendList, OnEvent);
            EventManager.Instance.AddListener(EventType.OnInqFriendRequestList, OnEvent);
            EventManager.Instance.AddListener(EventType.OnFriendRequest, OnEvent);
            EventManager.Instance.AddListener(EventType.OnDeleteFriend, OnEvent);
            EventManager.Instance.AddListener(EventType.OnAcceptFriendRequest, OnEvent);
            EventManager.Instance.AddListener(EventType.OnRefuseFriendRequest, OnEvent);

            var reqFriendList = new WrapperMessage
            {
                Flag = MessageFlag.FriendList,
                JsonMessage = JsonSerializer.Serialize(Util.User)
            };

            var reqFriendRequestList = new WrapperMessage
            {
                Flag = MessageFlag.FriendRequestList,
                JsonMessage = JsonSerializer.Serialize(Util.User)
            };

            NetworkManager.Instance.UserManageServer.Send(reqFriendList);
            NetworkManager.Instance.UserManageServer.Send(reqFriendRequestList);
        }

        private void OnEvent(EventType type, object sender, object[] args)
        {
            var serverMsg = args[0] as ServerMessage;
            if (serverMsg == null) return;

            if (!serverMsg.ReturnValue)
            {
                MessageBox.Show(serverMsg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Invoke(() =>
            { 
                switch (type)
                {
                    case EventType.OnInqFriendList:
                        HandleInqFriendList(serverMsg);
                        break;
                    case EventType.OnInqFriendRequestList:
                        HandleInqFriendRequestList(serverMsg);
                        break;
                    case EventType.OnFriendRequest:
                        HandleFriendRequest(serverMsg);
                        break;
                    case EventType.OnDeleteFriend:
                        HandleDeleteFriend(serverMsg);
                        break;
                    case EventType.OnAcceptFriendRequest:
                        HandleAcceptFriendRequest(serverMsg);
                        break;
                    case EventType.OnRefuseFriendRequest:
                        HandleRefuseFriendRequest(serverMsg);
                        break;
                }
            });
        }

        private void HandleRefuseFriendRequest(ServerMessage serverMsg)
        {
            _friendRequests.Find(x => x.FriendInfo.Id.Equals(_selectedFriendRequest.FriendInfo.Id)).Flag = FriendRequestFlag.Refuse;
            UpdateFriendRequestList();
        }

        private void HandleAcceptFriendRequest(ServerMessage serverMsg)
        {
            _friendRequests.Find(x => x.FriendInfo.Id.Equals(_selectedFriendRequest.FriendInfo.Id)).Flag = FriendRequestFlag.Accept;
            UpdateFriendRequestList();
        }

        private void HandleDeleteFriend(ServerMessage serverMsg)
        {
            _friends.Remove(_selectedFriend);
            UpdateFriendList();
        }

        private void UpdateFriendList()
        {
            listFriendList.Items.Clear();
            int i = 1;

            foreach (var friend in _friends)
            {
                if (friend.Flag == FriendFlag.Deleted) continue;

                var item = new ListViewItem(new string[] { $"{i ++}", $"{friend.FriendInfo.Id}", $"{friend.FriendInfo.NickName}" });
                listFriendList.Items.Add(item);
            }
        }

        private void UpdateFriendRequestList()
        {
            listReqList.Items.Clear();
            int i = 1;

            foreach (var request in _friendRequests)
            {
                var item = new ListViewItem(new string[] { $"{i}", $"{request.FriendInfo.Id}", $"{request.FriendInfo.NickName}", $"{request.Flag}" });
                listReqList.Items.Add(item);
            }
        }

        private void HandleFriendRequest(ServerMessage serverMsg)
        {
            MessageBox.Show(serverMsg.Message, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HandleInqFriendRequestList(ServerMessage serverMsg)
        {
            var list = JsonSerializer.Deserialize<List<FriendRequest>>(serverMsg.Message);
            if (list == null || list.Count <= 0) return;

            for (int i = 0; i < list.Count; i++)
            {
                var request = list[i];
                
                _friendRequests.Add(request);
            }
            UpdateFriendRequestList();
        }

        private void HandleInqFriendList(ServerMessage serverMsg)
        {
            var list = JsonSerializer.Deserialize<List<Friend>>(serverMsg.Message);
            if (list == null || list.Count <= 0) return;

            for (int i = 0; i < list.Count; i++)
            {
                var friend = list[i];
                _friends.Add(friend);
            }
            UpdateFriendList();
        }

        private void Frm_FriendList_FormClosing(object sender, FormClosingEventArgs e)
        {
            EventManager.Instance.RemoveListener(EventType.OnInqFriendList, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnInqFriendRequestList, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnFriendRequest, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnDeleteFriend, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnAcceptFriendRequest, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnRefuseFriendRequest, OnEvent);
        }
    }
}
