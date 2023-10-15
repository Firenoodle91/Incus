namespace HKInc.Ui.View.MPS
{
    partial class XFMPS2000
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFMPS2000));
            this.ListFormTemplatelayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.textItemCodeName = new DevExpress.XtraEditors.TextEdit();
            this.gridEx1 = new HKInc.Service.Controls.GridEx();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.datePeriodEditEx1 = new HKInc.Service.Controls.DatePeriodEditEx();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textCustomerCodeName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormTemplatelayoutControl1ConvertedLayout)).BeginInit();
            this.ListFormTemplatelayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textItemCodeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCustomerCodeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageList
            // 
            this.IconImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("IconImageList.ImageStream")));
            // 
            // ListFormTemplatelayoutControl1ConvertedLayout
            // 
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.datePeriodEditEx1);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.textItemCodeName);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.gridEx1);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Controls.Add(this.textCustomerCodeName);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListFormTemplatelayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 39);
            this.ListFormTemplatelayoutControl1ConvertedLayout.Name = "ListFormTemplatelayoutControl1ConvertedLayout";
            this.ListFormTemplatelayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.ListFormTemplatelayoutControl1ConvertedLayout.Size = new System.Drawing.Size(1070, 653);
            this.ListFormTemplatelayoutControl1ConvertedLayout.TabIndex = 4;
            // 
            // textItemCodeName
            // 
            this.textItemCodeName.Location = new System.Drawing.Point(413, 56);
            this.textItemCodeName.Name = "textItemCodeName";
            this.textItemCodeName.Size = new System.Drawing.Size(122, 24);
            this.textItemCodeName.StyleController = this.ListFormTemplatelayoutControl1ConvertedLayout;
            this.textItemCodeName.TabIndex = 1;
            // 
            // gridEx1
            // 
            this.gridEx1.DataSource = null;
            this.gridEx1.Location = new System.Drawing.Point(31, 141);
            this.gridEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEx1.Name = "gridEx1";
            this.gridEx1.Size = new System.Drawing.Size(1008, 481);
            this.gridEx1.TabIndex = 4;
            this.gridEx1.ViewType = HKInc.Utils.Enum.GridViewType.GridView;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1070, 653);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup2.Size = new System.Drawing.Size(1044, 85);
            this.layoutControlGroup2.Text = "조회조건";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(726, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(288, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.textItemCodeName;
            this.layoutControlItem6.Location = new System.Drawing.Point(294, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(216, 30);
            this.layoutControlItem6.Text = "품번/명";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(84, 18);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 85);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup3.Size = new System.Drawing.Size(1044, 542);
            this.layoutControlGroup3.Text = "생산실적리스트";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridEx1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1014, 487);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // datePeriodEditEx1
            // 
            this.datePeriodEditEx1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.datePeriodEditEx1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePeriodEditEx1.Appearance.Options.UseBackColor = true;
            this.datePeriodEditEx1.Appearance.Options.UseFont = true;
            this.datePeriodEditEx1.EditFrValue = new System.DateTime(2019, 9, 25, 0, 0, 0, 0);
            this.datePeriodEditEx1.EditToValue = new System.DateTime(2019, 10, 25, 23, 59, 59, 990);
            this.datePeriodEditEx1.Location = new System.Drawing.Point(119, 56);
            this.datePeriodEditEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.datePeriodEditEx1.MaximumSize = new System.Drawing.Size(200, 20);
            this.datePeriodEditEx1.MinimumSize = new System.Drawing.Size(200, 20);
            this.datePeriodEditEx1.Name = "datePeriodEditEx1";
            this.datePeriodEditEx1.Size = new System.Drawing.Size(200, 20);
            this.datePeriodEditEx1.TabIndex = 6;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.datePeriodEditEx1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(294, 30);
            this.layoutControlItem1.Text = "실적일자";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(84, 18);
            // 
            // textCustomerCodeName
            // 
            this.textCustomerCodeName.Location = new System.Drawing.Point(629, 56);
            this.textCustomerCodeName.Name = "textCustomerCodeName";
            this.textCustomerCodeName.Size = new System.Drawing.Size(122, 24);
            this.textCustomerCodeName.StyleController = this.ListFormTemplatelayoutControl1ConvertedLayout;
            this.textCustomerCodeName.TabIndex = 1;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textCustomerCodeName;
            this.layoutControlItem2.CustomizationFormText = "품번/명";
            this.layoutControlItem2.Location = new System.Drawing.Point(510, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(216, 30);
            this.layoutControlItem2.Text = "거래처코드/명";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 18);
            // 
            // XFMPS2000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 726);
            this.Controls.Add(this.ListFormTemplatelayoutControl1ConvertedLayout);
            this.Name = "XFMPS2000";
            this.Text = "거래처별 생산실적현황";
            this.Controls.SetChildIndex(this.ListFormTemplatelayoutControl1ConvertedLayout, 0);
            ((System.ComponentModel.ISupportInitialize)(this.GridBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormTemplatelayoutControl1ConvertedLayout)).EndInit();
            this.ListFormTemplatelayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textItemCodeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCustomerCodeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl ListFormTemplatelayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Service.Controls.GridEx gridEx1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit textItemCodeName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private Service.Controls.DatePeriodEditEx datePeriodEditEx1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit textCustomerCodeName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}