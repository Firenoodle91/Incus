namespace HKInc.Ui.View.View.REPORT
{
    partial class XRREP5011
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRREP5011));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_ItemCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lup_CustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCustomerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSalesList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.OutDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.lcOutDate = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSalesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDate)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_ItemCode);
            this.layoutControl1.Controls.Add(this.lup_CustomerCode);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.OutDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(969, 518);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_ItemCode
            // 
            this.lup_ItemCode.Constraint = null;
            this.lup_ItemCode.DataSource = null;
            this.lup_ItemCode.DisplayMember = "";
            this.lup_ItemCode.isImeModeDisable = false;
            this.lup_ItemCode.isRequired = false;
            this.lup_ItemCode.Location = new System.Drawing.Point(429, 41);
            this.lup_ItemCode.Name = "lup_ItemCode";
            this.lup_ItemCode.NullText = "";
            this.lup_ItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_ItemCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_ItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_ItemCode.Properties.NullText = "";
            this.lup_ItemCode.Properties.PopupView = this.gridView1;
            this.lup_ItemCode.Size = new System.Drawing.Size(170, 20);
            this.lup_ItemCode.StyleController = this.layoutControl1;
            this.lup_ItemCode.TabIndex = 1;
            this.lup_ItemCode.Value_1 = null;
            this.lup_ItemCode.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 272;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lup_CustomerCode
            // 
            this.lup_CustomerCode.Constraint = null;
            this.lup_CustomerCode.DataSource = null;
            this.lup_CustomerCode.DisplayMember = "";
            this.lup_CustomerCode.isImeModeDisable = false;
            this.lup_CustomerCode.isRequired = false;
            this.lup_CustomerCode.Location = new System.Drawing.Point(698, 41);
            this.lup_CustomerCode.Name = "lup_CustomerCode";
            this.lup_CustomerCode.NullText = "";
            this.lup_CustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_CustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_CustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_CustomerCode.Properties.NullText = "";
            this.lup_CustomerCode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_CustomerCode.Size = new System.Drawing.Size(249, 20);
            this.lup_CustomerCode.StyleController = this.layoutControl1;
            this.lup_CustomerCode.TabIndex = 2;
            this.lup_CustomerCode.Value_1 = null;
            this.lup_CustomerCode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.DetailHeight = 272;
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(22, 106);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(925, 392);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcSalesList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(969, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemName,
            this.lcCustomerName,
            this.lcOutDate});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(951, 65);
            this.lcCondition.Text = "조회조건";
            // 
            // lcCustomerName
            // 
            this.lcCustomerName.Control = this.lup_CustomerCode;
            this.lcCustomerName.Location = new System.Drawing.Point(581, 0);
            this.lcCustomerName.Name = "lcCustomerName";
            this.lcCustomerName.Size = new System.Drawing.Size(348, 24);
            this.lcCustomerName.TextSize = new System.Drawing.Size(91, 14);
            // 
            // lcItemName
            // 
            this.lcItemName.Control = this.lup_ItemCode;
            this.lcItemName.Location = new System.Drawing.Point(312, 0);
            this.lcItemName.Name = "lcItemName";
            this.lcItemName.Size = new System.Drawing.Size(269, 24);
            this.lcItemName.TextSize = new System.Drawing.Size(91, 14);
            // 
            // lcSalesList
            // 
            this.lcSalesList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcSalesList.Location = new System.Drawing.Point(0, 65);
            this.lcSalesList.Name = "lcSalesList";
            this.lcSalesList.OptionsItemText.TextToControlDistance = 4;
            this.lcSalesList.Size = new System.Drawing.Size(951, 437);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(929, 396);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // OutDate
            // 
            this.OutDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.OutDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutDate.Appearance.Options.UseBackColor = true;
            this.OutDate.Appearance.Options.UseFont = true;
            this.OutDate.EditFrValue = new System.DateTime(2019, 1, 27, 0, 0, 0, 0);
            this.OutDate.EditToValue = new System.DateTime(2019, 2, 27, 23, 59, 59, 990);
            this.OutDate.Location = new System.Drawing.Point(117, 41);
            this.OutDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OutDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.OutDate.MinimumSize = new System.Drawing.Size(213, 20);
            this.OutDate.Name = "OutDate";
            this.OutDate.Size = new System.Drawing.Size(213, 20);
            this.OutDate.TabIndex = 0;
            // 
            // lcOutDate
            // 
            this.lcOutDate.Control = this.OutDate;
            this.lcOutDate.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcOutDate.CustomizationFormText = "lcOutDate";
            this.lcOutDate.Location = new System.Drawing.Point(0, 0);
            this.lcOutDate.Name = "lcOutDate";
            this.lcOutDate.Size = new System.Drawing.Size(312, 24);
            this.lcOutDate.Text = "lcOutDate";
            this.lcOutDate.TextSize = new System.Drawing.Size(91, 14);
            // 
            // XRREP5001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 565);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XRREP5001";
            this.Text = "XRREP5000";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_CustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSalesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcSalesList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_CustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerName;
        private Service.Controls.SearchLookUpEditEx lup_ItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcItemName;
        private Service.Controls.DatePeriodEditEx OutDate;
        private DevExpress.XtraLayout.LayoutControlItem lcOutDate;
    }
}