namespace HKInc.Ui.View.UPH
{
    partial class XFUPH1100
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.datePeriodEditEx1 = new HKInc.Service.Controls.DatePeriodEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lupMachineCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lupItemCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEditEx2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lupWorkId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEditEx3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMachineCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupWorkId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx3View)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lupWorkId);
            this.layoutControl1.Controls.Add(this.lupItemCode);
            this.layoutControl1.Controls.Add(this.lupMachineCode);
            this.layoutControl1.Controls.Add(this.datePeriodEditEx1);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // datePeriodEditEx1
            // 
            this.datePeriodEditEx1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.datePeriodEditEx1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePeriodEditEx1.Appearance.Options.UseBackColor = true;
            this.datePeriodEditEx1.Appearance.Options.UseFont = true;
            this.datePeriodEditEx1.EditFrValue = new System.DateTime(2019, 6, 8, 0, 0, 0, 0);
            this.datePeriodEditEx1.EditToValue = new System.DateTime(2019, 7, 8, 23, 59, 59, 990);
            this.datePeriodEditEx1.Location = new System.Drawing.Point(74, 56);
            this.datePeriodEditEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.datePeriodEditEx1.MaximumSize = new System.Drawing.Size(200, 20);
            this.datePeriodEditEx1.MinimumSize = new System.Drawing.Size(243, 26);
            this.datePeriodEditEx1.Name = "datePeriodEditEx1";
            this.datePeriodEditEx1.Size = new System.Drawing.Size(243, 26);
            this.datePeriodEditEx1.TabIndex = 7;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 143);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 479);
            this.gridEx1.TabIndex = 6;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1044, 87);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(831, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(183, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.datePeriodEditEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(292, 32);
            this.layoutControlItem2.Text = "생산일";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(39, 18);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "UPH 이력";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 87);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup3.Size = new System.Drawing.Size(1044, 540);
            this.layoutControlGroup3.Text = "UPH 이력";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1014, 485);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lupMachineCode
            // 
            this.lupMachineCode.Constraint = null;
            this.lupMachineCode.DataSource = null;
            this.lupMachineCode.DisplayMember = "";
            this.lupMachineCode.isRequired = false;
            this.lupMachineCode.Location = new System.Drawing.Point(366, 56);
            this.lupMachineCode.Name = "lupMachineCode";
            this.lupMachineCode.NullText = "";
            this.lupMachineCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMachineCode.Properties.NullText = "";
            this.lupMachineCode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lupMachineCode.Size = new System.Drawing.Size(123, 24);
            this.lupMachineCode.StyleController = this.layoutControl1;
            this.lupMachineCode.TabIndex = 10;
            this.lupMachineCode.Value_1 = null;
            this.lupMachineCode.ValueMember = "";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lupMachineCode;
            this.layoutControlItem6.Location = new System.Drawing.Point(292, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(172, 32);
            this.layoutControlItem6.Text = "설비";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(39, 18);
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // lupItemCode
            // 
            this.lupItemCode.Constraint = null;
            this.lupItemCode.DataSource = null;
            this.lupItemCode.DisplayMember = "";
            this.lupItemCode.isRequired = false;
            this.lupItemCode.Location = new System.Drawing.Point(538, 56);
            this.lupItemCode.Name = "lupItemCode";
            this.lupItemCode.NullText = "";
            this.lupItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupItemCode.Properties.NullText = "";
            this.lupItemCode.Properties.PopupView = this.searchLookUpEditEx2View;
            this.lupItemCode.Size = new System.Drawing.Size(139, 24);
            this.lupItemCode.StyleController = this.layoutControl1;
            this.lupItemCode.TabIndex = 11;
            this.lupItemCode.Value_1 = null;
            this.lupItemCode.ValueMember = "";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lupItemCode;
            this.layoutControlItem7.Location = new System.Drawing.Point(464, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(188, 32);
            this.layoutControlItem7.Text = "품목";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(39, 18);
            // 
            // searchLookUpEditEx2View
            // 
            this.searchLookUpEditEx2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx2View.Name = "searchLookUpEditEx2View";
            this.searchLookUpEditEx2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx2View.OptionsView.ShowGroupPanel = false;
            // 
            // lupWorkId
            // 
            this.lupWorkId.Constraint = null;
            this.lupWorkId.DataSource = null;
            this.lupWorkId.DisplayMember = "";
            this.lupWorkId.isRequired = false;
            this.lupWorkId.Location = new System.Drawing.Point(726, 56);
            this.lupWorkId.Name = "lupWorkId";
            this.lupWorkId.NullText = "";
            this.lupWorkId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupWorkId.Properties.NullText = "";
            this.lupWorkId.Properties.PopupView = this.searchLookUpEditEx3View;
            this.lupWorkId.Size = new System.Drawing.Size(130, 24);
            this.lupWorkId.StyleController = this.layoutControl1;
            this.lupWorkId.TabIndex = 12;
            this.lupWorkId.Value_1 = null;
            this.lupWorkId.ValueMember = "";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lupWorkId;
            this.layoutControlItem8.Location = new System.Drawing.Point(652, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(179, 32);
            this.layoutControlItem8.Text = "작업자";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(39, 18);
            // 
            // searchLookUpEditEx3View
            // 
            this.searchLookUpEditEx3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx3View.Name = "searchLookUpEditEx3View";
            this.searchLookUpEditEx3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx3View.OptionsView.ShowGroupPanel = false;
            // 
            // XFUPH1100
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFUPH1100";
            this.Text = "XFUPH1100";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMachineCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupWorkId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx3View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Service.Controls.DatePeriodEditEx datePeriodEditEx1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Service.Controls.SearchLookUpEditEx lupWorkId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx3View;
        private Service.Controls.SearchLookUpEditEx lupItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx2View;
        private Service.Controls.SearchLookUpEditEx lupMachineCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}