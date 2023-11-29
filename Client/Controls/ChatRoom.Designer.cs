namespace Client
{
    partial class ChatRoom
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            txtLastChat = new TextBox();
            lblRoomName = new Label();
            SuspendLayout();
            // 
            // txtLastChat
            // 
            txtLastChat.BackColor = Color.FromArgb(224, 224, 224);
            txtLastChat.BorderStyle = BorderStyle.None;
            txtLastChat.Enabled = false;
            txtLastChat.Font = new Font("맑은 고딕", 15F, FontStyle.Regular, GraphicsUnit.Point);
            txtLastChat.Location = new Point(3, 47);
            txtLastChat.Name = "txtLastChat";
            txtLastChat.ReadOnly = true;
            txtLastChat.Size = new Size(294, 27);
            txtLastChat.TabIndex = 0;
            // 
            // lblRoomName
            // 
            lblRoomName.AutoSize = true;
            lblRoomName.Location = new Point(3, 16);
            lblRoomName.Name = "lblRoomName";
            lblRoomName.Size = new Size(67, 15);
            lblRoomName.TabIndex = 1;
            lblRoomName.Text = "채팅방이름";
            // 
            // ChatRoom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            Controls.Add(lblRoomName);
            Controls.Add(txtLastChat);
            Name = "ChatRoom";
            Size = new Size(300, 77);
            DoubleClick += OnDoubleClick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLastChat;
        private Label lblRoomName;
    }
}
