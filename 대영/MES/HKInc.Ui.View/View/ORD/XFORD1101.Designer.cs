namespace HKInc.Ui.View.View.ORD
{
    partial class XFORD1101
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFORD1101));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_Customer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_OutDatePlan = new HKInc.Service.Controls.DateEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcOutDatePlan = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustomerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcShipmentDayPlanList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Customer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OutDatePlan.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OutDatePlan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDatePlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShipmentDayPlanList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_Customer);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.dt_OutDatePlan);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 141);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 481);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_Customer
            // 
            this.lup_Customer.Constraint = null;
            this.lup_Customer.DataSource = null;
            this.lup_Customer.DisplayMember = "";
            this.lup_Customer.isImeModeDisable = false;
            this.lup_Customer.isRequired = false;
            this.lup_Customer.Location = new System.Drawing.Point(598, 56);
            this.lup_Customer.Name = "lup_Customer";
            this.lup_Customer.NullText = "";
            this.lup_Customer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Customer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Customer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Customer.Properties.NullText = "";
            this.lup_Customer.Properties.PopupView = this.searchLookUpEditEx2View;
            this.lup_Customer.Size = new System.Drawing.Size(118, 24);
            this.lup_Customer.StyleController = this.layoutControl1;
            this.lup_Customer.TabIndex = 2;
            this.lup_Customer.Value_1 = null;
            this.lup_Customer.ValueMember = "";
            // 
            // searchLookUpEditEx2View
            // 
            this.searchLookUpEditEx2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx2View.Name = "searchLookUpEditEx2View";
            this.searchLookUpEditEx2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx2View.OptionsView.ShowGroupPanel = false;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(356, 56);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Item.Size = new System.Drawing.Size(121, 24);
            this.lup_Item.StyleController = this.layoutControl1;
            this.lup_Item.TabIndex = 1;
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
            // dt_OutDatePlan
            // 
            this.dt_OutDatePlan.EditValue = null;
            this.dt_OutDatePlan.Location = new System.Drawing.Point(146, 56);
            this.dt_OutDatePlan.Name = "dt_OutDatePlan";
            this.dt_OutDatePlan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_OutDatePlan.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_OutDatePlan.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dt_OutDatePlan.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_OutDatePlan.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dt_OutDatePlan.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_OutDatePlan.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dt_OutDatePlan.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_OutDatePlan.Size = new System.Drawing.Size(89, 24);
            this.dt_OutDatePlan.StyleController = this.layoutControl1;
            this.dt_OutDatePlan.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcShipmentDayPlanList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcOutDatePlan,
            this.emptySpaceItem2,
            this.lcItem,
            this.lcCustomerName});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1044, 85);
            // 
            // lcOutDatePlan
            // 
            this.lcOutDatePlan.Control = this.dt_OutDatePlan;
            this.lcOutDatePlan.Location = new System.Drawing.Point(0, 0);
            this.lcOutDatePlan.Name = "lcOutDatePlan";
            this.lcOutDatePlan.Size = new System.Drawing.Size(210, 30);
            this.lcOutDatePlan.TextSize = new System.Drawing.Size(111, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(691, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(323, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(210, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(242, 30);
            this.lcItem.TextSize = new System.Drawing.Size(111, 18);
            // 
            // lcCustomerName
            // 
            this.lcCustomerName.Control = this.lup_Customer;
            this.lcCustomerName.Location = new System.Drawing.Point(452, 0);
            this.lcCustomerName.Name = "lcCustomerName";
            this.lcCustomerName.Size = new System.Drawing.Size(239, 30);
            this.lcCustomerName.TextSize = new System.Drawing.Size(111, 18);
            // 
            // lcShipmentDayPlanList
            // 
            this.lcShipmentDayPlanList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcShipmentDayPlanList.Location = new System.Drawing.Point(0, 85);
            this.lcShipmentDayPlanList.Name = "lcShipmentDayPlanList";
            this.lcShipmentDayPlanList.OptionsItemText.TextToControlDistance = 4;
            this.lcShipmentDayPlanList.Size = new System.Drawing.Size(1044, 542);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridEx1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1014, 487);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // XFORD1101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFORD1101";
            this.Text = "XFORD1101";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Customer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OutDatePlan.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OutDatePlan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDatePlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShipmentDayPlanList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.SearchLookUpEditEx lup_Customer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx2View;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private Service.Controls.DateEditEx dt_OutDatePlan;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcOutDatePlan;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerName;
        private DevExpress.XtraLayout.LayoutControlGroup lcShipmentDayPlanList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}