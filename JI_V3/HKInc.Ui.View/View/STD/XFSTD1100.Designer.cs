namespace HKInc.Ui.View.View.STD
{
    partial class XFSTD1100
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFSTD1100));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rbo_UseFlag = new HKInc.Service.Controls.HK_UseFlagRadioGroup();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.tx_ItemCodeName = new DevExpress.XtraEditors.TextEdit();
            this.lup_ProductTeamCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCondition = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcItemCodeName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProductTeam = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lup_cust = new HKInc.Service.Controls.GridLookUpEditEx();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_ItemCodeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ProductTeamCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCodeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductTeam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_cust.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditEx1View)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_cust);
            this.layoutControl1.Controls.Add(this.rbo_UseFlag);
            this.layoutControl1.Controls.Add(this.gridEx1);
            this.layoutControl1.Controls.Add(this.tx_ItemCodeName);
            this.layoutControl1.Controls.Add(this.lup_ProductTeamCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // rbo_UseFlag
            // 
            this.rbo_UseFlag.Location = new System.Drawing.Point(836, 50);
            this.rbo_UseFlag.MaximumSize = new System.Drawing.Size(210, 30);
            this.rbo_UseFlag.MinimumSize = new System.Drawing.Size(190, 30);
            this.rbo_UseFlag.Name = "rbo_UseFlag";
            this.rbo_UseFlag.SelectedValue = "Y";
            this.rbo_UseFlag.Size = new System.Drawing.Size(210, 30);
            this.rbo_UseFlag.TabIndex = 2;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(24, 134);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridEx1.Menu = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1022, 509);
            this.gridEx1.TabIndex = 3;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // tx_ItemCodeName
            // 
            this.tx_ItemCodeName.Location = new System.Drawing.Point(99, 50);
            this.tx_ItemCodeName.Name = "tx_ItemCodeName";
            this.tx_ItemCodeName.Size = new System.Drawing.Size(182, 24);
            this.tx_ItemCodeName.StyleController = this.layoutControl1;
            this.tx_ItemCodeName.TabIndex = 0;
            // 
            // lup_ProductTeamCode
            // 
            this.lup_ProductTeamCode.Constraint = null;
            this.lup_ProductTeamCode.DataSource = null;
            this.lup_ProductTeamCode.DisplayMember = "";
            this.lup_ProductTeamCode.isImeModeDisable = false;
            this.lup_ProductTeamCode.isRequired = false;
            this.lup_ProductTeamCode.Location = new System.Drawing.Point(360, 50);
            this.lup_ProductTeamCode.Name = "lup_ProductTeamCode";
            this.lup_ProductTeamCode.NullText = "";
            this.lup_ProductTeamCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_ProductTeamCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_ProductTeamCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_ProductTeamCode.Properties.NullText = "";
            this.lup_ProductTeamCode.Properties.PopupView = this.gridView1;
            this.lup_ProductTeamCode.Size = new System.Drawing.Size(222, 24);
            this.lup_ProductTeamCode.StyleController = this.layoutControl1;
            this.lup_ProductTeamCode.TabIndex = 1;
            this.lup_ProductTeamCode.Value_1 = null;
            this.lup_ProductTeamCode.ValueMember = "";
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
            this.lcCondition,
            this.lcItemList});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 667);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCondition
            // 
            this.lcCondition.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemCodeName,
            this.lcProductTeam,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.lcCondition.Location = new System.Drawing.Point(0, 0);
            this.lcCondition.Name = "lcCondition";
            this.lcCondition.OptionsItemText.TextToControlDistance = 4;
            this.lcCondition.Size = new System.Drawing.Size(1050, 84);
            this.lcCondition.Text = "조회조건";
            // 
            // lcItemCodeName
            // 
            this.lcItemCodeName.Control = this.tx_ItemCodeName;
            this.lcItemCodeName.Location = new System.Drawing.Point(0, 0);
            this.lcItemCodeName.Name = "lcItemCodeName";
            this.lcItemCodeName.Size = new System.Drawing.Size(261, 34);
            this.lcItemCodeName.Text = "품목코드/명";
            this.lcItemCodeName.TextSize = new System.Drawing.Size(71, 18);
            // 
            // lcProductTeam
            // 
            this.lcProductTeam.Control = this.lup_ProductTeamCode;
            this.lcProductTeam.CustomizationFormText = "lcProductTeam";
            this.lcProductTeam.Location = new System.Drawing.Point(261, 0);
            this.lcProductTeam.Name = "lcProductTeam";
            this.lcProductTeam.Size = new System.Drawing.Size(301, 34);
            this.lcProductTeam.Text = "제조팀";
            this.lcProductTeam.TextSize = new System.Drawing.Size(71, 18);
            this.lcProductTeam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(756, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(56, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.rbo_UseFlag;
            this.layoutControlItem1.Location = new System.Drawing.Point(812, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(214, 34);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcItemList
            // 
            this.lcItemList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.lcItemList.Location = new System.Drawing.Point(0, 84);
            this.lcItemList.Name = "lcItemList";
            this.lcItemList.OptionsItemText.TextToControlDistance = 4;
            this.lcItemList.Size = new System.Drawing.Size(1050, 563);
            this.lcItemList.Text = "품목리스트";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1026, 513);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(509, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(308, 36);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lup_cust
            // 
            this.lup_cust.Constraint = null;
            this.lup_cust.DataSource = null;
            this.lup_cust.DisplayMember = "";
            this.lup_cust.isRequired = false;
            this.lup_cust.Location = new System.Drawing.Point(661, 50);
            this.lup_cust.Name = "lup_cust";
            this.lup_cust.NullText = "";
            this.lup_cust.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_cust.Properties.NullText = "";
            this.lup_cust.Properties.PopupView = this.gridLookUpEditEx1View;
            this.lup_cust.SelectedIndex = -1;
            this.lup_cust.Size = new System.Drawing.Size(115, 24);
            this.lup_cust.StyleController = this.layoutControl1;
            this.lup_cust.TabIndex = 4;
            this.lup_cust.Value_1 = null;
            this.lup_cust.ValueMember = "";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lup_cust;
            this.layoutControlItem2.Location = new System.Drawing.Point(562, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(194, 34);
            this.layoutControlItem2.Text = "주거래처";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(71, 18);
            // 
            // gridLookUpEditEx1View
            // 
            this.gridLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEditEx1View.Name = "gridLookUpEditEx1View";
            this.gridLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // XFSTD1100
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XFSTD1100";
            this.Text = "XFSTD1100";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_ItemCodeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_ProductTeamCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCodeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProductTeam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_cust.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditEx1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit tx_ItemCodeName;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCodeName;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup lcCondition;
        private DevExpress.XtraLayout.LayoutControlGroup lcItemList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Service.Controls.SearchLookUpEditEx lup_ProductTeamCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcProductTeam;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private Service.Controls.HK_UseFlagRadioGroup rbo_UseFlag;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.GridLookUpEditEx lup_cust;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}