namespace HKInc.Main
{
    partial class DatabaseIP_Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseIP_Setting));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.BaseFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.tx_ip = new DevExpress.XtraEditors.TextEdit();
            this.tx_db = new DevExpress.XtraEditors.TextEdit();
            this.tx_user = new DevExpress.XtraEditors.TextEdit();
            this.tx_passwd = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton1item = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton2item = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseFormlayoutControl1ConvertedLayout)).BeginInit();
            this.BaseFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_db.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_user.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_passwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleButton1item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleButton2item)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(11, 106);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(123, 26);
            this.simpleButton1.StyleController = this.BaseFormlayoutControl1ConvertedLayout;
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "적용";
            this.simpleButton1.Click += new System.EventHandler(this.SimpleButton1_Click);
            // 
            // BaseFormlayoutControl1ConvertedLayout
            // 
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.simpleButton2);
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.simpleButton1);
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.tx_ip);
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.tx_db);
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.tx_user);
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.tx_passwd);
            this.BaseFormlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseFormlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 24);
            this.BaseFormlayoutControl1ConvertedLayout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BaseFormlayoutControl1ConvertedLayout.Name = "BaseFormlayoutControl1ConvertedLayout";
            this.BaseFormlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.BaseFormlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(290, 133);
            this.BaseFormlayoutControl1ConvertedLayout.TabIndex = 9;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(138, 106);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(124, 26);
            this.simpleButton2.StyleController = this.BaseFormlayoutControl1ConvertedLayout;
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "취소";
            this.simpleButton2.Click += new System.EventHandler(this.SimpleButton2_Click);
            // 
            // tx_ip
            // 
            this.tx_ip.Location = new System.Drawing.Point(54, 10);
            this.tx_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_ip.Name = "tx_ip";
            this.tx_ip.Size = new System.Drawing.Size(208, 20);
            this.tx_ip.StyleController = this.BaseFormlayoutControl1ConvertedLayout;
            this.tx_ip.TabIndex = 9;
            // 
            // tx_db
            // 
            this.tx_db.Location = new System.Drawing.Point(54, 34);
            this.tx_db.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_db.Name = "tx_db";
            this.tx_db.Size = new System.Drawing.Size(208, 20);
            this.tx_db.StyleController = this.BaseFormlayoutControl1ConvertedLayout;
            this.tx_db.TabIndex = 10;
            // 
            // tx_user
            // 
            this.tx_user.Location = new System.Drawing.Point(54, 58);
            this.tx_user.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_user.Name = "tx_user";
            this.tx_user.Size = new System.Drawing.Size(208, 20);
            this.tx_user.StyleController = this.BaseFormlayoutControl1ConvertedLayout;
            this.tx_user.TabIndex = 11;
            // 
            // tx_passwd
            // 
            this.tx_passwd.Location = new System.Drawing.Point(54, 82);
            this.tx_passwd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_passwd.Name = "tx_passwd";
            this.tx_passwd.Properties.PasswordChar = '*';
            this.tx_passwd.Size = new System.Drawing.Size(208, 20);
            this.tx_passwd.StyleController = this.BaseFormlayoutControl1ConvertedLayout;
            this.tx_passwd.TabIndex = 12;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.simpleButton1item,
            this.simpleButton2item});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(273, 142);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tx_ip;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(255, 24);
            this.layoutControlItem1.Text = "ip";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(40, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tx_db;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(255, 24);
            this.layoutControlItem2.Text = "db";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(40, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tx_user;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(255, 24);
            this.layoutControlItem3.Text = "user";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(40, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tx_passwd;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(255, 24);
            this.layoutControlItem4.Text = "passwd";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(40, 14);
            // 
            // simpleButton1item
            // 
            this.simpleButton1item.Control = this.simpleButton1;
            this.simpleButton1item.Location = new System.Drawing.Point(0, 96);
            this.simpleButton1item.Name = "simpleButton1item";
            this.simpleButton1item.Size = new System.Drawing.Size(127, 30);
            this.simpleButton1item.TextSize = new System.Drawing.Size(0, 0);
            this.simpleButton1item.TextVisible = false;
            // 
            // simpleButton2item
            // 
            this.simpleButton2item.Control = this.simpleButton2;
            this.simpleButton2item.Location = new System.Drawing.Point(127, 96);
            this.simpleButton2item.Name = "simpleButton2item";
            this.simpleButton2item.Size = new System.Drawing.Size(128, 30);
            this.simpleButton2item.TextSize = new System.Drawing.Size(0, 0);
            this.simpleButton2item.TextVisible = false;
            // 
            // DatabaseIP_Setting
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 180);
            this.Controls.Add(this.BaseFormlayoutControl1ConvertedLayout);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("DatabaseIP_Setting.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DatabaseIP_Setting";
            this.Text = "DatabaseIP_Setting";
            this.Controls.SetChildIndex(this.BaseFormlayoutControl1ConvertedLayout, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseFormlayoutControl1ConvertedLayout)).EndInit();
            this.BaseFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_ip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_db.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_user.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_passwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleButton1item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleButton2item)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControl BaseFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.TextEdit tx_ip;
        private DevExpress.XtraEditors.TextEdit tx_db;
        private DevExpress.XtraEditors.TextEdit tx_user;
        private DevExpress.XtraEditors.TextEdit tx_passwd;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem simpleButton1item;
        private DevExpress.XtraLayout.LayoutControlItem simpleButton2item;
    }
}