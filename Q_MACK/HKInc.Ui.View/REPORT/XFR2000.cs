using System;
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
    /// 납기준수율
    /// </summary>
    public partial class XFR2000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOTTRACKING> ModelService = (IService<VI_LOTTRACKING>)ProductionFactory.GetDomainService("VI_LOTTRACKING");
        public XFR2000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            
            dp_dt.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {
            // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략                      2022-02-23 김진우 ItemCodeColumnSetting 수정하여 원복       
            lupItem.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && 
                                                                                                       (p.TopCategory == MasterCodeSTR.Topcategory_Final_Product || p.TopCategory == MasterCodeSTR.Topcategory_Half_Product)).OrderBy(o => o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {

            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("OrderNo", "수주번호");
            GridExControl.MainGrid.AddColumn("Seq", "순번");
            GridExControl.MainGrid.AddColumn("CustCode", "거래처");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목");
            GridExControl.MainGrid.AddColumn("ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("OrderQty", "수주수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("PeriodDate", "납기일");
            GridExControl.MainGrid.AddColumn("OutDate", "최종출고일");
            GridExControl.MainGrid.AddColumn("OkQty", "납기준수출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FQty", "납기미준수출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("MQty", "미납품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Rat", "납기준수율",true, HorzAlignment.Far);
            GridExControl.MainGrid.AddColumn("TRat", "납품률", true, HorzAlignment.Far);
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string item = lupItem.EditValue.GetNullToEmpty();
            string dt = dp_dt.DateTime.ToShortDateString();
            
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var fdt = new SqlParameter("@YEAR", dt);
             
                var result = context.Database
                      .SqlQuery<TP_SALETODLVRAT>("SP_SALETODLV_RAT @YEAR", fdt).ToList();
                GridBindingSource.DataSource = result.Where(P=>String.IsNullOrEmpty(item)?true:P.ItemCode==item).OrderBy(o => o.PeriodDate).ToList();
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
            Chart1.DataSource = ds;
            Chart1.SeriesDataMember = "CustNm";
            Chart1.SeriesTemplate.ArgumentDataMember = "ItemNm";
            Chart1.SeriesTemplate.ValueDataMembers.AddRange("Rat1");
            Chart1.SeriesTemplate.Label.TextPattern = "{S}:{V:n0}";
            Chart1.CrosshairEnabled = DefaultBoolean.True;
            Chart1.CrosshairOptions.ShowValueLabels = true;

            XYDiagram diagram = (XYDiagram)Chart1.Diagram;

            diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            diagram.AxisY.Label.TextPattern = "{V:n0}";

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisX.VisualRange.Auto = true;
            diagram.AxisY.VisualRange.Auto = true;
          
            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }
      
    }
}