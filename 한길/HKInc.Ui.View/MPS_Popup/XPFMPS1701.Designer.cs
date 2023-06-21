namespace HKInc.Ui.View.MPS_Popup
{
    partial class XPFMPS1701
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFMPS1701));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.dateInDate = new HKInc.Service.Controls.DateEditEx();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lupInId = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lupWhCode = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEditEx2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lupWhPosition = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEditEx3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateInDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateInDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupInId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupWhCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupWhPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lupWhPosition);
            this.layoutControl1.Controls.Add(this.lupWhCode);
            this.layoutControl1.Controls.Add(this.lupInId);
            this.layoutControl1.Controls.Add(this.dateInDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(309, 159);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(309, 159);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // dateInDate
            // 
            this.dateInDate.EditValue = null;
            this.dateInDate.Location = new System.Drawing.Point(59, 16);
            this.dateInDate.Name = "dateInDate";
            this.dateInDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateInDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateInDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateInDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateInDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateInDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateInDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateInDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateInDate.Size = new System.Drawing.Size(234, 24);
            this.dateInDate.StyleController = this.layoutControl1;
            this.dateInDate.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateInDate;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(283, 30);
            this.layoutControlItem2.Text = "입고일";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(39, 18);
            // 
            // lupInId
            // 
            this.lupInId.Constraint = null;
            this.lupInId.DataSource = null;
            this.lupInId.DisplayMember = "";
            this.lupInId.isRequired = false;
            this.lupInId.Location = new System.Drawing.Point(59, 46);
            this.lupInId.Name = "lupInId";
            this.lupInId.NullText = "";
            this.lupInId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupInId.Properties.NullText = "";
            this.lupInId.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lupInId.Size = new System.Drawing.Size(234, 24);
            this.lupInId.StyleController = this.layoutControl1;
            this.lupInId.TabIndex = 6;
            this.lupInId.Value_1 = null;
            this.lupInId.ValueMember = "";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lupInId;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(283, 30);
            this.layoutControlItem3.Text = "입고자";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(39, 18);
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // lupWhCode
            // 
            this.lupWhCode.Constraint = null;
            this.lupWhCode.DataSource = null;
            this.lupWhCode.DisplayMember = "";
            this.lupWhCode.isRequired = false;
            this.lupWhCode.Location = new System.Drawing.Point(59, 76);
            this.lupWhCode.Name = "lupWhCode";
            this.lupWhCode.NullText = "";
            this.lupWhCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupWhCode.Properties.NullText = "";
            this.lupWhCode.Properties.PopupView = this.searchLookUpEditEx2View;
            this.lupWhCode.Size = new System.Drawing.Size(234, 24);
            this.lupWhCode.StyleController = this.layoutControl1;
            this.lupWhCode.TabIndex = 7;
            this.lupWhCode.Value_1 = null;
            this.lupWhCode.ValueMember = "";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lupWhCode;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(283, 30);
            this.layoutControlItem4.Text = "창고";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(39, 18);
            // 
            // searchLookUpEditEx2View
            // 
            this.searchLookUpEditEx2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx2View.Name = "searchLookUpEditEx2View";
            this.searchLookUpEditEx2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx2View.OptionsView.ShowGroupPanel = false;
            // 
            // lupWhPosition
            // 
            this.lupWhPosition.Constraint = null;
            this.lupWhPosition.DataSource = null;
            this.lupWhPosition.DisplayMember = "";
            this.lupWhPosition.isRequired = false;
            this.lupWhPosition.Location = new System.Drawing.Point(59, 106);
            this.lupWhPosition.Name = "lupWhPosition";
            this.lupWhPosition.NullText = "";
            this.lupWhPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupWhPosition.Properties.NullText = "";
            this.lupWhPosition.Properties.PopupView = this.searchLookUpEditEx3View;
            this.lupWhPosition.Size = new System.Drawing.Size(234, 24);
            this.lupWhPosition.StyleController = this.layoutControl1;
            this.lupWhPosition.TabIndex = 8;
            this.lupWhPosition.Value_1 = null;
            this.lupWhPosition.ValueMember = "";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lupWhPosition;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(283, 30);
            this.layoutControlItem5.Text = "위치";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(39, 18);
            // 
            // searchLookUpEditEx3View
            // 
            this.searchLookUpEditEx3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx3View.Name = "searchLookUpEditEx3View";
            this.searchLookUpEditEx3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx3View.OptionsView.ShowGroupPanel = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 120);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(283, 13);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XPFMPS1701
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 232);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFMPS1701";
            this.Text = "일괄 입고 정보";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateInDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateInDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupInId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupWhCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupWhPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.SearchLookUpEditEx lupWhPosition;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx3View;
        private Service.Controls.SearchLookUpEditEx lupWhCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx2View;
        private Service.Controls.SearchLookUpEditEx lupInId;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private Service.Controls.DateEditEx dateInDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}