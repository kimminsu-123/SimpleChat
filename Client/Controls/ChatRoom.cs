using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Network.Manager;
using Chungkang.GameNetwork.Utils;
using System.Text.Json;

namespace Client
{
    public partial class ChatRoom : UserControl
    {
        private ChatRoomInfo _chatRoomInfo;
        public ChatRoomInfo ChatRoomInfo
        {
            get
            {
                return _chatRoomInfo;
            }

            set
            {
                _chatRoomInfo = value;
            }
        }

        public ChatRoom()
        {
            InitializeComponent();
        }

        private void ChatRoom_Load(object sender, EventArgs e)
        {
            lblRoomName.Text = _chatRoomInfo.Name;
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            var frmChatRoom = new Frm_ChatRoom(_chatRoomInfo);
            frmChatRoom.ShowDialog();
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            var room = _chatRoomInfo;
            var chatUser = new ChatRoomUser { User = Util.User, Flag = ChatRoomUserFlag.Normal };
            room.Users.Clear();
            room.Users.Add(chatUser);
            var msg = new WrapperMessage
            {
                Flag = MessageFlag.LeaveChatRoom,
                JsonMessage = JsonSerializer.Serialize(room)
            };

            NetworkManager.Instance.ChatServer.Send(msg);
        }
    }
}
