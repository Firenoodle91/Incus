using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Helper;
using System.Linq;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 성적서 출력 양식2
    /// </summary>
    /// 
    public partial class XRQCT1001 : DevExpress.XtraReports.UI.XtraReport
    {
        private int cultureIndex;
        IService<TN_QCT1500> ModelService = (IService<TN_QCT1500>)ProductionFactory.GetDomainService("TN_QCT1500");
        XRLabel[][] lblList1;
        XRLabel[][] lblList2;

        public XRQCT1001()
        {
            InitializeComponent();
        }

        public XRQCT1001(TN_ORD1201 obj) : this()
        {
            cultureIndex = DataConvert.GetCultureIndex();

            cell_printDate.Text = DateTime.Today.ToShortDateString();
            cell_CustomerName.Text = obj.TN_ORD1200.TN_STD1400.CustomerName;
            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);
            cell_Qty.Text = obj.OutQty.ToString("#,#.##");
            cell_LotNo.Text = obj.ProductLotNo;
            cell_DueDate.Text = obj.TN_ORD1200.OutDate.ToShortDateString();

            //가장 최근에 한 출하검사 데이터
            var inspectData = ModelService.GetList(p => p.ItemCode == obj.ItemCode).OrderBy(p => p.CheckDate).ThenBy(p => p.InspNo).LastOrDefault();

            if (inspectData != null)
            {
                cell_InspectDate.Text = inspectData.CheckDate.Value.ToShortDateString();
                cell_Result.Text = inspectData.CheckResult == "OK" ? "합격" : "불합격";

                if (inspectData.TN_QCT1501List.Count > 0)
                {
                    for (int n = 1; n < 8; n++)
                    {
                        XRTableCell cell = (XRTableCell)xrTable1.FindControl("cell_CheckSpec" + n, true);
                        XRLabel label = (XRLabel)Detail.FindControl("Type" + n + "InstNo", true);

                        var listSpct = inspectData.TN_QCT1501List.Where(p => p.CheckList == "0" + n).FirstOrDefault();
                        var listInst = inspectData.TN_QCT1501List.Where(p => p.CheckList == "0" + n).FirstOrDefault();

                        //검사항목별 규격
                        if (listSpct != null) cell.Text = listSpct.TN_QCT1001.InspectionReportMemo;
                        else cell.Text = "";

                        //검사항목별 계측기NO
                        if (listInst != null) label.Text = listInst.TN_QCT1001.InstrumentNo;
                        else label.Text = "";
                    }

                    //검사 값 Label
                    lblList1 = new XRLabel[][]
                    {
                        new XRLabel[] {  Type1X1, Type2X1, Type3X1, Type4X1, Type5X1, Type6X1, Type7X1}, //X1
                        new XRLabel[] {  Type1X2, Type2X2, Type3X2, Type4X2, Type5X2, Type6X2, Type7X2}, //X2
                        new XRLabel[] {  Type1X3, Type2X3, Type3X3, Type4X3, Type5X3, Type6X3, Type7X3}, //X3
                        new XRLabel[] {  Type1X4, Type2X4, Type3X4, Type4X4, Type5X4, Type6X4, Type7X4}, //X4
                        new XRLabel[] {  Type1X5, Type2X5, Type3X5, Type4X5, Type5X5, Type6X5, Type7X5}, //X5
                        new XRLabel[] {  Type1X6, Type2X6, Type3X6, Type4X6, Type5X6, Type6X6, Type7X6}, //X6
                        new XRLabel[] {  Type1X7, Type2X7, Type3X7, Type4X7, Type5X7, Type6X7, Type7X7}, //X7
                        new XRLabel[] {  Type1X8, Type2X8, Type3X8, Type4X8, Type5X8, Type6X8, Type7X8}, //X8
                        new XRLabel[] {  Type1X9, Type2X9, Type3X9, Type4X9, Type5X9, Type6X9, Type7X9}  //X9
                    };

                    //Xbar, R, 계측기NO 값 Label
                    lblList2 = new XRLabel[][]
                    {
                        new XRLabel[] { Type1Xbar, Type2Xbar, Type3Xbar, Type4Xbar, Type5Xbar, Type6Xbar, Type7Xbar}, //Xbar(평균값)
                        new XRLabel[] { Type1R, Type2R, Type3R, Type4R, Type5R, Type6R, Type7R},                      //R(최대 값-최소 값)
                    };

                    string[] sumVal = new string[7];   //검사항목별 검사 값 합계
                    int[] varCnt = new int[7];         //검사항목별 검사 수
                    decimal[] maxVal = new decimal[7]; //검사항목별 최대 값
                    decimal[] minVal = new decimal[7]; //검사항목별 최소 값
                    decimal parseDecimal;              //TryParse out 변수

                    int i = 0;
                    foreach (var v in inspectData.TN_QCT1501List)
                    {
                        if (i == 7) break;

                        lblList1[0][i].Text = v.Reading1;
                        lblList1[1][i].Text = v.Reading2;
                        lblList1[2][i].Text = v.Reading3;
                        lblList1[3][i].Text = v.Reading4;
                        lblList1[4][i].Text = v.Reading5;
                        lblList1[5][i].Text = v.Reading6;
                        lblList1[6][i].Text = v.Reading7;
                        lblList1[7][i].Text = v.Reading8;
                        lblList1[8][i].Text = v.Reading9;


                        //if (v.CheckWay == MasterCodeSTR.InspectionWay_Input) //검사방법-치수검사
                        //{
                        //    //항목별 검사 값 합계
                        //    sumVal[i] = (v.Reading1.GetDecimalNullToZero() + v.Reading2.GetDecimalNullToZero() + v.Reading3.GetDecimalNullToZero()
                        //               + v.Reading4.GetDecimalNullToZero() + v.Reading5.GetDecimalNullToZero() + v.Reading6.GetDecimalNullToZero()
                        //               + v.Reading7.GetDecimalNullToZero() + v.Reading8.GetDecimalNullToZero() + v.Reading9.GetDecimalNullToZero()).GetNullToEmpty();
                        //}
                        //else //검사방법-외관검사
                        //{
                        //    //항목별 검사 값 합계(NG가 1개 이상일 시 NG)
                        //    if (v.Reading1 == "NG" || v.Reading2 == "NG" || v.Reading3 == "NG" || v.Reading4 == "NG" || v.Reading5 == "NG" ||
                        //       v.Reading6 == "NG" || v.Reading7 == "NG" || v.Reading8 == "NG" || v.Reading9 == "NG")

                        //        sumVal[i] = "NG";
                        //    else
                        //        sumVal[i] = "OK";
                        //}

                        if (v.CheckWay == MasterCodeSTR.CheckDataType_C) //검사방법-외관검사
                        {
                            //항목별 검사 값 합계(NG가 1개 이상일 시 NG)
                            if (v.Reading1 == "NG" || v.Reading2 == "NG" || v.Reading3 == "NG" || v.Reading4 == "NG" || v.Reading5 == "NG" ||
                               v.Reading6 == "NG" || v.Reading7 == "NG" || v.Reading8 == "NG" || v.Reading9 == "NG")

                                sumVal[i] = "NG";
                            else
                                sumVal[i] = "OK";

                        }
                        else //검사방법-치수검사
                        {
                            //항목별 검사 값 합계
                            sumVal[i] = (v.Reading1.GetDecimalNullToZero() + v.Reading2.GetDecimalNullToZero() + v.Reading3.GetDecimalNullToZero()
                                       + v.Reading4.GetDecimalNullToZero() + v.Reading5.GetDecimalNullToZero() + v.Reading6.GetDecimalNullToZero()
                                       + v.Reading7.GetDecimalNullToZero() + v.Reading8.GetDecimalNullToZero() + v.Reading9.GetDecimalNullToZero()).GetNullToEmpty();
                        }

                        i++;
                    }

                    //검사항목별 최대 값, 최소 값, 검사 수
                    for (int a = 0; a < lblList1.Length; a++)
                    {
                        for (int b = 0; b < 7; b++)
                        {
                            if(decimal.TryParse(lblList1[a][b].Text, out parseDecimal)) //검사 값이 숫자일 때
                            { 
                                if(minVal[b] == 0)
                                {
                                    minVal[b] = lblList1[a][b].Text.GetDecimalNullToZero();
                                }

                                if (minVal[b].GetDecimalNullToZero() > lblList1[a][b].Text.GetDecimalNullToZero())
                                    minVal[b] = lblList1[a][b].Text.GetDecimalNullToZero();

                                if (maxVal[b].GetDecimalNullToZero() < lblList1[a][b].Text.GetDecimalNullToZero())
                                    maxVal[b] = lblList1[a][b].Text.GetDecimalNullToZero();
                            }

                            if (lblList1[a][b].Text.GetNullToEmpty() != "")
                                varCnt[b] += 1;
                        }
                    }

                    for(int x = 0; x < 7; x++) //검사항목 수 x
                    {
                        // Xbar(평균값)
                        if (!decimal.TryParse(sumVal[x].GetNullToEmpty(), out parseDecimal) || sumVal[x].GetDecimalNullToZero() == 0 || varCnt[x].GetDecimalNullToZero() == 0)
                            lblList2[0][x].Text = "0";
                        else
                            lblList2[0][x].Text = string.Format("{0:n3}", (sumVal[x].GetDecimalNullToZero() / varCnt[x].GetDecimalNullToZero()));

                        //최대값 - 최소값
                        lblList2[1][x].Text = (maxVal[x] - minVal[x]).GetNullToEmpty();
                    }
                }
            }
        }

        public XRQCT1001(TN_QCT1500 obj) : this()
        {
            cultureIndex = DataConvert.GetCultureIndex();

            cell_printDate.Text = DateTime.Today.ToShortDateString();
            cell_CustomerName.Text = obj.TN_STD1400.CustomerName;
            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);
            cell_LotNo.Text = obj.ProductLotNo;
            cell_Qty.Text = obj.OutQty.GetDecimalNullToZero().ToString("N0");

            cell_CarType.Text = obj.TN_STD1100.CarType;

            cell_InspectDate.Text = obj.CheckDate.Value.ToShortDateString();
            cell_Result.Text = obj.CheckResult == "OK" ? "합격" : "불합격";

            #region 성적서샘플링 검사수량 가져오기
            var samplyQty = (decimal)0;

            ////검사 마스터
            //var qct1100Obj = ModelService.GetChildList<TN_QCT1100>(p => p.InspNo == obj.FinalInspNo).FirstOrDefault();

            //if (qct1100Obj != null)
            //{
            //    //작업실적관리 마스터
            //    var mps1201Obj = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == qct1100Obj.WorkNo && p.ProcessCode == qct1100Obj.ProcessCode && p.ProcessSeq == qct1100Obj.WorkSeq).FirstOrDefault();

            //    if (mps1201Obj != null)
            //    {
            //        //성적서샘플링관리
            //        var qct1600ObjList = ModelService.GetChildList<TN_QCT1600>(p => p.AQL == mps1201Obj.TN_STD1100.AQL && p.CustomerCode == mps1201Obj.CustomerCode).ToList();

            //        if (qct1600ObjList.Count > 0)
            //        {
            //            foreach (var list in qct1600ObjList)
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
            var qct1600ObjList = ModelService.GetChildList<TN_QCT1600>(p => p.AQL == obj.TN_STD1100.AQL && p.CustomerCode == obj.CustomerCode).ToList();
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

            cell_SampleQty.Text = samplyQty > 0 ? "n-" + samplyQty.ToString("#,###") : "n-200";
            #endregion

            if (obj.TN_QCT1501List.Count > 0)
            {
                for (int n = 1; n < 8; n++)
                {
                    XRTableCell cell_CheckList = (XRTableCell)FindControl("cell_CheckList" + n, true);
                    XRTableCell cell_CheckSpec = (XRTableCell)FindControl("cell_CheckSpec" + n, true);
                    XRLabel label = (XRLabel)Detail.FindControl("Type" + n + "InstNo", true);

                    var TN_QCT1501 = obj.TN_QCT1501List.Where(p => p.InspSeq == n).FirstOrDefault();
                    //var listSpct = obj.TN_QCT1501List.Where(p => p.CheckList == "0" + n).FirstOrDefault();
                    //var listInst = obj.TN_QCT1501List.Where(p => p.CheckList == "0" + n).FirstOrDefault();

                    //검사항목
                    if (TN_QCT1501 != null) cell_CheckList.Text = TN_QCT1501.CheckList;
                    else cell_CheckList.Text = "";

                    //검사항목별 규격
                    if (TN_QCT1501 != null) cell_CheckSpec.Text = TN_QCT1501.TN_QCT1001.InspectionReportMemo;
                    else cell_CheckSpec.Text = "";

                    //검사항목별 계측기NO
                    if (TN_QCT1501 != null) label.Text = TN_QCT1501.TN_QCT1001.InstrumentNo;
                    else label.Text = "";
                }

                //검사 값 Label
                lblList1 = new XRLabel[][]
                {
                    new XRLabel[] {  Type1X1, Type2X1, Type3X1, Type4X1, Type5X1, Type6X1, Type7X1}, //X1
                    new XRLabel[] {  Type1X2, Type2X2, Type3X2, Type4X2, Type5X2, Type6X2, Type7X2}, //X2
                    new XRLabel[] {  Type1X3, Type2X3, Type3X3, Type4X3, Type5X3, Type6X3, Type7X3}, //X3
                    new XRLabel[] {  Type1X4, Type2X4, Type3X4, Type4X4, Type5X4, Type6X4, Type7X4}, //X4
                    new XRLabel[] {  Type1X5, Type2X5, Type3X5, Type4X5, Type5X5, Type6X5, Type7X5}, //X5
                    new XRLabel[] {  Type1X6, Type2X6, Type3X6, Type4X6, Type5X6, Type6X6, Type7X6}, //X6
                    new XRLabel[] {  Type1X7, Type2X7, Type3X7, Type4X7, Type5X7, Type6X7, Type7X7}, //X7
                    new XRLabel[] {  Type1X8, Type2X8, Type3X8, Type4X8, Type5X8, Type6X8, Type7X8}, //X8
                    new XRLabel[] {  Type1X9, Type2X9, Type3X9, Type4X9, Type5X9, Type6X9, Type7X9}  //X9
                };

                //Xbar, R, 계측기NO 값 Label
                lblList2 = new XRLabel[][]
                {
                    new XRLabel[] { Type1Xbar, Type2Xbar, Type3Xbar, Type4Xbar, Type5Xbar, Type6Xbar, Type7Xbar}, //Xbar(평균값)
                    new XRLabel[] { Type1R, Type2R, Type3R, Type4R, Type5R, Type6R, Type7R},                      //R(최대 값-최소 값)
                };

                string[] sumVal = new string[7];   //검사항목별 검사 값 합계
                int[] varCnt = new int[7];         //검사항목별 검사 수
                decimal[] maxVal = new decimal[7]; //검사항목별 최대 값
                decimal[] minVal = new decimal[7]; //검사항목별 최소 값
                decimal parseDecimal;              //TryParse out 변수

                int i = 0;
                foreach (var v in obj.TN_QCT1501List)
                {
                    if (i == 7) break;

                    lblList1[0][i].Text = v.Reading1;
                    lblList1[1][i].Text = v.Reading2;
                    lblList1[2][i].Text = v.Reading3;
                    lblList1[3][i].Text = v.Reading4;
                    lblList1[4][i].Text = v.Reading5;
                    lblList1[5][i].Text = v.Reading6;
                    lblList1[6][i].Text = v.Reading7;
                    lblList1[7][i].Text = v.Reading8;
                    lblList1[8][i].Text = v.Reading9;


                    if (v.CheckWay == MasterCodeSTR.InspectionWay_Input) //검사방법-치수검사
                    {
                        //항목별 검사 값 합계
                        sumVal[i] = (v.Reading1.GetDecimalNullToZero() + v.Reading2.GetDecimalNullToZero() + v.Reading3.GetDecimalNullToZero()
                                    + v.Reading4.GetDecimalNullToZero() + v.Reading5.GetDecimalNullToZero() + v.Reading6.GetDecimalNullToZero()
                                    + v.Reading7.GetDecimalNullToZero() + v.Reading8.GetDecimalNullToZero() + v.Reading9.GetDecimalNullToZero()).GetNullToEmpty();
                    }
                    else //검사방법-외관검사
                    {
                        //항목별 검사 값 합계(NG가 1개 이상일 시 NG)
                        if (v.Reading1 == "NG" || v.Reading2 == "NG" || v.Reading3 == "NG" || v.Reading4 == "NG" || v.Reading5 == "NG" ||
                            v.Reading6 == "NG" || v.Reading7 == "NG" || v.Reading8 == "NG" || v.Reading9 == "NG")

                            sumVal[i] = "NG";
                        else
                            sumVal[i] = "OK";
                    }
                    i++;
                }

                //검사항목별 최대 값, 최소 값, 검사 수
                for (int a = 0; a < lblList1.Length; a++)
                {
                    for (int b = 0; b < 7; b++)
                    {
                        if (decimal.TryParse(lblList1[a][b].Text, out parseDecimal)) //검사 값이 숫자일 때
                        {
                            if (minVal[b] == 0)
                            {
                                minVal[b] = lblList1[a][b].Text.GetDecimalNullToZero();
                            }

                            if (minVal[b].GetDecimalNullToZero() > lblList1[a][b].Text.GetDecimalNullToZero())
                                minVal[b] = lblList1[a][b].Text.GetDecimalNullToZero();

                            if (maxVal[b].GetDecimalNullToZero() < lblList1[a][b].Text.GetDecimalNullToZero())
                                maxVal[b] = lblList1[a][b].Text.GetDecimalNullToZero();
                        }

                        if (lblList1[a][b].Text.GetNullToEmpty() != "")
                            varCnt[b] += 1;
                    }
                }

                for (int x = 0; x < 7; x++) //검사항목 수 x
                {
                    // Xbar(평균값)
                    if (!decimal.TryParse(sumVal[x].GetNullToEmpty(), out parseDecimal) || sumVal[x].GetDecimalNullToZero() == 0 || varCnt[x].GetDecimalNullToZero() == 0)
                        lblList2[0][x].Text = "0";
                    else
                        lblList2[0][x].Text = string.Format("{0:n3}", (sumVal[x].GetDecimalNullToZero() / varCnt[x].GetDecimalNullToZero()));

                    //최대값 - 최소값
                    lblList2[1][x].Text = (maxVal[x] - minVal[x]).GetNullToEmpty();
                }
            }
        }
    }
}
