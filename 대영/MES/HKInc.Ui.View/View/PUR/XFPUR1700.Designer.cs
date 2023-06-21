namespace HKInc.Ui.View.View.PUR
{
    partial class XFPUR1700
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFPUR1700));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dt_DisposalDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.lup_DisposalId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_InCustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcInCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDisposalId = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDisposalDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReturnMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReturnDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_DisposalId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_InCustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDisposalId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDisposalDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReturnMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReturnDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dt_DisposalDate);
            this.layoutControl1.Controls.Add(this.lup_DisposalId);
            this.layoutControl1.Controls.Add(this.lup_InCustomerCode);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dt_DisposalDate
            // 
            this.dt_DisposalDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_DisposalDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_DisposalDate.Appearance.Options.UseBackColor = true;
            this.dt_DisposalDate.Appearance.Options.UseFont = true;
            this.dt_DisposalDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dt_DisposalDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dt_DisposalDate.Location = new System.Drawing.Point(120, 50);
            this.dt_DisposalDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_DisposalDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_DisposalDate.MinimumSize = new System.Drawing.Size(243, 26);
            this.dt_DisposalDate.Name = "dt_DisposalDate";
            this.dt_DisposalDate.Size = new System.Drawing.Size(243, 26);
            this.dt_DisposalDate.TabIndex = 0;
            // 
            // lup_DisposalId
            // 
            this.lup_DisposalId.Constraint = null;
            this.lup_DisposalId.DataSource = null;
            this.lup_DisposalId.DisplayMember = "";
            this.lup_DisposalId.isImeModeDisable = false;
            this.lup_DisposalId.isRequired = false;
            this.lup_DisposalId.Location = new System.Drawing.Point(736, 50);
            this.lup_DisposalId.Name = "lup_DisposalId";
            this.lup_DisposalId.NullText = "";
            this.lup_DisposalId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_DisposalId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_DisposalId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_DisposalId.Properties.NullText = "";
            this.lup_DisposalId.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_DisposalId.Size = new System.Drawing.Size(87, 24);
            this.lup_DisposalId.StyleController = this.layoutControl1;
            this.lup_DisposalId.TabIndex = 2;
            this.lup_DisposalId.Value_1 = null;
            this.lup_DisposalId.ValueMember = "";
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
            this.lup_InCustomerCode.Location = new System.Drawing.Point(463, 50);
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
            this.gridEx2.Location = new System.Drawing.Point(24, 424);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(1022, 219);
            this.gridEx2.TabIndex = 5;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 130);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 228);
            this.gridEx1.TabIndex = 4;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcReturnMasterList,
            this.lcReturnDetailList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcInCustomer,
            this.lcDisposalId,
            this.lcDisposalDate,
            this.emptySpaceItem1});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1050, 80);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(803, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(223, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcInCustomer
            // 
            this.lcInCustomer.Control = this.lup_InCustomerCode;
            this.lcInCustomer.Location = new System.Drawing.Point(343, 0);
            this.lcInCustomer.Name = "lcInCustomer";
            this.lcInCustomer.Size = new System.Drawing.Size(228, 30);
            this.lcInCustomer.Text = "거래처";
            this.lcInCustomer.TextSize = new System.Drawing.Size(92, 18);
            // 
            // lcDisposalId
            // 
            this.lcDisposalId.Control = this.lup_DisposalId;
            this.lcDisposalId.Location = new System.Drawing.Point(616, 0);
            this.lcDisposalId.Name = "lcDisposalId";
            this.lcDisposalId.Size = new System.Drawing.Size(187, 30);
            this.lcDisposalId.TextSize = new System.Drawing.Size(92, 18);
            // 
            // lcDisposalDate
            // 
            this.lcDisposalDate.Control = this.dt_DisposalDate;
            this.lcDisposalDate.Location = new System.Drawing.Point(0, 0);
            this.lcDisposalDate.Name = "lcDisposalDate";
            this.lcDisposalDate.Size = new System.Drawing.Size(343, 30);
            this.lcDisposalDate.TextSize = new System.Drawing.Size(92, 18);
            // 
            // lcReturnMasterList
            // 
            this.lcReturnMasterList.CustomizationFormText = "반품목록";
            this.lcReturnMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcReturnMasterList.Location = new System.Drawing.Point(0, 80);
            this.lcReturnMasterList.Name = "lcReturnMasterList";
            this.lcReturnMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcReturnMasterList.Size = new System.Drawing.Size(1050, 282);
            this.lcReturnMasterList.Text = "반품목록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1026, 232);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcReturnDetailList
            // 
            this.lcReturnDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcReturnDetailList.Location = new System.Drawing.Point(0, 374);
            this.lcReturnDetailList.Name = "lcReturnDetailList";
            this.lcReturnDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcReturnDetailList.Size = new System.Drawing.Size(1050, 273);
            this.lcReturnDetailList.Text = "반품상세목록";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1026, 223);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 362);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1050, 12);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(571, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(45, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XFPUR1700
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFPUR1700";
            this.Text = "XFPUR1200";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_DisposalId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_InCustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDisposalId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDisposalDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReturnMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReturnDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dt_DisposalDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcReturnMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcReturnDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_InCustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcInCustomer;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private Service.Controls.SearchLookUpEditEx lup_DisposalId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcDisposalId;
        private DevExpress.XtraLayout.LayoutControlItem lcDisposalDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}