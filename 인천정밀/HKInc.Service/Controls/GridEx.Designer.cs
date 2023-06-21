namespace HKInc.Service.Controls
{
    partial class GridEx
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridEx));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.barButtonAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonExport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonFileChoose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridLink = new HKInc.Service.Controls.GridControlEx();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonAdd,
            this.barButtonDelete,
            this.barButtonExport,
            this.barButtonFileChoose});
            this.barManager.MaxItemId = 4;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonFileChoose)});
            this.barTools.OptionsBar.AllowQuickCustomization = false;
            this.barTools.OptionsBar.DisableCustomization = true;
            this.barTools.OptionsBar.DrawDragBorder = false;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // barButtonAdd
            // 
            this.barButtonAdd.Id = 0;
            this.barButtonAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.Image")));
            this.barButtonAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            this.barButtonAdd.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
            this.barButtonAdd.Name = "barButtonAdd";
            this.barButtonAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonAdd.ShortcutKeyDisplayString = "F3";
            this.barButtonAdd.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            // 
            // barButtonDelete
            // 
            this.barButtonDelete.Id = 1;
            this.barButtonDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonDelete.ImageOptions.Image")));
            this.barButtonDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonDelete.ImageOptions.LargeImage")));
            this.barButtonDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.barButtonDelete.Name = "barButtonDelete";
            this.barButtonDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonDelete.ShortcutKeyDisplayString = "F7";
            this.barButtonDelete.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            // 
            // barButtonExport
            // 
            this.barButtonExport.Id = 2;
            this.barButtonExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonExport.ImageOptions.Image")));
            this.barButtonExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonExport.ImageOptions.LargeImage")));
            this.barButtonExport.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8);
            this.barButtonExport.Name = "barButtonExport";
            this.barButtonExport.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonExport.ShortcutKeyDisplayString = "F8";
            // 
            // barButtonFileChoose
            // 
            this.barButtonFileChoose.Id = 3;
            this.barButtonFileChoose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonFileChoose.ImageOptions.Image")));
            this.barButtonFileChoose.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonFileChoose.ImageOptions.LargeImage")));
            this.barButtonFileChoose.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F10);
            this.barButtonFileChoose.Name = "barButtonFileChoose";
            this.barButtonFileChoose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonFileChoose.ShortcutKeyDisplayString = "F10";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(661, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 479);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(661, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 448);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(661, 31);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 448);
            // 
            // gridLink
            // 
            this.gridLink.ColumnAutoWidth = false;
            this.gridLink.DataSource = null;
            this.gridLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLink.Editable = false;
            this.gridLink.EnableAppearanceEvenRow = true;
            this.gridLink.EnableAppearanceFocusedCell = false;
            this.gridLink.EnableAppearanceFocusedRow = true;
            this.gridLink.EnableAppearanceOddRow = true;
            this.gridLink.FocusedColumn = null;
            this.gridLink.FocusedRowHandle = -2147483648;
            this.gridLink.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLink.Location = new System.Drawing.Point(0, 31);
            this.gridLink.MultiSelect = true;
            this.gridLink.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridLink.Name = "gridLink";
            this.gridLink.ReadOnly = true;
            this.gridLink.ShowColumnHeaders = true;
            this.gridLink.ShowDetailButtons = false;
            this.gridLink.ShowFooter = false;
            this.gridLink.ShowGroupPanel = false;
            this.gridLink.Size = new System.Drawing.Size(661, 448);
            this.gridLink.TabIndex = 4;
            this.gridLink.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // GridEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridLink);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "GridEx";
            this.Size = new System.Drawing.Size(661, 479);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.BarButtonItem barButtonAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonExport;
        private DevExpress.XtraBars.BarButtonItem barButtonFileChoose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private GridControlEx gridLink;
    }
}
