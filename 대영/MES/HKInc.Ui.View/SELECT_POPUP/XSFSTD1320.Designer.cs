namespace HKInc.Ui.View.SELECT_POPUP
{
    partial class XSFSTD1320
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSFSTD1320));
            this.treeListEx1 = new HKInc.Service.Controls.TreeListEx();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_Typecode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcTypeCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcBomList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TreeListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Typecode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcTypeCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBomList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // treeListEx1
            // 
            this.treeListEx1.DataSource = null;
            this.treeListEx1.KeyFieldName = "ID";
            this.treeListEx1.Location = new System.Drawing.Point(24, 128);
            this.treeListEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeListEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.treeListEx1.Name = "treeListEx1";
            this.treeListEx1.ParentFieldName = "ParentID";
            this.treeListEx1.Size = new System.Drawing.Size(1022, 515);
            this.treeListEx1.TabIndex = 2;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_Typecode);
            this.layoutControl1.Controls.Add(this.treeListEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_Typecode
            // 
            this.lup_Typecode.Constraint = null;
            this.lup_Typecode.DataSource = null;
            this.lup_Typecode.DisplayMember = "";
            this.lup_Typecode.isImeModeDisable = false;
            this.lup_Typecode.isRequired = false;
            this.lup_Typecode.Location = new System.Drawing.Point(54, 50);
            this.lup_Typecode.Name = "lup_Typecode";
            this.lup_Typecode.NullText = "";
            this.lup_Typecode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Typecode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Typecode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Typecode.Properties.NullText = "";
            this.lup_Typecode.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_Typecode.Size = new System.Drawing.Size(177, 24);
            this.lup_Typecode.StyleController = this.layoutControl1;
            this.lup_Typecode.TabIndex = 0;
            this.lup_Typecode.Value_1 = null;
            this.lup_Typecode.ValueMember = "";
            // 
            // searchGridLookUpEditEx1View
            // 
            this.searchGridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchGridLookUpEditEx1View.Name = "searchGridLookUpEditEx1View";
            this.searchGridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchGridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.lcBomList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcTypeCode,
            this.emptySpaceItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1050, 78);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // lcTypeCode
            // 
            this.lcTypeCode.Control = this.lup_Typecode;
            this.lcTypeCode.Location = new System.Drawing.Point(0, 0);
            this.lcTypeCode.Name = "lcTypeCode";
            this.lcTypeCode.Size = new System.Drawing.Size(211, 28);
            this.lcTypeCode.Text = "품목";
            this.lcTypeCode.TextSize = new System.Drawing.Size(26, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(211, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(815, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcBomList
            // 
            this.lcBomList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcBomList.Location = new System.Drawing.Point(0, 78);
            this.lcBomList.Name = "lcBomList";
            this.lcBomList.OptionsItemText.TextToControlDistance = 4;
            this.lcBomList.Size = new System.Drawing.Size(1050, 569);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeListEx1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1026, 519);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // XSFSTD1320
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XSFSTD1320";
            this.Text = "BOM 타입";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TreeListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Typecode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcTypeCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBomList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Service.Controls.TreeListEx treeListEx1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.SearchLookUpEditEx lup_Typecode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem lcTypeCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcBomList;
    }
}