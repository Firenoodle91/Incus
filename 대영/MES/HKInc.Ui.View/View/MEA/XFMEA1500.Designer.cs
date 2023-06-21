namespace HKInc.Ui.View.View.MEA
{
    partial class XFMEA1500
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFMEA1500));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rbo_UseFlag = new HKInc.Service.Controls.HK_UseFlagRadioGroup();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.tx_InstrCodeName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcRBIDCodeName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcRBIDList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_InstrCodeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRBIDCodeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRBIDList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.rbo_UseFlag);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_InstrCodeName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1107, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // rbo_UseFlag
            // 
            this.rbo_UseFlag.Location = new System.Drawing.Point(876, 50);
            this.rbo_UseFlag.MaximumSize = new System.Drawing.Size(190, 30);
            this.rbo_UseFlag.MinimumSize = new System.Drawing.Size(190, 30);
            this.rbo_UseFlag.Name = "rbo_UseFlag";
            this.rbo_UseFlag.SelectedValue = "Y";
            this.rbo_UseFlag.Size = new System.Drawing.Size(190, 30);
            this.rbo_UseFlag.TabIndex = 1;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 134);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1059, 509);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // tx_InstrCodeName
            // 
            this.tx_InstrCodeName.Location = new System.Drawing.Point(125, 50);
            this.tx_InstrCodeName.Name = "tx_InstrCodeName";
            this.tx_InstrCodeName.Size = new System.Drawing.Size(171, 24);
            this.tx_InstrCodeName.StyleController = this.layoutControl1;
            this.tx_InstrCodeName.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcRBIDList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1107, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcRBIDCodeName,
            this.layoutControlItem1});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1087, 84);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(276, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(576, 34);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcRBIDCodeName
            // 
            this.lcRBIDCodeName.Control = this.tx_InstrCodeName;
            this.lcRBIDCodeName.CustomizationFormText = "설비코드/명";
            this.lcRBIDCodeName.Location = new System.Drawing.Point(0, 0);
            this.lcRBIDCodeName.Name = "lcRBIDCodeName";
            this.lcRBIDCodeName.Size = new System.Drawing.Size(276, 34);
            this.lcRBIDCodeName.Text = "래그비드코드/명";
            this.lcRBIDCodeName.TextSize = new System.Drawing.Size(97, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.rbo_UseFlag;
            this.layoutControlItem1.Location = new System.Drawing.Point(852, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(211, 34);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcRBIDList
            // 
            this.lcRBIDList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcRBIDList.Location = new System.Drawing.Point(0, 84);
            this.lcRBIDList.Name = "lcRBIDList";
            this.lcRBIDList.OptionsItemText.TextToControlDistance = 4;
            this.lcRBIDList.Size = new System.Drawing.Size(1087, 563);
            this.lcRBIDList.Text = "계측기목록";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1063, 513);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // XFMEA1500
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFMEA1500";
            this.Text = "XFMEA1500";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_InstrCodeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRBIDCodeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRBIDList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit tx_InstrCodeName;
        private DevExpress.XtraLayout.LayoutControlItem lcRBIDCodeName;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcRBIDList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.HK_UseFlagRadioGroup rbo_UseFlag;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}