namespace HKInc.Ui.View.View.ORD
{
    partial class XFORD1200
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFORD1200));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_OrderStatus = new DevExpress.XtraEditors.SimpleButton();
            this.lup_CustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.dateOrderDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcOutDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutDetailList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.tx_Customerworkno = new DevExpress.XtraEditors.TextEdit();
            this.lcCustomerWorkNo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.btn_OrderStatus);
            this.layoutControl1.Controls.Add(this.lup_CustomerCode);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.dateOrderDate);
            this.layoutControl1.Controls.Add(this.tx_Customerworkno);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_OrderStatus
            // 
            this.btn_OrderStatus.AutoWidthInLayoutControl = true;
            this.btn_OrderStatus.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_OrderStatus.ImageOptions.Image")));
            this.btn_OrderStatus.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_OrderStatus.Location = new System.Drawing.Point(935, 50);
            this.btn_OrderStatus.Name = "btn_OrderStatus";
            this.btn_OrderStatus.Size = new System.Drawing.Size(111, 27);
            this.btn_OrderStatus.StyleController = this.layoutControl1;
            this.btn_OrderStatus.TabIndex = 2;
            this.btn_OrderStatus.Text = "수주현황(&O)";
            // 
            // lup_CustomerCode
            // 
            this.lup_CustomerCode.Constraint = null;
            this.lup_CustomerCode.DataSource = null;
            this.lup_CustomerCode.DisplayMember = "";
            this.lup_CustomerCode.isImeModeDisable = false;
            this.lup_CustomerCode.isRequired = false;
            this.lup_CustomerCode.Location = new System.Drawing.Point(527, 50);
            this.lup_CustomerCode.Name = "lup_CustomerCode";
            this.lup_CustomerCode.NullText = "";
            this.lup_CustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_CustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_CustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_CustomerCode.Properties.NullText = "";
            this.lup_CustomerCode.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_CustomerCode.Size = new System.Drawing.Size(62, 24);
            this.lup_CustomerCode.StyleController = this.layoutControl1;
            this.lup_CustomerCode.TabIndex = 1;
            this.lup_CustomerCode.Value_1 = null;
            this.lup_CustomerCode.ValueMember = "";
            // 
            // searchGridLookUpEditEx1View
            // 
            this.searchGridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchGridLookUpEditEx1View.Name = "searchGridLookUpEditEx1View";
            this.searchGridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchGridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(24, 401);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx2.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(1022, 242);
            this.gridEx2.TabIndex = 4;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 131);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 204);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // dateOrderDate
            // 
            this.dateOrderDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dateOrderDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOrderDate.Appearance.Options.UseBackColor = true;
            this.dateOrderDate.Appearance.Options.UseFont = true;
            this.dateOrderDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.dateOrderDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.dateOrderDate.Location = new System.Drawing.Point(152, 50);
            this.dateOrderDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dateOrderDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dateOrderDate.MinimumSize = new System.Drawing.Size(243, 26);
            this.dateOrderDate.Name = "dateOrderDate";
            this.dateOrderDate.Size = new System.Drawing.Size(243, 26);
            this.dateOrderDate.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.lcOutMasterList,
            this.lcOutDetailList,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcOutDate,
            this.emptySpaceItem2,
            this.lcCustomer,
            this.layoutControlItem1,
            this.lcCustomerWorkNo});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1050, 81);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // lcOutDate
            // 
            this.lcOutDate.Control = this.dateOrderDate;
            this.lcOutDate.Location = new System.Drawing.Point(0, 0);
            this.lcOutDate.Name = "lcOutDate";
            this.lcOutDate.Size = new System.Drawing.Size(375, 31);
            this.lcOutDate.TextSize = new System.Drawing.Size(124, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(763, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(148, 31);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcCustomer
            // 
            this.lcCustomer.Control = this.lup_CustomerCode;
            this.lcCustomer.Location = new System.Drawing.Point(375, 0);
            this.lcCustomer.Name = "lcCustomer";
            this.lcCustomer.Size = new System.Drawing.Size(194, 31);
            this.lcCustomer.TextSize = new System.Drawing.Size(124, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_OrderStatus;
            this.layoutControlItem1.Location = new System.Drawing.Point(911, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(115, 31);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcOutMasterList
            // 
            this.lcOutMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcOutMasterList.Location = new System.Drawing.Point(0, 81);
            this.lcOutMasterList.Name = "lcOutMasterList";
            this.lcOutMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcOutMasterList.Size = new System.Drawing.Size(1050, 258);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1026, 208);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcOutDetailList
            // 
            this.lcOutDetailList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcOutDetailList.Location = new System.Drawing.Point(0, 351);
            this.lcOutDetailList.Name = "lcOutDetailList";
            this.lcOutDetailList.OptionsItemText.TextToControlDistance = 4;
            this.lcOutDetailList.Size = new System.Drawing.Size(1050, 296);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1026, 246);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 339);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1050, 12);
            // 
            // tx_Customerworkno
            // 
            this.tx_Customerworkno.Location = new System.Drawing.Point(721, 50);
            this.tx_Customerworkno.Name = "tx_Customerworkno";
            this.tx_Customerworkno.Size = new System.Drawing.Size(62, 24);
            this.tx_Customerworkno.StyleController = this.layoutControl1;
            this.tx_Customerworkno.TabIndex = 5;
            // 
            // lcCustomerWorkNo
            // 
            this.lcCustomerWorkNo.Control = this.tx_Customerworkno;
            this.lcCustomerWorkNo.Location = new System.Drawing.Point(569, 0);
            this.lcCustomerWorkNo.Name = "lcCustomerWorkNo";
            this.lcCustomerWorkNo.Size = new System.Drawing.Size(194, 31);
            this.lcCustomerWorkNo.TextSize = new System.Drawing.Size(124, 18);
            // 
            // XFORD1200
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFORD1200";
            this.Text = "XFORD1200";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Customerworkno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerWorkNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private Service.Controls.DatePeriodEditEx dateOrderDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem lcOutDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcOutMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcOutDetailList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_CustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomer;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.SimpleButton btn_OrderStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit tx_Customerworkno;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerWorkNo;
    }
}