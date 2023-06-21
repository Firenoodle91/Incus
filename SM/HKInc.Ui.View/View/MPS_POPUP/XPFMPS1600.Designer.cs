namespace HKInc.Ui.View.View.MPS_POPUP
{
    partial class XPFMPS1600
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFMPS1600));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dp_dt = new HKInc.Service.Controls.DateEditEx();
            this.tx_qty = new DevExpress.XtraEditors.TextEdit();
            this.tx_itemName = new DevExpress.XtraEditors.TextEdit();
            this.tx_inqty = new DevExpress.XtraEditors.TextEdit();
            this.tx_itemCode = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcSrcAdjustDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSrcUseAdjustQty = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSrcOutAdjustQty = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.tx_lotno = new DevExpress.XtraEditors.TextEdit();
            this.lcLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_qty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_inqty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcAdjustDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcUseAdjustQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcOutAdjustQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_lotno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLotNo)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dp_dt);
            this.layoutControl1.Controls.Add(this.tx_qty);
            this.layoutControl1.Controls.Add(this.tx_itemName);
            this.layoutControl1.Controls.Add(this.tx_inqty);
            this.layoutControl1.Controls.Add(this.tx_itemCode);
            this.layoutControl1.Controls.Add(this.tx_lotno);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 34);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(534, 242);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dp_dt
            // 
            this.dp_dt.EditValue = null;
            this.dp_dt.Location = new System.Drawing.Point(162, 14);
            this.dp_dt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dp_dt.Name = "dp_dt";
            this.dp_dt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dp_dt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dp_dt.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dp_dt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dp_dt.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dp_dt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dp_dt.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dp_dt.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dp_dt.Size = new System.Drawing.Size(358, 28);
            this.dp_dt.StyleController = this.layoutControl1;
            this.dp_dt.TabIndex = 0;
            // 
            // tx_qty
            // 
            this.tx_qty.Location = new System.Drawing.Point(162, 174);
            this.tx_qty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tx_qty.Name = "tx_qty";
            this.tx_qty.Properties.Appearance.Options.UseTextOptions = true;
            this.tx_qty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tx_qty.Size = new System.Drawing.Size(358, 28);
            this.tx_qty.StyleController = this.layoutControl1;
            this.tx_qty.TabIndex = 4;
            // 
            // tx_itemName
            // 
            this.tx_itemName.Location = new System.Drawing.Point(162, 78);
            this.tx_itemName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tx_itemName.Name = "tx_itemName";
            this.tx_itemName.Size = new System.Drawing.Size(358, 28);
            this.tx_itemName.StyleController = this.layoutControl1;
            this.tx_itemName.TabIndex = 2;
            // 
            // tx_inqty
            // 
            this.tx_inqty.Location = new System.Drawing.Point(162, 142);
            this.tx_inqty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tx_inqty.Name = "tx_inqty";
            this.tx_inqty.Properties.Appearance.Options.UseTextOptions = true;
            this.tx_inqty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tx_inqty.Size = new System.Drawing.Size(358, 28);
            this.tx_inqty.StyleController = this.layoutControl1;
            this.tx_inqty.TabIndex = 3;
            // 
            // tx_itemCode
            // 
            this.tx_itemCode.Location = new System.Drawing.Point(162, 46);
            this.tx_itemCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tx_itemCode.Name = "tx_itemCode";
            this.tx_itemCode.Size = new System.Drawing.Size(358, 28);
            this.tx_itemCode.StyleController = this.layoutControl1;
            this.tx_itemCode.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcSrcAdjustDate,
            this.lcSrcUseAdjustQty,
            this.lcItemName,
            this.lcSrcOutAdjustQty,
            this.lcItemCode,
            this.lcLotNo});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(534, 242);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcSrcAdjustDate
            // 
            this.lcSrcAdjustDate.Control = this.dp_dt;
            this.lcSrcAdjustDate.Location = new System.Drawing.Point(0, 0);
            this.lcSrcAdjustDate.Name = "lcSrcAdjustDate";
            this.lcSrcAdjustDate.Size = new System.Drawing.Size(510, 32);
            this.lcSrcAdjustDate.TextSize = new System.Drawing.Size(143, 22);
            // 
            // lcSrcUseAdjustQty
            // 
            this.lcSrcUseAdjustQty.Control = this.tx_qty;
            this.lcSrcUseAdjustQty.Location = new System.Drawing.Point(0, 160);
            this.lcSrcUseAdjustQty.Name = "lcSrcUseAdjustQty";
            this.lcSrcUseAdjustQty.Size = new System.Drawing.Size(510, 58);
            this.lcSrcUseAdjustQty.TextSize = new System.Drawing.Size(143, 22);
            // 
            // lcItemName
            // 
            this.lcItemName.Control = this.tx_itemName;
            this.lcItemName.Location = new System.Drawing.Point(0, 64);
            this.lcItemName.Name = "lcItemName";
            this.lcItemName.Size = new System.Drawing.Size(510, 32);
            this.lcItemName.TextSize = new System.Drawing.Size(143, 22);
            // 
            // lcSrcOutAdjustQty
            // 
            this.lcSrcOutAdjustQty.Control = this.tx_inqty;
            this.lcSrcOutAdjustQty.Location = new System.Drawing.Point(0, 128);
            this.lcSrcOutAdjustQty.Name = "lcSrcOutAdjustQty";
            this.lcSrcOutAdjustQty.Size = new System.Drawing.Size(510, 32);
            this.lcSrcOutAdjustQty.TextSize = new System.Drawing.Size(143, 22);
            // 
            // lcItemCode
            // 
            this.lcItemCode.AllowHide = false;
            this.lcItemCode.Control = this.tx_itemCode;
            this.lcItemCode.Location = new System.Drawing.Point(0, 32);
            this.lcItemCode.Name = "lcItemCode";
            this.lcItemCode.Size = new System.Drawing.Size(510, 32);
            this.lcItemCode.TextSize = new System.Drawing.Size(143, 22);
            // 
            // tx_lotno
            // 
            this.tx_lotno.Location = new System.Drawing.Point(162, 110);
            this.tx_lotno.Name = "tx_lotno";
            this.tx_lotno.Size = new System.Drawing.Size(358, 28);
            this.tx_lotno.StyleController = this.layoutControl1;
            this.tx_lotno.TabIndex = 5;
            // 
            // lcLotNo
            // 
            this.lcLotNo.Control = this.tx_lotno;
            this.lcLotNo.Location = new System.Drawing.Point(0, 96);
            this.lcLotNo.Name = "lcLotNo";
            this.lcLotNo.Size = new System.Drawing.Size(510, 32);
            this.lcLotNo.TextSize = new System.Drawing.Size(143, 22);
            // 
            // XPFMPS1600
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 309);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "XPFMPS1600";
            this.Text = "XPFMPS1600";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_qty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_inqty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcAdjustDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcUseAdjustQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcOutAdjustQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_lotno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLotNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.DateEditEx dp_dt;
        private DevExpress.XtraLayout.LayoutControlItem lcSrcAdjustDate;
        private DevExpress.XtraEditors.TextEdit tx_qty;
        private DevExpress.XtraEditors.TextEdit tx_itemName;
        private DevExpress.XtraLayout.LayoutControlItem lcSrcUseAdjustQty;
        private DevExpress.XtraLayout.LayoutControlItem lcItemName;
        private DevExpress.XtraEditors.TextEdit tx_inqty;
        private DevExpress.XtraLayout.LayoutControlItem lcSrcOutAdjustQty;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCode;
        private DevExpress.XtraEditors.TextEdit tx_itemCode;
        private DevExpress.XtraEditors.TextEdit tx_lotno;
        private DevExpress.XtraLayout.LayoutControlItem lcLotNo;
    }
}