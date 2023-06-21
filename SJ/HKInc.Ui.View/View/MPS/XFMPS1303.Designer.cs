namespace HKInc.Ui.View.View.MPS
{
    partial class XFMPS1303
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFMPS1303));
            this.ListFormTemplatelayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.lup_ProcTeamCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_WorkDate = new HKInc.Service.Controls.DateEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_Process = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView11 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcYearMonth = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProductTeam = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProcess = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProductionDailyList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cbo_FirstDay = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lcDay = new DevExpress.XtraLayout.LayoutControlItem();
            this.cbo_LastDay = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormTemplatelayoutControl1ConvertedLayout)).BeginInit();
            this.ListFormTemplatelayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ProcTeamCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_WorkDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_WorkDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Process.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcYearMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductTeam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductionDailyList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_FirstDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_LastDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // ListFormTemplatelayoutControl1ConvertedLayout
            // 
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.cbo_LastDay);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.cbo_FirstDay);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.lup_ProcTeamCode);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.dt_WorkDate);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.gridEx1);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.lup_Process);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListFormTemplatelayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 30);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Name = "ListFormTemplatelayoutControl1ConvertedLayout";
            this.ListFormTemplatelayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.ListFormTemplatelayoutControl1ConvertedLayout.Size = new System.Drawing.Size(1070, 667);
            this.ListFormTemplatelayoutControl1ConvertedLayout.TabIndex = 0;
            // 
            // lup_ProcTeamCode
            // 
            this.lup_ProcTeamCode.Constraint = null;
            this.lup_ProcTeamCode.DataSource = null;
            this.lup_ProcTeamCode.DisplayMember = "";
            this.lup_ProcTeamCode.isImeModeDisable = false;
            this.lup_ProcTeamCode.isRequired = false;
            this.lup_ProcTeamCode.Location = new System.Drawing.Point(422, 50);
            this.lup_ProcTeamCode.Name = "lup_ProcTeamCode";
            this.lup_ProcTeamCode.NullText = "";
            this.lup_ProcTeamCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_ProcTeamCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_ProcTeamCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_ProcTeamCode.Properties.NullText = "";
            this.lup_ProcTeamCode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_ProcTeamCode.Size = new System.Drawing.Size(374, 24);
            this.lup_ProcTeamCode.StyleController = this.ListFormTemplatelayoutControl1ConvertedLayout;
            this.lup_ProcTeamCode.TabIndex = 6;
            this.lup_ProcTeamCode.Value_1 = null;
            this.lup_ProcTeamCode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dt_WorkDate
            // 
            this.dt_WorkDate.EditValue = null;
            this.dt_WorkDate.Location = new System.Drawing.Point(60, 50);
            this.dt_WorkDate.Name = "dt_WorkDate";
            this.dt_WorkDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_WorkDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_WorkDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dt_WorkDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_WorkDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dt_WorkDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_WorkDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dt_WorkDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_WorkDate.Size = new System.Drawing.Size(91, 24);
            this.dt_WorkDate.StyleController = this.ListFormTemplatelayoutControl1ConvertedLayout;
            this.dt_WorkDate.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 128);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 515);
            this.gridEx1.TabIndex = 5;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_Process
            // 
            this.lup_Process.Constraint = null;
            this.lup_Process.DataSource = null;
            this.lup_Process.DisplayMember = "";
            this.lup_Process.isImeModeDisable = false;
            this.lup_Process.isRequired = false;
            this.lup_Process.Location = new System.Drawing.Point(901, 50);
            this.lup_Process.Name = "lup_Process";
            this.lup_Process.NullText = "";
            this.lup_Process.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Process.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Process.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Process.Properties.NullText = "";
            this.lup_Process.Properties.PopupView = this.gridView11;
            this.lup_Process.Size = new System.Drawing.Size(89, 24);
            this.lup_Process.StyleController = this.ListFormTemplatelayoutControl1ConvertedLayout;
            this.lup_Process.TabIndex = 7;
            this.lup_Process.Value_1 = null;
            this.lup_Process.ValueMember = "";
            // 
            // gridView11
            // 
            this.gridView11.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView11.Name = "gridView11";
            this.gridView11.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView11.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcProductionDailyList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.lcYearMonth,
            this.lcProductTeam,
            this.lcProcess,
            this.lcDay,
            this.layoutControlItem1});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1050, 78);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(970, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(56, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcYearMonth
            // 
            this.lcYearMonth.Control = this.dt_WorkDate;
            this.lcYearMonth.Location = new System.Drawing.Point(0, 0);
            this.lcYearMonth.MaxSize = new System.Drawing.Size(131, 28);
            this.lcYearMonth.MinSize = new System.Drawing.Size(131, 28);
            this.lcYearMonth.Name = "lcYearMonth";
            this.lcYearMonth.Size = new System.Drawing.Size(131, 28);
            this.lcYearMonth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcYearMonth.Text = "년-월";
            this.lcYearMonth.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcYearMonth.TextSize = new System.Drawing.Size(31, 18);
            this.lcYearMonth.TextToControlDistance = 5;
            // 
            // lcProductTeam
            // 
            this.lcProductTeam.Control = this.lup_ProcTeamCode;
            this.lcProductTeam.Location = new System.Drawing.Point(297, 0);
            this.lcProductTeam.Name = "lcProductTeam";
            this.lcProductTeam.Size = new System.Drawing.Size(479, 28);
            this.lcProductTeam.TextSize = new System.Drawing.Size(97, 18);
            // 
            // lcProcess
            // 
            this.lcProcess.Control = this.lup_Process;
            this.lcProcess.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcProcess.CustomizationFormText = "lcProcess";
            this.lcProcess.Location = new System.Drawing.Point(776, 0);
            this.lcProcess.Name = "lcProcess";
            this.lcProcess.Size = new System.Drawing.Size(194, 28);
            this.lcProcess.TextSize = new System.Drawing.Size(97, 18);
            // 
            // lcProductionDailyList
            // 
            this.lcProductionDailyList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcProductionDailyList.Location = new System.Drawing.Point(0, 78);
            this.lcProductionDailyList.Name = "lcProductionDailyList";
            this.lcProductionDailyList.OptionsItemText.TextToControlDistance = 4;
            this.lcProductionDailyList.Size = new System.Drawing.Size(1050, 569);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1026, 519);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // cbo_FirstDay
            // 
            this.cbo_FirstDay.Location = new System.Drawing.Point(173, 50);
            this.cbo_FirstDay.Name = "cbo_FirstDay";
            this.cbo_FirstDay.Properties.Appearance.Options.UseTextOptions = true;
            this.cbo_FirstDay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_FirstDay.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cbo_FirstDay.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_FirstDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_FirstDay.Properties.DropDownRows = 30;
            this.cbo_FirstDay.Size = new System.Drawing.Size(62, 24);
            this.cbo_FirstDay.StyleController = this.ListFormTemplatelayoutControl1ConvertedLayout;
            this.cbo_FirstDay.TabIndex = 8;
            // 
            // lcDay
            // 
            this.lcDay.Control = this.cbo_FirstDay;
            this.lcDay.Location = new System.Drawing.Point(131, 0);
            this.lcDay.MaxSize = new System.Drawing.Size(84, 28);
            this.lcDay.MinSize = new System.Drawing.Size(84, 28);
            this.lcDay.Name = "lcDay";
            this.lcDay.Size = new System.Drawing.Size(84, 28);
            this.lcDay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcDay.Text = "일";
            this.lcDay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcDay.TextSize = new System.Drawing.Size(13, 18);
            this.lcDay.TextToControlDistance = 5;
            // 
            // cbo_LastDay
            // 
            this.cbo_LastDay.Location = new System.Drawing.Point(255, 50);
            this.cbo_LastDay.Name = "cbo_LastDay";
            this.cbo_LastDay.Properties.Appearance.Options.UseTextOptions = true;
            this.cbo_LastDay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_LastDay.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cbo_LastDay.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbo_LastDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_LastDay.Properties.DropDownRows = 30;
            this.cbo_LastDay.Size = new System.Drawing.Size(62, 24);
            this.cbo_LastDay.StyleController = this.ListFormTemplatelayoutControl1ConvertedLayout;
            this.cbo_LastDay.TabIndex = 9;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbo_LastDay;
            this.layoutControlItem1.Location = new System.Drawing.Point(215, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(82, 28);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(82, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(82, 28);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "~";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(11, 18);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // XFMPS1303
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.ListFormTemplatelayoutControl1ConvertedLayout);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFMPS1303";
            this.Text = "XFMPS1303";
            this.Controls.SetChildIndex(this.ListFormTemplatelayoutControl1ConvertedLayout, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormTemplatelayoutControl1ConvertedLayout)).EndInit();
            this.ListFormTemplatelayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_ProcTeamCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_WorkDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_WorkDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Process.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcYearMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductTeam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductionDailyList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_FirstDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_LastDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl ListFormTemplatelayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcProductionDailyList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.DateEditEx dt_WorkDate;
        private DevExpress.XtraLayout.LayoutControlItem lcYearMonth;
        private Service.Controls.SearchLookUpEditEx lup_ProcTeamCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcProductTeam;
        private Service.Controls.SearchLookUpEditEx lup_Process;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView11;
        private DevExpress.XtraLayout.LayoutControlItem lcProcess;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_FirstDay;
        private DevExpress.XtraLayout.LayoutControlItem lcDay;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_LastDay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}