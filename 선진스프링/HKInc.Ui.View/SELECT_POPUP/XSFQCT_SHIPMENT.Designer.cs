namespace HKInc.Ui.View.SELECT_POPUP
{
    partial class XSFQCT_SHIPMENT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSFQCT_SHIPMENT));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_WorkDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcCheckDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProductLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcInspectionObjectList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lup_Customer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lcCustomerName = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInspectionObjectList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Customer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.dt_WorkDate);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.lup_Customer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1199, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(977, 50);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(164, 24);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 2;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(428, 50);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Item.Size = new System.Drawing.Size(178, 24);
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
            // dt_WorkDate
            // 
            this.dt_WorkDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_WorkDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_WorkDate.Appearance.Options.UseBackColor = true;
            this.dt_WorkDate.Appearance.Options.UseFont = true;
            this.dt_WorkDate.EditFrValue = new System.DateTime(2020, 1, 24, 0, 0, 0, 0);
            this.dt_WorkDate.EditToValue = new System.DateTime(2020, 2, 24, 23, 59, 59, 990);
            this.dt_WorkDate.Location = new System.Drawing.Point(124, 50);
            this.dt_WorkDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_WorkDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_WorkDate.MinimumSize = new System.Drawing.Size(200, 21);
            this.dt_WorkDate.Name = "dt_WorkDate";
            this.dt_WorkDate.Size = new System.Drawing.Size(200, 21);
            this.dt_WorkDate.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 128);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1151, 515);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcInspectionObjectList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1199, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcCheckDate,
            this.lcItem,
            this.lcProductLotNo,
            this.lcCustomerName});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1179, 78);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(1121, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(34, 28);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcCheckDate
            // 
            this.lcCheckDate.Control = this.dt_WorkDate;
            this.lcCheckDate.Location = new System.Drawing.Point(0, 0);
            this.lcCheckDate.Name = "lcCheckDate";
            this.lcCheckDate.Size = new System.Drawing.Size(304, 28);
            this.lcCheckDate.Text = "검사일";
            this.lcCheckDate.TextSize = new System.Drawing.Size(96, 18);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(304, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(282, 28);
            this.lcItem.TextSize = new System.Drawing.Size(96, 18);
            // 
            // lcProductLotNo
            // 
            this.lcProductLotNo.Control = this.textEdit1;
            this.lcProductLotNo.Location = new System.Drawing.Point(853, 0);
            this.lcProductLotNo.Name = "lcProductLotNo";
            this.lcProductLotNo.Size = new System.Drawing.Size(268, 28);
            this.lcProductLotNo.TextSize = new System.Drawing.Size(96, 18);
            // 
            // lcInspectionObjectList
            // 
            this.lcInspectionObjectList.CustomizationFormText = "발주마스터목록";
            this.lcInspectionObjectList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcInspectionObjectList.Location = new System.Drawing.Point(0, 78);
            this.lcInspectionObjectList.Name = "lcInspectionObjectList";
            this.lcInspectionObjectList.OptionsItemText.TextToControlDistance = 4;
            this.lcInspectionObjectList.Size = new System.Drawing.Size(1179, 569);
            this.lcInspectionObjectList.Text = "검사대상목록";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1155, 519);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lup_Customer
            // 
            this.lup_Customer.Constraint = null;
            this.lup_Customer.DataSource = null;
            this.lup_Customer.DisplayMember = "";
            this.lup_Customer.isImeModeDisable = false;
            this.lup_Customer.isRequired = false;
            this.lup_Customer.Location = new System.Drawing.Point(710, 50);
            this.lup_Customer.Name = "lup_Customer";
            this.lup_Customer.NullText = "";
            this.lup_Customer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Customer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Customer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Customer.Properties.NullText = "";
            this.lup_Customer.Properties.PopupView = this.searchLookUpEditEx1View1;
            this.lup_Customer.Size = new System.Drawing.Size(163, 24);
            this.lup_Customer.StyleController = this.layoutControl1;
            this.lup_Customer.TabIndex = 1;
            this.lup_Customer.Value_1 = null;
            this.lup_Customer.ValueMember = "";
            // 
            // searchLookUpEditEx1View1
            // 
            this.searchLookUpEditEx1View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View1.Name = "searchLookUpEditEx1View1";
            this.searchLookUpEditEx1View1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View1.OptionsView.ShowGroupPanel = false;
            // 
            // lcCustomerName
            // 
            this.lcCustomerName.Control = this.lup_Customer;
            this.lcCustomerName.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcCustomerName.CustomizationFormText = "lcItem";
            this.lcCustomerName.Location = new System.Drawing.Point(586, 0);
            this.lcCustomerName.Name = "lcCustomerName";
            this.lcCustomerName.Size = new System.Drawing.Size(267, 28);
            this.lcCustomerName.Text = "lcItem";
            this.lcCustomerName.TextSize = new System.Drawing.Size(96, 18);
            // 
            // XSFQCT_SHIPMENT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XSFQCT_SHIPMENT";
            this.Text = "XSFQCT_SHIPMENT";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCheckDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInspectionObjectList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Customer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcInspectionObjectList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private Service.Controls.DatePeriodEditEx dt_WorkDate;
        private DevExpress.XtraLayout.LayoutControlItem lcCheckDate;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem lcProductLotNo;
        private Service.Controls.SearchLookUpEditEx lup_Customer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View1;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerName;
    }
}