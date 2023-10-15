namespace HKInc.Ui.View.View.PUR_POPUP
{
    partial class XPFPUR1202
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
            this.tx_CustomerLotNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcScanInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCustomerLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustomerLotNoScanList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_CustomerLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcScanInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerLotNoScanList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_CustomerLotNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(450, 627);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tx_CustomerLotNo
            // 
            this.tx_CustomerLotNo.Location = new System.Drawing.Point(145, 56);
            this.tx_CustomerLotNo.Name = "tx_CustomerLotNo";
            this.tx_CustomerLotNo.Size = new System.Drawing.Size(274, 24);
            this.tx_CustomerLotNo.StyleController = this.layoutControl1;
            this.tx_CustomerLotNo.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcScanInfo,
            this.lcCustomerLotNoScanList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(450, 627);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcScanInfo
            // 
            this.lcScanInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCustomerLotNo});
            this.lcScanInfo.Location = new System.Drawing.Point(0, 0);
            this.lcScanInfo.Name = "lcScanInfo";
            this.lcScanInfo.OptionsItemText.TextToControlDistance = 4;
            this.lcScanInfo.Size = new System.Drawing.Size(424, 85);
            // 
            // lcCustomerLotNo
            // 
            this.lcCustomerLotNo.Control = this.tx_CustomerLotNo;
            this.lcCustomerLotNo.Location = new System.Drawing.Point(0, 0);
            this.lcCustomerLotNo.Name = "lcCustomerLotNo";
            this.lcCustomerLotNo.Size = new System.Drawing.Size(394, 30);
            this.lcCustomerLotNo.TextSize = new System.Drawing.Size(110, 18);
            // 
            // lcCustomerLotNoScanList
            // 
            this.lcCustomerLotNoScanList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcCustomerLotNoScanList.Location = new System.Drawing.Point(0, 85);
            this.lcCustomerLotNoScanList.Name = "lcCustomerLotNoScanList";
            this.lcCustomerLotNoScanList.OptionsItemText.TextToControlDistance = 4;
            this.lcCustomerLotNoScanList.Size = new System.Drawing.Size(424, 516);
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 141);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(388, 455);
            this.gridEx1.TabIndex = 5;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(394, 461);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // XPFPUR1202
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 627);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFPUR1202";
            this.Text = "XPFPUR1202";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_CustomerLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcScanInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerLotNoScanList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraEditors.TextEdit tx_CustomerLotNo;
        private DevExpress.XtraLayout.LayoutControlGroup lcScanInfo;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerLotNo;
        private DevExpress.XtraLayout.LayoutControlGroup lcCustomerLotNoScanList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}