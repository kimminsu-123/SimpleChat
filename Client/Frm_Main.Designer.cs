namespace Client
{
    partial class Frm_Main
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
            btnFriendList = new Button();
            lblUserName = new Label();
            layoutChatRooms = new TableLayoutPanel();
            chatRoom1 = new ChatRoom();
            btnRefresh = new Button();
            btnCreateRoom = new Button();
            listFriendList = new ListView();
            ColFLSeq = new ColumnHeader();
            ColFLId = new ColumnHeader();
            ColFLNickname = new ColumnHeader();
            txtRoomName = new TextBox();
            label1 = new Label();
            layoutChatRooms.SuspendLayout();
            SuspendLayout();
            // 
            // btnFriendList
            // 
            btnFriendList.Location = new Point(12, 48);
            btnFriendList.Name = "btnFriendList";
            btnFriendList.Size = new Size(85, 77);
            btnFriendList.TabIndex = 1;
            btnFriendList.Text = "친구 목록";
            btnFriendList.UseVisualStyleBackColor = true;
            btnFriendList.Click += btnFriendList_Click;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblUserName.Location = new Point(12, 9);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(54, 21);
            lblUserName.TabIndex = 2;
            lblUserName.Text = "label1";
            // 
            // layoutChatRooms
            // 
            layoutChatRooms.AutoScroll = true;
            layoutChatRooms.ColumnCount = 1;
            layoutChatRooms.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutChatRooms.Controls.Add(chatRoom1, 0, 6);
            layoutChatRooms.Location = new Point(103, 48);
            layoutChatRooms.Name = "layoutChatRooms";
            layoutChatRooms.RowCount = 7;
            layoutChatRooms.RowStyles.Add(new RowStyle());
            layoutChatRooms.RowStyles.Add(new RowStyle());
            layoutChatRooms.RowStyles.Add(new RowStyle());
            layoutChatRooms.RowStyles.Add(new RowStyle());
            layoutChatRooms.RowStyles.Add(new RowStyle());
            layoutChatRooms.RowStyles.Add(new RowStyle());
            layoutChatRooms.RowStyles.Add(new RowStyle());
            layoutChatRooms.Size = new Size(307, 172);
            layoutChatRooms.TabIndex = 0;
            // 
            // chatRoom1
            // 
            chatRoom1.BackColor = Color.Silver;
            chatRoom1.ChatRoomInfo = null;
            chatRoom1.Location = new Point(3, 3);
            chatRoom1.Name = "chatRoom1";
            chatRoom1.Size = new Size(300, 51);
            chatRoom1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(12, 143);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(85, 77);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "새로고침";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnCreateRoom
            // 
            btnCreateRoom.Location = new Point(12, 241);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(85, 77);
            btnCreateRoom.TabIndex = 4;
            btnCreateRoom.Text = "방생성";
            btnCreateRoom.UseVisualStyleBackColor = true;
            btnCreateRoom.Click += btnCreateRoom_Click;
            // 
            // listFriendList
            // 
            listFriendList.Columns.AddRange(new ColumnHeader[] { ColFLSeq, ColFLId, ColFLNickname });
            listFriendList.FullRowSelect = true;
            listFriendList.GridLines = true;
            listFriendList.LabelWrap = false;
            listFriendList.Location = new Point(103, 294);
            listFriendList.Name = "listFriendList";
            listFriendList.Size = new Size(307, 144);
            listFriendList.TabIndex = 10;
            listFriendList.UseCompatibleStateImageBehavior = false;
            listFriendList.View = View.Details;
            // 
            // ColFLSeq
            // 
            ColFLSeq.Text = "순번";
            ColFLSeq.Width = 50;
            // 
            // ColFLId
            // 
            ColFLId.Text = "아이디";
            ColFLId.TextAlign = HorizontalAlignment.Center;
            ColFLId.Width = 100;
            // 
            // ColFLNickname
            // 
            ColFLNickname.Text = "닉네임";
            ColFLNickname.TextAlign = HorizontalAlignment.Center;
            ColFLNickname.Width = 100;
            // 
            // txtRoomName
            // 
            txtRoomName.Location = new Point(103, 265);
            txtRoomName.Name = "txtRoomName";
            txtRoomName.Size = new Size(307, 23);
            txtRoomName.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(103, 247);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 12;
            label1.Text = "방 이름";
            // 
            // Frm_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(422, 450);
            Controls.Add(label1);
            Controls.Add(txtRoomName);
            Controls.Add(listFriendList);
            Controls.Add(btnCreateRoom);
            Controls.Add(btnRefresh);
            Controls.Add(lblUserName);
            Controls.Add(btnFriendList);
            Controls.Add(layoutChatRooms);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Frm_Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "채팅";
            FormClosed += Frm_Main_FormClosed;
            Load += Frm_Main_Load;
            layoutChatRooms.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnFriendList;
        private Label lblUserName;
        private TableLayoutPanel layoutChatRooms;
        private Button btnRefresh;
        private ChatRoom chatRoom1;
        private Button btnCreateRoom;
        private ListView listFriendList;
        private ColumnHeader ColFLSeq;
        private ColumnHeader ColFLId;
        private ColumnHeader ColFLNickname;
        private TextBox txtRoomName;
        private Label label1;
    }
}