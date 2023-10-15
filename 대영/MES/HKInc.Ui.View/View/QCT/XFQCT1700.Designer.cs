namespace HKInc.Ui.View.View.QCT
{
    partial class XFQCT1700
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFQCT1700));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.tx_EduContent = new DevExpress.XtraEditors.TextEdit();
            this.lup_EduFlag = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_PlanDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcEduContent = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcEduFlag = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPlanDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcEduPlanList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_EduContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_EduFlag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPlanDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduPlanList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_EduContent);
            this.layoutControl1.Controls.Add(this.lup_EduFlag);
            this.layoutControl1.Controls.Add(this.dt_PlanDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 518);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(22, 106);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(892, 392);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // tx_EduContent
            // 
            this.tx_EduContent.Location = new System.Drawing.Point(524, 41);
            this.tx_EduContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_EduContent.Name = "tx_EduContent";
            this.tx_EduContent.Size = new System.Drawing.Size(171, 20);
            this.tx_EduContent.StyleController = this.layoutControl1;
            this.tx_EduContent.TabIndex = 0;
            // 
            // lup_EduFlag
            // 
            this.lup_EduFlag.Constraint = null;
            this.lup_EduFlag.DataSource = null;
            this.lup_EduFlag.DisplayMember = "";
            this.lup_EduFlag.isImeModeDisable = false;
            this.lup_EduFlag.isRequired = false;
            this.lup_EduFlag.Location = new System.Drawing.Point(305, 41);
            this.lup_EduFlag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_EduFlag.Name = "lup_EduFlag";
            this.lup_EduFlag.NullText = "";
            this.lup_EduFlag.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_EduFlag.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_EduFlag.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_EduFlag.Properties.NullText = "";
            this.lup_EduFlag.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_EduFlag.Size = new System.Drawing.Size(171, 20);
            this.lup_EduFlag.StyleController = this.layoutControl1;
            this.lup_EduFlag.TabIndex = 1;
            this.lup_EduFlag.Value_1 = null;
            this.lup_EduFlag.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.DetailHeight = 272;
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dt_PlanDate
            // 
            this.dt_PlanDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_PlanDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_PlanDate.Appearance.Options.UseBackColor = true;
            this.dt_PlanDate.Appearance.Options.UseFont = true;
            this.dt_PlanDate.EditFrValue = new System.DateTime(2020, 1, 24, 0, 0, 0, 0);
            this.dt_PlanDate.EditToValue = new System.DateTime(2020, 2, 24, 23, 59, 59, 990);
            this.dt_PlanDate.Location = new System.Drawing.Point(66, 41);
            this.dt_PlanDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dt_PlanDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_PlanDate.MinimumSize = new System.Drawing.Size(175, 16);
            this.dt_PlanDate.Name = "dt_PlanDate";
            this.dt_PlanDate.Size = new System.Drawing.Size(191, 20);
            this.dt_PlanDate.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcEduPlanList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.lcEduContent,
            this.lcEduFlag,
            this.lcPlanDate});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(918, 65);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(677, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(219, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcEduContent
            // 
            this.lcEduContent.Control = this.tx_EduContent;
            this.lcEduContent.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcEduContent.CustomizationFormText = "교육항목";
            this.lcEduContent.Location = new System.Drawing.Point(458, 0);
            this.lcEduContent.Name = "lcEduContent";
            this.lcEduContent.Size = new System.Drawing.Size(219, 24);
            this.lcEduContent.Text = "교육항목";
            this.lcEduContent.TextSize = new System.Drawing.Size(40, 14);
            // 
            // lcEduFlag
            // 
            this.lcEduFlag.Control = this.lup_EduFlag;
            this.lcEduFlag.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcEduFlag.CustomizationFormText = "구분";
            this.lcEduFlag.Location = new System.Drawing.Point(239, 0);
            this.lcEduFlag.Name = "lcEduFlag";
            this.lcEduFlag.Size = new System.Drawing.Size(219, 24);
            this.lcEduFlag.Text = "구분";
            this.lcEduFlag.TextSize = new System.Drawing.Size(40, 14);
            // 
            // lcPlanDate
            // 
            this.lcPlanDate.Control = this.dt_PlanDate;
            this.lcPlanDate.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcPlanDate.CustomizationFormText = "lcPlanDate";
            this.lcPlanDate.Location = new System.Drawing.Point(0, 0);
            this.lcPlanDate.Name = "lcPlanDate";
            this.lcPlanDate.Size = new System.Drawing.Size(239, 24);
            this.lcPlanDate.Text = "계획일자";
            this.lcPlanDate.TextSize = new System.Drawing.Size(40, 14);
            // 
            // lcEduPlanList
            // 
            this.lcEduPlanList.CustomizationFormText = "교육계획목록";
            this.lcEduPlanList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcEduPlanList.Location = new System.Drawing.Point(0, 65);
            this.lcEduPlanList.Name = "lcEduPlanList";
            this.lcEduPlanList.OptionsItemText.TextToControlDistance = 4;
            this.lcEduPlanList.Size = new System.Drawing.Size(918, 437);
            this.lcEduPlanList.Text = "교육계획목록";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(896, 396);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(509, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(308, 36);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XFQCT1700
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XFQCT1700";
            this.Text = "XFSTD1100";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_EduContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_EduFlag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPlanDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduPlanList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcEduPlanList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit tx_EduContent;
        private DevExpress.XtraLayout.LayoutControlItem lcEduContent;
        private Service.Controls.SearchLookUpEditEx lup_EduFlag;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcEduFlag;
        private Service.Controls.DatePeriodEditEx dt_PlanDate;
        private DevExpress.XtraLayout.LayoutControlItem lcPlanDate;
    }
}