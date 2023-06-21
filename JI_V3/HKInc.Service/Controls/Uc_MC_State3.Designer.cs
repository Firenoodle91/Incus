namespace HKInc.Service.Controls
{
    partial class Uc_MC_State3
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
            this.lb_MC = new DevExpress.XtraEditors.LabelControl();
            this.lb_item = new DevExpress.XtraEditors.LabelControl();
            this.tx_qty = new DevExpress.XtraEditors.LabelControl();
            this.pic_andon = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_andon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_MC
            // 
            this.lb_MC.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lb_MC.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_MC.Appearance.ForeColor = System.Drawing.Color.White;
            this.lb_MC.Appearance.Options.UseBackColor = true;
            this.lb_MC.Appearance.Options.UseFont = true;
            this.lb_MC.Appearance.Options.UseForeColor = true;
            this.lb_MC.Appearance.Options.UseTextOptions = true;
            this.lb_MC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lb_MC.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lb_MC.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_MC.Location = new System.Drawing.Point(53, 6);
            this.lb_MC.Name = "lb_MC";
            this.lb_MC.Size = new System.Drawing.Size(166, 33);
            this.lb_MC.TabIndex = 0;
            this.lb_MC.Text = " ";
            // 
            // lb_item
            // 
            this.lb_item.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lb_item.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_item.Appearance.ForeColor = System.Drawing.Color.White;
            this.lb_item.Appearance.Options.UseBackColor = true;
            this.lb_item.Appearance.Options.UseFont = true;
            this.lb_item.Appearance.Options.UseForeColor = true;
            this.lb_item.Appearance.Options.UseTextOptions = true;
            this.lb_item.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lb_item.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lb_item.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_item.Location = new System.Drawing.Point(150, 59);
            this.lb_item.Name = "lb_item";
            this.lb_item.Size = new System.Drawing.Size(133, 23);
            this.lb_item.TabIndex = 1;
            this.lb_item.Text = " ";
            // 
            // tx_qty
            // 
            this.tx_qty.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tx_qty.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_qty.Appearance.ForeColor = System.Drawing.Color.White;
            this.tx_qty.Appearance.Options.UseBackColor = true;
            this.tx_qty.Appearance.Options.UseFont = true;
            this.tx_qty.Appearance.Options.UseForeColor = true;
            this.tx_qty.Appearance.Options.UseTextOptions = true;
            this.tx_qty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tx_qty.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tx_qty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tx_qty.Location = new System.Drawing.Point(150, 88);
            this.tx_qty.Name = "tx_qty";
            this.tx_qty.Size = new System.Drawing.Size(133, 23);
            this.tx_qty.TabIndex = 2;
            this.tx_qty.Text = " ";
            // 
            // pic_andon
            // 
            this.pic_andon.Location = new System.Drawing.Point(11, 67);
            this.pic_andon.Name = "pic_andon";
            this.pic_andon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pic_andon.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.pic_andon.Properties.Appearance.Options.UseBackColor = true;
            this.pic_andon.Properties.Appearance.Options.UseForeColor = true;
            this.pic_andon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic_andon.Properties.NullText = "No Call";
            this.pic_andon.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_andon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pic_andon.Size = new System.Drawing.Size(50, 35);
            this.pic_andon.TabIndex = 3;
            // 
            // Uc_MC_State3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HKInc.Service.Properties.Resources.stopbg1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pic_andon);
            this.Controls.Add(this.tx_qty);
            this.Controls.Add(this.lb_item);
            this.Controls.Add(this.lb_MC);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(300, 129);
            this.MinimumSize = new System.Drawing.Size(300, 129);
            this.Name = "Uc_MC_State3";
            this.Size = new System.Drawing.Size(296, 125);
            ((System.ComponentModel.ISupportInitialize)(this.pic_andon.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lb_MC;
        private DevExpress.XtraEditors.LabelControl lb_item;
        private DevExpress.XtraEditors.LabelControl tx_qty;
        private DevExpress.XtraEditors.PictureEdit pic_andon;
    }
}
