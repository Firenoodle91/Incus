namespace HKInc.Ui.View.View.ORD_POPUP
{
    partial class XPFORD1000
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFORD1000));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_OrderCustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dt_OrderDueDate = new HKInc.Service.Controls.DateEditEx();
            this.dt_OrderDate = new HKInc.Service.Controls.DateEditEx();
            this.tx_OrderNo = new DevExpress.XtraEditors.TextEdit();
            this.tx_OrderManagerName = new DevExpress.XtraEditors.TextEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.lup_OrderId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcOrderNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOrderDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDueDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOrderManagerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOrderId = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcMemo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOrderCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_OrderCustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OrderDueDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OrderDueDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OrderDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OrderDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_OrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_OrderManagerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_OrderId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDueDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderManagerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_OrderCustomerCode);
            this.layoutControl1.Controls.Add(this.dt_OrderDueDate);
            this.layoutControl1.Controls.Add(this.dt_OrderDate);
            this.layoutControl1.Controls.Add(this.tx_OrderNo);
            this.layoutControl1.Controls.Add(this.tx_OrderManagerName);
            this.layoutControl1.Controls.Add(this.memoEdit1);
            this.layoutControl1.Controls.Add(this.lup_OrderId);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(427, 342);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_OrderCustomerCode
            // 
            this.lup_OrderCustomerCode.Constraint = null;
            this.lup_OrderCustomerCode.DataSource = null;
            this.lup_OrderCustomerCode.DisplayMember = "";
            this.lup_OrderCustomerCode.isImeModeDisable = false;
            this.lup_OrderCustomerCode.isRequired = false;
            this.lup_OrderCustomerCode.Location = new System.Drawing.Point(98, 46);
            this.lup_OrderCustomerCode.Name = "lup_OrderCustomerCode";
            this.lup_OrderCustomerCode.NullText = "";
            this.lup_OrderCustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_OrderCustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_OrderCustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_OrderCustomerCode.Properties.NullText = "";
            this.lup_OrderCustomerCode.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_OrderCustomerCode.Size = new System.Drawing.Size(313, 24);
            this.lup_OrderCustomerCode.StyleController = this.layoutControl1;
            this.lup_OrderCustomerCode.TabIndex = 1;
            this.lup_OrderCustomerCode.Value_1 = null;
            this.lup_OrderCustomerCode.ValueMember = "";
            // 
            // searchGridLookUpEditEx1View
            // 
            this.searchGridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchGridLookUpEditEx1View.Name = "searchGridLookUpEditEx1View";
            this.searchGridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchGridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dt_OrderDueDate
            // 
            this.dt_OrderDueDate.EditValue = null;
            this.dt_OrderDueDate.Location = new System.Drawing.Point(98, 106);
            this.dt_OrderDueDate.Name = "dt_OrderDueDate";
            this.dt_OrderDueDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_OrderDueDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_OrderDueDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dt_OrderDueDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_OrderDueDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dt_OrderDueDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_OrderDueDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dt_OrderDueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_OrderDueDate.Size = new System.Drawing.Size(313, 24);
            this.dt_OrderDueDate.StyleController = this.layoutControl1;
            this.dt_OrderDueDate.TabIndex = 3;
            // 
            // dt_OrderDate
            // 
            this.dt_OrderDate.EditValue = null;
            this.dt_OrderDate.Location = new System.Drawing.Point(98, 76);
            this.dt_OrderDate.Name = "dt_OrderDate";
            this.dt_OrderDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_OrderDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_OrderDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dt_OrderDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_OrderDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dt_OrderDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dt_OrderDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dt_OrderDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dt_OrderDate.Size = new System.Drawing.Size(313, 24);
            this.dt_OrderDate.StyleController = this.layoutControl1;
            this.dt_OrderDate.TabIndex = 2;
            // 
            // tx_OrderNo
            // 
            this.tx_OrderNo.Enabled = false;
            this.tx_OrderNo.Location = new System.Drawing.Point(98, 16);
            this.tx_OrderNo.Name = "tx_OrderNo";
            this.tx_OrderNo.Size = new System.Drawing.Size(313, 24);
            this.tx_OrderNo.StyleController = this.layoutControl1;
            this.tx_OrderNo.TabIndex = 0;
            // 
            // tx_OrderManagerName
            // 
            this.tx_OrderManagerName.Location = new System.Drawing.Point(98, 136);
            this.tx_OrderManagerName.Name = "tx_OrderManagerName";
            this.tx_OrderManagerName.Size = new System.Drawing.Size(313, 24);
            this.tx_OrderManagerName.StyleController = this.layoutControl1;
            this.tx_OrderManagerName.TabIndex = 4;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(98, 196);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(313, 130);
            this.memoEdit1.StyleController = this.layoutControl1;
            this.memoEdit1.TabIndex = 6;
            // 
            // lup_OrderId
            // 
            this.lup_OrderId.Constraint = null;
            this.lup_OrderId.DataSource = null;
            this.lup_OrderId.DisplayMember = "";
            this.lup_OrderId.isImeModeDisable = false;
            this.lup_OrderId.isRequired = false;
            this.lup_OrderId.Location = new System.Drawing.Point(98, 166);
            this.lup_OrderId.Name = "lup_OrderId";
            this.lup_OrderId.NullText = "";
            this.lup_OrderId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_OrderId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_OrderId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_OrderId.Properties.NullText = "";
            this.lup_OrderId.Properties.PopupView = this.gridView1;
            this.lup_OrderId.Size = new System.Drawing.Size(313, 24);
            this.lup_OrderId.StyleController = this.layoutControl1;
            this.lup_OrderId.TabIndex = 5;
            this.lup_OrderId.Value_1 = null;
            this.lup_OrderId.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcOrderNo,
            this.lcOrderDate,
            this.lcDueDate,
            this.lcOrderManagerName,
            this.lcOrderId,
            this.lcMemo,
            this.lcOrderCustomer});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(427, 342);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcOrderNo
            // 
            this.lcOrderNo.Control = this.tx_OrderNo;
            this.lcOrderNo.Location = new System.Drawing.Point(0, 0);
            this.lcOrderNo.Name = "lcOrderNo";
            this.lcOrderNo.Size = new System.Drawing.Size(401, 30);
            this.lcOrderNo.Text = "수주번호";
            this.lcOrderNo.TextSize = new System.Drawing.Size(78, 18);
            // 
            // lcOrderDate
            // 
            this.lcOrderDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lcOrderDate.AppearanceItemCaption.Options.UseForeColor = true;
            this.lcOrderDate.Control = this.dt_OrderDate;
            this.lcOrderDate.Location = new System.Drawing.Point(0, 60);
            this.lcOrderDate.Name = "lcOrderDate";
            this.lcOrderDate.Size = new System.Drawing.Size(401, 30);
            this.lcOrderDate.Text = "수주일";
            this.lcOrderDate.TextSize = new System.Drawing.Size(78, 18);
            // 
            // lcDueDate
            // 
            this.lcDueDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lcDueDate.AppearanceItemCaption.Options.UseForeColor = true;
            this.lcDueDate.Control = this.dt_OrderDueDate;
            this.lcDueDate.Location = new System.Drawing.Point(0, 90);
            this.lcDueDate.Name = "lcDueDate";
            this.lcDueDate.Size = new System.Drawing.Size(401, 30);
            this.lcDueDate.Text = "납기일";
            this.lcDueDate.TextSize = new System.Drawing.Size(78, 18);
            // 
            // lcOrderManagerName
            // 
            this.lcOrderManagerName.Control = this.tx_OrderManagerName;
            this.lcOrderManagerName.Location = new System.Drawing.Point(0, 120);
            this.lcOrderManagerName.Name = "lcOrderManagerName";
            this.lcOrderManagerName.Size = new System.Drawing.Size(401, 30);
            this.lcOrderManagerName.Text = "고객사담당자";
            this.lcOrderManagerName.TextSize = new System.Drawing.Size(78, 18);
            // 
            // lcOrderId
            // 
            this.lcOrderId.Control = this.lup_OrderId;
            this.lcOrderId.CustomizationFormText = "layoutControlItem8";
            this.lcOrderId.Location = new System.Drawing.Point(0, 150);
            this.lcOrderId.Name = "lcOrderId";
            this.lcOrderId.Size = new System.Drawing.Size(401, 30);
            this.lcOrderId.Text = "담당자";
            this.lcOrderId.TextSize = new System.Drawing.Size(78, 18);
            // 
            // lcMemo
            // 
            this.lcMemo.Control = this.memoEdit1;
            this.lcMemo.Location = new System.Drawing.Point(0, 180);
            this.lcMemo.Name = "lcMemo";
            this.lcMemo.Size = new System.Drawing.Size(401, 136);
            this.lcMemo.Text = "비고";
            this.lcMemo.TextSize = new System.Drawing.Size(78, 18);
            // 
            // lcOrderCustomer
            // 
            this.lcOrderCustomer.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.lcOrderCustomer.AppearanceItemCaption.Options.UseForeColor = true;
            this.lcOrderCustomer.Control = this.lup_OrderCustomerCode;
            this.lcOrderCustomer.Location = new System.Drawing.Point(0, 30);
            this.lcOrderCustomer.Name = "lcOrderCustomer";
            this.lcOrderCustomer.Size = new System.Drawing.Size(401, 30);
            this.lcOrderCustomer.Text = "수주처";
            this.lcOrderCustomer.TextSize = new System.Drawing.Size(78, 18);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(HKInc.Ui.Model.Domain.VIEW.TN_ORD1000);
            // 
            // XPFORD1000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 415);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFORD1000";
            this.Text = "XPFORD1000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_OrderCustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OrderDueDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OrderDueDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OrderDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_OrderDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_OrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_OrderManagerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_OrderId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDueDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderManagerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOrderCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.DateEditEx dt_OrderDate;
        private DevExpress.XtraEditors.TextEdit tx_OrderNo;
        private Service.Controls.DateEditEx dt_OrderDueDate;
        private DevExpress.XtraEditors.TextEdit tx_OrderManagerName;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private Service.Controls.SearchLookUpEditEx lup_OrderCustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private Service.Controls.SearchLookUpEditEx lup_OrderId;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcOrderNo;
        private DevExpress.XtraLayout.LayoutControlItem lcOrderDate;
        private DevExpress.XtraLayout.LayoutControlItem lcDueDate;
        private DevExpress.XtraLayout.LayoutControlItem lcOrderManagerName;
        private DevExpress.XtraLayout.LayoutControlItem lcOrderId;
        private DevExpress.XtraLayout.LayoutControlItem lcMemo;
        private DevExpress.XtraLayout.LayoutControlItem lcOrderCustomer;
    }
}