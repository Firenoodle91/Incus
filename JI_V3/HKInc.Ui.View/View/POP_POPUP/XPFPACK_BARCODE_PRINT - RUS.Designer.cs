namespace HKInc.Ui.View.View.POP_POPUP
{
    partial class XPFPACK_BARCODE_PRINT_RUS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFPACK_BARCODE_PRINT_RUS));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbo_PrintDivision = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dt_PrintDate = new HKInc.Service.Controls.DateEditEx();
            this.spin_PrintQty = new DevExpress.XtraEditors.SpinEdit();
            this.spin_PerBoxQty = new DevExpress.XtraEditors.SpinEdit();
            this.pic_ProdImage = new DevExpress.XtraEditors.PictureEdit();
            this.pic_PackPlasticImage = new DevExpress.XtraEditors.PictureEdit();
            this.pic_OutBoxImage = new DevExpress.XtraEditors.PictureEdit();
            this.lcPackPlasticImage = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutBoxImage = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.lcPrintInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPrintQty3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPerBoxQty = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPrintDivision = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcManufactureDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProdImage = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.lcPackPlasticImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintQty3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPerBoxQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManufactureDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProdImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbo_PrintDivision);
            this.layoutControl1.Controls.Add(this.dt_PrintDate);
            this.layoutControl1.Controls.Add(this.spin_PrintQty);
            this.layoutControl1.Controls.Add(this.spin_PerBoxQty);
            this.layoutControl1.Controls.Add(this.pic_ProdImage);
            this.layoutControl1.Controls.Add(this.pic_PackPlasticImage);
            this.layoutControl1.Controls.Add(this.pic_OutBoxImage);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPackPlasticImage,
            this.lcOutBoxImage});
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(652, 357);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbo_PrintDivision
            // 
            this.cbo_PrintDivision.Location = new System.Drawing.Point(95, 280);
            this.cbo_PrintDivision.Name = "cbo_PrintDivision";
            this.cbo_PrintDivision.Properties.Appearance.Options.UseTextOptions = true;
            this.cbo_PrintDivision.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_PrintDivision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_PrintDivision.Properties.ReadOnly = true;
            this.cbo_PrintDivision.Size = new System.Drawing.Size(228, 24);
            this.cbo_PrintDivision.StyleController = this.layoutControl1;
            this.cbo_PrintDivision.TabIndex = 5;
            // 
            // dt_PrintDate
            // 
            this.dt_PrintDate.EditValue = null;
            this.dt_PrintDate.Location = new System.Drawing.Point(398, 280);
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
            this.dt_PrintDate.Size = new System.Drawing.Size(230, 24);
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
            this.spin_PrintQty.Location = new System.Drawing.Point(95, 308);
            this.spin_PrintQty.Name = "spin_PrintQty";
            this.spin_PrintQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_PrintQty.Size = new System.Drawing.Size(228, 24);
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
            this.spin_PerBoxQty.Location = new System.Drawing.Point(398, 308);
            this.spin_PerBoxQty.Name = "spin_PerBoxQty";
            this.spin_PerBoxQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_PerBoxQty.Size = new System.Drawing.Size(230, 24);
            this.spin_PerBoxQty.StyleController = this.layoutControl1;
            this.spin_PerBoxQty.TabIndex = 1;
            // 
            // pic_ProdImage
            // 
            this.pic_ProdImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pic_ProdImage.Location = new System.Drawing.Point(24, 50);
            this.pic_ProdImage.Name = "pic_ProdImage";
            this.pic_ProdImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_ProdImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_ProdImage.Size = new System.Drawing.Size(604, 176);
            this.pic_ProdImage.StyleController = this.layoutControl1;
            this.pic_ProdImage.TabIndex = 0;
            // 
            // pic_PackPlasticImage
            // 
            this.pic_PackPlasticImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pic_PackPlasticImage.Location = new System.Drawing.Point(241, 50);
            this.pic_PackPlasticImage.Name = "pic_PackPlasticImage";
            this.pic_PackPlasticImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_PackPlasticImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_PackPlasticImage.Size = new System.Drawing.Size(182, 176);
            this.pic_PackPlasticImage.StyleController = this.layoutControl1;
            this.pic_PackPlasticImage.TabIndex = 1;
            // 
            // pic_OutBoxImage
            // 
            this.pic_OutBoxImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pic_OutBoxImage.Location = new System.Drawing.Point(241, 50);
            this.pic_OutBoxImage.Name = "pic_OutBoxImage";
            this.pic_OutBoxImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_OutBoxImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_OutBoxImage.Size = new System.Drawing.Size(387, 176);
            this.pic_OutBoxImage.StyleController = this.layoutControl1;
            this.pic_OutBoxImage.TabIndex = 2;
            // 
            // lcPackPlasticImage
            // 
            this.lcPackPlasticImage.CustomizationFormText = "lcPackPlasticImage";
            this.lcPackPlasticImage.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcPackPlasticImage.Location = new System.Drawing.Point(217, 0);
            this.lcPackPlasticImage.Name = "lcPackPlasticImage";
            this.lcPackPlasticImage.OptionsItemText.TextToControlDistance = 4;
            this.lcPackPlasticImage.Size = new System.Drawing.Size(210, 230);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.pic_PackPlasticImage;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(186, 180);
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
            this.lcOutBoxImage.Location = new System.Drawing.Point(217, 0);
            this.lcOutBoxImage.Name = "lcOutBoxImage";
            this.lcOutBoxImage.OptionsItemText.TextToControlDistance = 4;
            this.lcOutBoxImage.Size = new System.Drawing.Size(415, 230);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.pic_OutBoxImage;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(391, 180);
            this.layoutControlItem3.Text = "layoutControlItem1";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleSeparator1,
            this.lcPrintInfo,
            this.lcProdImage});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(652, 357);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 336);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(632, 1);
            // 
            // lcPrintInfo
            // 
            this.lcPrintInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPrintQty3,
            this.lcPerBoxQty,
            this.lcPrintDivision,
            this.lcManufactureDate});
            this.lcPrintInfo.Location = new System.Drawing.Point(0, 230);
            this.lcPrintInfo.Name = "lcPrintInfo";
            this.lcPrintInfo.OptionsItemText.TextToControlDistance = 4;
            this.lcPrintInfo.Size = new System.Drawing.Size(632, 106);
            this.lcPrintInfo.Text = "인쇄정보";
            // 
            // lcPrintQty3
            // 
            this.lcPrintQty3.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcPrintQty3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcPrintQty3.Control = this.spin_PrintQty;
            this.lcPrintQty3.Location = new System.Drawing.Point(0, 28);
            this.lcPrintQty3.Name = "lcPrintQty3";
            this.lcPrintQty3.Size = new System.Drawing.Size(303, 28);
            this.lcPrintQty3.Text = "인쇄수량";
            this.lcPrintQty3.TextSize = new System.Drawing.Size(68, 18);
            // 
            // lcPerBoxQty
            // 
            this.lcPerBoxQty.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcPerBoxQty.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcPerBoxQty.Control = this.spin_PerBoxQty;
            this.lcPerBoxQty.CustomizationFormText = "lcResultQty";
            this.lcPerBoxQty.Location = new System.Drawing.Point(303, 28);
            this.lcPerBoxQty.Name = "lcPerBoxQty";
            this.lcPerBoxQty.Size = new System.Drawing.Size(305, 28);
            this.lcPerBoxQty.Text = "BOX당수량";
            this.lcPerBoxQty.TextSize = new System.Drawing.Size(68, 18);
            // 
            // lcPrintDivision
            // 
            this.lcPrintDivision.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcPrintDivision.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcPrintDivision.Control = this.cbo_PrintDivision;
            this.lcPrintDivision.Location = new System.Drawing.Point(0, 0);
            this.lcPrintDivision.Name = "lcPrintDivision";
            this.lcPrintDivision.Size = new System.Drawing.Size(303, 28);
            this.lcPrintDivision.Text = "출력구분";
            this.lcPrintDivision.TextSize = new System.Drawing.Size(68, 18);
            // 
            // lcManufactureDate
            // 
            this.lcManufactureDate.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcManufactureDate.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcManufactureDate.Control = this.dt_PrintDate;
            this.lcManufactureDate.Location = new System.Drawing.Point(303, 0);
            this.lcManufactureDate.Name = "lcManufactureDate";
            this.lcManufactureDate.Size = new System.Drawing.Size(305, 28);
            this.lcManufactureDate.Text = "제조일";
            this.lcManufactureDate.TextSize = new System.Drawing.Size(68, 18);
            // 
            // lcProdImage
            // 
            this.lcProdImage.CustomizationFormText = "lcProdImage";
            this.lcProdImage.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcProdImage.Location = new System.Drawing.Point(0, 0);
            this.lcProdImage.Name = "lcProdImage";
            this.lcProdImage.OptionsItemText.TextToControlDistance = 4;
            this.lcProdImage.Size = new System.Drawing.Size(632, 230);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pic_ProdImage;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(608, 180);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // XPFPACK_BARCODE_PRINT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 416);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
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
            ((System.ComponentModel.ISupportInitialize)(this.lcPackPlasticImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintQty3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPerBoxQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManufactureDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProdImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
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
    }
}