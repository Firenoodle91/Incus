namespace HKInc.Ui.View.View.REPORT
{
    partial class XRREP9000
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRREP9000));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dt_Date = new HKInc.Service.Controls.DatePeriodEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_ItemCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView11 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tx_DelivNo = new DevExpress.XtraEditors.TextEdit();
            this.tx_PlanNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPlanStartDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDelivNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPlanNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcWorkPlanToResult = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_DelivNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_PlanNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPlanStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDelivNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPlanNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkPlanToResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dt_Date);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_ItemCode);
            this.layoutControl1.Controls.Add(this.tx_DelivNo);
            this.layoutControl1.Controls.Add(this.tx_PlanNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 518);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dt_Date
            // 
            this.dt_Date.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_Date.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_Date.Appearance.Options.UseBackColor = true;
            this.dt_Date.Appearance.Options.UseFont = true;
            this.dt_Date.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dt_Date.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dt_Date.Location = new System.Drawing.Point(78, 45);
            this.dt_Date.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dt_Date.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_Date.MinimumSize = new System.Drawing.Size(213, 22);
            this.dt_Date.Name = "dt_Date";
            this.dt_Date.Size = new System.Drawing.Size(213, 22);
            this.dt_Date.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 116);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(888, 368);
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
            this.lup_ItemCode.Location = new System.Drawing.Point(349, 45);
            this.lup_ItemCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_ItemCode.Name = "lup_ItemCode";
            this.lup_ItemCode.NullText = "";
            this.lup_ItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_ItemCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_ItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_ItemCode.Properties.NullText = "";
            this.lup_ItemCode.Properties.PopupView = this.gridView11;
            this.lup_ItemCode.Size = new System.Drawing.Size(103, 20);
            this.lup_ItemCode.StyleController = this.layoutControl1;
            this.lup_ItemCode.TabIndex = 0;
            this.lup_ItemCode.Value_1 = null;
            this.lup_ItemCode.ValueMember = "";
            // 
            // gridView11
            // 
            this.gridView11.DetailHeight = 272;
            this.gridView11.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView11.Name = "gridView11";
            this.gridView11.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView11.OptionsView.ShowGroupPanel = false;
            // 
            // tx_DelivNo
            // 
            this.tx_DelivNo.Location = new System.Drawing.Point(510, 45);
            this.tx_DelivNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_DelivNo.Name = "tx_DelivNo";
            this.tx_DelivNo.Size = new System.Drawing.Size(162, 20);
            this.tx_DelivNo.StyleController = this.layoutControl1;
            this.tx_DelivNo.TabIndex = 2;
            // 
            // tx_PlanNo
            // 
            this.tx_PlanNo.Location = new System.Drawing.Point(730, 45);
            this.tx_PlanNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_PlanNo.Name = "tx_PlanNo";
            this.tx_PlanNo.Size = new System.Drawing.Size(182, 20);
            this.tx_PlanNo.StyleController = this.layoutControl1;
            this.tx_PlanNo.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcWorkPlanToResult,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPlanStartDate,
            this.lcItemCode,
            this.lcDelivNo,
            this.lcPlanNo});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(916, 71);
            this.lcCondition.Text = "조회조건";
            // 
            // lcPlanStartDate
            // 
            this.lcPlanStartDate.Control = this.dt_Date;
            this.lcPlanStartDate.Location = new System.Drawing.Point(0, 0);
            this.lcPlanStartDate.Name = "lcPlanStartDate";
            this.lcPlanStartDate.Size = new System.Drawing.Size(271, 26);
            this.lcPlanStartDate.Text = "납품예정일";
            this.lcPlanStartDate.TextSize = new System.Drawing.Size(50, 14);
            // 
            // lcItemCode
            // 
            this.lcItemCode.Control = this.lup_ItemCode;
            this.lcItemCode.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcItemCode.CustomizationFormText = "lcItem";
            this.lcItemCode.Location = new System.Drawing.Point(271, 0);
            this.lcItemCode.Name = "lcItemCode";
            this.lcItemCode.Size = new System.Drawing.Size(161, 26);
            this.lcItemCode.Text = "lcItem";
            this.lcItemCode.TextSize = new System.Drawing.Size(50, 14);
            // 
            // lcDelivNo
            // 
            this.lcDelivNo.Control = this.tx_DelivNo;
            this.lcDelivNo.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcDelivNo.CustomizationFormText = "lcWorkNo";
            this.lcDelivNo.Location = new System.Drawing.Point(432, 0);
            this.lcDelivNo.Name = "lcDelivNo";
            this.lcDelivNo.Size = new System.Drawing.Size(220, 26);
            this.lcDelivNo.TextSize = new System.Drawing.Size(50, 14);
            // 
            // lcPlanNo
            // 
            this.lcPlanNo.Control = this.tx_PlanNo;
            this.lcPlanNo.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcPlanNo.CustomizationFormText = "lcWorkNo";
            this.lcPlanNo.Location = new System.Drawing.Point(652, 0);
            this.lcPlanNo.Name = "lcPlanNo";
            this.lcPlanNo.Size = new System.Drawing.Size(240, 26);
            this.lcPlanNo.Text = "lcPlanNo";
            this.lcPlanNo.TextSize = new System.Drawing.Size(50, 14);
            // 
            // lcWorkPlanToResult
            // 
            this.lcWorkPlanToResult.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcWorkPlanToResult.Location = new System.Drawing.Point(0, 71);
            this.lcWorkPlanToResult.Name = "lcWorkPlanToResult";
            this.lcWorkPlanToResult.OptionsItemText.TextToControlDistance = 4;
            this.lcWorkPlanToResult.Size = new System.Drawing.Size(916, 417);
            this.lcWorkPlanToResult.Text = "영업계획 및 매출현황";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(892, 372);
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
            // XRREP9000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XRREP9000";
            this.Text = "XRREP9000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_DelivNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_PlanNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPlanStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDelivNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPlanNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkPlanToResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dt_Date;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcWorkPlanToResult;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlItem lcPlanStartDate;
        private Service.Controls.SearchLookUpEditEx lup_ItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView11;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCode;
        private DevExpress.XtraEditors.TextEdit tx_DelivNo;
        private DevExpress.XtraEditors.TextEdit tx_PlanNo;
        private DevExpress.XtraLayout.LayoutControlItem lcDelivNo;
        private DevExpress.XtraLayout.LayoutControlItem lcPlanNo;
    }
}