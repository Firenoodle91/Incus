using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using System.Data.SqlClient;

namespace HKInc.Ui.View.PUR
{
    /// <summary>
    /// 20220414 오세완 차장
    /// 재고조정이 적용되는 자재재고관리 
    /// </summary>
    public partial class XFPUR1500_V2 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<VI_PURINOUT_V2> ModelService = (IService<VI_PURINOUT_V2>)ProductionFactory.GetDomainService("VI_PURINOUT_V2");
        #endregion

        public XFPUR1500_V2()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            lupitemtype.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype,2));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ITEM_NM", "품목");                  
            MasterGridExControl.MainGrid.AddColumn("ITEM_NM1", "품번");                 
            MasterGridExControl.MainGrid.AddColumn("TOP_CATEGORY_NAME", "대분류");            
            MasterGridExControl.MainGrid.AddColumn("MIDDLE_CATEGORY_NAME", "중분류");         

            MasterGridExControl.MainGrid.AddColumn("BOTTOM_CATEGORY_NAME", "차종");           
            MasterGridExControl.MainGrid.AddColumn("UNIT_NAME", "단위");                     
            MasterGridExControl.MainGrid.AddColumn("SAFE_QTY", "안전재고", HorzAlignment.Far, FormatType.Numeric, "N2"); 
            MasterGridExControl.MainGrid.AddColumn("SUM_INPUT", "입고수량", HorzAlignment.Far, FormatType.Numeric, "N2");
            MasterGridExControl.MainGrid.AddColumn("SUM_OUTPUT", "출고수량", HorzAlignment.Far, FormatType.Numeric, "N2");

            MasterGridExControl.MainGrid.AddColumn("TOTAL_QTY", "재고수량", HorzAlignment.Far, FormatType.Numeric, "N2"); 

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("InputDate", "입출고일", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("ItemNm", "품목");
            DetailGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("InputQty", "입고량", HorzAlignment.Far, FormatType.Numeric, "N2"); 

            DetailGridExControl.MainGrid.AddColumn("OutputQty", "출고량", HorzAlignment.Far, FormatType.Numeric, "N2");
            DetailGridExControl.MainGrid.AddColumn("UserName", "입출고자");
            DetailGridExControl.MainGrid.AddColumn("WhCodeName", "입고창고");
            DetailGridExControl.MainGrid.AddColumn("WhPositionName", "위치");
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            MasterGridExControl.MainGrid.Clear();

            string sMiddlecategory_code = lupitemtype.EditValue.GetNullToEmpty();
            string sItemcode = tx_Item.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Middlecategory = new SqlParameter("@MIDDLE_CATEGORY", sMiddlecategory_code);
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", sItemcode);

                var vResult = context.Database.SqlQuery<TP_XFPUR1500_LIST>("USP_GET_XFPUR1500_LIST @MIDDLE_CATEGORY, @ITEM_CODE", sp_Middlecategory, sp_Itemcode).ToList();
                if (vResult == null)
                    MasterGridBindingSource.Clear();
                else if(vResult.Count == 0)
                    MasterGridBindingSource.Clear();
                else
                {
                    vResult = vResult.OrderBy(o => o.ITEM_CODE).ToList();
                    MasterGridBindingSource.DataSource = vResult;
                }
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            SetRefreshMessage(MasterGridBindingSource.Count);
        }

        protected override void MasterFocusedRowChanged()
        {
            TP_XFPUR1500_LIST obj = MasterGridBindingSource.Current as TP_XFPUR1500_LIST;
            if (obj == null)
            {
                DetailGridBindingSource.Clear();
            }
            else
            {
                List<VI_PURINOUT_V2> detail_Arr = ModelService.GetList(p => p.ItemCode == obj.ITEM_CODE);
                if(detail_Arr != null)
                    if(detail_Arr.Count > 0)
                    {
                        detail_Arr = detail_Arr.OrderBy(p => p.InputDate).ToList();
                        DetailGridBindingSource.DataSource = detail_Arr;
                    }
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            SetRefreshMessage(DetailGridBindingSource.Count);
        }

        /// <summary>
        /// EXCEL 출력
        /// 2022-04-01 김진우  TN_STD1100. 추가
        /// </summary>
        protected override void DataExport()
        {
            DialogResult dlg = MessageBox.Show("선택하신 정보만 엑셀변환 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (dlg == DialogResult.Yes)
            {
                HKInc.Service.Controls.GridEx gv = new HKInc.Service.Controls.GridEx();
                BindingSource gv1 = new BindingSource();
                gv.MainGrid.AddColumn("ITEM_CODE", "품목코드");
                gv.MainGrid.AddColumn("ITEM_NM", "품목");
                gv.MainGrid.AddColumn("ITEM_NM1", "품번");
                gv.MainGrid.AddColumn("TOP_CATEGORY_NAME", "대분류");
                gv.MainGrid.AddColumn("MIDDLE_CATEGORY_NAME", "중분류");
                gv.MainGrid.AddColumn("BOTTOM_CATEGORY_NAME", "차종");
                gv.MainGrid.AddColumn("UNIT_NAME", "단위");
                gv.MainGrid.AddColumn("SAFE_QTY", "안전재고", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("SUM_INPUT", "입고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("SUM_OUTPUT", "출고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("TOTAL_QTY", "재고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv1.DataSource = MasterGridBindingSource.Current as TP_XFPUR1500_LIST;
                gv.DataSource = gv1;
                HKInc.Service.Helper.ExcelExport.ExportToExcel(gv.MainGrid.MainView, DetailGridExControl.MainGrid.MainView);
            }
            else { base.DataExport(); }
        }
    }
}
