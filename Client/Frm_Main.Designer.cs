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
            tableLayoutPanel1 = new TableLayoutPanel();
            chatRoom6 = new ChatRoom();
            chatRoom5 = new ChatRoom();
            chatRoom4 = new ChatRoom();
            chatRoom3 = new ChatRoom();
            btnFriendList = new Button();
            lblUserName = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(chatRoom6, 0, 5);
            tableLayoutPanel1.Controls.Add(chatRoom5, 0, 4);
            tableLayoutPanel1.Controls.Add(chatRoom4, 0, 3);
            tableLayoutPanel1.Controls.Add(chatRoom3, 0, 2);
            tableLayoutPanel1.Location = new Point(103, 48);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(307, 390);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // chatRoom6
            // 
            chatRoom6.BackColor = Color.Silver;
            chatRoom6.Location = new Point(3, 252);
            chatRoom6.Name = "chatRoom6";
            chatRoom6.Size = new Size(300, 77);
            chatRoom6.TabIndex = 5;
            // 
            // chatRoom5
            // 
            chatRoom5.BackColor = Color.Silver;
            chatRoom5.Location = new Point(3, 169);
            chatRoom5.Name = "chatRoom5";
            chatRoom5.Size = new Size(300, 77);
            chatRoom5.TabIndex = 4;
            // 
            // chatRoom4
            // 
            chatRoom4.BackColor = Color.Silver;
            chatRoom4.Location = new Point(3, 86);
            chatRoom4.Name = "chatRoom4";
            chatRoom4.Size = new Size(300, 77);
            chatRoom4.TabIndex = 3;
            // 
            // chatRoom3
            // 
            chatRoom3.BackColor = Color.Silver;
            chatRoom3.Location = new Point(3, 3);
            chatRoom3.Name = "chatRoom3";
            chatRoom3.Size = new Size(300, 77);
            chatRoom3.TabIndex = 2;
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
            // Frm_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(422, 450);
            Controls.Add(lblUserName);
            Controls.Add(btnFriendList);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Frm_Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "채팅";
            FormClosed += Frm_Main_FormClosed;
            Load += Frm_Main_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ChatRoom chatRoom6;
        private ChatRoom chatRoom5;
        private ChatRoom chatRoom4;
        private ChatRoom chatRoom3;
        private Button btnFriendList;
        private Label lblUserName;
    }
}