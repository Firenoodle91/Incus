namespace HKInc.Ui.View.SYS
{
    partial class UserGroupList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserGroupList));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rbo_UseFlag = new HKInc.Service.Controls.HK_UseFlagRadioGroup();
            this.treeList = new HKInc.Service.Controls.TreeListEx();
            this.textUserGroupName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcAuthGroupList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcAuthGroupName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.TreeListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textUserGroupName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAuthGroupList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAuthGroupName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.rbo_UseFlag);
            this.layoutControl1.Controls.Add(this.treeList);
            this.layoutControl1.Controls.Add(this.textUserGroupName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // rbo_UseFlag
            // 
            this.rbo_UseFlag.Location = new System.Drawing.Point(848, 56);
            this.rbo_UseFlag.MaximumSize = new System.Drawing.Size(190, 30);
            this.rbo_UseFlag.MinimumSize = new System.Drawing.Size(190, 30);
            this.rbo_UseFlag.Name = "rbo_UseFlag";
            this.rbo_UseFlag.SelectedValue = "Y";
            this.rbo_UseFlag.Size = new System.Drawing.Size(190, 30);
            this.rbo_UseFlag.TabIndex = 1;
            // 
            // treeList
            // 
            this.treeList.DataSource = null;
            this.treeList.KeyFieldName = "ID";
            this.treeList.Location = new System.Drawing.Point(31, 147);
            this.treeList.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.treeList.Name = "treeList";
            this.treeList.ParentFieldName = "ParentID";
            this.treeList.Size = new System.Drawing.Size(1008, 475);
            this.treeList.TabIndex = 2;
            // 
            // textUserGroupName
            // 
            this.textUserGroupName.Location = new System.Drawing.Point(151, 56);
            this.textUserGroupName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textUserGroupName.Name = "textUserGroupName";
            this.textUserGroupName.Size = new System.Drawing.Size(206, 24);
            this.textUserGroupName.StyleController = this.layoutControl1;
            this.textUserGroupName.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcAuthGroupList,
            this.lcCondition});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcAuthGroupList
            // 
            this.lcAuthGroupList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcAuthGroupList.Location = new System.Drawing.Point(0, 91);
            this.lcAuthGroupList.Name = "lcAuthGroupList";
            this.lcAuthGroupList.Size = new System.Drawing.Size(1044, 536);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1014, 481);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcAuthGroupName,
            this.emptySpaceItem1,
            this.layoutControlItem2});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.Size = new System.Drawing.Size(1044, 91);
            // 
            // lcAuthGroupName
            // 
            this.lcAuthGroupName.Control = this.textUserGroupName;
            this.lcAuthGroupName.Location = new System.Drawing.Point(0, 0);
            this.lcAuthGroupName.Name = "lcAuthGroupName";
            this.lcAuthGroupName.Size = new System.Drawing.Size(332, 36);
            this.lcAuthGroupName.TextSize = new System.Drawing.Size(117, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(332, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(485, 36);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.rbo_UseFlag;
            this.layoutControlItem2.Location = new System.Drawing.Point(817, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(197, 36);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // UserGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "UserGroupList";
            this.Text = "UserList";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.TreeListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textUserGroupName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAuthGroupList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAuthGroupName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit textUserGroupName;
        private DevExpress.XtraLayout.LayoutControlGroup lcAuthGroupList;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcAuthGroupName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Service.Controls.TreeListEx treeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.HK_UseFlagRadioGroup rbo_UseFlag;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}