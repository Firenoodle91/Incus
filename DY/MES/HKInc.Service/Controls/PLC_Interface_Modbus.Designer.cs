namespace HKInc.Service.Controls
{
    partial class PLC_Interface_Modbus
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tx_TimerPeriod = new DevExpress.XtraEditors.TextEdit();
            this.tx_PLC_Count = new DevExpress.XtraEditors.TextEdit();
            this.tx_DB_Period = new DevExpress.XtraEditors.TextEdit();
            this.tx_Status = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tx_TimerPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_PLC_Count.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_DB_Period.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Status.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(369, 164);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Modbus 통신";
            this.groupControl1.DoubleClick += new System.EventHandler(this.groupControl1_DoubleClick);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tx_TimerPeriod);
            this.layoutControl1.Controls.Add(this.tx_PLC_Count);
            this.layoutControl1.Controls.Add(this.tx_DB_Period);
            this.layoutControl1.Controls.Add(this.tx_Status);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 28);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(365, 134);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tx_TimerPeriod
            // 
            this.tx_TimerPeriod.Location = new System.Drawing.Point(181, 12);
            this.tx_TimerPeriod.Name = "tx_TimerPeriod";
            this.tx_TimerPeriod.Properties.ReadOnly = true;
            this.tx_TimerPeriod.Size = new System.Drawing.Size(172, 24);
            this.tx_TimerPeriod.StyleController = this.layoutControl1;
            this.tx_TimerPeriod.TabIndex = 4;
            // 
            // tx_PLC_Count
            // 
            this.tx_PLC_Count.Location = new System.Drawing.Point(181, 40);
            this.tx_PLC_Count.Name = "tx_PLC_Count";
            this.tx_PLC_Count.Properties.ReadOnly = true;
            this.tx_PLC_Count.Size = new System.Drawing.Size(172, 24);
            this.tx_PLC_Count.StyleController = this.layoutControl1;
            this.tx_PLC_Count.TabIndex = 5;
            // 
            // tx_DB_Period
            // 
            this.tx_DB_Period.Location = new System.Drawing.Point(181, 68);
            this.tx_DB_Period.Name = "tx_DB_Period";
            this.tx_DB_Period.Properties.ReadOnly = true;
            this.tx_DB_Period.Size = new System.Drawing.Size(172, 24);
            this.tx_DB_Period.StyleController = this.layoutControl1;
            this.tx_DB_Period.TabIndex = 6;
            // 
            // tx_Status
            // 
            this.tx_Status.Enabled = false;
            this.tx_Status.Location = new System.Drawing.Point(12, 96);
            this.tx_Status.Name = "tx_Status";
            this.tx_Status.Size = new System.Drawing.Size(341, 24);
            this.tx_Status.StyleController = this.layoutControl1;
            this.tx_Status.TabIndex = 7;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(365, 134);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tx_TimerPeriod;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(345, 28);
            this.layoutControlItem1.Text = "실적 통신 시간";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(166, 18);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tx_PLC_Count;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(345, 28);
            this.layoutControlItem2.Text = "실적 카운트";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(166, 18);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tx_DB_Period;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(345, 28);
            this.layoutControlItem3.Text = "데이터베이스 업데이트 주기";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(166, 18);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tx_Status;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 84);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(345, 30);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // PLC_Interface_Modbus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "PLC_Interface_Modbus";
            this.Size = new System.Drawing.Size(369, 164);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tx_TimerPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_PLC_Count.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_DB_Period.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_Status.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit tx_TimerPeriod;
        private DevExpress.XtraEditors.TextEdit tx_PLC_Count;
        private DevExpress.XtraEditors.TextEdit tx_DB_Period;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit tx_Status;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}
