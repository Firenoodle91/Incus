namespace HKInc.Ui.View.View.PUR_POPUP
{
    partial class XPFPUR_STOCK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFPUR_STOCK));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dp_dt = new HKInc.Service.Controls.DateEditEx();
            this.tx_itemCode = new DevExpress.XtraEditors.TextEdit();
            this.tx_itemName = new DevExpress.XtraEditors.TextEdit();
            this.tx_lotno = new DevExpress.XtraEditors.TextEdit();
            this.tx_qty = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcSrcAdjustDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSrcInAdjustQty = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_lotno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_qty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcAdjustDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcInAdjustQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dp_dt);
            this.layoutControl1.Controls.Add(this.tx_itemCode);
            this.layoutControl1.Controls.Add(this.tx_itemName);
            this.layoutControl1.Controls.Add(this.tx_lotno);
            this.layoutControl1.Controls.Add(this.tx_qty);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(374, 150);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dp_dt
            // 
            this.dp_dt.EditValue = null;
            this.dp_dt.Location = new System.Drawing.Point(100, 11);
            this.dp_dt.Name = "dp_dt";
            this.dp_dt.Properties.Appearance.Options.UseTextOptions = true;
            this.dp_dt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
            this.dp_dt.Size = new System.Drawing.Size(263, 20);
            this.dp_dt.StyleController = this.layoutControl1;
            this.dp_dt.TabIndex = 0;
            // 
            // tx_itemCode
            // 
            this.tx_itemCode.Location = new System.Drawing.Point(100, 33);
            this.tx_itemCode.Name = "tx_itemCode";
            this.tx_itemCode.Size = new System.Drawing.Size(263, 20);
            this.tx_itemCode.StyleController = this.layoutControl1;
            this.tx_itemCode.TabIndex = 1;
            // 
            // tx_itemName
            // 
            this.tx_itemName.Location = new System.Drawing.Point(100, 55);
            this.tx_itemName.Name = "tx_itemName";
            this.tx_itemName.Size = new System.Drawing.Size(263, 20);
            this.tx_itemName.StyleController = this.layoutControl1;
            this.tx_itemName.TabIndex = 2;
            // 
            // tx_lotno
            // 
            this.tx_lotno.Location = new System.Drawing.Point(100, 77);
            this.tx_lotno.Margin = new System.Windows.Forms.Padding(2);
            this.tx_lotno.Name = "tx_lotno";
            this.tx_lotno.Size = new System.Drawing.Size(263, 20);
            this.tx_lotno.StyleController = this.layoutControl1;
            this.tx_lotno.TabIndex = 5;
            // 
            // tx_qty
            // 
            this.tx_qty.Location = new System.Drawing.Point(100, 99);
            this.tx_qty.Name = "tx_qty";
            this.tx_qty.Properties.Appearance.Options.UseTextOptions = true;
            this.tx_qty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tx_qty.Size = new System.Drawing.Size(263, 20);
            this.tx_qty.StyleController = this.layoutControl1;
            this.tx_qty.TabIndex = 3;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcSrcAdjustDate,
            this.lcItemCode,
            this.lcItemName,
            this.lcLotNo,
            this.lcSrcInAdjustQty,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(374, 150);
            this.Root.TextVisible = false;
            // 
            // lcSrcAdjustDate
            // 
            this.lcSrcAdjustDate.Control = this.dp_dt;
            this.lcSrcAdjustDate.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcSrcAdjustDate.CustomizationFormText = "lcSrcAdjustDate";
            this.lcSrcAdjustDate.Location = new System.Drawing.Point(0, 0);
            this.lcSrcAdjustDate.Name = "lcSrcAdjustDate";
            this.lcSrcAdjustDate.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcSrcAdjustDate.Size = new System.Drawing.Size(354, 22);
            this.lcSrcAdjustDate.TextSize = new System.Drawing.Size(86, 14);
            // 
            // lcItemCode
            // 
            this.lcItemCode.Control = this.tx_itemCode;
            this.lcItemCode.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcItemCode.CustomizationFormText = "lcItemCode";
            this.lcItemCode.Location = new System.Drawing.Point(0, 22);
            this.lcItemCode.Name = "lcItemCode";
            this.lcItemCode.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcItemCode.Size = new System.Drawing.Size(354, 22);
            this.lcItemCode.TextSize = new System.Drawing.Size(86, 14);
            // 
            // lcItemName
            // 
            this.lcItemName.Control = this.tx_itemName;
            this.lcItemName.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcItemName.CustomizationFormText = "lcItemName";
            this.lcItemName.Location = new System.Drawing.Point(0, 44);
            this.lcItemName.Name = "lcItemName";
            this.lcItemName.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcItemName.Size = new System.Drawing.Size(354, 22);
            this.lcItemName.TextSize = new System.Drawing.Size(86, 14);
            // 
            // lcLotNo
            // 
            this.lcLotNo.Control = this.tx_lotno;
            this.lcLotNo.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcLotNo.CustomizationFormText = "lcLotNo";
            this.lcLotNo.Location = new System.Drawing.Point(0, 66);
            this.lcLotNo.Name = "lcLotNo";
            this.lcLotNo.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcLotNo.Size = new System.Drawing.Size(354, 22);
            this.lcLotNo.TextSize = new System.Drawing.Size(86, 14);
            // 
            // lcSrcInAdjustQty
            // 
            this.lcSrcInAdjustQty.Control = this.tx_qty;
            this.lcSrcInAdjustQty.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcSrcInAdjustQty.CustomizationFormText = "lcAdjustQty";
            this.lcSrcInAdjustQty.Location = new System.Drawing.Point(0, 88);
            this.lcSrcInAdjustQty.Name = "lcSrcInAdjustQty";
            this.lcSrcInAdjustQty.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcSrcInAdjustQty.Size = new System.Drawing.Size(354, 22);
            this.lcSrcInAdjustQty.Text = "lcAdjustQty";
            this.lcSrcInAdjustQty.TextSize = new System.Drawing.Size(86, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 110);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(354, 20);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XPFPUR_STOCK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 197);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFPUR_STOCK";
            this.Text = "XPFPUR_STOCK";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_lotno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_qty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcAdjustDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSrcInAdjustQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Service.Controls.DateEditEx dp_dt;
        private DevExpress.XtraLayout.LayoutControlItem lcSrcAdjustDate;
        private DevExpress.XtraEditors.TextEdit tx_itemCode;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCode;
        private DevExpress.XtraEditors.TextEdit tx_itemName;
        private DevExpress.XtraLayout.LayoutControlItem lcItemName;
        private DevExpress.XtraEditors.TextEdit tx_lotno;
        private DevExpress.XtraLayout.LayoutControlItem lcLotNo;
        private DevExpress.XtraEditors.TextEdit tx_qty;
        private DevExpress.XtraLayout.LayoutControlItem lcSrcInAdjustQty;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}