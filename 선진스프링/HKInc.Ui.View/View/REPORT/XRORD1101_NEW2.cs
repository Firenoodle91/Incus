using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Linq;
using HKInc.Utils;
using HKInc.Utils.Class;
using System.Collections.Generic;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraPrinting.BarCode;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 출고증
    /// </summary>
    public partial class XRORD1101_NEW2 : DevExpress.XtraReports.UI.XtraReport
    {
        private List<TN_STD1100> ItemList;
        private List<TN_STD1400> CustomerList;
        private List<VI_PROD_STOCK_ITEM> StockList;
        //private List<TN_STD1000> CarTypeList;
        private List<TN_WMS2000> PositionList;

        private int rowNumber = 0;
        private int cultureIndex;

        public XRORD1101_NEW2()
        {
            InitializeComponent();

            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_Customer.BeforePrint += Cell_Customer_BeforePrint;
            cell_ItemCode.BeforePrint += Cell_ItemCode_BeforePrint;
            cell_ItemName.BeforePrint += Cell_ItemName_BeforePrint;
            cell_StockQty.BeforePrint += Cell_StockQty_BeforePrint;
            cell_WhPosition.BeforePrint += Cell_WhPosition_BeforePrint;
            xrTableCell27.BeforePrint += XrTableCell27_BeforePrint;
            //cell_BarCode.BeforePrint += Cell_BarCode_BeforePrint; ;

            IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");
            ItemList = ModelService.GetChildList<TN_STD1100>(p => true).ToList();
            CustomerList = ModelService.GetChildList<TN_STD1400>(p => true).ToList();
            StockList = ModelService.GetChildList<VI_PROD_STOCK_ITEM>(p => true).ToList();
            //CarTypeList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.CarType);
            PositionList = ModelService.GetChildList<TN_WMS2000>(p => true).ToList();

            cultureIndex = DataConvert.GetCultureIndex();
        }

        public XRORD1101_NEW2(List<TN_ORD1101> list) : this()
        {
            bindingSource1.DataSource = list.ToList();

            var managerCode = list.First().TN_ORD1103.BusinessManagementId;
            if (!managerCode.IsNullOrEmpty())
            {
                IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");
                cell_ManagerName.Text = ModelService.GetChildList<User>(p => p.LoginId == managerCode).First().UserName;                
            }

            cell_TotalAmt.Text = "금액 합계 : " + list.Sum(p => p.Amt).GetDecimalNullToZero().ToString("N0");
        }

        private void Cell_No_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (cell_No.Text.IsNullOrEmpty())
                cell_No.Text = string.Empty;
            else
            {
                rowNumber++;
                cell_No.Text = rowNumber.ToString("n0");
            }
        }

        private void XrTableCell27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (cell_No.Text.IsNullOrEmpty())
            //    cell_ItemCode.Text = string.Empty;

            var itemCode = cell_ItemCode.Text;
            if (itemCode.IsNullOrEmpty())
                xrTableCell27.Text = string.Empty;
            else
                xrTableCell27.Text = "□";
            //if(itemCode == "3815101800")
            //    xrTableCell27.Text = "1";
        }

        private void Cell_ItemCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (cell_No.Text.IsNullOrEmpty())
                cell_ItemCode.Text = string.Empty;
        }

        private void Cell_ItemName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (cell_No.Text.IsNullOrEmpty())
                cell_ItemName.Text = string.Empty;

            var itemCode = cell_ItemCode.Text;
            if (itemCode.IsNullOrEmpty())
                cell_ItemName.Text = string.Empty;
            else
            {
                var itemObj = ItemList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                if (itemObj == null)
                    cell_ItemName.Text = string.Empty;
                else
                    cell_ItemName.Text = cultureIndex == 1 ? itemObj.ItemName : (cultureIndex == 2 ? itemObj.ItemNameENG : itemObj.ItemNameCHN);
            }
        }

        private void Cell_Customer_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (cell_No.Text.IsNullOrEmpty())
            //    cell_Customer.Text = string.Empty;

            var customerCode = cell_Customer.Text;
            if (customerCode.IsNullOrEmpty())
                cell_Customer.Text = string.Empty;
            else
            {
                var customerObj = CustomerList.Where(p => p.CustomerCode == customerCode).FirstOrDefault();
                if (customerObj == null)
                    cell_Customer.Text = string.Empty;
                else
                    cell_Customer.Text = cultureIndex == 1 ? customerObj.CustomerName : (cultureIndex == 2 ? customerObj.CustomerNameENG : customerObj.CustomerNameCHN);
            }
        }

        private void Cell_StockQty_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var itemCode = cell_ItemCode.Text;
            if (itemCode.IsNullOrEmpty())
                cell_StockQty.Text = string.Empty;
            else
            {
                var stockObj = StockList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                if (stockObj == null)
                    cell_StockQty.Text = "0";
                else
                    cell_StockQty.Text = stockObj.SumStockQty.ToString("#,0.##");
            }
        }

        private void Cell_OutDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
        private void Cell_CarType_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //var carType = cell_CarType.Text;
            //if (carType.IsNullOrEmpty())
            //    cell_CarType.Text = string.Empty;
            //else
            //{
            //    var carTypeObj = CarTypeList.Where(p => p.CodeVal == carType).FirstOrDefault();
            //    if (carTypeObj == null)
            //        cell_CarType.Text = string.Empty;
            //    else
            //        cell_CarType.Text = cultureIndex == 1 ? carTypeObj.CodeName : (cultureIndex == 2 ? carTypeObj.CodeNameENG : carTypeObj.CodeNameCHN);
            //}
        }

        private void Cell_WhPosition_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var positionCode = cell_WhPosition.Text;
            if (positionCode.IsNullOrEmpty())
                cell_WhPosition.Text = string.Empty;
            else
            {
                var positionObj = PositionList.Where(p => p.PositionCode == positionCode).FirstOrDefault();
                if (positionObj == null)
                    cell_WhPosition.Text = string.Empty;
                else
                    cell_WhPosition.Text = positionObj.PositionName;
            }
        }

        private void Cell_BarCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var value = cell_BarCode.Text;
            if (value.IsNullOrEmpty())
            {
                cell_BarCode.Text = string.Empty;
                xrLabel3.Text = string.Empty;
            }
            else
            {
                //cell_BarCode.Controls.Add(CreateQRCodeBarCode(value));
            }
        }

        private XRBarCode CreateQRCodeBarCode(string BarCodeText)
        {
            // Create a bar code control.
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to QRCode.
            barCode.Symbology = new QRCodeGenerator();

            // Adjust the bar code's main properties.
            barCode.Text = BarCodeText;
            barCode.Width = 58;
            barCode.Height = 40;

            // If the AutoModule property is set to false, uncomment the next line.
            barCode.AutoModule = true;
            // barcode.Module = 3;

            // Adjust the properties specific to the bar code type.
            ((QRCodeGenerator)barCode.Symbology).CompactionMode = QRCodeCompactionMode.AlphaNumeric;
            ((QRCodeGenerator)barCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            ((QRCodeGenerator)barCode.Symbology).Version = QRCodeVersion.AutoVersion;
            barCode.ShowText = false;

            return barCode;
        }
    }
}


                



