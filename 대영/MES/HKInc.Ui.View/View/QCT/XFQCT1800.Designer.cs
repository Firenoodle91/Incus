namespace HKInc.Ui.View.View.QCT
{
    partial class XFQCT1800
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFQCT1800));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.tx_EduDocName = new DevExpress.XtraEditors.TextEdit();
            this.dt_EduDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcEduDocName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcEduDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcEduPlanDataList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_EduDocName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduDocName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduPlanDataList)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.tx_EduDocName);
            this.layoutControl1.Controls.Add(this.dt_EduDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 130);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 513);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // tx_EduDocName
            // 
            this.tx_EduDocName.Location = new System.Drawing.Point(435, 50);
            this.tx_EduDocName.Name = "tx_EduDocName";
            this.tx_EduDocName.Size = new System.Drawing.Size(188, 24);
            this.tx_EduDocName.StyleController = this.layoutControl1;
            this.tx_EduDocName.TabIndex = 0;
            // 
            // dt_EduDate
            // 
            this.dt_EduDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_EduDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_EduDate.Appearance.Options.UseBackColor = true;
            this.dt_EduDate.Appearance.Options.UseFont = true;
            this.dt_EduDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dt_EduDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dt_EduDate.Location = new System.Drawing.Point(106, 50);
            this.dt_EduDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_EduDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_EduDate.MinimumSize = new System.Drawing.Size(243, 26);
            this.dt_EduDate.Name = "dt_EduDate";
            this.dt_EduDate.Size = new System.Drawing.Size(243, 26);
            this.dt_EduDate.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcEduPlanDataList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.lcEduDocName,
            this.lcEduDate});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1050, 80);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(603, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(423, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcEduDocName
            // 
            this.lcEduDocName.Control = this.tx_EduDocName;
            this.lcEduDocName.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcEduDocName.CustomizationFormText = "교육계획서명";
            this.lcEduDocName.Location = new System.Drawing.Point(329, 0);
            this.lcEduDocName.Name = "lcEduDocName";
            this.lcEduDocName.Size = new System.Drawing.Size(274, 30);
            this.lcEduDocName.Text = "교육계획서명";
            this.lcEduDocName.TextSize = new System.Drawing.Size(78, 18);
            // 
            // lcEduDate
            // 
            this.lcEduDate.Control = this.dt_EduDate;
            this.lcEduDate.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcEduDate.CustomizationFormText = "교육일자";
            this.lcEduDate.Location = new System.Drawing.Point(0, 0);
            this.lcEduDate.Name = "lcEduDate";
            this.lcEduDate.Size = new System.Drawing.Size(329, 30);
            this.lcEduDate.Text = "교육일자";
            this.lcEduDate.TextSize = new System.Drawing.Size(78, 18);
            // 
            // lcEduPlanDataList
            // 
            this.lcEduPlanDataList.CustomizationFormText = "교육계획자료목록";
            this.lcEduPlanDataList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcEduPlanDataList.Location = new System.Drawing.Point(0, 80);
            this.lcEduPlanDataList.Name = "lcEduPlanDataList";
            this.lcEduPlanDataList.OptionsItemText.TextToControlDistance = 4;
            this.lcEduPlanDataList.Size = new System.Drawing.Size(1050, 567);
            this.lcEduPlanDataList.Text = "교육계획목록";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1026, 517);
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
            // XFQCT1800
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFQCT1800";
            this.Text = "XFSTD1100";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_EduDocName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduDocName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcEduPlanDataList)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControlGroup lcEduPlanDataList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit tx_EduDocName;
        private DevExpress.XtraLayout.LayoutControlItem lcEduDocName;
        private Service.Controls.DatePeriodEditEx dt_EduDate;
        private DevExpress.XtraLayout.LayoutControlItem lcEduDate;
    }
}