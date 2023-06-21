namespace HKInc.Ui.View.View.REPORT
{
    partial class XRPUR1301
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
            this.cell_OutQty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_OutDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.WhRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_InLotNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_InCustomerLotNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tx_OutLotNo = new DevExpress.XtraReports.UI.XRLabel();
            this.bar_OutLotNo = new DevExpress.XtraReports.UI.XRBarCode();
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
            this.cell_ItemCode});
            this.ItemCodeRow.Dpi = 254F;
            this.ItemCodeRow.Name = "ItemCodeRow";
            this.ItemCodeRow.Weight = 1D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "품번[도번]";
            this.xrLabel1.Weight = 0.57719246020349668D;
            // 
            // cell_ItemCode
            // 
            this.cell_ItemCode.Dpi = 254F;
            this.cell_ItemCode.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.ItemNameRow.Weight = 1D;
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
            this.cell_OutQty,
            this.xrLabel4,
            this.cell_OutDate});
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
            this.xrLabel3.Text = "출고량";
            this.xrLabel3.Weight = 0.57719249963604191D;
            // 
            // cell_OutQty
            // 
            this.cell_OutQty.Dpi = 254F;
            this.cell_OutQty.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell_OutQty.Name = "cell_OutQty";
            this.cell_OutQty.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
            this.cell_OutQty.StylePriority.UseFont = false;
            this.cell_OutQty.StylePriority.UsePadding = false;
            this.cell_OutQty.StylePriority.UseTextAlignment = false;
            this.cell_OutQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cell_OutQty.Weight = 0.9375389142485715D;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "출고일";
            this.xrLabel4.Weight = 0.57719245894914317D;
            // 
            // cell_OutDate
            // 
            this.cell_OutDate.Dpi = 254F;
            this.cell_OutDate.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell_OutDate.Name = "cell_OutDate";
            this.cell_OutDate.StylePriority.UseFont = false;
            this.cell_OutDate.StylePriority.UseTextAlignment = false;
            this.cell_OutDate.Text = "cell_OutDate";
            this.cell_OutDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cell_OutDate.Weight = 0.90807612716624342D;
            // 
            // WhRow
            // 
            this.WhRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel5,
            this.cell_InLotNo,
            this.xrTableCell2,
            this.cell_InCustomerLotNo});
            this.WhRow.Dpi = 254F;
            this.WhRow.Name = "WhRow";
            this.WhRow.Weight = 1D;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "입고LOT";
            this.xrLabel5.Weight = 0.57719249963604669D;
            // 
            // cell_InLotNo
            // 
            this.cell_InLotNo.Dpi = 254F;
            this.cell_InLotNo.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell_InLotNo.Name = "cell_InLotNo";
            this.cell_InLotNo.StylePriority.UseFont = false;
            this.cell_InLotNo.Text = "cell_InLotNo";
            this.cell_InLotNo.Weight = 0.93753956490545187D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "납품처LOT";
            this.xrTableCell2.Weight = 0.577192500950125D;
            // 
            // cell_InCustomerLotNo
            // 
            this.cell_InCustomerLotNo.Dpi = 254F;
            this.cell_InCustomerLotNo.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell_InCustomerLotNo.Name = "cell_InCustomerLotNo";
            this.cell_InCustomerLotNo.StylePriority.UseFont = false;
            this.cell_InCustomerLotNo.Text = "cell_InCustomerLotNo";
            this.cell_InCustomerLotNo.Weight = 0.90807543450837647D;
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
            this.tx_OutLotNo,
            this.bar_OutLotNo});
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 3D;
            // 
            // tx_OutLotNo
            // 
            this.tx_OutLotNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tx_OutLotNo.Dpi = 254F;
            this.tx_OutLotNo.LocationFloat = new DevExpress.Utils.PointFloat(293.1342F, 18.30177F);
            this.tx_OutLotNo.Name = "tx_OutLotNo";
            this.tx_OutLotNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.tx_OutLotNo.SizeF = new System.Drawing.SizeF(343.096F, 77.49583F);
            this.tx_OutLotNo.StylePriority.UseBorders = false;
            this.tx_OutLotNo.StylePriority.UseTextAlignment = false;
            this.tx_OutLotNo.Text = "123456";
            this.tx_OutLotNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // bar_OutLotNo
            // 
            this.bar_OutLotNo.AutoModule = true;
            this.bar_OutLotNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.bar_OutLotNo.Dpi = 254F;
            this.bar_OutLotNo.LocationFloat = new DevExpress.Utils.PointFloat(137.6884F, 5.201218F);
            this.bar_OutLotNo.Module = 5.08F;
            this.bar_OutLotNo.Name = "bar_OutLotNo";
            this.bar_OutLotNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
            this.bar_OutLotNo.ShowText = false;
            this.bar_OutLotNo.SizeF = new System.Drawing.SizeF(155.4458F, 117.3642F);
            this.bar_OutLotNo.StylePriority.UseBorders = false;
            this.bar_OutLotNo.StylePriority.UseTextAlignment = false;
            qrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
            qrCodeGenerator1.ErrorCorrectionLevel = DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.H;
            this.bar_OutLotNo.Symbology = qrCodeGenerator1;
            this.bar_OutLotNo.Text = "123456";
            this.bar_OutLotNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            // XRPUR1301
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
        private DevExpress.XtraReports.UI.XRTable xrTable6;
        private DevExpress.XtraReports.UI.XRTableRow ItemCodeRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel1;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItemCode;
        private DevExpress.XtraReports.UI.XRTableRow ItemNameRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel2;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItemName;
        private DevExpress.XtraReports.UI.XRTableRow QtyDateRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel3;
        private DevExpress.XtraReports.UI.XRTableCell cell_OutQty;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel4;
        private DevExpress.XtraReports.UI.XRTableCell cell_OutDate;
        private DevExpress.XtraReports.UI.XRTableRow WhRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel5;
        private DevExpress.XtraReports.UI.XRTableCell cell_InCustomerLotNo;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell cell_InLotNo;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRLabel tx_OutLotNo;
        private DevExpress.XtraReports.UI.XRBarCode bar_OutLotNo;
    }
}
