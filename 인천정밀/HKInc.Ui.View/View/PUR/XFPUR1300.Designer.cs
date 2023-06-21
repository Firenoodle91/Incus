namespace HKInc.Ui.View.View.PUR
{
    partial class XFPUR1300
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFPUR1300));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_PurchaseStatus = new DevExpress.XtraEditors.SimpleButton();
            this.lup_OutId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.dt_OutDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcOutDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcOutId = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_OutId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDetailList)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.btn_PurchaseStatus);
            this.layoutControl1.Controls.Add(this.lup_OutId);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.dt_OutDate);
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
            // lup_OutId
            // 
            this.lup_OutId.Constraint = null;
            this.lup_OutId.DataSource = null;
            this.lup_OutId.DisplayMember = "";
            this.lup_OutId.isImeModeDisable = false;
            this.lup_OutId.isRequired = false;
            this.lup_OutId.Location = new System.Drawing.Point(366, 56);
            this.lup_OutId.Name = "lup_OutId";
            this.lup_OutId.NullText = "";
            this.lup_OutId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_OutId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_OutId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_OutId.Properties.NullText = "";
            this.lup_OutId.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_OutId.Size = new System.Drawing.Size(160, 24);
            this.lup_OutId.StyleController = this.layoutControl1;
            this.lup_OutId.TabIndex = 2;
            this.lup_OutId.Value_1 = null;
            this.lup_OutId.ValueMember = "";
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
            this.gridEx2.TabIndex = 0;
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
            // dt_OutDate
            // 
            this.dt_OutDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_OutDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_OutDate.Appearance.Options.UseBackColor = true;
            this.dt_OutDate.Appearance.Options.UseFont = true;
            this.dt_OutDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dt_OutDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dt_OutDate.Location = new System.Drawing.Point(74, 56);
            this.dt_OutDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_OutDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_OutDate.MinimumSize = new System.Drawing.Size(243, 26);
            this.dt_OutDate.Name = "dt_OutDate";
            this.dt_OutDate.Size = new System.Drawing.Size(243, 26);
            this.dt_OutDate.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcOutMasterList,
            this.lcOutDetailList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcOutDate,
            this.emptySpaceItem2,
            this.lcOutId,
            this.layoutControlItem1});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1044, 88);
            this.lcCondition.Text = "조회조건";
            // 
            // lcOutDate
            // 
            this.lcOutDate.Control = this.dt_OutDate;
            this.lcOutDate.Location = new System.Drawing.Point(0, 0);
            this.lcOutDate.Name = "lcOutDate";
            this.lcOutDate.Size = new System.Drawing.Size(292, 33);
            this.lcOutDate.Text = "출고일";
            this.lcOutDate.TextSize = new System.Drawing.Size(39, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(501, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(395, 33);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcOutId
            // 
            this.lcOutId.Control = this.lup_OutId;
            this.lcOutId.Location = new System.Drawing.Point(292, 0);
            this.lcOutId.Name = "lcOutId";
            this.lcOutId.Size = new System.Drawing.Size(209, 33);
            this.lcOutId.Text = "출고자";
            this.lcOutId.TextSize = new System.Drawing.Size(39, 18);
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
            // lcOutMasterList
            // 
            this.lcOutMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcOutMasterList.Location = new System.Drawing.Point(0, 88);
            this.lcOutMasterList.Name = "lcOutMasterList";
            this.lcOutMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcOutMasterList.Size = new System.Drawing.Size(1044, 235);
            this.lcOutMasterList.Text = "출고목록";
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
            // lcOutDetailList
            // 
            this.lcOutDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcOutDetailList.Location = new System.Drawing.Point(0, 329);
            this.lcOutDetailList.Name = "lcOutDetailList";
            this.lcOutDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcOutDetailList.Size = new System.Drawing.Size(1044, 298);
            this.lcOutDetailList.Text = "출고상세목록";
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
            // XFPUR1300
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFPUR1300";
            this.Text = "XFPUR1300";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_OutId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dt_OutDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcOutDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcOutMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcOutDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_OutId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcOutId;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.SimpleButton btn_PurchaseStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}