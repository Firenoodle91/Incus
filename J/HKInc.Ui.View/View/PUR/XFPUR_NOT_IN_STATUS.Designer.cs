namespace HKInc.Ui.View.View.PUR
{
    partial class XFPUR_NOT_IN_STATUS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFPUR_NOT_IN_STATUS));
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.dt_SearchDate = new HKInc.Service.Controls.DatePeriodEditEx();
            this.lup_Item = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchGridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPurInStatusList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout)).BeginInit();
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPurInStatusList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // ListMasterDetailFormTemplatelayoutControl1ConvertedLayout
            // 
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.dt_SearchDate);
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.lup_Item);
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.gridEx1);
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 24);
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.Name = "ListMasterDetailFormTemplatelayoutControl1ConvertedLayout";
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.Size = new System.Drawing.Size(936, 518);
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.TabIndex = 0;
            // 
            // dt_SearchDate
            // 
            this.dt_SearchDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dt_SearchDate.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_SearchDate.Appearance.Options.UseBackColor = true;
            this.dt_SearchDate.Appearance.Options.UseFont = true;
            this.dt_SearchDate.EditFrValue = new System.DateTime(2020, 3, 14, 0, 0, 0, 0);
            this.dt_SearchDate.EditToValue = new System.DateTime(2020, 4, 14, 23, 59, 59, 990);
            this.dt_SearchDate.Location = new System.Drawing.Point(59, 41);
            this.dt_SearchDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dt_SearchDate.MaximumSize = new System.Drawing.Size(200, 20);
            this.dt_SearchDate.MinimumSize = new System.Drawing.Size(175, 16);
            this.dt_SearchDate.Name = "dt_SearchDate";
            this.dt_SearchDate.Size = new System.Drawing.Size(180, 20);
            this.dt_SearchDate.TabIndex = 0;
            // 
            // lup_Item
            // 
            this.lup_Item.Constraint = null;
            this.lup_Item.DataSource = null;
            this.lup_Item.DisplayMember = "";
            this.lup_Item.isImeModeDisable = false;
            this.lup_Item.isRequired = false;
            this.lup_Item.Location = new System.Drawing.Point(280, 41);
            this.lup_Item.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lup_Item.Name = "lup_Item";
            this.lup_Item.NullText = "";
            this.lup_Item.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Item.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Item.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Item.Properties.NullText = "";
            this.lup_Item.Properties.PopupView = this.searchGridLookUpEditEx1View;
            this.lup_Item.Size = new System.Drawing.Size(112, 20);
            this.lup_Item.StyleController = this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout;
            this.lup_Item.TabIndex = 1;
            this.lup_Item.Value_1 = null;
            this.lup_Item.ValueMember = "";
            // 
            // searchGridLookUpEditEx1View
            // 
            this.searchGridLookUpEditEx1View.DetailHeight = 272;
            this.searchGridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchGridLookUpEditEx1View.Name = "searchGridLookUpEditEx1View";
            this.searchGridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchGridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(22, 106);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(892, 392);
            this.gridEx1.TabIndex = 2;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCondition,
            this.lcPurInStatusList});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lcItem,
            this.lcDate});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.Size = new System.Drawing.Size(918, 65);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(374, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(522, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItem
            // 
            this.lcItem.Control = this.lup_Item;
            this.lcItem.Location = new System.Drawing.Point(221, 0);
            this.lcItem.Name = "lcItem";
            this.lcItem.Size = new System.Drawing.Size(153, 24);
            this.lcItem.TextSize = new System.Drawing.Size(34, 14);
            // 
            // lcDate
            // 
            this.lcDate.Control = this.dt_SearchDate;
            this.lcDate.Location = new System.Drawing.Point(0, 0);
            this.lcDate.Name = "lcDate";
            this.lcDate.Size = new System.Drawing.Size(221, 24);
            this.lcDate.TextSize = new System.Drawing.Size(34, 14);
            // 
            // lcPurInStatusList
            // 
            this.lcPurInStatusList.CustomizationFormText = "출고정보";
            this.lcPurInStatusList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lcPurInStatusList.Location = new System.Drawing.Point(0, 65);
            this.lcPurInStatusList.Name = "lcPurInStatusList";
            this.lcPurInStatusList.Size = new System.Drawing.Size(918, 437);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridEx1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(896, 396);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // XFPUR_NOT_IN_STATUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "XFPUR_NOT_IN_STATUS";
            this.Text = "XFPUR_NOT_IN_STATUS";
            this.Controls.SetChildIndex(this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout)).EndInit();
            this.ListMasterDetailFormTemplatelayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Item.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPurInStatusList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl ListMasterDetailFormTemplatelayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcPurInStatusList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private Service.Controls.SearchLookUpEditEx lup_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView searchGridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcItem;
        private Service.Controls.DatePeriodEditEx dt_SearchDate;
        private DevExpress.XtraLayout.LayoutControlItem lcDate;
    }
}