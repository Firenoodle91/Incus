namespace HKInc.Ui.View.View.PUR
{
    partial class XFPUR1200
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFPUR1200));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dt_InDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.btn_PurchaseStatus = new DevExpress.XtraEditors.SimpleButton();
            this.lup_InId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_InCustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcInCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcInId = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcInDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcInMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcInDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_InId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_InCustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dt_InDate);
            this.layoutControl1.Controls.Add(this.btn_PurchaseStatus);
            this.layoutControl1.Controls.Add(this.lup_InId);
            this.layoutControl1.Controls.Add(this.lup_InCustomerCode);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dt_InDate
            // 
            this.dt_InDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_InDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_InDate.Appearance.Options.UseBackColor = true;
            this.dt_InDate.Appearance.Options.UseFont = true;
            this.dt_InDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dt_InDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dt_InDate.Location = new System.Drawing.Point(89, 56);
            this.dt_InDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_InDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_InDate.MinimumSize = new System.Drawing.Size(243, 26);
            this.dt_InDate.Name = "dt_InDate";
            this.dt_InDate.Size = new System.Drawing.Size(243, 26);
            this.dt_InDate.TabIndex = 0;
            // 
            // btn_PurchaseStatus
            // 
            this.btn_PurchaseStatus.AutoWidthInLayoutControl = true;
            this.btn_PurchaseStatus.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_PurchaseStatus.ImageOptions.Image")));
            this.btn_PurchaseStatus.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_PurchaseStatus.Location = new System.Drawing.Point(957, 56);
            this.btn_PurchaseStatus.Name = "btn_PurchaseStatus";
            this.btn_PurchaseStatus.Size = new System.Drawing.Size(82, 27);
            this.btn_PurchaseStatus.StyleController = this.layoutControl1;
            this.btn_PurchaseStatus.TabIndex = 3;
            this.btn_PurchaseStatus.Text = "구매현황";
            // 
            // lup_InId
            // 
            this.lup_InId.Constraint = null;
            this.lup_InId.DataSource = null;
            this.lup_InId.DisplayMember = "";
            this.lup_InId.isImeModeDisable = false;
            this.lup_InId.isRequired = false;
            this.lup_InId.Location = new System.Drawing.Point(588, 56);
            this.lup_InId.Name = "lup_InId";
            this.lup_InId.NullText = "";
            this.lup_InId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_InId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_InId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_InId.Properties.NullText = "";
            this.lup_InId.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_InId.Size = new System.Drawing.Size(85, 24);
            this.lup_InId.StyleController = this.layoutControl1;
            this.lup_InId.TabIndex = 2;
            this.lup_InId.Value_1 = null;
            this.lup_InId.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // lup_InCustomerCode
            // 
            this.lup_InCustomerCode.Constraint = null;
            this.lup_InCustomerCode.DataSource = null;
            this.lup_InCustomerCode.DisplayMember = "";
            this.lup_InCustomerCode.isImeModeDisable = false;
            this.lup_InCustomerCode.isRequired = false;
            this.lup_InCustomerCode.Location = new System.Drawing.Point(396, 56);
            this.lup_InCustomerCode.Name = "lup_InCustomerCode";
            this.lup_InCustomerCode.NullText = "";
            this.lup_InCustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_InCustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_InCustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_InCustomerCode.Properties.NullText = "";
            this.lup_InCustomerCode.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_InCustomerCode.Size = new System.Drawing.Size(128, 24);
            this.lup_InCustomerCode.StyleController = this.layoutControl1;
            this.lup_InCustomerCode.TabIndex = 1;
            this.lup_InCustomerCode.Value_1 = null;
            this.lup_InCustomerCode.ValueMember = "";
            // 
            // searchGridLookUpEditEx1View
            // 
            this.searchGridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchGridLookUpEditEx1View.Name = "searchGridLookUpEditEx1View";
            this.searchGridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchGridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(31, 378);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(1008, 244);
            this.gridEx2.TabIndex = 5;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 144);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 167);
            this.gridEx1.TabIndex = 4;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcInMasterList,
            this.lcInDetailList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcInCustomer,
            this.lcInId,
            this.layoutControlItem1,
            this.lcInDate});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1044, 88);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(648, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(278, 33);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcInCustomer
            // 
            this.lcInCustomer.Control = this.lup_InCustomerCode;
            this.lcInCustomer.Location = new System.Drawing.Point(307, 0);
            this.lcInCustomer.Name = "lcInCustomer";
            this.lcInCustomer.Size = new System.Drawing.Size(192, 33);
            this.lcInCustomer.Text = "거래처";
            this.lcInCustomer.TextSize = new System.Drawing.Size(54, 18);
            // 
            // lcInId
            // 
            this.lcInId.Control = this.lup_InId;
            this.lcInId.Location = new System.Drawing.Point(499, 0);
            this.lcInId.Name = "lcInId";
            this.lcInId.Size = new System.Drawing.Size(149, 33);
            this.lcInId.TextSize = new System.Drawing.Size(54, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_PurchaseStatus;
            this.layoutControlItem1.Location = new System.Drawing.Point(926, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(88, 33);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcInDate
            // 
            this.lcInDate.Control = this.dt_InDate;
            this.lcInDate.Location = new System.Drawing.Point(0, 0);
            this.lcInDate.Name = "lcInDate";
            this.lcInDate.Size = new System.Drawing.Size(307, 33);
            this.lcInDate.TextSize = new System.Drawing.Size(54, 18);
            // 
            // lcInMasterList
            // 
            this.lcInMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcInMasterList.Location = new System.Drawing.Point(0, 88);
            this.lcInMasterList.Name = "lcInMasterList";
            this.lcInMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcInMasterList.Size = new System.Drawing.Size(1044, 228);
            this.lcInMasterList.Text = "입고등록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1014, 173);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcInDetailList
            // 
            this.lcInDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcInDetailList.Location = new System.Drawing.Point(0, 322);
            this.lcInDetailList.Name = "lcInDetailList";
            this.lcInDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcInDetailList.Size = new System.Drawing.Size(1044, 305);
            this.lcInDetailList.Text = "입고상세";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1014, 250);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 316);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1044, 6);
            // 
            // XFPUR1200
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFPUR1200";
            this.Text = "XFPUR1200";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_InId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_InCustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dt_InDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcInMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcInDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_InCustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcInCustomer;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.SimpleButton btn_PurchaseStatus;
        private Service.Controls.SearchLookUpEditEx lup_InId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcInId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lcInDate;
    }
}