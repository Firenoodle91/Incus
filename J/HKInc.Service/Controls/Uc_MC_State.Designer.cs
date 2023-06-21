namespace HKInc.Service.Controls
{
    partial class Uc_MC_State
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
            this.tx_qty = new DevExpress.XtraEditors.SpinEdit();
            this.lb_MC = new DevExpress.XtraEditors.LabelControl();
            this.lb_item = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pic_state = new DevExpress.XtraEditors.PictureEdit();
            this.pic_andon = new DevExpress.XtraEditors.PictureEdit();
            this.pic_mc = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_qty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_state.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_andon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_mc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tx_qty
            // 
            this.tx_qty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tx_qty.Location = new System.Drawing.Point(23, 271);
            this.tx_qty.Name = "tx_qty";
            this.tx_qty.Properties.Appearance.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_qty.Properties.Appearance.Options.UseFont = true;
            this.tx_qty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tx_qty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tx_qty.Size = new System.Drawing.Size(202, 22);
            this.tx_qty.TabIndex = 2;
            // 
            // lb_MC
            // 
            this.lb_MC.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_MC.Appearance.Options.UseFont = true;
            this.lb_MC.Appearance.Options.UseTextOptions = true;
            this.lb_MC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lb_MC.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lb_MC.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_MC.Location = new System.Drawing.Point(3, 3);
            this.lb_MC.Name = "lb_MC";
            this.lb_MC.Size = new System.Drawing.Size(247, 42);
            this.lb_MC.TabIndex = 3;
            this.lb_MC.Text = " ";
            // 
            // lb_item
            // 
            this.lb_item.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_item.Appearance.Options.UseFont = true;
            this.lb_item.Appearance.Options.UseTextOptions = true;
            this.lb_item.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lb_item.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lb_item.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_item.Location = new System.Drawing.Point(28, 211);
            this.lb_item.Name = "lb_item";
            this.lb_item.Size = new System.Drawing.Size(188, 25);
            this.lb_item.TabIndex = 4;
            this.lb_item.Text = " ";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(7, 178);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(232, 27);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = " 생산품목";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(0, 242);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(247, 27);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = " 생산수량";
            // 
            // pic_state
            // 
            this.pic_state.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pic_state.EditValue = global::HKInc.Service.Properties.Resources.bar_blue;
            this.pic_state.Location = new System.Drawing.Point(-7, 40);
            this.pic_state.Name = "pic_state";
            this.pic_state.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic_state.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_state.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pic_state.Size = new System.Drawing.Size(266, 22);
            this.pic_state.TabIndex = 7;
            // 
            // pic_andon
            // 
            this.pic_andon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pic_andon.EditValue = global::HKInc.Service.Properties.Resources.bt_4;
            this.pic_andon.Location = new System.Drawing.Point(23, 312);
            this.pic_andon.Name = "pic_andon";
            this.pic_andon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic_andon.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_andon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pic_andon.Size = new System.Drawing.Size(207, 30);
            this.pic_andon.TabIndex = 8;
            // 
            // pic_mc
            // 
            this.pic_mc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pic_mc.EditValue = global::HKInc.Service.Properties.Resources.mc;
            this.pic_mc.Location = new System.Drawing.Point(20, 68);
            this.pic_mc.Name = "pic_mc";
            this.pic_mc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic_mc.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_mc.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pic_mc.Size = new System.Drawing.Size(214, 110);
            this.pic_mc.TabIndex = 9;
            // 
            // Uc_MC_State
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HKInc.Service.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pic_mc);
            this.Controls.Add(this.pic_andon);
            this.Controls.Add(this.pic_state);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lb_item);
            this.Controls.Add(this.lb_MC);
            this.Controls.Add(this.tx_qty);
            this.MaximumSize = new System.Drawing.Size(250, 379);
            this.MinimumSize = new System.Drawing.Size(250, 379);
            this.Name = "Uc_MC_State";
            this.Size = new System.Drawing.Size(250, 379);
            ((System.ComponentModel.ISupportInitialize)(this.tx_qty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_state.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_andon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_mc.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit tx_qty;
        private DevExpress.XtraEditors.LabelControl lb_MC;
        private DevExpress.XtraEditors.LabelControl lb_item;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pic_state;
        private DevExpress.XtraEditors.PictureEdit pic_andon;
        private DevExpress.XtraEditors.PictureEdit pic_mc;
    }
}
