using Chungkang.GameNetwork.Common.Message;
using Chungkang.GameNetwork.Common.Util;
using System.Text.Json;
using Chungkang.GameNetwork.Network.Manager;
using Chungkang.GameNetwork.Manager;
using Chungkang.GameNetwork.Utils;

namespace Client
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void OnLoginCallback(EventType type, object sender, params object[] args)
        {
            var serverMsg = args[0] as ServerMessage;
            if (serverMsg == null) return;

            if (!serverMsg.ReturnValue)
            {
                MessageBox.Show(serverMsg.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Util.User = JsonSerializer.Deserialize<UserInfo>(serverMsg.Message);

            Invoke(() =>
            {
                var mainForm = new Frm_Main();
                mainForm.Show();
                Hide();
            });
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var id = txtId.Text.Trim();
            var pw = txtPw.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show($"아이디를 입력하세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(pw))
            {
                MessageBox.Show($"비밀번호를 입력하세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userInfo = new UserInfo(id, pw);
            var json = JsonSerializer.Serialize(userInfo);

            var wrapperMsg = new WrapperMessage
            {
                Flag = MessageFlag.Login,
                JsonMessage = json
            };

            try
            {
                NetworkManager.Instance.UserManageServer.Send(wrapperMsg);
            }
            catch (Exception err)
            {
                MessageBox.Show($"로그인 실패 : {err.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            new Frm_Register().ShowDialog();
        }

        private void Frm_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            EventManager.Instance.RemoveListener(EventType.OnLogin, OnLoginCallback);
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            EventManager.Instance.AddListener(EventType.OnLogin, OnLoginCallback);
        }
    }
}