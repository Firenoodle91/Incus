namespace HKInc.Ui.View.View.QCT
{
    partial class XFQCT1201
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFQCT1201));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Iup_CustomerCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tx_ReleaseNumber = new DevExpress.XtraEditors.TextEdit();
            this.lup_ItemCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx2 = new HKInc.Service.Controls.GridEx();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.dt_OutDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcOutDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.IcReleaseNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustomerCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOutMasterList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubDetailGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Iup_CustomerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ReleaseNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IcReleaseNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutMasterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Iup_CustomerCode);
            this.layoutControl1.Controls.Add(this.tx_ReleaseNumber);
            this.layoutControl1.Controls.Add(this.lup_ItemCode);
            this.layoutControl1.Controls.Add(this.gridEx2);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.dt_OutDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1160, 652);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Iup_CustomerCode
            // 
            this.Iup_CustomerCode.Constraint = null;
            this.Iup_CustomerCode.DataSource = null;
            this.Iup_CustomerCode.DisplayMember = "";
            this.Iup_CustomerCode.isImeModeDisable = false;
            this.Iup_CustomerCode.isRequired = false;
            this.Iup_CustomerCode.Location = new System.Drawing.Point(879, 56);
            this.Iup_CustomerCode.Name = "Iup_CustomerCode";
            this.Iup_CustomerCode.NullText = "";
            this.Iup_CustomerCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.Iup_CustomerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Iup_CustomerCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Iup_CustomerCode.Properties.NullText = "";
            this.Iup_CustomerCode.Properties.PopupView = this.gridView1;
            this.Iup_CustomerCode.Size = new System.Drawing.Size(250, 24);
            this.Iup_CustomerCode.StyleController = this.layoutControl1;
            this.Iup_CustomerCode.TabIndex = 3;
            this.Iup_CustomerCode.Value_1 = null;
            this.Iup_CustomerCode.ValueMember = "";
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // tx_ReleaseNumber
            // 
            this.tx_ReleaseNumber.Location = new System.Drawing.Point(392, 56);
            this.tx_ReleaseNumber.Name = "tx_ReleaseNumber";
            this.tx_ReleaseNumber.Size = new System.Drawing.Size(162, 24);
            this.tx_ReleaseNumber.StyleController = this.layoutControl1;
            this.tx_ReleaseNumber.TabIndex = 1;
            // 
            // lup_ItemCode
            // 
            this.lup_ItemCode.Constraint = null;
            this.lup_ItemCode.DataSource = null;
            this.lup_ItemCode.DisplayMember = "";
            this.lup_ItemCode.isImeModeDisable = false;
            this.lup_ItemCode.isRequired = false;
            this.lup_ItemCode.Location = new System.Drawing.Point(616, 56);
            this.lup_ItemCode.Name = "lup_ItemCode";
            this.lup_ItemCode.NullText = "";
            this.lup_ItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_ItemCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_ItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_ItemCode.Properties.NullText = "";
            this.lup_ItemCode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_ItemCode.Size = new System.Drawing.Size(201, 24);
            this.lup_ItemCode.StyleController = this.layoutControl1;
            this.lup_ItemCode.TabIndex = 2;
            this.lup_ItemCode.Value_1 = null;
            this.lup_ItemCode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx2
            // 
            this.gridEx2.DataSource = null;
            this.gridEx2.Location = new System.Drawing.Point(31, 148);
            this.gridEx2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx2.Name = "gridEx2";
            this.gridEx2.Size = new System.Drawing.Size(467, 467);
            this.gridEx2.TabIndex = 4;
            this.gridEx2.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(540, 148);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(589, 467);
            this.gridEx1.TabIndex = 5;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // dt_OutDate
            // 
            this.dt_OutDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_OutDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_OutDate.Appearance.Options.UseBackColor = true;
            this.dt_OutDate.Appearance.Options.UseFont = true;
            this.dt_OutDate.EditFrValue = new System.DateTime(2019, 1, 28, 0, 0, 0, 0);
            this.dt_OutDate.EditToValue = new System.DateTime(2019, 2, 28, 23, 59, 59, 990);
            this.dt_OutDate.Location = new System.Drawing.Point(87, 56);
            this.dt_OutDate.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.dt_OutDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_OutDate.MinimumSize = new System.Drawing.Size(243, 31);
            this.dt_OutDate.Name = "dt_OutDate";
            this.dt_OutDate.Size = new System.Drawing.Size(243, 31);
            this.dt_OutDate.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.lcOutMasterList,
            this.layoutControlGroup4,
            this.splitterItem2,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1160, 652);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcOutDate,
            this.lcItem,
            this.IcReleaseNumber,
            this.lcCustomerCode});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1134, 92);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // lcOutDate
            // 
            this.lcOutDate.Control = this.dt_OutDate;
            this.lcOutDate.CustomizationFormText = "출고일자";
            this.lcOutDate.Location = new System.Drawing.Point(0, 0);
            this.lcOutDate.Name = "lcOutDate";
            this.lcOutDate.Size = new System.Drawing.Size(305, 37);
            this.lcOutDate.Text = "출고일자";
            this.lcOutDate.TextSize = new System.Drawing.Size(52, 18);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_ItemCode;
            this.lcItem.Location = new System.Drawing.Point(529, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(263, 37);
            this.lcItem.Text = "품목";
            this.lcItem.TextSize = new System.Drawing.Size(52, 18);
            // 
            // IcReleaseNumber
            // 
            this.IcReleaseNumber.Control = this.tx_ReleaseNumber;
            this.IcReleaseNumber.Location = new System.Drawing.Point(305, 0);
            this.IcReleaseNumber.Name = "IcReleaseNumber";
            this.IcReleaseNumber.Size = new System.Drawing.Size(224, 37);
            this.IcReleaseNumber.Text = "출고번호";
            this.IcReleaseNumber.TextSize = new System.Drawing.Size(52, 18);
            // 
            // lcCustomerCode
            // 
            this.lcCustomerCode.Control = this.Iup_CustomerCode;
            this.lcCustomerCode.Location = new System.Drawing.Point(792, 0);
            this.lcCustomerCode.Name = "lcCustomerCode";
            this.lcCustomerCode.Size = new System.Drawing.Size(312, 37);
            this.lcCustomerCode.Text = "거래처";
            this.lcCustomerCode.TextSize = new System.Drawing.Size(52, 18);
            // 
            // lcOutMasterList
            // 
            this.lcOutMasterList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcOutMasterList.Location = new System.Drawing.Point(0, 92);
            this.lcOutMasterList.Name = "lcOutMasterList";
            this.lcOutMasterList.OptionsItemText.TextToControlDistance = 4;
            this.lcOutMasterList.Size = new System.Drawing.Size(503, 528);
            this.lcOutMasterList.Text = "LOT역 추적";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridEx2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(473, 473);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(509, 92);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup4.Size = new System.Drawing.Size(625, 528);
            this.layoutControlGroup4.Text = "LOT추적";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridEx1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(595, 473);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.CustomizationFormText = "splitterItem2";
            this.splitterItem2.Location = new System.Drawing.Point(0, 620);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(1134, 6);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(503, 92);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(6, 528);
            // 
            // XFQCT1201
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 725);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "XFQCT1201";
            this.Text = "XFQCT1201";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MasterGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubDetailGridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Iup_CustomerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ReleaseNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IcReleaseNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomerCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOutMasterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.DatePeriodEditEx dt_OutDate;
        private DevExpress.XtraLayout.LayoutControlItem lcOutDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private Service.Controls.GridEx gridEx2;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcOutMasterList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private Service.Controls.SearchLookUpEditEx lup_ItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private DevExpress.XtraEditors.TextEdit tx_ReleaseNumber;
        private DevExpress.XtraLayout.LayoutControlItem IcReleaseNumber;
        private Service.Controls.SearchLookUpEditEx Iup_CustomerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomerCode;
    }
}