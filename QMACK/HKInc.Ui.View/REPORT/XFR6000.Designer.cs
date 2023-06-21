namespace HKInc.Ui.View.REPORT
{
    partial class XFR6000
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFR6000));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.slup_Machine_group_code = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.설비그룹 = new DevExpress.XtraLayout.LayoutControlItem();
            this.slup_Machine_code = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.설비명 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEditEx2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slup_Machine_group_code.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.설비그룹)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slup_Machine_code.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.설비명)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.slup_Machine_code);
            this.layoutControl1.Controls.Add(this.slup_Machine_group_code);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // slup_Machine_group_code
            // 
            this.slup_Machine_group_code.Constraint = null;
            this.slup_Machine_group_code.DataSource = null;
            this.slup_Machine_group_code.DisplayMember = "";
            this.slup_Machine_group_code.isImeModeDisable = false;
            this.slup_Machine_group_code.isRequired = false;
            this.slup_Machine_group_code.Location = new System.Drawing.Point(80, 50);
            this.slup_Machine_group_code.Name = "slup_Machine_group_code";
            this.slup_Machine_group_code.NullText = "";
            this.slup_Machine_group_code.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.slup_Machine_group_code.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.slup_Machine_group_code.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slup_Machine_group_code.Properties.NullText = "";
            this.slup_Machine_group_code.Properties.PopupView = this.searchLookUpEditEx1View;
            this.slup_Machine_group_code.Size = new System.Drawing.Size(202, 24);
            this.slup_Machine_group_code.StyleController = this.layoutControl1;
            this.slup_Machine_group_code.TabIndex = 7;
            this.slup_Machine_group_code.Value_1 = null;
            this.slup_Machine_group_code.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 128);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 515);
            this.gridEx1.TabIndex = 6;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.설비그룹,
            this.설비명,
            this.emptySpaceItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1050, 78);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "설비 목록";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 78);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup3.Size = new System.Drawing.Size(1050, 569);
            this.layoutControlGroup3.Text = "설비 목록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1026, 519);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // 설비그룹
            // 
            this.설비그룹.Control = this.slup_Machine_group_code;
            this.설비그룹.Location = new System.Drawing.Point(0, 0);
            this.설비그룹.Name = "설비그룹";
            this.설비그룹.Size = new System.Drawing.Size(262, 28);
            this.설비그룹.TextSize = new System.Drawing.Size(52, 18);
            // 
            // slup_Machine_code
            // 
            this.slup_Machine_code.Constraint = null;
            this.slup_Machine_code.DataSource = null;
            this.slup_Machine_code.DisplayMember = "";
            this.slup_Machine_code.isImeModeDisable = false;
            this.slup_Machine_code.isRequired = false;
            this.slup_Machine_code.Location = new System.Drawing.Point(342, 50);
            this.slup_Machine_code.Name = "slup_Machine_code";
            this.slup_Machine_code.NullText = "";
            this.slup_Machine_code.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slup_Machine_code.Properties.NullText = "";
            this.slup_Machine_code.Properties.PopupView = this.searchLookUpEditEx2View;
            this.slup_Machine_code.Size = new System.Drawing.Size(197, 24);
            this.slup_Machine_code.StyleController = this.layoutControl1;
            this.slup_Machine_code.TabIndex = 8;
            this.slup_Machine_code.Value_1 = null;
            this.slup_Machine_code.ValueMember = "";
            // 
            // 설비명
            // 
            this.설비명.Control = this.slup_Machine_code;
            this.설비명.Location = new System.Drawing.Point(262, 0);
            this.설비명.Name = "설비명";
            this.설비명.Size = new System.Drawing.Size(257, 28);
            this.설비명.TextSize = new System.Drawing.Size(52, 18);
            // 
            // searchLookUpEditEx2View
            // 
            this.searchLookUpEditEx2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx2View.Name = "searchLookUpEditEx2View";
            this.searchLookUpEditEx2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx2View.OptionsView.ShowGroupPanel = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(519, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(507, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XFR6000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFR6000";
            this.Text = "XFR6000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slup_Machine_group_code.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.설비그룹)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slup_Machine_code.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.설비명)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
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
        private Service.Controls.SearchLookUpEditEx slup_Machine_group_code;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem 설비그룹;
        private Service.Controls.SearchLookUpEditEx slup_Machine_code;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx2View;
        private DevExpress.XtraLayout.LayoutControlItem 설비명;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}