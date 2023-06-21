namespace HKInc.Ui.View.MPS_Popup
{
    partial class XPFMPS1700
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFMPS1700));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lupitemcode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dp_dt = new HKInc.Service.Controls.DateEditEx();
            this.tx_qty = new DevExpress.XtraEditors.TextEdit();
            this.tx_itemcode = new DevExpress.XtraEditors.TextEdit();
            this.tx_inqty = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textLotNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutItemLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupitemcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_qty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_inqty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemLotNo)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textLotNo);
            this.layoutControl1.Controls.Add(this.lupitemcode);
            this.layoutControl1.Controls.Add(this.dp_dt);
            this.layoutControl1.Controls.Add(this.tx_qty);
            this.layoutControl1.Controls.Add(this.tx_itemcode);
            this.layoutControl1.Controls.Add(this.tx_inqty);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(427, 215);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lupitemcode
            // 
            this.lupitemcode.Constraint = null;
            this.lupitemcode.DataSource = null;
            this.lupitemcode.DisplayMember = "";
            this.lupitemcode.isImeModeDisable = false;
            this.lupitemcode.isRequired = false;
            this.lupitemcode.Location = new System.Drawing.Point(105, 46);
            this.lupitemcode.Name = "lupitemcode";
            this.lupitemcode.NullText = "";
            this.lupitemcode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lupitemcode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lupitemcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupitemcode.Properties.NullText = "";
            this.lupitemcode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lupitemcode.Size = new System.Drawing.Size(306, 24);
            this.lupitemcode.StyleController = this.layoutControl1;
            this.lupitemcode.TabIndex = 1;
            this.lupitemcode.Value_1 = null;
            this.lupitemcode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dp_dt
            // 
            this.dp_dt.EditValue = null;
            this.dp_dt.Location = new System.Drawing.Point(105, 16);
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
            this.dp_dt.Size = new System.Drawing.Size(306, 24);
            this.dp_dt.StyleController = this.layoutControl1;
            this.dp_dt.TabIndex = 0;
            // 
            // tx_qty
            // 
            this.tx_qty.Location = new System.Drawing.Point(105, 166);
            this.tx_qty.Name = "tx_qty";
            this.tx_qty.Properties.Appearance.Options.UseTextOptions = true;
            this.tx_qty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tx_qty.Size = new System.Drawing.Size(306, 24);
            this.tx_qty.StyleController = this.layoutControl1;
            this.tx_qty.TabIndex = 4;
            // 
            // tx_itemcode
            // 
            this.tx_itemcode.Location = new System.Drawing.Point(105, 76);
            this.tx_itemcode.Name = "tx_itemcode";
            this.tx_itemcode.Size = new System.Drawing.Size(306, 24);
            this.tx_itemcode.StyleController = this.layoutControl1;
            this.tx_itemcode.TabIndex = 2;
            // 
            // tx_inqty
            // 
            this.tx_inqty.Location = new System.Drawing.Point(105, 136);
            this.tx_inqty.Name = "tx_inqty";
            this.tx_inqty.Properties.Appearance.Options.UseTextOptions = true;
            this.tx_inqty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tx_inqty.Size = new System.Drawing.Size(306, 24);
            this.tx_inqty.StyleController = this.layoutControl1;
            this.tx_inqty.TabIndex = 3;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutItemLotNo});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(427, 215);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dp_dt;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem2.Text = "조정일";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(85, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tx_qty;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 150);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(401, 39);
            this.layoutControlItem1.Text = "사용조정수량";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(85, 18);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tx_itemcode;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem4.Text = "품명";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(85, 18);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.tx_inqty;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem5.Text = "출고조정수량";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(85, 18);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lupitemcode;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem6.Text = "품번";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(85, 18);
            // 
            // textLotNo
            // 
            this.textLotNo.Location = new System.Drawing.Point(105, 106);
            this.textLotNo.Name = "textLotNo";
            this.textLotNo.Size = new System.Drawing.Size(306, 24);
            this.textLotNo.StyleController = this.layoutControl1;
            this.textLotNo.TabIndex = 5;
            // 
            // layoutItemLotNo
            // 
            this.layoutItemLotNo.Control = this.textLotNo;
            this.layoutItemLotNo.Location = new System.Drawing.Point(0, 90);
            this.layoutItemLotNo.Name = "layoutItemLotNo";
            this.layoutItemLotNo.Size = new System.Drawing.Size(401, 30);
            this.layoutItemLotNo.Text = "출고 LOT NO";
            this.layoutItemLotNo.TextSize = new System.Drawing.Size(85, 18);
            // 
            // XPFMPS1700
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 288);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFMPS1700";
            this.Text = "XPFMPS1700";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lupitemcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_qty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_itemcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_inqty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemLotNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.DateEditEx dp_dt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit tx_qty;
        private DevExpress.XtraEditors.TextEdit tx_itemcode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit tx_inqty;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Service.Controls.SearchLookUpEditEx lupitemcode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit textLotNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemLotNo;
    }
}