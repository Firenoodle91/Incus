namespace HKInc.Ui.View.SYS
{
    partial class LabelList
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
            this.radioGroup = new DevExpress.XtraEditors.RadioGroup();
            this.textLabelText = new DevExpress.XtraEditors.TextEdit();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.textFieldName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcGroupUserList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGroupCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcItemFieldName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItemLabelText = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFieldName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupUserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemFieldName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemLabelText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.radioGroup);
            this.layoutControl1.Controls.Add(this.textLabelText);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.textFieldName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 509);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // radioGroup
            // 
            this.radioGroup.Location = new System.Drawing.Point(826, 43);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Size = new System.Drawing.Size(86, 20);
            this.radioGroup.StyleController = this.layoutControl1;
            this.radioGroup.TabIndex = 7;
            // 
            // textLabelText
            // 
            this.textLabelText.Location = new System.Drawing.Point(278, 43);
            this.textLabelText.Name = "textLabelText";
            this.textLabelText.Size = new System.Drawing.Size(121, 20);
            this.textLabelText.StyleController = this.layoutControl1;
            this.textLabelText.TabIndex = 6;
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
            // textFieldName
            // 
            this.textFieldName.Location = new System.Drawing.Point(116, 43);
            this.textFieldName.Name = "textFieldName";
            this.textFieldName.Size = new System.Drawing.Size(66, 20);
            this.textFieldName.StyleController = this.layoutControl1;
            this.textFieldName.TabIndex = 4;
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
            this.lcGroupUserList.Text = "lcGroupLabelList";
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
            this.lcItemFieldName,
            this.emptySpaceItem1,
            this.lcItemLabelText,
            this.layoutControlItem1});
            this.lcGroupCondition.Location = new System.Drawing.Point(0, 0);
            this.lcGroupCondition.Name = "lcGroupCondition";
            this.lcGroupCondition.Size = new System.Drawing.Size(916, 67);
            // 
            // lcItemFieldName
            // 
            this.lcItemFieldName.Control = this.textFieldName;
            this.lcItemFieldName.Location = new System.Drawing.Point(0, 0);
            this.lcItemFieldName.Name = "lcItemFieldName";
            this.lcItemFieldName.Size = new System.Drawing.Size(162, 24);
            this.lcItemFieldName.TextSize = new System.Drawing.Size(89, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(379, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(423, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItemLabelText
            // 
            this.lcItemLabelText.Control = this.textLabelText;
            this.lcItemLabelText.Location = new System.Drawing.Point(162, 0);
            this.lcItemLabelText.Name = "lcItemLabelText";
            this.lcItemLabelText.Size = new System.Drawing.Size(217, 24);
            this.lcItemLabelText.TextSize = new System.Drawing.Size(89, 14);
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
            // LabelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Name = "LabelList";
            this.Text = "LabelList";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFieldName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupUserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemFieldName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemLabelText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraEditors.TextEdit textFieldName;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupUserList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcItemFieldName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit textLabelText;
        private DevExpress.XtraLayout.LayoutControlItem lcItemLabelText;
        private DevExpress.XtraEditors.RadioGroup radioGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}