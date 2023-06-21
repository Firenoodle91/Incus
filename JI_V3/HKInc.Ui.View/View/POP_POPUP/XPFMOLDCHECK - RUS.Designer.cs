namespace HKInc.Ui.View.View.POP_POPUP
{
    partial class XPFMOLDCHECK_RUS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFMOLDCHECK_RUS));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx_POP();
            this.lup_CheckId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pic_CheckPointImage = new DevExpress.XtraEditors.PictureEdit();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.tx_Mold = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcMold = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcCheckPointImage = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCheckList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCheckId = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CheckId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_CheckPointImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Mold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckPointImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.lup_CheckId);
            this.layoutControl1.Controls.Add(this.pic_CheckPointImage);
            this.layoutControl1.Controls.Add(this.btn_Save);
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.tx_Mold);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(986, 507);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(15, 227);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(956, 265);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_CheckId
            // 
            this.lup_CheckId.Constraint = null;
            this.lup_CheckId.DataSource = null;
            this.lup_CheckId.DisplayMember = "";
            this.lup_CheckId.isImeModeDisable = false;
            this.lup_CheckId.isRequired = false;
            this.lup_CheckId.Location = new System.Drawing.Point(111, 191);
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
            this.lup_CheckId.Size = new System.Drawing.Size(318, 34);
            this.lup_CheckId.StyleController = this.layoutControl1;
            this.lup_CheckId.TabIndex = 1;
            this.lup_CheckId.Value_1 = null;
            this.lup_CheckId.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // pic_CheckPointImage
            // 
            this.pic_CheckPointImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pic_CheckPointImage.Location = new System.Drawing.Point(202, 72);
            this.pic_CheckPointImage.Name = "pic_CheckPointImage";
            this.pic_CheckPointImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_CheckPointImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_CheckPointImage.Size = new System.Drawing.Size(226, 74);
            this.pic_CheckPointImage.StyleController = this.layoutControl1;
            this.pic_CheckPointImage.TabIndex = 0;
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Appearance.Options.UseTextOptions = true;
            this.btn_Save.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_Save.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.ImageOptions.Image")));
            this.btn_Save.Location = new System.Drawing.Point(679, 42);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(148, 104);
            this.btn_Save.StyleController = this.layoutControl1;
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "저장";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseTextOptions = true;
            this.btn_Cancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_Cancel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_Cancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.ImageOptions.Image")));
            this.btn_Cancel.Location = new System.Drawing.Point(829, 42);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(142, 104);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 8;
            this.btn_Cancel.Text = "취소";
            // 
            // tx_Mold
            // 
            this.tx_Mold.Location = new System.Drawing.Point(15, 72);
            this.tx_Mold.Name = "tx_Mold";
            this.tx_Mold.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.tx_Mold.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_Mold.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.tx_Mold.Properties.Appearance.Options.UseBackColor = true;
            this.tx_Mold.Properties.Appearance.Options.UseFont = true;
            this.tx_Mold.Properties.Appearance.Options.UseForeColor = true;
            this.tx_Mold.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tx_Mold.Size = new System.Drawing.Size(185, 34);
            this.tx_Mold.StyleController = this.layoutControl1;
            this.tx_Mold.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcCheckList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(986, 507);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.AppearanceGroup.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcCondition.AppearanceGroup.Options.UseFont = true;
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcMold,
            this.emptySpaceItem2,
            this.lcCheckPointImage,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(974, 149);
            // 
            // lcMold
            // 
            this.lcMold.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcMold.AppearanceItemCaption.Options.UseFont = true;
            this.lcMold.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcMold.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcMold.Control = this.tx_Mold;
            this.lcMold.Location = new System.Drawing.Point(0, 0);
            this.lcMold.MinSize = new System.Drawing.Size(50, 25);
            this.lcMold.Name = "lcMold";
            this.lcMold.Size = new System.Drawing.Size(187, 106);
            this.lcMold.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcMold.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcMold.TextSize = new System.Drawing.Size(140, 28);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(415, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(249, 106);
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
            this.lcCheckPointImage.Location = new System.Drawing.Point(187, 0);
            this.lcCheckPointImage.Name = "lcCheckPointImage";
            this.lcCheckPointImage.Size = new System.Drawing.Size(228, 106);
            this.lcCheckPointImage.Text = "점검포인트사진";
            this.lcCheckPointImage.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcCheckPointImage.TextSize = new System.Drawing.Size(140, 28);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_Save;
            this.layoutControlItem4.Location = new System.Drawing.Point(664, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(102, 31);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(150, 106);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_Cancel;
            this.layoutControlItem5.Location = new System.Drawing.Point(814, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(102, 31);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(144, 106);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // lcCheckList
            // 
            this.lcCheckList.AppearanceGroup.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcCheckList.AppearanceGroup.Options.UseFont = true;
            this.lcCheckList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.lcCheckId,
            this.emptySpaceItem1});
            this.lcCheckList.Location = new System.Drawing.Point(0, 149);
            this.lcCheckList.Name = "lcCheckList";
            this.lcCheckList.OptionsItemText.TextToControlDistance = 4;
            this.lcCheckList.Size = new System.Drawing.Size(974, 346);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 36);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(958, 267);
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
            this.lcCheckId.Location = new System.Drawing.Point(0, 0);
            this.lcCheckId.Name = "lcCheckId";
            this.lcCheckId.Size = new System.Drawing.Size(416, 36);
            this.lcCheckId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcCheckId.TextLocation = DevExpress.Utils.Locations.Left;
            this.lcCheckId.TextSize = new System.Drawing.Size(91, 28);
            this.lcCheckId.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(416, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(542, 36);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XPFMOLDCHECK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 566);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(9, 12, 9, 12);
            this.Name = "XPFMOLDCHECK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "금형일상점검";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_CheckId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_CheckPointImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Mold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckPointImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcMold;
        private Service.Controls.GridEx_POP gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCheckList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Service.Controls.SearchLookUpEditEx lup_CheckId;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcCheckId;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.PictureEdit pic_CheckPointImage;
        private DevExpress.XtraLayout.LayoutControlItem lcCheckPointImage;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit tx_Mold;
    }
}