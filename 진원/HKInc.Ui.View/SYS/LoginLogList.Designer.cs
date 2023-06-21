namespace HKInc.Ui.View.SYS
{
    partial class LoginLogList
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcGroupUserList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGroupCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.dateLogdate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.lcItemLogDate = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupUserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemLogDate)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dateLogdate);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 509);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 110);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(888, 375);
            this.gridEx1.TabIndex = 5;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcGroupUserList,
            this.lcGroupCondition});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 509);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcGroupUserList
            // 
            this.lcGroupUserList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcGroupUserList.Location = new System.Drawing.Point(0, 67);
            this.lcGroupUserList.Name = "lcGroupUserList";
            this.lcGroupUserList.Size = new System.Drawing.Size(916, 422);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(892, 379);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lcGroupCondition
            // 
            this.lcGroupCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.lcItemLogDate});
            this.lcGroupCondition.Location = new System.Drawing.Point(0, 0);
            this.lcGroupCondition.Name = "lcGroupCondition";
            this.lcGroupCondition.Size = new System.Drawing.Size(916, 67);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(300, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(592, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // dateLogdate
            // 
            this.dateLogdate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dateLogdate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLogdate.Appearance.Options.UseBackColor = true;
            this.dateLogdate.Appearance.Options.UseFont = true;
            this.dateLogdate.EditFrValue = new System.DateTime(2017, 7, 7, 17, 49, 32, 904);
            this.dateLogdate.EditToValue = new System.DateTime(2017, 7, 7, 17, 49, 32, 904);
            this.dateLogdate.Location = new System.Drawing.Point(107, 43);
            this.dateLogdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateLogdate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dateLogdate.MinimumSize = new System.Drawing.Size(213, 20);
            this.dateLogdate.Name = "dateLogdate";
            this.dateLogdate.Size = new System.Drawing.Size(213, 20);
            this.dateLogdate.TabIndex = 6;
            // 
            // lcItemLogDate
            // 
            this.lcItemLogDate.Control = this.dateLogdate;
            this.lcItemLogDate.Location = new System.Drawing.Point(0, 0);
            this.lcItemLogDate.Name = "lcItemLogDate";
            this.lcItemLogDate.Size = new System.Drawing.Size(300, 24);
            this.lcItemLogDate.TextSize = new System.Drawing.Size(80, 14);
            // 
            // SysLogList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Name = "SysLogList";
            this.Text = "UserList";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupUserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemLogDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupUserList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Service.Controls.DatePeriodEditEx dateLogdate;
        private DevExpress.XtraLayout.LayoutControlItem lcItemLogDate;
    }
}