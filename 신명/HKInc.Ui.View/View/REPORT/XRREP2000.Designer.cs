namespace HKInc.Ui.View.View.REPORT
{
    partial class XRREP2000
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRREP2000));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.Chart1 = new DevExpress.XtraCharts.ChartControl();
            this.dp_dt = new HKInc.Service.Controls.DateEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcYear = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemCodeName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcWorkPlanToResult = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDueRate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCodeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkPlanToResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDueRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Controls.Add(this.Chart1);
            this.layoutControl1.Controls.Add(this.dp_dt);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(206, 56);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(187, 24);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 1;
            // 
            // Chart1
            // 
            this.Chart1.Legend.Name = "Default Legend";
            this.Chart1.Location = new System.Drawing.Point(31, 141);
            this.Chart1.Name = "Chart1";
            this.Chart1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.Chart1.Size = new System.Drawing.Size(1008, 205);
            this.Chart1.TabIndex = 2;
            // 
            // dp_dt
            // 
            this.dp_dt.EditValue = null;
            this.dp_dt.Location = new System.Drawing.Point(80, 56);
            this.dp_dt.Name = "dp_dt";
            this.dp_dt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dp_dt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dp_dt.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.dp_dt.Properties.DisplayFormat.FormatString = "yyyy";
            this.dp_dt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dp_dt.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dp_dt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dp_dt.Properties.Mask.EditMask = "yyyy";
            this.dp_dt.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dp_dt.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearsGroupView;
            this.dp_dt.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.dp_dt.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dp_dt.Size = new System.Drawing.Size(71, 24);
            this.dp_dt.StyleController = this.layoutControl1;
            this.dp_dt.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 407);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 215);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcWorkPlanToResult,
            this.lcDueRate});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.lcYear,
            this.lcItemCodeName});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1044, 85);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(368, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(646, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcYear
            // 
            this.lcYear.Control = this.dp_dt;
            this.lcYear.Location = new System.Drawing.Point(0, 0);
            this.lcYear.Name = "lcYear";
            this.lcYear.Size = new System.Drawing.Size(126, 30);
            this.lcYear.Text = "년도";
            this.lcYear.TextSize = new System.Drawing.Size(45, 18);
            // 
            // lcItemCodeName
            // 
            this.lcItemCodeName.Control = this.textEdit1;
            this.lcItemCodeName.Location = new System.Drawing.Point(126, 0);
            this.lcItemCodeName.Name = "lcItemCodeName";
            this.lcItemCodeName.Size = new System.Drawing.Size(242, 30);
            this.lcItemCodeName.Text = "품번/명";
            this.lcItemCodeName.TextSize = new System.Drawing.Size(45, 18);
            // 
            // lcWorkPlanToResult
            // 
            this.lcWorkPlanToResult.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcWorkPlanToResult.Location = new System.Drawing.Point(0, 351);
            this.lcWorkPlanToResult.Name = "lcWorkPlanToResult";
            this.lcWorkPlanToResult.OptionsItemText.TextToControlDistance = 4;
            this.lcWorkPlanToResult.Size = new System.Drawing.Size(1044, 276);
            this.lcWorkPlanToResult.Text = "계획대비 실적";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1014, 221);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcDueRate
            // 
            this.lcDueRate.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcDueRate.Location = new System.Drawing.Point(0, 85);
            this.lcDueRate.Name = "lcDueRate";
            this.lcDueRate.OptionsItemText.TextToControlDistance = 4;
            this.lcDueRate.Size = new System.Drawing.Size(1044, 266);
            this.lcDueRate.Text = "납품률";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.Chart1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(106, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1014, 211);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // XRREP2000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Name = "XRREP2000";
            this.Text = "XRREP2000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dp_dt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCodeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkPlanToResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDueRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcWorkPlanToResult;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Service.Controls.DateEditEx dp_dt;
        private DevExpress.XtraLayout.LayoutControlItem lcYear;
        private DevExpress.XtraCharts.ChartControl Chart1;
        private DevExpress.XtraLayout.LayoutControlGroup lcDueRate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCodeName;
    }
}