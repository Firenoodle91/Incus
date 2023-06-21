namespace HKInc.Ui.View.View.ORD
{
    partial class XFORD_STOCK_DAY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFORD_STOCK_DAY));
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbo_YearMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcStockList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lup_MainCustomer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_BusinessManagementId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lcManagerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcMainCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_YearMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStockList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MainCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_BusinessManagementId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManagerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMainCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 128);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1101, 224);
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
            this.lup_Item.Location = new System.Drawing.Point(184, 50);
            this.lup_Item.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.gridView1;
            this.lup_Item.Size = new System.Drawing.Size(139, 24);
            this.lup_Item.StyleController = this.layoutControl1;
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
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbo_YearMonth);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.lup_BusinessManagementId);
            this.layoutControl1.Controls.Add(this.lup_MainCustomer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup3;
            this.layoutControl1.Size = new System.Drawing.Size(1149, 695);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
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
            this.cbo_YearMonth.Size = new System.Drawing.Size(100, 24);
            this.cbo_YearMonth.StyleController = this.layoutControl1;
            this.cbo_YearMonth.TabIndex = 0;
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(24, 418);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(1101, 253);
            this.gridEx2.TabIndex = 3;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcStockList,
            this.lcDetailList,
            this.splitterItem1});
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup3.Size = new System.Drawing.Size(1149, 695);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItem,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.lcManagerName,
            this.lcMainCustomer});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1129, 78);
            // 
            // lcItem
            // 
            this.lcItem.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcItem.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(104, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(199, 28);
            this.lcItem.Text = "품명";
            this.lcItem.TextSize = new System.Drawing.Size(52, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(704, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(401, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbo_YearMonth;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(104, 28);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcStockList
            // 
            this.lcStockList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcStockList.Location = new System.Drawing.Point(0, 78);
            this.lcStockList.Name = "lcStockList";
            this.lcStockList.OptionsItemText.TextToControlDistance = 4;
            this.lcStockList.Size = new System.Drawing.Size(1129, 278);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1105, 228);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcDetailList
            // 
            this.lcDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcDetailList.Location = new System.Drawing.Point(0, 368);
            this.lcDetailList.Name = "lcDetailList";
            this.lcDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcDetailList.Size = new System.Drawing.Size(1129, 307);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1105, 257);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // lup_MainCustomer
            // 
            this.lup_MainCustomer.Constraint = null;
            this.lup_MainCustomer.DataSource = null;
            this.lup_MainCustomer.DisplayMember = "";
            this.lup_MainCustomer.isImeModeDisable = false;
            this.lup_MainCustomer.isRequired = false;
            this.lup_MainCustomer.Location = new System.Drawing.Point(583, 50);
            this.lup_MainCustomer.Name = "lup_MainCustomer";
            this.lup_MainCustomer.NullText = "";
            this.lup_MainCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_MainCustomer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_MainCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MainCustomer.Properties.NullText = "";
            this.lup_MainCustomer.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_MainCustomer.Size = new System.Drawing.Size(141, 24);
            this.lup_MainCustomer.StyleController = this.layoutControl1;
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
            this.lup_BusinessManagementId.Location = new System.Drawing.Point(383, 50);
            this.lup_BusinessManagementId.Name = "lup_BusinessManagementId";
            this.lup_BusinessManagementId.NullText = "";
            this.lup_BusinessManagementId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_BusinessManagementId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_BusinessManagementId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_BusinessManagementId.Properties.NullText = "";
            this.lup_BusinessManagementId.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_BusinessManagementId.Size = new System.Drawing.Size(140, 24);
            this.lup_BusinessManagementId.StyleController = this.layoutControl1;
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
            // lcManagerName
            // 
            this.lcManagerName.Control = this.lup_BusinessManagementId;
            this.lcManagerName.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcManagerName.CustomizationFormText = "담당자";
            this.lcManagerName.Location = new System.Drawing.Point(303, 0);
            this.lcManagerName.Name = "lcManagerName";
            this.lcManagerName.Size = new System.Drawing.Size(200, 28);
            this.lcManagerName.Text = "담당자명";
            this.lcManagerName.TextSize = new System.Drawing.Size(52, 18);
            // 
            // lcMainCustomer
            // 
            this.lcMainCustomer.Control = this.lup_MainCustomer;
            this.lcMainCustomer.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcMainCustomer.CustomizationFormText = "수주처";
            this.lcMainCustomer.Location = new System.Drawing.Point(503, 0);
            this.lcMainCustomer.Name = "lcMainCustomer";
            this.lcMainCustomer.Size = new System.Drawing.Size(201, 28);
            this.lcMainCustomer.Text = "주거래처";
            this.lcMainCustomer.TextSize = new System.Drawing.Size(52, 18);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 356);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1129, 12);
            // 
            // XFORD_STOCK_DAY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 754);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "XFORD_STOCK_DAY";
            this.Text = "XFORD_STOCK_DAY";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbo_YearMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStockList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MainCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_BusinessManagementId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManagerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMainCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.GridEx gridEx2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcStockList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_YearMonth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.SearchLookUpEditEx lup_BusinessManagementId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private Service.Controls.SearchLookUpEditEx lup_MainCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcManagerName;
        private DevExpress.XtraLayout.LayoutControlItem lcMainCustomer;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
    }
}