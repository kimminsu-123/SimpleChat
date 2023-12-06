namespace Client
{
    partial class Frm_Register
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
            btnRegister = new Button();
            txtId = new TextBox();
            txtPw = new TextBox();
            txtRePw = new TextBox();
            txtNickName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(12, 204);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(301, 23);
            btnRegister.TabIndex = 5;
            btnRegister.Text = "회원가입";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // txtId
            // 
            txtId.Location = new Point(12, 28);
            txtId.MaxLength = 20;
            txtId.Name = "txtId";
            txtId.Size = new Size(301, 23);
            txtId.TabIndex = 1;
            // 
            // txtPw
            // 
            txtPw.Location = new Point(12, 72);
            txtPw.MaxLength = 20;
            txtPw.Name = "txtPw";
            txtPw.PasswordChar = '*';
            txtPw.Size = new Size(301, 23);
            txtPw.TabIndex = 2;
            // 
            // txtRePw
            // 
            txtRePw.Location = new Point(12, 116);
            txtRePw.MaxLength = 20;
            txtRePw.Name = "txtRePw";
            txtRePw.PasswordChar = '*';
            txtRePw.Size = new Size(301, 23);
            txtRePw.TabIndex = 3;
            // 
            // txtNickName
            // 
            txtNickName.Location = new Point(12, 160);
            txtNickName.MaxLength = 20;
            txtNickName.Name = "txtNickName";
            txtNickName.Size = new Size(301, 23);
            txtNickName.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 5;
            label1.Text = "ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 6;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 98);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 7;
            label3.Text = "Re-Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 142);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 8;
            label4.Text = "NickName";
            // 
            // Frm_Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(325, 239);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtNickName);
            Controls.Add(txtRePw);
            Controls.Add(txtPw);
            Controls.Add(txtId);
            Controls.Add(btnRegister);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Frm_Register";
            StartPosition = FormStartPosition.CenterParent;
            Text = "회원가입";
            FormClosed += Frm_Register_FormClosed;
            Load += Frm_Register_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRegister;
        private TextBox txtId;
        private TextBox txtPw;
        private TextBox txtRePw;
        private TextBox txtNickName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}