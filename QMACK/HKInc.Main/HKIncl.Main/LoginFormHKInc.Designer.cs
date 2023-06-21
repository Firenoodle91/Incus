﻿namespace HKInc.Main
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
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemember.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadioGroupMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
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
            this.pictureEdit2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureEdit2.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(60, 86);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit2.Size = new System.Drawing.Size(204, 87);
            this.pictureEdit2.TabIndex = 7;
            this.pictureEdit2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureEdit2_MouseClick);
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
            this.lbLogin.Appearance.Options.UseFont = true;
            this.lbLogin.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbLogin.Location = new System.Drawing.Point(595, 262);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(101, 94);
            this.lbLogin.TabIndex = 3;
            this.lbLogin.Text = "                         ";
            // 
            // chkRemember
            // 
            this.chkRemember.Location = new System.Drawing.Point(373, 355);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Properties.Caption = "RememberMe";
            this.chkRemember.Size = new System.Drawing.Size(124, 22);
            this.chkRemember.TabIndex = 2;
            // 
            // RadioGroupMode
            // 
            this.RadioGroupMode.Location = new System.Drawing.Point(370, 173);
            this.RadioGroupMode.Name = "RadioGroupMode";
            this.RadioGroupMode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.RadioGroupMode.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RadioGroupMode.Properties.Appearance.Options.UseBackColor = true;
            this.RadioGroupMode.Properties.Appearance.Options.UseFont = true;
            this.RadioGroupMode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.RadioGroupMode.Properties.Columns = 2;
            this.RadioGroupMode.Size = new System.Drawing.Size(305, 83);
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
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(427, 442);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEdit1.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "Korea",
            "English",
            "china"});
            this.comboBoxEdit1.Size = new System.Drawing.Size(150, 34);
            this.comboBoxEdit1.TabIndex = 13;
            this.comboBoxEdit1.Visible = false;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "컷팅",
            "면취 ",
            "가공",
            "검사, 포장"});
            this.comboBox1.Location = new System.Drawing.Point(513, 386);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(203, 32);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Visible = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // LoginFormHKInc
            // 
            this.Appearance.BackColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBoxEdit1);
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
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
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
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}