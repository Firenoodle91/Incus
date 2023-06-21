namespace HKInc.Ui.View.View.QCT
{
    partial class XFQCT1600
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFQCT1600));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_AQL = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcAQL = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReportSamplingList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lup_Customer = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.lcCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_AQL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReportSamplingList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Customer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_Customer);
            this.layoutControl1.Controls.Add(this.lup_AQL);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(997, 488);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_AQL
            // 
            this.lup_AQL.Constraint = null;
            this.lup_AQL.DataSource = null;
            this.lup_AQL.DisplayMember = "";
            this.lup_AQL.isImeModeDisable = false;
            this.lup_AQL.isRequired = false;
            this.lup_AQL.Location = new System.Drawing.Point(88, 43);
            this.lup_AQL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_AQL.Name = "lup_AQL";
            this.lup_AQL.NullText = "";
            this.lup_AQL.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_AQL.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_AQL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_AQL.Properties.NullText = "";
            this.lup_AQL.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_AQL.Size = new System.Drawing.Size(160, 20);
            this.lup_AQL.StyleController = this.layoutControl1;
            this.lup_AQL.TabIndex = 10;
            this.lup_AQL.Value_1 = null;
            this.lup_AQL.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 110);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(949, 354);
            this.gridEx1.TabIndex = 7;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcReportSamplingList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(997, 488);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcAQL,
            this.lcCustomer});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(977, 67);
            this.lcCondition.Text = "조회조건";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(451, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(502, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcAQL
            // 
            this.lcAQL.Control = this.lup_AQL;
            this.lcAQL.Location = new System.Drawing.Point(0, 0);
            this.lcAQL.Name = "lcAQL";
            this.lcAQL.Size = new System.Drawing.Size(228, 24);
            this.lcAQL.TextSize = new System.Drawing.Size(60, 14);
            // 
            // lcReportSamplingList
            // 
            this.lcReportSamplingList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcReportSamplingList.Location = new System.Drawing.Point(0, 67);
            this.lcReportSamplingList.Name = "lcReportSamplingList";
            this.lcReportSamplingList.OptionsItemText.TextToControlDistance = 4;
            this.lcReportSamplingList.Size = new System.Drawing.Size(977, 401);
            this.lcReportSamplingList.Text = "성적서샘플링목록";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(953, 358);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // lup_Customer
            // 
            this.lup_Customer.Constraint = null;
            this.lup_Customer.DataSource = null;
            this.lup_Customer.DisplayMember = "";
            this.lup_Customer.isImeModeDisable = false;
            this.lup_Customer.isRequired = false;
            this.lup_Customer.Location = new System.Drawing.Point(316, 43);
            this.lup_Customer.Name = "lup_Customer";
            this.lup_Customer.NullText = "";
            this.lup_Customer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Customer.Properties.NullText = "";
            this.lup_Customer.Properties.PopupView = this.gridView1;
            this.lup_Customer.Size = new System.Drawing.Size(155, 20);
            this.lup_Customer.StyleController = this.layoutControl1;
            this.lup_Customer.TabIndex = 11;
            this.lup_Customer.Value_1 = null;
            this.lup_Customer.ValueMember = "";
            // 
            // lcCustomer
            // 
            this.lcCustomer.Control = this.lup_Customer;
            this.lcCustomer.Location = new System.Drawing.Point(228, 0);
            this.lcCustomer.Name = "lcCustomer";
            this.lcCustomer.Size = new System.Drawing.Size(223, 24);
            this.lcCustomer.TextSize = new System.Drawing.Size(60, 14);
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // XFQCT1600
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 544);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XFQCT1600";
            this.Text = "XFQCT1600";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_AQL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReportSamplingList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Customer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcReportSamplingList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_AQL;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcAQL;
        private Service.Controls.SearchLookUpEditEx lup_Customer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcCustomer;
    }
}