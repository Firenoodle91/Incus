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
    public partial class XRQCT1004 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        List<TN_STD1000> ListCheckWay;

        public XRQCT1004()
        {
            InitializeComponent();
        }

        public XRQCT1004(TN_QCT1100 obj) : this()
        {
            //bindingSource1.DataSource = obj;

            var tN_QCT1101 = ModelService.GetChildList<TN_QCT1101>(x => x.InspNo == obj.InspNo).ToList();
            ListCheckWay = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeMain == MasterCodeSTR.InspectionWay).ToList();

            List<XRQCT1004_DATA> list = new List<XRQCT1004_DATA>();

            foreach (var s in tN_QCT1101)
            {
                string checkwayName = ListCheckWay.Where(x => x.CodeVal == s.CheckWay).Select(ss => ss.CodeName).FirstOrDefault();

                XRQCT1004_DATA data = new XRQCT1004_DATA(s.InspSeq, s.CheckList, checkwayName, s.TN_QCT1001.MaxReading
                    , s.Reading1, s.Reading2, s.Reading3, s.Reading4, s.Reading5, s.Reading6, s.Reading7, s.Reading8, s.Reading9, s.Judge, s.Memo);
                list.Add(data);
            }

            //bindingSource1.DataSource = tN_QCT1101;
            bindingSource1.DataSource = list;

            try
            {
                printTitle(obj);

                TN_STD1104 tN_STD1104 = ModelService.GetChildList<TN_STD1104>(x => x.ItemCode == obj.ItemCode && x.CheckDivision == MasterCodeSTR.InspectionDivision_IN).OrderByDescending(o => o.ApplyDate).FirstOrDefault();

                if (tN_STD1104 != null && tN_STD1104.FileUrl != null)
                    xrPictureBox1.ImageUrl = GlobalVariable.HTTP_SERVER + tN_STD1104.FileUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void printTitle(TN_QCT1100 obj)
        {

            //TN_STD1400 cust = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == HKInc.Ui.Model.BaseDomain.GsValue.CustCode).FirstOrDefault();
            TN_STD1400 cust = ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y" && x.CustomerCode == obj.CustomerCode).FirstOrDefault();

            cellCust.Text = cust.CustomerName.GetNullToEmpty();
            cellCar.Text = obj.TN_STD1100.CarType.GetNullToEmpty();
            cellItem.Text = obj.TN_STD1100.ItemName.GetNullToEmpty();
            xrTableCell42.Text = obj.WorkDate.ToString("yyyy-MM-dd");
        }
    }

    /// <summary>
    /// 출력물 데이터 가공
    /// </summary>
    public class XRQCT1004_DATA
    {
        public int InspSeq { get; set; }
        public string CheckList { get; set; }
        public string CheckWay { get; set; }
        public string MaxReading { get; set; }
        public string Reading1 { get; set; }
        public string Reading2 { get; set; }
        public string Reading3 { get; set; }
        public string Reading4 { get; set; }
        public string Reading5 { get; set; }
        public string Reading6 { get; set; }
        public string Reading7 { get; set; }
        public string Reading8 { get; set; }
        public string Reading9 { get; set; }
        public string Judge { get; set; }
        public string Memo { get; set; }

        public XRQCT1004_DATA(int seq, string check, string way, string max, string r1, string r2, string r3, string r4, string r5, string r6, string r7, string r8, string r9, string judge, string memo)
        {
            this.InspSeq = seq;
            this.CheckList = check;
            this.CheckWay = way;
            this.MaxReading = max;
            this.Reading1 = r1;
            this.Reading2 = r2;
            this.Reading3 = r3;
            this.Reading4 = r4;
            this.Reading5 = r5;
            this.Reading6 = r6;
            this.Reading7 = r7;
            this.Reading8 = r8;
            this.Reading9 = r9;
            this.Judge = judge;
            this.Memo = memo;
        }

    }
}
