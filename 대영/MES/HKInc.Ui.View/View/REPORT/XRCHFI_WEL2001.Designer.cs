namespace HKInc.Ui.View.View.REPORT
{
    partial class XRCHFI_WEL2001
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblJudgement = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInValue = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSpecUp = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPointNo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSpecDown = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.Label1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.Label2 = new DevExpress.XtraReports.UI.XRLabel();
            this.Label14 = new DevExpress.XtraReports.UI.XRLabel();
            this.Label5 = new DevExpress.XtraReports.UI.XRLabel();
            this.Label3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblJudgement,
            this.lblInValue,
            this.lblSpecUp,
            this.lblPointNo,
            this.lblSpecDown});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 110F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblJudgement
            // 
            this.lblJudgement.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblJudgement.Dpi = 254F;
            this.lblJudgement.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Judgement]")});
            this.lblJudgement.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblJudgement.LocationFloat = new DevExpress.Utils.PointFloat(1407.334F, 0F);
            this.lblJudgement.Name = "lblJudgement";
            this.lblJudgement.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblJudgement.SizeF = new System.Drawing.SizeF(364.6666F, 110F);
            this.lblJudgement.StylePriority.UseBorders = false;
            this.lblJudgement.StylePriority.UseFont = false;
            this.lblJudgement.StylePriority.UseTextAlignment = false;
            this.lblJudgement.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblInValue
            // 
            this.lblInValue.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblInValue.Dpi = 254F;
            this.lblInValue.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Invalue]")});
            this.lblInValue.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInValue.LocationFloat = new DevExpress.Utils.PointFloat(981.5732F, 0F);
            this.lblInValue.Name = "lblInValue";
            this.lblInValue.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInValue.SizeF = new System.Drawing.SizeF(425.7601F, 110F);
            this.lblInValue.StylePriority.UseBorders = false;
            this.lblInValue.StylePriority.UseFont = false;
            this.lblInValue.StylePriority.UseTextAlignment = false;
            this.lblInValue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSpecUp
            // 
            this.lblSpecUp.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblSpecUp.Dpi = 254F;
            this.lblSpecUp.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SpecUp]")});
            this.lblSpecUp.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSpecUp.LocationFloat = new DevExpress.Utils.PointFloat(734.9828F, 0F);
            this.lblSpecUp.Name = "lblSpecUp";
            this.lblSpecUp.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 10, 0, 0, 254F);
            this.lblSpecUp.SizeF = new System.Drawing.SizeF(246.5903F, 110F);
            this.lblSpecUp.StylePriority.UseBorders = false;
            this.lblSpecUp.StylePriority.UseFont = false;
            this.lblSpecUp.StylePriority.UsePadding = false;
            this.lblSpecUp.StylePriority.UseTextAlignment = false;
            this.lblSpecUp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblPointNo
            // 
            this.lblPointNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblPointNo.Dpi = 254F;
            this.lblPointNo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PointNo]")});
            this.lblPointNo.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPointNo.LocationFloat = new DevExpress.Utils.PointFloat(0.001550293F, 0F);
            this.lblPointNo.Multiline = true;
            this.lblPointNo.Name = "lblPointNo";
            this.lblPointNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblPointNo.SizeF = new System.Drawing.SizeF(405.3432F, 110F);
            this.lblPointNo.StylePriority.UseBorders = false;
            this.lblPointNo.StylePriority.UseFont = false;
            this.lblPointNo.StylePriority.UseTextAlignment = false;
            this.lblPointNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSpecDown
            // 
            this.lblSpecDown.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblSpecDown.Dpi = 254F;
            this.lblSpecDown.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SpecDown]")});
            this.lblSpecDown.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSpecDown.LocationFloat = new DevExpress.Utils.PointFloat(405.3448F, 0F);
            this.lblSpecDown.Name = "lblSpecDown";
            this.lblSpecDown.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 10, 0, 0, 254F);
            this.lblSpecDown.SizeF = new System.Drawing.SizeF(329.6383F, 110F);
            this.lblSpecDown.StylePriority.UseBorders = false;
            this.lblSpecDown.StylePriority.UseFont = false;
            this.lblSpecDown.StylePriority.UsePadding = false;
            this.lblSpecDown.StylePriority.UseTextAlignment = false;
            this.lblSpecDown.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 156.8667F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 150F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.BorderColor = System.Drawing.Color.Black;
            this.xrLabel11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0.002067057F, 0.3999756F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(1772F, 85.19454F);
            this.xrLabel11.StylePriority.UseBorderColor = false;
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "주식회사 대영정밀";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BorderColor = System.Drawing.Color.Black;
            this.lblTitle.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.lblTitle.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblTitle.Dpi = 254F;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTitle.SizeF = new System.Drawing.SizeF(1772.001F, 168.5209F);
            this.lblTitle.StylePriority.UseBorderColor = false;
            this.lblTitle.StylePriority.UseBorderDashStyle = false;
            this.lblTitle.StylePriority.UseBorders = false;
            this.lblTitle.StylePriority.UseFont = false;
            this.lblTitle.StylePriority.UseTextAlignment = false;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 168.5209F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(886F, 110F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "이미지1";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel1.WordWrap = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(886.0001F, 168.5209F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(886F, 110F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "이미지2";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Label1.Dpi = 254F;
            this.Label1.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label1.LocationFloat = new DevExpress.Utils.PointFloat(0.002067057F, 1236.608F);
            this.Label1.Multiline = true;
            this.Label1.Name = "Label1";
            this.Label1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Label1.SizeF = new System.Drawing.SizeF(405.3432F, 109.9999F);
            this.Label1.StylePriority.UseBorders = false;
            this.Label1.StylePriority.UseFont = false;
            this.Label1.StylePriority.UseTextAlignment = false;
            this.Label1.Text = "검사포인트";
            this.Label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Label1.WordWrap = false;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0.002067057F, 1139.241F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(1771.998F, 97.36664F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "상세정보";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel8.WordWrap = false;
            // 
            // Label2
            // 
            this.Label2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Label2.Dpi = 254F;
            this.Label2.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label2.LocationFloat = new DevExpress.Utils.PointFloat(405.3447F, 1236.607F);
            this.Label2.Multiline = true;
            this.Label2.Name = "Label2";
            this.Label2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Label2.SizeF = new System.Drawing.SizeF(329.6383F, 110F);
            this.Label2.StylePriority.UseBorders = false;
            this.Label2.StylePriority.UseFont = false;
            this.Label2.StylePriority.UseTextAlignment = false;
            this.Label2.Text = "스펙하한";
            this.Label2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Label2.WordWrap = false;
            // 
            // Label14
            // 
            this.Label14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Label14.Dpi = 254F;
            this.Label14.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label14.LocationFloat = new DevExpress.Utils.PointFloat(981.5734F, 1236.608F);
            this.Label14.Name = "Label14";
            this.Label14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Label14.SizeF = new System.Drawing.SizeF(425.76F, 109.9999F);
            this.Label14.StylePriority.UseBorders = false;
            this.Label14.StylePriority.UseFont = false;
            this.Label14.StylePriority.UseTextAlignment = false;
            this.Label14.Text = "평가값";
            this.Label14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Label14.WordWrap = false;
            // 
            // Label5
            // 
            this.Label5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Label5.Dpi = 254F;
            this.Label5.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label5.LocationFloat = new DevExpress.Utils.PointFloat(1407.333F, 1236.608F);
            this.Label5.Name = "Label5";
            this.Label5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Label5.SizeF = new System.Drawing.SizeF(364.6666F, 109.9999F);
            this.Label5.StylePriority.UseBorders = false;
            this.Label5.StylePriority.UseFont = false;
            this.Label5.StylePriority.UseTextAlignment = false;
            this.Label5.Text = "평가";
            this.Label5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Label5.WordWrap = false;
            // 
            // Label3
            // 
            this.Label3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Label3.Dpi = 254F;
            this.Label3.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label3.LocationFloat = new DevExpress.Utils.PointFloat(734.983F, 1236.608F);
            this.Label3.Multiline = true;
            this.Label3.Name = "Label3";
            this.Label3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Label3.SizeF = new System.Drawing.SizeF(246.5903F, 109.9999F);
            this.Label3.StylePriority.UseBorders = false;
            this.Label3.StylePriority.UseFont = false;
            this.Label3.StylePriority.UseTextAlignment = false;
            this.Label3.Text = "스펙상한";
            this.Label3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Label3.WordWrap = false;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.ImageUrl = "none";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 278.521F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(886F, 375.36F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox2.Dpi = 254F;
            this.xrPictureBox2.ImageUrl = "none";
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(886.0001F, 278.5209F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(886F, 375.36F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox2.StylePriority.UseBorders = false;
            // 
            // xrPictureBox4
            // 
            this.xrPictureBox4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox4.Dpi = 254F;
            this.xrPictureBox4.ImageUrl = "none";
            this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(886.0006F, 763.8809F);
            this.xrPictureBox4.Name = "xrPictureBox4";
            this.xrPictureBox4.SizeF = new System.Drawing.SizeF(886F, 375.36F);
            this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox4.StylePriority.UseBorders = false;
            // 
            // xrPictureBox3
            // 
            this.xrPictureBox3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox3.Dpi = 254F;
            this.xrPictureBox3.ImageUrl = "none";
            this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 763.881F);
            this.xrPictureBox3.Name = "xrPictureBox3";
            this.xrPictureBox3.SizeF = new System.Drawing.SizeF(886F, 375.36F);
            this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox3.StylePriority.UseBorders = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(886.0006F, 653.8809F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(886F, 110F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "이미지4";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 653.8809F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(886F, 110F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "이미지3";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel3.WordWrap = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(1605.328F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(164.6721F, 42.33334F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "A4(210x297)";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel11});
            this.PageFooter.Dpi = 254F;
            this.PageFooter.HeightF = 85.59452F;
            this.PageFooter.Name = "PageFooter";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel4,
            this.xrPictureBox3,
            this.xrPictureBox4,
            this.xrPictureBox2,
            this.xrPictureBox1,
            this.Label3,
            this.Label5,
            this.Label14,
            this.Label2,
            this.xrLabel8,
            this.Label1,
            this.xrLabel2,
            this.xrLabel1,
            this.lblTitle});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 1346.608F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataMember = "TN_WEL2002";
            this.objectDataSource1.DataSource = typeof(HKInc.Ui.Model.Domain.VIEW.TN_WEL2003);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // XRCHFI_WEL2001
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.bindingSource1,
            this.objectDataSource1});
            this.DataSource = this.bindingSource1;
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(169, 159, 157, 150);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel lblSpecUp;
        private DevExpress.XtraReports.UI.XRLabel lblPointNo;
        private DevExpress.XtraReports.UI.XRLabel lblSpecDown;
        private DevExpress.XtraReports.UI.XRLabel lblInValue;
        private DevExpress.XtraReports.UI.XRLabel lblJudgement;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraReports.UI.XRLabel lblTitle;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel Label1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel Label2;
        private DevExpress.XtraReports.UI.XRLabel Label14;
        private DevExpress.XtraReports.UI.XRLabel Label5;
        private DevExpress.XtraReports.UI.XRLabel Label3;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox2;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox4;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
