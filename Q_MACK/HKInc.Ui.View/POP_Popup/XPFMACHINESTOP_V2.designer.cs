namespace HKInc.Ui.View.POP_POPUP
{
    partial class XPFMACHINESTOP_V2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFMACHINESTOP_V2));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_Machine = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel_Button = new DevExpress.XtraEditors.PanelControl();
            this.dt_StopStartDate = new HKInc.Service.Controls.DateEditEx();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.lup_MachineGroup = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcStopInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcStopStartDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcMachineGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcMachine = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcStopType = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Machine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_StopStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_StopStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MachineGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachineGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_Machine);
            this.layoutControl1.Controls.Add(this.panel_Button);
            this.layoutControl1.Controls.Add(this.dt_StopStartDate);
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.lup_MachineGroup);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1262, 918);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_Machine
            // 
            this.lup_Machine.Constraint = null;
            this.lup_Machine.DataSource = null;
            this.lup_Machine.DisplayMember = "";
            this.lup_Machine.isImeModeDisable = false;
            this.lup_Machine.isRequired = false;
            this.lup_Machine.Location = new System.Drawing.Point(24, 258);
            this.lup_Machine.Name = "lup_Machine";
            this.lup_Machine.NullText = "";
            this.lup_Machine.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Machine.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lup_Machine.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Machine.Properties.Appearance.Options.UseFont = true;
            this.lup_Machine.Properties.AutoHeight = false;
            this.lup_Machine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Machine.Properties.NullText = "";
            this.lup_Machine.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Machine.Size = new System.Drawing.Size(282, 46);
            this.lup_Machine.StyleController = this.layoutControl1;
            this.lup_Machine.TabIndex = 1;
            this.lup_Machine.Value_1 = null;
            this.lup_Machine.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // panel_Button
            // 
            this.panel_Button.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel_Button.Location = new System.Drawing.Point(323, 117);
            this.panel_Button.Name = "panel_Button";
            this.panel_Button.Size = new System.Drawing.Size(926, 788);
            this.panel_Button.TabIndex = 3;
            // 
            // dt_StopStartDate
            // 
            this.dt_StopStartDate.EditValue = null;
            this.dt_StopStartDate.Location = new System.Drawing.Point(24, 347);
            this.dt_StopStartDate.Name = "dt_StopStartDate";
            this.dt_StopStartDate.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.dt_StopStartDate.Properties.Appearance.Options.UseFont = true;
            this.dt_StopStartDate.Properties.AutoHeight = false;
            this.dt_StopStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_StopStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_StopStartDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dt_StopStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_StopStartDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dt_StopStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_StopStartDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dt_StopStartDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_StopStartDate.Size = new System.Drawing.Size(282, 47);
            this.dt_StopStartDate.StyleController = this.layoutControl1;
            this.dt_StopStartDate.TabIndex = 2;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("맑은 고딕", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseTextOptions = true;
            this.btn_Cancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_Cancel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_Cancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.ImageOptions.Image")));
            this.btn_Cancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel.Location = new System.Drawing.Point(1097, 12);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(153, 66);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "취소";
            // 
            // lup_MachineGroup
            // 
            this.lup_MachineGroup.Constraint = null;
            this.lup_MachineGroup.DataSource = null;
            this.lup_MachineGroup.DisplayMember = "";
            this.lup_MachineGroup.isImeModeDisable = false;
            this.lup_MachineGroup.isRequired = false;
            this.lup_MachineGroup.Location = new System.Drawing.Point(24, 168);
            this.lup_MachineGroup.Name = "lup_MachineGroup";
            this.lup_MachineGroup.NullText = "";
            this.lup_MachineGroup.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_MachineGroup.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lup_MachineGroup.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_MachineGroup.Properties.Appearance.Options.UseFont = true;
            this.lup_MachineGroup.Properties.AutoHeight = false;
            this.lup_MachineGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MachineGroup.Properties.NullText = "";
            this.lup_MachineGroup.Properties.PopupView = this.gridView1;
            this.lup_MachineGroup.Size = new System.Drawing.Size(282, 46);
            this.lup_MachineGroup.StyleController = this.layoutControl1;
            this.lup_MachineGroup.TabIndex = 0;
            this.lup_MachineGroup.Value_1 = null;
            this.lup_MachineGroup.ValueMember = "";
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
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.lcStopInfo,
            this.lcStopType});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1262, 918);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_Cancel;
            this.layoutControlItem1.Location = new System.Drawing.Point(1085, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(157, 70);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(157, 70);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(157, 70);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(1085, 70);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcStopInfo
            // 
            this.lcStopInfo.AppearanceGroup.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lcStopInfo.AppearanceGroup.Options.UseFont = true;
            this.lcStopInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcStopStartDate,
            this.lcMachineGroup,
            this.lcMachine});
            this.lcStopInfo.Location = new System.Drawing.Point(0, 70);
            this.lcStopInfo.Name = "lcStopInfo";
            this.lcStopInfo.OptionsItemText.TextToControlDistance = 4;
            this.lcStopInfo.Size = new System.Drawing.Size(310, 828);
            this.lcStopInfo.Text = "비가동정보";
            // 
            // lcStopStartDate
            // 
            this.lcStopStartDate.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lcStopStartDate.AppearanceItemCaption.Options.UseFont = true;
            this.lcStopStartDate.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcStopStartDate.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcStopStartDate.Control = this.dt_StopStartDate;
            this.lcStopStartDate.Location = new System.Drawing.Point(0, 180);
            this.lcStopStartDate.MaxSize = new System.Drawing.Size(0, 90);
            this.lcStopStartDate.MinSize = new System.Drawing.Size(1, 90);
            this.lcStopStartDate.Name = "lcStopStartDate";
            this.lcStopStartDate.Size = new System.Drawing.Size(286, 590);
            this.lcStopStartDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcStopStartDate.Text = "비가동시작시간";
            this.lcStopStartDate.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcStopStartDate.TextSize = new System.Drawing.Size(175, 35);
            // 
            // lcMachineGroup
            // 
            this.lcMachineGroup.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lcMachineGroup.AppearanceItemCaption.Options.UseFont = true;
            this.lcMachineGroup.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcMachineGroup.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcMachineGroup.Control = this.lup_MachineGroup;
            this.lcMachineGroup.CustomizationFormText = "설비";
            this.lcMachineGroup.Location = new System.Drawing.Point(0, 0);
            this.lcMachineGroup.MaxSize = new System.Drawing.Size(0, 90);
            this.lcMachineGroup.MinSize = new System.Drawing.Size(1, 90);
            this.lcMachineGroup.Name = "lcMachineGroup";
            this.lcMachineGroup.Size = new System.Drawing.Size(286, 90);
            this.lcMachineGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcMachineGroup.Text = "설비그룹";
            this.lcMachineGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcMachineGroup.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcMachineGroup.TextSize = new System.Drawing.Size(100, 35);
            this.lcMachineGroup.TextToControlDistance = 5;
            this.lcMachineGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcMachine
            // 
            this.lcMachine.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lcMachine.AppearanceItemCaption.Options.UseFont = true;
            this.lcMachine.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcMachine.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcMachine.Control = this.lup_Machine;
            this.lcMachine.Location = new System.Drawing.Point(0, 90);
            this.lcMachine.MaxSize = new System.Drawing.Size(0, 90);
            this.lcMachine.MinSize = new System.Drawing.Size(1, 90);
            this.lcMachine.Name = "lcMachine";
            this.lcMachine.Size = new System.Drawing.Size(286, 90);
            this.lcMachine.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcMachine.Text = "설비";
            this.lcMachine.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcMachine.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcMachine.TextSize = new System.Drawing.Size(50, 35);
            this.lcMachine.TextToControlDistance = 5;
            // 
            // lcStopType
            // 
            this.lcStopType.AppearanceGroup.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lcStopType.AppearanceGroup.Options.UseFont = true;
            this.lcStopType.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcStopType.Location = new System.Drawing.Point(310, 70);
            this.lcStopType.Name = "lcStopType";
            this.lcStopType.OptionsItemText.TextToControlDistance = 4;
            this.lcStopType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcStopType.Size = new System.Drawing.Size(932, 828);
            this.lcStopType.Text = "비가동유형";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panel_Button;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(926, 788);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // XPFMACHINESTOP_V2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFMACHINESTOP_V2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "비가동등록";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Machine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_StopStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_StopStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MachineGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachineGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcStopInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcStopType;
        private Service.Controls.DateEditEx dt_StopStartDate;
        private DevExpress.XtraLayout.LayoutControlItem lcStopStartDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.PanelControl panel_Button;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Service.Controls.SearchLookUpEditEx lup_Machine;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcMachine;
        private Service.Controls.SearchLookUpEditEx lup_MachineGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcMachineGroup;
    }
}