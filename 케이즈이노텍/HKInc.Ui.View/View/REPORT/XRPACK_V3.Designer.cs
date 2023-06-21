namespace HKInc.Ui.View.View.REPORT
{
    partial class XRPACK_V3
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
            this.WorkOrderRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_OwnCompanyName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_Productdate = new DevExpress.XtraReports.UI.XRTableCell();
            this.ItemCodeRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_ItemCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_OrderPackage = new DevExpress.XtraReports.UI.XRTableCell();
            this.ItemNameRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_ItenName = new DevExpress.XtraReports.UI.XRTableCell();
            this.QtyRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cell_BoxQty = new DevExpress.XtraReports.UI.XRTableCell();
            this.tx_WorkNo = new DevExpress.XtraReports.UI.XRLabel();
            this.bar_WorkNo = new DevExpress.XtraReports.UI.XRBarCode();
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
            this.Detail.HeightF = 393.6758F;
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
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(18.00009F, 5.583321F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.WorkOrderRow,
            this.ItemCodeRow,
            this.ItemNameRow,
            this.QtyRow});
            this.xrTable6.SizeF = new System.Drawing.SizeF(771.4955F, 377.4807F);
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseFont = false;
            this.xrTable6.StylePriority.UseTextAlignment = false;
            this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // WorkOrderRow
            // 
            this.WorkOrderRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.cell_OwnCompanyName,
            this.xrTableCell2,
            this.cell_Productdate});
            this.WorkOrderRow.Dpi = 254F;
            this.WorkOrderRow.Name = "WorkOrderRow";
            this.WorkOrderRow.Weight = 0.71391692095974668D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "제조사";
            this.xrTableCell3.Weight = 1.9747518119600493D;
            // 
            // cell_OwnCompanyName
            // 
            this.cell_OwnCompanyName.Dpi = 254F;
            this.cell_OwnCompanyName.Font = new System.Drawing.Font("맑은 고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_OwnCompanyName.Multiline = true;
            this.cell_OwnCompanyName.Name = "cell_OwnCompanyName";
            this.cell_OwnCompanyName.StylePriority.UseFont = false;
            this.cell_OwnCompanyName.Weight = 5.5873204280436291D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "제조일";
            this.xrTableCell2.Weight = 1.6384655021805503D;
            // 
            // cell_Productdate
            // 
            this.cell_Productdate.Dpi = 254F;
            this.cell_Productdate.Font = new System.Drawing.Font("맑은 고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_Productdate.Multiline = true;
            this.cell_Productdate.Name = "cell_Productdate";
            this.cell_Productdate.StylePriority.UseFont = false;
            this.cell_Productdate.Weight = 6.3189469663150275D;
            // 
            // ItemCodeRow
            // 
            this.ItemCodeRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel1,
            this.cell_ItemCode,
            this.xrLabel5,
            this.cell_OrderPackage});
            this.ItemCodeRow.Dpi = 254F;
            this.ItemCodeRow.Name = "ItemCodeRow";
            this.ItemCodeRow.Weight = 0.7139169705349806D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "품목코드";
            this.xrLabel1.Weight = 1.2402576457503716D;
            // 
            // cell_ItemCode
            // 
            this.cell_ItemCode.Dpi = 254F;
            this.cell_ItemCode.Font = new System.Drawing.Font("맑은 고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_ItemCode.Multiline = true;
            this.cell_ItemCode.Name = "cell_ItemCode";
            this.cell_ItemCode.StylePriority.UseFont = false;
            this.cell_ItemCode.Text = "cell_ItemCode";
            this.cell_ItemCode.Weight = 3.5091594502358219D;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "정렬포장";
            this.xrLabel5.Weight = 1.0290508559385492D;
            // 
            // cell_OrderPackage
            // 
            this.cell_OrderPackage.Dpi = 254F;
            this.cell_OrderPackage.Font = new System.Drawing.Font("맑은 고딕", 6F);
            this.cell_OrderPackage.Name = "cell_OrderPackage";
            this.cell_OrderPackage.StylePriority.UseFont = false;
            this.cell_OrderPackage.Weight = 3.9686634316843774D;
            // 
            // ItemNameRow
            // 
            this.ItemNameRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.cell_ItenName});
            this.ItemNameRow.Dpi = 254F;
            this.ItemNameRow.Name = "ItemNameRow";
            this.ItemNameRow.Weight = 1.6475006901603593D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "품명";
            this.xrTableCell1.Weight = 1.2402576457503716D;
            // 
            // cell_ItenName
            // 
            this.cell_ItenName.Dpi = 254F;
            this.cell_ItenName.Font = new System.Drawing.Font("맑은 고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_ItenName.Multiline = true;
            this.cell_ItenName.Name = "cell_ItenName";
            this.cell_ItenName.StylePriority.UseFont = false;
            this.cell_ItenName.Text = "cell_ItenName";
            this.cell_ItenName.Weight = 8.5068737378587471D;
            // 
            // QtyRow
            // 
            this.QtyRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel2,
            this.xrTableCell5,
            this.cell_BoxQty});
            this.QtyRow.Dpi = 254F;
            this.QtyRow.Name = "QtyRow";
            this.QtyRow.Weight = 1.070663604390925D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tx_WorkNo,
            this.bar_WorkNo});
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Weight = 7.5620702796252122D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "포장수량";
            this.xrTableCell5.Weight = 1.6384674921971068D;
            // 
            // cell_BoxQty
            // 
            this.cell_BoxQty.Dpi = 254F;
            this.cell_BoxQty.Font = new System.Drawing.Font("맑은 고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cell_BoxQty.Multiline = true;
            this.cell_BoxQty.Name = "cell_BoxQty";
            this.cell_BoxQty.StylePriority.UseFont = false;
            this.cell_BoxQty.Text = "cell_BoxQty";
            this.cell_BoxQty.Weight = 6.3189467077951D;
            // 
            // tx_WorkNo
            // 
            this.tx_WorkNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tx_WorkNo.Dpi = 254F;
            this.tx_WorkNo.Font = new System.Drawing.Font("맑은 고딕", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_WorkNo.LocationFloat = new DevExpress.Utils.PointFloat(112.8215F, 51.48834F);
            this.tx_WorkNo.Name = "tx_WorkNo";
            this.tx_WorkNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.tx_WorkNo.SizeF = new System.Drawing.SizeF(252.7536F, 31.60413F);
            this.tx_WorkNo.StylePriority.UseBorders = false;
            this.tx_WorkNo.StylePriority.UseFont = false;
            this.tx_WorkNo.StylePriority.UseTextAlignment = false;
            this.tx_WorkNo.Text = "123456";
            this.tx_WorkNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // bar_WorkNo
            // 
            this.bar_WorkNo.AutoModule = true;
            this.bar_WorkNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.bar_WorkNo.Dpi = 254F;
            this.bar_WorkNo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.bar_WorkNo.Module = 5.08F;
            this.bar_WorkNo.Name = "bar_WorkNo";
            this.bar_WorkNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 5, 5, 5, 254F);
            this.bar_WorkNo.ShowText = false;
            this.bar_WorkNo.SizeF = new System.Drawing.SizeF(102.5652F, 96.48071F);
            this.bar_WorkNo.StylePriority.UseBorders = false;
            this.bar_WorkNo.StylePriority.UsePadding = false;
            this.bar_WorkNo.StylePriority.UseTextAlignment = false;
            qrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
            qrCodeGenerator1.ErrorCorrectionLevel = DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.H;
            this.bar_WorkNo.Symbology = qrCodeGenerator1;
            this.bar_WorkNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.BottomMargin.HeightF = 2.529469F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.StylePriority.UseTextAlignment = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // XRPACK_V3
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 3);
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
        private DevExpress.XtraReports.UI.XRTableCell cell_OrderPackage;
        private DevExpress.XtraReports.UI.XRTableRow QtyRow;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel tx_WorkNo;
        private DevExpress.XtraReports.UI.XRBarCode bar_WorkNo;
        private DevExpress.XtraReports.UI.XRTableRow WorkOrderRow;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell cell_OwnCompanyName;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItemCode;
        private DevExpress.XtraReports.UI.XRTableCell xrLabel5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell cell_Productdate;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell cell_BoxQty;
        private DevExpress.XtraReports.UI.XRTableRow ItemNameRow;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell cell_ItenName;
    }
}
