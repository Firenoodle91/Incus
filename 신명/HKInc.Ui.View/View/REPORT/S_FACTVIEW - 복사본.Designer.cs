namespace HKInc.Ui.View.View.REPORT
{
    partial class S_FACTVIEW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(S_FACTVIEW));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.factview1 = new HKInc.Service.Controls.FACTVIEW();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.factvieW_MAIN1 = new HKInc.Service.Controls.FACTVIEW_MAIN();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PY = new System.Windows.Forms.Label();
            this.PX = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDesign = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDesign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.factview1);
            this.layoutControl1.Controls.Add(this.flowLayoutPanel1);
            this.layoutControl1.Controls.Add(this.panel2);
            this.layoutControl1.Controls.Add(this.panel1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(936, 518);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // factview1
            // 
            this.factview1.BackColor = System.Drawing.SystemColors.Control;
            this.factview1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.factview1.Item = "";
            this.factview1.ItemCode = "";
            this.factview1.Location = new System.Drawing.Point(653, 113);
            this.factview1.Machine = "";
            this.factview1.MachineCode = "";
            this.factview1.MaximumSize = new System.Drawing.Size(300, 350);
            this.factview1.Name = "factview1";
            this.factview1.NewOrEdit = "";
            this.factview1.Size = new System.Drawing.Size(260, 334);
            this.factview1.TabIndex = 0;
            this.factview1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.factview1_MouseDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.factvieW_MAIN1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(653, 21);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(260, 88);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // factvieW_MAIN1
            // 
            this.factvieW_MAIN1.BackColor = System.Drawing.SystemColors.Control;
            this.factvieW_MAIN1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.factvieW_MAIN1.Item = "";
            this.factvieW_MAIN1.Location = new System.Drawing.Point(3, 3);
            this.factvieW_MAIN1.Machine = "";
            this.factvieW_MAIN1.MaximumSize = new System.Drawing.Size(161, 88);
            this.factvieW_MAIN1.Name = "factvieW_MAIN1";
            this.factvieW_MAIN1.Size = new System.Drawing.Size(159, 74);
            this.factvieW_MAIN1.TabIndex = 0;
            this.factvieW_MAIN1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.factvieW_MAIN1_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.PY);
            this.panel2.Controls.Add(this.PX);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(653, 451);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 45);
            this.panel2.TabIndex = 8;
            // 
            // PY
            // 
            this.PY.AutoSize = true;
            this.PY.Location = new System.Drawing.Point(42, 224);
            this.PY.Name = "PY";
            this.PY.Size = new System.Drawing.Size(38, 14);
            this.PY.TabIndex = 8;
            this.PY.Text = "label3";
            // 
            // PX
            // 
            this.PX.AutoSize = true;
            this.PX.Location = new System.Drawing.Point(42, 169);
            this.PX.Name = "PX";
            this.PX.Size = new System.Drawing.Size(38, 14);
            this.PX.TabIndex = 0;
            this.PX.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(70, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 54);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(24, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 470);
            this.panel1.TabIndex = 7;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            this.panel1.DragOver += new System.Windows.Forms.DragEventHandler(this.panel1_DragOver);
            this.panel1.DragLeave += new System.EventHandler(this.panel1_DragLeave);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcList,
            this.lcDesign});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(936, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcList
            // 
            this.lcList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcList.Location = new System.Drawing.Point(0, 0);
            this.lcList.Name = "lcList";
            this.lcList.OptionsCustomization.AllowDrop = DevExpress.XtraLayout.ItemDragDropMode.Allow;
            this.lcList.OptionsItemText.TextToControlDistance = 4;
            this.lcList.Size = new System.Drawing.Size(608, 498);
            this.lcList.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panel1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(584, 474);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lcDesign
            // 
            this.lcDesign.CustomizationFormText = "도면";
            this.lcDesign.ExpandButtonMode = DevExpress.Utils.Controls.ExpandButtonMode.Inverted;
            this.lcDesign.ExpandButtonVisible = true;
            this.lcDesign.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem4});
            this.lcDesign.Location = new System.Drawing.Point(608, 0);
            this.lcDesign.Name = "lcDesign";
            this.lcDesign.OptionsItemText.TextToControlDistance = 4;
            this.lcDesign.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 7, 7);
            this.lcDesign.Size = new System.Drawing.Size(308, 498);
            this.lcDesign.Text = "도구";
            this.lcDesign.TextLocation = DevExpress.Utils.Locations.Left;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.flowLayoutPanel1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(264, 92);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.factview1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 92);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(264, 338);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.panel2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 430);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(264, 49);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // S_FACTVIEW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "S_FACTVIEW";
            this.Text = "XFWORKDT";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDesign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lcDesign;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraLayout.LayoutControlGroup lcList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PY;
        private System.Windows.Forms.Label PX;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        private Service.Controls.FACTVIEW factview1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Service.Controls.FACTVIEW_MAIN factvieW_MAIN1;
    }
}