namespace HKInc.Ui.View.View.MPS
{
    partial class XFMPS1201
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFMPS1201));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.spin_ChangeDay = new DevExpress.XtraEditors.SpinEdit();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.tx_WorkNo = new DevExpress.XtraEditors.TextEdit();
            this.dt_WorkDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcWorkDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcWorkNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcWorkList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcChangeStandardDay = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_ChangeDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_WorkNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcChangeStandardDay)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spin_ChangeDay);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_WorkNo);
            this.layoutControl1.Controls.Add(this.dt_WorkDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // spin_ChangeDay
            // 
            this.spin_ChangeDay.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_ChangeDay.Location = new System.Drawing.Point(109, 141);
            this.spin_ChangeDay.Name = "spin_ChangeDay";
            this.spin_ChangeDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_ChangeDay.Size = new System.Drawing.Size(56, 24);
            this.spin_ChangeDay.StyleController = this.layoutControl1;
            this.spin_ChangeDay.TabIndex = 2;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 171);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 451);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // tx_WorkNo
            // 
            this.tx_WorkNo.Location = new System.Drawing.Point(393, 56);
            this.tx_WorkNo.Name = "tx_WorkNo";
            this.tx_WorkNo.Size = new System.Drawing.Size(139, 24);
            this.tx_WorkNo.StyleController = this.layoutControl1;
            this.tx_WorkNo.TabIndex = 1;
            // 
            // dt_WorkDate
            // 
            this.dt_WorkDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_WorkDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_WorkDate.Appearance.Options.UseBackColor = true;
            this.dt_WorkDate.Appearance.Options.UseFont = true;
            this.dt_WorkDate.EditFrValue = new System.DateTime(2020, 1, 19, 0, 0, 0, 0);
            this.dt_WorkDate.EditToValue = new System.DateTime(2020, 2, 19, 23, 59, 59, 990);
            this.dt_WorkDate.Location = new System.Drawing.Point(109, 56);
            this.dt_WorkDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_WorkDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_WorkDate.MinimumSize = new System.Drawing.Size(200, 20);
            this.dt_WorkDate.Name = "dt_WorkDate";
            this.dt_WorkDate.Size = new System.Drawing.Size(200, 20);
            this.dt_WorkDate.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcWorkList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcWorkDate,
            this.lcWorkNo,
            this.emptySpaceItem2});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1044, 85);
            // 
            // lcWorkDate
            // 
            this.lcWorkDate.Control = this.dt_WorkDate;
            this.lcWorkDate.Location = new System.Drawing.Point(0, 0);
            this.lcWorkDate.Name = "lcWorkDate";
            this.lcWorkDate.Size = new System.Drawing.Size(284, 30);
            this.lcWorkDate.TextSize = new System.Drawing.Size(74, 18);
            // 
            // lcWorkNo
            // 
            this.lcWorkNo.Control = this.tx_WorkNo;
            this.lcWorkNo.Location = new System.Drawing.Point(284, 0);
            this.lcWorkNo.Name = "lcWorkNo";
            this.lcWorkNo.Size = new System.Drawing.Size(223, 30);
            this.lcWorkNo.TextSize = new System.Drawing.Size(74, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(507, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(507, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcWorkList
            // 
            this.lcWorkList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.lcChangeStandardDay});
            this.lcWorkList.Location = new System.Drawing.Point(0, 85);
            this.lcWorkList.Name = "lcWorkList";
            this.lcWorkList.OptionsItemText.TextToControlDistance = 4;
            this.lcWorkList.Size = new System.Drawing.Size(1044, 542);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridEx1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1014, 457);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.emptySpaceItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.emptySpaceItem1.Location = new System.Drawing.Point(140, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(874, 30);
            this.emptySpaceItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 10);
            this.emptySpaceItem1.Text = "※ 체크한 항목만 일괄변경됩니다.";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(74, 0);
            this.emptySpaceItem1.TextVisible = true;
            // 
            // lcChangeStandardDay
            // 
            this.lcChangeStandardDay.Control = this.spin_ChangeDay;
            this.lcChangeStandardDay.Location = new System.Drawing.Point(0, 0);
            this.lcChangeStandardDay.MaxSize = new System.Drawing.Size(140, 30);
            this.lcChangeStandardDay.MinSize = new System.Drawing.Size(140, 30);
            this.lcChangeStandardDay.Name = "lcChangeStandardDay";
            this.lcChangeStandardDay.Size = new System.Drawing.Size(140, 30);
            this.lcChangeStandardDay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcChangeStandardDay.Text = "변경기준일";
            this.lcChangeStandardDay.TextSize = new System.Drawing.Size(74, 18);
            // 
            // XFMPS1201
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFMPS1201";
            this.Text = "XFMPS1201";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spin_ChangeDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_WorkNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcChangeStandardDay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SpinEdit spin_ChangeDay;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraEditors.TextEdit tx_WorkNo;
        private Service.Controls.DatePeriodEditEx dt_WorkDate;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcWorkDate;
        private DevExpress.XtraLayout.LayoutControlItem lcWorkNo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcWorkList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lcChangeStandardDay;
    }
}