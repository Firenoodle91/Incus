namespace HKInc.Ui.View.View.REPORT
{
    partial class XRREP5009
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRREP5009));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_CustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_ItemCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_YYYY = new HKInc.Service.Controls.DateEditEx();
            this.dt_MM = new HKInc.Service.Controls.DateEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcYear = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPlanMonth = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcXRREP5005master = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.lcInDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcResultMonth = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_YYYY.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_YYYY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_MM.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_MM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPlanMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcXRREP5005master)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcResultMonth)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_CustomerCode);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_ItemCode);
            this.layoutControl1.Controls.Add(this.dt_YYYY);
            this.layoutControl1.Controls.Add(this.dt_MM);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 518);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_CustomerCode
            // 
            this.lup_CustomerCode.Constraint = null;
            this.lup_CustomerCode.DataSource = null;
            this.lup_CustomerCode.DisplayMember = "";
            this.lup_CustomerCode.isImeModeDisable = false;
            this.lup_CustomerCode.isRequired = false;
            this.lup_CustomerCode.Location = new System.Drawing.Point(454, 45);
            this.lup_CustomerCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_CustomerCode.Name = "lup_CustomerCode";
            this.lup_CustomerCode.NullText = "";
            this.lup_CustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_CustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_CustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_CustomerCode.Properties.NullText = "";
            this.lup_CustomerCode.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_CustomerCode.Size = new System.Drawing.Size(144, 20);
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
            this.gridEx2.Location = new System.Drawing.Point(24, 483);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx2.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(888, 1);
            this.gridEx2.TabIndex = 3;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 114);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(888, 320);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_ItemCode
            // 
            this.lup_ItemCode.Constraint = null;
            this.lup_ItemCode.DataSource = null;
            this.lup_ItemCode.DisplayMember = "";
            this.lup_ItemCode.isImeModeDisable = false;
            this.lup_ItemCode.isRequired = false;
            this.lup_ItemCode.Location = new System.Drawing.Point(666, 45);
            this.lup_ItemCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_ItemCode.Name = "lup_ItemCode";
            this.lup_ItemCode.NullText = "";
            this.lup_ItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_ItemCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_ItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_ItemCode.Properties.NullText = "";
            this.lup_ItemCode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_ItemCode.Size = new System.Drawing.Size(246, 20);
            this.lup_ItemCode.StyleController = this.layoutControl1;
            this.lup_ItemCode.TabIndex = 2;
            this.lup_ItemCode.Value_1 = null;
            this.lup_ItemCode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.DetailHeight = 272;
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dt_YYYY
            // 
            this.dt_YYYY.EditValue = null;
            this.dt_YYYY.Location = new System.Drawing.Point(88, 45);
            this.dt_YYYY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dt_YYYY.Name = "dt_YYYY";
            this.dt_YYYY.Properties.Appearance.Options.UseTextOptions = true;
            this.dt_YYYY.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dt_YYYY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_YYYY.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_YYYY.Properties.DisplayFormat.FormatString = "yyyy/MM";
            this.dt_YYYY.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_YYYY.Properties.EditFormat.FormatString = "yyyy/MM";
            this.dt_YYYY.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_YYYY.Properties.Mask.EditMask = "yyyy";
            this.dt_YYYY.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_YYYY.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dt_YYYY.Size = new System.Drawing.Size(109, 20);
            this.dt_YYYY.StyleController = this.layoutControl1;
            this.dt_YYYY.TabIndex = 0;
            // 
            // dt_MM
            // 
            this.dt_MM.EditValue = null;
            this.dt_MM.Location = new System.Drawing.Point(265, 45);
            this.dt_MM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dt_MM.Name = "dt_MM";
            this.dt_MM.Properties.Appearance.Options.UseTextOptions = true;
            this.dt_MM.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dt_MM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_MM.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_MM.Properties.DisplayFormat.FormatString = "yyyy/MM";
            this.dt_MM.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_MM.Properties.EditFormat.FormatString = "yyyy/MM";
            this.dt_MM.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_MM.Properties.Mask.EditMask = "MM";
            this.dt_MM.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_MM.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dt_MM.Size = new System.Drawing.Size(121, 20);
            this.dt_MM.StyleController = this.layoutControl1;
            this.dt_MM.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcXRREP5005master,
            this.splitterItem1,
            this.lcInDetailList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCustomer,
            this.lcItem,
            this.lcYear,
            this.lcPlanMonth});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(916, 69);
            this.lcCondition.Text = "조회조건";
            // 
            // lcCustomer
            // 
            this.lcCustomer.Control = this.lup_CustomerCode;
            this.lcCustomer.Location = new System.Drawing.Point(366, 0);
            this.lcCustomer.Name = "lcCustomer";
            this.lcCustomer.Size = new System.Drawing.Size(212, 24);
            this.lcCustomer.TextSize = new System.Drawing.Size(60, 14);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_ItemCode;
            this.lcItem.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcItem.CustomizationFormText = "lcItem";
            this.lcItem.Location = new System.Drawing.Point(578, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(314, 24);
            this.lcItem.TextSize = new System.Drawing.Size(60, 14);
            // 
            // lcYear
            // 
            this.lcYear.Control = this.dt_YYYY;
            this.lcYear.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcYear.CustomizationFormText = "DelivMonth";
            this.lcYear.Location = new System.Drawing.Point(0, 0);
            this.lcYear.Name = "lcYear";
            this.lcYear.Size = new System.Drawing.Size(177, 24);
            this.lcYear.Text = "년도";
            this.lcYear.TextSize = new System.Drawing.Size(60, 14);
            // 
            // lcPlanMonth
            // 
            this.lcPlanMonth.Control = this.dt_MM;
            this.lcPlanMonth.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcPlanMonth.CustomizationFormText = "DelivMonth";
            this.lcPlanMonth.Location = new System.Drawing.Point(177, 0);
            this.lcPlanMonth.Name = "lcPlanMonth";
            this.lcPlanMonth.Size = new System.Drawing.Size(189, 24);
            this.lcPlanMonth.Text = "월";
            this.lcPlanMonth.TextSize = new System.Drawing.Size(60, 14);
            // 
            // lcXRREP5005master
            // 
            this.lcXRREP5005master.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcXRREP5005master.Location = new System.Drawing.Point(0, 69);
            this.lcXRREP5005master.Name = "lcXRREP5005master";
            this.lcXRREP5005master.OptionsItemText.TextToControlDistance = 4;
            this.lcXRREP5005master.Size = new System.Drawing.Size(916, 369);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(892, 324);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 488);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(916, 10);
            // 
            // lcInDetailList
            // 
            this.lcInDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcInDetailList.Location = new System.Drawing.Point(0, 438);
            this.lcInDetailList.Name = "lcInDetailList";
            this.lcInDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcInDetailList.Size = new System.Drawing.Size(916, 50);
            this.lcInDetailList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(892, 5);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // lcResultMonth
            // 
            this.lcResultMonth.Control = this.dt_YYYY;
            this.lcResultMonth.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcResultMonth.CustomizationFormText = "기간";
            this.lcResultMonth.Location = new System.Drawing.Point(0, 0);
            this.lcResultMonth.Name = "lcResultMonth";
            this.lcResultMonth.Size = new System.Drawing.Size(248, 24);
            this.lcResultMonth.Text = "기간";
            this.lcResultMonth.TextSize = new System.Drawing.Size(60, 14);
            // 
            // XRREP5009
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XRREP5009";
            this.Text = "XRREP5004";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_YYYY.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_YYYY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_MM.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_MM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPlanMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcXRREP5005master)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcResultMonth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcXRREP5005master;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcInDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_CustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomer;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private Service.Controls.SearchLookUpEditEx lup_ItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private Service.Controls.DateEditEx dt_YYYY;
        private DevExpress.XtraLayout.LayoutControlItem lcResultMonth;
        private DevExpress.XtraLayout.LayoutControlItem lcYear;
        private Service.Controls.DateEditEx dt_MM;
        private DevExpress.XtraLayout.LayoutControlItem lcPlanMonth;
    }
}