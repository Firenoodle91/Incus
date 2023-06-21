using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 디디디 성적서
    /// </summary>
    public partial class XRQCT1100_B_ : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_QCT1200> ModelService = (IService<TN_QCT1200>)ProductionFactory.GetDomainService("TN_QCT1200");

        List<TN_STD1000> qctQ006;   //검사항목
        List<TN_STD1000> qctQ002;   //검사방법


        public XRQCT1100_B_()
        {
            InitializeComponent();
        }

        public XRQCT1100_B_(TN_QCT1200 masterObj) : this()
        {
            qctQ002 = ModelService.GetChildList<TN_STD1000>(x => x.Codemain == MasterCodeSTR.QCTYPE && x.Useyn == "Y");
            qctQ006 = ModelService.GetChildList<TN_STD1000>(x => x.Codemain == MasterCodeSTR.QCPOINT && x.Useyn == "Y");

            MainData(masterObj);
            SubData(masterObj);
        }

        private void MainData(TN_QCT1200 obj)
        {
            cell_ItemName1.Text = obj.TN_STD1100.ItemNm.GetNullToEmpty();
            cell_ItemName2.Text = obj.TN_STD1100.ItemNm1.GetNullToEmpty();
            cell_Grade.Text = "";
            cell_Company.Text = GlobalVariable.COMPANY_NAME.GetNullToEmpty();
            cell_LotNO.Text = obj.LotNo;
            cell_LotQty.Text = "";
            cell_readQty.Text = "n=3/lot, c=0";
            cell_InspDate.Text = obj.CheckDate?.ToString("yyyy-MM-dd").GetNullToEmpty();
            cell_WorkId.Text = "여 기 찬   (인)";
            cell_No.Text = "";
            cell_InspDate2.Text = "20  .   .   .";
            cell_InspId.Text = obj.CheckId.GetNullToEmpty();
            cell_Judge.Text = obj.CheckResult.GetNullToEmpty();
        }

        private void SubData(TN_QCT1200 obj)
        {
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                SqlParameter param1 = new SqlParameter("@ItemCode", obj.ItemCode);
                SqlParameter param2 = new SqlParameter("@InspNo", obj.No);

                List<XRQCT1100_DATA_B> result = context.Database.SqlQuery<XRQCT1100_DATA_B>("USP_GET_XRQCT1100_LISTDATA_B @ItemCode, @InspNo", param1, param2).ToList();

                bindingSource1.DataSource = result;
            }
        }
    }

    class XRQCT1100_DATA_B
    {
        public long RowNo { get; set; }
        public string InspNO { get; set; }
        public string LotNo { get; set; }
        /// <summary>
        /// 검사방법
        /// </summary>
        public string CheckProvNM { get; set; }
        /// <summary>
        /// 검사항목
        /// </summary>
        public string CheckNameNM { get; set; }
        /// <summary>
        /// 검사항목 + 규격
        /// </summary>
        public string CehckNameStand { get; set; }
        public string Reading1 { get; set; }
        public string Reading2 { get; set; }
        public string Reading3 { get; set; }
        public string Reading4 { get; set; }
        public string Reading5 { get; set; }
        public string Reading6 { get; set; }
        public string Reading7 { get; set; }
        public string Reading8 { get; set; }
        public string Reading9 { get; set; }
        /// <summary>
        /// 규격
        /// </summary>
        public string CheckStand { get; set; }
        /// <summary>
        /// 판정
        /// </summary>
        public string Judge { get; set; }
        /// <summary>
        /// 양품수량
        /// </summary>
        public int? OkQty { get; set; }
    }
}
