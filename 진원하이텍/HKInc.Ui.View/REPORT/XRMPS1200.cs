using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
//using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Linq;
using HKInc.Utils;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;

namespace HKInc.Ui.View.REPORT
{
    public partial class XRMPS1200 : DevExpress.XtraReports.UI.XtraReport
    {
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;
        //IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");
        IService<TN_MPS1300> ModelService = (IService<TN_MPS1300>)ProductionFactory.GetDomainService("TN_MPS1300");

        public XRMPS1200()
        {
            InitializeComponent();
            xrLabel10.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
        }

        public XRMPS1200(TN_MPS1400 detailObj) : this()
        {
            bindingSource1.DataSource = detailObj;
            xrLabel10.Text = Utils.Common.GlobalVariable.COMPANY_NAME;

            //var cultureIndex = DataConvert.GetCultureIndex();
            //var ProductTeamCodeList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode);
            //var MachineGroupList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup);
            //var ProcessList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            //var SurfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);

            //var MachineGroupList = ModelService.GetChildList<TN_MEA1000>(x => x.UseYn == "Y").ToList();
            var ProcessList = ModelService.GetChildList<TN_STD1000>(x => (x.Codemain == MasterCodeSTR.Process) && (x.Useyn == "Y")).ToList();

            //lbl_Emergency.Visible = false;
            //if (detailObj.EmergencyFlag == "Y")
            //    lbl_Emergency.Visible = true;



            #region 수주처

            var tN_ORD1000 = ModelService.GetChildList<TN_ORD1000>(x => x.OrderNo == detailObj.OrderNo).FirstOrDefault();
            //var tN_STD1100_CarTyp = ModelService.GetChildList<TN_STD1000>(x => x.Codemain == MasterCodeSTR.CARTYPE && x.Useyn == "Y").FirstOrDefault();
            if (tN_ORD1000 == null)
            {
                cell_MainCustomerName.Text = string.Empty;
            }
            else
            {
                cell_MainCustomerName.Text = tN_ORD1000.TN_STD1400.CustomerName;
            }

            //cell_PlanEndDate.Text = detailObj.TN_MPS1100.PlanEndDate.ToString("yyyy-MM-dd"); //생산완료일
            //cell_CarType.Text = detailObj.TN_STD1100.BottomCategory; //차종

            //if (detailObj.TN_STD1400 == null)
            //{
            //    cell_MainCustomerName.Text = string.Empty;
            //}
            //else
            //{
            //    if (cultureIndex == 1)
            //    {
            //        //국문
            //        cell_MainCustomerName.Text = detailObj.TN_STD1400.CustomerName;
            //    }
            //    else if (cultureIndex == 2)
            //    {
            //        //영문
            //        cell_MainCustomerName.Text = detailObj.TN_STD1400.CustomerNameENG;
            //    }
            //    else if (cultureIndex == 3)
            //    {
            //        //중문
            //        cell_MainCustomerName.Text = detailObj.TN_STD1400.CustomerNameCHN;
            //    }
            //}
            #endregion

            cell_ItemCode.Text = detailObj.TN_MPS1300.TN_STD1100.ItemNm;
            cell_PlanEndDate.Text = detailObj.TN_MPS1300.PlanEnddt?.ToString("yyyy-MM-dd");
            //cell_CarType.Text = detailObj.TN_MPS1300.TN_STD1100.BottomCategory;
            if (detailObj.TN_MPS1300.TN_STD1100.BottomCategory != null)
            {
                TN_STD1000 carTyp = ModelService.GetChildList<TN_STD1000>(x => x.Useyn == "Y" && x.Codemain == MasterCodeSTR.CARTYPE && x.Mcode == detailObj.TN_MPS1300.TN_STD1100.BottomCategory).FirstOrDefault();

                if (carTyp != null)
                    cell_CarType.Text = carTyp.CodeName;
            }

            xrTableCell56.Text = ModelService.GetChildList<TN_ORD1100>(x => x.DelivSeq == detailObj.DelivSeq).Select(s => s.DelivQty).FirstOrDefault().GetNullToEmpty();
            //cell_ItemCode.Text = detailObj.ItemCode; //품번
            //cell_PlanEndDate.Text = detailObj.TN_MPS1100.PlanEndDate.ToString("yyyy-MM-dd"); //생산완료일  
            //cell_CarType.Text = detailObj.TN_STD1100.CarType; //차종

            #region 품명
            if (detailObj.TN_MPS1300.TN_STD1100 == null)
            {
                cell_ItemName.Text = string.Empty;
            }
            else
            {
                cell_ItemName.Text = detailObj.TN_MPS1300.TN_STD1100.ItemNm1;
            }
            //else
            //{
            //    if (cultureIndex == 1)
            //    {
            //        //국문
            //        cell_ItemName.Text = detailObj.TN_STD1100.ItemName;
            //    }
            //    else if (cultureIndex == 2)
            //    {
            //        //영문
            //        cell_ItemName.Text = detailObj.TN_STD1100.ItemNameENG;
            //    }
            //    else if (cultureIndex == 3)
            //    {
            //        //중문
            //        cell_ItemName.Text = detailObj.TN_STD1100.ItemNameCHN;
            //    }
            //}
            #endregion

            /*
            ///설비그룹
            var machineGroupObj = detailObj.TN_MPS1100.TN_MPS1200List.Where(p => p.WorkNo == detailObj.WorkNo && !p.MachineGroupCode.IsNullOrEmpty()).OrderBy(p => p.ProcessSeq).FirstOrDefault();
            if (machineGroupObj == null)
            {
                cell_MachineGroup.Text = string.Empty;
            }
            else
            {
                var machineGroupCode = machineGroupObj.MachineGroupCode.GetNullToEmpty();
                var codeObj = Service.Service.DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup).Where(p => p.CodeVal == machineGroupCode).FirstOrDefault();
                if (codeObj == null)
                {
                    cell_MachineGroup.Text = string.Empty;
                }
                else
                {
                    if (DataConvert.GetCultureIndex() == 1)
                    {
                        //국문
                        cell_MachineGroup.Text = codeObj.CodeName;
                    }
                    else if (DataConvert.GetCultureIndex() == 2)
                    {
                        //영문
                        cell_MachineGroup.Text = codeObj.CodeNameENG;
                    }
                    else if (DataConvert.GetCultureIndex() == 3)
                    {
                        //중문
                        cell_MachineGroup.Text = codeObj.CodeNameCHN;
                    }
                }
            }

            ///재질
            if (detailObj.TN_STD1100 == null)
            {
                cell_SrcItemName.Text = string.Empty;
            }
            else
            {
                if (detailObj.TN_STD1100.TN_STD1100_SRC == null)
                    cell_SrcItemName.Text = string.Empty;
                else
                {
                    if (DataConvert.GetCultureIndex() == 1)
                    {
                        //국문
                        cell_SrcItemName.Text = detailObj.TN_STD1100.TN_STD1100_SRC.ItemName;
                    }
                    else if (DataConvert.GetCultureIndex() == 2)
                    {
                        //영문
                        cell_SrcItemName.Text = detailObj.TN_STD1100.TN_STD1100_SRC.ItemNameENG;
                    }
                    else if (DataConvert.GetCultureIndex() == 3)
                    {
                        //중문
                        cell_SrcItemName.Text = detailObj.TN_STD1100.TN_STD1100_SRC.ItemNameCHN;
                    }
                }
            }
            */


            #region 현재고
            //var sumQty = ModelService.GetChildList<VI_PROD_STOCK_ITEM>(p => p.ItemCode == detailObj.ItemCode).FirstOrDefault();

            //if (sumQty == null)
            //{
            //    cell_ItemQty.Text = "0";
            //}
            //else
            //{
            //    cell_ItemQty.Text = sumQty.SumStockQty.ToString("#,0.##");
            //}
            #endregion

            #region 월소요량
            var previousThirdDate = DateTime.Today.AddMonths(-3);
            //var TN_ORD1201List = ModelService.GetChildList<TN_ORD1201>(p => p.ItemCode == detailObj.ItemCode
            //                                       && p.CreateTime.Year >= previousThirdDate.Year
            //                                       && p.CreateTime.Month >= previousThirdDate.Month
            //                                       && p.CreateTime.Year <= previousThirdDate.Year
            //                                       && p.CreateTime.Month <= previousThirdDate.Month).ToList();
            //if (TN_ORD1201List.Count > 0)
            //    cell_MonthUseQty.Text = TN_ORD1201List.Average(p => p.OutQty).GetDecimalNullToZero().ToString("#,0.##");
            //else
            //    cell_MonthUseQty.Text = "0";
            #endregion

            #region 영업담당

            //var tN_ORD1000 = ModelService.GetChildList<TN_ORD1000>(x => x.OrderNo == detailObj.OrderNo).FirstOrDefault();

            //// 20210607 오세완 차장 반제품 작업지시에서 수동으로 만든 작업지시는 영업담당자가 없어서 로직 변경
            //if(detailObj.TN_STD1400  == null)
            //{
            //    cell_ManagerName.Text = "";
            //}
            //else
            //{
            //    var loginId = detailObj.TN_STD1400.BusinessManagementId.GetNullToEmpty();
            //    var userObj = ModelService.GetChildList<User>(p => p.LoginId == loginId).FirstOrDefault();
            //    cell_ManagerName.Text = userObj == null ? string.Empty : userObj.UserName;
            //}

            var tN_MPS1300 = ModelService.GetChildList<TN_MPS1300>(x => x.DelivSeq == detailObj.DelivSeq && x.PlanNo == detailObj.PlanNo).FirstOrDefault();
            if (tN_MPS1300 != null)
            {
                string orderId = ModelService.GetChildList<TN_ORD1000>(x => x.OrderNo == tN_MPS1300.OrderNo).FirstOrDefault().OrderId;
                if(orderId != null)
                    cell_ManagerName.Text = ModelService.GetChildList<User>(x => x.LoginId == orderId).FirstOrDefault().UserName;
            }

            #endregion

            #region 생산팀
            //var teamObj = ProductTeamCodeList.Where(p => p.CodeVal == detailObj.TN_STD1100.ProcTeamCode).FirstOrDefault();
            //if (teamObj == null)
            //    cell_ProductTeam.Text = string.Empty;
            //else
            //{
            //    if (cultureIndex == 1)
            //        cell_ProductTeam.Text = teamObj.CodeName;
            //    else if(cultureIndex == 2)
            //        cell_ProductTeam.Text = teamObj.CodeNameENG;
            //    else if (cultureIndex == 3)
            //        cell_ProductTeam.Text = teamObj.CodeNameCHN;
            //}
            #endregion

            //xrTableCell63.Text = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == detailObj.TN_STD1100.SrcCode).Select(s => s.ItemName).FirstOrDefault();
            xrTableCell63.Text = ModelService.GetChildList<TN_STD1100>(x => x.ItemCode == detailObj.TN_MPS1300.TN_STD1100.SrcCode).Select(s => s.ItemNm).FirstOrDefault();

            #region 설비그룹
            cell_MachineName.Text = detailObj.MachineCode == null ? string.Empty : ModelService.GetChildList<TN_MEA1000>(x => x.MachineCode == detailObj.MachineCode && x.UseYn == "Y").Select(s => s.MachineName).FirstOrDefault();
            ////설비
            //cell_MachineName.Text = detailObj.MachineCode == null ? "" : detailObj.TN_MEA1000 == null ? "" : detailObj.TN_MEA1000.MachineName;

            //var machineGroupObj = MachineGroupList.Where(p => p.CodeVal == detailObj.MachineGroupCode).FirstOrDefault();
            //if (machineGroupObj == null)
            //    cell_MachineGroup.Text = string.Empty;
            //else
            //{
            //    if (cultureIndex == 1)
            //        cell_MachineGroup.Text = machineGroupObj.CodeName;
            //    else if (cultureIndex == 2)
            //        cell_MachineGroup.Text = machineGroupObj.CodeNameENG;
            //    else if (cultureIndex == 3)
            //        cell_MachineGroup.Text = machineGroupObj.CodeNameCHN;
            //}
            #endregion

            #region 공정정보
            //var TN_MPS1000List = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == detailObj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.ProcessSeq).ToList();
            var TN_MPS1000List = ModelService.GetChildList<TN_MPS1000>(x => x.ItemCode == detailObj.ItemCode && x.UseYn == "Y").OrderBy(o => o.ProcessSeq).ToList();

            for (int i = 0; i < TN_MPS1000List.Count; i++)
            {
                var checkProcess = TN_MPS1000List[i];
                if (checkProcess == null)
                    FindControl("cell_Process" + (i + 1).ToString(), true).Text = string.Empty;
                else
                {
                    //var processObj = ProcessList.Where(p => p.CodeVal == checkProcess.ProcessCode).FirstOrDefault();
                    var processObj = ProcessList.Where(x => x.Mcode == checkProcess.ProcessCode).FirstOrDefault();
                    if (processObj == null)
                        FindControl("cell_Process" + (i + 1).ToString(), true).Text = string.Empty;
                    else
                    {
                        FindControl("cell_Process" + (i + 1).ToString(), true).Text = processObj.CodeName;
                        //if (cultureIndex == 1)
                        //{
                        //    FindControl("cell_Process" + (i + 1).ToString(), true).Text = processObj.CodeName;
                        //}
                        //else if (cultureIndex == 2)
                        //{
                        //    FindControl("cell_Process" + (i + 1).ToString(), true).Text = processObj.CodeNameENG;
                        //}
                        //else if (cultureIndex == 3)
                        //{
                        //    FindControl("cell_Process" + (i + 1).ToString(), true).Text = processObj.CodeNameCHN;
                        //}

                        //if (processObj.CodeName.Contains("표면처리"))
                        //{
                        //    var surfaceObj = SurfaceList.Where(p => p.CodeVal == detailObj.TN_STD1100.SurfaceList).FirstOrDefault();
                        //    if (surfaceObj != null)
                        //        FindControl("cell_Process" + (i + 1).ToString(), true).Text += "_" + surfaceObj.CodeName;
                        //}
                    }
                }
            }
            #endregion

            #region 부적합정보 품목이슈
            cell_IssueDateTime.Text = string.Empty;
            cell_Issue.Text = string.Empty;
            cell_IssueDateTime2.Text = string.Empty;
            cell_Issue2.Text = string.Empty;
            cell_IssueDateTime3.Text = string.Empty;
            cell_Issue3.Text = string.Empty;
            //if (detailObj.TN_STD1100 != null && detailObj.TN_STD1100.TN_STD1102List.Count > 0)
            //{
            //    var Today = DateTime.Today;
            //    var TN_STD1102List = detailObj.TN_STD1100.TN_STD1102List.Where(p => p.StartDate <= Today && p.EndDate >= Today).OrderByDescending(c => c.Seq).Take(3).ToList();

            //    for (int i = 0; i < TN_STD1102List.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            cell_IssueDateTime.Text = TN_STD1102List[i].CreateTime.ToShortDateString();
            //            cell_Issue.Text = TN_STD1102List[i].Issue;
            //        }
            //        else if (i == 1)
            //        {
            //            cell_IssueDateTime2.Text = TN_STD1102List[i].CreateTime.ToShortDateString();
            //            cell_Issue2.Text = TN_STD1102List[i].Issue;
            //        }
            //        else
            //        {
            //            cell_IssueDateTime3.Text = TN_STD1102List[i].CreateTime.ToShortDateString();
            //            cell_Issue3.Text = TN_STD1102List[i].Issue;
            //        }
            //    }
            //}
            #endregion
        }

        //private void InitializeComponent()
        //{
        //    this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        //    this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
        //    this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        //    ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        //    // 
        //    // topMarginBand1
        //    // 
        //    this.topMarginBand1.Name = "topMarginBand1";
        //    // 
        //    // detailBand1
        //    // 
        //    this.detailBand1.Name = "detailBand1";
        //    // 
        //    // bottomMarginBand1
        //    // 
        //    this.bottomMarginBand1.Name = "bottomMarginBand1";
        //    // 
        //    // XRMPS1200
        //    // 
        //    this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
        //    this.topMarginBand1,
        //    this.detailBand1,
        //    this.bottomMarginBand1});
        //    this.Version = "20.1";
        //    ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        //}
    }
}


                



