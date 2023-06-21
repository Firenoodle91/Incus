namespace HKInc.Ui.View.View.MPS
{
    partial class XFMPS1500
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFMPS1500));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.tx_WorkNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcWorkNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItemMoveNoList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.tx_ItemMoveNo = new DevExpress.XtraEditors.TextEdit();
            this.lcItemMoveNo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_WorkNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMoveNoList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ItemMoveNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMoveNo)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_WorkNo);
            this.layoutControl1.Controls.Add(this.tx_ItemMoveNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(31, 389);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(1008, 233);
            this.gridEx2.TabIndex = 2;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 141);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 181);
            this.gridEx1.TabIndex = 1;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // tx_WorkNo
            // 
            this.tx_WorkNo.Location = new System.Drawing.Point(113, 56);
            this.tx_WorkNo.Name = "tx_WorkNo";
            this.tx_WorkNo.Size = new System.Drawing.Size(160, 24);
            this.tx_WorkNo.StyleController = this.layoutControl1;
            this.tx_WorkNo.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcItemMoveNoList,
            this.lcDetailList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
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
            this.lcCondition.Size = new System.Drawing.Size(1044, 85);
            this.lcCondition.Text = "조회조건";
            // 
            // lcWorkNo
            // 
            this.lcWorkNo.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lcWorkNo.AppearanceItemCaption.Options.UseForeColor = true;
            this.lcWorkNo.Control = this.tx_WorkNo;
            this.lcWorkNo.Location = new System.Drawing.Point(0, 0);
            this.lcWorkNo.Name = "lcWorkNo";
            this.lcWorkNo.Size = new System.Drawing.Size(248, 30);
            this.lcWorkNo.Text = "작업지시번호";
            this.lcWorkNo.TextSize = new System.Drawing.Size(78, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(496, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(518, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItemMoveNoList
            // 
            this.lcItemMoveNoList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcItemMoveNoList.Location = new System.Drawing.Point(0, 85);
            this.lcItemMoveNoList.Name = "lcItemMoveNoList";
            this.lcItemMoveNoList.OptionsItemText.TextToControlDistance = 4;
            this.lcItemMoveNoList.Size = new System.Drawing.Size(1044, 242);
            this.lcItemMoveNoList.Text = "이동표 목록";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1014, 187);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lcDetailList
            // 
            this.lcDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcDetailList.Location = new System.Drawing.Point(0, 333);
            this.lcDetailList.Name = "lcDetailList";
            this.lcDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcDetailList.Size = new System.Drawing.Size(1044, 294);
            this.lcDetailList.Text = "상세내역";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1014, 239);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 327);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1044, 6);
            // 
            // tx_ItemMoveNo
            // 
            this.tx_ItemMoveNo.Location = new System.Drawing.Point(348, 56);
            this.tx_ItemMoveNo.Name = "tx_ItemMoveNo";
            this.tx_ItemMoveNo.Size = new System.Drawing.Size(173, 24);
            this.tx_ItemMoveNo.StyleController = this.layoutControl1;
            this.tx_ItemMoveNo.TabIndex = 0;
            // 
            // lcItemMoveNo
            // 
            this.lcItemMoveNo.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lcItemMoveNo.AppearanceItemCaption.Options.UseForeColor = true;
            this.lcItemMoveNo.Control = this.tx_ItemMoveNo;
            this.lcItemMoveNo.CustomizationFormText = "이동표번호";
            this.lcItemMoveNo.Location = new System.Drawing.Point(248, 0);
            this.lcItemMoveNo.Name = "lcItemMoveNo";
            this.lcItemMoveNo.Size = new System.Drawing.Size(248, 30);
            this.lcItemMoveNo.Text = "이동표번호";
            this.lcItemMoveNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcItemMoveNo.TextSize = new System.Drawing.Size(65, 18);
            this.lcItemMoveNo.TextToControlDistance = 4;
            // 
            // XFMPS1500
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFMPS1500";
            this.Text = "XFMPS1500";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_WorkNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMoveNoList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ItemMoveNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMoveNo)).EndInit();
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
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcItemMoveNoList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.TextEdit tx_ItemMoveNo;
        private DevExpress.XtraLayout.LayoutControlItem lcItemMoveNo;
    }
}