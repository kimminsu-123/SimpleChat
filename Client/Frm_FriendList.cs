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
        private ListViewItem _selectedFriend;
        private ListViewItem _selectedFriendRequest;

        public Frm_FriendList()
        {
            InitializeComponent();
        }

        private void btnReqFriend_Click(object sender, EventArgs e)
        {
            var id = txtUserId.Text.Trim();
            if(string.IsNullOrEmpty(id))
            {
                MessageBox.Show("추가하고 싶은 친구의 아이디를 입력하세요", "입력", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


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

            _selectedFriend = listFriendList.SelectedItems[0];
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

            _selectedFriendRequest = listReqList.SelectedItems[0];
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

            _selectedFriendRequest = listReqList.SelectedItems[0];
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
            _selectedFriendRequest.SubItems[3].Text = FriendRequestFlag.Refuse.ToString();
        }

        private void HandleAcceptFriendRequest(ServerMessage serverMsg)
        {
            _selectedFriendRequest.SubItems[3].Text = FriendRequestFlag.Accept.ToString();
        }

        private void HandleDeleteFriend(ServerMessage serverMsg)
        {
            listReqList.Items.Remove(_selectedFriend);
        }

        private void HandleFriendRequest(ServerMessage serverMsg)
        {
            var request = JsonSerializer.Deserialize<FriendRequest>(serverMsg.Message);
            if (request == null) return;

            var item = new ListViewItem(new string[] { $"{listReqList.Items.Count + 1}", $"{request.FriendInfo.Id}", $"{request.FriendInfo.NickName}", $"{request.Flag}" });
            listReqList.Items.Add(item);
        }

        private void HandleInqFriendRequestList(ServerMessage serverMsg)
        {
            var list = JsonSerializer.Deserialize<List<FriendRequest>>(serverMsg.Message);
            if (list == null || list.Count <= 0) return;

            listReqList.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                var request = list[i];
                var item = new ListViewItem(new string[] { $"{i + 1}", $"{request.FriendInfo.Id}", $"{request.FriendInfo.NickName}", $"{request.Flag}" });
                listReqList.Items.Add(item);
            }
        }

        private void HandleInqFriendList(ServerMessage serverMsg)
        {
            var list = JsonSerializer.Deserialize<List<Friend>>(serverMsg.Message);
            if (list == null || list.Count <= 0) return;

            listFriendList.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                var friend = list[i];
                var item = new ListViewItem(new string[] { $"{i + 1}", $"{friend.FriendInfo.Id}", $"{friend.FriendInfo.NickName}" });
                listFriendList.Items.Add(item);
            }
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
