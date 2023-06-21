namespace HKInc.Ui.View.View.POP_POPUP
{
    partial class XPFMACHINECHECK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFMACHINECHECK));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx_POP();
            this.lup_Machine = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.lup_CheckId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pic_CheckPointImage = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcMachine = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcCheckPointImage = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCheckList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCheckId = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Machine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CheckId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_CheckPointImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckPointImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_Machine);
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.btn_Save);
            this.layoutControl1.Controls.Add(this.lup_CheckId);
            this.layoutControl1.Controls.Add(this.pic_CheckPointImage);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(820, 486);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(22, 191);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(776, 227);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_Machine
            // 
            this.lup_Machine.Constraint = null;
            this.lup_Machine.DataSource = null;
            this.lup_Machine.DisplayMember = "";
            this.lup_Machine.isImeModeDisable = false;
            this.lup_Machine.isRequired = false;
            this.lup_Machine.Location = new System.Drawing.Point(22, 66);
            this.lup_Machine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_Machine.Name = "lup_Machine";
            this.lup_Machine.NullText = "";
            this.lup_Machine.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Machine.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lup_Machine.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Machine.Properties.Appearance.Options.UseFont = true;
            this.lup_Machine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Machine.Properties.NullText = "";
            this.lup_Machine.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Machine.Size = new System.Drawing.Size(149, 28);
            this.lup_Machine.StyleController = this.layoutControl1;
            this.lup_Machine.TabIndex = 0;
            this.lup_Machine.Value_1 = null;
            this.lup_Machine.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.DetailHeight = 272;
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseTextOptions = true;
            this.btn_Cancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_Cancel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_Cancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.ImageOptions.Image")));
            this.btn_Cancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel.Location = new System.Drawing.Point(404, 432);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(405, 44);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "취소";
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Appearance.Options.UseTextOptions = true;
            this.btn_Save.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_Save.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.ImageOptions.Image")));
            this.btn_Save.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Save.Location = new System.Drawing.Point(11, 432);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(389, 44);
            this.btn_Save.StyleController = this.layoutControl1;
            this.btn_Save.TabIndex = 6;
            this.btn_Save.Text = "저장";
            // 
            // lup_CheckId
            // 
            this.lup_CheckId.Constraint = null;
            this.lup_CheckId.DataSource = null;
            this.lup_CheckId.DisplayMember = "";
            this.lup_CheckId.isImeModeDisable = false;
            this.lup_CheckId.isRequired = false;
            this.lup_CheckId.Location = new System.Drawing.Point(104, 159);
            this.lup_CheckId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_CheckId.Name = "lup_CheckId";
            this.lup_CheckId.NullText = "";
            this.lup_CheckId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_CheckId.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lup_CheckId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_CheckId.Properties.Appearance.Options.UseFont = true;
            this.lup_CheckId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_CheckId.Properties.NullText = "";
            this.lup_CheckId.Properties.PopupView = this.gridView1;
            this.lup_CheckId.Size = new System.Drawing.Size(116, 28);
            this.lup_CheckId.StyleController = this.layoutControl1;
            this.lup_CheckId.TabIndex = 1;
            this.lup_CheckId.Value_1 = null;
            this.lup_CheckId.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 272;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // pic_CheckPointImage
            // 
            this.pic_CheckPointImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pic_CheckPointImage.Location = new System.Drawing.Point(175, 66);
            this.pic_CheckPointImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pic_CheckPointImage.Name = "pic_CheckPointImage";
            this.pic_CheckPointImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_CheckPointImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_CheckPointImage.Size = new System.Drawing.Size(112, 48);
            this.pic_CheckPointImage.StyleController = this.layoutControl1;
            this.pic_CheckPointImage.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.lcCheckList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(820, 486);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.AppearanceGroup.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcCondition.AppearanceGroup.Options.UseFont = true;
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcMachine,
            this.emptySpaceItem2,
            this.lcCheckPointImage});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(802, 118);
            // 
            // lcMachine
            // 
            this.lcMachine.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcMachine.AppearanceItemCaption.Options.UseFont = true;
            this.lcMachine.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcMachine.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcMachine.Control = this.lup_Machine;
            this.lcMachine.Location = new System.Drawing.Point(0, 0);
            this.lcMachine.MinSize = new System.Drawing.Size(128, 56);
            this.lcMachine.Name = "lcMachine";
            this.lcMachine.Size = new System.Drawing.Size(153, 77);
            this.lcMachine.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcMachine.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcMachine.TextSize = new System.Drawing.Size(112, 21);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(269, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(511, 77);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcCheckPointImage
            // 
            this.lcCheckPointImage.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lcCheckPointImage.AppearanceItemCaption.Options.UseFont = true;
            this.lcCheckPointImage.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcCheckPointImage.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcCheckPointImage.Control = this.pic_CheckPointImage;
            this.lcCheckPointImage.CustomizationFormText = "품목한도사진";
            this.lcCheckPointImage.Location = new System.Drawing.Point(153, 0);
            this.lcCheckPointImage.Name = "lcCheckPointImage";
            this.lcCheckPointImage.Size = new System.Drawing.Size(116, 77);
            this.lcCheckPointImage.Text = "점검포인트사진";
            this.lcCheckPointImage.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcCheckPointImage.TextSize = new System.Drawing.Size(112, 21);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Save;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 422);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(80, 36);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(393, 48);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem1";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_Cancel;
            this.layoutControlItem1.Location = new System.Drawing.Point(393, 422);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(80, 36);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(409, 48);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcCheckList
            // 
            this.lcCheckList.AppearanceGroup.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcCheckList.AppearanceGroup.Options.UseFont = true;
            this.lcCheckList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.lcCheckId,
            this.emptySpaceItem1});
            this.lcCheckList.Location = new System.Drawing.Point(0, 118);
            this.lcCheckList.Name = "lcCheckList";
            this.lcCheckList.OptionsItemText.TextToControlDistance = 4;
            this.lcCheckList.Size = new System.Drawing.Size(802, 304);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(780, 231);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lcCheckId
            // 
            this.lcCheckId.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcCheckId.AppearanceItemCaption.Options.UseFont = true;
            this.lcCheckId.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcCheckId.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcCheckId.Control = this.lup_CheckId;
            this.lcCheckId.CustomizationFormText = "lcMachine";
            this.lcCheckId.Location = new System.Drawing.Point(0, 0);
            this.lcCheckId.Name = "lcCheckId";
            this.lcCheckId.Size = new System.Drawing.Size(202, 32);
            this.lcCheckId.Text = "lcMachine";
            this.lcCheckId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcCheckId.TextLocation = DevExpress.Utils.Locations.Left;
            this.lcCheckId.TextSize = new System.Drawing.Size(77, 21);
            this.lcCheckId.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(202, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(578, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XPFMACHINECHECK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 533);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XPFMACHINECHECK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "비가동등록";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Machine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CheckId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_CheckPointImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckPointImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.SearchLookUpEditEx lup_Machine;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcMachine;
        private Service.Controls.GridEx_POP gridEx1;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcCheckList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Service.Controls.SearchLookUpEditEx lup_CheckId;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcCheckId;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.PictureEdit pic_CheckPointImage;
        private DevExpress.XtraLayout.LayoutControlItem lcCheckPointImage;
    }
}