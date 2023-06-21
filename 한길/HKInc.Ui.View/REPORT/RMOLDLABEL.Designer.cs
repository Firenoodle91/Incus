namespace HKInc.Ui.View.REPORT
{
    partial class RMOLDLABEL
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
            DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator code39ExtendedGenerator1 = new DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.ItemCodeRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Tc_MoldCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.ItemNameRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Tc_MoldName = new DevExpress.XtraReports.UI.XRTableCell();
            this.QtyDateRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Tc_CustomerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.WhRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Tc_Spec1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Tc_W2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.bar_inlot = new DevExpress.XtraReports.UI.XRBarCode();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 422.4375F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.Dpi = 254F;
            this.xrTable6.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(12.7F, 14.81667F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.ItemCodeRow,
            this.ItemNameRow,
            this.QtyDateRow,
            this.WhRow,
            this.xrTableRow1});
            this.xrTable6.SizeF = new System.Drawing.SizeF(773.9186F, 367.9001F);
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseFont = false;
            this.xrTable6.StylePriority.UseTextAlignment = false;
            this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ItemCodeRow
            // 
            this.ItemCodeRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel1,
            this.Tc_MoldCode});
            this.ItemCodeRow.Dpi = 254F;
            this.ItemCodeRow.Name = "ItemCodeRow";
            this.ItemCodeRow.Weight = 1D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Text = "금형코드";
            this.xrLabel1.Weight = 0.71332996526860692D;
            // 
            // Tc_MoldCode
            // 
            this.Tc_MoldCode.Dpi = 254F;
            this.Tc_MoldCode.Name = "Tc_MoldCode";
            this.Tc_MoldCode.Text = "Tc_MoldCode";
            this.Tc_MoldCode.Weight = 2.2866700347313929D;
            this.Tc_MoldCode.WordWrap = false;
            // 
            // ItemNameRow
            // 
            this.ItemNameRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel2,
            this.Tc_MoldName});
            this.ItemNameRow.Dpi = 254F;
            this.ItemNameRow.Name = "ItemNameRow";
            this.ItemNameRow.Weight = 1D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Text = "금형명";
            this.xrLabel2.Weight = 0.71332996526860692D;
            // 
            // Tc_MoldName
            // 
            this.Tc_MoldName.Dpi = 254F;
            this.Tc_MoldName.Name = "Tc_MoldName";
            this.Tc_MoldName.Text = "Tc_MoldName";
            this.Tc_MoldName.Weight = 2.2866700347313929D;
            this.Tc_MoldName.WordWrap = false;
            // 
            // QtyDateRow
            // 
            this.QtyDateRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel3,
            this.Tc_CustomerName});
            this.QtyDateRow.Dpi = 254F;
            this.QtyDateRow.Name = "QtyDateRow";
            this.QtyDateRow.Weight = 1D;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Text = "거래처";
            this.xrLabel3.Weight = 0.71332994555233675D;
            // 
            // Tc_CustomerName
            // 
            this.Tc_CustomerName.Dpi = 254F;
            this.Tc_CustomerName.Name = "Tc_CustomerName";
            this.Tc_CustomerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
            this.Tc_CustomerName.StylePriority.UsePadding = false;
            this.Tc_CustomerName.StylePriority.UseTextAlignment = false;
            this.Tc_CustomerName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Tc_CustomerName.Weight = 2.2866700544476632D;
            this.Tc_CustomerName.WordWrap = false;
            // 
            // WhRow
            // 
            this.WhRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel5,
            this.Tc_Spec1,
            this.xrTableCell2,
            this.Tc_W2});
            this.WhRow.Dpi = 254F;
            this.WhRow.Name = "WhRow";
            this.WhRow.Weight = 1D;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Text = "규격";
            this.xrLabel5.Weight = 0.71332997512674923D;
            // 
            // Tc_Spec1
            // 
            this.Tc_Spec1.Dpi = 254F;
            this.Tc_Spec1.Name = "Tc_Spec1";
            this.Tc_Spec1.Text = "Tc_Spec1";
            this.Tc_Spec1.Weight = 0.80140717621287583D;
            this.Tc_Spec1.WordWrap = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "위치";
            this.xrTableCell2.Weight = 0.48693729392217289D;
            // 
            // Tc_W2
            // 
            this.Tc_W2.Dpi = 254F;
            this.Tc_W2.Name = "Tc_W2";
            this.Tc_W2.Text = "Tc_W2";
            this.Tc_W2.Weight = 0.99832555473820228D;
            this.Tc_W2.WordWrap = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 2.1282604543984265D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.bar_inlot});
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 3D;
            // 
            // bar_inlot
            // 
            this.bar_inlot.AutoModule = true;
            this.bar_inlot.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.bar_inlot.Dpi = 254F;
            this.bar_inlot.LocationFloat = new DevExpress.Utils.PointFloat(82.55F, 0F);
            this.bar_inlot.Module = 5.08F;
            this.bar_inlot.Name = "bar_inlot";
            this.bar_inlot.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 254F);
            this.bar_inlot.SizeF = new System.Drawing.SizeF(608.8186F, 127.7667F);
            this.bar_inlot.StylePriority.UseBorders = false;
            this.bar_inlot.StylePriority.UsePadding = false;
            this.bar_inlot.StylePriority.UseTextAlignment = false;
            code39ExtendedGenerator1.CalcCheckSum = false;
            code39ExtendedGenerator1.WideNarrowRatio = 3F;
            this.bar_inlot.Symbology = code39ExtendedGenerator1;
            this.bar_inlot.Text = "123456";
            this.bar_inlot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 4F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.StylePriority.UseTextAlignment = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // RMOLDLABEL
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 4);
            this.PageHeight = 399;
            this.PageWidth = 800;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRBarCode bar_inlot;
        private DevExpress.XtraReports.UI.XRTable xrTable6;
        private DevExpress.XtraReports.UI.XRTableRow ItemCodeRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel1;
        private DevExpress.XtraReports.UI.XRTableCell Tc_MoldCode;
        private DevExpress.XtraReports.UI.XRTableRow ItemNameRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel2;
        private DevExpress.XtraReports.UI.XRTableCell Tc_MoldName;
        private DevExpress.XtraReports.UI.XRTableRow QtyDateRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel3;
        private DevExpress.XtraReports.UI.XRTableCell Tc_CustomerName;
        private DevExpress.XtraReports.UI.XRTableRow WhRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell Tc_Spec1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell Tc_W2;
    }
}
