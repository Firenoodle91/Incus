namespace HKInc.Ui.View.ORD_Popup
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
            this.lupempid = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.searchLookUpEditEx2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lupcustcode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dateEditEx2 = new HKInc.Service.Controls.DateEditEx();
            this.dateEditEx1 = new HKInc.Service.Controls.DateEditEx();
            this.tx_orderno = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupempid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupcustcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_orderno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lupempid);
            this.layoutControl1.Controls.Add(this.lupcustcode);
            this.layoutControl1.Controls.Add(this.dateEditEx2);
            this.layoutControl1.Controls.Add(this.dateEditEx1);
            this.layoutControl1.Controls.Add(this.tx_orderno);
            this.layoutControl1.Controls.Add(this.textEdit2);
            this.layoutControl1.Controls.Add(this.memoEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(427, 342);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lupempid
            // 
            this.lupempid.Constraint = null;
            this.lupempid.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "OrderId", true));
            this.lupempid.DataSource = null;
            this.lupempid.DisplayMember = "";
            this.lupempid.isImeModeDisable = false;
            this.lupempid.isRequired = false;
            this.lupempid.Location = new System.Drawing.Point(98, 166);
            this.lupempid.Name = "lupempid";
            this.lupempid.NullText = "";
            this.lupempid.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lupempid.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lupempid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupempid.Properties.NullText = "";
            this.lupempid.Properties.PopupView = this.searchLookUpEditEx2View;
            this.lupempid.Size = new System.Drawing.Size(313, 24);
            this.lupempid.StyleController = this.layoutControl1;
            this.lupempid.TabIndex = 5;
            this.lupempid.Value_1 = null;
            this.lupempid.ValueMember = "";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(HKInc.Ui.Model.Domain.TN_ORD1000);
            // 
            // searchLookUpEditEx2View
            // 
            this.searchLookUpEditEx2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx2View.Name = "searchLookUpEditEx2View";
            this.searchLookUpEditEx2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx2View.OptionsView.ShowGroupPanel = false;
            // 
            // lupcustcode
            // 
            this.lupcustcode.Constraint = null;
            this.lupcustcode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CustomerCode", true));
            this.lupcustcode.DataSource = null;
            this.lupcustcode.DisplayMember = "";
            this.lupcustcode.isImeModeDisable = false;
            this.lupcustcode.isRequired = false;
            this.lupcustcode.Location = new System.Drawing.Point(98, 76);
            this.lupcustcode.Name = "lupcustcode";
            this.lupcustcode.NullText = "";
            this.lupcustcode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lupcustcode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lupcustcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupcustcode.Properties.NullText = "";
            this.lupcustcode.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lupcustcode.Size = new System.Drawing.Size(313, 24);
            this.lupcustcode.StyleController = this.layoutControl1;
            this.lupcustcode.TabIndex = 2;
            this.lupcustcode.Value_1 = null;
            this.lupcustcode.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // dateEditEx2
            // 
            this.dateEditEx2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PeriodDate", true));
            this.dateEditEx2.EditValue = null;
            this.dateEditEx2.Location = new System.Drawing.Point(98, 106);
            this.dateEditEx2.Name = "dateEditEx2";
            this.dateEditEx2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEx2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEx2.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEditEx2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEx2.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEditEx2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEx2.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateEditEx2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditEx2.Size = new System.Drawing.Size(313, 24);
            this.dateEditEx2.StyleController = this.layoutControl1;
            this.dateEditEx2.TabIndex = 3;
            // 
            // dateEditEx1
            // 
            this.dateEditEx1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "OrderDate", true));
            this.dateEditEx1.EditValue = null;
            this.dateEditEx1.Location = new System.Drawing.Point(98, 46);
            this.dateEditEx1.Name = "dateEditEx1";
            this.dateEditEx1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEx1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEx1.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEditEx1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEx1.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEditEx1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEx1.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateEditEx1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditEx1.Size = new System.Drawing.Size(313, 24);
            this.dateEditEx1.StyleController = this.layoutControl1;
            this.dateEditEx1.TabIndex = 1;
            // 
            // tx_orderno
            // 
            this.tx_orderno.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "OrderNo", true));
            this.tx_orderno.Enabled = false;
            this.tx_orderno.Location = new System.Drawing.Point(98, 16);
            this.tx_orderno.Name = "tx_orderno";
            this.tx_orderno.Size = new System.Drawing.Size(313, 24);
            this.tx_orderno.StyleController = this.layoutControl1;
            this.tx_orderno.TabIndex = 0;
            // 
            // textEdit2
            // 
            this.textEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CustOrderid", true));
            this.textEdit2.Location = new System.Drawing.Point(98, 136);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(313, 24);
            this.textEdit2.StyleController = this.layoutControl1;
            this.textEdit2.TabIndex = 4;
            // 
            // memoEdit1
            // 
            this.memoEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Memo", true));
            this.memoEdit1.Location = new System.Drawing.Point(98, 196);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(313, 130);
            this.memoEdit1.StyleController = this.layoutControl1;
            this.memoEdit1.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(427, 342);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tx_orderno;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem1.Text = "수주번호";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(78, 18);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem2.Control = this.dateEditEx1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem2.Text = "수주일";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(78, 18);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem4.Control = this.dateEditEx2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem4.Text = "납기일";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(78, 18);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textEdit2;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem5.Text = "고객사담당자";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(78, 18);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.memoEdit1;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 180);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(401, 136);
            this.layoutControlItem7.Text = "메모";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(78, 18);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.layoutControlItem8.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem8.Control = this.lupcustcode;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem8.Text = "수주처";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(78, 18);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lupempid;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 150);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(401, 30);
            this.layoutControlItem9.Text = "영업담당자";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(78, 18);
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
            ((System.ComponentModel.ISupportInitialize)(this.lupempid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupcustcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEx1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_orderno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.DateEditEx dateEditEx1;
        private DevExpress.XtraEditors.TextEdit tx_orderno;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Service.Controls.DateEditEx dateEditEx2;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private Service.Controls.SearchLookUpEditEx lupempid;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx2View;
        private Service.Controls.SearchLookUpEditEx lupcustcode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}