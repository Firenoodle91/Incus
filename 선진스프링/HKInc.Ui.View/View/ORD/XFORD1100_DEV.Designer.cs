namespace HKInc.Ui.View.View.ORD
{
    partial class XFORD1100_DEV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFORD1100_DEV));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_OrderCustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx3 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.dt_OrderDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcReqDevelopmentDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustomerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.lcReqDevelopmentMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReqDevelopmentDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReqDevelopmentDelivList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubDetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_OrderCustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReqDevelopmentDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReqDevelopmentMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReqDevelopmentDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReqDevelopmentDelivList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_OrderCustomerCode);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx3);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.dt_OrderDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_OrderCustomerCode
            // 
            this.lup_OrderCustomerCode.Constraint = null;
            this.lup_OrderCustomerCode.DataSource = null;
            this.lup_OrderCustomerCode.DisplayMember = "";
            this.lup_OrderCustomerCode.isImeModeDisable = false;
            this.lup_OrderCustomerCode.isRequired = false;
            this.lup_OrderCustomerCode.Location = new System.Drawing.Point(510, 56);
            this.lup_OrderCustomerCode.Name = "lup_OrderCustomerCode";
            this.lup_OrderCustomerCode.NullText = "";
            this.lup_OrderCustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_OrderCustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_OrderCustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_OrderCustomerCode.Properties.NullText = "";
            this.lup_OrderCustomerCode.Properties.PopupView = this.gridView1;
            this.lup_OrderCustomerCode.Size = new System.Drawing.Size(66, 24);
            this.lup_OrderCustomerCode.StyleController = this.layoutControl1;
            this.lup_OrderCustomerCode.TabIndex = 1;
            this.lup_OrderCustomerCode.Value_1 = null;
            this.lup_OrderCustomerCode.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(697, 56);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Item.Size = new System.Drawing.Size(68, 24);
            this.lup_Item.StyleController = this.layoutControl1;
            this.lup_Item.TabIndex = 2;
            this.lup_Item.Value_1 = null;
            this.lup_Item.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(31, 366);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(455, 256);
            this.gridEx2.TabIndex = 4;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx3
            // 
            this.gridEx3.DataSource = null;
            this.gridEx3.Location = new System.Drawing.Point(528, 143);
            this.gridEx3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx3.Name = "gridEx3";
            this.gridEx3.Size = new System.Drawing.Size(511, 479);
            this.gridEx3.TabIndex = 5;
            this.gridEx3.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 143);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(455, 156);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // dt_OrderDate
            // 
            this.dt_OrderDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_OrderDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_OrderDate.Appearance.Options.UseBackColor = true;
            this.dt_OrderDate.Appearance.Options.UseFont = true;
            this.dt_OrderDate.EditFrValue = new System.DateTime(2019, 1, 28, 0, 0, 0, 0);
            this.dt_OrderDate.EditToValue = new System.DateTime(2019, 2, 28, 23, 59, 59, 990);
            this.dt_OrderDate.Location = new System.Drawing.Point(146, 56);
            this.dt_OrderDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_OrderDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_OrderDate.MinimumSize = new System.Drawing.Size(243, 26);
            this.dt_OrderDate.Name = "dt_OrderDate";
            this.dt_OrderDate.Size = new System.Drawing.Size(243, 26);
            this.dt_OrderDate.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.splitterItem1,
            this.splitterItem2,
            this.lcReqDevelopmentMasterList,
            this.lcReqDevelopmentDetailList,
            this.lcReqDevelopmentDelivList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcReqDevelopmentDate,
            this.emptySpaceItem2,
            this.lcItem,
            this.lcCustomerName});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1044, 87);
            this.lcCondition.Text = "조회조건";
            // 
            // lcReqDevelopmentDate
            // 
            this.lcReqDevelopmentDate.Control = this.dt_OrderDate;
            this.lcReqDevelopmentDate.Location = new System.Drawing.Point(0, 0);
            this.lcReqDevelopmentDate.Name = "lcReqDevelopmentDate";
            this.lcReqDevelopmentDate.Size = new System.Drawing.Size(364, 32);
            this.lcReqDevelopmentDate.Text = "수주일";
            this.lcReqDevelopmentDate.TextSize = new System.Drawing.Size(111, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(740, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(274, 32);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(551, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(189, 32);
            this.lcItem.TextSize = new System.Drawing.Size(111, 18);
            // 
            // lcCustomerName
            // 
            this.lcCustomerName.Control = this.lup_OrderCustomerCode;
            this.lcCustomerName.Location = new System.Drawing.Point(364, 0);
            this.lcCustomerName.Name = "lcCustomerName";
            this.lcCustomerName.Size = new System.Drawing.Size(187, 32);
            this.lcCustomerName.TextSize = new System.Drawing.Size(111, 18);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 304);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(491, 6);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(491, 87);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(6, 540);
            // 
            // lcReqDevelopmentMasterList
            // 
            this.lcReqDevelopmentMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcReqDevelopmentMasterList.Location = new System.Drawing.Point(0, 87);
            this.lcReqDevelopmentMasterList.Name = "lcReqDevelopmentMasterList";
            this.lcReqDevelopmentMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcReqDevelopmentMasterList.Size = new System.Drawing.Size(491, 217);
            this.lcReqDevelopmentMasterList.Text = "수주내역";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(461, 162);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // lcReqDevelopmentDetailList
            // 
            this.lcReqDevelopmentDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.lcReqDevelopmentDetailList.Location = new System.Drawing.Point(0, 310);
            this.lcReqDevelopmentDetailList.Name = "lcReqDevelopmentDetailList";
            this.lcReqDevelopmentDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcReqDevelopmentDetailList.Size = new System.Drawing.Size(491, 317);
            this.lcReqDevelopmentDetailList.Text = "수주상세";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridEx2;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(461, 262);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // lcReqDevelopmentDelivList
            // 
            this.lcReqDevelopmentDelivList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.lcReqDevelopmentDelivList.Location = new System.Drawing.Point(497, 87);
            this.lcReqDevelopmentDelivList.Name = "lcReqDevelopmentDelivList";
            this.lcReqDevelopmentDelivList.OptionsItemText.TextToControlDistance = 4;
            this.lcReqDevelopmentDelivList.Size = new System.Drawing.Size(547, 540);
            this.lcReqDevelopmentDelivList.Text = "납품계획";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridEx3;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(517, 485);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // XFORD1100_DEV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFORD1100_DEV";
            this.Text = "XFORD1100_DEV";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubDetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_OrderCustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReqDevelopmentDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReqDevelopmentMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReqDevelopmentDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReqDevelopmentDelivList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx3;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dt_OrderDate;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcReqDevelopmentDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcReqDevelopmentMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup lcReqDevelopmentDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup lcReqDevelopmentDelivList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private Service.Controls.SearchLookUpEditEx lup_OrderCustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerName;
    }
}