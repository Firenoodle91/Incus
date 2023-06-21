namespace HKInc.Service.Helper
{
    partial class FactoryLabel
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
            this.labelControl = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // labelControl
            // 
            this.labelControl.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.labelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.labelControl.Appearance.Options.UseBackColor = true;
            this.labelControl.Appearance.Options.UseFont = true;
            this.labelControl.Appearance.Options.UseTextOptions = true;
            this.labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl.Location = new System.Drawing.Point(0, 0);
            this.labelControl.Name = "labelControl";
            this.labelControl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelControl.Size = new System.Drawing.Size(175, 35);
            this.labelControl.TabIndex = 1;
            this.labelControl.Text = "1공장";
            // 
            // FactoryLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl);
            this.Name = "FactoryLabel";
            this.Size = new System.Drawing.Size(175, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl;
    }
}
