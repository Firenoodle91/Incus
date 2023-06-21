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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelList));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rbo_UseFlag = new HKInc.Service.Controls.HK_UseFlagRadioGroup();
            this.textLabelText = new DevExpress.XtraEditors.TextEdit();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.textFieldName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcLabelList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcFieldName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcLabelText = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFieldName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLabelList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFieldName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLabelText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.rbo_UseFlag);
            this.layoutControl1.Controls.Add(this.textLabelText);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.textFieldName);
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
            this.rbo_UseFlag.TabIndex = 2;
            // 
            // textLabelText
            // 
            this.textLabelText.Location = new System.Drawing.Point(294, 56);
            this.textLabelText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textLabelText.Name = "textLabelText";
            this.textLabelText.Size = new System.Drawing.Size(162, 24);
            this.textLabelText.StyleController = this.layoutControl1;
            this.textLabelText.TabIndex = 1;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 147);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 475);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // textFieldName
            // 
            this.textFieldName.Location = new System.Drawing.Point(110, 56);
            this.textFieldName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textFieldName.Name = "textFieldName";
            this.textFieldName.Size = new System.Drawing.Size(99, 24);
            this.textFieldName.StyleController = this.layoutControl1;
            this.textFieldName.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcLabelList,
            this.lcCondition});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcLabelList
            // 
            this.lcLabelList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcLabelList.Location = new System.Drawing.Point(0, 91);
            this.lcLabelList.Name = "lcLabelList";
            this.lcLabelList.Size = new System.Drawing.Size(1044, 536);
            this.lcLabelList.Text = "lcGroupLabelList";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1014, 481);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcFieldName,
            this.emptySpaceItem1,
            this.lcLabelText,
            this.layoutControlItem1});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.Size = new System.Drawing.Size(1044, 91);
            // 
            // lcFieldName
            // 
            this.lcFieldName.Control = this.textFieldName;
            this.lcFieldName.Location = new System.Drawing.Point(0, 0);
            this.lcFieldName.Name = "lcFieldName";
            this.lcFieldName.Size = new System.Drawing.Size(184, 36);
            this.lcFieldName.TextSize = new System.Drawing.Size(76, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(431, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(386, 36);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcLabelText
            // 
            this.lcLabelText.Control = this.textLabelText;
            this.lcLabelText.Location = new System.Drawing.Point(184, 0);
            this.lcLabelText.Name = "lcLabelText";
            this.lcLabelText.Size = new System.Drawing.Size(247, 36);
            this.lcLabelText.TextSize = new System.Drawing.Size(76, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.rbo_UseFlag;
            this.layoutControlItem1.Location = new System.Drawing.Point(817, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(197, 36);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // LabelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "LabelList";
            this.Text = "LabelList";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textLabelText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFieldName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLabelList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFieldName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLabelText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraEditors.TextEdit textFieldName;
        private DevExpress.XtraLayout.LayoutControlGroup lcLabelList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcFieldName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit textLabelText;
        private DevExpress.XtraLayout.LayoutControlItem lcLabelText;
        private Service.Controls.HK_UseFlagRadioGroup rbo_UseFlag;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}