namespace HKInc.Ui.View.View.REPORT
{
    partial class XRREP5004
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRREP5004));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dt_OrderDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.lup_CustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOrderDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcXRREP5004master = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.lup_ItemCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView11 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lcItemCode = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcXRREP5004master)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCode)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dt_OrderDate);
            this.layoutControl1.Controls.Add(this.lup_CustomerCode);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_ItemCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 518);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dt_OrderDate
            // 
            this.dt_OrderDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_OrderDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_OrderDate.Appearance.Options.UseBackColor = true;
            this.dt_OrderDate.Appearance.Options.UseFont = true;
            this.dt_OrderDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dt_OrderDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dt_OrderDate.Location = new System.Drawing.Point(88, 45);
            this.dt_OrderDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dt_OrderDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_OrderDate.MinimumSize = new System.Drawing.Size(213, 22);
            this.dt_OrderDate.Name = "dt_OrderDate";
            this.dt_OrderDate.Size = new System.Drawing.Size(213, 22);
            this.dt_OrderDate.TabIndex = 0;
            // 
            // lup_CustomerCode
            // 
            this.lup_CustomerCode.Constraint = null;
            this.lup_CustomerCode.DataSource = null;
            this.lup_CustomerCode.DisplayMember = "";
            this.lup_CustomerCode.isImeModeDisable = false;
            this.lup_CustomerCode.isRequired = false;
            this.lup_CustomerCode.Location = new System.Drawing.Point(369, 45);
            this.lup_CustomerCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_CustomerCode.Name = "lup_CustomerCode";
            this.lup_CustomerCode.NullText = "";
            this.lup_CustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_CustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_CustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_CustomerCode.Properties.NullText = "";
            this.lup_CustomerCode.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_CustomerCode.Size = new System.Drawing.Size(187, 20);
            this.lup_CustomerCode.StyleController = this.layoutControl1;
            this.lup_CustomerCode.TabIndex = 1;
            this.lup_CustomerCode.Value_1 = null;
            this.lup_CustomerCode.ValueMember = "";
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
            this.gridEx2.Location = new System.Drawing.Point(24, 342);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx2.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(888, 152);
            this.gridEx2.TabIndex = 3;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 116);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(888, 167);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcXRREP5004master,
            this.lcOutMasterList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcCustomer,
            this.lcOrderDate,
            this.lcItemCode});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(916, 71);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(782, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(110, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcCustomer
            // 
            this.lcCustomer.Control = this.lup_CustomerCode;
            this.lcCustomer.Location = new System.Drawing.Point(281, 0);
            this.lcCustomer.Name = "lcCustomer";
            this.lcCustomer.Size = new System.Drawing.Size(255, 26);
            this.lcCustomer.TextSize = new System.Drawing.Size(60, 14);
            // 
            // lcOrderDate
            // 
            this.lcOrderDate.Control = this.dt_OrderDate;
            this.lcOrderDate.Location = new System.Drawing.Point(0, 0);
            this.lcOrderDate.Name = "lcOrderDate";
            this.lcOrderDate.Size = new System.Drawing.Size(281, 26);
            this.lcOrderDate.Text = "납품예정일";
            this.lcOrderDate.TextSize = new System.Drawing.Size(60, 14);
            // 
            // lcXRREP5004master
            // 
            this.lcXRREP5004master.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcXRREP5004master.Location = new System.Drawing.Point(0, 71);
            this.lcXRREP5004master.Name = "lcXRREP5004master";
            this.lcXRREP5004master.OptionsItemText.TextToControlDistance = 4;
            this.lcXRREP5004master.Size = new System.Drawing.Size(916, 216);
            this.lcXRREP5004master.Text = "영업계획 및 매출현황";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(892, 171);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcOutMasterList
            // 
            this.lcOutMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcOutMasterList.Location = new System.Drawing.Point(0, 297);
            this.lcOutMasterList.Name = "lcOutMasterList";
            this.lcOutMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcOutMasterList.Size = new System.Drawing.Size(916, 201);
            this.lcOutMasterList.Text = "출고현황";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(892, 156);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 287);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(916, 10);
            // 
            // lup_ItemCode
            // 
            this.lup_ItemCode.Constraint = null;
            this.lup_ItemCode.DataSource = null;
            this.lup_ItemCode.DisplayMember = "";
            this.lup_ItemCode.isImeModeDisable = false;
            this.lup_ItemCode.isRequired = false;
            this.lup_ItemCode.Location = new System.Drawing.Point(624, 45);
            this.lup_ItemCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_ItemCode.Name = "lup_ItemCode";
            this.lup_ItemCode.NullText = "";
            this.lup_ItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_ItemCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_ItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_ItemCode.Properties.NullText = "";
            this.lup_ItemCode.Properties.PopupView = this.gridView11;
            this.lup_ItemCode.Size = new System.Drawing.Size(178, 20);
            this.lup_ItemCode.StyleController = this.layoutControl1;
            this.lup_ItemCode.TabIndex = 0;
            this.lup_ItemCode.Value_1 = null;
            this.lup_ItemCode.ValueMember = "";
            // 
            // gridView11
            // 
            this.gridView11.DetailHeight = 272;
            this.gridView11.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView11.Name = "gridView11";
            this.gridView11.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView11.OptionsView.ShowGroupPanel = false;
            // 
            // lcItemCode
            // 
            this.lcItemCode.Control = this.lup_ItemCode;
            this.lcItemCode.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcItemCode.CustomizationFormText = "lcItem";
            this.lcItemCode.Location = new System.Drawing.Point(536, 0);
            this.lcItemCode.Name = "lcItemCode";
            this.lcItemCode.Size = new System.Drawing.Size(246, 26);
            this.lcItemCode.Text = "lcItem";
            this.lcItemCode.TextSize = new System.Drawing.Size(60, 14);
            // 
            // XRREP5004
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XRREP5004";
            this.Text = "XRREP5004";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcXRREP5004master)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dt_OrderDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcXRREP5004master;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcOutMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_CustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomer;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlItem lcOrderDate;
        private Service.Controls.SearchLookUpEditEx lup_ItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView11;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCode;
    }
}