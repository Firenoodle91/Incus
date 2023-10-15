namespace HKInc.Ui.View.View.PUR_POPUP
{
    partial class XPFPUR1302
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFPUR1302));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tx_InLotNo = new DevExpress.XtraEditors.TextEdit();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.tx_OutLotNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcReturnInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcInLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_InLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_OutLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReturnInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tx_InLotNo);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_OutLotNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1229, 580);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tx_InLotNo
            // 
            this.tx_InLotNo.Location = new System.Drawing.Point(106, 56);
            this.tx_InLotNo.Name = "tx_InLotNo";
            this.tx_InLotNo.Properties.ReadOnly = true;
            this.tx_InLotNo.Size = new System.Drawing.Size(204, 24);
            this.tx_InLotNo.StyleController = this.layoutControl1;
            this.tx_InLotNo.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 141);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1167, 408);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // tx_OutLotNo
            // 
            this.tx_OutLotNo.Location = new System.Drawing.Point(391, 56);
            this.tx_OutLotNo.Name = "tx_OutLotNo";
            this.tx_OutLotNo.Size = new System.Drawing.Size(204, 24);
            this.tx_OutLotNo.StyleController = this.layoutControl1;
            this.tx_OutLotNo.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcReturnInfo,
            this.lcCondition});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1229, 580);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcReturnInfo
            // 
            this.lcReturnInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcReturnInfo.Location = new System.Drawing.Point(0, 85);
            this.lcReturnInfo.Name = "lcReturnInfo";
            this.lcReturnInfo.OptionsItemText.TextToControlDistance = 4;
            this.lcReturnInfo.Size = new System.Drawing.Size(1203, 469);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridEx1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1173, 414);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcInLotNo,
            this.lcOutLotNo,
            this.emptySpaceItem1});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1203, 85);
            // 
            // lcInLotNo
            // 
            this.lcInLotNo.Control = this.tx_InLotNo;
            this.lcInLotNo.Location = new System.Drawing.Point(0, 0);
            this.lcInLotNo.Name = "lcInLotNo";
            this.lcInLotNo.Size = new System.Drawing.Size(285, 30);
            this.lcInLotNo.TextSize = new System.Drawing.Size(71, 18);
            // 
            // lcOutLotNo
            // 
            this.lcOutLotNo.Control = this.tx_OutLotNo;
            this.lcOutLotNo.CustomizationFormText = "layoutControlItem2";
            this.lcOutLotNo.Location = new System.Drawing.Point(285, 0);
            this.lcOutLotNo.Name = "lcOutLotNo";
            this.lcOutLotNo.Size = new System.Drawing.Size(285, 30);
            this.lcOutLotNo.TextSize = new System.Drawing.Size(71, 18);
            this.lcOutLotNo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(570, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(603, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XPFPUR1302
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 653);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFPUR1302";
            this.Text = "XFPUR1200";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_InLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_OutLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReturnInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcReturnInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit tx_InLotNo;
        private DevExpress.XtraLayout.LayoutControlItem lcInLotNo;
        private DevExpress.XtraEditors.TextEdit tx_OutLotNo;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcOutLotNo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}