﻿namespace HKInc.Ui.View.ORD
{
    partial class XFORD1000
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFORD1000));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lupCustcode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.dateOrderDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupCustcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lupCustcode);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.dateOrderDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lupCustcode
            // 
            this.lupCustcode.Constraint = null;
            this.lupCustcode.DataSource = null;
            this.lupCustcode.DisplayMember = "";
            this.lupCustcode.isRequired = false;
            this.lupCustcode.Location = new System.Drawing.Point(366, 56);
            this.lupCustcode.Name = "lupCustcode";
            this.lupCustcode.NullText = "";
            this.lupCustcode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lupCustcode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lupCustcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCustcode.Properties.NullText = "";
            this.lupCustcode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lupCustcode.Size = new System.Drawing.Size(191, 24);
            this.lupCustcode.StyleController = this.layoutControl1;
            this.lupCustcode.TabIndex = 1;
            this.lupCustcode.Value_1 = null;
            this.lupCustcode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(31, 401);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(1008, 221);
            this.gridEx2.TabIndex = 3;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 143);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 197);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // dateOrderDate
            // 
            this.dateOrderDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dateOrderDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOrderDate.Appearance.Options.UseBackColor = true;
            this.dateOrderDate.Appearance.Options.UseFont = true;
            this.dateOrderDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dateOrderDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dateOrderDate.Location = new System.Drawing.Point(74, 56);
            this.dateOrderDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dateOrderDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dateOrderDate.MinimumSize = new System.Drawing.Size(243, 26);
            this.dateOrderDate.Name = "dateOrderDate";
            this.dateOrderDate.Size = new System.Drawing.Size(243, 26);
            this.dateOrderDate.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1044, 87);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateOrderDate;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(292, 32);
            this.layoutControlItem1.Text = "수주일";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(39, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(532, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(482, 32);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lupCustcode;
            this.layoutControlItem5.Location = new System.Drawing.Point(292, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(240, 32);
            this.layoutControlItem5.Text = "수주처";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(39, 18);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 87);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup3.Size = new System.Drawing.Size(1044, 258);
            this.layoutControlGroup3.Text = "수주등록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1014, 203);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 345);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup4.Size = new System.Drawing.Size(1044, 282);
            this.layoutControlGroup4.Text = "수주상세";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1014, 227);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // XFORD1000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFORD1000";
            this.Text = "XFORD1000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lupCustcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dateOrderDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lupCustcode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}