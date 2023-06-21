namespace HKInc.Main
{
    partial class ChangeSettingKor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeSettingKor));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelSkin = new System.Windows.Forms.Label();
            this.labelNotification = new System.Windows.Forms.Label();
            this.labelConfirmPassword = new System.Windows.Forms.Label();
            this.labelNewPassword = new System.Windows.Forms.Label();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.lupSkin = new DevExpress.XtraEditors.ComboBoxEdit();
            this.rdoGrpNoti = new DevExpress.XtraEditors.RadioGroup();
            this.txtCnfPwd = new DevExpress.XtraEditors.TextEdit();
            this.txtNewPwd = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupSkin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoGrpNoti.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCnfPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelSkin);
            this.panelControl1.Controls.Add(this.labelNotification);
            this.panelControl1.Controls.Add(this.labelConfirmPassword);
            this.panelControl1.Controls.Add(this.labelNewPassword);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnConfirm);
            this.panelControl1.Controls.Add(this.lupSkin);
            this.panelControl1.Controls.Add(this.rdoGrpNoti);
            this.panelControl1.Controls.Add(this.txtCnfPwd);
            this.panelControl1.Controls.Add(this.txtNewPwd);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(429, 329);
            this.panelControl1.TabIndex = 0;
            // 
            // labelSkin
            // 
            this.labelSkin.AutoSize = true;
            this.labelSkin.Location = new System.Drawing.Point(98, 181);
            this.labelSkin.Name = "labelSkin";
            this.labelSkin.Size = new System.Drawing.Size(33, 18);
            this.labelSkin.TabIndex = 13;
            this.labelSkin.Text = "Skin";
            this.labelSkin.Visible = false;
            // 
            // labelNotification
            // 
            this.labelNotification.AutoSize = true;
            this.labelNotification.Location = new System.Drawing.Point(54, 131);
            this.labelNotification.Name = "labelNotification";
            this.labelNotification.Size = new System.Drawing.Size(78, 18);
            this.labelNotification.TabIndex = 12;
            this.labelNotification.Text = "Notification";
            this.labelNotification.Visible = false;
            // 
            // labelConfirmPassword
            // 
            this.labelConfirmPassword.AutoSize = true;
            this.labelConfirmPassword.Location = new System.Drawing.Point(18, 78);
            this.labelConfirmPassword.Name = "labelConfirmPassword";
            this.labelConfirmPassword.Size = new System.Drawing.Size(119, 18);
            this.labelConfirmPassword.TabIndex = 9;
            this.labelConfirmPassword.Text = "ConfirmPassword";
            // 
            // labelNewPassword
            // 
            this.labelNewPassword.AutoSize = true;
            this.labelNewPassword.Location = new System.Drawing.Point(32, 39);
            this.labelNewPassword.Name = "labelNewPassword";
            this.labelNewPassword.Size = new System.Drawing.Size(102, 18);
            this.labelNewPassword.TabIndex = 8;
            this.labelNewPassword.Text = "New Password";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(241, 243);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cacel";
            // 
            // btnConfirm
            // 
            this.btnConfirm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirm.ImageOptions.Image")));
            this.btnConfirm.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnConfirm.Location = new System.Drawing.Point(85, 243);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(86, 30);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "OK";
            // 
            // lupSkin
            // 
            this.lupSkin.Location = new System.Drawing.Point(145, 177);
            this.lupSkin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lupSkin.Name = "lupSkin";
            this.lupSkin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupSkin.Size = new System.Drawing.Size(243, 24);
            this.lupSkin.TabIndex = 5;
            this.lupSkin.Visible = false;
            // 
            // rdoGrpNoti
            // 
            this.rdoGrpNoti.Location = new System.Drawing.Point(145, 117);
            this.rdoGrpNoti.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdoGrpNoti.Name = "rdoGrpNoti";
            this.rdoGrpNoti.Properties.Columns = 2;
            this.rdoGrpNoti.Size = new System.Drawing.Size(243, 44);
            this.rdoGrpNoti.TabIndex = 4;
            this.rdoGrpNoti.Visible = false;
            // 
            // txtCnfPwd
            // 
            this.txtCnfPwd.Location = new System.Drawing.Point(145, 75);
            this.txtCnfPwd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCnfPwd.Name = "txtCnfPwd";
            this.txtCnfPwd.Properties.MaxLength = 15;
            this.txtCnfPwd.Properties.UseSystemPasswordChar = true;
            this.txtCnfPwd.Size = new System.Drawing.Size(243, 24);
            this.txtCnfPwd.TabIndex = 1;
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Location = new System.Drawing.Point(145, 35);
            this.txtNewPwd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.Properties.MaxLength = 15;
            this.txtNewPwd.Properties.UseSystemPasswordChar = true;
            this.txtNewPwd.Size = new System.Drawing.Size(243, 24);
            this.txtNewPwd.TabIndex = 0;
            // 
            // ChangeSettingKor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 329);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChangeSettingKor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Setting";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupSkin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoGrpNoti.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCnfPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPwd.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label labelSkin;
        private System.Windows.Forms.Label labelNotification;
        private System.Windows.Forms.Label labelConfirmPassword;
        private System.Windows.Forms.Label labelNewPassword;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.ComboBoxEdit lupSkin;
        private DevExpress.XtraEditors.RadioGroup rdoGrpNoti;
        private DevExpress.XtraEditors.TextEdit txtCnfPwd;
        private DevExpress.XtraEditors.TextEdit txtNewPwd;
    }
}