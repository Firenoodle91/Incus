namespace HKInc.Ui.View.View.REPORT
{
    partial class XRREP5000
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRREP5000));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_Manager = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_CustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_BillDate = new HKInc.Service.Controls.DateEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcBillDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcManagerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSalesList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Manager.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_BillDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_BillDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManagerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSalesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_Manager);
            this.layoutControl1.Controls.Add(this.lup_CustomerCode);
            this.layoutControl1.Controls.Add(this.dt_BillDate);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1107, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_Manager
            // 
            this.lup_Manager.Constraint = null;
            this.lup_Manager.DataSource = null;
            this.lup_Manager.DisplayMember = "";
            this.lup_Manager.isImeModeDisable = false;
            this.lup_Manager.isRequired = false;
            this.lup_Manager.Location = new System.Drawing.Point(315, 50);
            this.lup_Manager.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lup_Manager.Name = "lup_Manager";
            this.lup_Manager.NullText = "";
            this.lup_Manager.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Manager.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Manager.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Manager.Properties.NullText = "";
            this.lup_Manager.Properties.PopupView = this.gridView1;
            this.lup_Manager.Size = new System.Drawing.Size(62, 24);
            this.lup_Manager.StyleController = this.layoutControl1;
            this.lup_Manager.TabIndex = 1;
            this.lup_Manager.Value_1 = null;
            this.lup_Manager.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lup_CustomerCode
            // 
            this.lup_CustomerCode.Constraint = null;
            this.lup_CustomerCode.DataSource = null;
            this.lup_CustomerCode.DisplayMember = "";
            this.lup_CustomerCode.isImeModeDisable = false;
            this.lup_CustomerCode.isRequired = false;
            this.lup_CustomerCode.Location = new System.Drawing.Point(490, 50);
            this.lup_CustomerCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lup_CustomerCode.Name = "lup_CustomerCode";
            this.lup_CustomerCode.NullText = "";
            this.lup_CustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_CustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_CustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_CustomerCode.Properties.NullText = "";
            this.lup_CustomerCode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_CustomerCode.Size = new System.Drawing.Size(62, 24);
            this.lup_CustomerCode.StyleController = this.layoutControl1;
            this.lup_CustomerCode.TabIndex = 2;
            this.lup_CustomerCode.Value_1 = null;
            this.lup_CustomerCode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dt_BillDate
            // 
            this.dt_BillDate.EditValue = null;
            this.dt_BillDate.Location = new System.Drawing.Point(133, 50);
            this.dt_BillDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dt_BillDate.Name = "dt_BillDate";
            this.dt_BillDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_BillDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_BillDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dt_BillDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_BillDate.Properties.EditFormat.FormatString = "yyyy/MM";
            this.dt_BillDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_BillDate.Properties.Mask.EditMask = "yyyy/MM";
            this.dt_BillDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_BillDate.Size = new System.Drawing.Size(69, 24);
            this.dt_BillDate.StyleController = this.layoutControl1;
            this.dt_BillDate.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 128);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1059, 515);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcSalesList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1107, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcBillDate,
            this.lcCustomer,
            this.lcManagerName});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1087, 78);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(532, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(531, 28);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcBillDate
            // 
            this.lcBillDate.Control = this.dt_BillDate;
            this.lcBillDate.Location = new System.Drawing.Point(0, 0);
            this.lcBillDate.Name = "lcBillDate";
            this.lcBillDate.Size = new System.Drawing.Size(182, 28);
            this.lcBillDate.TextSize = new System.Drawing.Size(105, 18);
            // 
            // lcCustomer
            // 
            this.lcCustomer.Control = this.lup_CustomerCode;
            this.lcCustomer.Location = new System.Drawing.Point(357, 0);
            this.lcCustomer.Name = "lcCustomer";
            this.lcCustomer.Size = new System.Drawing.Size(175, 28);
            this.lcCustomer.TextSize = new System.Drawing.Size(105, 18);
            // 
            // lcManagerName
            // 
            this.lcManagerName.Control = this.lup_Manager;
            this.lcManagerName.Location = new System.Drawing.Point(182, 0);
            this.lcManagerName.Name = "lcManagerName";
            this.lcManagerName.Size = new System.Drawing.Size(175, 28);
            this.lcManagerName.TextSize = new System.Drawing.Size(105, 18);
            // 
            // lcSalesList
            // 
            this.lcSalesList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcSalesList.Location = new System.Drawing.Point(0, 78);
            this.lcSalesList.Name = "lcSalesList";
            this.lcSalesList.OptionsItemText.TextToControlDistance = 4;
            this.lcSalesList.Size = new System.Drawing.Size(1087, 569);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1063, 519);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // XRREP5000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XRREP5000";
            this.Text = "XRREP5000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Manager.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_BillDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_BillDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManagerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSalesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcSalesList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_CustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private Service.Controls.DateEditEx dt_BillDate;
        private DevExpress.XtraLayout.LayoutControlItem lcBillDate;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomer;
        private Service.Controls.SearchLookUpEditEx lup_Manager;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcManagerName;
    }
}