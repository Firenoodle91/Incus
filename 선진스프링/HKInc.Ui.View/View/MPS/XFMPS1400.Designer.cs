namespace HKInc.Ui.View.View.MPS
{
    partial class XFMPS1400
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFMPS1400));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dt_StartDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.lup_StopCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_MachineCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcMachine = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcStopType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcStopStartDay = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcMachineStopList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_StopCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MachineCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopStartDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachineStopList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dt_StartDate);
            this.layoutControl1.Controls.Add(this.lup_StopCode);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_MachineCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dt_StartDate
            // 
            this.dt_StartDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_StartDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_StartDate.Appearance.Options.UseBackColor = true;
            this.dt_StartDate.Appearance.Options.UseFont = true;
            this.dt_StartDate.EditFrValue = new System.DateTime(2020, 2, 12, 0, 0, 0, 0);
            this.dt_StartDate.EditToValue = new System.DateTime(2020, 3, 12, 23, 59, 59, 990);
            this.dt_StartDate.Location = new System.Drawing.Point(130, 56);
            this.dt_StartDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_StartDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_StartDate.MinimumSize = new System.Drawing.Size(200, 21);
            this.dt_StartDate.Name = "dt_StartDate";
            this.dt_StartDate.Size = new System.Drawing.Size(200, 21);
            this.dt_StartDate.TabIndex = 0;
            // 
            // lup_StopCode
            // 
            this.lup_StopCode.Constraint = null;
            this.lup_StopCode.DataSource = null;
            this.lup_StopCode.DisplayMember = "";
            this.lup_StopCode.isImeModeDisable = false;
            this.lup_StopCode.isRequired = false;
            this.lup_StopCode.Location = new System.Drawing.Point(634, 56);
            this.lup_StopCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lup_StopCode.Name = "lup_StopCode";
            this.lup_StopCode.NullText = "";
            this.lup_StopCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_StopCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_StopCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_StopCode.Properties.NullText = "";
            this.lup_StopCode.Properties.PopupView = this.gridView1;
            this.lup_StopCode.Size = new System.Drawing.Size(95, 24);
            this.lup_StopCode.StyleController = this.layoutControl1;
            this.lup_StopCode.TabIndex = 2;
            this.lup_StopCode.Value_1 = null;
            this.lup_StopCode.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 141);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 481);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_MachineCode
            // 
            this.lup_MachineCode.Constraint = null;
            this.lup_MachineCode.DataSource = null;
            this.lup_MachineCode.DisplayMember = "";
            this.lup_MachineCode.isImeModeDisable = false;
            this.lup_MachineCode.isRequired = false;
            this.lup_MachineCode.Location = new System.Drawing.Point(435, 56);
            this.lup_MachineCode.Name = "lup_MachineCode";
            this.lup_MachineCode.NullText = "";
            this.lup_MachineCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_MachineCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_MachineCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MachineCode.Properties.NullText = "";
            this.lup_MachineCode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_MachineCode.Size = new System.Drawing.Size(94, 24);
            this.lup_MachineCode.StyleController = this.layoutControl1;
            this.lup_MachineCode.TabIndex = 1;
            this.lup_MachineCode.Value_1 = null;
            this.lup_MachineCode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcMachineStopList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcMachine,
            this.emptySpaceItem1,
            this.lcStopType,
            this.lcStopStartDay});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1044, 85);
            // 
            // lcMachine
            // 
            this.lcMachine.Control = this.lup_MachineCode;
            this.lcMachine.Location = new System.Drawing.Point(305, 0);
            this.lcMachine.Name = "lcMachine";
            this.lcMachine.Size = new System.Drawing.Size(199, 30);
            this.lcMachine.TextSize = new System.Drawing.Size(95, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(704, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(310, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcStopType
            // 
            this.lcStopType.Control = this.lup_StopCode;
            this.lcStopType.Location = new System.Drawing.Point(504, 0);
            this.lcStopType.Name = "lcStopType";
            this.lcStopType.Size = new System.Drawing.Size(200, 30);
            this.lcStopType.TextSize = new System.Drawing.Size(95, 18);
            // 
            // lcStopStartDay
            // 
            this.lcStopStartDay.Control = this.dt_StartDate;
            this.lcStopStartDay.Location = new System.Drawing.Point(0, 0);
            this.lcStopStartDay.Name = "lcStopStartDay";
            this.lcStopStartDay.Size = new System.Drawing.Size(305, 30);
            this.lcStopStartDay.TextSize = new System.Drawing.Size(95, 18);
            // 
            // lcMachineStopList
            // 
            this.lcMachineStopList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcMachineStopList.Location = new System.Drawing.Point(0, 85);
            this.lcMachineStopList.Name = "lcMachineStopList";
            this.lcMachineStopList.OptionsItemText.TextToControlDistance = 4;
            this.lcMachineStopList.Size = new System.Drawing.Size(1044, 542);
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
            // XFMPS1400
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Name = "XFMPS1400";
            this.Text = "XFMPS1400";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_StopCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MachineCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopStartDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachineStopList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.SearchLookUpEditEx lup_MachineCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcMachine;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcMachineStopList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.DatePeriodEditEx dt_StartDate;
        private Service.Controls.SearchLookUpEditEx lup_StopCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcStopType;
        private DevExpress.XtraLayout.LayoutControlItem lcStopStartDay;
    }
}