namespace HKInc.Ui.View.View.ORD
{
    partial class XFORD_STOCK_LOT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFORD_STOCK_LOT));
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.cbo_YearMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.lup_MainCustomer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_BusinessManagementId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.IcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcManagerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcMainCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.재고현황 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_YearMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MainCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_BusinessManagementId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManagerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMainCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.재고현황)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.cbo_YearMonth);
            this.dataLayoutControl1.Controls.Add(this.gridEx1);
            this.dataLayoutControl1.Controls.Add(this.lup_Item);
            this.dataLayoutControl1.Controls.Add(this.gridEx2);
            this.dataLayoutControl1.Controls.Add(this.lup_MainCustomer);
            this.dataLayoutControl1.Controls.Add(this.lup_BusinessManagementId);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 30);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(1149, 695);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // cbo_YearMonth
            // 
            this.cbo_YearMonth.Location = new System.Drawing.Point(24, 50);
            this.cbo_YearMonth.Name = "cbo_YearMonth";
            this.cbo_YearMonth.Properties.Appearance.Options.UseTextOptions = true;
            this.cbo_YearMonth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_YearMonth.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cbo_YearMonth.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_YearMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_YearMonth.Size = new System.Drawing.Size(93, 24);
            this.cbo_YearMonth.StyleController = this.dataLayoutControl1;
            this.cbo_YearMonth.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 128);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1101, 230);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(177, 50);
            this.lup_Item.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.gridView1;
            this.lup_Item.Size = new System.Drawing.Size(119, 24);
            this.lup_Item.StyleController = this.dataLayoutControl1;
            this.lup_Item.TabIndex = 1;
            this.lup_Item.Value_1 = null;
            this.lup_Item.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(24, 436);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(1101, 235);
            this.gridEx2.TabIndex = 3;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_MainCustomer
            // 
            this.lup_MainCustomer.Constraint = null;
            this.lup_MainCustomer.DataSource = null;
            this.lup_MainCustomer.DisplayMember = "";
            this.lup_MainCustomer.isImeModeDisable = false;
            this.lup_MainCustomer.isRequired = false;
            this.lup_MainCustomer.Location = new System.Drawing.Point(540, 50);
            this.lup_MainCustomer.Name = "lup_MainCustomer";
            this.lup_MainCustomer.NullText = "";
            this.lup_MainCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_MainCustomer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_MainCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MainCustomer.Properties.NullText = "";
            this.lup_MainCustomer.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_MainCustomer.Size = new System.Drawing.Size(127, 24);
            this.lup_MainCustomer.StyleController = this.dataLayoutControl1;
            this.lup_MainCustomer.TabIndex = 2;
            this.lup_MainCustomer.Value_1 = null;
            this.lup_MainCustomer.ValueMember = "";
            // 
            // searchGridLookUpEditEx1View
            // 
            this.searchGridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchGridLookUpEditEx1View.Name = "searchGridLookUpEditEx1View";
            this.searchGridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchGridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // lup_BusinessManagementId
            // 
            this.lup_BusinessManagementId.Constraint = null;
            this.lup_BusinessManagementId.DataSource = null;
            this.lup_BusinessManagementId.DisplayMember = "";
            this.lup_BusinessManagementId.isImeModeDisable = false;
            this.lup_BusinessManagementId.isRequired = false;
            this.lup_BusinessManagementId.Location = new System.Drawing.Point(356, 50);
            this.lup_BusinessManagementId.Name = "lup_BusinessManagementId";
            this.lup_BusinessManagementId.NullText = "";
            this.lup_BusinessManagementId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_BusinessManagementId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_BusinessManagementId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_BusinessManagementId.Properties.NullText = "";
            this.lup_BusinessManagementId.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_BusinessManagementId.Size = new System.Drawing.Size(124, 24);
            this.lup_BusinessManagementId.StyleController = this.dataLayoutControl1;
            this.lup_BusinessManagementId.TabIndex = 1;
            this.lup_BusinessManagementId.Value_1 = null;
            this.lup_BusinessManagementId.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.재고현황,
            this.lcDetailList,
            this.splitterItem1,
            this.splitterItem2});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1149, 695);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.IcItem,
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.lcManagerName,
            this.lcMainCustomer});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1129, 78);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // IcItem
            // 
            this.IcItem.AppearanceItemCaption.Options.UseTextOptions = true;
            this.IcItem.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.IcItem.Control = this.lup_Item;
            this.IcItem.Location = new System.Drawing.Point(97, 0);
            this.IcItem.Name = "IcItem";
            this.IcItem.Size = new System.Drawing.Size(179, 28);
            this.IcItem.Text = "품목";
            this.IcItem.TextLocation = DevExpress.Utils.Locations.Left;
            this.IcItem.TextSize = new System.Drawing.Size(52, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(647, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(458, 28);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbo_YearMonth;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(97, 28);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcManagerName
            // 
            this.lcManagerName.Control = this.lup_BusinessManagementId;
            this.lcManagerName.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcManagerName.CustomizationFormText = "담당자";
            this.lcManagerName.Location = new System.Drawing.Point(276, 0);
            this.lcManagerName.Name = "lcManagerName";
            this.lcManagerName.Size = new System.Drawing.Size(184, 28);
            this.lcManagerName.Text = "담당자명";
            this.lcManagerName.TextSize = new System.Drawing.Size(52, 18);
            // 
            // lcMainCustomer
            // 
            this.lcMainCustomer.Control = this.lup_MainCustomer;
            this.lcMainCustomer.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcMainCustomer.CustomizationFormText = "수주처";
            this.lcMainCustomer.Location = new System.Drawing.Point(460, 0);
            this.lcMainCustomer.Name = "lcMainCustomer";
            this.lcMainCustomer.Size = new System.Drawing.Size(187, 28);
            this.lcMainCustomer.Text = "주거래처";
            this.lcMainCustomer.TextSize = new System.Drawing.Size(52, 18);
            // 
            // 재고현황
            // 
            this.재고현황.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.재고현황.Location = new System.Drawing.Point(0, 78);
            this.재고현황.Name = "재고현황";
            this.재고현황.OptionsItemText.TextToControlDistance = 4;
            this.재고현황.Size = new System.Drawing.Size(1129, 284);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1105, 234);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // lcDetailList
            // 
            this.lcDetailList.CustomizationFormText = "layoutControlGroup3";
            this.lcDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.lcDetailList.Location = new System.Drawing.Point(0, 386);
            this.lcDetailList.Name = "lcDetailList";
            this.lcDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcDetailList.Size = new System.Drawing.Size(1129, 289);
            this.lcDetailList.Text = "상세내역";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridEx2;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1105, 239);
            this.layoutControlItem5.Text = "layoutControlItem4";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 374);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1129, 12);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(0, 362);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(1129, 12);
            // 
            // XFORD_STOCK_LOT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 754);
            this.Controls.Add(this.dataLayoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "XFORD_STOCK_LOT";
            this.Text = "XFORD_STOCK_LOT";
            this.Controls.SetChildIndex(this.dataLayoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbo_YearMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MainCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_BusinessManagementId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManagerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMainCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.재고현황)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem IcItem;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup 재고현황;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.GridEx gridEx2;
        private DevExpress.XtraLayout.LayoutControlGroup lcDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_YearMonth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private Service.Controls.SearchLookUpEditEx lup_MainCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private Service.Controls.SearchLookUpEditEx lup_BusinessManagementId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcManagerName;
        private DevExpress.XtraLayout.LayoutControlItem lcMainCustomer;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
    }
}