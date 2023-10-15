namespace HKInc.Ui.View.View.MPS
{
    partial class XFMPS1900
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFMPS1900));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.tx_WorkNo = new DevExpress.XtraEditors.TextEdit();
            this.tx_ItemMoveNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcWorkNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItemMoveNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProductResultList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_WorkNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ItemMoveNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMoveNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductResultList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_WorkNo);
            this.layoutControl1.Controls.Add(this.tx_ItemMoveNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 128);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 515);
            this.gridEx1.TabIndex = 1;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // tx_WorkNo
            // 
            this.tx_WorkNo.Location = new System.Drawing.Point(106, 50);
            this.tx_WorkNo.Name = "tx_WorkNo";
            this.tx_WorkNo.Size = new System.Drawing.Size(165, 24);
            this.tx_WorkNo.StyleController = this.layoutControl1;
            this.tx_WorkNo.TabIndex = 0;
            // 
            // tx_ItemMoveNo
            // 
            this.tx_ItemMoveNo.Location = new System.Drawing.Point(344, 50);
            this.tx_ItemMoveNo.Name = "tx_ItemMoveNo";
            this.tx_ItemMoveNo.Size = new System.Drawing.Size(178, 24);
            this.tx_ItemMoveNo.StyleController = this.layoutControl1;
            this.tx_ItemMoveNo.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcProductResultList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcWorkNo,
            this.emptySpaceItem2,
            this.lcItemMoveNo});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1050, 78);
            this.lcCondition.Text = "조회조건";
            // 
            // lcWorkNo
            // 
            this.lcWorkNo.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lcWorkNo.AppearanceItemCaption.Options.UseForeColor = true;
            this.lcWorkNo.Control = this.tx_WorkNo;
            this.lcWorkNo.Location = new System.Drawing.Point(0, 0);
            this.lcWorkNo.Name = "lcWorkNo";
            this.lcWorkNo.Size = new System.Drawing.Size(251, 28);
            this.lcWorkNo.Text = "작업지시번호";
            this.lcWorkNo.TextSize = new System.Drawing.Size(78, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(502, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(524, 28);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItemMoveNo
            // 
            this.lcItemMoveNo.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lcItemMoveNo.AppearanceItemCaption.Options.UseForeColor = true;
            this.lcItemMoveNo.Control = this.tx_ItemMoveNo;
            this.lcItemMoveNo.CustomizationFormText = "이동표번호";
            this.lcItemMoveNo.Location = new System.Drawing.Point(251, 0);
            this.lcItemMoveNo.Name = "lcItemMoveNo";
            this.lcItemMoveNo.Size = new System.Drawing.Size(251, 28);
            this.lcItemMoveNo.Text = "이동표번호";
            this.lcItemMoveNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcItemMoveNo.TextSize = new System.Drawing.Size(65, 18);
            this.lcItemMoveNo.TextToControlDistance = 4;
            // 
            // lcProductResultList
            // 
            this.lcProductResultList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcProductResultList.Location = new System.Drawing.Point(0, 78);
            this.lcProductResultList.Name = "lcProductResultList";
            this.lcProductResultList.OptionsItemText.TextToControlDistance = 4;
            this.lcProductResultList.Size = new System.Drawing.Size(1050, 569);
            this.lcProductResultList.Text = "생산실적목록";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1026, 519);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // XFMPS1900
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFMPS1900";
            this.Text = "XFMPS1900";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_WorkNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ItemMoveNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMoveNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductResultList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit tx_WorkNo;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcWorkNo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcProductResultList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit tx_ItemMoveNo;
        private DevExpress.XtraLayout.LayoutControlItem lcItemMoveNo;
    }
}