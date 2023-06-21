namespace HKInc.Ui.View.View.REPORT
{
    partial class XRREP3000
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRREP3000));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_WorkId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chart1 = new DevExpress.XtraCharts.ChartControl();
            this.dp_dt = new HKInc.Service.Controls.DatePeriodEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcResultDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcWorkId = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcWorkIdResultList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcWorkIdOkResult = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_WorkId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcResultDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkIdResultList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkIdOkResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_WorkId);
            this.layoutControl1.Controls.Add(this.Chart1);
            this.layoutControl1.Controls.Add(this.dp_dt);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_WorkId
            // 
            this.lup_WorkId.Constraint = null;
            this.lup_WorkId.DataSource = null;
            this.lup_WorkId.DisplayMember = "";
            this.lup_WorkId.isImeModeDisable = false;
            this.lup_WorkId.isRequired = false;
            this.lup_WorkId.Location = new System.Drawing.Point(379, 50);
            this.lup_WorkId.Name = "lup_WorkId";
            this.lup_WorkId.NullText = "";
            this.lup_WorkId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_WorkId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_WorkId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_WorkId.Properties.NullText = "";
            this.lup_WorkId.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_WorkId.Size = new System.Drawing.Size(153, 24);
            this.lup_WorkId.StyleController = this.layoutControl1;
            this.lup_WorkId.TabIndex = 1;
            this.lup_WorkId.Value_1 = null;
            this.lup_WorkId.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // Chart1
            // 
            this.Chart1.Legend.Name = "Default Legend";
            this.Chart1.Location = new System.Drawing.Point(24, 132);
            this.Chart1.Name = "Chart1";
            this.Chart1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.Chart1.Size = new System.Drawing.Size(1022, 215);
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
            this.dp_dt.Location = new System.Drawing.Point(67, 50);
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
            this.gridEx1.Location = new System.Drawing.Point(24, 401);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 242);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcWorkIdResultList,
            this.lcWorkIdOkResult});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.lcResultDate,
            this.lcWorkId});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1050, 82);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(512, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(514, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcResultDate
            // 
            this.lcResultDate.Control = this.dp_dt;
            this.lcResultDate.Location = new System.Drawing.Point(0, 0);
            this.lcResultDate.MaxSize = new System.Drawing.Size(312, 32);
            this.lcResultDate.MinSize = new System.Drawing.Size(312, 32);
            this.lcResultDate.Name = "lcResultDate";
            this.lcResultDate.Size = new System.Drawing.Size(312, 32);
            this.lcResultDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcResultDate.Text = "기간";
            this.lcResultDate.TextSize = new System.Drawing.Size(39, 18);
            // 
            // lcWorkId
            // 
            this.lcWorkId.Control = this.lup_WorkId;
            this.lcWorkId.Location = new System.Drawing.Point(312, 0);
            this.lcWorkId.Name = "lcWorkId";
            this.lcWorkId.Size = new System.Drawing.Size(200, 32);
            this.lcWorkId.Text = "작업자";
            this.lcWorkId.TextSize = new System.Drawing.Size(39, 18);
            // 
            // lcWorkIdResultList
            // 
            this.lcWorkIdResultList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcWorkIdResultList.Location = new System.Drawing.Point(0, 351);
            this.lcWorkIdResultList.Name = "lcWorkIdResultList";
            this.lcWorkIdResultList.OptionsItemText.TextToControlDistance = 4;
            this.lcWorkIdResultList.Size = new System.Drawing.Size(1050, 296);
            this.lcWorkIdResultList.Text = "작업자별 실적정보";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1026, 246);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcWorkIdOkResult
            // 
            this.lcWorkIdOkResult.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcWorkIdOkResult.Location = new System.Drawing.Point(0, 82);
            this.lcWorkIdOkResult.Name = "lcWorkIdOkResult";
            this.lcWorkIdOkResult.OptionsItemText.TextToControlDistance = 4;
            this.lcWorkIdOkResult.Size = new System.Drawing.Size(1050, 269);
            this.lcWorkIdOkResult.Text = "작업자별 양품실적";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Chart1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1026, 219);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // XRREP3000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XRREP3000";
            this.Text = "XRREP3000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_WorkId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcResultDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkIdResultList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkIdOkResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcWorkIdResultList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private Service.Controls.DatePeriodEditEx dp_dt;
        private DevExpress.XtraLayout.LayoutControlItem lcResultDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraCharts.ChartControl Chart1;
        private DevExpress.XtraLayout.LayoutControlGroup lcWorkIdOkResult;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.SearchLookUpEditEx lup_WorkId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcWorkId;
    }
}