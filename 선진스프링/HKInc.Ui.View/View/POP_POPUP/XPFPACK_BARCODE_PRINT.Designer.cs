namespace HKInc.Ui.View.View.POP_POPUP
{
    partial class XPFPACK_BARCODE_PRINT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFPACK_BARCODE_PRINT));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbo_PrintDivision = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dt_PrintDate = new HKInc.Service.Controls.DateEditEx();
            this.spin_PrintQty = new DevExpress.XtraEditors.SpinEdit();
            this.spin_PerBoxQty = new DevExpress.XtraEditors.SpinEdit();
            this.pic_ProdImage = new DevExpress.XtraEditors.PictureEdit();
            this.pic_PackPlasticImage = new DevExpress.XtraEditors.PictureEdit();
            this.pic_OutBoxImage = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.lcPrintInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPrintQty3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPerBoxQty = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPrintDivision = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcManufactureDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProdImage = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPackPlasticImage = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutBoxImage = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tx_ItemName = new DevExpress.XtraEditors.TextEdit();
            this.lcItemName = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_PrintDivision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_PrintDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_PrintDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_PrintQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_PerBoxQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ProdImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_PackPlasticImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_OutBoxImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintQty3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPerBoxQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManufactureDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProdImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPackPlasticImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemName)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tx_ItemName);
            this.layoutControl1.Controls.Add(this.cbo_PrintDivision);
            this.layoutControl1.Controls.Add(this.dt_PrintDate);
            this.layoutControl1.Controls.Add(this.spin_PrintQty);
            this.layoutControl1.Controls.Add(this.spin_PerBoxQty);
            this.layoutControl1.Controls.Add(this.pic_ProdImage);
            this.layoutControl1.Controls.Add(this.pic_PackPlasticImage);
            this.layoutControl1.Controls.Add(this.pic_OutBoxImage);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(570, 277);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbo_PrintDivision
            // 
            this.cbo_PrintDivision.Location = new System.Drawing.Point(78, 212);
            this.cbo_PrintDivision.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_PrintDivision.Name = "cbo_PrintDivision";
            this.cbo_PrintDivision.Properties.Appearance.Options.UseTextOptions = true;
            this.cbo_PrintDivision.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_PrintDivision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_PrintDivision.Size = new System.Drawing.Size(204, 20);
            this.cbo_PrintDivision.StyleController = this.layoutControl1;
            this.cbo_PrintDivision.TabIndex = 5;
            // 
            // dt_PrintDate
            // 
            this.dt_PrintDate.EditValue = null;
            this.dt_PrintDate.Location = new System.Drawing.Point(342, 212);
            this.dt_PrintDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dt_PrintDate.Name = "dt_PrintDate";
            this.dt_PrintDate.Properties.Appearance.Options.UseTextOptions = true;
            this.dt_PrintDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dt_PrintDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_PrintDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_PrintDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dt_PrintDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_PrintDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dt_PrintDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_PrintDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dt_PrintDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_PrintDate.Size = new System.Drawing.Size(206, 20);
            this.dt_PrintDate.StyleController = this.layoutControl1;
            this.dt_PrintDate.TabIndex = 4;
            // 
            // spin_PrintQty
            // 
            this.spin_PrintQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_PrintQty.Location = new System.Drawing.Point(78, 236);
            this.spin_PrintQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spin_PrintQty.Name = "spin_PrintQty";
            this.spin_PrintQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_PrintQty.Size = new System.Drawing.Size(204, 20);
            this.spin_PrintQty.StyleController = this.layoutControl1;
            this.spin_PrintQty.TabIndex = 0;
            // 
            // spin_PerBoxQty
            // 
            this.spin_PerBoxQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_PerBoxQty.Location = new System.Drawing.Point(342, 236);
            this.spin_PerBoxQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spin_PerBoxQty.Name = "spin_PerBoxQty";
            this.spin_PerBoxQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_PerBoxQty.Size = new System.Drawing.Size(206, 20);
            this.spin_PerBoxQty.StyleController = this.layoutControl1;
            this.spin_PerBoxQty.TabIndex = 1;
            // 
            // pic_ProdImage
            // 
            this.pic_ProdImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pic_ProdImage.Location = new System.Drawing.Point(22, 41);
            this.pic_ProdImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pic_ProdImage.Name = "pic_ProdImage";
            this.pic_ProdImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_ProdImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_ProdImage.Size = new System.Drawing.Size(164, 102);
            this.pic_ProdImage.StyleController = this.layoutControl1;
            this.pic_ProdImage.TabIndex = 0;
            // 
            // pic_PackPlasticImage
            // 
            this.pic_PackPlasticImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pic_PackPlasticImage.Location = new System.Drawing.Point(212, 41);
            this.pic_PackPlasticImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pic_PackPlasticImage.Name = "pic_PackPlasticImage";
            this.pic_PackPlasticImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_PackPlasticImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_PackPlasticImage.Size = new System.Drawing.Size(157, 102);
            this.pic_PackPlasticImage.StyleController = this.layoutControl1;
            this.pic_PackPlasticImage.TabIndex = 1;
            // 
            // pic_OutBoxImage
            // 
            this.pic_OutBoxImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pic_OutBoxImage.Location = new System.Drawing.Point(395, 41);
            this.pic_OutBoxImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pic_OutBoxImage.Name = "pic_OutBoxImage";
            this.pic_OutBoxImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_OutBoxImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_OutBoxImage.Size = new System.Drawing.Size(153, 102);
            this.pic_OutBoxImage.StyleController = this.layoutControl1;
            this.pic_OutBoxImage.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleSeparator1,
            this.lcPrintInfo,
            this.lcProdImage,
            this.lcPackPlasticImage,
            this.lcOutBoxImage});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(570, 277);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 260);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(552, 1);
            // 
            // lcPrintInfo
            // 
            this.lcPrintInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPrintQty3,
            this.lcPerBoxQty,
            this.lcPrintDivision,
            this.lcManufactureDate,
            this.lcItemName});
            this.lcPrintInfo.Location = new System.Drawing.Point(0, 147);
            this.lcPrintInfo.Name = "lcPrintInfo";
            this.lcPrintInfo.OptionsItemText.TextToControlDistance = 4;
            this.lcPrintInfo.Size = new System.Drawing.Size(552, 113);
            this.lcPrintInfo.Text = "인쇄정보";
            // 
            // lcPrintQty3
            // 
            this.lcPrintQty3.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcPrintQty3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcPrintQty3.Control = this.spin_PrintQty;
            this.lcPrintQty3.Location = new System.Drawing.Point(0, 48);
            this.lcPrintQty3.Name = "lcPrintQty3";
            this.lcPrintQty3.Size = new System.Drawing.Size(264, 24);
            this.lcPrintQty3.Text = "인쇄수량";
            this.lcPrintQty3.TextSize = new System.Drawing.Size(53, 14);
            // 
            // lcPerBoxQty
            // 
            this.lcPerBoxQty.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcPerBoxQty.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcPerBoxQty.Control = this.spin_PerBoxQty;
            this.lcPerBoxQty.CustomizationFormText = "lcResultQty";
            this.lcPerBoxQty.Location = new System.Drawing.Point(264, 48);
            this.lcPerBoxQty.Name = "lcPerBoxQty";
            this.lcPerBoxQty.Size = new System.Drawing.Size(266, 24);
            this.lcPerBoxQty.Text = "BOX당수량";
            this.lcPerBoxQty.TextSize = new System.Drawing.Size(53, 14);
            // 
            // lcPrintDivision
            // 
            this.lcPrintDivision.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcPrintDivision.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcPrintDivision.Control = this.cbo_PrintDivision;
            this.lcPrintDivision.Location = new System.Drawing.Point(0, 24);
            this.lcPrintDivision.Name = "lcPrintDivision";
            this.lcPrintDivision.Size = new System.Drawing.Size(264, 24);
            this.lcPrintDivision.Text = "출력구분";
            this.lcPrintDivision.TextSize = new System.Drawing.Size(53, 14);
            // 
            // lcManufactureDate
            // 
            this.lcManufactureDate.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcManufactureDate.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcManufactureDate.Control = this.dt_PrintDate;
            this.lcManufactureDate.Location = new System.Drawing.Point(264, 24);
            this.lcManufactureDate.Name = "lcManufactureDate";
            this.lcManufactureDate.Size = new System.Drawing.Size(266, 24);
            this.lcManufactureDate.Text = "제조일";
            this.lcManufactureDate.TextSize = new System.Drawing.Size(53, 14);
            // 
            // lcProdImage
            // 
            this.lcProdImage.CustomizationFormText = "lcProdImage";
            this.lcProdImage.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcProdImage.Location = new System.Drawing.Point(0, 0);
            this.lcProdImage.Name = "lcProdImage";
            this.lcProdImage.OptionsItemText.TextToControlDistance = 4;
            this.lcProdImage.Size = new System.Drawing.Size(190, 147);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pic_ProdImage;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(168, 106);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcPackPlasticImage
            // 
            this.lcPackPlasticImage.CustomizationFormText = "lcPackPlasticImage";
            this.lcPackPlasticImage.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcPackPlasticImage.Location = new System.Drawing.Point(190, 0);
            this.lcPackPlasticImage.Name = "lcPackPlasticImage";
            this.lcPackPlasticImage.OptionsItemText.TextToControlDistance = 4;
            this.lcPackPlasticImage.Size = new System.Drawing.Size(183, 147);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.pic_PackPlasticImage;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(161, 106);
            this.layoutControlItem2.Text = "layoutControlItem1";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lcOutBoxImage
            // 
            this.lcOutBoxImage.CustomizationFormText = "lcOutBoxImage";
            this.lcOutBoxImage.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcOutBoxImage.Location = new System.Drawing.Point(373, 0);
            this.lcOutBoxImage.Name = "lcOutBoxImage";
            this.lcOutBoxImage.OptionsItemText.TextToControlDistance = 4;
            this.lcOutBoxImage.Size = new System.Drawing.Size(179, 147);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.pic_OutBoxImage;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(157, 106);
            this.layoutControlItem3.Text = "layoutControlItem1";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // tx_ItemName
            // 
            this.tx_ItemName.Location = new System.Drawing.Point(78, 188);
            this.tx_ItemName.Name = "tx_ItemName";
            this.tx_ItemName.Size = new System.Drawing.Size(470, 20);
            this.tx_ItemName.StyleController = this.layoutControl1;
            this.tx_ItemName.TabIndex = 6;
            // 
            // lcItemName
            // 
            this.lcItemName.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcItemName.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcItemName.Control = this.tx_ItemName;
            this.lcItemName.Location = new System.Drawing.Point(0, 0);
            this.lcItemName.Name = "lcItemName";
            this.lcItemName.Size = new System.Drawing.Size(530, 24);
            this.lcItemName.Text = "품명";
            this.lcItemName.TextSize = new System.Drawing.Size(53, 14);
            // 
            // XPFPACK_BARCODE_PRINT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 324);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XPFPACK_BARCODE_PRINT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "작업시작";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbo_PrintDivision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_PrintDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_PrintDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_PrintQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_PerBoxQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ProdImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_PackPlasticImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_OutBoxImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintQty3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPerBoxQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManufactureDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProdImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPackPlasticImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraEditors.SpinEdit spin_PrintQty;
        private DevExpress.XtraLayout.LayoutControlItem lcPrintQty3;
        private DevExpress.XtraEditors.SpinEdit spin_PerBoxQty;
        private DevExpress.XtraLayout.LayoutControlItem lcPerBoxQty;
        private DevExpress.XtraLayout.LayoutControlGroup lcPrintInfo;
        private Service.Controls.DateEditEx dt_PrintDate;
        private DevExpress.XtraLayout.LayoutControlItem lcManufactureDate;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_PrintDivision;
        private DevExpress.XtraLayout.LayoutControlItem lcPrintDivision;
        private DevExpress.XtraEditors.PictureEdit pic_ProdImage;
        private DevExpress.XtraEditors.PictureEdit pic_PackPlasticImage;
        private DevExpress.XtraEditors.PictureEdit pic_OutBoxImage;
        private DevExpress.XtraLayout.LayoutControlGroup lcProdImage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcPackPlasticImage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcOutBoxImage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit tx_ItemName;
        private DevExpress.XtraLayout.LayoutControlItem lcItemName;
    }
}