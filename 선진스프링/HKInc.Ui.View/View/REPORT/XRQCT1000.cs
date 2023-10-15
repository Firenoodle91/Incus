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
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using System.Collections.Generic;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 성적서 출력 양식1
    /// </summary>
    /// 
    public partial class XRQCT1000 : DevExpress.XtraReports.UI.XtraReport
    {
        private int rowNumber = 0;

        IService<TN_QCT1500> ModelService = (IService<TN_QCT1500>)ProductionFactory.GetDomainService("TN_QCT1500");

        private int cultureIndex;

        public XRQCT1000()
        {
            InitializeComponent();
        }

        public XRQCT1000(TN_ORD1201 obj) : this()
        {
            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_Avg.BeforePrint += cell_Avg_BeforePrint;

            cultureIndex = DataConvert.GetCultureIndex();

            cell_SalesDate.Text = obj.TN_ORD1200.OutReqDate == null ? DateTime.Today.ToShortDateString() : ((DateTime)obj.TN_ORD1200.OutReqDate).ToShortDateString();
            cell_CustomerName.Text = obj.TN_ORD1200.TN_STD1400.CustomerName;
            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);
            cell_Qty.Text = obj.OutQty.ToString("#,#.##");
            cell_LotNo.Text = obj.ProductLotNo;

            //가장 최근에 한 출하검사 데이터
            var inspectData = ModelService.GetList(p => p.ItemCode == obj.ItemCode).OrderBy(p => p.CheckDate).ThenBy(p => p.InspNo).LastOrDefault();

            if(inspectData != null)
            {
                cell_InspectDate.Text = inspectData.CheckDate.Value.ToShortDateString();
                cell_InspectUserName.Text = ModelService.GetChildList<User>(p => p.Active == "Y").Where(p => p.LoginId == inspectData.CheckId).FirstOrDefault().UserName;
                cell_Result.Text = inspectData.CheckResult == "OK" ? "합격" : "불합격";
                List<TN_QCT1501> tn1501 = inspectData.TN_QCT1501List.Where(p => (p.TN_QCT1001 != null && p.TN_QCT1001.InspectionReportFlag == "Y")).ToList();
                var listCount = tn1501.Count;
              //  var listCount = inspectData.TN_QCT1501List.Count;
                int printRowCnt = 16;

                var valueCount = listCount / printRowCnt;
                var modCount = listCount % printRowCnt;

                if (modCount > 0)
                {
                    var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                    while (true)
                    {
                        tn1501.Add(new TN_QCT1501()
                        {
                            InspNo = inspectData.InspNo,
                            InspSeq = -1
                        });

                        if (tn1501.Count == checkCount) break;
                    }
                }

                DetailReport.DataSource = tn1501;//
                //if (modCount > 0)
                //{
                //    var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                //    while (true)
                //    {
                //        inspectData.TN_QCT1501List.Add(new TN_QCT1501()
                //        {
                //            InspNo = inspectData.InspNo,
                //            InspSeq = -1
                //        });

                //        if (inspectData.TN_QCT1501List.Count == checkCount) break;
                //    }
                //}

                //DetailReport.DataSource = inspectData.TN_QCT1501List.Where(p => (p.TN_QCT1001 != null && p.TN_QCT1001.InspectionReportFlag == "Y") || p.InspSeq == -1).ToList();
                //DetailReport.DataSource = inspectData.TN_QCT1501List;

                cell_No.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InspSeq]") });
                cell_No.TextFormatString = "{0:n0}";

                cell_CheckList.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TN_QCT1001].[CheckList]") });
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
                cell_Avg.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "") });
            }

        }

        public XRQCT1000(TN_QCT1500 obj) : this()
        {
            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_Avg.BeforePrint += cell_Avg_BeforePrint;

            cultureIndex = DataConvert.GetCultureIndex();

            cell_SalesDate.Text = obj.PrintDate == null ? DateTime.Today.ToShortDateString() : ((DateTime)obj.PrintDate).ToShortDateString();
            cell_CustomerName.Text = obj.TN_STD1400.CustomerName;
            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);
            cell_LotNo.Text = ModelService.GetChildList<TN_MPS1201>(p => p.ProductLotNo == obj.ProductLotNo).First().TN_MPS1200.Temp1.GetNullToEmpty(); //obj.ProductLotNo;
            cell_Qty.Text = obj.OutQty.GetDecimalNullToZero().ToString("N0");

            #region 성적서샘플링 검사수량 가져오기
            var samplyQty = (decimal) 0;

            ////검사 마스터
            //var qct1100Obj = ModelService.GetChildList<TN_QCT1100>(p => p.InspNo == obj.FinalInspNo).FirstOrDefault();

            //if (qct1100Obj != null)
            //{
            //    //작업실적관리 마스터
            //    var mps1201Obj = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == qct1100Obj.WorkNo && p.ProcessCode == qct1100Obj.ProcessCode && p.ProcessSeq == qct1100Obj.WorkSeq).FirstOrDefault();  

            //    if(mps1201Obj != null)
            //    {
            //        //성적서샘플링관리
            //        var qct1600ObjList = ModelService.GetChildList<TN_QCT1600>(p => p.AQL == mps1201Obj.TN_STD1100.AQL && p.CustomerCode == mps1201Obj.CustomerCode).ToList();

            //        if(qct1600ObjList.Count > 0)
            //        {
            //            foreach(var list in qct1600ObjList)
            //            {
            //                if (list.MinValue <= mps1201Obj.OkSumQty && mps1201Obj.OkSumQty <= list.MaxValue)
            //                {
            //                    samplyQty = list.CheckQty.GetDecimalNullToZero();
            //                    break;
            //                }
            //            }
            //        }

            //    }
            //}

            //성적서샘플링관리
            var qct1600ObjList = ModelService.GetChildList<TN_QCT1600>(p => p.AQL == obj.TN_STD1100.AQL).ToList();
            var qty = obj.OutQty.GetDecimalNullToZero();
            if (qct1600ObjList.Count > 0)
            {
                foreach (var list in qct1600ObjList)
                {
                    if (list.MinValue <= qty && qty <= list.MaxValue)
                    {
                        samplyQty = list.CheckQty.GetDecimalNullToZero();
                        break;
                    }
                }
            }
            cell_SampleQty.Text = samplyQty > 0 ? "n = " + samplyQty.ToString("N0") : "n = 200";
            #endregion

            if (obj != null)
            {
                cell_InspectDate.Text = obj.CheckDate.Value.ToShortDateString();
                cell_InspectUserName.Text = ModelService.GetChildList<User>(p => p.Active == "Y").Where(p => p.LoginId == obj.CheckId).FirstOrDefault().UserName;
                cell_Result.Text = obj.CheckResult == "OK" ? "합격" : "불합격";

                List<TN_QCT1501> tn1501 = obj.TN_QCT1501List.Where(p => (p.TN_QCT1001 != null && p.TN_QCT1001.InspectionReportFlag == "Y")).ToList();
                var listCount = tn1501.Count;
                int printRowCnt = 16;

                var valueCount = listCount / printRowCnt;
                var modCount = listCount % printRowCnt;

                if (modCount > 0)
                {
                    var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                    while (true)
                    {
                        tn1501.Add(new TN_QCT1501()
                        {
                            InspNo = obj.InspNo,
                            InspSeq = -1
                        });

                        if (tn1501.Count == checkCount) break;
                    }
                }

                DetailReport.DataSource = tn1501;// obj.TN_QCT1501List.Where(p => (p.TN_QCT1001 != null && p.TN_QCT1001.InspectionReportFlag == "Y") || p.InspSeq == -1).ToList();

                cell_No.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InspSeq]") });
                cell_No.TextFormatString = "{0:n0}";

                cell_CheckList.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TN_QCT1001].[CheckList]") });
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
                cell_Avg.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "") });
            }

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

        private void cell_Avg_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int valCnt = 0;
            decimal valSum = 0;
            decimal parseDecimal;
            string[] val = new string[9];

            val[0] = cell_Reading1.Text;  
            val[1] = cell_Reading2.Text;
            val[2] = cell_Reading3.Text;
            val[3] = cell_Reading4.Text;
            val[4] = cell_Reading5.Text;
            val[5] = cell_Reading6.Text;
            val[6] = cell_Reading7.Text;
            val[7] = cell_Reading8.Text;
            val[8] = cell_Reading9.Text;

            for (int i = 0; i < val.Length; i++)
            {
                if (!val[i].IsNullOrEmpty() && decimal.TryParse(val[i], out parseDecimal))
                {
                    valSum += val[i].GetNullToZero();
                    valCnt++;
                }
            }

            if (valSum == 0 || valCnt == 0) cell_Avg.Text = string.Empty;
            else cell_Avg.Text = string.Format("{0:n3}", (valSum.GetNullToZero() / valCnt.GetNullToZero()).GetDecimalNullToZero());
        }
    }
}
