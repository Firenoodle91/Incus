namespace HKInc.Ui.View.SELECT_POPUP
{
    partial class XSFQCT_CHECK_DIVISION_COPY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSFQCT_CHECK_DIVISION_COPY));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lup_InspectionDivision = new HKInc.Service.Controls.SearchLookUpEditEx();
            this.searchLookUpEditEx1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcChangingCheckDivision = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcInspectionDivision = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_InspectionDivision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcChangingCheckDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInspectionDivision)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lup_InspectionDivision);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(315, 115);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lup_InspectionDivision
            // 
            this.lup_InspectionDivision.Constraint = null;
            this.lup_InspectionDivision.DataSource = null;
            this.lup_InspectionDivision.DisplayMember = "";
            this.lup_InspectionDivision.isImeModeDisable = false;
            this.lup_InspectionDivision.isRequired = true;
            this.lup_InspectionDivision.Location = new System.Drawing.Point(31, 56);
            this.lup_InspectionDivision.Name = "lup_InspectionDivision";
            this.lup_InspectionDivision.NullText = "";
            this.lup_InspectionDivision.Properties.Appearance.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.lup_InspectionDivision.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lup_InspectionDivision.Properties.Appearance.Options.UseBackColor = true;
            this.lup_InspectionDivision.Properties.Appearance.Options.UseForeColor = true;
            this.lup_InspectionDivision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_InspectionDivision.Properties.NullText = "";
            this.lup_InspectionDivision.Properties.PopupView = this.searchLookUpEditEx1View;
            this.lup_InspectionDivision.Size = new System.Drawing.Size(253, 24);
            this.lup_InspectionDivision.StyleController = this.layoutControl1;
            this.lup_InspectionDivision.TabIndex = 0;
            this.lup_InspectionDivision.Value_1 = null;
            this.lup_InspectionDivision.ValueMember = "";
            // 
            // searchLookUpEditEx1View
            // 
            this.searchLookUpEditEx1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditEx1View.Name = "searchLookUpEditEx1View";
            this.searchLookUpEditEx1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditEx1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcChangingCheckDivision});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(315, 115);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcChangingCheckDivision
            // 
            this.lcChangingCheckDivision.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcInspectionDivision});
            this.lcChangingCheckDivision.Location = new System.Drawing.Point(0, 0);
            this.lcChangingCheckDivision.Name = "lcChangingCheckDivision";
            this.lcChangingCheckDivision.OptionsItemText.TextToControlDistance = 4;
            this.lcChangingCheckDivision.Size = new System.Drawing.Size(289, 89);
            this.lcChangingCheckDivision.Text = "lcChangingInspectionDivision";
            // 
            // lcInspectionDivision
            // 
            this.lcInspectionDivision.Control = this.lup_InspectionDivision;
            this.lcInspectionDivision.Location = new System.Drawing.Point(0, 0);
            this.lcInspectionDivision.Name = "lcInspectionDivision";
            this.lcInspectionDivision.Size = new System.Drawing.Size(259, 34);
            this.lcInspectionDivision.Text = "검사구분";
            this.lcInspectionDivision.TextSize = new System.Drawing.Size(0, 0);
            this.lcInspectionDivision.TextVisible = false;
            // 
            // XSFQCT_CHECK_DIVISION_COPY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 188);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "XSFQCT_CHECK_DIVISION_COPY";
            this.Text = "XSFQCT_CHECK_DIVISION_COPY";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lup_InspectionDivision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEx1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcChangingCheckDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInspectionDivision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.SearchLookUpEditEx lup_InspectionDivision;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditEx1View;
        private DevExpress.XtraLayout.LayoutControlItem lcInspectionDivision;
        private DevExpress.XtraLayout.LayoutControlGroup lcChangingCheckDivision;
    }
}