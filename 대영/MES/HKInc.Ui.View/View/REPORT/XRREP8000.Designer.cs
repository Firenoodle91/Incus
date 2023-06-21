namespace HKInc.Ui.View.View.REPORT
{
    partial class XRREP8000
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRREP8000));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Chart1 = new DevExpress.XtraCharts.ChartControl();
            this.dp_dt = new HKInc.Service.Controls.DatePeriodEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_MachineCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView11 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcMachine = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcStopRatePresent = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcStopRate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MachineCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopRatePresent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Chart1);
            this.layoutControl1.Controls.Add(this.dp_dt);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.lup_MachineCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItem});
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Chart1
            // 
            this.Chart1.Legend.Name = "Default Legend";
            this.Chart1.Location = new System.Drawing.Point(24, 132);
            this.Chart1.Name = "Chart1";
            this.Chart1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.Chart1.Size = new System.Drawing.Size(1022, 171);
            this.Chart1.TabIndex = 2;
            // 
            // dp_dt
            // 
            this.dp_dt.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dp_dt.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dp_dt.Appearance.Options.UseBackColor = true;
            this.dp_dt.Appearance.Options.UseFont = true;
            this.dp_dt.EditFrValue = new System.DateTime(2019, 3, 8, 0, 0, 0, 0);
            this.dp_dt.EditToValue = new System.DateTime(2019, 4, 8, 23, 59, 59, 990);
            this.dp_dt.Location = new System.Drawing.Point(91, 51);
            this.dp_dt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dp_dt.MaximumSize = new System.Drawing.Size(200, 20);
            this.dp_dt.MinimumSize = new System.Drawing.Size(243, 26);
            this.dp_dt.Name = "dp_dt";
            this.dp_dt.Size = new System.Drawing.Size(243, 26);
            this.dp_dt.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 357);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 286);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(441, 50);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.gridView1;
            this.lup_Item.Size = new System.Drawing.Size(80, 24);
            this.lup_Item.StyleController = this.layoutControl1;
            this.lup_Item.TabIndex = 1;
            this.lup_Item.Value_1 = null;
            this.lup_Item.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lup_MachineCode
            // 
            this.lup_MachineCode.Constraint = null;
            this.lup_MachineCode.DataSource = null;
            this.lup_MachineCode.DisplayMember = "";
            this.lup_MachineCode.isImeModeDisable = false;
            this.lup_MachineCode.isRequired = false;
            this.lup_MachineCode.Location = new System.Drawing.Point(402, 50);
            this.lup_MachineCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lup_MachineCode.Name = "lup_MachineCode";
            this.lup_MachineCode.NullText = "";
            this.lup_MachineCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_MachineCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_MachineCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MachineCode.Properties.NullText = "";
            this.lup_MachineCode.Properties.PopupView = this.gridView11;
            this.lup_MachineCode.Size = new System.Drawing.Size(105, 24);
            this.lup_MachineCode.StyleController = this.layoutControl1;
            this.lup_MachineCode.TabIndex = 1;
            this.lup_MachineCode.Value_1 = null;
            this.lup_MachineCode.ValueMember = "";
            // 
            // gridView11
            // 
            this.gridView11.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView11.Name = "gridView11";
            this.gridView11.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView11.OptionsView.ShowGroupPanel = false;
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.CustomizationFormText = "품목";
            this.lcItem.Location = new System.Drawing.Point(312, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(189, 32);
            this.lcItem.Text = "품목";
            this.lcItem.TextSize = new System.Drawing.Size(26, 18);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.lcStopRatePresent,
            this.lcStopRate});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.lcMachine});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1050, 82);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dp_dt;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(312, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(312, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(312, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "기간";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(62, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(487, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(539, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcMachine
            // 
            this.lcMachine.Control = this.lup_MachineCode;
            this.lcMachine.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcMachine.CustomizationFormText = "lcMachine";
            this.lcMachine.Location = new System.Drawing.Point(312, 0);
            this.lcMachine.Name = "lcMachine";
            this.lcMachine.Size = new System.Drawing.Size(175, 32);
            this.lcMachine.TextSize = new System.Drawing.Size(62, 18);
            // 
            // lcStopRatePresent
            // 
            this.lcStopRatePresent.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcStopRatePresent.Location = new System.Drawing.Point(0, 307);
            this.lcStopRatePresent.Name = "lcStopRatePresent";
            this.lcStopRatePresent.OptionsItemText.TextToControlDistance = 4;
            this.lcStopRatePresent.Size = new System.Drawing.Size(1050, 340);
            this.lcStopRatePresent.Text = "목록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1026, 290);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcStopRate
            // 
            this.lcStopRate.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcStopRate.Location = new System.Drawing.Point(0, 82);
            this.lcStopRate.Name = "lcStopRate";
            this.lcStopRate.OptionsItemText.TextToControlDistance = 4;
            this.lcStopRate.Size = new System.Drawing.Size(1050, 225);
            this.lcStopRate.Text = "차트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Chart1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1026, 175);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // XRREP8000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XRREP8000";
            this.Text = "XRREP8000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MachineCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopRatePresent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStopRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup lcStopRatePresent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private Service.Controls.DatePeriodEditEx dp_dt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraCharts.ChartControl Chart1;
        private DevExpress.XtraLayout.LayoutControlGroup lcStopRate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Service.Controls.SearchLookUpEditEx lup_MachineCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView11;
        private DevExpress.XtraLayout.LayoutControlItem lcMachine;
    }
}