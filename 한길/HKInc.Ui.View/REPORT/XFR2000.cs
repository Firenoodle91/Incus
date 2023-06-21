﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using System.Data.SqlClient;
using DevExpress.XtraCharts;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 납기준수화면
    /// </summary>
    public partial class XFR2000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOTTRACKING> ModelService = (IService<VI_LOTTRACKING>)ProductionFactory.GetDomainService("VI_LOTTRACKING");

        public XFR2000()
        {
            InitializeComponent();
            GridExControl = gridEx1;            
            dp_dt.DateTime = DateTime.Today;
            Chart1.BoundDataChanged += Chart1_BoundDataChanged;

        }

        protected override void InitCombo()
        {
            lupItem.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory!="P03").OrderBy(o=>o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("OrderNo", "수주번호");
            GridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("CustCode", "거래처");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목");
            GridExControl.MainGrid.AddColumn("OrderQty", "수주수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("PeriodDate", "납기일");
            GridExControl.MainGrid.AddColumn("OutDate", "최종출고일");
            GridExControl.MainGrid.AddColumn("OkQty", "납기준수출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FQty", "납기미준수출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("MQty", "미납품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("Rat", "납기준수율",true, HorzAlignment.Far);
            //GridExControl.MainGrid.AddColumn("TRat", "납품률", true, HorzAlignment.Far);
            GridExControl.MainGrid.AddColumn("Rat1", "납품률", HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");

        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            //string item = lupItem.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            string dt = dp_dt.DateTime.ToShortDateString();
            
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var fdt = new SqlParameter("@YEAR", dt);
                var result = context.Database
                      .SqlQuery<TP_SALETODLVRAT>("SP_SALETODLV_RAT @YEAR", fdt).ToList();
                GridBindingSource.DataSource = result.Where(P => P.ItemNm.Contains(Item) || P.ItemNm1.Contains(Item)).OrderBy(o => o.PeriodDate).ToList();
            }
           
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            List<TP_SALETODLVRAT> ds = GridBindingSource.DataSource as List<TP_SALETODLVRAT>;
            //타이틀설정
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DefaultBoolean.False;
            //chartTitle1.Text = "타이틀제목";
            //chartTitle1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //chartTitle1.TextColor = Color.Black;
            //ChartControls[TabPageIndex].Titles.Clear();
            //ChartControls[TabPageIndex].Titles.Add(chartTitle1);

            //X축설정 (Series)
            Chart1.DataSource = ds.GroupBy(p => new { p.CustNm, p.ItemNm }).Select(p => new TP_SALETODLVRAT
            {
                CustNm = p.Key.CustNm
                 , ItemNm = p.Key.ItemNm
                 , Rat1 = p.Sum(c => c.OrderQty).GetDecimalNullToZero() == 0 ? 0 : (p.Sum(c => c.OkQty) / p.Sum(c => c.OrderQty).GetDecimalNullToZero()) * 100
            }).ToList();
            Chart1.SeriesDataMember = "CustNm";
            Chart1.SeriesTemplate.ArgumentDataMember = "ItemNm";
            Chart1.SeriesTemplate.ValueDataMembers.AddRange("Rat1");
            Chart1.SeriesTemplate.Label.TextPattern = "{S}:{V:n2}%";
            Chart1.CrosshairEnabled = DefaultBoolean.True;
            Chart1.CrosshairOptions.ShowValueLabels = true;

            XYDiagram diagram = (XYDiagram)Chart1.Diagram;

            diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            diagram.AxisY.Label.TextPattern = "{V:n2}%";

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisX.VisualRange.Auto = true;
            diagram.AxisY.VisualRange.Auto = true;

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        private void Chart1_BoundDataChanged(object sender, EventArgs e)
        {
            foreach (Series series in this.Chart1.Series)
            {
                series.CrosshairLabelPattern = "{S}:{V:n2}%";
            }
        }
    }
}