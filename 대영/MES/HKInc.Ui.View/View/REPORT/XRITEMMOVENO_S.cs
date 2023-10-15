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
    /// 이동표 출력 양식(10*10)
    /// </summary>
    public partial class XRITEMMOVENO_S : DevExpress.XtraReports.UI.XtraReport
    {
        public XRITEMMOVENO_S()
        {
            InitializeComponent();
        }

        public XRITEMMOVENO_S(TEMP_ITEM_MOVE_NO_MASTER masterObj, List<TEMP_ITEM_MOVE_NO_DETAIL> detailList) : this()
        {
            IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");

            var cultureIndex = DataConvert.GetCultureIndex();
            var TN_STD1400 = ModelService.GetList(p => p.CustomerCode == masterObj.CustomerCode).FirstOrDefault();
            var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            var userList = ModelService.GetChildList<User>(p => true).ToList();
            var SurfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);

            cell_WorkNo.Text = masterObj.WorkNo;
            cell_LotNo.Text = masterObj.ProductLotNo;

            cell_ItemName.Text = cultureIndex == 1 ? masterObj.ItemName : (cultureIndex == 2 ? masterObj.ItemNameENG : masterObj.ItemNameCHN);
            cell_CustName.Text = TN_STD1400 == null ? string.Empty : (cultureIndex == 1 ? TN_STD1400.CustomerName : (cultureIndex == 2 ? TN_STD1400.CustomerNameENG : TN_STD1400.CustomerNameCHN));

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).First();
            xrBarCode1.Text = masterObj.ItemMoveNo.GetNullToEmpty();
            xrLabel2.Text = masterObj.ItemMoveNo.GetNullToEmpty();
            ILabelConvert LabelConvert = HelperFactory.GetLabelConvert();
            int i = 0;
            foreach (var v in detailList)
            {
                if (i == 6) break;

                if(i == 0)
                {
                    cell_MachineName.Text = v.MachineName.GetNullToEmpty();
                }

                var processObj = processList.Where(p => p.CodeVal == v.ProcessCode).FirstOrDefault();
                var userObj = userList.Where(p => p.LoginId == v.WorkId).FirstOrDefault();

                XRTableCell cell_Proc = (XRTableCell)xrTable1.FindControl("cell_Proc" + i, true);
                XRTableCell cell_Qty = (XRTableCell)xrTable1.FindControl("cell_Qty" + i, true);
                XRTableCell cell_WorkId = (XRTableCell)xrTable1.FindControl("cell_WorkId" + i, true);
                XRTableCell cell_WorkDate = (XRTableCell)xrTable1.FindControl("cell_WorkDate" + i, true);

                if (v.OutProcFlag == "Y")
                {
                    cell_Proc.Text = processObj == null ? string.Empty : (cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN)) + "(" + LabelConvert.GetLabelText("Outsourcing") + ")";
                }
                else
                {
                    cell_Proc.Text = processObj == null ? string.Empty : (cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN));
                }

                cell_Qty.Text = v.OkQty == null ? string.Empty : ((decimal)v.OkQty).ToString("#,#.##");
                cell_WorkId.Text = userObj == null ? string.Empty : userObj.UserName;
                //cell_WorkDate.Text = v.ResultStartDate == null ? string.Empty : ((DateTime)v.ResultStartDate).ToString("MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                cell_WorkDate.Text = v.ResultDate == null ? string.Empty : ((DateTime)v.ResultDate).ToString("MM/dd", System.Globalization.CultureInfo.InvariantCulture);

                i++;
            }
        }
    }
}
