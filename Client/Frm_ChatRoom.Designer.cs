namespace Client
{
    partial class Frm_ChatRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textChat = new TextBox();
            btnSendChat = new Button();
            layoutChatRooms = new TableLayoutPanel();
            chatRoom1 = new ChatRoom();
            listBoxChattings = new ListBox();
            SuspendLayout();
            // 
            // textChat
            // 
            textChat.Location = new Point(12, 220);
            textChat.Multiline = true;
            textChat.Name = "textChat";
            textChat.Size = new Size(328, 23);
            textChat.TabIndex = 0;
            textChat.KeyDown += textChat_KeyDown;
            // 
            // btnSendChat
            // 
            btnSendChat.Location = new Point(346, 220);
            btnSendChat.Name = "btnSendChat";
            btnSendChat.Size = new Size(75, 23);
            btnSendChat.TabIndex = 1;
            btnSendChat.Text = "전 송";
            btnSendChat.UseVisualStyleBackColor = true;
            btnSendChat.Click += btnSendChat_Click;
            // 
            // layoutChatRooms
            // 
            layoutChatRooms.AutoScroll = true;
            layoutChatRooms.ColumnCount = 1;
            layoutChatRooms.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutChatRooms.Location = new Point(0, 0);
            layoutChatRooms.Name = "layoutChatRooms";
            layoutChatRooms.RowCount = 7;
            layoutChatRooms.Size = new Size(200, 100);
            layoutChatRooms.TabIndex = 0;
            // 
            // chatRoom1
            // 
            chatRoom1.BackColor = Color.Silver;
            chatRoom1.ChatRoomInfo = null;
            chatRoom1.Location = new Point(3, 3);
            chatRoom1.Name = "chatRoom1";
            chatRoom1.Size = new Size(194, 51);
            chatRoom1.TabIndex = 0;
            // 
            // listBoxChattings
            // 
            listBoxChattings.FormattingEnabled = true;
            listBoxChattings.ItemHeight = 15;
            listBoxChattings.Location = new Point(12, 12);
            listBoxChattings.Name = "listBoxChattings";
            listBoxChattings.Size = new Size(409, 199);
            listBoxChattings.TabIndex = 2;
            // 
            // Frm_ChatRoom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(433, 255);
            Controls.Add(listBoxChattings);
            Controls.Add(btnSendChat);
            Controls.Add(textChat);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Frm_ChatRoom";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "채팅방 이름";
            FormClosed += Frm_ChatRoom_FormClosed;
            Load += Frm_ChatRoom_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textChat;
        private Button btnSendChat;
        private TableLayoutPanel layoutChatRooms;
        private ChatRoom chatRoom1;
        private ListBox listBoxChattings;
    }
}