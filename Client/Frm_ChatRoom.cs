using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Manager;
using Chungkang.GameNetwork.Network.Manager;
using Chungkang.GameNetwork.Utils;
using System.Text.Json;

namespace Client
{
    public partial class Frm_ChatRoom : Form
    {
        private ChatRoomInfo _chatRoomInfo;

        public Frm_ChatRoom(ChatRoomInfo roomInfo)
        {
            InitializeComponent();

            _chatRoomInfo = roomInfo;
        }

        private void Frm_ChatRoom_Load(object sender, EventArgs e)
        {
            Initialize();

            InqAllChat(_chatRoomInfo);
        }

        private void Initialize()
        {
            listBoxChattings.Items.Clear();
            Text = _chatRoomInfo.Name;
            EventManager.Instance.AddListener(EventType.OnReceiveChat, OnEvent);
            EventManager.Instance.AddListener(EventType.OnInqChatsInRoom, OnEvent);
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            SendChat(textChat.Text);
        }

        private void textChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            SendChat(textChat.Text);
        }

        private void SendChat(string chat)
        {
            if (string.IsNullOrEmpty(chat))
            {
                MessageBox.Show("메세지를 입력하세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var wrapperChat = new Chat()
            {
                ChatRoom = _chatRoomInfo,
                Sender = Util.User,
                SendDttm = DateTime.Now,
                Message = chat
            };
            var msg = new WrapperMessage()
            {
                Flag = MessageFlag.SendChat,
                JsonMessage = JsonSerializer.Serialize(wrapperChat)
            };

            try
            {
                NetworkManager.Instance.ChatServer.Send(msg);
                textChat.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"메세지 전송 중 오류: {ex.Message}");
            }
        }

        private void InqAllChat(ChatRoomInfo room)
        {
            var msg = new WrapperMessage()
            {
                Flag = MessageFlag.InqChatsInRoom,
                JsonMessage = JsonSerializer.Serialize(room)
            };

            try
            {
                NetworkManager.Instance.ChatServer.Send(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"메세지 전송 중 오류: {ex.Message}");
            }
        }

        private void OnEvent(EventType type, object sender, object[] args)
        {
            var serverMsg = args[0] as ServerMessage;
            if (serverMsg == null) return;

            if (!serverMsg.ReturnValue)
            {
                MessageBox.Show($"{serverMsg.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Invoke(() =>
            {
                switch (type)
                {
                    case EventType.OnReceiveChat:
                        HandleReceiveChat(JsonSerializer.Deserialize<Chat>(serverMsg.Message));
                        break;
                    case EventType.OnInqChatsInRoom:
                        if (serverMsg.RequesterIP != NetworkManager.Instance.ChatServer.LocalEndPoint.Address.ToString()) break;
                        if (serverMsg.RequesterPort != NetworkManager.Instance.ChatServer.LocalEndPoint.Port) break;

                        HandleInqAllChatsInRoom(JsonSerializer.Deserialize<List<Chat>>(serverMsg.Message));
                        break;
                }
            });
        }

        private void HandleReceiveChat(Chat? chat)
        {
            if (chat == null) return;
            if (!chat.ChatRoom.Id.Equals(_chatRoomInfo.Id)) return;

            var chatStr = $"[{chat.SendDttm.ToString("yyyy-MM-dd HH:mm:ss")}] {chat.Sender.NickName} : {chat.Message}";
            listBoxChattings.Items.Add(chatStr);
            listBoxChattings.TopIndex = listBoxChattings.Items.Count - 1;
        }

        private void HandleInqAllChatsInRoom(List<Chat>? chats)
        {
            if (chats == null || chats.Count <= 0) return;
            if (!chats[0].ChatRoom.Id.Equals(_chatRoomInfo.Id)) return;
            
            foreach ( var chat in chats)
            {
                HandleReceiveChat(chat);
            }
        }

        private void Frm_ChatRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            EventManager.Instance.RemoveListener(EventType.OnReceiveChat, OnEvent);
            EventManager.Instance.RemoveListener(EventType.OnInqChatsInRoom, OnEvent);
        }
    }
}
