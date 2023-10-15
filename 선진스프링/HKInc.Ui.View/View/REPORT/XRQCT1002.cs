using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Collections.Generic;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Class;
using System.Linq;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 성적서 출력 양식3
    /// </summary>
    /// 
    public partial class XRQCT1002 : DevExpress.XtraReports.UI.XtraReport
    {
        private int rowNumber = 0;

        IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        //private List<TN_STD1000> checkList;
        private List<TN_STD1000> checkWayList;
        private int cultureIndex;

        public XRQCT1002()
        {
            InitializeComponent();
        }

        public XRQCT1002(TN_ORD1201 obj) : this()
        {   checkWayList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay);

            cultureIndex = DataConvert.GetCultureIndex();

            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_CheckWayList.BeforePrint += cell_CheckWayList_BeforePrint;

            cell_CustomerName.Text = obj.TN_ORD1200.TN_STD1400.CustomerName;
            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);
            cell_CarType.Text = obj.TN_STD1100.CarType;
            cell_Qty.Text = obj.OutQty.ToString("#,#.##");
            cell_LotNo.Text = obj.ProductLotNo;

            //가장 최근에 한 출하검사 데이터
            var inspectData = ModelService.GetList(p => p.CheckDivision == MasterCodeSTR.InspectionDivision_Shipment && p.ItemCode == obj.ItemCode).OrderBy(p => p.CheckDate).ThenBy(p => p.InspNo).LastOrDefault();

            if (inspectData != null)
            {
                cell_InspectDate.Text = inspectData.CheckDate.Value.ToShortDateString();
                cell_Result.Text = inspectData.CheckResult;

                var listCount = inspectData.TN_QCT1101List.Count;
                int printRowCnt = 20;

                var valueCount = listCount / printRowCnt;
                var modCount = listCount % printRowCnt;

                if (modCount > 0)
                {
                    var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                    while (true)
                    {
                        inspectData.TN_QCT1101List.Add(new TN_QCT1101()
                        {
                            InspNo = inspectData.InspNo,
                            InspSeq = -1
                        });

                        if (inspectData.TN_QCT1101List.Count == checkCount) break;
                    }
                }

                DetailReport.DataSource = inspectData.TN_QCT1101List;

                cell_No.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InspSeq]") });
                cell_No.TextFormatString = "{0:n0}";

                cell_CheckList.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TN_QCT1001].[CheckList]") });
                cell_CheckWayList.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TN_QCT1001].[CheckWay]") });
                cell_CheckSpec.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TN_QCT1001].[InspectionReportMemo]") });
                cell_Reading1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading1]") });
                cell_Reading2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading2]") });
                cell_Reading3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading3]") });
                cell_Reading4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading4]") });
                cell_Reading5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading5]") });
                cell_Reading6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading6]") });
                cell_Reading7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading7]") });
                cell_Reading8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading8]") });
                cell_Reading9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading9]") });
                cell_OkNg.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Judge]") });
            }

        }

        public XRQCT1002(TN_QCT1500 obj) : this()
        {
            checkWayList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay);

            cultureIndex = DataConvert.GetCultureIndex();

            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_CheckWayList.BeforePrint += cell_CheckWayList_BeforePrint;

            cell_CustomerName.Text = obj.TN_STD1400.CustomerName;
            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);
            cell_CarType.Text = obj.TN_STD1100.CarType;
            cell_LotNo.Text = ModelService.GetChildList<TN_MPS1201>(p => p.ProductLotNo == obj.ProductLotNo).First().TN_MPS1200.Temp1.GetNullToEmpty(); //obj.ProductLotNo;
            cell_Qty.Text = obj.OutQty.GetDecimalNullToZero().ToString("N0");

            cell_InspectDate.Text = obj.CheckDate.Value.ToShortDateString();
            cell_Result.Text = obj.CheckResult;

            var listCount = obj.TN_QCT1501List.Count;
            int printRowCnt = 20;

            var valueCount = listCount / printRowCnt;
            var modCount = listCount % printRowCnt;

            if (modCount > 0)
            {
                var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                while (true)
                {
                    obj.TN_QCT1501List.Add(new TN_QCT1501()
                    {
                        InspNo = obj.InspNo,
                        InspSeq = -1
                    });

                    if (obj.TN_QCT1501List.Count == checkCount) break;
                }
            }

            DetailReport.DataSource = obj.TN_QCT1501List.Where(p => (p.TN_QCT1001 != null && p.TN_QCT1001.InspectionReportFlag == "Y") || p.InspSeq == -1).ToList();
            //DetailReport.DataSource = obj.TN_QCT1501List;

            cell_No.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InspSeq]") });
            cell_No.TextFormatString = "{0:n0}";

            cell_CheckList.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TN_QCT1001].[CheckList]") });
            cell_CheckWayList.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TN_QCT1001].[CheckWay]") });
            cell_CheckSpec.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TN_QCT1001].[InspectionReportMemo]") });
            cell_Reading1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading1]") });
            cell_Reading2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading2]") });
            cell_Reading3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading3]") });
            cell_Reading4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading4]") });
            cell_Reading5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading5]") });
            cell_Reading6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading6]") });
            cell_Reading7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading7]") });
            cell_Reading8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading8]") });
            cell_Reading9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Reading9]") });
            cell_OkNg.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Judge]") });

        }

        private void Cell_No_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (cell_No.Text.IsNullOrEmpty() || cell_No.Text == "-1")
                cell_No.Text = string.Empty;
            else
            {
                rowNumber++;
                cell_No.Text = rowNumber.ToString("n0");
            }
            //if (cell_No.Text == "-1")
            //    cell_No.Text = string.Empty;
        }

        private void cell_CheckWayList_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var checkWay = cell_CheckWayList.Text;
            if (checkWay.IsNullOrEmpty())
                cell_CheckWayList.Text = string.Empty;
            else
            {
                var checkWayObj = checkWayList.Where(p => p.CodeVal == checkWay).FirstOrDefault();
                if (checkWayObj == null)
                    cell_CheckWayList.Text = string.Empty;
                else
                    cell_CheckWayList.Text = cultureIndex == 1 ? checkWayObj.CodeName : (cultureIndex == 2 ? checkWayObj.CodeNameENG : checkWayObj.CodeNameCHN);
            }
        }

    }
}
