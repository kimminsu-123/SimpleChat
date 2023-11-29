using Chungkang.GameNetwork.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Util.User?.NickName;
        }

        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void btnFriendList_Click(object sender, EventArgs e)
        {
            var frmFriendList = new Frm_FriendList();
            frmFriendList.ShowDialog();
        }
    }
}
