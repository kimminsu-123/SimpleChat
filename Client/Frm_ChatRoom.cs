using Chungkang.GameNetwork.Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void Initialize()
        {
            Text = _chatRoomInfo.Name;
        }

        private void Frm_ChatRoom_Load(object sender, EventArgs e)
        {
            Initialize();
        }
    }
}
