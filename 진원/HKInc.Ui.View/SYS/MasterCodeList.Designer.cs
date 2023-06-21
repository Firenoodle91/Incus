namespace HKInc.Ui.View.SYS
{
    partial class MasterCodeList
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
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.radioGroup = new DevExpress.XtraEditors.RadioGroup();
            this.textCodeName = new DevExpress.XtraEditors.TextEdit();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.textCodeId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcGroupMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGroupDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.lcGroupCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcItemMasterCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItemCodeMeaning = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCodeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCodeId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMasterCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCodeMeaning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.radioGroup);
            this.layoutControl1.Controls.Add(this.textCodeName);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.textCodeId);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(761, 316, 450, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 509);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(36, 334);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(864, 139);
            this.gridEx2.TabIndex = 8;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // radioGroup
            // 
            this.radioGroup.Location = new System.Drawing.Point(826, 43);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Size = new System.Drawing.Size(86, 20);
            this.radioGroup.StyleController = this.layoutControl1;
            this.radioGroup.TabIndex = 7;
            // 
            // textCodeName
            // 
            this.textCodeName.Location = new System.Drawing.Point(298, 43);
            this.textCodeName.Name = "textCodeName";
            this.textCodeName.Size = new System.Drawing.Size(102, 20);
            this.textCodeName.StyleController = this.layoutControl1;
            this.textCodeName.TabIndex = 6;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 110);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(888, 184);
            this.gridEx1.TabIndex = 5;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // textCodeId
            // 
            this.textCodeId.Location = new System.Drawing.Point(134, 43);
            this.textCodeId.Name = "textCodeId";
            this.textCodeId.Size = new System.Drawing.Size(50, 20);
            this.textCodeId.StyleController = this.layoutControl1;
            this.textCodeId.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcGroupMasterList,
            this.lcGroupCondition});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 509);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcGroupMasterList
            // 
            this.lcGroupMasterList.ExpandButtonVisible = true;
            this.lcGroupMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.lcGroupDetailList,
            this.splitterItem1});
            this.lcGroupMasterList.Location = new System.Drawing.Point(0, 67);
            this.lcGroupMasterList.Name = "lcGroupMasterList";
            this.lcGroupMasterList.Size = new System.Drawing.Size(916, 422);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(892, 188);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lcGroupDetailList
            // 
            this.lcGroupDetailList.ExpandButtonVisible = true;
            this.lcGroupDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcGroupDetailList.Location = new System.Drawing.Point(0, 193);
            this.lcGroupDetailList.Name = "lcGroupDetailList";
            this.lcGroupDetailList.Size = new System.Drawing.Size(892, 186);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(868, 143);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 188);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(892, 5);
            // 
            // lcGroupCondition
            // 
            this.lcGroupCondition.ExpandButtonVisible = true;
            this.lcGroupCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemMasterCode,
            this.emptySpaceItem1,
            this.lcItemCodeMeaning,
            this.layoutControlItem1});
            this.lcGroupCondition.Location = new System.Drawing.Point(0, 0);
            this.lcGroupCondition.Name = "lcGroupCondition";
            this.lcGroupCondition.Size = new System.Drawing.Size(916, 67);
            // 
            // lcItemMasterCode
            // 
            this.lcItemMasterCode.Control = this.textCodeId;
            this.lcItemMasterCode.Location = new System.Drawing.Point(0, 0);
            this.lcItemMasterCode.Name = "lcItemMasterCode";
            this.lcItemMasterCode.Size = new System.Drawing.Size(164, 24);
            this.lcItemMasterCode.TextSize = new System.Drawing.Size(107, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(380, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(422, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItemCodeMeaning
            // 
            this.lcItemCodeMeaning.Control = this.textCodeName;
            this.lcItemCodeMeaning.Location = new System.Drawing.Point(164, 0);
            this.lcItemCodeMeaning.Name = "lcItemCodeMeaning";
            this.lcItemCodeMeaning.Size = new System.Drawing.Size(216, 24);
            this.lcItemCodeMeaning.TextSize = new System.Drawing.Size(107, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup;
            this.layoutControlItem1.Location = new System.Drawing.Point(802, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(90, 24);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // MasterCodeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Name = "MasterCodeList";
            this.Text = "UserList";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCodeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCodeId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMasterCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCodeMeaning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraEditors.TextEdit textCodeId;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcItemMasterCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit textCodeName;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCodeMeaning;
        private DevExpress.XtraEditors.RadioGroup radioGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.GridEx gridEx2;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
    }
}