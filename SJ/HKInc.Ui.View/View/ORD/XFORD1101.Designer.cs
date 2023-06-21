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
            this.lup_Manager = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcManagerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcShipmentReportMaster = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcShipmentReportDetail = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.dt_OutDatePlan = new HKInc.Service.Controls.DateEditEx();
            this.lcOutDatePlan = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Manager.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManagerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShipmentReportMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShipmentReportDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OutDatePlan.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OutDatePlan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDatePlan)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dt_OutDatePlan);
            this.layoutControl1.Controls.Add(this.lup_Manager);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 509);
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
            this.lup_Manager.Location = new System.Drawing.Point(331, 43);
            this.lup_Manager.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_Manager.Name = "lup_Manager";
            this.lup_Manager.NullText = "";
            this.lup_Manager.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Manager.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Manager.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Manager.Properties.NullText = "";
            this.lup_Manager.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Manager.Size = new System.Drawing.Size(126, 20);
            this.lup_Manager.StyleController = this.layoutControl1;
            this.lup_Manager.TabIndex = 1;
            this.lup_Manager.Value_1 = null;
            this.lup_Manager.ValueMember = "";
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
            this.gridEx2.Location = new System.Drawing.Point(24, 308);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(888, 177);
            this.gridEx2.TabIndex = 5;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 110);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(888, 146);
            this.gridEx1.TabIndex = 4;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcShipmentReportMaster,
            this.lcShipmentReportDetail,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 509);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcManagerName,
            this.lcOutDatePlan});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(916, 67);
            this.lcCondition.Text = "lcCondition";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(437, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(455, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcManagerName
            // 
            this.lcManagerName.Control = this.lup_Manager;
            this.lcManagerName.Location = new System.Drawing.Point(218, 0);
            this.lcManagerName.Name = "lcManagerName";
            this.lcManagerName.Size = new System.Drawing.Size(219, 24);
            this.lcManagerName.Text = "lcManagerName";
            this.lcManagerName.TextSize = new System.Drawing.Size(85, 14);
            // 
            // lcShipmentReportMaster
            // 
            this.lcShipmentReportMaster.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcShipmentReportMaster.Location = new System.Drawing.Point(0, 67);
            this.lcShipmentReportMaster.Name = "lcShipmentReportMaster";
            this.lcShipmentReportMaster.OptionsItemText.TextToControlDistance = 4;
            this.lcShipmentReportMaster.Size = new System.Drawing.Size(916, 193);
            this.lcShipmentReportMaster.Text = "lcShipmentReportMaster";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(892, 150);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcShipmentReportDetail
            // 
            this.lcShipmentReportDetail.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcShipmentReportDetail.Location = new System.Drawing.Point(0, 265);
            this.lcShipmentReportDetail.Name = "lcShipmentReportDetail";
            this.lcShipmentReportDetail.OptionsItemText.TextToControlDistance = 4;
            this.lcShipmentReportDetail.Size = new System.Drawing.Size(916, 224);
            this.lcShipmentReportDetail.Text = "lcShipmentReportDetail";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(892, 181);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 260);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(916, 5);
            // 
            // dt_OutDatePlan
            // 
            this.dt_OutDatePlan.EditValue = null;
            this.dt_OutDatePlan.Location = new System.Drawing.Point(113, 43);
            this.dt_OutDatePlan.Name = "dt_OutDatePlan";
            this.dt_OutDatePlan.Properties.Appearance.Options.UseTextOptions = true;
            this.dt_OutDatePlan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
            this.dt_OutDatePlan.Size = new System.Drawing.Size(125, 20);
            this.dt_OutDatePlan.StyleController = this.layoutControl1;
            this.dt_OutDatePlan.TabIndex = 6;
            // 
            // lcOutDatePlan
            // 
            this.lcOutDatePlan.Control = this.dt_OutDatePlan;
            this.lcOutDatePlan.Location = new System.Drawing.Point(0, 0);
            this.lcOutDatePlan.Name = "lcOutDatePlan";
            this.lcOutDatePlan.Size = new System.Drawing.Size(218, 24);
            this.lcOutDatePlan.TextSize = new System.Drawing.Size(85, 14);
            // 
            // XFORD1101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XFORD1101";
            this.Text = "XFORD1000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Manager.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcManagerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShipmentReportMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShipmentReportDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OutDatePlan.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OutDatePlan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDatePlan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcShipmentReportMaster;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcShipmentReportDetail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private Service.Controls.SearchLookUpEditEx lup_Manager;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcManagerName;
        private Service.Controls.DateEditEx dt_OutDatePlan;
        private DevExpress.XtraLayout.LayoutControlItem lcOutDatePlan;
    }
}