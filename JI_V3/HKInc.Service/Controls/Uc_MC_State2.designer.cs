namespace HKInc.Service.Controls
{
    partial class Uc_MC_State2
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_machine = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Prod = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Qual = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lbl_itemName = new DevExpress.XtraEditors.LabelControl();
            this.lbl_prodQty = new DevExpress.XtraEditors.LabelControl();
            this.pnl_state = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 120);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_machine, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Prod, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Qual, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_itemName, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_prodQty, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnl_state, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(280, 120);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_machine
            // 
            this.lbl_machine.Appearance.BackColor = System.Drawing.Color.Black;
            this.lbl_machine.Appearance.Font = new System.Drawing.Font("Digital-7", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_machine.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbl_machine.Appearance.Options.UseBackColor = true;
            this.lbl_machine.Appearance.Options.UseFont = true;
            this.lbl_machine.Appearance.Options.UseForeColor = true;
            this.lbl_machine.Appearance.Options.UseTextOptions = true;
            this.lbl_machine.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_machine.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbl_machine.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_machine, 3);
            this.lbl_machine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_machine.Location = new System.Drawing.Point(0, 0);
            this.lbl_machine.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_machine.Name = "lbl_machine";
            this.lbl_machine.Size = new System.Drawing.Size(138, 23);
            this.lbl_machine.TabIndex = 2;
            this.lbl_machine.Text = "JI-CP-003";
            // 
            // lbl_Prod
            // 
            this.lbl_Prod.Appearance.BackColor = System.Drawing.Color.Black;
            this.lbl_Prod.Appearance.Font = new System.Drawing.Font("DS-Digital", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Prod.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbl_Prod.Appearance.Options.UseBackColor = true;
            this.lbl_Prod.Appearance.Options.UseFont = true;
            this.lbl_Prod.Appearance.Options.UseForeColor = true;
            this.lbl_Prod.Appearance.Options.UseTextOptions = true;
            this.lbl_Prod.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_Prod.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_Prod, 3);
            this.lbl_Prod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Prod.Location = new System.Drawing.Point(0, 83);
            this.lbl_Prod.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Prod.Name = "lbl_Prod";
            this.lbl_Prod.Size = new System.Drawing.Size(138, 37);
            this.lbl_Prod.TabIndex = 0;
            this.lbl_Prod.Text = "생산";
            // 
            // lbl_Qual
            // 
            this.lbl_Qual.Appearance.BackColor = System.Drawing.Color.Black;
            this.lbl_Qual.Appearance.Font = new System.Drawing.Font("DS-Digital", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Qual.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbl_Qual.Appearance.Options.UseBackColor = true;
            this.lbl_Qual.Appearance.Options.UseFont = true;
            this.lbl_Qual.Appearance.Options.UseForeColor = true;
            this.lbl_Qual.Appearance.Options.UseTextOptions = true;
            this.lbl_Qual.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_Qual.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_Qual, 3);
            this.lbl_Qual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Qual.Location = new System.Drawing.Point(138, 83);
            this.lbl_Qual.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Qual.Name = "lbl_Qual";
            this.lbl_Qual.Size = new System.Drawing.Size(142, 37);
            this.lbl_Qual.TabIndex = 0;
            this.lbl_Qual.Text = "품질";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("DS-Digital", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tableLayoutPanel1.SetColumnSpan(this.labelControl1, 2);
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(0, 53);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(92, 30);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "생산수량";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("DS-Digital", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tableLayoutPanel1.SetColumnSpan(this.labelControl2, 2);
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(0, 23);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(92, 30);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "생산품명";
            // 
            // lbl_itemName
            // 
            this.lbl_itemName.Appearance.BackColor = System.Drawing.Color.Black;
            this.lbl_itemName.Appearance.Font = new System.Drawing.Font("DS-Digital", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_itemName.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbl_itemName.Appearance.Options.UseBackColor = true;
            this.lbl_itemName.Appearance.Options.UseFont = true;
            this.lbl_itemName.Appearance.Options.UseForeColor = true;
            this.lbl_itemName.Appearance.Options.UseTextOptions = true;
            this.lbl_itemName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_itemName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_itemName, 4);
            this.lbl_itemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_itemName.Location = new System.Drawing.Point(92, 23);
            this.lbl_itemName.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_itemName.Name = "lbl_itemName";
            this.lbl_itemName.Size = new System.Drawing.Size(188, 30);
            this.lbl_itemName.TabIndex = 1;
            this.lbl_itemName.Text = "68201-372210(PRD28580815A)";
            // 
            // lbl_prodQty
            // 
            this.lbl_prodQty.Appearance.BackColor = System.Drawing.Color.Black;
            this.lbl_prodQty.Appearance.Font = new System.Drawing.Font("DS-Digital", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_prodQty.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbl_prodQty.Appearance.Options.UseBackColor = true;
            this.lbl_prodQty.Appearance.Options.UseFont = true;
            this.lbl_prodQty.Appearance.Options.UseForeColor = true;
            this.lbl_prodQty.Appearance.Options.UseTextOptions = true;
            this.lbl_prodQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_prodQty.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_prodQty, 4);
            this.lbl_prodQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_prodQty.Location = new System.Drawing.Point(92, 53);
            this.lbl_prodQty.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_prodQty.Name = "lbl_prodQty";
            this.lbl_prodQty.Size = new System.Drawing.Size(188, 30);
            this.lbl_prodQty.TabIndex = 1;
            this.lbl_prodQty.Text = "100,000,000,000";
            // 
            // pnl_state
            // 
            this.pnl_state.BackColor = System.Drawing.Color.Lime;
            this.tableLayoutPanel1.SetColumnSpan(this.pnl_state, 3);
            this.pnl_state.Location = new System.Drawing.Point(141, 3);
            this.pnl_state.Name = "pnl_state";
            this.pnl_state.Size = new System.Drawing.Size(136, 17);
            this.pnl_state.TabIndex = 4;
            // 
            // Uc_MC_State2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.panel1);
            this.Name = "Uc_MC_State2";
            this.Size = new System.Drawing.Size(280, 120);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl lbl_machine;
        private DevExpress.XtraEditors.LabelControl lbl_Prod;
        private DevExpress.XtraEditors.LabelControl lbl_Qual;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lbl_itemName;
        private DevExpress.XtraEditors.LabelControl lbl_prodQty;
        private System.Windows.Forms.Panel pnl_state;
    }
}
