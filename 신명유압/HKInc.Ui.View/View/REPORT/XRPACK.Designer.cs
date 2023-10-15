namespace HKInc.Ui.View.View.REPORT
{
    partial class XRPACK
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
            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator1 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.ItemCodeRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_ItemCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.ItemNameRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_ItemName = new DevExpress.XtraReports.UI.XRTableCell();
            this.QtyDateRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_Qty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_PrintDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.bar_ProductLotNo = new DevExpress.XtraReports.UI.XRBarCode();
            this.tx_ProductLotNo = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_CarType = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 437.9001F;
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
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(12.7F, 0F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.ItemCodeRow,
            this.ItemNameRow,
            this.QtyDateRow,
            this.xrTableRow1});
            this.xrTable6.SizeF = new System.Drawing.SizeF(773.9186F, 382.8F);
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseFont = false;
            this.xrTable6.StylePriority.UseTextAlignment = false;
            this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ItemCodeRow
            // 
            this.ItemCodeRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel1,
            this.cell_ItemCode});
            this.ItemCodeRow.Dpi = 254F;
            this.ItemCodeRow.Name = "ItemCodeRow";
            this.ItemCodeRow.Weight = 1.1660182960225547D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "품번(도번)";
            this.xrLabel1.Weight = 0.57719246020349668D;
            // 
            // cell_ItemCode
            // 
            this.cell_ItemCode.Dpi = 254F;
            this.cell_ItemCode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_ItemCode.Name = "cell_ItemCode";
            this.cell_ItemCode.StylePriority.UseFont = false;
            this.cell_ItemCode.Text = "cell_ItemCode";
            this.cell_ItemCode.Weight = 2.4228075397965032D;
            // 
            // ItemNameRow
            // 
            this.ItemNameRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel2,
            this.cell_ItemName});
            this.ItemNameRow.Dpi = 254F;
            this.ItemNameRow.Name = "ItemNameRow";
            this.ItemNameRow.Weight = 0.91615723196834531D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "품명";
            this.xrLabel2.Weight = 0.57719246020349668D;
            // 
            // cell_ItemName
            // 
            this.cell_ItemName.Dpi = 254F;
            this.cell_ItemName.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell_ItemName.Name = "cell_ItemName";
            this.cell_ItemName.StylePriority.UseFont = false;
            this.cell_ItemName.Text = "cell_ItemName";
            this.cell_ItemName.Weight = 2.4228075397965032D;
            // 
            // QtyDateRow
            // 
            this.QtyDateRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel3,
            this.cell_Qty,
            this.xrLabel4,
            this.cell_PrintDate});
            this.QtyDateRow.Dpi = 254F;
            this.QtyDateRow.Name = "QtyDateRow";
            this.QtyDateRow.Weight = 1D;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "수량";
            this.xrLabel3.Weight = 0.57719249963604191D;
            // 
            // cell_Qty
            // 
            this.cell_Qty.Dpi = 254F;
            this.cell_Qty.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_Qty.Name = "cell_Qty";
            this.cell_Qty.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
            this.cell_Qty.StylePriority.UseFont = false;
            this.cell_Qty.StylePriority.UsePadding = false;
            this.cell_Qty.StylePriority.UseTextAlignment = false;
            this.cell_Qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cell_Qty.Weight = 0.9375389142485715D;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "제조일";
            this.xrLabel4.Weight = 0.57719245894914317D;
            // 
            // cell_PrintDate
            // 
            this.cell_PrintDate.Dpi = 254F;
            this.cell_PrintDate.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_PrintDate.Name = "cell_PrintDate";
            this.cell_PrintDate.StylePriority.UseFont = false;
            this.cell_PrintDate.StylePriority.UseTextAlignment = false;
            this.cell_PrintDate.Text = "cell_PrintDate";
            this.cell_PrintDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cell_PrintDate.Weight = 0.90807612716624342D;
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
            this.bar_ProductLotNo,
            this.tx_ProductLotNo});
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 3D;
            // 
            // bar_ProductLotNo
            // 
            this.bar_ProductLotNo.AutoModule = true;
            this.bar_ProductLotNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.bar_ProductLotNo.Dpi = 254F;
            this.bar_ProductLotNo.LocationFloat = new DevExpress.Utils.PointFloat(7F, 0.003387451F);
            this.bar_ProductLotNo.Module = 5.08F;
            this.bar_ProductLotNo.Name = "bar_ProductLotNo";
            this.bar_ProductLotNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 5, 5, 5, 254F);
            this.bar_ProductLotNo.ShowText = false;
            this.bar_ProductLotNo.SizeF = new System.Drawing.SizeF(141.9F, 125.7632F);
            this.bar_ProductLotNo.StylePriority.UseBorders = false;
            this.bar_ProductLotNo.StylePriority.UsePadding = false;
            this.bar_ProductLotNo.StylePriority.UseTextAlignment = false;
            qrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
            qrCodeGenerator1.ErrorCorrectionLevel = DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.H;
            this.bar_ProductLotNo.Symbology = qrCodeGenerator1;
            this.bar_ProductLotNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // tx_ProductLotNo
            // 
            this.tx_ProductLotNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tx_ProductLotNo.Dpi = 254F;
            this.tx_ProductLotNo.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_ProductLotNo.LocationFloat = new DevExpress.Utils.PointFloat(151.9657F, 0F);
            this.tx_ProductLotNo.Name = "tx_ProductLotNo";
            this.tx_ProductLotNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.tx_ProductLotNo.SizeF = new System.Drawing.SizeF(612.6527F, 120.4227F);
            this.tx_ProductLotNo.StylePriority.UseBorders = false;
            this.tx_ProductLotNo.StylePriority.UseFont = false;
            this.tx_ProductLotNo.StylePriority.UseTextAlignment = false;
            this.tx_ProductLotNo.Text = "123456";
            this.tx_ProductLotNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.cell_CarType});
            this.xrTableRow3.Dpi = 254F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1.1660182960225547D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "제조사";
            this.xrTableCell3.Weight = 0.57719246020349668D;
            // 
            // cell_CarType
            // 
            this.cell_CarType.Dpi = 254F;
            this.cell_CarType.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_CarType.Multiline = true;
            this.cell_CarType.Name = "cell_CarType";
            this.cell_CarType.StylePriority.UseFont = false;
            this.cell_CarType.Text = "(주)신명유압";
            this.cell_CarType.Weight = 2.4228075397965032D;
            // 
            // XRPACK
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
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRTable xrTable6;
        private DevExpress.XtraReports.UI.XRTableRow ItemCodeRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel1;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItemCode;
        private DevExpress.XtraReports.UI.XRTableRow ItemNameRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel2;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItemName;
        private DevExpress.XtraReports.UI.XRTableRow QtyDateRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel3;
        private DevExpress.XtraReports.UI.XRTableCell cell_Qty;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel4;
        private DevExpress.XtraReports.UI.XRTableCell cell_PrintDate;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRLabel tx_ProductLotNo;
        private DevExpress.XtraReports.UI.XRBarCode bar_ProductLotNo;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell cell_CarType;
    }
}
