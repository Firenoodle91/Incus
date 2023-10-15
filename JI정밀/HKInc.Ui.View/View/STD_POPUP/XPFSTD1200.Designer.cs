namespace HKInc.Ui.View.View.STD_POPUP
{
    partial class XPFSTD1200
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFSTD1200));
            this.gridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lupDepartmentManager = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.tNSTD1200BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.treeListParentDepartment = new HKInc.Service.Controls.TreeListLookUpEditEx();
            this.treeListLookUpEditEx1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.chkUseFlag = new DevExpress.XtraEditors.CheckEdit();
            this.textDepartmentName = new DevExpress.XtraEditors.TextEdit();
            this.textDepartmentCode = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcUseFlag = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDepartmentCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDepartmentName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDepartmentManager = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcParentDepartment = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupDepartmentManager.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tNSTD1200BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListParentDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEditEx1TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseFlag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDepartmentName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDepartmentCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUseFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDepartmentCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDepartmentName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDepartmentManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcParentDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // gridLookUpEditEx1View
            // 
            this.gridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEditEx1View.Name = "gridLookUpEditEx1View";
            this.gridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lupDepartmentManager);
            this.layoutControl1.Controls.Add(this.treeListParentDepartment);
            this.layoutControl1.Controls.Add(this.chkUseFlag);
            this.layoutControl1.Controls.Add(this.textDepartmentName);
            this.layoutControl1.Controls.Add(this.textDepartmentCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(398, 147);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lupDepartmentManager
            // 
            this.lupDepartmentManager.Constraint = null;
            this.lupDepartmentManager.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tNSTD1200BindingSource, "DepartmentManager", true));
            this.lupDepartmentManager.DataSource = null;
            this.lupDepartmentManager.DisplayMember = "";
            this.lupDepartmentManager.isImeModeDisable = false;
            this.lupDepartmentManager.isRequired = false;
            this.lupDepartmentManager.Location = new System.Drawing.Point(85, 106);
            this.lupDepartmentManager.Name = "lupDepartmentManager";
            this.lupDepartmentManager.NullText = "";
            this.lupDepartmentManager.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lupDepartmentManager.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lupDepartmentManager.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDepartmentManager.Properties.NullText = "";
            this.lupDepartmentManager.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lupDepartmentManager.Size = new System.Drawing.Size(297, 24);
            this.lupDepartmentManager.StyleController = this.layoutControl1;
            this.lupDepartmentManager.TabIndex = 3;
            this.lupDepartmentManager.Value_1 = null;
            this.lupDepartmentManager.ValueMember = "";
            // 
            // tNSTD1200BindingSource
            // 
            this.tNSTD1200BindingSource.DataSource = typeof(HKInc.Ui.Model.Domain.VIEW.TN_STD1200);
            // 
            // searchGridLookUpEditEx1View
            // 
            this.searchGridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchGridLookUpEditEx1View.Name = "searchGridLookUpEditEx1View";
            this.searchGridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchGridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // treeListParentDepartment
            // 
            this.treeListParentDepartment.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tNSTD1200BindingSource, "ParentDepartmentCode", true));
            this.treeListParentDepartment.DataSource = null;
            this.treeListParentDepartment.DisplayMember = "";
            this.treeListParentDepartment.KeyFieldName = "ID";
            this.treeListParentDepartment.Location = new System.Drawing.Point(85, 76);
            this.treeListParentDepartment.Name = "treeListParentDepartment";
            this.treeListParentDepartment.ParentFieldName = "ParentID";
            this.treeListParentDepartment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.treeListParentDepartment.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.treeListParentDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.treeListParentDepartment.Properties.NullText = "";
            this.treeListParentDepartment.Properties.TreeList = this.treeListLookUpEditEx1TreeList;
            this.treeListParentDepartment.ShowColumns = false;
            this.treeListParentDepartment.Size = new System.Drawing.Size(297, 24);
            this.treeListParentDepartment.StyleController = this.layoutControl1;
            this.treeListParentDepartment.TabIndex = 2;
            this.treeListParentDepartment.ValueMember = "";
            // 
            // treeListLookUpEditEx1TreeList
            // 
            this.treeListLookUpEditEx1TreeList.Location = new System.Drawing.Point(0, 0);
            this.treeListLookUpEditEx1TreeList.Name = "treeListLookUpEditEx1TreeList";
            this.treeListLookUpEditEx1TreeList.OptionsView.ShowColumns = false;
            this.treeListLookUpEditEx1TreeList.OptionsView.ShowHorzLines = false;
            this.treeListLookUpEditEx1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            this.treeListLookUpEditEx1TreeList.OptionsView.ShowVertLines = false;
            this.treeListLookUpEditEx1TreeList.Size = new System.Drawing.Size(400, 200);
            this.treeListLookUpEditEx1TreeList.TabIndex = 0;
            // 
            // chkUseFlag
            // 
            this.chkUseFlag.AutoSizeInLayoutControl = true;
            this.chkUseFlag.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tNSTD1200BindingSource, "UseFlag", true));
            this.chkUseFlag.Location = new System.Drawing.Point(363, 16);
            this.chkUseFlag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkUseFlag.Name = "chkUseFlag";
            this.chkUseFlag.Properties.Caption = "";
            this.chkUseFlag.Properties.DisplayValueChecked = "Y";
            this.chkUseFlag.Properties.DisplayValueGrayed = "N";
            this.chkUseFlag.Properties.DisplayValueUnchecked = "N";
            this.chkUseFlag.Properties.ValueChecked = "Y";
            this.chkUseFlag.Properties.ValueGrayed = "N";
            this.chkUseFlag.Properties.ValueUnchecked = "N";
            this.chkUseFlag.Size = new System.Drawing.Size(19, 19);
            this.chkUseFlag.StyleController = this.layoutControl1;
            this.chkUseFlag.TabIndex = 4;
            // 
            // textDepartmentName
            // 
            this.textDepartmentName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tNSTD1200BindingSource, "DepartmentName", true));
            this.textDepartmentName.Location = new System.Drawing.Point(85, 46);
            this.textDepartmentName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textDepartmentName.Name = "textDepartmentName";
            this.textDepartmentName.Size = new System.Drawing.Size(297, 24);
            this.textDepartmentName.StyleController = this.layoutControl1;
            this.textDepartmentName.TabIndex = 1;
            // 
            // textDepartmentCode
            // 
            this.textDepartmentCode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tNSTD1200BindingSource, "DepartmentCode", true));
            this.textDepartmentCode.Location = new System.Drawing.Point(85, 16);
            this.textDepartmentCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textDepartmentCode.Name = "textDepartmentCode";
            this.textDepartmentCode.Properties.ReadOnly = true;
            this.textDepartmentCode.Size = new System.Drawing.Size(111, 24);
            this.textDepartmentCode.StyleController = this.layoutControl1;
            this.textDepartmentCode.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcUseFlag,
            this.lcDepartmentCode,
            this.lcDepartmentName,
            this.lcDepartmentManager,
            this.emptySpaceItem1,
            this.lcParentDepartment});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(398, 147);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcUseFlag
            // 
            this.lcUseFlag.Control = this.chkUseFlag;
            this.lcUseFlag.Location = new System.Drawing.Point(290, 0);
            this.lcUseFlag.Name = "lcUseFlag";
            this.lcUseFlag.Size = new System.Drawing.Size(82, 30);
            this.lcUseFlag.Text = "사용여부";
            this.lcUseFlag.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcUseFlag.TextSize = new System.Drawing.Size(52, 18);
            this.lcUseFlag.TextToControlDistance = 5;
            // 
            // lcDepartmentCode
            // 
            this.lcDepartmentCode.Control = this.textDepartmentCode;
            this.lcDepartmentCode.Location = new System.Drawing.Point(0, 0);
            this.lcDepartmentCode.Name = "lcDepartmentCode";
            this.lcDepartmentCode.Size = new System.Drawing.Size(186, 30);
            this.lcDepartmentCode.Text = "부서코드";
            this.lcDepartmentCode.TextSize = new System.Drawing.Size(65, 18);
            // 
            // lcDepartmentName
            // 
            this.lcDepartmentName.Control = this.textDepartmentName;
            this.lcDepartmentName.Location = new System.Drawing.Point(0, 30);
            this.lcDepartmentName.Name = "lcDepartmentName";
            this.lcDepartmentName.Size = new System.Drawing.Size(372, 30);
            this.lcDepartmentName.Text = "부서명";
            this.lcDepartmentName.TextSize = new System.Drawing.Size(65, 18);
            // 
            // lcDepartmentManager
            // 
            this.lcDepartmentManager.Control = this.lupDepartmentManager;
            this.lcDepartmentManager.Location = new System.Drawing.Point(0, 90);
            this.lcDepartmentManager.Name = "lcDepartmentManager";
            this.lcDepartmentManager.Size = new System.Drawing.Size(372, 31);
            this.lcDepartmentManager.Text = "부서관리자";
            this.lcDepartmentManager.TextSize = new System.Drawing.Size(65, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(186, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(104, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcParentDepartment
            // 
            this.lcParentDepartment.Control = this.treeListParentDepartment;
            this.lcParentDepartment.Location = new System.Drawing.Point(0, 60);
            this.lcParentDepartment.Name = "lcParentDepartment";
            this.lcParentDepartment.Size = new System.Drawing.Size(372, 30);
            this.lcParentDepartment.Text = "상위부서";
            this.lcParentDepartment.TextSize = new System.Drawing.Size(65, 18);
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // XPFSTD1200
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 220);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFSTD1200";
            this.Text = "XPF1200";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lupDepartmentManager.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tNSTD1200BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListParentDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEditEx1TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseFlag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDepartmentName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDepartmentCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUseFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDepartmentCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDepartmentName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDepartmentManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcParentDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.BindingSource tNSTD1200BindingSource;
        private Service.Controls.TreeListLookUpEditEx treeListParentDepartment;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEditEx1TreeList;
        private DevExpress.XtraEditors.CheckEdit chkUseFlag;
        private DevExpress.XtraEditors.TextEdit textDepartmentName;
        private DevExpress.XtraEditors.TextEdit textDepartmentCode;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lcUseFlag;
        private DevExpress.XtraLayout.LayoutControlItem lcParentDepartment;
        private DevExpress.XtraLayout.LayoutControlItem lcDepartmentCode;
        private DevExpress.XtraLayout.LayoutControlItem lcDepartmentName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Service.Controls.SearchLookUpEditEx lupDepartmentManager;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcDepartmentManager;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}