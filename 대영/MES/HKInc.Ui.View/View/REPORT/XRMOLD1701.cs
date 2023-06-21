using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.REPORT
{
    public partial class XRMOLD1701 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_MOLD1700> ModelService = (IService<TN_MOLD1700>)ProductionFactory.GetDomainService("TN_MOLD1700");

        public XRMOLD1701()
        {
            InitializeComponent();

            SetCellData();
            DataLoad();
        }

        private void DataLoad()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                SqlParameter param = new SqlParameter("@MoldCode", string.Empty);

                List<XFMOLD1701_DATA> result = context.Database.SqlQuery<XFMOLD1701_DATA>("EXEC USP_GET_XRMOLD1701 @MoldCode", param).ToList();

                bindingSource1.DataSource = result;
            }
        }
        private void SetCellData()
        {
            cell_DpmNo.Text = DbRequestHandler.GetSeqStandard("DPM01").ToString();
            cell_PrintDate.Text = DateTime.Today.ToShortDateString();
            cell_Year.Text = DateTime.Today.Year.ToString();
        }
    }

    public class XFMOLD1701_DATA
    {
        public long RowNum { get; set; }
        public string MoldMCode { get; set; }
        public string MoldCode { get; set; }
        public string MoldName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CarType { get; set; }
        /// <summary>누적타발수 실적</summary>
        public int? A1 { get; set; }
        /// <summary>누적타발수 배점</summary>
        public int? A2 { get; set; }
        /// <summary>금형사용량 실적</summary>
        public int? B1 { get; set; }
        /// <summary>금형사용량 배점</summary>
        public int? B2 { get; set; }
        /// <summary>품질문제이력 실적</summary>
        public int? C1 { get; set; }
        /// <summary>품질문제이력 배점</summary>
        public int? C2 { get; set; }
        /// <summary>종합점수</summary>
        public int? TotSrc { get; set; }
        /// <summary>등급</summary>
        public string Grade { get; set; }
        public string GradeName { get; set; }
    }
}
