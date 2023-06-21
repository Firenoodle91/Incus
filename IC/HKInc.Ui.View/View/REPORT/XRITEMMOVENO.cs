using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain.TEMP;
using System.Collections.Generic;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Linq;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Helper;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 이동표 출력 양식
    /// </summary>
    public partial class XRITEMMOVENO : DevExpress.XtraReports.UI.XtraReport
    {
        XRLabel[][] lblList; 

        public XRITEMMOVENO()
        {
            InitializeComponent();
            xrLabel11.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
        }

        public XRITEMMOVENO(TEMP_ITEM_MOVE_NO_MASTER masterObj, List<TEMP_ITEM_MOVE_NO_DETAIL> detailList) : this()
        {
            IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");

            lblList = new XRLabel[][] 
            {
                new XRLabel[] {  proc0, wkqty0, okqty0, work0, lbl_ResultStartDate0, lbl_ResultEndDate0 },
                new XRLabel[] {  proc1, wkqty1, okqty1, work1, lbl_ResultStartDate1, lbl_ResultEndDate1 },
                new XRLabel[] {  proc2, wkqty2, okqty2, work2, lbl_ResultStartDate2, lbl_ResultEndDate2 },
                new XRLabel[] {  proc3, wkqty3, okqty3, work3, lbl_ResultStartDate3, lbl_ResultEndDate3 },
                new XRLabel[] {  proc4, wkqty4, okqty4, work4, lbl_ResultStartDate4, lbl_ResultEndDate4 },
                new XRLabel[] {  proc5, wkqty5, okqty5, work5, lbl_ResultStartDate5, lbl_ResultEndDate5 },
                new XRLabel[] {  proc6, wkqty6, okqty6, work6, lbl_ResultStartDate6, lbl_ResultEndDate6 },
                new XRLabel[] {  proc7, wkqty7, okqty7, work7, lbl_ResultStartDate7, lbl_ResultEndDate7 },
                new XRLabel[] {  proc8, wkqty8, okqty8, work8, lbl_ResultStartDate8, lbl_ResultEndDate8 },
                new XRLabel[] {  proc9, wkqty9, okqty9, work9, lbl_ResultStartDate9, lbl_ResultEndDate9 }
            };

            var cultureIndex = DataConvert.GetCultureIndex();
            var TN_STD1400 = ModelService.GetList(p => p.CustomerCode == masterObj.CustomerCode).FirstOrDefault();
            var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            var userList = ModelService.GetChildList<User>(p => true).ToList();
            var SurfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);

            bar_ItemMoveNo.Text = masterObj.ItemMoveNo;
            bar_WorkNo.Text = masterObj.WorkNo;
            bar_ProductLotNo.Text = masterObj.ProductLotNo;

            lbl_ItemCode.Text = masterObj.ItemCode;
            lbl_ItemName.Text = cultureIndex == 1 ? masterObj.ItemName : (cultureIndex == 2 ? masterObj.ItemNameENG : masterObj.ItemNameCHN);
            lbl_CustomerName.Text = TN_STD1400 == null ? string.Empty : (cultureIndex == 1 ? TN_STD1400.CustomerName : (cultureIndex == 2 ? TN_STD1400.CustomerNameENG : TN_STD1400.CustomerNameCHN));
            lbl_OrderNo.Text = masterObj.OrderNo;
            lbl_Memo.Text = masterObj.Memo;
            lbl_BoxInQty.Text = masterObj.BoxInQty.GetDecimalNullToZero().ToString("#,0.##");

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).First();

            ILabelConvert LabelConvert = HelperFactory.GetLabelConvert();
            int i = 0;
            foreach (var v in detailList)
            {
                if (i == 10) break;

                var processObj = processList.Where(p => p.CodeVal == v.ProcessCode).FirstOrDefault();
                var userObj = userList.Where(p => p.LoginId == v.WorkId).FirstOrDefault();
                if (v.OutProcFlag == "Y")
                {
                    if (processObj.CodeName.Contains("표면처리"))
                    {
                        var surfaceObj = SurfaceList.Where(p => p.CodeVal == itemObj.SurfaceList).FirstOrDefault();
                        if (surfaceObj != null)
                        {
                            lblList[i][0].Text = processObj == null ? string.Empty : (cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN)) 
                                + "_" + surfaceObj.CodeName + "(" + LabelConvert.GetLabelText("Outsourcing") + ")";
                        }
                        else
                            lblList[i][0].Text = processObj == null ? string.Empty : (cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN)) + "(" + LabelConvert.GetLabelText("Outsourcing") + ")";
                    }
                    else
                        lblList[i][0].Text = processObj == null ? string.Empty : (cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN)) + "(" + LabelConvert.GetLabelText("Outsourcing") + ")";
                }
                else
                {
                    if (processObj.CodeName.Contains("표면처리"))
                    {
                        var surfaceObj = SurfaceList.Where(p => p.CodeVal == itemObj.SurfaceList).FirstOrDefault();
                        if (surfaceObj != null)
                        {
                            lblList[i][0].Text = processObj == null ? string.Empty : (cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN))
                                + "_" + surfaceObj.CodeName;
                        }
                        else
                            lblList[i][0].Text = processObj == null ? string.Empty : (cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN));
                    }
                    else
                        lblList[i][0].Text = processObj == null ? string.Empty : (cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN));
                }
                lblList[i][1].Text = v.OkSumQty == null ? string.Empty : ((decimal)v.OkSumQty).ToString("#,#.##");
                lblList[i][2].Text = v.OkQty == null ? string.Empty : ((decimal)v.OkQty).ToString("#,#.##");
                lblList[i][3].Text = userObj == null ? string.Empty : userObj.UserName;
                lblList[i][4].Text = v.ResultStartDate == null ? string.Empty : ((DateTime)v.ResultStartDate).ToString("yyyy-MM-dd HH:mm:ss");
                lblList[i][5].Text = v.ResultEndDate == null ? string.Empty : ((DateTime)v.ResultEndDate).ToString("yyyy-MM-dd HH:mm:ss");

                i++;
            }
        }
    }
}
