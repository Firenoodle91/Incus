namespace HKInc.Ui.View.SELECT_POPUP
{
    partial class XSFORD1100
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSFORD1100));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_OrderCustomer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_DelivDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcDelivDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOrderCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDelivList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tx_Customerworkno = new DevExpress.XtraEditors.TextEdit();
            this.lcCustomerWorkNo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_OrderCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDelivDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDelivList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Customerworkno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerWorkNo)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_OrderCustomer);
            this.layoutControl1.Controls.Add(this.lup_Item);
            this.layoutControl1.Controls.Add(this.dt_DelivDate);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_Customerworkno);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1283, -290, 812, 500);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1056, 594);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_OrderCustomer
            // 
            this.lup_OrderCustomer.Constraint = null;
            this.lup_OrderCustomer.DataSource = null;
            this.lup_OrderCustomer.DisplayMember = "";
            this.lup_OrderCustomer.isImeModeDisable = false;
            this.lup_OrderCustomer.isRequired = false;
            this.lup_OrderCustomer.Location = new System.Drawing.Point(152, 74);
            this.lup_OrderCustomer.Name = "lup_OrderCustomer";
            this.lup_OrderCustomer.NullText = "";
            this.lup_OrderCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_OrderCustomer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_OrderCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_OrderCustomer.Properties.NullText = "";
            this.lup_OrderCustomer.Properties.PopupView = this.gridView1;
            this.lup_OrderCustomer.Size = new System.Drawing.Size(200, 24);
            this.lup_OrderCustomer.StyleController = this.layoutControl1;
            this.lup_OrderCustomer.TabIndex = 2;
            this.lup_OrderCustomer.Value_1 = null;
            this.lup_OrderCustomer.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
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
            this.lup_Item.Size = new System.Drawing.Size(548, 24);
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
            // dt_DelivDate
            // 
            this.dt_DelivDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_DelivDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_DelivDate.Appearance.Options.UseBackColor = true;
            this.dt_DelivDate.Appearance.Options.UseFont = true;
            this.dt_DelivDate.EditFrValue = new System.DateTime(2020, 1, 25, 0, 0, 0, 0);
            this.dt_DelivDate.EditToValue = new System.DateTime(2020, 2, 25, 23, 59, 59, 990);
            this.dt_DelivDate.Location = new System.Drawing.Point(152, 50);
            this.dt_DelivDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dt_DelivDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_DelivDate.MinimumSize = new System.Drawing.Size(200, 20);
            this.dt_DelivDate.Name = "dt_DelivDate";
            this.dt_DelivDate.Size = new System.Drawing.Size(200, 20);
            this.dt_DelivDate.TabIndex = 0;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 156);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 414);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcDelivList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1056, 594);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcDelivDate,
            this.lcItem,
            this.lcOrderCustomer,
            this.lcCustomerWorkNo});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1036, 106);
            this.lcCondition.Text = "조회조건";
            // 
            // lcDelivDate
            // 
            this.lcDelivDate.Control = this.dt_DelivDate;
            this.lcDelivDate.Location = new System.Drawing.Point(0, 0);
            this.lcDelivDate.Name = "lcDelivDate";
            this.lcDelivDate.Size = new System.Drawing.Size(332, 24);
            this.lcDelivDate.TextSize = new System.Drawing.Size(124, 18);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(332, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(680, 28);
            this.lcItem.TextSize = new System.Drawing.Size(124, 18);
            // 
            // lcOrderCustomer
            // 
            this.lcOrderCustomer.Control = this.lup_OrderCustomer;
            this.lcOrderCustomer.Location = new System.Drawing.Point(0, 24);
            this.lcOrderCustomer.Name = "lcOrderCustomer";
            this.lcOrderCustomer.Size = new System.Drawing.Size(332, 32);
            this.lcOrderCustomer.TextSize = new System.Drawing.Size(124, 18);
            // 
            // lcDelivList
            // 
            this.lcDelivList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcDelivList.Location = new System.Drawing.Point(0, 106);
            this.lcDelivList.Name = "lcDelivList";
            this.lcDelivList.OptionsItemText.TextToControlDistance = 4;
            this.lcDelivList.Size = new System.Drawing.Size(1036, 468);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1012, 418);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // tx_Customerworkno
            // 
            this.tx_Customerworkno.Location = new System.Drawing.Point(484, 78);
            this.tx_Customerworkno.Name = "tx_Customerworkno";
            this.tx_Customerworkno.Size = new System.Drawing.Size(548, 24);
            this.tx_Customerworkno.StyleController = this.layoutControl1;
            this.tx_Customerworkno.TabIndex = 4;
            // 
            // lcCustomerWorkNo
            // 
            this.lcCustomerWorkNo.Control = this.tx_Customerworkno;
            this.lcCustomerWorkNo.Location = new System.Drawing.Point(332, 28);
            this.lcCustomerWorkNo.Name = "lcCustomerWorkNo";
            this.lcCustomerWorkNo.Size = new System.Drawing.Size(680, 28);
            this.lcCustomerWorkNo.TextSize = new System.Drawing.Size(124, 18);
            // 
            // XSFORD1100
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 653);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XSFORD1100";
            this.Text = "XSFORD1100";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_OrderCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDelivDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDelivList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Customerworkno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerWorkNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcDelivList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private Service.Controls.SearchLookUpEditEx lup_OrderCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private Service.Controls.DatePeriodEditEx dt_DelivDate;
        private DevExpress.XtraLayout.LayoutControlItem lcDelivDate;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private DevExpress.XtraLayout.LayoutControlItem lcOrderCustomer;
        private DevExpress.XtraEditors.TextEdit tx_Customerworkno;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerWorkNo;
    }
}