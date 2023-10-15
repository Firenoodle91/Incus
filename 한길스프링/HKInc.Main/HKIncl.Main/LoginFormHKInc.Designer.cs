namespace HKInc.Main
{
    partial class LoginFormHKInc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginFormHKInc));
            this.lbCancel = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.txtLoginId = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lbLogin = new DevExpress.XtraEditors.LabelControl();
            this.chkRemember = new DevExpress.XtraEditors.CheckEdit();
            this.RadioGroupMode = new DevExpress.XtraEditors.RadioGroup();
            this.btnDatabaseIP = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemember.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadioGroupMode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCancel
            // 
            this.lbCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbCancel.Appearance.Options.UseForeColor = true;
            this.lbCancel.Location = new System.Drawing.Point(653, 450);
            this.lbCancel.Name = "lbCancel";
            this.lbCancel.Size = new System.Drawing.Size(63, 18);
            this.lbCancel.TabIndex = 4;
            this.lbCancel.Text = "    Exit    ";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 453);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(344, 18);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Copyright ⓒ 2019 HKInc ,INCUS All Rights Reserved";
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(42, 88);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit2.Size = new System.Drawing.Size(242, 98);
            this.pictureEdit2.TabIndex = 7;
            // 
            // txtLoginId
            // 
            this.txtLoginId.Location = new System.Drawing.Point(370, 274);
            this.txtLoginId.Name = "txtLoginId";
            this.txtLoginId.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtLoginId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginId.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtLoginId.Properties.Appearance.Options.UseBackColor = true;
            this.txtLoginId.Properties.Appearance.Options.UseFont = true;
            this.txtLoginId.Properties.Appearance.Options.UseForeColor = true;
            this.txtLoginId.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtLoginId.Size = new System.Drawing.Size(207, 28);
            this.txtLoginId.TabIndex = 0;
            this.txtLoginId.Click += new System.EventHandler(this.txtLoginId_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(370, 318);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(207, 28);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Click += new System.EventHandler(this.txtPassword_Click);
            // 
            // lbLogin
            // 
            this.lbLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.lbLogin.Appearance.Options.UseFont = true;
            this.lbLogin.Appearance.Options.UseForeColor = true;
            this.lbLogin.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbLogin.Location = new System.Drawing.Point(602, 268);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(80, 84);
            this.lbLogin.TabIndex = 3;
            this.lbLogin.Text = "로그인";
            // 
            // chkRemember
            // 
            this.chkRemember.Location = new System.Drawing.Point(370, 355);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Properties.Caption = "ID저장";
            this.chkRemember.Size = new System.Drawing.Size(69, 22);
            this.chkRemember.TabIndex = 2;
            // 
            // RadioGroupMode
            // 
            this.RadioGroupMode.Location = new System.Drawing.Point(360, 183);
            this.RadioGroupMode.Name = "RadioGroupMode";
            this.RadioGroupMode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.RadioGroupMode.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RadioGroupMode.Properties.Appearance.Options.UseBackColor = true;
            this.RadioGroupMode.Properties.Appearance.Options.UseFont = true;
            this.RadioGroupMode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.RadioGroupMode.Size = new System.Drawing.Size(356, 79);
            this.RadioGroupMode.TabIndex = 11;
            // 
            // btnDatabaseIP
            // 
            this.btnDatabaseIP.Location = new System.Drawing.Point(602, 16);
            this.btnDatabaseIP.Name = "btnDatabaseIP";
            this.btnDatabaseIP.Size = new System.Drawing.Size(127, 23);
            this.btnDatabaseIP.TabIndex = 12;
            this.btnDatabaseIP.Text = "Database IP Setting";
            this.btnDatabaseIP.Visible = false;
            // 
            // LoginFormHKInc
            // 
            this.Appearance.BackColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.btnDatabaseIP);
            this.Controls.Add(this.RadioGroupMode);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.pictureEdit2);
            this.Controls.Add(this.chkRemember);
            this.Controls.Add(this.lbLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLoginId);
            this.Controls.Add(this.lbCancel);
            this.DoubleBuffered = true;
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginFormHKInc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemember.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadioGroupMode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lbCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.TextEdit txtLoginId;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lbLogin;
        private DevExpress.XtraEditors.CheckEdit chkRemember;
        private DevExpress.XtraEditors.RadioGroup RadioGroupMode;
        private DevExpress.XtraEditors.SimpleButton btnDatabaseIP;
    }
}