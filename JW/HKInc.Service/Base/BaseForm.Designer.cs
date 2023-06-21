namespace HKInc.Service.Base
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.barManage = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.barButtonRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonExport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStaticItemName = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticMessage = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barButtonConfirm = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManage
            // 
            this.barManage.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools,
            this.bar3});
            this.barManage.DockControls.Add(this.barDockControlTop);
            this.barManage.DockControls.Add(this.barDockControlBottom);
            this.barManage.DockControls.Add(this.barDockControlLeft);
            this.barManage.DockControls.Add(this.barDockControlRight);
            this.barManage.DockManager = this.dockManager;
            this.barManage.Form = this;
            this.barManage.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonRefresh,
            this.barButtonSave,
            this.barButtonExport,
            this.barButtonPrint,
            this.barButtonClose,
            this.barStaticItemName,
            this.barStaticMessage,
            this.barButtonConfirm});
            this.barManage.MaxItemId = 8;
            this.barManage.StatusBar = this.bar3;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonConfirm),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonClose)});
            this.barTools.OptionsBar.AllowQuickCustomization = false;
            this.barTools.OptionsBar.DisableCustomization = true;
            this.barTools.OptionsBar.DrawDragBorder = false;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // barButtonRefresh
            // 
            this.barButtonRefresh.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonRefresh.Caption = "Refresh";
            this.barButtonRefresh.Id = 0;
            this.barButtonRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonRefresh.ImageOptions.Image")));
            this.barButtonRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonRefresh.ImageOptions.LargeImage")));
            this.barButtonRefresh.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F1);
            this.barButtonRefresh.Name = "barButtonRefresh";
            this.barButtonRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonRefresh.ShortcutKeyDisplayString = "F1";
            this.barButtonRefresh.Tag = ((short)(5000));
            // 
            // barButtonSave
            // 
            this.barButtonSave.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonSave.Caption = "Save";
            this.barButtonSave.Id = 1;
            this.barButtonSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonSave.ImageOptions.Image")));
            this.barButtonSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonSave.ImageOptions.LargeImage")));
            this.barButtonSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.barButtonSave.Name = "barButtonSave";
            this.barButtonSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonSave.ShortcutKeyDisplayString = "F5";
            this.barButtonSave.Tag = ((short)(6000));
            // 
            // barButtonExport
            // 
            this.barButtonExport.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonExport.Caption = "Export";
            this.barButtonExport.Id = 2;
            this.barButtonExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonExport.ImageOptions.Image")));
            this.barButtonExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonExport.ImageOptions.LargeImage")));
            this.barButtonExport.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.barButtonExport.Name = "barButtonExport";
            this.barButtonExport.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonExport.ShortcutKeyDisplayString = "F9";
            this.barButtonExport.Tag = ((short)(7000));
            // 
            // barButtonPrint
            // 
            this.barButtonPrint.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonPrint.Caption = "Print";
            this.barButtonPrint.Id = 3;
            this.barButtonPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonPrint.ImageOptions.Image")));
            this.barButtonPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonPrint.ImageOptions.LargeImage")));
            this.barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11);
            this.barButtonPrint.Name = "barButtonPrint";
            this.barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonPrint.ShortcutKeyDisplayString = "F11";
            this.barButtonPrint.Tag = ((short)(8000));
            // 
            // barButtonClose
            // 
            this.barButtonClose.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonClose.Caption = "Close";
            this.barButtonClose.Id = 4;
            this.barButtonClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonClose.ImageOptions.Image")));
            this.barButtonClose.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonClose.ImageOptions.LargeImage")));
            this.barButtonClose.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12);
            this.barButtonClose.Name = "barButtonClose";
            this.barButtonClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonClose.ShortcutKeyDisplayString = "F12";
            this.barButtonClose.Tag = ((short)(9000));
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemName),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticMessage)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barStaticItemName
            // 
            this.barStaticItemName.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barStaticItemName.Caption = "Name";
            this.barStaticItemName.Id = 5;
            this.barStaticItemName.Name = "barStaticItemName";
            // 
            // barStaticMessage
            // 
            this.barStaticMessage.Id = 6;
            this.barStaticMessage.Name = "barStaticMessage";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManage;
            this.barDockControlTop.Size = new System.Drawing.Size(924, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 483);
            this.barDockControlBottom.Manager = this.barManage;
            this.barDockControlBottom.Size = new System.Drawing.Size(924, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManage;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 452);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(924, 31);
            this.barDockControlRight.Manager = this.barManage;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 452);
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.MenuManager = this.barManage;
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // barButtonConfirm
            // 
            this.barButtonConfirm.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonConfirm.Caption = "Confirm";
            this.barButtonConfirm.Id = 7;
            this.barButtonConfirm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonConfirm.ImageOptions.Image")));
            this.barButtonConfirm.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonConfirm.ImageOptions.LargeImage")));
            this.barButtonConfirm.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
            this.barButtonConfirm.Name = "barButtonConfirm";
            this.barButtonConfirm.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonConfirm.ShortcutKeyDisplayString = "F4";
            this.barButtonConfirm.Tag = ((short)(9998));
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 508);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.barManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManage;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonRefresh;
        private DevExpress.XtraBars.BarButtonItem barButtonSave;
        private DevExpress.XtraBars.BarButtonItem barButtonExport;
        private DevExpress.XtraBars.BarButtonItem barButtonPrint;
        private DevExpress.XtraBars.BarButtonItem barButtonClose;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.BarStaticItem barStaticItemName;
        private DevExpress.XtraBars.BarStaticItem barStaticMessage;
        private DevExpress.XtraBars.BarButtonItem barButtonConfirm;
    }
}