namespace HKInc.Ui.View.View.REPORT
{
    partial class XRPUR1201_V2
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
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_ItemName = new DevExpress.XtraReports.UI.XRTableCell();
            this.ItemNameRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_ItemName1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.QtyDateRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_InQty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_InDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.bar_inlot = new DevExpress.XtraReports.UI.XRBarCode();
            this.tx_inlot = new DevExpress.XtraReports.UI.XRLabel();
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
            this.Detail.HeightF = 585.0866F;
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
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(12.70016F, 14.81668F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.ItemCodeRow,
            this.ItemNameRow,
            this.QtyDateRow,
            this.xrTableRow1});
            this.xrTable6.SizeF = new System.Drawing.SizeF(962.2999F, 545.27F);
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseFont = false;
            this.xrTable6.StylePriority.UseTextAlignment = false;
            this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ItemCodeRow
            // 
            this.ItemCodeRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel1,
            this.cell_ItemCode,
            this.xrTableCell2,
            this.cell_ItemName});
            this.ItemCodeRow.Dpi = 254F;
            this.ItemCodeRow.Name = "ItemCodeRow";
            this.ItemCodeRow.Weight = 1.2644714535884174D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "품목코드";
            this.xrLabel1.Weight = 0.577191809566527D;
            // 
            // cell_ItemCode
            // 
            this.cell_ItemCode.Dpi = 254F;
            this.cell_ItemCode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_ItemCode.Name = "cell_ItemCode";
            this.cell_ItemCode.StylePriority.UseFont = false;
            this.cell_ItemCode.Text = "cell_ItemCode";
            this.cell_ItemCode.Weight = 0.93754014088340742D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "품번";
            this.xrTableCell2.Weight = 0.57719174187366185D;
            // 
            // cell_ItemName
            // 
            this.cell_ItemName.Dpi = 254F;
            this.cell_ItemName.Font = new System.Drawing.Font("맑은 고딕", 7.8F);
            this.cell_ItemName.Multiline = true;
            this.cell_ItemName.Name = "cell_ItemName";
            this.cell_ItemName.StylePriority.UseFont = false;
            this.cell_ItemName.Text = "cell_ItemName";
            this.cell_ItemName.Weight = 0.90807630767640357D;
            // 
            // ItemNameRow
            // 
            this.ItemNameRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel2,
            this.cell_ItemName1});
            this.ItemNameRow.Dpi = 254F;
            this.ItemNameRow.Name = "ItemNameRow";
            this.ItemNameRow.Weight = 0.9706138827968589D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "품명";
            this.xrLabel2.Weight = 0.57719246020349668D;
            // 
            // cell_ItemName1
            // 
            this.cell_ItemName1.Dpi = 254F;
            this.cell_ItemName1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_ItemName1.Name = "cell_ItemName1";
            this.cell_ItemName1.StylePriority.UseFont = false;
            this.cell_ItemName1.Text = "cell_ItemName1";
            this.cell_ItemName1.Weight = 2.4228075397965032D;
            // 
            // QtyDateRow
            // 
            this.QtyDateRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel3,
            this.cell_InQty,
            this.xrLabel4,
            this.cell_InDate});
            this.QtyDateRow.Dpi = 254F;
            this.QtyDateRow.Name = "QtyDateRow";
            this.QtyDateRow.Weight = 1.1193456183333246D;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "입고량";
            this.xrLabel3.Weight = 0.57719249963604191D;
            // 
            // cell_InQty
            // 
            this.cell_InQty.Dpi = 254F;
            this.cell_InQty.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_InQty.Name = "cell_InQty";
            this.cell_InQty.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
            this.cell_InQty.StylePriority.UseFont = false;
            this.cell_InQty.StylePriority.UsePadding = false;
            this.cell_InQty.StylePriority.UseTextAlignment = false;
            this.cell_InQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cell_InQty.Weight = 0.9375389142485715D;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "입고일";
            this.xrLabel4.Weight = 0.57719245894914317D;
            // 
            // cell_InDate
            // 
            this.cell_InDate.Dpi = 254F;
            this.cell_InDate.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_InDate.Name = "cell_InDate";
            this.cell_InDate.StylePriority.UseFont = false;
            this.cell_InDate.StylePriority.UseTextAlignment = false;
            this.cell_InDate.Text = "cell_InDate";
            this.cell_InDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cell_InDate.Weight = 0.90807612716624342D;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1.7738294996798261D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.bar_inlot,
            this.tx_inlot});
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 3D;
            // 
            // bar_inlot
            // 
            this.bar_inlot.AutoModule = true;
            this.bar_inlot.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.bar_inlot.Dpi = 254F;
            this.bar_inlot.LocationFloat = new DevExpress.Utils.PointFloat(3.30022F, 0F);
            this.bar_inlot.Module = 5.08F;
            this.bar_inlot.Name = "bar_inlot";
            this.bar_inlot.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 5, 5, 5, 254F);
            this.bar_inlot.ShowText = false;
            this.bar_inlot.SizeF = new System.Drawing.SizeF(181.8436F, 188.6051F);
            this.bar_inlot.StylePriority.UseBorders = false;
            this.bar_inlot.StylePriority.UsePadding = false;
            this.bar_inlot.StylePriority.UseTextAlignment = false;
            qrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
            qrCodeGenerator1.ErrorCorrectionLevel = DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.H;
            this.bar_inlot.Symbology = qrCodeGenerator1;
            this.bar_inlot.Text = "IN-200527-00001001-2";
            this.bar_inlot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // tx_inlot
            // 
            this.tx_inlot.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tx_inlot.Dpi = 254F;
            this.tx_inlot.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_inlot.LocationFloat = new DevExpress.Utils.PointFloat(188.1441F, 56.83502F);
            this.tx_inlot.Name = "tx_inlot";
            this.tx_inlot.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.tx_inlot.SizeF = new System.Drawing.SizeF(762.3683F, 127.77F);
            this.tx_inlot.StylePriority.UseBorders = false;
            this.tx_inlot.StylePriority.UseFont = false;
            this.tx_inlot.StylePriority.UseTextAlignment = false;
            this.tx_inlot.Text = "123456";
            this.tx_inlot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.BottomMargin.HeightF = 0.2213927F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.StylePriority.UseTextAlignment = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // XRPUR1201_V2
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 599;
            this.PageWidth = 1000;
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
        private DevExpress.XtraReports.UI.XRTableRow ItemNameRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel2;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItemName1;
        private DevExpress.XtraReports.UI.XRTableRow QtyDateRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel3;
        private DevExpress.XtraReports.UI.XRTableCell cell_InQty;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel4;
        private DevExpress.XtraReports.UI.XRTableCell cell_InDate;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRBarCode bar_inlot;
        private DevExpress.XtraReports.UI.XRLabel tx_inlot;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItemCode;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItemName;
    }
}
