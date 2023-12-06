namespace Client
{
    partial class Frm_FriendList
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
            btnReqFriend = new Button();
            txtUserId = new TextBox();
            btnDelFriend = new Button();
            btnAcceptFriend = new Button();
            btnRefuseFriend = new Button();
            listFriendList = new ListView();
            ColFLSeq = new ColumnHeader();
            ColFLId = new ColumnHeader();
            ColFLNickname = new ColumnHeader();
            listReqList = new ListView();
            ColFRLSeq = new ColumnHeader();
            ColFRLId = new ColumnHeader();
            ColFRLNickName = new ColumnHeader();
            ColFRLFlag = new ColumnHeader();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnReqFriend
            // 
            btnReqFriend.Location = new Point(12, 62);
            btnReqFriend.Name = "btnReqFriend";
            btnReqFriend.Size = new Size(230, 36);
            btnReqFriend.TabIndex = 1;
            btnReqFriend.Text = "친구 요청";
            btnReqFriend.UseVisualStyleBackColor = true;
            btnReqFriend.Click += btnReqFriend_Click;
            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(12, 12);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(467, 23);
            txtUserId.TabIndex = 5;
            // 
            // btnDelFriend
            // 
            btnDelFriend.Location = new Point(12, 104);
            btnDelFriend.Name = "btnDelFriend";
            btnDelFriend.Size = new Size(230, 36);
            btnDelFriend.TabIndex = 6;
            btnDelFriend.Text = "친구 삭제";
            btnDelFriend.UseVisualStyleBackColor = true;
            btnDelFriend.Click += btnDelFriend_Click;
            // 
            // btnAcceptFriend
            // 
            btnAcceptFriend.Location = new Point(249, 62);
            btnAcceptFriend.Name = "btnAcceptFriend";
            btnAcceptFriend.Size = new Size(230, 36);
            btnAcceptFriend.TabIndex = 7;
            btnAcceptFriend.Text = "친구 수락";
            btnAcceptFriend.UseVisualStyleBackColor = true;
            btnAcceptFriend.Click += btnAcceptFriend_Click;
            // 
            // btnRefuseFriend
            // 
            btnRefuseFriend.Location = new Point(249, 104);
            btnRefuseFriend.Name = "btnRefuseFriend";
            btnRefuseFriend.Size = new Size(230, 36);
            btnRefuseFriend.TabIndex = 8;
            btnRefuseFriend.Text = "친구 거절";
            btnRefuseFriend.UseVisualStyleBackColor = true;
            btnRefuseFriend.Click += btnRefuseFriend_Click;
            // 
            // listFriendList
            // 
            listFriendList.Columns.AddRange(new ColumnHeader[] { ColFLSeq, ColFLId, ColFLNickname });
            listFriendList.FullRowSelect = true;
            listFriendList.GridLines = true;
            listFriendList.LabelWrap = false;
            listFriendList.Location = new Point(12, 184);
            listFriendList.MultiSelect = false;
            listFriendList.Name = "listFriendList";
            listFriendList.Size = new Size(230, 161);
            listFriendList.TabIndex = 9;
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
            // listReqList
            // 
            listReqList.Columns.AddRange(new ColumnHeader[] { ColFRLSeq, ColFRLId, ColFRLNickName, ColFRLFlag });
            listReqList.FullRowSelect = true;
            listReqList.GridLines = true;
            listReqList.LabelWrap = false;
            listReqList.Location = new Point(250, 184);
            listReqList.MultiSelect = false;
            listReqList.Name = "listReqList";
            listReqList.Size = new Size(230, 161);
            listReqList.TabIndex = 10;
            listReqList.UseCompatibleStateImageBehavior = false;
            listReqList.View = View.Details;
            // 
            // ColFRLSeq
            // 
            ColFRLSeq.Text = "순번";
            ColFRLSeq.Width = 50;
            // 
            // ColFRLId
            // 
            ColFRLId.Text = "아이디";
            ColFRLId.TextAlign = HorizontalAlignment.Center;
            ColFRLId.Width = 100;
            // 
            // ColFRLNickName
            // 
            ColFRLNickName.Text = "닉네임";
            ColFRLNickName.TextAlign = HorizontalAlignment.Center;
            ColFRLNickName.Width = 100;
            // 
            // ColFRLFlag
            // 
            ColFRLFlag.Text = "구분";
            ColFRLFlag.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 166);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 11;
            label1.Text = "친구 목록";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(249, 166);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 12;
            label2.Text = "요청 목록";
            // 
            // Frm_FriendList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 357);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listReqList);
            Controls.Add(listFriendList);
            Controls.Add(btnRefuseFriend);
            Controls.Add(btnAcceptFriend);
            Controls.Add(btnDelFriend);
            Controls.Add(txtUserId);
            Controls.Add(btnReqFriend);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Frm_FriendList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "친구 목록";
            FormClosed += Frm_FriendList_FormClosed;
            Load += Frm_FriendList_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnReqFriend;
        private TextBox txtUserId;
        private Button btnDelFriend;
        private Button btnAcceptFriend;
        private Button btnRefuseFriend;
        private ListView listFriendList;
        private ListView listReqList;
        private Label label1;
        private Label label2;
        private ColumnHeader ColFLSeq;
        private ColumnHeader ColFLId;
        private ColumnHeader ColFLNickname;
        private ColumnHeader ColFRLSeq;
        private ColumnHeader ColFRLId;
        private ColumnHeader ColFRLNickName;
        private ColumnHeader ColFRLFlag;
    }
}