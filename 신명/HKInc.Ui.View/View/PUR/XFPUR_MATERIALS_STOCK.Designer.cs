namespace HKInc.Ui.View.View.PUR
{
    partial class XFPUR_MATERIALS_STOCK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFPUR_MATERIALS_STOCK));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dateEditEx1 = new HKInc.Service.Controls.DateEditEx();
            this.cbo_YearMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.dt_m = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcPurStockList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.tx_Lotno = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_YearMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPurStockList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Lotno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tx_Lotno);
            this.layoutControl1.Controls.Add(this.dateEditEx1);
            this.layoutControl1.Controls.Add(this.cbo_YearMonth);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 518);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dateEditEx1
            // 
            this.dateEditEx1.EditValue = null;
            this.dateEditEx1.Location = new System.Drawing.Point(22, 41);
            this.dateEditEx1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateEditEx1.Name = "dateEditEx1";
            this.dateEditEx1.Properties.Appearance.Options.UseTextOptions = true;
            this.dateEditEx1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateEditEx1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEx1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEx1.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEditEx1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEx1.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEditEx1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEx1.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateEditEx1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditEx1.Size = new System.Drawing.Size(104, 20);
            this.dateEditEx1.StyleController = this.layoutControl1;
            this.dateEditEx1.TabIndex = 5;
            // 
            // cbo_YearMonth
            // 
            this.cbo_YearMonth.Location = new System.Drawing.Point(437, 44);
            this.cbo_YearMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_YearMonth.Name = "cbo_YearMonth";
            this.cbo_YearMonth.Properties.Appearance.Options.UseTextOptions = true;
            this.cbo_YearMonth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_YearMonth.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cbo_YearMonth.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_YearMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_YearMonth.Size = new System.Drawing.Size(387, 20);
            this.cbo_YearMonth.StyleController = this.layoutControl1;
            this.cbo_YearMonth.TabIndex = 0;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(174, 41);
            this.lup_Item.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_Item.Size = new System.Drawing.Size(258, 20);
            this.lup_Item.StyleController = this.layoutControl1;
            this.lup_Item.TabIndex = 1;
            this.lup_Item.Value_1 = null;
            this.lup_Item.ValueMember = "";
            // 
            // searchGridLookUpEditEx1View
            // 
            this.searchGridLookUpEditEx1View.DetailHeight = 272;
            this.searchGridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchGridLookUpEditEx1View.Name = "searchGridLookUpEditEx1View";
            this.searchGridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchGridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(22, 326);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(892, 172);
            this.gridEx2.TabIndex = 4;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(22, 106);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(892, 165);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cbo_YearMonth;
            this.layoutControlItem5.Location = new System.Drawing.Point(468, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(448, 33);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcPurStockList,
            this.lcDetailList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.dt_m,
            this.lcItem,
            this.emptySpaceItem2,
            this.layoutControlItem2});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(918, 65);
            this.lcCondition.Text = "조회조건";
            // 
            // dt_m
            // 
            this.dt_m.Control = this.dateEditEx1;
            this.dt_m.Location = new System.Drawing.Point(0, 0);
            this.dt_m.Name = "dt_m";
            this.dt_m.Size = new System.Drawing.Size(108, 24);
            this.dt_m.TextSize = new System.Drawing.Size(0, 0);
            this.dt_m.TextVisible = false;
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(108, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(306, 24);
            this.lcItem.Text = "품명";
            this.lcItem.TextSize = new System.Drawing.Size(40, 14);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(694, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(202, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcPurStockList
            // 
            this.lcPurStockList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcPurStockList.Location = new System.Drawing.Point(0, 65);
            this.lcPurStockList.Name = "lcPurStockList";
            this.lcPurStockList.OptionsItemText.TextToControlDistance = 4;
            this.lcPurStockList.Size = new System.Drawing.Size(918, 210);
            this.lcPurStockList.Text = "자재재고목록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(896, 169);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcDetailList
            // 
            this.lcDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcDetailList.Location = new System.Drawing.Point(0, 285);
            this.lcDetailList.Name = "lcDetailList";
            this.lcDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcDetailList.Size = new System.Drawing.Size(918, 217);
            this.lcDetailList.Text = "상세내역";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(896, 176);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 275);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(918, 10);
            // 
            // tx_Lotno
            // 
            this.tx_Lotno.Location = new System.Drawing.Point(480, 41);
            this.tx_Lotno.Name = "tx_Lotno";
            this.tx_Lotno.Size = new System.Drawing.Size(232, 20);
            this.tx_Lotno.StyleController = this.layoutControl1;
            this.tx_Lotno.TabIndex = 6;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tx_Lotno;
            this.layoutControlItem2.Location = new System.Drawing.Point(414, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(280, 24);
            this.layoutControlItem2.Text = "LOTNO";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(40, 14);
            // 
            // XFPUR_MATERIALS_STOCK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XFPUR_MATERIALS_STOCK";
            this.Text = "XFPUR_MATERIALS_STOCK";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_YearMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPurStockList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Lotno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcPurStockList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_YearMonth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Service.Controls.DateEditEx dateEditEx1;
        private DevExpress.XtraLayout.LayoutControlItem dt_m;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit tx_Lotno;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}