namespace HKInc.Ui.View.View.POP_POPUP
{
    partial class XPFITEMMOVEPRINT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFITEMMOVEPRINT));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_RePrint = new DevExpress.XtraEditors.SimpleButton();
            this.lup_ItemMoveNo = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_Print = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            this.lcItemMoveNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.spin_NotPrintQty = new DevExpress.XtraEditors.SpinEdit();
            this.lcNotPrintQty = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemMoveNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMoveNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_NotPrintQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNotPrintQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spin_NotPrintQty);
            this.layoutControl1.Controls.Add(this.btn_RePrint);
            this.layoutControl1.Controls.Add(this.lup_ItemMoveNo);
            this.layoutControl1.Controls.Add(this.btn_Print);
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(660, 292);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_RePrint
            // 
            this.btn_RePrint.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_RePrint.Appearance.Options.UseFont = true;
            this.btn_RePrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_RePrint.ImageOptions.Image")));
            this.btn_RePrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btn_RePrint.Location = new System.Drawing.Point(333, 12);
            this.btn_RePrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_RePrint.Name = "btn_RePrint";
            this.btn_RePrint.Size = new System.Drawing.Size(315, 114);
            this.btn_RePrint.StyleController = this.layoutControl1;
            this.btn_RePrint.TabIndex = 1;
            this.btn_RePrint.Text = "재출력";
            // 
            // lup_ItemMoveNo
            // 
            this.lup_ItemMoveNo.Constraint = null;
            this.lup_ItemMoveNo.DataSource = null;
            this.lup_ItemMoveNo.DisplayMember = "";
            this.lup_ItemMoveNo.isImeModeDisable = false;
            this.lup_ItemMoveNo.isRequired = false;
            this.lup_ItemMoveNo.Location = new System.Drawing.Point(12, 50);
            this.lup_ItemMoveNo.Name = "lup_ItemMoveNo";
            this.lup_ItemMoveNo.NullText = "";
            this.lup_ItemMoveNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_ItemMoveNo.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lup_ItemMoveNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_ItemMoveNo.Properties.Appearance.Options.UseFont = true;
            this.lup_ItemMoveNo.Properties.AutoHeight = false;
            this.lup_ItemMoveNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_ItemMoveNo.Properties.NullText = "";
            this.lup_ItemMoveNo.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_ItemMoveNo.Size = new System.Drawing.Size(317, 76);
            this.lup_ItemMoveNo.StyleController = this.layoutControl1;
            this.lup_ItemMoveNo.TabIndex = 0;
            this.lup_ItemMoveNo.Value_1 = null;
            this.lup_ItemMoveNo.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // btn_Print
            // 
            this.btn_Print.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_Print.Appearance.Options.UseFont = true;
            this.btn_Print.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Print.ImageOptions.Image")));
            this.btn_Print.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btn_Print.Location = new System.Drawing.Point(12, 177);
            this.btn_Print.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(316, 103);
            this.btn_Print.StyleController = this.layoutControl1;
            this.btn_Print.TabIndex = 2;
            this.btn_Print.Text = "새 출력";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.ImageOptions.Image")));
            this.btn_Cancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btn_Cancel.Location = new System.Drawing.Point(333, 177);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(315, 103);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "취소";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.simpleSeparator1,
            this.simpleSeparator2,
            this.lcItemMoveNo,
            this.layoutControlItem1,
            this.lcNotPrintQty,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(660, 292);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_Print;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 165);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(94, 46);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(320, 107);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_Cancel;
            this.layoutControlItem5.Location = new System.Drawing.Point(321, 165);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(94, 46);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(319, 107);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 118);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(640, 1);
            // 
            // simpleSeparator2
            // 
            this.simpleSeparator2.AllowHotTrack = false;
            this.simpleSeparator2.Location = new System.Drawing.Point(320, 165);
            this.simpleSeparator2.Name = "simpleSeparator2";
            this.simpleSeparator2.Size = new System.Drawing.Size(1, 107);
            // 
            // lcItemMoveNo
            // 
            this.lcItemMoveNo.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lcItemMoveNo.AppearanceItemCaption.Options.UseFont = true;
            this.lcItemMoveNo.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcItemMoveNo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcItemMoveNo.Control = this.lup_ItemMoveNo;
            this.lcItemMoveNo.Location = new System.Drawing.Point(0, 0);
            this.lcItemMoveNo.MinSize = new System.Drawing.Size(131, 90);
            this.lcItemMoveNo.Name = "lcItemMoveNo";
            this.lcItemMoveNo.Size = new System.Drawing.Size(321, 118);
            this.lcItemMoveNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcItemMoveNo.Text = "이동표번호";
            this.lcItemMoveNo.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcItemMoveNo.TextSize = new System.Drawing.Size(125, 35);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_RePrint;
            this.layoutControlItem1.Location = new System.Drawing.Point(321, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(119, 46);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(319, 118);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // spin_NotPrintQty
            // 
            this.spin_NotPrintQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_NotPrintQty.Location = new System.Drawing.Point(152, 131);
            this.spin_NotPrintQty.Name = "spin_NotPrintQty";
            this.spin_NotPrintQty.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.spin_NotPrintQty.Properties.Appearance.Options.UseFont = true;
            this.spin_NotPrintQty.Properties.AutoHeight = false;
            this.spin_NotPrintQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_NotPrintQty.Size = new System.Drawing.Size(177, 42);
            this.spin_NotPrintQty.StyleController = this.layoutControl1;
            this.spin_NotPrintQty.TabIndex = 4;
            // 
            // lcNotPrintQty
            // 
            this.lcNotPrintQty.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lcNotPrintQty.AppearanceItemCaption.Options.UseFont = true;
            this.lcNotPrintQty.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcNotPrintQty.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcNotPrintQty.Control = this.spin_NotPrintQty;
            this.lcNotPrintQty.Location = new System.Drawing.Point(0, 119);
            this.lcNotPrintQty.Name = "lcNotPrintQty";
            this.lcNotPrintQty.Size = new System.Drawing.Size(321, 46);
            this.lcNotPrintQty.Text = "미출력수량";
            this.lcNotPrintQty.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcNotPrintQty.TextSize = new System.Drawing.Size(135, 35);
            this.lcNotPrintQty.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(321, 119);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(319, 46);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XPFITEMMOVEPRINT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 351);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFITEMMOVEPRINT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "이동표출력";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemMoveNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMoveNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_NotPrintQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNotPrintQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btn_Print;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private Service.Controls.SearchLookUpEditEx lup_ItemMoveNo;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcItemMoveNo;
        private DevExpress.XtraEditors.SimpleButton btn_RePrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SpinEdit spin_NotPrintQty;
        private DevExpress.XtraLayout.LayoutControlItem lcNotPrintQty;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}