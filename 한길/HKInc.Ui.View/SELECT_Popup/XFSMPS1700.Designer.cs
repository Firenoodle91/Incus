﻿namespace HKInc.Ui.View.SELECT_Popup
{
    partial class XFSMPS1700
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFSMPS1700));
            this.BaseFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.dp_date = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lupItemCode = new HKInc.Service.Controls.GridLookUpEditEx();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lupMachineCode = new HKInc.Service.Controls.GridLookUpEditEx();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseFormlayoutControl1ConvertedLayout)).BeginInit();
            this.BaseFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMachineCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // BaseFormlayoutControl1ConvertedLayout
            // 
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.lupMachineCode);
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.lupItemCode);
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.gridEx1);
            this.BaseFormlayoutControl1ConvertedLayout.Controls.Add(this.dp_date);
            this.BaseFormlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseFormlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 39);
            this.BaseFormlayoutControl1ConvertedLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BaseFormlayoutControl1ConvertedLayout.Name = "BaseFormlayoutControl1ConvertedLayout";
            this.BaseFormlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.BaseFormlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(1056, 580);
            this.BaseFormlayoutControl1ConvertedLayout.TabIndex = 4;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 143);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(994, 406);
            this.gridEx1.TabIndex = 6;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // dp_date
            // 
            this.dp_date.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dp_date.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dp_date.Appearance.Options.UseBackColor = true;
            this.dp_date.Appearance.Options.UseFont = true;
            this.dp_date.EditFrValue = new System.DateTime(2019, 2, 13, 0, 0, 0, 0);
            this.dp_date.EditToValue = new System.DateTime(2019, 3, 13, 23, 59, 59, 990);
            this.dp_date.Location = new System.Drawing.Point(73, 56);
            this.dp_date.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dp_date.MaximumSize = new System.Drawing.Size(200, 20);
            this.dp_date.MinimumSize = new System.Drawing.Size(243, 26);
            this.dp_date.Name = "dp_date";
            this.dp_date.Size = new System.Drawing.Size(243, 26);
            this.dp_date.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1056, 580);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1030, 87);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dp_date;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(291, 32);
            this.layoutControlItem1.Text = "생산일";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(39, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(487, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(513, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 87);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1030, 467);
            this.layoutControlGroup3.Text = "정보";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1000, 412);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lupItemCode
            // 
            this.lupItemCode.Constraint = null;
            this.lupItemCode.DataSource = null;
            this.lupItemCode.DisplayMember = "";
            this.lupItemCode.isRequired = false;
            this.lupItemCode.Location = new System.Drawing.Point(364, 56);
            this.lupItemCode.Name = "lupItemCode";
            this.lupItemCode.NullText = "";
            this.lupItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupItemCode.Properties.NullText = "";
            this.lupItemCode.Properties.PopupView = this.gridLookUpEditEx1View;
            this.lupItemCode.SelectedIndex = -1;
            this.lupItemCode.Size = new System.Drawing.Size(50, 24);
            this.lupItemCode.StyleController = this.BaseFormlayoutControl1ConvertedLayout;
            this.lupItemCode.TabIndex = 7;
            this.lupItemCode.Value_1 = null;
            this.lupItemCode.ValueMember = "";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lupItemCode;
            this.layoutControlItem2.Location = new System.Drawing.Point(291, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(98, 32);
            this.layoutControlItem2.Text = "품목";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(39, 18);
            // 
            // gridLookUpEditEx1View
            // 
            this.gridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEditEx1View.Name = "gridLookUpEditEx1View";
            this.gridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // lupMachineCode
            // 
            this.lupMachineCode.Constraint = null;
            this.lupMachineCode.DataSource = null;
            this.lupMachineCode.DisplayMember = "";
            this.lupMachineCode.isRequired = false;
            this.lupMachineCode.Location = new System.Drawing.Point(462, 56);
            this.lupMachineCode.Name = "lupMachineCode";
            this.lupMachineCode.NullText = "";
            this.lupMachineCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMachineCode.Properties.NullText = "";
            this.lupMachineCode.Properties.PopupView = this.gridView1;
            this.lupMachineCode.SelectedIndex = -1;
            this.lupMachineCode.Size = new System.Drawing.Size(50, 24);
            this.lupMachineCode.StyleController = this.BaseFormlayoutControl1ConvertedLayout;
            this.lupMachineCode.TabIndex = 8;
            this.lupMachineCode.Value_1 = null;
            this.lupMachineCode.ValueMember = "";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lupMachineCode;
            this.layoutControlItem4.Location = new System.Drawing.Point(389, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(98, 32);
            this.layoutControlItem4.Text = "설비";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(39, 18);
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // XFSMPS1700
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 653);
            this.Controls.Add(this.BaseFormlayoutControl1ConvertedLayout);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "XFSMPS1700";
            this.Text = "생산품 정보";
            this.Controls.SetChildIndex(this.BaseFormlayoutControl1ConvertedLayout, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseFormlayoutControl1ConvertedLayout)).EndInit();
            this.BaseFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMachineCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl BaseFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dp_date;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Service.Controls.GridLookUpEditEx lupMachineCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Service.Controls.GridLookUpEditEx lupItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}