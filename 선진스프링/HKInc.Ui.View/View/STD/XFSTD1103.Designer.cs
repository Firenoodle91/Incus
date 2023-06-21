namespace HKInc.Ui.View.View.STD
{
    partial class XFSTD1103
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFSTD1103));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.lup_Customer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx3 = new HKInc.Service.Controls.GridEx();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustomerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustomerList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.lcHistoryCostList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubDetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Customer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcHistoryCostList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.lup_Customer);
            this.layoutControl1.Controls.Add(this.gridEx3);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1117, 627);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(581, 141);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(505, 232);
            this.gridEx2.TabIndex = 3;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_Customer
            // 
            this.lup_Customer.Constraint = null;
            this.lup_Customer.DataSource = null;
            this.lup_Customer.DisplayMember = "";
            this.lup_Customer.isImeModeDisable = false;
            this.lup_Customer.isRequired = false;
            this.lup_Customer.Location = new System.Drawing.Point(403, 56);
            this.lup_Customer.Name = "lup_Customer";
            this.lup_Customer.NullText = "";
            this.lup_Customer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Customer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Customer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Customer.Properties.NullText = "";
            this.lup_Customer.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Customer.Size = new System.Drawing.Size(150, 24);
            this.lup_Customer.StyleController = this.layoutControl1;
            this.lup_Customer.TabIndex = 1;
            this.lup_Customer.Value_1 = null;
            this.lup_Customer.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx3
            // 
            this.gridEx3.DataSource = null;
            this.gridEx3.Location = new System.Drawing.Point(31, 440);
            this.gridEx3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx3.Name = "gridEx3";
            this.gridEx3.Size = new System.Drawing.Size(1055, 156);
            this.gridEx3.TabIndex = 4;
            this.gridEx3.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(146, 56);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.gridView1;
            this.lup_Item.Size = new System.Drawing.Size(136, 24);
            this.lup_Item.StyleController = this.layoutControl1;
            this.lup_Item.TabIndex = 0;
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
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 141);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(508, 232);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcItemList,
            this.lcCustomerList,
            this.splitterItem1,
            this.lcHistoryCostList,
            this.splitterItem2});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1117, 627);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcItem,
            this.lcCustomerName});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1091, 85);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(528, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(533, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(0, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(257, 30);
            this.lcItem.TextSize = new System.Drawing.Size(111, 18);
            // 
            // lcCustomerName
            // 
            this.lcCustomerName.Control = this.lup_Customer;
            this.lcCustomerName.Location = new System.Drawing.Point(257, 0);
            this.lcCustomerName.Name = "lcCustomerName";
            this.lcCustomerName.Size = new System.Drawing.Size(271, 30);
            this.lcCustomerName.TextSize = new System.Drawing.Size(111, 18);
            // 
            // lcItemList
            // 
            this.lcItemList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcItemList.Location = new System.Drawing.Point(0, 85);
            this.lcItemList.Name = "lcItemList";
            this.lcItemList.OptionsItemText.TextToControlDistance = 4;
            this.lcItemList.Size = new System.Drawing.Size(544, 293);
            this.lcItemList.Text = "품목정보";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(514, 238);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcCustomerList
            // 
            this.lcCustomerList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcCustomerList.Location = new System.Drawing.Point(550, 85);
            this.lcCustomerList.Name = "lcCustomerList";
            this.lcCustomerList.OptionsItemText.TextToControlDistance = 4;
            this.lcCustomerList.Size = new System.Drawing.Size(541, 293);
            this.lcCustomerList.Text = "도면";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(511, 238);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(544, 85);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(6, 293);
            // 
            // lcHistoryCostList
            // 
            this.lcHistoryCostList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcHistoryCostList.Location = new System.Drawing.Point(0, 384);
            this.lcHistoryCostList.Name = "lcHistoryCostList";
            this.lcHistoryCostList.OptionsItemText.TextToControlDistance = 4;
            this.lcHistoryCostList.Size = new System.Drawing.Size(1091, 217);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridEx3;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1061, 162);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.CustomizationFormText = "splitterItem1";
            this.splitterItem2.Location = new System.Drawing.Point(0, 378);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(1091, 6);
            // 
            // XFSTD1103
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 700);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFSTD1103";
            this.Text = "XFSTD1103";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubDetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Customer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcHistoryCostList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcItemList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcCustomerList;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private Service.Controls.GridEx gridEx3;
        private DevExpress.XtraLayout.LayoutControlGroup lcHistoryCostList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.SearchLookUpEditEx lup_Customer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerName;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private Service.Controls.GridEx gridEx2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}