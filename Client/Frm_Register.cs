using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using Chungkang.GameNetwork.Network.Manager;
using Chungkang.GameNetwork.Manager;
using System.Text.Json;

namespace Client
{
    public partial class Frm_Register : Form
    {
        public Frm_Register()
        {
            InitializeComponent();
        }

        private void OnRegisterCallback(EventType type, object sender, params object[] args)
        {
            var serverMsg = args[0] as ServerMessage;
            if (serverMsg == null) return;

            if (!serverMsg.ReturnValue)
            {
                MessageBox.Show(serverMsg.Message, "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(serverMsg.Message, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Invoke(() => Close());
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var id = txtId.Text.Trim();
            var pw = txtPw.Text.Trim();
            var rePw = txtRePw.Text.Trim();
            var nickName = txtNickName.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("아이디를 입력하세요.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("비밀번호를 입력하세요.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(rePw))
            {
                MessageBox.Show("비밀번호가 맞지 않습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(nickName))
            {
                MessageBox.Show("닉네임을 입력하세요.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!pw.Equals(rePw))
            {
                MessageBox.Show("비밀번호가 맞지 않습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var userInfo = new UserInfo(id, pw, nickName);
            var wrapperMessage = new WrapperMessage
            {
                Flag = MessageFlag.Register,
                JsonMessage = JsonSerializer.Serialize(userInfo)
            };

            try
            {
                NetworkManager.Instance.UserManageServer.Send(wrapperMessage);
            }
            catch (Exception err)
            {
                MessageBox.Show($"회원가입 요청에 실패하였습니다. : {err.Message}", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frm_Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            EventManager.Instance.RemoveListener(EventType.OnRegister, OnRegisterCallback);
        }

        private void Frm_Register_Load(object sender, EventArgs e)
        {
            EventManager.Instance.AddListener(EventType.OnRegister, OnRegisterCallback);
        }
    }
}
