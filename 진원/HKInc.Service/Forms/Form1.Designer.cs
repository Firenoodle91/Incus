namespace HKInc.Service.Forms
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tx_ip = new System.Windows.Forms.TextBox();
            this.tx_db = new System.Windows.Forms.TextBox();
            this.tx_user = new System.Windows.Forms.TextBox();
            this.tx_passwd = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tx_admin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(74, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(13, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "DB_NAME";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "UserID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(13, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Passwd";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tx_ip
            // 
            this.tx_ip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tx_ip.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_ip.Location = new System.Drawing.Point(131, 75);
            this.tx_ip.Name = "tx_ip";
            this.tx_ip.Size = new System.Drawing.Size(300, 23);
            this.tx_ip.TabIndex = 4;
            // 
            // tx_db
            // 
            this.tx_db.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tx_db.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_db.Location = new System.Drawing.Point(125, 16);
            this.tx_db.Name = "tx_db";
            this.tx_db.Size = new System.Drawing.Size(300, 23);
            this.tx_db.TabIndex = 5;
            // 
            // tx_user
            // 
            this.tx_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tx_user.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_user.Location = new System.Drawing.Point(125, 67);
            this.tx_user.Name = "tx_user";
            this.tx_user.Size = new System.Drawing.Size(300, 23);
            this.tx_user.TabIndex = 6;
            // 
            // tx_passwd
            // 
            this.tx_passwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tx_passwd.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_passwd.Location = new System.Drawing.Point(125, 117);
            this.tx_passwd.Name = "tx_passwd";
            this.tx_passwd.Size = new System.Drawing.Size(300, 23);
            this.tx_passwd.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 40);
            this.button1.TabIndex = 8;
            this.button1.Text = "적용";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(269, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 40);
            this.button2.TabIndex = 9;
            this.button2.Text = "취소";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tx_passwd);
            this.panel1.Controls.Add(this.tx_user);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tx_db);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(12, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 219);
            this.panel1.TabIndex = 14;
            this.panel1.Visible = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(229, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 40);
            this.button5.TabIndex = 15;
            this.button5.Text = "내부IP";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(355, 7);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 40);
            this.button6.TabIndex = 16;
            this.button6.Text = "DB정보수정";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(103, 7);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(120, 40);
            this.button7.TabIndex = 17;
            this.button7.Text = "공인IP";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tx_admin);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.button9);
            this.panel2.Controls.Add(this.button8);
            this.panel2.Location = new System.Drawing.Point(9, 123);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(466, 251);
            this.panel2.TabIndex = 18;
            this.panel2.Visible = false;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(145, 98);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(120, 40);
            this.button8.TabIndex = 0;
            this.button8.Text = "확인";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(273, 98);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(120, 40);
            this.button9.TabIndex = 1;
            this.button9.Text = "취소";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(44, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "관리자 비번";
            // 
            // tx_admin
            // 
            this.tx_admin.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_admin.Location = new System.Drawing.Point(166, 43);
            this.tx_admin.Name = "tx_admin";
            this.tx_admin.Size = new System.Drawing.Size(238, 30);
            this.tx_admin.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(120, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(320, 40);
            this.label6.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(120, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(320, 40);
            this.label5.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(117, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(320, 40);
            this.label8.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(129, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(320, 40);
            this.label9.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 388);
            this.Controls.Add(this.tx_ip);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tx_ip;
        private System.Windows.Forms.TextBox tx_db;
        private System.Windows.Forms.TextBox tx_user;
        private System.Windows.Forms.TextBox tx_passwd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tx_admin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
    }
}