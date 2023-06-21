using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using System.Linq;
using HKInc.Service.Helper;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using System.IO;
using System.Collections.Generic;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.QCT_POPUP
{
    /// <summary>
    /// 부적합보고서
    /// </summary>
    public partial class XRQCT1300 : DevExpress.XtraReports.UI.XtraReport
    {
        //IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");
        private int cultureIndex;
        private List<TN_STD1000> teamList;

        public XRQCT1300()
        {
            InitializeComponent();
        }

        public XRQCT1300(VI_QCT1300_LIST masterObj, TN_QCT1300 detailObj) : this()
        {
            bindingSource1.DataSource = detailObj;
            
            cell_Team.BeforePrint += Cell_Team_BeforePrint;
            teamList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode);

            //다국어인덱스
            cultureIndex = DataConvert.GetCultureIndex();

            //구분
            var pType1 = masterObj.P_TYPE == "사내" ? "■사내" : "□사내";
            var pType2 = masterObj.P_TYPE == "고객" ? "■고객" : "□고객";
            cell_PTYPE.Text = string.Format("{0}{1}{2}", pType1, Environment.NewLine, pType2);

            //사용자리스트
            IService<VI_QCT1300_LIST> ModelService = (IService<VI_QCT1300_LIST>)ProductionFactory.GetDomainService("VI_QCT1300_LIST");
            var userList = ModelService.GetChildList<User>(p => true).ToList();

            //담당자
            var workIdObj = userList.Where(p => p.LoginId == detailObj.ManagerId).FirstOrDefault();
            cell_WorkId.Text = workIdObj == null ? string.Empty : workIdObj.UserName;

            //발생일자
            cell_OccurDate.Text = detailObj.OccurDate == null ? string.Empty : ((DateTime)detailObj.OccurDate).ToShortDateString();

            //검사자
            var checkIdObj = userList.Where(p => p.LoginId == detailObj.CheckId).FirstOrDefault();
            cell_CheckId.Text = checkIdObj == null ? string.Empty : checkIdObj.UserName;

            //거래처명
            var customerObj = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == detailObj.CustomerCode2).FirstOrDefault();
            cell_CustomerName.Text = customerObj == null ? string.Empty : cultureIndex == 1 ? customerObj.CustomerName : (cultureIndex == 2 ? customerObj.CustomerNameENG : customerObj.CustomerNameCHN);

            //도번/품명
            if (masterObj.TN_STD1100 == null)
                cell_ItemCodeName.Text = string.Empty;
            else
            {
                var itemName = cultureIndex == 1 ? masterObj.TN_STD1100.ItemName : (cultureIndex == 2 ? masterObj.TN_STD1100.ItemNameENG : masterObj.TN_STD1100.ItemNameCHN);
                cell_ItemCodeName.Text = masterObj.ItemCode + " / " + itemName;
            }

            ////가공시간
            //if(masterObj.ProcessMinute != null)
            //    cell_ProcessMinute.Text = ((int)masterObj.ProcessMinute).ToString("#,#");

            //재질
            if (masterObj.TN_STD1100 == null || masterObj.TN_STD1100.TN_STD1100_SRC == null)
                cell_SrcItemCodeName.Text = string.Empty;
            else
            {
                var itemName = cultureIndex == 1 ? masterObj.TN_STD1100.TN_STD1100_SRC.ItemName : (cultureIndex == 2 ? masterObj.TN_STD1100.TN_STD1100_SRC.ItemNameENG : masterObj.TN_STD1100.TN_STD1100_SRC.ItemNameCHN);
                cell_SrcItemCodeName.Text = itemName;
            }

            ////불량수량
            //if (masterObj.BadQty != null)
            //    cell_MasterBadQty.Text = masterObj.BadQty.GetDecimalNullToZero().ToString("#,#");

            //발생장소
            var occurLocationList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.OccurPosition);
            var occurLocationObj = occurLocationList.Where(p => p.CodeVal == detailObj.OccurLocation).FirstOrDefault();
            cell_OccurLocation.Text = occurLocationObj == null ? string.Empty : occurLocationObj.CodeName;
                       
            //생산설비
            var machineObj = ModelService.GetChildList<TN_MEA1000>(p => p.MachineCode == detailObj.MachineCode).FirstOrDefault();
            cell_MachineCode.Text = machineObj == null ? string.Empty : cultureIndex == 1 ? machineObj.MachineName : (cultureIndex == 2 ? machineObj.MachineNameENG : machineObj.MachineNameCHN);

            //부적합내용
            var qcrTypeList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.QcrType);
            var qcrTypeObj = qcrTypeList.Where(p => p.CodeVal == detailObj.QcrType).FirstOrDefault();
            var qcrContent = detailObj.QcrContent.GetNullToEmpty();
            var qcrType = qcrTypeObj == null ? string.Empty : qcrTypeObj.CodeName;
            cell_QcrContent.Text = qcrType.IsNullOrEmpty() ? qcrContent : (qcrType + " : " + qcrContent);

            //처리방법
            var solutionList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadReportSolution);
            var solutionObj = solutionList.Where(p => p.CodeVal == detailObj.SolutionContent).FirstOrDefault();
            cell_Solution.Text = solutionObj == null ? string.Empty : solutionObj.CodeName;

            //사진
            var fileUrl = detailObj.FileUrl;
            var fileUrl2 = detailObj.FileUrl2;

            if (!fileUrl.IsNullOrEmpty())
            {
                var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + fileUrl);
                if (fileData != null)
                {
                    var stream = new MemoryStream(fileData);
                    pic_Ok.Image = Image.FromStream(stream);
                }
            }
            else
            {
                var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).FirstOrDefault();
                if (itemObj != null)
                {
                    if (!itemObj.ProdFileUrl.IsNullOrEmpty())
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + itemObj.ProdFileUrl);
                        if (fileData != null)
                        {
                            var stream = new MemoryStream(fileData);
                            pic_Ok.Image = Image.FromStream(stream);
                        }
                    }
                }
            }

            if (!fileUrl2.IsNullOrEmpty())
            {
                var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + fileUrl2);
                if (fileData != null)
                {
                    var stream = new MemoryStream(fileData);
                    pic_Bad.Image = Image.FromStream(stream);
                }
            }
            else
            {
                if (detailObj.PType == "고객")
                {
                    var TN_QCT1400 = ModelService.GetChildList<TN_QCT1400>(p => p.ClaimNo == detailObj.PKey).FirstOrDefault();
                    if (TN_QCT1400 != null && !TN_QCT1400.FileUrl.IsNullOrEmpty())
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_QCT1400.FileUrl);
                        if (fileData != null)
                        {
                            var stream = new MemoryStream(fileData);
                            pic_Bad.Image = Image.FromStream(stream);
                        }
                    }
                }
            }
        }

        private void Cell_Team_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var teamCode = cell_Team.Text;
            if (teamCode.IsNullOrEmpty())
                cell_Team.Text = string.Empty;
            else
            {
                var obj = teamList.Where(p => p.CodeVal == teamCode).FirstOrDefault();
                if (obj == null)
                    cell_Team.Text = string.Empty;
                else
                    cell_Team.Text = cultureIndex == 1 ? obj.CodeName : (cultureIndex == 2 ? obj.CodeNameENG : obj.CodeNameCHN);
            }
        }
    }
}
