namespace HKInc.Ui.View.View.POP_POPUP
{
    partial class XPFINSPECTION_FINAL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFINSPECTION_FINAL));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_CheckId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcInspectionAddInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCheckId2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcInspFinalList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.btn_file = new DevExpress.XtraEditors.ButtonEdit();
            this.lcFileName = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CheckId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInspectionAddInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckId2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInspFinalList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_file.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFileName)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_file);
            this.layoutControl1.Controls.Add(this.lup_CheckId);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(933, 462);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_CheckId
            // 
            this.lup_CheckId.Constraint = null;
            this.lup_CheckId.DataSource = null;
            this.lup_CheckId.DisplayMember = "";
            this.lup_CheckId.isImeModeDisable = false;
            this.lup_CheckId.isRequired = false;
            this.lup_CheckId.Location = new System.Drawing.Point(22, 58);
            this.lup_CheckId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_CheckId.Name = "lup_CheckId";
            this.lup_CheckId.NullText = "";
            this.lup_CheckId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_CheckId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_CheckId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_CheckId.Properties.NullText = "";
            this.lup_CheckId.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_CheckId.Size = new System.Drawing.Size(166, 20);
            this.lup_CheckId.StyleController = this.layoutControl1;
            this.lup_CheckId.TabIndex = 0;
            this.lup_CheckId.Value_1 = null;
            this.lup_CheckId.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.DetailHeight = 272;
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx1
            // 
            this.gridEx1.Appearance.Options.UseTextOptions = true;
            this.gridEx1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(22, 123);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(889, 319);
            this.gridEx1.TabIndex = 5;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcInspectionAddInfo,
            this.lcInspFinalList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(933, 462);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcInspectionAddInfo
            // 
            this.lcInspectionAddInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCheckId2,
            this.emptySpaceItem1,
            this.lcFileName});
            this.lcInspectionAddInfo.Location = new System.Drawing.Point(0, 0);
            this.lcInspectionAddInfo.Name = "lcInspectionAddInfo";
            this.lcInspectionAddInfo.OptionsItemText.TextToControlDistance = 4;
            this.lcInspectionAddInfo.Size = new System.Drawing.Size(915, 82);
            // 
            // lcCheckId2
            // 
            this.lcCheckId2.AppearanceItemCaption.BackColor = System.Drawing.Color.Yellow;
            this.lcCheckId2.AppearanceItemCaption.Options.UseBackColor = true;
            this.lcCheckId2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcCheckId2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcCheckId2.Control = this.lup_CheckId;
            this.lcCheckId2.Location = new System.Drawing.Point(0, 0);
            this.lcCheckId2.Name = "lcCheckId2";
            this.lcCheckId2.Size = new System.Drawing.Size(170, 41);
            this.lcCheckId2.Text = "검사자";
            this.lcCheckId2.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcCheckId2.TextSize = new System.Drawing.Size(56, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(341, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(552, 41);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcInspFinalList
            // 
            this.lcInspFinalList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.lcInspFinalList.Location = new System.Drawing.Point(0, 82);
            this.lcInspFinalList.Name = "lcInspFinalList";
            this.lcInspFinalList.OptionsItemText.TextToControlDistance = 4;
            this.lcInspFinalList.Size = new System.Drawing.Size(915, 364);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.gridEx1;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(893, 323);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // btn_file
            // 
            this.btn_file.Location = new System.Drawing.Point(192, 58);
            this.btn_file.Name = "btn_file";
            this.btn_file.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btn_file.Size = new System.Drawing.Size(167, 20);
            this.btn_file.StyleController = this.layoutControl1;
            this.btn_file.TabIndex = 6;
            // 
            // lcFileName
            // 
            this.lcFileName.Control = this.btn_file;
            this.lcFileName.Location = new System.Drawing.Point(170, 0);
            this.lcFileName.Name = "lcFileName";
            this.lcFileName.Size = new System.Drawing.Size(171, 41);
            this.lcFileName.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcFileName.TextSize = new System.Drawing.Size(56, 14);
            // 
            // XPFINSPECTION_FINAL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 509);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "XPFINSPECTION_FINAL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "품질정보등록";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_CheckId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInspectionAddInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckId2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInspFinalList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_file.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFileName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private Service.Controls.SearchLookUpEditEx lup_CheckId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcCheckId2;
        private DevExpress.XtraLayout.LayoutControlGroup lcInspectionAddInfo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcInspFinalList;
        private DevExpress.XtraEditors.ButtonEdit btn_file;
        private DevExpress.XtraLayout.LayoutControlItem lcFileName;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}