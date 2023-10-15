namespace HKInc.Service.Controls
{
	partial class DatePeriodEditEx
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
            this.lbl = new DevExpress.XtraEditors.LabelControl();
            this.datDateTo = new HKInc.Service.Controls.DateEditEx();
            this.datDateFr = new HKInc.Service.Controls.DateEditEx();
            ((System.ComponentModel.ISupportInitialize)(this.datDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDateFr.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDateFr.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbl.Appearance.Options.UseForeColor = true;
            this.lbl.Location = new System.Drawing.Point(105, 6);
            this.lbl.Margin = new System.Windows.Forms.Padding(0);
            this.lbl.MaximumSize = new System.Drawing.Size(23, 26);
            this.lbl.MinimumSize = new System.Drawing.Size(23, 26);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(23, 26);
            this.lbl.TabIndex = 3;
            this.lbl.Text = " ~";
            // 
            // datDateTo
            // 
            this.datDateTo.EditValue = "2014-02-20";
            this.datDateTo.Location = new System.Drawing.Point(126, 1);
            this.datDateTo.Margin = new System.Windows.Forms.Padding(0);
            this.datDateTo.Name = "datDateTo";
            this.datDateTo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.datDateTo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datDateTo.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.datDateTo.Properties.Appearance.Options.UseBackColor = true;
            this.datDateTo.Properties.Appearance.Options.UseFont = true;
            this.datDateTo.Properties.Appearance.Options.UseForeColor = true;
            this.datDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDateTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.datDateTo.Size = new System.Drawing.Size(103, 24);
            this.datDateTo.TabIndex = 2;
            // 
            // datDateFr
            // 
            this.datDateFr.EditValue = "2014-02-20";
            this.datDateFr.Location = new System.Drawing.Point(1, 1);
            this.datDateFr.Margin = new System.Windows.Forms.Padding(0);
            this.datDateFr.Name = "datDateFr";
            this.datDateFr.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.datDateFr.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datDateFr.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.datDateFr.Properties.Appearance.Options.UseBackColor = true;
            this.datDateFr.Properties.Appearance.Options.UseFont = true;
            this.datDateFr.Properties.Appearance.Options.UseForeColor = true;
            this.datDateFr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDateFr.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDateFr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.datDateFr.Size = new System.Drawing.Size(103, 24);
            this.datDateFr.TabIndex = 0;
            // 
            // DatePeriodEditEx
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.datDateTo);
            this.Controls.Add(this.datDateFr);
            this.Controls.Add(this.lbl);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(200, 20);
            this.Name = "DatePeriodEditEx";
            this.Size = new System.Drawing.Size(233, 26);
            ((System.ComponentModel.ISupportInitialize)(this.datDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDateFr.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDateFr.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private DateEditEx datDateFr;
		private DateEditEx datDateTo;
        private DevExpress.XtraEditors.LabelControl lbl;
	}
}
