namespace HKInc.Ui.View.View.POP_POPUP
{
    partial class XPFTOOL_CHANGE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFTOOL_CHANGE));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Exit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ToolChange = new DevExpress.XtraEditors.SimpleButton();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcToolList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.Label_MachineInfo = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcToolList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label_MachineInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Exit);
            this.layoutControl1.Controls.Add(this.btn_ToolChange);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(539, 525);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Exit
            // 
            this.btn_Exit.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Exit.Appearance.Options.UseFont = true;
            this.btn_Exit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exit.ImageOptions.Image")));
            this.btn_Exit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Exit.Location = new System.Drawing.Point(279, 477);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(248, 36);
            this.btn_Exit.StyleController = this.layoutControl1;
            this.btn_Exit.TabIndex = 29;
            this.btn_Exit.Text = "종료";
            // 
            // btn_ToolChange
            // 
            this.btn_ToolChange.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.btn_ToolChange.Appearance.Options.UseFont = true;
            this.btn_ToolChange.Appearance.Options.UseImage = true;
            this.btn_ToolChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_ToolChange.ImageOptions.Image")));
            this.btn_ToolChange.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_ToolChange.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_ToolChange.Location = new System.Drawing.Point(12, 477);
            this.btn_ToolChange.Name = "btn_ToolChange";
            this.btn_ToolChange.Size = new System.Drawing.Size(263, 36);
            this.btn_ToolChange.StyleController = this.layoutControl1;
            this.btn_ToolChange.TabIndex = 21;
            this.btn_ToolChange.Text = "교체";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 70);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(491, 348);
            this.gridEx1.TabIndex = 13;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcToolList,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.Label_MachineInfo});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(539, 525);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcToolList
            // 
            this.lcToolList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcToolList.Location = new System.Drawing.Point(0, 25);
            this.lcToolList.Name = "lcToolList";
            this.lcToolList.Size = new System.Drawing.Size(519, 397);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridEx1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(495, 352);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_ToolChange;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 465);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(267, 40);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Exit;
            this.layoutControlItem3.Location = new System.Drawing.Point(267, 465);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(252, 40);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 422);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(519, 43);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Label_MachineInfo
            // 
            this.Label_MachineInfo.AllowHotTrack = false;
            this.Label_MachineInfo.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.Label_MachineInfo.AppearanceItemCaption.Options.UseFont = true;
            this.Label_MachineInfo.Location = new System.Drawing.Point(0, 0);
            this.Label_MachineInfo.Name = "Label_MachineInfo";
            this.Label_MachineInfo.Size = new System.Drawing.Size(519, 25);
            this.Label_MachineInfo.TextSize = new System.Drawing.Size(183, 21);
            this.Label_MachineInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // XPFTOOL_CHANGE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 572);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XPFTOOL_CHANGE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "공구 교체";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcToolList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label_MachineInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcToolList;
        private DevExpress.XtraEditors.SimpleButton btn_Exit;
        private DevExpress.XtraEditors.SimpleButton btn_ToolChange;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SimpleLabelItem Label_MachineInfo;
    }
}