﻿namespace HKInc.Ui.View.UPH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFUPH1100));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lupItemCode = new HKInc.Service.Controls.GridLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lupWorkId = new HKInc.Service.Controls.GridLookUpEditEx();
            this.gridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.datePeriodEditEx1 = new HKInc.Service.Controls.DatePeriodEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lupMachineCode = new HKInc.Service.Controls.GridLookUpEditEx();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupWorkId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMachineCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lupItemCode);
            this.layoutControl1.Controls.Add(this.lupWorkId);
            this.layoutControl1.Controls.Add(this.datePeriodEditEx1);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lupMachineCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lupItemCode
            // 
            this.lupItemCode.Constraint = null;
            this.lupItemCode.DataSource = null;
            this.lupItemCode.DisplayMember = "";
            this.lupItemCode.isRequired = false;
            this.lupItemCode.Location = new System.Drawing.Point(569, 50);
            this.lupItemCode.Name = "lupItemCode";
            this.lupItemCode.NullText = "";
            this.lupItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lupItemCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lupItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupItemCode.Properties.NullText = "";
            this.lupItemCode.Properties.PopupView = this.gridView1;
            this.lupItemCode.SelectedIndex = -1;
            this.lupItemCode.Size = new System.Drawing.Size(166, 24);
            this.lupItemCode.StyleController = this.layoutControl1;
            this.lupItemCode.TabIndex = 9;
            this.lupItemCode.Value_1 = null;
            this.lupItemCode.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lupWorkId
            // 
            this.lupWorkId.Constraint = null;
            this.lupWorkId.DataSource = null;
            this.lupWorkId.DisplayMember = "";
            this.lupWorkId.isRequired = false;
            this.lupWorkId.Location = new System.Drawing.Point(782, 50);
            this.lupWorkId.Name = "lupWorkId";
            this.lupWorkId.NullText = "";
            this.lupWorkId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lupWorkId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lupWorkId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupWorkId.Properties.NullText = "";
            this.lupWorkId.Properties.PopupView = this.gridLookUpEditEx1View;
            this.lupWorkId.SelectedIndex = -1;
            this.lupWorkId.Size = new System.Drawing.Size(166, 24);
            this.lupWorkId.StyleController = this.layoutControl1;
            this.lupWorkId.TabIndex = 8;
            this.lupWorkId.Value_1 = null;
            this.lupWorkId.ValueMember = "";
            // 
            // gridLookUpEditEx1View
            // 
            this.gridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEditEx1View.Name = "gridLookUpEditEx1View";
            this.gridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // datePeriodEditEx1
            // 
            this.datePeriodEditEx1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.datePeriodEditEx1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePeriodEditEx1.Appearance.Options.UseBackColor = true;
            this.datePeriodEditEx1.Appearance.Options.UseFont = true;
            this.datePeriodEditEx1.EditFrValue = new System.DateTime(((long)(0)));
            this.datePeriodEditEx1.EditToValue = new System.DateTime(((long)(0)));
            this.datePeriodEditEx1.Location = new System.Drawing.Point(67, 50);
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
            this.gridEx1.Location = new System.Drawing.Point(24, 130);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 513);
            this.gridEx1.TabIndex = 6;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lupMachineCode
            // 
            this.lupMachineCode.Constraint = null;
            this.lupMachineCode.DataSource = null;
            this.lupMachineCode.DisplayMember = "";
            this.lupMachineCode.isRequired = false;
            this.lupMachineCode.Location = new System.Drawing.Point(357, 50);
            this.lupMachineCode.Name = "lupMachineCode";
            this.lupMachineCode.NullText = "";
            this.lupMachineCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lupMachineCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lupMachineCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMachineCode.Properties.NullText = "";
            this.lupMachineCode.Properties.PopupView = this.gridView2;
            this.lupMachineCode.SelectedIndex = -1;
            this.lupMachineCode.Size = new System.Drawing.Size(165, 24);
            this.lupMachineCode.StyleController = this.layoutControl1;
            this.lupMachineCode.TabIndex = 9;
            this.lupMachineCode.Value_1 = null;
            this.lupMachineCode.ValueMember = "";
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1050, 80);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(928, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(98, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.datePeriodEditEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(290, 30);
            this.layoutControlItem2.Text = "생산일";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(39, 18);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lupWorkId;
            this.layoutControlItem4.Location = new System.Drawing.Point(715, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(213, 30);
            this.layoutControlItem4.Text = "작업자";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(39, 18);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lupItemCode;
            this.layoutControlItem5.Location = new System.Drawing.Point(502, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(213, 30);
            this.layoutControlItem5.Text = "품목";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(39, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lupMachineCode;
            this.layoutControlItem1.CustomizationFormText = "품목";
            this.layoutControlItem1.Location = new System.Drawing.Point(290, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(212, 30);
            this.layoutControlItem1.Text = "설비";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(39, 18);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "UPH 이력";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 80);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup3.Size = new System.Drawing.Size(1050, 567);
            this.layoutControlGroup3.Text = "UPH 이력";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1026, 517);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lupItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupWorkId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMachineCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
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
        private Service.Controls.GridLookUpEditEx lupWorkId;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEditEx1View;
        private Service.Controls.DatePeriodEditEx datePeriodEditEx1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.GridLookUpEditEx lupItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Service.Controls.GridLookUpEditEx lupMachineCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}