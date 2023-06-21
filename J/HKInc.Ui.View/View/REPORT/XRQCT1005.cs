using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 수입검사 성적서 출력물
    /// </summary>
    public partial class XRQCT1005 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");
        List<TN_STD1000> ListCheckWay;

        public XRQCT1005()
        {
            InitializeComponent();
        }

        public XRQCT1005(TN_QCT1100 obj) : this()
        {
            //수입검사 obj를 가져와서 SCM과 내부 수입검사 내용을 처리한다.
            ListCheckWay = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeMain == MasterCodeSTR.InspectionWay).ToList();

            SetTitle(obj);
            SetImage(obj.ItemCode);

            SetData(obj);
        }

        private void SetData(TN_QCT1100 obj)
        {
            List<XRQCT1005_DATA> list = new List<XRQCT1005_DATA>();

            //수요자
            List<TN_QCT1101> d_Result = ModelService.GetChildList<TN_QCT1101>(x => x.InspNo == obj.InspNo).ToList();

            //공급자
            SCM_VI_QCT1100_SCM scmQC = ModelService.GetChildList<SCM_VI_QCT1100_SCM>(x => x.WorkNo == obj.WorkNo && x.ItemCode == obj.ItemCode).FirstOrDefault();
            List<SCM_VI_QCT1101_SCM> s_Result = null;

            if (scmQC != null)
            {
                //List<SCM_VI_QCT1101_SCM> s_Result = ModelService.GetChildList<SCM_VI_QCT1101_SCM>(x => x.InspNo == scmQC.InspNo).ToList();
                s_Result = ModelService.GetChildList<SCM_VI_QCT1101_SCM>(x => x.InspNo == scmQC.InspNo).ToList();
            }

            //수요자
            int count = 1;
            foreach (var s in d_Result)
            {
                XRQCT1005_DATA data = new XRQCT1005_DATA();
                data.No = count;
                data.InspNo = obj.InspNo;
                data.WorkNo = obj.WorkNo;
                data.InLotNo = obj.InLotNo;
                data.CheckList = s.CheckList;
                data.CheckWay = s.CheckWay;
                data.CheckWayNM = ListCheckWay.Where(x => x.CodeVal == s.CheckWay).Select(ss => ss.CodeName).FirstOrDefault();
                data.D_Reading1 = s.Reading1;
                data.D_Reading2 = s.Reading2;
                data.D_Reading3 = s.Reading3;
                data.D_Reading4 = s.Reading4;
                data.D_Reading5 = s.Reading5;
                data.D_Judge = s.Judge;

                list.Add(data);
                count++;
            }

            //공급자
            if (s_Result != null)
            {
                foreach (var s in s_Result)
                {
                    XRQCT1005_DATA data = list.Where(x => x.WorkNo == scmQC.WorkNo && x.InLotNo == scmQC.InLotNo && x.CheckList == s.CheckList).LastOrDefault();

                    if (data != null)
                    {
                        data.S_Reading1 = s.Reading1;
                        data.S_Reading2 = s.Reading2;
                        data.S_Reading3 = s.Reading3;
                        data.S_Reading4 = s.Reading4;
                        data.S_Reading5 = s.Reading5;
                        data.S_Judge = s.Judge;
                    }
                }
            }

            bindingSource1.DataSource = list;
        }

        private void SetTitle(TN_QCT1100 obj)
        {
            TN_STD1400 custom = ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y" && x.CustomerCode == obj.CustomerCode).FirstOrDefault();

            cellCust.Text = custom.CustomerName.GetNullToEmpty();
            cellCar.Text = obj.TN_STD1100.CarType.GetNullToEmpty();
            cellItem.Text = obj.TN_STD1100.ItemName.GetNullToEmpty();
            xrTableCell42.Text = obj.WorkDate.ToString("yyyy-MM-dd");
        }

        private void SetImage(string itemCode)
        {
            try
            {
                TN_STD1104 tN_STD1104 = ModelService.GetChildList<TN_STD1104>(x => x.ItemCode == itemCode && x.CheckDivision == MasterCodeSTR.InspectionDivision_IN).OrderByDescending(o => o.ApplyDate).FirstOrDefault();

                if (tN_STD1104 != null)
                {
                    xrPictureBox1.ImageUrl = GlobalVariable.HTTP_SERVER + tN_STD1104.FileUrl;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class XRQCT1005_DATA
    { 
        public int No { get; set; }
        public string InspNo { get; set; }
        public string WorkNo { get; set; }
        public string InLotNo { get; set; }
        public string CheckList { get; set; }
        public string CheckWay { get; set; }
        public string CheckWayNM { get; set; }
        public string S_Reading1 { get; set; }
        public string S_Reading2 { get; set; }
        public string S_Reading3 { get; set; }
        public string S_Reading4 { get; set; }
        public string S_Reading5 { get; set; }
        public string S_Judge { get; set; }
        public string D_Reading1 { get; set; }
        public string D_Reading2 { get; set; }
        public string D_Reading3 { get; set; }
        public string D_Reading4 { get; set; }
        public string D_Reading5 { get; set; }
        public string D_Judge { get; set; }

        public XRQCT1005_DATA()
        {

        }
    }

    
}
