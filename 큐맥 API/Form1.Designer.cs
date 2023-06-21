namespace WindowsFormsApp2
{
    partial class Form1
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dateFrom = new DevExpress.XtraEditors.DateEdit();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.memo_Send = new DevExpress.XtraEditors.MemoEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.memo_Rec = new DevExpress.XtraEditors.MemoEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.memo_Error = new DevExpress.XtraEditors.MemoEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_Send.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_Rec.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_Error.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "To";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "From";
            this.label1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateFrom
            // 
            this.dateFrom.EditValue = null;
            this.dateFrom.Location = new System.Drawing.Point(66, 22);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Size = new System.Drawing.Size(100, 20);
            this.dateFrom.TabIndex = 8;
            this.dateFrom.Visible = false;
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(217, 21);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Size = new System.Drawing.Size(100, 20);
            this.dateTo.TabIndex = 9;
            this.dateTo.Visible = false;
            // 
            // memo_Send
            // 
            this.memo_Send.Location = new System.Drawing.Point(28, 230);
            this.memo_Send.Name = "memo_Send";
            this.memo_Send.Size = new System.Drawing.Size(243, 114);
            this.memo_Send.TabIndex = 10;
            this.memo_Send.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "Send";
            this.label3.Visible = false;
            // 
            // memo_Rec
            // 
            this.memo_Rec.Location = new System.Drawing.Point(321, 230);
            this.memo_Rec.Name = "memo_Rec";
            this.memo_Rec.Size = new System.Drawing.Size(243, 114);
            this.memo_Rec.TabIndex = 13;
            this.memo_Rec.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(319, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "Receive";
            this.label4.Visible = false;
            // 
            // memo_Error
            // 
            this.memo_Error.Location = new System.Drawing.Point(28, 80);
            this.memo_Error.Name = "memo_Error";
            this.memo_Error.Size = new System.Drawing.Size(536, 102);
            this.memo_Error.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "Error";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(486, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "스레드시작";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(486, 51);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 18;
            this.button3.Text = "스레드중지";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 202);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.memo_Error);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.memo_Rec);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.memo_Send);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_Send.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_Rec.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_Error.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.DateEdit dateFrom;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private DevExpress.XtraEditors.MemoEdit memo_Send;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.MemoEdit memo_Rec;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.MemoEdit memo_Error;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

