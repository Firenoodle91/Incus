namespace HKInc.Ui.View.View.QCT
{
    partial class XFQCT1200
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFQCT1200));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tx_WorkNo = new DevExpress.XtraEditors.TextEdit();
            this.tx_ProductLotNo = new DevExpress.XtraEditors.TextEdit();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_ResultDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.tx_CustomerLotNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcResultDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcProductLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcWorkNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustomerLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcLotTrackingInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lup_Process = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.lcProcessName = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_WorkNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ProductLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_CustomerLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcResultDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLotTrackingInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Process.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProcessName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_Process);
            this.layoutControl1.Controls.Add(this.tx_WorkNo);
            this.layoutControl1.Controls.Add(this.tx_ProductLotNo);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.dt_ResultDate);
            this.layoutControl1.Controls.Add(this.tx_CustomerLotNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1208, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tx_WorkNo
            // 
            this.tx_WorkNo.Location = new System.Drawing.Point(774, 50);
            this.tx_WorkNo.Name = "tx_WorkNo";
            this.tx_WorkNo.Size = new System.Drawing.Size(148, 24);
            this.tx_WorkNo.StyleController = this.layoutControl1;
            this.tx_WorkNo.TabIndex = 4;
            // 
            // tx_ProductLotNo
            // 
            this.tx_ProductLotNo.Location = new System.Drawing.Point(484, 78);
            this.tx_ProductLotNo.Name = "tx_ProductLotNo";
            this.tx_ProductLotNo.Size = new System.Drawing.Size(158, 24);
            this.tx_ProductLotNo.StyleController = this.layoutControl1;
            this.tx_ProductLotNo.TabIndex = 2;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 156);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1160, 487);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(484, 50);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Item.Size = new System.Drawing.Size(158, 24);
            this.lup_Item.StyleController = this.layoutControl1;
            this.lup_Item.TabIndex = 1;
            this.lup_Item.Value_1 = null;
            this.lup_Item.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dt_ResultDate
            // 
            this.dt_ResultDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_ResultDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_ResultDate.Appearance.Options.UseBackColor = true;
            this.dt_ResultDate.Appearance.Options.UseFont = true;
            this.dt_ResultDate.EditFrValue = new System.DateTime(2020, 1, 17, 0, 0, 0, 0);
            this.dt_ResultDate.EditToValue = new System.DateTime(2020, 2, 17, 23, 59, 59, 990);
            this.dt_ResultDate.Location = new System.Drawing.Point(152, 50);
            this.dt_ResultDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_ResultDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_ResultDate.MinimumSize = new System.Drawing.Size(200, 20);
            this.dt_ResultDate.Name = "dt_ResultDate";
            this.dt_ResultDate.Size = new System.Drawing.Size(200, 20);
            this.dt_ResultDate.TabIndex = 0;
            // 
            // tx_CustomerLotNo
            // 
            this.tx_CustomerLotNo.Location = new System.Drawing.Point(774, 78);
            this.tx_CustomerLotNo.Name = "tx_CustomerLotNo";
            this.tx_CustomerLotNo.Size = new System.Drawing.Size(148, 24);
            this.tx_CustomerLotNo.StyleController = this.layoutControl1;
            this.tx_CustomerLotNo.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcLotTrackingInfo});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1208, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcResultDate,
            this.lcItem,
            this.emptySpaceItem2,
            this.lcWorkNo,
            this.lcProductLotNo,
            this.lcCustomerLotNo,
            this.lcProcessName});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1188, 106);
            // 
            // lcResultDate
            // 
            this.lcResultDate.Control = this.dt_ResultDate;
            this.lcResultDate.Location = new System.Drawing.Point(0, 0);
            this.lcResultDate.Name = "lcResultDate";
            this.lcResultDate.Size = new System.Drawing.Size(332, 56);
            this.lcResultDate.TextSize = new System.Drawing.Size(124, 18);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(332, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(290, 28);
            this.lcItem.TextSize = new System.Drawing.Size(124, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(902, 28);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(262, 28);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcProductLotNo
            // 
            this.lcProductLotNo.Control = this.tx_ProductLotNo;
            this.lcProductLotNo.Location = new System.Drawing.Point(332, 28);
            this.lcProductLotNo.Name = "lcProductLotNo";
            this.lcProductLotNo.Size = new System.Drawing.Size(290, 28);
            this.lcProductLotNo.TextSize = new System.Drawing.Size(124, 18);
            // 
            // lcWorkNo
            // 
            this.lcWorkNo.Control = this.tx_WorkNo;
            this.lcWorkNo.Location = new System.Drawing.Point(622, 0);
            this.lcWorkNo.Name = "lcWorkNo";
            this.lcWorkNo.Size = new System.Drawing.Size(280, 28);
            this.lcWorkNo.TextSize = new System.Drawing.Size(124, 18);
            // 
            // lcCustomerLotNo
            // 
            this.lcCustomerLotNo.Control = this.tx_CustomerLotNo;
            this.lcCustomerLotNo.CustomizationFormText = "layoutControlItem2";
            this.lcCustomerLotNo.Location = new System.Drawing.Point(622, 28);
            this.lcCustomerLotNo.Name = "lcCustomerLotNo";
            this.lcCustomerLotNo.Size = new System.Drawing.Size(280, 28);
            this.lcCustomerLotNo.Text = "layoutControlItem2";
            this.lcCustomerLotNo.TextSize = new System.Drawing.Size(124, 18);
            // 
            // lcLotTrackingInfo
            // 
            this.lcLotTrackingInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcLotTrackingInfo.Location = new System.Drawing.Point(0, 106);
            this.lcLotTrackingInfo.Name = "lcLotTrackingInfo";
            this.lcLotTrackingInfo.OptionsItemText.TextToControlDistance = 4;
            this.lcLotTrackingInfo.Size = new System.Drawing.Size(1188, 541);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridEx1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1164, 491);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lup_Process
            // 
            this.lup_Process.Constraint = null;
            this.lup_Process.DataSource = null;
            this.lup_Process.DisplayMember = "";
            this.lup_Process.isImeModeDisable = false;
            this.lup_Process.isRequired = false;
            this.lup_Process.Location = new System.Drawing.Point(1054, 50);
            this.lup_Process.Name = "lup_Process";
            this.lup_Process.NullText = "";
            this.lup_Process.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Process.Properties.NullText = "";
            this.lup_Process.Properties.PopupView = this.gridView1;
            this.lup_Process.Size = new System.Drawing.Size(130, 24);
            this.lup_Process.StyleController = this.layoutControl1;
            this.lup_Process.TabIndex = 5;
            this.lup_Process.Value_1 = null;
            this.lup_Process.ValueMember = "";
            // 
            // lcProcessName
            // 
            this.lcProcessName.Control = this.lup_Process;
            this.lcProcessName.Location = new System.Drawing.Point(902, 0);
            this.lcProcessName.Name = "lcProcessName";
            this.lcProcessName.Size = new System.Drawing.Size(262, 28);
            this.lcProcessName.TextSize = new System.Drawing.Size(124, 18);
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // XFQCT1200
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 726);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XFQCT1200";
            this.Text = "XFQCT1200";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_WorkNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ProductLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_CustomerLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcResultDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWorkNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLotTrackingInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Process.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProcessName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private Service.Controls.DatePeriodEditEx dt_ResultDate;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlItem lcResultDate;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcLotTrackingInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit tx_ProductLotNo;
        private DevExpress.XtraLayout.LayoutControlItem lcProductLotNo;
        private DevExpress.XtraEditors.TextEdit tx_WorkNo;
        private DevExpress.XtraLayout.LayoutControlItem lcWorkNo;
        private DevExpress.XtraEditors.TextEdit tx_CustomerLotNo;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerLotNo;
        private Service.Controls.SearchLookUpEditEx lup_Process;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcProcessName;
    }
}