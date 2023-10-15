namespace HKInc.Ui.View.View.QCT
{
    partial class XFQCT1900
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFQCT1900));
            this.spcc_Top = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcCondition = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.slup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dpee_Shipment = new HKInc.Service.Controls.DatePeriodEditEx();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcShipmentPeriod = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.spcc_BottomGrid = new DevExpress.XtraEditors.SplitContainerControl();
            this.spcc_Left = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcShipMaster = new DevExpress.XtraEditors.GroupControl();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.gcShipDetail = new DevExpress.XtraEditors.GroupControl();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gcInspectionDetail = new DevExpress.XtraEditors.GroupControl();
            this.gridEx3 = new HKInc.Service.Controls.GridEx();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubDetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcc_Top)).BeginInit();
            this.spcc_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).BeginInit();
            this.gcCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShipmentPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcc_BottomGrid)).BeginInit();
            this.spcc_BottomGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcc_Left)).BeginInit();
            this.spcc_Left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcShipMaster)).BeginInit();
            this.gcShipMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcShipDetail)).BeginInit();
            this.gcShipDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcInspectionDetail)).BeginInit();
            this.gcInspectionDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // spcc_Top
            // 
            this.spcc_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcc_Top.Horizontal = false;
            this.spcc_Top.Location = new System.Drawing.Point(0, 30);
            this.spcc_Top.Name = "spcc_Top";
            this.spcc_Top.Panel1.Controls.Add(this.gcCondition);
            this.spcc_Top.Panel1.Text = "Panel1";
            this.spcc_Top.Panel2.Controls.Add(this.spcc_BottomGrid);
            this.spcc_Top.Panel2.Text = "Panel2";
            this.spcc_Top.Size = new System.Drawing.Size(1070, 667);
            this.spcc_Top.SplitterPosition = 113;
            this.spcc_Top.TabIndex = 4;
            // 
            // gcCondition
            // 
            this.gcCondition.Controls.Add(this.layoutControl1);
            this.gcCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCondition.Location = new System.Drawing.Point(0, 0);
            this.gcCondition.Name = "gcCondition";
            this.gcCondition.Size = new System.Drawing.Size(1070, 113);
            this.gcCondition.TabIndex = 0;
            this.gcCondition.Text = "groupControl1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.slup_Item);
            this.layoutControl1.Controls.Add(this.dpee_Shipment);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 28);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1066, 83);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // slup_Item
            // 
            this.slup_Item.Constraint = null;
            this.slup_Item.DataSource = null;
            this.slup_Item.DisplayMember = "";
            this.slup_Item.isImeModeDisable = false;
            this.slup_Item.isRequired = false;
            this.slup_Item.Location = new System.Drawing.Point(478, 12);
            this.slup_Item.Name = "slup_Item";
            this.slup_Item.NullText = "";
            this.slup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.slup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.slup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slup_Item.Properties.NullText = "";
            this.slup_Item.Properties.PopupView = this.searchLookUpEditEx1View;
            this.slup_Item.Size = new System.Drawing.Size(196, 24);
            this.slup_Item.StyleController = this.layoutControl1;
            this.slup_Item.TabIndex = 5;
            this.slup_Item.Value_1 = null;
            this.slup_Item.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dpee_Shipment
            // 
            this.dpee_Shipment.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dpee_Shipment.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpee_Shipment.Appearance.Options.UseBackColor = true;
            this.dpee_Shipment.Appearance.Options.UseFont = true;
            this.dpee_Shipment.EditFrValue = new System.DateTime(2021, 8, 17, 0, 0, 0, 0);
            this.dpee_Shipment.EditToValue = new System.DateTime(2021, 9, 17, 23, 59, 59, 990);
            this.dpee_Shipment.Location = new System.Drawing.Point(123, 12);
            this.dpee_Shipment.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dpee_Shipment.MaximumSize = new System.Drawing.Size(200, 20);
            this.dpee_Shipment.MinimumSize = new System.Drawing.Size(240, 20);
            this.dpee_Shipment.Name = "dpee_Shipment";
            this.dpee_Shipment.Size = new System.Drawing.Size(240, 20);
            this.dpee_Shipment.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcShipmentPeriod,
            this.emptySpaceItem1,
            this.lcItem});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1066, 83);
            this.Root.TextVisible = false;
            // 
            // lcShipmentPeriod
            // 
            this.lcShipmentPeriod.Control = this.dpee_Shipment;
            this.lcShipmentPeriod.Location = new System.Drawing.Point(0, 0);
            this.lcShipmentPeriod.Name = "lcShipmentPeriod";
            this.lcShipmentPeriod.Size = new System.Drawing.Size(355, 63);
            this.lcShipmentPeriod.TextSize = new System.Drawing.Size(108, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(666, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(380, 63);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.slup_Item;
            this.lcItem.Location = new System.Drawing.Point(355, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(311, 63);
            this.lcItem.TextSize = new System.Drawing.Size(108, 18);
            // 
            // spcc_BottomGrid
            // 
            this.spcc_BottomGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcc_BottomGrid.Horizontal = false;
            this.spcc_BottomGrid.Location = new System.Drawing.Point(0, 0);
            this.spcc_BottomGrid.Name = "spcc_BottomGrid";
            this.spcc_BottomGrid.Panel1.Controls.Add(this.spcc_Left);
            this.spcc_BottomGrid.Panel1.Text = "Panel1";
            this.spcc_BottomGrid.Panel2.Controls.Add(this.gcInspectionDetail);
            this.spcc_BottomGrid.Panel2.Text = "Panel2";
            this.spcc_BottomGrid.Size = new System.Drawing.Size(1070, 542);
            this.spcc_BottomGrid.SplitterPosition = 308;
            this.spcc_BottomGrid.TabIndex = 0;
            // 
            // spcc_Left
            // 
            this.spcc_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcc_Left.Location = new System.Drawing.Point(0, 0);
            this.spcc_Left.Name = "spcc_Left";
            this.spcc_Left.Panel1.Controls.Add(this.gcShipMaster);
            this.spcc_Left.Panel1.Text = "Panel1";
            this.spcc_Left.Panel2.Controls.Add(this.gcShipDetail);
            this.spcc_Left.Panel2.Text = "Panel2";
            this.spcc_Left.Size = new System.Drawing.Size(1070, 308);
            this.spcc_Left.SplitterPosition = 519;
            this.spcc_Left.TabIndex = 0;
            // 
            // gcShipMaster
            // 
            this.gcShipMaster.Controls.Add(this.gridEx1);
            this.gcShipMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcShipMaster.Location = new System.Drawing.Point(0, 0);
            this.gcShipMaster.Name = "gcShipMaster";
            this.gcShipMaster.Size = new System.Drawing.Size(519, 308);
            this.gcShipMaster.TabIndex = 0;
            this.gcShipMaster.Text = "groupControl2";
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEx1.Location = new System.Drawing.Point(2, 28);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(515, 278);
            this.gridEx1.TabIndex = 0;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gcShipDetail
            // 
            this.gcShipDetail.Controls.Add(this.gridEx2);
            this.gcShipDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcShipDetail.Location = new System.Drawing.Point(0, 0);
            this.gcShipDetail.Name = "gcShipDetail";
            this.gcShipDetail.Size = new System.Drawing.Size(539, 308);
            this.gcShipDetail.TabIndex = 0;
            this.gcShipDetail.Text = "groupControl1";
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEx2.Location = new System.Drawing.Point(2, 28);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx2.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(535, 278);
            this.gridEx2.TabIndex = 0;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gcInspectionDetail
            // 
            this.gcInspectionDetail.Controls.Add(this.gridEx3);
            this.gcInspectionDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcInspectionDetail.Location = new System.Drawing.Point(0, 0);
            this.gcInspectionDetail.Name = "gcInspectionDetail";
            this.gcInspectionDetail.Size = new System.Drawing.Size(1070, 222);
            this.gcInspectionDetail.TabIndex = 0;
            this.gcInspectionDetail.Text = "groupControl1";
            // 
            // gridEx3
            // 
            this.gridEx3.DataSource = null;
            this.gridEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEx3.Location = new System.Drawing.Point(2, 28);
            this.gridEx3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx3.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx3.Name = "gridEx3";
            this.gridEx3.Size = new System.Drawing.Size(1066, 192);
            this.gridEx3.TabIndex = 0;
            this.gridEx3.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // XFQCT1900
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.spcc_Top);
            this.Name = "XFQCT1900";
            this.Text = "XFQCT1900";
            this.Controls.SetChildIndex(this.spcc_Top, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubDetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcc_Top)).EndInit();
            this.spcc_Top.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).EndInit();
            this.gcCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShipmentPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcc_BottomGrid)).EndInit();
            this.spcc_BottomGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcc_Left)).EndInit();
            this.spcc_Left.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcShipMaster)).EndInit();
            this.gcShipMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcShipDetail)).EndInit();
            this.gcShipDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcInspectionDetail)).EndInit();
            this.gcInspectionDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl spcc_Top;
        private DevExpress.XtraEditors.SplitContainerControl spcc_BottomGrid;
        private DevExpress.XtraEditors.GroupControl gcCondition;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.DatePeriodEditEx dpee_Shipment;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lcShipmentPeriod;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SplitContainerControl spcc_Left;
        private DevExpress.XtraEditors.GroupControl gcShipMaster;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraEditors.GroupControl gcShipDetail;
        private Service.Controls.GridEx gridEx2;
        private DevExpress.XtraEditors.GroupControl gcInspectionDetail;
        private Service.Controls.GridEx gridEx3;
        private Service.Controls.SearchLookUpEditEx slup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
    }
}