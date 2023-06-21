namespace HKInc.Ui.View.View.MPS_POPUP
{
    partial class XPFMPS1200_BAR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFMPS1200_BAR));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.spin_PrintQty = new DevExpress.XtraEditors.SpinEdit();
            this.spin_PerBoxQty = new DevExpress.XtraEditors.SpinEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPrintInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPrintQty3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPerBoxQty = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_PrintQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_PerBoxQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintQty3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPerBoxQty)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spin_PrintQty);
            this.layoutControl1.Controls.Add(this.spin_PerBoxQty);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(653, 116);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // spin_PrintQty
            // 
            this.spin_PrintQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_PrintQty.Location = new System.Drawing.Point(106, 50);
            this.spin_PrintQty.Name = "spin_PrintQty";
            this.spin_PrintQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_PrintQty.Size = new System.Drawing.Size(218, 24);
            this.spin_PrintQty.StyleController = this.layoutControl1;
            this.spin_PrintQty.TabIndex = 5;
            // 
            // spin_PerBoxQty
            // 
            this.spin_PerBoxQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_PerBoxQty.Location = new System.Drawing.Point(410, 50);
            this.spin_PerBoxQty.Name = "spin_PerBoxQty";
            this.spin_PerBoxQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_PerBoxQty.Size = new System.Drawing.Size(219, 24);
            this.spin_PerBoxQty.StyleController = this.layoutControl1;
            this.spin_PerBoxQty.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPrintInfo});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(653, 116);
            this.Root.TextVisible = false;
            // 
            // lcPrintInfo
            // 
            this.lcPrintInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPrintQty3,
            this.lcPerBoxQty});
            this.lcPrintInfo.Location = new System.Drawing.Point(0, 0);
            this.lcPrintInfo.Name = "lcPrintInfo";
            this.lcPrintInfo.Size = new System.Drawing.Size(633, 96);
            // 
            // lcPrintQty3
            // 
            this.lcPrintQty3.Control = this.spin_PrintQty;
            this.lcPrintQty3.Location = new System.Drawing.Point(0, 0);
            this.lcPrintQty3.Name = "lcPrintQty3";
            this.lcPrintQty3.Size = new System.Drawing.Size(304, 46);
            this.lcPrintQty3.TextSize = new System.Drawing.Size(79, 18);
            // 
            // lcPerBoxQty
            // 
            this.lcPerBoxQty.Control = this.spin_PerBoxQty;
            this.lcPerBoxQty.Location = new System.Drawing.Point(304, 0);
            this.lcPerBoxQty.Name = "lcPerBoxQty";
            this.lcPerBoxQty.Size = new System.Drawing.Size(305, 46);
            this.lcPerBoxQty.TextSize = new System.Drawing.Size(79, 18);
            // 
            // XPFMPS1200_BAR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 175);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XPFMPS1200_BAR";
            this.Text = "XPFMPS1200_BAR";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spin_PrintQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_PerBoxQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrintQty3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPerBoxQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SpinEdit spin_PrintQty;
        private DevExpress.XtraEditors.SpinEdit spin_PerBoxQty;
        private DevExpress.XtraLayout.LayoutControlGroup lcPrintInfo;
        private DevExpress.XtraLayout.LayoutControlItem lcPrintQty3;
        private DevExpress.XtraLayout.LayoutControlItem lcPerBoxQty;
    }
}