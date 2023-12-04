using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Manager;
using Chungkang.GameNetwork.Network.Manager;
using Chungkang.GameNetwork.Utils;
using System.Data;
using System.Diagnostics;
using System.Text.Json;

namespace Client
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
            layoutChatRooms.Controls.Clear();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            EventManager.Instance.AddListener(EventType.OnInqFriendList, OnEvent);
            EventManager.Instance.AddListener(EventType.OnInqChatRooms, OnEvent);
            EventManager.Instance.AddListener(EventType.OnCreateChatRoom, OnEvent);
            EventManager.Instance.AddListener(EventType.OnLeaveChatRoom, OnEvent);
            EventManager.Instance.AddListener(EventType.OnChatSend, OnEvent);

            lblUserName.Text = Util.User?.NickName;

            btnRefresh_Click(sender, e);
        }

        private void OnEvent(EventType type, object sender, object[] args)
        {
            var serverMsg = args[0] as ServerMessage;
            if (serverMsg == null) return;

            if (!serverMsg.ReturnValue)
            {
                MessageBox.Show(serverMsg.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Invoke(() =>
            {
                switch (type)
                {
                    case EventType.OnInqFriendList:
                        var list = JsonSerializer.Deserialize<List<Friend>>(serverMsg.Message);
                        HandleInqFriendList(list);
                        break;
                    case EventType.OnInqChatRooms:
                        var chatRooms = JsonSerializer.Deserialize<List<ChatRoomInfo>>(serverMsg.Message);
                        HandleInqChatRooms(chatRooms);
                        break;
                    case EventType.OnCreateChatRoom:
                        var chatRoom = JsonSerializer.Deserialize<ChatRoomInfo>(serverMsg.Message);
                        HandleCreateChatRoom(chatRoom);
                        break;
                    case EventType.OnLeaveChatRoom:
                        HandleLeaveChatRoom(serverMsg);
                        break;
                }
            });
        }

        private void HandleInqFriendList(List<Friend> list)
        {
            if (list == null || list.Count <= 0) return;

            listFriendList.Items.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                var friend = list[i];

                if (friend.Flag == FriendFlag.Deleted) continue;

                var item = new ListViewItem(new string[] { $"{i + 1}", $"{friend.FriendInfo.Id}", $"{friend.FriendInfo.NickName}" });
                listFriendList.Items.Add(item);
            }
        }

        private void HandleInqChatRooms(List<ChatRoomInfo> list)
        {
            var chatRooms = list.Where(x => x.Users.Any(u => u.User.Id.Equals(Util.User.Id))).ToList();
            if (chatRooms == null || chatRooms.Count <= 0) return;

            layoutChatRooms.Controls.Clear();
            foreach (var room in chatRooms)
            {
                ChatRoom roomUI = new ChatRoom()
                {
                    ChatRoomInfo = room,
                };

                layoutChatRooms.Controls.Add(roomUI);
            }
        }

        private void HandleCreateChatRoom(ChatRoomInfo roomInfo)
        {
            if (roomInfo == null) return;
            if (!roomInfo.Users.Any(u => u.User.Id.Equals(Util.User.Id))) return;
            
            ChatRoom roomUI = new ChatRoom()
            {
                ChatRoomInfo = roomInfo,
            };

            layoutChatRooms.Controls.Add(roomUI);
        }

        private void HandleLeaveChatRoom(ServerMessage serverMsg)
        {
            var chatRoom = JsonSerializer.Deserialize<ChatRoomInfo>(serverMsg.Message);
            if (chatRoom == null)
            {
                MessageBox.Show("성공한 채팅방이 없습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!chatRoom.Users.Any(u => u.User.Id.Equals(Util.User.Id))) return;

            foreach (var control in layoutChatRooms.Controls)
            {
                var room = control as ChatRoom;
                if (room == null) continue;

                if (room.ChatRoomInfo.Id.Equals(chatRoom.Id))
                {
                    layoutChatRooms.Controls.Remove(room);
                    break;
                }
            }

            MessageBox.Show("나가기에 성공하였습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            EventManager.Instance.RemoveListener(EventType.OnInqFriendList, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnInqChatRooms, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnCreateChatRoom, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnLeaveChatRoom, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnChatSend, OnEvent);

            Process.GetCurrentProcess().Kill();
        }

        private void btnFriendList_Click(object sender, EventArgs e)
        {
            var frmFriendList = new Frm_FriendList();
            frmFriendList.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var msg = new WrapperMessage
            {
                Flag = MessageFlag.InqChatRooms,
                JsonMessage = JsonSerializer.Serialize(Util.User)
            };
            NetworkManager.Instance.ChatServer.Send(msg);

            var reqFriendList = new WrapperMessage
            {
                Flag = MessageFlag.FriendList,
                JsonMessage = JsonSerializer.Serialize(Util.User)
            };

            NetworkManager.Instance.UserManageServer.Send(reqFriendList);
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            var roomName = txtRoomName.Text.Trim();
            var users = new List<ChatRoomUser>();

            if (string.IsNullOrEmpty(roomName))
            {
                MessageBox.Show("방 이름을 설정해주세요");
                return;
            }

            if (listFriendList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("초대할 친구를 선택해주세요");
                return;
            }

            users.Add(new ChatRoomUser() { User = Util.User, Flag = ChatRoomUserFlag.Normal });

            foreach(ListViewItem item in listFriendList.SelectedItems)
            {
                var user = new UserInfo(item.SubItems[1].Text, "", item.SubItems[2].Text);
                users.Add(new ChatRoomUser() { User = user, Flag = ChatRoomUserFlag.Normal});
            }

            var chatRoom = new ChatRoomInfo()
            {
                Name = roomName,
                Creater = Util.User.NickName,
                Users = users
            };

            var msg = new WrapperMessage()
            {
                Flag = MessageFlag.CreateChatRoom,
                JsonMessage = JsonSerializer.Serialize(chatRoom)
            };

            NetworkManager.Instance.ChatServer.Send(msg);
        }
    }
}
