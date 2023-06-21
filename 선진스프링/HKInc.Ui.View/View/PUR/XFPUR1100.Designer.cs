namespace HKInc.Ui.View.View.PUR
{
    partial class XFPUR1100
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFPUR1100));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_PurchaseStatus = new DevExpress.XtraEditors.SimpleButton();
            this.lup_PoCustomer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_PoId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.dt_PoDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPoDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcPoId = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPoCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPoMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPoDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_PoCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_PoId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_PurchaseStatus);
            this.layoutControl1.Controls.Add(this.lup_PoCustomer);
            this.layoutControl1.Controls.Add(this.lup_PoId);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.dt_PoDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_PurchaseStatus
            // 
            this.btn_PurchaseStatus.AutoWidthInLayoutControl = true;
            this.btn_PurchaseStatus.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_PurchaseStatus.ImageOptions.Image")));
            this.btn_PurchaseStatus.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_PurchaseStatus.Location = new System.Drawing.Point(927, 56);
            this.btn_PurchaseStatus.Name = "btn_PurchaseStatus";
            this.btn_PurchaseStatus.Size = new System.Drawing.Size(112, 27);
            this.btn_PurchaseStatus.StyleController = this.layoutControl1;
            this.btn_PurchaseStatus.TabIndex = 3;
            this.btn_PurchaseStatus.Text = "구매현황(&F)";
            // 
            // lup_PoCustomer
            // 
            this.lup_PoCustomer.Constraint = null;
            this.lup_PoCustomer.DataSource = null;
            this.lup_PoCustomer.DisplayMember = "";
            this.lup_PoCustomer.isImeModeDisable = false;
            this.lup_PoCustomer.isRequired = false;
            this.lup_PoCustomer.Location = new System.Drawing.Point(464, 56);
            this.lup_PoCustomer.Name = "lup_PoCustomer";
            this.lup_PoCustomer.NullText = "";
            this.lup_PoCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_PoCustomer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_PoCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_PoCustomer.Properties.NullText = "";
            this.lup_PoCustomer.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_PoCustomer.Size = new System.Drawing.Size(93, 24);
            this.lup_PoCustomer.StyleController = this.layoutControl1;
            this.lup_PoCustomer.TabIndex = 1;
            this.lup_PoCustomer.Value_1 = null;
            this.lup_PoCustomer.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // lup_PoId
            // 
            this.lup_PoId.Constraint = null;
            this.lup_PoId.DataSource = null;
            this.lup_PoId.DisplayMember = "";
            this.lup_PoId.isImeModeDisable = false;
            this.lup_PoId.isRequired = false;
            this.lup_PoId.Location = new System.Drawing.Point(655, 56);
            this.lup_PoId.Name = "lup_PoId";
            this.lup_PoId.NullText = "";
            this.lup_PoId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_PoId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_PoId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_PoId.Properties.NullText = "";
            this.lup_PoId.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_PoId.Size = new System.Drawing.Size(94, 24);
            this.lup_PoId.StyleController = this.layoutControl1;
            this.lup_PoId.TabIndex = 2;
            this.lup_PoId.Value_1 = null;
            this.lup_PoId.ValueMember = "";
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
            this.gridEx2.Location = new System.Drawing.Point(31, 385);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(1008, 237);
            this.gridEx2.TabIndex = 5;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 144);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 174);
            this.gridEx1.TabIndex = 4;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // dt_PoDate
            // 
            this.dt_PoDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_PoDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_PoDate.Appearance.Options.UseBackColor = true;
            this.dt_PoDate.Appearance.Options.UseFont = true;
            this.dt_PoDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dt_PoDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dt_PoDate.Location = new System.Drawing.Point(123, 56);
            this.dt_PoDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_PoDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_PoDate.MinimumSize = new System.Drawing.Size(243, 26);
            this.dt_PoDate.Name = "dt_PoDate";
            this.dt_PoDate.Size = new System.Drawing.Size(243, 26);
            this.dt_PoDate.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcPoMasterList,
            this.lcPoDetailList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPoDate,
            this.emptySpaceItem2,
            this.lcPoId,
            this.lcPoCustomer,
            this.layoutControlItem1});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1044, 88);
            this.lcCondition.Text = "조회조건";
            // 
            // lcPoDate
            // 
            this.lcPoDate.Control = this.dt_PoDate;
            this.lcPoDate.Location = new System.Drawing.Point(0, 0);
            this.lcPoDate.Name = "lcPoDate";
            this.lcPoDate.Size = new System.Drawing.Size(341, 33);
            this.lcPoDate.Text = "발주일";
            this.lcPoDate.TextSize = new System.Drawing.Size(88, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(724, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(172, 33);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcPoId
            // 
            this.lcPoId.Control = this.lup_PoId;
            this.lcPoId.Location = new System.Drawing.Point(532, 0);
            this.lcPoId.Name = "lcPoId";
            this.lcPoId.Size = new System.Drawing.Size(192, 33);
            this.lcPoId.Text = "발주자";
            this.lcPoId.TextSize = new System.Drawing.Size(88, 18);
            // 
            // lcPoCustomer
            // 
            this.lcPoCustomer.Control = this.lup_PoCustomer;
            this.lcPoCustomer.Location = new System.Drawing.Point(341, 0);
            this.lcPoCustomer.Name = "lcPoCustomer";
            this.lcPoCustomer.Size = new System.Drawing.Size(191, 33);
            this.lcPoCustomer.TextSize = new System.Drawing.Size(88, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_PurchaseStatus;
            this.layoutControlItem1.Location = new System.Drawing.Point(896, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(118, 33);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcPoMasterList
            // 
            this.lcPoMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcPoMasterList.Location = new System.Drawing.Point(0, 88);
            this.lcPoMasterList.Name = "lcPoMasterList";
            this.lcPoMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcPoMasterList.Size = new System.Drawing.Size(1044, 235);
            this.lcPoMasterList.Text = "발주등록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1014, 180);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcPoDetailList
            // 
            this.lcPoDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcPoDetailList.Location = new System.Drawing.Point(0, 329);
            this.lcPoDetailList.Name = "lcPoDetailList";
            this.lcPoDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcPoDetailList.Size = new System.Drawing.Size(1044, 298);
            this.lcPoDetailList.Text = "발주상세";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1014, 243);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 323);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1044, 6);
            // 
            // XFPUR1100
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFPUR1100";
            this.Text = "XFPUR1100";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_PoCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_PoId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dt_PoDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcPoDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcPoMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcPoDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_PoId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcPoId;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private Service.Controls.SearchLookUpEditEx lup_PoCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcPoCustomer;
        private DevExpress.XtraEditors.SimpleButton btn_PurchaseStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}