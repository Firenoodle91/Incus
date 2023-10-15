using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using HKInc.Service.Service;
using System.Collections.Generic;

/// <summary>
/// 원자재 수입 검사 성적서 양식
/// </summary>
/// 
namespace HKInc.Ui.View.View.REPORT
{
    public partial class XRQCT1003 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        private int cultureIndex;
        private List<TN_STD1000> checkWayList;

        public XRQCT1003()
        {
            InitializeComponent();
            xrLabel1.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
        }

        public XRQCT1003(TN_QCT1100 obj) : this()
        {
            bindingSource1.DataSource = obj;
            xrLabel1.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
            cultureIndex = DataConvert.GetCultureIndex();

            checkWayList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay);

            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_CheckWay.BeforePrint += cell_CheckWay_BeforePrint;

            cell_InputCustomer.Text = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == obj.CustomerCode).FirstOrDefault().CustomerName;
            cell_Weight.Text = ( obj.TN_STD1100.Weight == null || obj.TN_STD1100.Weight == 0 )? string.Empty : obj.TN_STD1100.Weight.GetDecimalNullToZero().ToString("#,0.#####");
            cell_Spec.Text = obj.TN_STD1100.CombineSpec;
            cell_LotNo.Text = obj.InLotNo;
            cell_InspDate.Text = obj.CheckDate.Value.ToShortDateString();
            cell_InspId.Text = ModelService.GetChildList<User>(p => p.LoginId == obj.CheckId).FirstOrDefault().UserName;
            cell_Memo.Text = obj.Memo;
            cell_Result.Text = obj.CheckResult == "OK" ? "합격" : "불합격";

            var listCount = obj.TN_QCT1101List.Count;
            int printRowCnt = 20;

            var valueCount = listCount / printRowCnt;
            var modCount = listCount % printRowCnt;

            if (modCount > 0)
            {
                var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                while (true)
                {
                    obj.TN_QCT1101List.Add(new TN_QCT1101()
                    {
                        InspNo = obj.InspNo,
                        InspSeq = -1
                    });

                    if (obj.TN_QCT1101List.Count == checkCount) break;
                }
            }

        }

        private void Cell_No_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (cell_No.Text == "-1")
                cell_No.Text = string.Empty;
        }

        private void cell_CheckWay_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var checkWay = cell_CheckWay.Text;
            if (checkWay.IsNullOrEmpty())
                cell_CheckWay.Text = string.Empty;
            else
            {
                var checkWayObj = checkWayList.Where(p => p.CodeVal == checkWay).FirstOrDefault();
                if (checkWayObj == null)
                    cell_CheckWay.Text = string.Empty;
                else
                    cell_CheckWay.Text = cultureIndex == 1 ? checkWayObj.CodeName : (cultureIndex == 2 ? checkWayObj.CodeNameENG : checkWayObj.CodeNameCHN);
            }
        }
    }
}
