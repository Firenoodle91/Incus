﻿namespace HKInc.Ui.View.View.MPS_POPUP
{
    partial class XPFMPS1801
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFMPS1801));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_WhCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.spin_StockQty = new DevExpress.XtraEditors.SpinEdit();
            this.lup_Position = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCurrentWhInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcWhName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPositionName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcStockQty = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_WhCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_StockQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Position.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrentWhInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWhName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPositionName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStockQty)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_WhCode);
            this.layoutControl1.Controls.Add(this.spin_StockQty);
            this.layoutControl1.Controls.Add(this.lup_Position);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(304, 173);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_WhCode
            // 
            this.lup_WhCode.Constraint = null;
            this.lup_WhCode.DataSource = null;
            this.lup_WhCode.DisplayMember = "";
            this.lup_WhCode.isImeModeDisable = false;
            this.lup_WhCode.isRequired = false;
            this.lup_WhCode.Location = new System.Drawing.Point(73, 56);
            this.lup_WhCode.Name = "lup_WhCode";
            this.lup_WhCode.NullText = "";
            this.lup_WhCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_WhCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_WhCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_WhCode.Properties.NullText = "";
            this.lup_WhCode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_WhCode.Properties.ReadOnly = true;
            this.lup_WhCode.Size = new System.Drawing.Size(200, 24);
            this.lup_WhCode.StyleController = this.layoutControl1;
            this.lup_WhCode.TabIndex = 0;
            this.lup_WhCode.Value_1 = null;
            this.lup_WhCode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // spin_StockQty
            // 
            this.spin_StockQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_StockQty.Location = new System.Drawing.Point(73, 116);
            this.spin_StockQty.Name = "spin_StockQty";
            this.spin_StockQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_StockQty.Size = new System.Drawing.Size(200, 24);
            this.spin_StockQty.StyleController = this.layoutControl1;
            this.spin_StockQty.TabIndex = 2;
            // 
            // lup_Position
            // 
            this.lup_Position.Constraint = null;
            this.lup_Position.DataSource = null;
            this.lup_Position.DisplayMember = "";
            this.lup_Position.isImeModeDisable = false;
            this.lup_Position.isRequired = false;
            this.lup_Position.Location = new System.Drawing.Point(73, 86);
            this.lup_Position.Name = "lup_Position";
            this.lup_Position.NullText = "";
            this.lup_Position.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Position.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Position.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Position.Properties.NullText = "";
            this.lup_Position.Properties.PopupView = this.gridView1;
            this.lup_Position.Properties.ReadOnly = true;
            this.lup_Position.Size = new System.Drawing.Size(200, 24);
            this.lup_Position.StyleController = this.layoutControl1;
            this.lup_Position.TabIndex = 1;
            this.lup_Position.Value_1 = null;
            this.lup_Position.ValueMember = "";
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
            this.lcCurrentWhInfo});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(304, 173);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcCurrentWhInfo
            // 
            this.lcCurrentWhInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcWhName,
            this.lcPositionName,
            this.lcStockQty});
            this.lcCurrentWhInfo.Location = new System.Drawing.Point(0, 0);
            this.lcCurrentWhInfo.Name = "lcCurrentWhInfo";
            this.lcCurrentWhInfo.OptionsItemText.TextToControlDistance = 4;
            this.lcCurrentWhInfo.Size = new System.Drawing.Size(278, 147);
            this.lcCurrentWhInfo.Text = "현 창고위치정보";
            // 
            // lcWhName
            // 
            this.lcWhName.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcWhName.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcWhName.Control = this.lup_WhCode;
            this.lcWhName.Location = new System.Drawing.Point(0, 0);
            this.lcWhName.Name = "lcWhName";
            this.lcWhName.Size = new System.Drawing.Size(248, 30);
            this.lcWhName.Text = "창고명";
            this.lcWhName.TextSize = new System.Drawing.Size(39, 18);
            // 
            // lcPositionName
            // 
            this.lcPositionName.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcPositionName.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcPositionName.Control = this.lup_Position;
            this.lcPositionName.CustomizationFormText = "layoutControlItem1";
            this.lcPositionName.Location = new System.Drawing.Point(0, 30);
            this.lcPositionName.Name = "lcPositionName";
            this.lcPositionName.Size = new System.Drawing.Size(248, 30);
            this.lcPositionName.Text = "위치명";
            this.lcPositionName.TextSize = new System.Drawing.Size(39, 18);
            // 
            // lcStockQty
            // 
            this.lcStockQty.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcStockQty.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcStockQty.Control = this.spin_StockQty;
            this.lcStockQty.Location = new System.Drawing.Point(0, 60);
            this.lcStockQty.Name = "lcStockQty";
            this.lcStockQty.Size = new System.Drawing.Size(248, 32);
            this.lcStockQty.Text = "재고량";
            this.lcStockQty.TextSize = new System.Drawing.Size(39, 18);
            // 
            // XPFMPS1801
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 246);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFMPS1801";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "창고위치변경";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_WhCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_StockQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Position.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrentWhInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcWhName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPositionName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStockQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SpinEdit spin_StockQty;
        private DevExpress.XtraLayout.LayoutControlItem lcStockQty;
        private DevExpress.XtraLayout.LayoutControlGroup lcCurrentWhInfo;
        private Service.Controls.SearchLookUpEditEx lup_WhCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcWhName;
        private Service.Controls.SearchLookUpEditEx lup_Position;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcPositionName;
    }
}