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
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.REPORT
{
    public partial class XRMPS1200_DEV : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");

        public XRMPS1200_DEV()
        {
            InitializeComponent();
        }

        public XRMPS1200_DEV(TN_MPS1200 detailObj) : this()
        {
            bindingSource1.DataSource = detailObj;

            var cultureIndex = DataConvert.GetCultureIndex();
            var ProductTeamCodeList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode);
            var MachineGroupList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup);
            var ProcessList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            var SurfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);

            cell_WorkDate.Text = detailObj.WorkDate.ToShortDateString();

            //lbl_Emergency.Visible = false;
            //if (detailObj.EmergencyFlag == "Y")
            //lbl_Emergency.Visible = true;

            #region 수주처
            if (detailObj.TN_STD1400 == null)
            {
                cell_MainCustomerName.Text = string.Empty;
            }
            else
            {
                if (cultureIndex == 1)
                {
                    //국문
                    cell_MainCustomerName.Text = detailObj.TN_STD1400.CustomerName;
                }
                else if (cultureIndex == 2)
                {
                    //영문
                    cell_MainCustomerName.Text = detailObj.TN_STD1400.CustomerNameENG;
                }
                else if (cultureIndex == 3)
                {
                    //중문
                    cell_MainCustomerName.Text = detailObj.TN_STD1400.CustomerNameCHN;
                }
            }
            #endregion

            //cell_ItemCode.Text = detailObj.ItemCode; //품번
            //cell_PlanEndDate.Text = detailObj.TN_MPS1100.PlanEndDate.ToString("yyyy-MM-dd"); //생산완료일  
            //cell_CarType.Text = detailObj.TN_STD1100.CarType; //차종

            #region 품명
            if (detailObj.TN_STD1100 == null)
            {
                cell_ItemName.Text = string.Empty;
            }
            else
            {
                if (cultureIndex == 1)
                {
                    //국문
                    cell_ItemName.Text = detailObj.TN_STD1100.ItemName;
                }
                else if (cultureIndex == 2)
                {
                    //영문
                    cell_ItemName.Text = detailObj.TN_STD1100.ItemNameENG;
                }
                else if (cultureIndex == 3)
                {
                    //중문
                    cell_ItemName.Text = detailObj.TN_STD1100.ItemNameCHN;
                }
            }
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
            var sumQty = ModelService.GetChildList<VI_PROD_STOCK_ITEM>(p => p.ItemCode == detailObj.ItemCode).FirstOrDefault();

            if (sumQty == null)
            {
                cell_ItemQty.Text = "0";
            }
            else
            {
                cell_ItemQty.Text = sumQty.SumStockQty.ToString("#,0.##");
            }
            #endregion

            #region 월소요량
            var previousThirdDate = DateTime.Today.AddMonths(-3);
            var TN_ORD1201List = ModelService.GetChildList<TN_ORD1201>(p => p.ItemCode == detailObj.ItemCode
                                                   && p.CreateTime.Year >= previousThirdDate.Year
                                                   && p.CreateTime.Month >= previousThirdDate.Month
                                                   && p.CreateTime.Year <= previousThirdDate.Year
                                                   && p.CreateTime.Month <= previousThirdDate.Month).ToList();
            if (TN_ORD1201List.Count > 0)
                cell_MonthUseQty.Text = TN_ORD1201List.Average(p => p.OutQty).GetDecimalNullToZero().ToString("#,0.##");
            else
                cell_MonthUseQty.Text = "0";
            #endregion

            #region 영업담당
            var loginId = detailObj.TN_STD1400.BusinessManagementId.GetNullToEmpty();
            var userObj = ModelService.GetChildList<User>(p => p.LoginId == loginId).FirstOrDefault();
            cell_ManagerName.Text = userObj == null ? string.Empty : userObj.UserName;
            #endregion

            #region 생산팀
            var teamObj = ProductTeamCodeList.Where(p => p.CodeVal == detailObj.TN_STD1100.ProcTeamCode).FirstOrDefault();
            if (teamObj == null)
                cell_ProductTeam.Text = string.Empty;
            else
            {
                if (cultureIndex == 1)
                    cell_ProductTeam.Text = teamObj.CodeName;
                else if(cultureIndex == 2)
                    cell_ProductTeam.Text = teamObj.CodeNameENG;
                else if (cultureIndex == 3)
                    cell_ProductTeam.Text = teamObj.CodeNameCHN;
            }
            #endregion

            #region 설비그룹
            var machineGroupObj = MachineGroupList.Where(p => p.CodeVal == detailObj.MachineGroupCode).FirstOrDefault();
            if (machineGroupObj == null)
                cell_MachineGroup.Text = string.Empty;
            else
            {
                if (cultureIndex == 1)
                    cell_MachineGroup.Text = machineGroupObj.CodeName;
                else if (cultureIndex == 2)
                    cell_MachineGroup.Text = machineGroupObj.CodeNameENG;
                else if (cultureIndex == 3)
                    cell_MachineGroup.Text = machineGroupObj.CodeNameCHN;
            }
            #endregion

            #region 공정정보
            var TN_MPS1000List = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == detailObj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.ProcessSeq).ToList();

            for (int i = 0; i < TN_MPS1000List.Count; i++)
            {
                var checkProcess = TN_MPS1000List[i];
                if (checkProcess == null)
                    FindControl("cell_Process" + (i + 1).ToString(), true).Text = string.Empty;
                else
                {
                    var processObj = ProcessList.Where(p => p.CodeVal == checkProcess.ProcessCode).FirstOrDefault();
                    if (processObj == null)
                        FindControl("cell_Process" + (i + 1).ToString(), true).Text = string.Empty;
                    else
                    {
                        if (cultureIndex == 1)
                        {
                            FindControl("cell_Process" + (i + 1).ToString(), true).Text = processObj.CodeName;
                        }
                        else if (cultureIndex == 2)
                        {
                            FindControl("cell_Process" + (i + 1).ToString(), true).Text = processObj.CodeNameENG;
                        }
                        else if (cultureIndex == 3)
                        {
                            FindControl("cell_Process" + (i + 1).ToString(), true).Text = processObj.CodeNameCHN;
                        }

                        if (processObj.CodeName.Contains("표면처리"))
                        {
                            var surfaceObj = SurfaceList.Where(p => p.CodeVal == detailObj.TN_STD1100.SurfaceList).FirstOrDefault();
                            if(surfaceObj != null)
                                FindControl("cell_Process" + (i + 1).ToString(), true).Text += Environment.NewLine + surfaceObj.CodeName;
                        }
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
            if (detailObj.TN_STD1100 != null && detailObj.TN_STD1100.TN_STD1102List.Count > 0)
            {
                var Today = DateTime.Today;
                var TN_STD1102List = detailObj.TN_STD1100.TN_STD1102List.Where(p=>p.StartDate <= Today && p.EndDate >= Today).OrderByDescending(c => c.Seq).Take(3).ToList();

                for (int i = 0; i < TN_STD1102List.Count; i++)
                {
                    if (i == 0)
                    {
                        cell_IssueDateTime.Text = TN_STD1102List[i].CreateTime.ToShortDateString();
                        cell_Issue.Text = TN_STD1102List[i].Issue;
                    }
                    else if (i == 1)
                    {
                        cell_IssueDateTime2.Text = TN_STD1102List[i].CreateTime.ToShortDateString();
                        cell_Issue2.Text = TN_STD1102List[i].Issue;
                    }
                    else
                    {
                        cell_IssueDateTime3.Text = TN_STD1102List[i].CreateTime.ToShortDateString();
                        cell_Issue3.Text = TN_STD1102List[i].Issue;
                    }
                }
            }
            #endregion
        }
    }
}


                



