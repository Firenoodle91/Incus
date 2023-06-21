namespace HKInc.Ui.View.POP_Popup
{
    partial class XPFJOBEND
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPFJOBEND));
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_qtytostop = new DevExpress.XtraEditors.SimpleButton();
            this.btn_qtytoend = new DevExpress.XtraEditors.SimpleButton();
            this.btn_stop = new DevExpress.XtraEditors.SimpleButton();
            this.btn_end = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(98, 27);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(52, 50);
            this.pictureEdit1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(171, 27);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(406, 50);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "작업종료 방법을 선택하세요.";
            // 
            // btn_qtytostop
            // 
            this.btn_qtytostop.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_qtytostop.Appearance.Options.UseFont = true;
            this.btn_qtytostop.Appearance.Options.UseTextOptions = true;
            this.btn_qtytostop.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_qtytostop.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_qtytostop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_qtytostop.ImageOptions.Image")));
            this.btn_qtytostop.Location = new System.Drawing.Point(45, 105);
            this.btn_qtytostop.Name = "btn_qtytostop";
            this.btn_qtytostop.Size = new System.Drawing.Size(246, 60);
            this.btn_qtytostop.TabIndex = 2;
            this.btn_qtytostop.Text = "실적등록 후 일시중지";
            this.btn_qtytostop.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btn_qtytoend
            // 
            this.btn_qtytoend.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_qtytoend.Appearance.Options.UseFont = true;
            this.btn_qtytoend.Appearance.Options.UseTextOptions = true;
            this.btn_qtytoend.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_qtytoend.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_qtytoend.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_qtytoend.ImageOptions.Image")));
            this.btn_qtytoend.Location = new System.Drawing.Point(310, 105);
            this.btn_qtytoend.Name = "btn_qtytoend";
            this.btn_qtytoend.Size = new System.Drawing.Size(246, 60);
            this.btn_qtytoend.TabIndex = 3;
            this.btn_qtytoend.Text = "실적등록후 작업종료";
            this.btn_qtytoend.Click += new System.EventHandler(this.btn_qtytoend_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_stop.Appearance.Options.UseFont = true;
            this.btn_stop.Appearance.Options.UseTextOptions = true;
            this.btn_stop.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_stop.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_stop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_stop.ImageOptions.Image")));
            this.btn_stop.Location = new System.Drawing.Point(45, 181);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(172, 60);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "일시중지";
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_end
            // 
            this.btn_end.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_end.Appearance.Options.UseFont = true;
            this.btn_end.Appearance.Options.UseTextOptions = true;
            this.btn_end.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_end.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_end.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_end.ImageOptions.Image")));
            this.btn_end.Location = new System.Drawing.Point(229, 181);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(166, 60);
            this.btn_end.TabIndex = 5;
            this.btn_end.Text = "작업종료";
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.Appearance.Options.UseFont = true;
            this.btn_exit.Appearance.Options.UseTextOptions = true;
            this.btn_exit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btn_exit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btn_exit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.ImageOptions.Image")));
            this.btn_exit.Location = new System.Drawing.Point(404, 181);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(154, 60);
            this.btn_exit.TabIndex = 6;
            this.btn_exit.Text = "취소";
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // XPFJOBEND
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 265);
            this.ControlBox = false;
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_end);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_qtytoend);
            this.Controls.Add(this.btn_qtytostop);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.pictureEdit1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "XPFJOBEND";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "작업종료";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_qtytostop;
        private DevExpress.XtraEditors.SimpleButton btn_qtytoend;
        private DevExpress.XtraEditors.SimpleButton btn_stop;
        private DevExpress.XtraEditors.SimpleButton btn_end;
        private DevExpress.XtraEditors.SimpleButton btn_exit;
    }
}