namespace HKInc.Ui.View.SELECT_POPUP
{
    partial class XSFPUR1700
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSFPUR1700));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_InCustomer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_InDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_PoId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcInDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPoId = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcInCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPoMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tx_InNo = new DevExpress.XtraEditors.TextEdit();
            this.lcInNo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_InCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_PoId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_InNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInNo)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_InCustomer);
            this.layoutControl1.Controls.Add(this.dt_InDate);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_PoId);
            this.layoutControl1.Controls.Add(this.tx_InNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPoId});
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1056, 594);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_InCustomer
            // 
            this.lup_InCustomer.Constraint = null;
            this.lup_InCustomer.DataSource = null;
            this.lup_InCustomer.DisplayMember = "";
            this.lup_InCustomer.isImeModeDisable = false;
            this.lup_InCustomer.isRequired = false;
            this.lup_InCustomer.Location = new System.Drawing.Point(408, 50);
            this.lup_InCustomer.Name = "lup_InCustomer";
            this.lup_InCustomer.NullText = "";
            this.lup_InCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_InCustomer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_InCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_InCustomer.Properties.NullText = "";
            this.lup_InCustomer.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_InCustomer.Size = new System.Drawing.Size(105, 24);
            this.lup_InCustomer.StyleController = this.layoutControl1;
            this.lup_InCustomer.TabIndex = 1;
            this.lup_InCustomer.Value_1 = null;
            this.lup_InCustomer.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dt_InDate
            // 
            this.dt_InDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_InDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_InDate.Appearance.Options.UseBackColor = true;
            this.dt_InDate.Appearance.Options.UseFont = true;
            this.dt_InDate.EditFrValue = new System.DateTime(2020, 1, 24, 0, 0, 0, 0);
            this.dt_InDate.EditToValue = new System.DateTime(2020, 2, 24, 23, 59, 59, 990);
            this.dt_InDate.Location = new System.Drawing.Point(114, 50);
            this.dt_InDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_InDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_InDate.MinimumSize = new System.Drawing.Size(200, 21);
            this.dt_InDate.Name = "dt_InDate";
            this.dt_InDate.Size = new System.Drawing.Size(200, 21);
            this.dt_InDate.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 128);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 442);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_PoId
            // 
            this.lup_PoId.Constraint = null;
            this.lup_PoId.DataSource = null;
            this.lup_PoId.DisplayMember = "";
            this.lup_PoId.isImeModeDisable = false;
            this.lup_PoId.isRequired = false;
            this.lup_PoId.Location = new System.Drawing.Point(610, 50);
            this.lup_PoId.Name = "lup_PoId";
            this.lup_PoId.NullText = "";
            this.lup_PoId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_PoId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_PoId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_PoId.Properties.NullText = "";
            this.lup_PoId.Properties.PopupView = this.gridView1;
            this.lup_PoId.Size = new System.Drawing.Size(102, 24);
            this.lup_PoId.StyleController = this.layoutControl1;
            this.lup_PoId.TabIndex = 2;
            this.lup_PoId.Value_1 = null;
            this.lup_PoId.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcPoMasterList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1056, 594);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcInDate,
            this.lcInCustomer,
            this.lcInNo});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1036, 78);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(752, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(260, 28);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcInDate
            // 
            this.lcInDate.Control = this.dt_InDate;
            this.lcInDate.CustomizationFormText = "입고일";
            this.lcInDate.Location = new System.Drawing.Point(0, 0);
            this.lcInDate.Name = "lcInDate";
            this.lcInDate.Size = new System.Drawing.Size(294, 28);
            this.lcInDate.Text = "입고일";
            this.lcInDate.TextSize = new System.Drawing.Size(86, 18);
            // 
            // lcPoId
            // 
            this.lcPoId.Control = this.lup_PoId;
            this.lcPoId.CustomizationFormText = "발주자";
            this.lcPoId.Location = new System.Drawing.Point(494, 0);
            this.lcPoId.Name = "lcPoId";
            this.lcPoId.Size = new System.Drawing.Size(198, 28);
            this.lcPoId.Text = "발주자";
            this.lcPoId.TextSize = new System.Drawing.Size(88, 18);
            // 
            // lcInCustomer
            // 
            this.lcInCustomer.Control = this.lup_InCustomer;
            this.lcInCustomer.Location = new System.Drawing.Point(294, 0);
            this.lcInCustomer.Name = "lcInCustomer";
            this.lcInCustomer.Size = new System.Drawing.Size(199, 28);
            this.lcInCustomer.TextSize = new System.Drawing.Size(86, 18);
            // 
            // lcPoMasterList
            // 
            this.lcPoMasterList.CustomizationFormText = "발주마스터목록";
            this.lcPoMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcPoMasterList.Location = new System.Drawing.Point(0, 78);
            this.lcPoMasterList.Name = "lcPoMasterList";
            this.lcPoMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcPoMasterList.Size = new System.Drawing.Size(1036, 496);
            this.lcPoMasterList.Text = "발주마스터목록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1012, 446);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // tx_InNo
            // 
            this.tx_InNo.Location = new System.Drawing.Point(607, 50);
            this.tx_InNo.Name = "tx_InNo";
            this.tx_InNo.Size = new System.Drawing.Size(165, 24);
            this.tx_InNo.StyleController = this.layoutControl1;
            this.tx_InNo.TabIndex = 0;
            // 
            // lcInNo
            // 
            this.lcInNo.Control = this.tx_InNo;
            this.lcInNo.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcInNo.CustomizationFormText = "lcPoNo";
            this.lcInNo.Location = new System.Drawing.Point(493, 0);
            this.lcInNo.Name = "lcInNo";
            this.lcInNo.Size = new System.Drawing.Size(259, 28);
            this.lcInNo.Text = "lcInNo";
            this.lcInNo.TextSize = new System.Drawing.Size(86, 18);
            // 
            // XSFPUR1700
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 653);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XSFPUR1700";
            this.Text = "XSFPUR1100_NEW";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_InCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_PoId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPoMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_InNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcPoMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private Service.Controls.DatePeriodEditEx dt_InDate;
        private DevExpress.XtraLayout.LayoutControlItem lcInDate;
        private Service.Controls.SearchLookUpEditEx lup_PoId;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcPoId;
        private Service.Controls.SearchLookUpEditEx lup_InCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcInCustomer;
        private DevExpress.XtraEditors.TextEdit tx_InNo;
        private DevExpress.XtraLayout.LayoutControlItem lcInNo;
    }
}