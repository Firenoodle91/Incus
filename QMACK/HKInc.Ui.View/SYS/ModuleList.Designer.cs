namespace HKInc.Ui.View.SYS
{
    partial class ModuleList
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
            this.textModuleName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcGroupCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcItemModuleName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcGroupModuleList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupModuleList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.textModuleName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(924, 452);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 110);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(876, 318);
            this.gridEx1.TabIndex = 5;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // textModuleName
            // 
            this.textModuleName.Location = new System.Drawing.Point(131, 43);
            this.textModuleName.Name = "textModuleName";
            this.textModuleName.Size = new System.Drawing.Size(133, 20);
            this.textModuleName.StyleController = this.layoutControl1;
            this.textModuleName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcGroupCondition,
            this.lcGroupModuleList});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(924, 452);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcGroupCondition
            // 
            this.lcGroupCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemModuleName,
            this.emptySpaceItem1});
            this.lcGroupCondition.Location = new System.Drawing.Point(0, 0);
            this.lcGroupCondition.Name = "lcGroupCondition";
            this.lcGroupCondition.Size = new System.Drawing.Size(904, 67);
            // 
            // lcItemModuleName
            // 
            this.lcItemModuleName.Control = this.textModuleName;
            this.lcItemModuleName.Location = new System.Drawing.Point(0, 0);
            this.lcItemModuleName.Name = "lcItemModuleName";
            this.lcItemModuleName.Size = new System.Drawing.Size(244, 24);
            this.lcItemModuleName.TextSize = new System.Drawing.Size(104, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(244, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(636, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcGroupModuleList
            // 
            this.lcGroupModuleList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcGroupModuleList.Location = new System.Drawing.Point(0, 67);
            this.lcGroupModuleList.Name = "lcGroupModuleList";
            this.lcGroupModuleList.Size = new System.Drawing.Size(904, 365);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(880, 322);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ModuleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 508);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ModuleList";
            this.Text = "ModuleList";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textModuleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupModuleList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraEditors.TextEdit textModuleName;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcItemModuleName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupModuleList;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}