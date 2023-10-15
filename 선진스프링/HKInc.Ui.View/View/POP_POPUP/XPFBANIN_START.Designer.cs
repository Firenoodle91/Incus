namespace HKInc.Ui.View.View.POP_POPUP
{
    partial class XPFBANIN_START
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFBANIN_START));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_Machine = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tx_BanOutLotNo = new DevExpress.XtraEditors.TextEdit();
            this.tx_BanItemName = new DevExpress.XtraEditors.TextEdit();
            this.tx_BanAvailableQty = new DevExpress.XtraEditors.TextEdit();
            this.btn_Start = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.lup_MachineGroup = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcBanProductLotNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcBanItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcBanAvailableQty = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            this.lcMachine = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcMachineGroup = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Machine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_BanOutLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_BanItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_BanAvailableQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MachineGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBanProductLotNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBanItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBanAvailableQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachineGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_Machine);
            this.layoutControl1.Controls.Add(this.tx_BanOutLotNo);
            this.layoutControl1.Controls.Add(this.tx_BanItemName);
            this.layoutControl1.Controls.Add(this.tx_BanAvailableQty);
            this.layoutControl1.Controls.Add(this.btn_Start);
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.lup_MachineGroup);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(637, 261);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_Machine
            // 
            this.lup_Machine.Constraint = null;
            this.lup_Machine.DataSource = null;
            this.lup_Machine.DisplayMember = "";
            this.lup_Machine.isImeModeDisable = false;
            this.lup_Machine.isRequired = false;
            this.lup_Machine.Location = new System.Drawing.Point(435, 16);
            this.lup_Machine.Name = "lup_Machine";
            this.lup_Machine.NullText = "";
            this.lup_Machine.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Machine.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lup_Machine.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_Machine.Properties.Appearance.Options.UseFont = true;
            this.lup_Machine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Machine.Properties.NullText = "";
            this.lup_Machine.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_Machine.Size = new System.Drawing.Size(186, 34);
            this.lup_Machine.StyleController = this.layoutControl1;
            this.lup_Machine.TabIndex = 1;
            this.lup_Machine.Value_1 = null;
            this.lup_Machine.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // tx_BanOutLotNo
            // 
            this.tx_BanOutLotNo.Location = new System.Drawing.Point(210, 56);
            this.tx_BanOutLotNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tx_BanOutLotNo.Name = "tx_BanOutLotNo";
            this.tx_BanOutLotNo.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tx_BanOutLotNo.Properties.Appearance.Options.UseFont = true;
            this.tx_BanOutLotNo.Size = new System.Drawing.Size(411, 34);
            this.tx_BanOutLotNo.StyleController = this.layoutControl1;
            this.tx_BanOutLotNo.TabIndex = 2;
            // 
            // tx_BanItemName
            // 
            this.tx_BanItemName.Location = new System.Drawing.Point(210, 96);
            this.tx_BanItemName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tx_BanItemName.Name = "tx_BanItemName";
            this.tx_BanItemName.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tx_BanItemName.Properties.Appearance.Options.UseFont = true;
            this.tx_BanItemName.Properties.Appearance.Options.UseTextOptions = true;
            this.tx_BanItemName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tx_BanItemName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tx_BanItemName.Properties.ReadOnly = true;
            this.tx_BanItemName.Size = new System.Drawing.Size(411, 34);
            this.tx_BanItemName.StyleController = this.layoutControl1;
            this.tx_BanItemName.TabIndex = 3;
            // 
            // tx_BanAvailableQty
            // 
            this.tx_BanAvailableQty.EditValue = "";
            this.tx_BanAvailableQty.Location = new System.Drawing.Point(210, 136);
            this.tx_BanAvailableQty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tx_BanAvailableQty.Name = "tx_BanAvailableQty";
            this.tx_BanAvailableQty.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tx_BanAvailableQty.Properties.Appearance.Options.UseFont = true;
            this.tx_BanAvailableQty.Properties.Appearance.Options.UseTextOptions = true;
            this.tx_BanAvailableQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tx_BanAvailableQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tx_BanAvailableQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.tx_BanAvailableQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.tx_BanAvailableQty.Properties.NullText = "0";
            this.tx_BanAvailableQty.Properties.ReadOnly = true;
            this.tx_BanAvailableQty.Size = new System.Drawing.Size(411, 34);
            this.tx_BanAvailableQty.StyleController = this.layoutControl1;
            this.tx_BanAvailableQty.TabIndex = 4;
            // 
            // btn_Start
            // 
            this.btn_Start.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start.Appearance.Options.UseFont = true;
            this.btn_Start.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Start.ImageOptions.Image")));
            this.btn_Start.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Start.Location = new System.Drawing.Point(16, 179);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(291, 66);
            this.btn_Start.StyleController = this.layoutControl1;
            this.btn_Start.TabIndex = 5;
            this.btn_Start.Text = "시작";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.ImageOptions.Image")));
            this.btn_Cancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel.Location = new System.Drawing.Point(316, 179);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(305, 66);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "취소";
            // 
            // lup_MachineGroup
            // 
            this.lup_MachineGroup.Constraint = null;
            this.lup_MachineGroup.DataSource = null;
            this.lup_MachineGroup.DisplayMember = "";
            this.lup_MachineGroup.isImeModeDisable = false;
            this.lup_MachineGroup.isRequired = false;
            this.lup_MachineGroup.Location = new System.Drawing.Point(101, 16);
            this.lup_MachineGroup.Name = "lup_MachineGroup";
            this.lup_MachineGroup.NullText = "";
            this.lup_MachineGroup.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_MachineGroup.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lup_MachineGroup.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_MachineGroup.Properties.Appearance.Options.UseFont = true;
            this.lup_MachineGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MachineGroup.Properties.NullText = "";
            this.lup_MachineGroup.Properties.PopupView = this.gridView1;
            this.lup_MachineGroup.Size = new System.Drawing.Size(134, 34);
            this.lup_MachineGroup.StyleController = this.layoutControl1;
            this.lup_MachineGroup.TabIndex = 0;
            this.lup_MachineGroup.Value_1 = null;
            this.lup_MachineGroup.ValueMember = "";
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
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.lcBanProductLotNo,
            this.lcBanItemName,
            this.lcBanAvailableQty,
            this.simpleSeparator1,
            this.simpleSeparator2,
            this.lcMachine,
            this.lcMachineGroup});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(637, 261);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_Start;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 163);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(94, 46);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(297, 72);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_Cancel;
            this.layoutControlItem5.Location = new System.Drawing.Point(300, 163);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(94, 46);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(311, 72);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // lcBanProductLotNo
            // 
            this.lcBanProductLotNo.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lcBanProductLotNo.AppearanceItemCaption.Options.UseFont = true;
            this.lcBanProductLotNo.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcBanProductLotNo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcBanProductLotNo.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcBanProductLotNo.Control = this.tx_BanOutLotNo;
            this.lcBanProductLotNo.Location = new System.Drawing.Point(0, 40);
            this.lcBanProductLotNo.Name = "lcBanProductLotNo";
            this.lcBanProductLotNo.Size = new System.Drawing.Size(611, 40);
            this.lcBanProductLotNo.Text = "반제품 생산 LOT NO";
            this.lcBanProductLotNo.TextSize = new System.Drawing.Size(191, 28);
            // 
            // lcBanItemName
            // 
            this.lcBanItemName.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lcBanItemName.AppearanceItemCaption.Options.UseFont = true;
            this.lcBanItemName.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcBanItemName.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcBanItemName.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcBanItemName.Control = this.tx_BanItemName;
            this.lcBanItemName.Location = new System.Drawing.Point(0, 80);
            this.lcBanItemName.Name = "lcBanItemName";
            this.lcBanItemName.Size = new System.Drawing.Size(611, 40);
            this.lcBanItemName.Text = "반제품품명";
            this.lcBanItemName.TextSize = new System.Drawing.Size(191, 28);
            // 
            // lcBanAvailableQty
            // 
            this.lcBanAvailableQty.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lcBanAvailableQty.AppearanceItemCaption.Options.UseFont = true;
            this.lcBanAvailableQty.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcBanAvailableQty.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcBanAvailableQty.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcBanAvailableQty.Control = this.tx_BanAvailableQty;
            this.lcBanAvailableQty.Location = new System.Drawing.Point(0, 120);
            this.lcBanAvailableQty.Name = "lcBanAvailableQty";
            this.lcBanAvailableQty.Size = new System.Drawing.Size(611, 40);
            this.lcBanAvailableQty.Text = "가용 가능량";
            this.lcBanAvailableQty.TextSize = new System.Drawing.Size(191, 28);
            this.lcBanAvailableQty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 160);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(611, 3);
            // 
            // simpleSeparator2
            // 
            this.simpleSeparator2.AllowHotTrack = false;
            this.simpleSeparator2.Location = new System.Drawing.Point(297, 163);
            this.simpleSeparator2.Name = "simpleSeparator2";
            this.simpleSeparator2.Size = new System.Drawing.Size(3, 72);
            // 
            // lcMachine
            // 
            this.lcMachine.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcMachine.AppearanceItemCaption.Options.UseFont = true;
            this.lcMachine.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcMachine.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcMachine.Control = this.lup_Machine;
            this.lcMachine.Location = new System.Drawing.Point(225, 0);
            this.lcMachine.Name = "lcMachine";
            this.lcMachine.Size = new System.Drawing.Size(386, 40);
            this.lcMachine.Text = "설비";
            this.lcMachine.TextSize = new System.Drawing.Size(191, 28);
            // 
            // lcMachineGroup
            // 
            this.lcMachineGroup.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lcMachineGroup.AppearanceItemCaption.Options.UseFont = true;
            this.lcMachineGroup.Control = this.lup_MachineGroup;
            this.lcMachineGroup.CustomizationFormText = "설비";
            this.lcMachineGroup.Location = new System.Drawing.Point(0, 0);
            this.lcMachineGroup.Name = "lcMachineGroup";
            this.lcMachineGroup.Size = new System.Drawing.Size(225, 40);
            this.lcMachineGroup.Text = "설비그룹";
            this.lcMachineGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcMachineGroup.TextSize = new System.Drawing.Size(80, 28);
            this.lcMachineGroup.TextToControlDistance = 5;
            this.lcMachineGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // XPFBANIN_START
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 334);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.MinimumSize = new System.Drawing.Size(655, 381);
            this.Name = "XPFBANIN_START";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "작업시작";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Machine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_BanOutLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_BanItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_BanAvailableQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MachineGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBanProductLotNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBanItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBanAvailableQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMachineGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit tx_BanOutLotNo;
        private DevExpress.XtraEditors.TextEdit tx_BanItemName;
        private DevExpress.XtraEditors.TextEdit tx_BanAvailableQty;
        private DevExpress.XtraEditors.SimpleButton btn_Start;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraLayout.LayoutControlItem lcBanProductLotNo;
        private DevExpress.XtraLayout.LayoutControlItem lcBanItemName;
        private DevExpress.XtraLayout.LayoutControlItem lcBanAvailableQty;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private Service.Controls.SearchLookUpEditEx lup_Machine;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcMachine;
        private Service.Controls.SearchLookUpEditEx lup_MachineGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcMachineGroup;
    }
}