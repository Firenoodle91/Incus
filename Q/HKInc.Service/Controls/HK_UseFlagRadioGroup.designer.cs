namespace HKInc.Service.Controls
{
    partial class HK_UseFlagRadioGroup
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rbo_All = new DevExpress.XtraEditors.CheckEdit();
            this.rbo_NotUse = new DevExpress.XtraEditors.CheckEdit();
            this.rbo_Use = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcUse = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcNotUse = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcAll = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbo_All.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbo_NotUse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbo_Use.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNotUse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAll)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.rbo_All);
            this.layoutControl1.Controls.Add(this.rbo_NotUse);
            this.layoutControl1.Controls.Add(this.rbo_Use);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
            this.layoutControl1.MaximumSize = new System.Drawing.Size(0, 30);
            this.layoutControl1.MinimumSize = new System.Drawing.Size(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(210, 30);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // rbo_All
            // 
            this.rbo_All.AutoSizeInLayoutControl = true;
            this.rbo_All.Location = new System.Drawing.Point(159, 2);
            this.rbo_All.Margin = new System.Windows.Forms.Padding(0);
            this.rbo_All.Name = "rbo_All";
            this.rbo_All.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbo_All.Properties.Appearance.Options.UseFont = true;
            this.rbo_All.Properties.Caption = "All";
            this.rbo_All.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.rbo_All.Properties.RadioGroupIndex = 0;
            this.rbo_All.Size = new System.Drawing.Size(46, 24);
            this.rbo_All.StyleController = this.layoutControl1;
            this.rbo_All.TabIndex = 6;
            this.rbo_All.TabStop = false;
            this.rbo_All.Tag = "A";
            // 
            // rbo_NotUse
            // 
            this.rbo_NotUse.AutoSizeInLayoutControl = true;
            this.rbo_NotUse.Location = new System.Drawing.Point(69, 3);
            this.rbo_NotUse.Margin = new System.Windows.Forms.Padding(0);
            this.rbo_NotUse.Name = "rbo_NotUse";
            this.rbo_NotUse.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbo_NotUse.Properties.Appearance.Options.UseFont = true;
            this.rbo_NotUse.Properties.Caption = "NotUse";
            this.rbo_NotUse.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.rbo_NotUse.Properties.RadioGroupIndex = 0;
            this.rbo_NotUse.Size = new System.Drawing.Size(78, 24);
            this.rbo_NotUse.StyleController = this.layoutControl1;
            this.rbo_NotUse.TabIndex = 5;
            this.rbo_NotUse.TabStop = false;
            this.rbo_NotUse.Tag = "N";
            // 
            // rbo_Use
            // 
            this.rbo_Use.AutoSizeInLayoutControl = true;
            this.rbo_Use.Location = new System.Drawing.Point(3, 3);
            this.rbo_Use.Margin = new System.Windows.Forms.Padding(0);
            this.rbo_Use.Name = "rbo_Use";
            this.rbo_Use.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbo_Use.Properties.Appearance.Options.UseFont = true;
            this.rbo_Use.Properties.Caption = "Use";
            this.rbo_Use.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.rbo_Use.Properties.RadioGroupIndex = 0;
            this.rbo_Use.Size = new System.Drawing.Size(53, 24);
            this.rbo_Use.StyleController = this.layoutControl1;
            this.rbo_Use.TabIndex = 4;
            this.rbo_Use.TabStop = false;
            this.rbo_Use.Tag = "Y";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.BackColor = System.Drawing.Color.Transparent;
            this.layoutControlGroup1.AppearanceGroup.Options.UseBackColor = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcUse,
            this.lcNotUse,
            this.lcAll});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(210, 30);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcUse
            // 
            this.lcUse.Control = this.rbo_Use;
            this.lcUse.Location = new System.Drawing.Point(0, 0);
            this.lcUse.Name = "lcUse";
            this.lcUse.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 10, 3, 3);
            this.lcUse.Size = new System.Drawing.Size(66, 30);
            this.lcUse.TextSize = new System.Drawing.Size(0, 0);
            this.lcUse.TextVisible = false;
            // 
            // lcNotUse
            // 
            this.lcNotUse.Control = this.rbo_NotUse;
            this.lcNotUse.Location = new System.Drawing.Point(66, 0);
            this.lcNotUse.Name = "lcNotUse";
            this.lcNotUse.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 10, 3, 3);
            this.lcNotUse.Size = new System.Drawing.Size(91, 30);
            this.lcNotUse.TextSize = new System.Drawing.Size(0, 0);
            this.lcNotUse.TextVisible = false;
            // 
            // lcAll
            // 
            this.lcAll.Control = this.rbo_All;
            this.lcAll.Location = new System.Drawing.Point(157, 0);
            this.lcAll.Name = "lcAll";
            this.lcAll.Size = new System.Drawing.Size(53, 30);
            this.lcAll.TextSize = new System.Drawing.Size(0, 0);
            this.lcAll.TextVisible = false;
            // 
            // HK_UseFlagRadioGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximumSize = new System.Drawing.Size(210, 30);
            this.MinimumSize = new System.Drawing.Size(190, 30);
            this.Name = "HK_UseFlagRadioGroup";
            this.Size = new System.Drawing.Size(210, 30);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbo_All.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbo_NotUse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbo_Use.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNotUse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit rbo_NotUse;
        private DevExpress.XtraEditors.CheckEdit rbo_Use;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lcUse;
        private DevExpress.XtraLayout.LayoutControlItem lcNotUse;
        private DevExpress.XtraEditors.CheckEdit rbo_All;
        private DevExpress.XtraLayout.LayoutControlItem lcAll;
    }
}
