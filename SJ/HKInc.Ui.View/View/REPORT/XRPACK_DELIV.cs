using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Linq;
using HKInc.Utils.Class;
using System.Collections.Generic;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 포장라벨출력/박스라벨출력 A4
    /// </summary>
    public partial class XRPACK_DELIV : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRPACK_DELIV()
        {
            InitializeComponent();

            for (int i = 1; i <= 10; i++)
            {
                FindControl("xrBarCode" + i.ToString(), true).Visible = false;
            }
        }

        public XRPACK_DELIV(TEMP_XFPOP_PACK TEMP_XFPOP_PACK, int perBoxQty, string printDate, int printQty, string customerLotNo, string itemNm) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
            var itemCode = itemObj.CustomerItemCode;
            //var itemName = itemObj.CustomerItemName;
            var itemName = itemNm;

            string checkName = string.Empty;
            var LastFinalCheckObj = ModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP_PACK.WorkNo 
                                                                            && p.CheckDivision == MasterCodeSTR.InspectionDivision_Final
                                                                            && p.ProductLotNo == TEMP_XFPOP_PACK.ProductLotNo
                                                                            && p.Temp1 == TEMP_XFPOP_PACK.ItemMoveNo
                                                                            )
                                                                            .OrderBy(p => p.RowId)
                                                                            .LastOrDefault();
            if(LastFinalCheckObj != null)
            {
                checkName = ModelService.GetChildList<User>(p => p.LoginId == LastFinalCheckObj.CheckId).First().Description;
            }

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CustomerName" + i.ToString(), true).Text = "(주)선진스프링";
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("cell_ProductLotNo" + i.ToString(), true).Text = customerLotNo.IsNullOrEmpty() ? TEMP_XFPOP_PACK.ProductLotNo : customerLotNo;
                FindControl("xrBarCode" + i.ToString(), true).Text = TEMP_XFPOP_PACK.ProductLotNo;
                FindControl("xrBarCode" + i.ToString(), true).Visible = true;
                FindControl("lblDate" + i.ToString(), true).Text = DateTime.Today.ToString("yyyy.MM.dd");
                FindControl("lblWorker" + i.ToString(), true).Text = checkName;
                //FindControl("lblDate" + i.ToString(), true).Font = new Font("맑은 고딕", 9.75f, FontStyle.Bold);
                //FindControl("lblWorker" + i.ToString(), true).Font = new Font("맑은 고딕", 9.75f, FontStyle.Bold);
                FindControl("lblWorker" + i.ToString(), true).WordWrap = false;
            }
        }
        
        public void SetBinding(TEMP_XFPOP_PACK TEMP_XFPOP_PACK, int perBoxQty, string printDate, int printQty, string customerLotNo, string itemNm)
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
            var itemCode = itemObj.CustomerItemCode;
            //var itemName = itemObj.CustomerItemName;
            var itemName = itemNm;

            string checkName = string.Empty;
            var LastFinalCheckObj = ModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP_PACK.WorkNo
                                                                            && p.CheckDivision == MasterCodeSTR.InspectionDivision_Final
                                                                            && p.ProductLotNo == TEMP_XFPOP_PACK.ProductLotNo
                                                                            && p.Temp1 == TEMP_XFPOP_PACK.ItemMoveNo
                                                                            )
                                                                            .OrderBy(p => p.RowId)
                                                                            .LastOrDefault();
            if (LastFinalCheckObj != null)
            {
                checkName = ModelService.GetChildList<User>(p => p.LoginId == LastFinalCheckObj.CheckId).First().Description;
            }

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CustomerName" + i.ToString(), true).Text = "(주)선진스프링";
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("cell_ProductLotNo" + i.ToString(), true).Text = customerLotNo.IsNullOrEmpty() ? TEMP_XFPOP_PACK.ProductLotNo : customerLotNo;
                FindControl("xrBarCode" + i.ToString(), true).Text = TEMP_XFPOP_PACK.ProductLotNo;
                FindControl("xrBarCode" + i.ToString(), true).Visible = true;
                FindControl("lblDate" + i.ToString(), true).Text = DateTime.Today.ToString("yyyy.MM.dd");
                FindControl("lblWorker" + i.ToString(), true).Text = checkName;
                //FindControl("lblDate" + i.ToString(), true).Font = new Font("맑은 고딕", 9.75f, FontStyle.Bold);
                //FindControl("lblWorker" + i.ToString(), true).Font = new Font("맑은 고딕", 9.75f, FontStyle.Bold);
                FindControl("lblWorker" + i.ToString(), true).WordWrap = false;
            }
        }

        public XRPACK_DELIV(VI_MPS1800_LIST VI_MPS1800_LIST, int perBoxQty, string printDate, int printQty, string customerLotNo, string itemNm) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();
            
            var itemCode = VI_MPS1800_LIST.TN_STD1100.CustomerItemCode;
            var itemName = itemNm; //cultureIndex == 1 ? VI_MPS1800_LIST.TN_STD1100.ItemName : (cultureIndex == 2 ? VI_MPS1800_LIST.TN_STD1100.ItemNameENG : VI_MPS1800_LIST.TN_STD1100.ItemNameCHN);
            var carType = VI_MPS1800_LIST.TN_STD1100.CarType;

            string checkName = string.Empty;
            var LastFinalCheckObj = ModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == VI_MPS1800_LIST.WorkNo
                                                                            && p.CheckDivision == MasterCodeSTR.InspectionDivision_Final
                                                                            && p.ProductLotNo == VI_MPS1800_LIST.ProductLotNo
                                                                            //&& p.Temp1 == VI_MPS1800_LIST.ItemMoveNo
                                                                            )
                                                                            .OrderBy(p => p.RowId)
                                                                            .LastOrDefault();
            if (LastFinalCheckObj != null)
            {
                checkName = ModelService.GetChildList<User>(p => p.LoginId == LastFinalCheckObj.CheckId).First().Description;
            }

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CustomerName" + i.ToString(), true).Text = "(주)선진스프링";
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("cell_ProductLotNo" + i.ToString(), true).Text = customerLotNo.IsNullOrEmpty() ? VI_MPS1800_LIST.ProductLotNo : customerLotNo;
                FindControl("xrBarCode" + i.ToString(), true).Text = VI_MPS1800_LIST.ProductLotNo;
                FindControl("xrBarCode" + i.ToString(), true).Visible = true;
                FindControl("lblDate" + i.ToString(), true).Text = DateTime.Today.ToString("yyyy.MM.dd");
                FindControl("lblWorker" + i.ToString(), true).Text = checkName;
                //FindControl("lblDate" + i.ToString(), true).Font = new Font("맑은 고딕", 9.75f, FontStyle.Bold);
                //FindControl("lblWorker" + i.ToString(), true).Font = new Font("맑은 고딕", 9.75f, FontStyle.Bold);
                FindControl("lblWorker" + i.ToString(), true).WordWrap = false;
            }
        }

        public void SetBinding(VI_MPS1800_LIST VI_MPS1800_LIST, int perBoxQty, string printDate, int printQty, string customerLotNo, string itemNm)
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var itemCode = VI_MPS1800_LIST.TN_STD1100.CustomerItemCode;
            var itemName = itemNm; //cultureIndex == 1 ? VI_MPS1800_LIST.TN_STD1100.ItemName : (cultureIndex == 2 ? VI_MPS1800_LIST.TN_STD1100.ItemNameENG : VI_MPS1800_LIST.TN_STD1100.ItemNameCHN);
            var carType = VI_MPS1800_LIST.TN_STD1100.CarType.GetNullToEmpty();

            string checkName = string.Empty;
            var LastFinalCheckObj = ModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == VI_MPS1800_LIST.WorkNo
                                                                            && p.CheckDivision == MasterCodeSTR.InspectionDivision_Final
                                                                            && p.ProductLotNo == VI_MPS1800_LIST.ProductLotNo
                                                                            //&& p.Temp1 == TEMP_XFPOP_PACK.ItemMoveNo
                                                                            )
                                                                            .OrderBy(p => p.RowId)
                                                                            .LastOrDefault();
            if (LastFinalCheckObj != null)
            {
                checkName = ModelService.GetChildList<User>(p => p.LoginId == LastFinalCheckObj.CheckId).First().Description;
            }

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CustomerName" + i.ToString(), true).Text = "(주)선진스프링";
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("cell_ProductLotNo" + i.ToString(), true).Text = customerLotNo.IsNullOrEmpty() ? VI_MPS1800_LIST.ProductLotNo : customerLotNo;
                FindControl("xrBarCode" + i.ToString(), true).Text = VI_MPS1800_LIST.ProductLotNo;
                FindControl("xrBarCode" + i.ToString(), true).Visible = true;
                FindControl("lblDate" + i.ToString(), true).Text = DateTime.Today.ToString("yyyy.MM.dd");
                FindControl("lblWorker" + i.ToString(), true).Text = checkName;
                //FindControl("lblDate" + i.ToString(), true).Font = new Font("맑은 고딕", 9.75f, FontStyle.Bold);
                //FindControl("lblWorker" + i.ToString(), true).Font = new Font("맑은 고딕", 9.75f, FontStyle.Bold);
                FindControl("lblWorker" + i.ToString(), true).WordWrap = false;
            }
        }

        public XRPACK_DELIV(TN_STD1100 TN_STD1100_Obj, string itemCode, string itemName, string lotNo, int perBoxQty, int printQty, string customerName, string printDate) : this()
        {
            string checkName = string.Empty;
            checkName = ModelService.GetChildList<User>(p => p.LoginId == GlobalVariable.LoginId).First().Description;

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CustomerName" + i.ToString(), true).Text = customerName;
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("cell_ProductLotNo" + i.ToString(), true).Text = lotNo;
                FindControl("xrBarCode" + i.ToString(), true).Text = lotNo;
                FindControl("xrBarCode" + i.ToString(), true).Visible = true;
                FindControl("lblDate" + i.ToString(), true).Text = DateTime.Today.ToString("yyyy.MM.dd");
                FindControl("lblWorker" + i.ToString(), true).Text = checkName;
                FindControl("lblWorker" + i.ToString(), true).WordWrap = false;

                ((XRTableCell)FindControl("cell_ItemCode" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRTableCell)FindControl("cell_ItemName" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRTableCell)FindControl("cell_CustomerName" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRTableCell)FindControl("cell_Qty" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRTableCell)FindControl("cell_PrintDate" + i.ToString(), true)).EditOptions.Enabled = true;
                //((XRTableCell)FindControl("cell_ProductLotNo" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRLabel)FindControl("lblDate" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRLabel)FindControl("lblWorker" + i.ToString(), true)).EditOptions.Enabled = true;                
            }
        }

        public void SetBinding(TN_STD1100 TN_STD1100_Obj, string itemCode, string itemName, string lotNo, int perBoxQty, int printQty, string customerName, string printDate)
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            string checkName = string.Empty;
            checkName = ModelService.GetChildList<User>(p => p.LoginId == GlobalVariable.LoginId).First().Description;

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CustomerName" + i.ToString(), true).Text = customerName;
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("cell_ProductLotNo" + i.ToString(), true).Text = lotNo;
                FindControl("xrBarCode" + i.ToString(), true).Text = lotNo;
                FindControl("xrBarCode" + i.ToString(), true).Visible = true;
                FindControl("lblDate" + i.ToString(), true).Text = DateTime.Today.ToString("yyyy.MM.dd");
                FindControl("lblWorker" + i.ToString(), true).Text = checkName;
                FindControl("lblWorker" + i.ToString(), true).WordWrap = false;

                ((XRTableCell)FindControl("cell_ItemCode" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRTableCell)FindControl("cell_ItemName" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRTableCell)FindControl("cell_CustomerName" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRTableCell)FindControl("cell_Qty" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRTableCell)FindControl("cell_PrintDate" + i.ToString(), true)).EditOptions.Enabled = true;
                //((XRTableCell)FindControl("cell_ProductLotNo" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRLabel)FindControl("lblDate" + i.ToString(), true)).EditOptions.Enabled = true;
                ((XRLabel)FindControl("lblWorker" + i.ToString(), true)).EditOptions.Enabled = true;
            }
        }
    }
}
