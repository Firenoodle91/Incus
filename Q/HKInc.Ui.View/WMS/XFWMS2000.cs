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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.WMS
{
    /// <summary>
    /// 2021-10-25  김진우 주임 추가
    /// </summary>
    public partial class XFWMS2000 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_WMS2000> ModelService = (IService<TN_WMS2000>)ProductionFactory.GetDomainService("TN_WMS2000");

        public XFWMS2000()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
        }

        protected override void InitCombo()
        {
            lupWHCODE.SetDefault(true, "WhCode", "WhName", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList());
            lup_item.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WhCode", "창고코드");
            MasterGridExControl.MainGrid.AddColumn("WhPositionCode", "창고위치코드");
            MasterGridExControl.MainGrid.AddColumn("WhPositionName", "창고위치명");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고코드");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "제품코드");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "창고위치");
            DetailGridExControl.MainGrid.AddColumn("Qty", "수량");

            SubDetailGridExControl.SetToolbarVisible(false);
            SubDetailGridExControl.MainGrid.AddColumn("WhCode", "창고코드");
            SubDetailGridExControl.MainGrid.AddColumn("WhPosition", "창고위치");
            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", "제품코드");
            SubDetailGridExControl.MainGrid.AddColumn("LotNo", "LOT NO");
            SubDetailGridExControl.MainGrid.AddColumn("Qty", "수량");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");

            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");

            SubDetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            SubDetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string WhCode = lupWHCODE.EditValue.GetNullToEmpty();
            string Item = lup_item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(WhCode) ? true : p.WhCode == WhCode)
                                                                    .OrderBy(O => O.PositionC)
                                                                    .OrderBy(O => O.PositionB)
                                                                    .OrderBy(O => O.PositionA)
                                                                    .OrderBy(p => p.WhCode)
                                                                    //  .OrderBy(p => p.WhCode).OrderBy(O=>O.PosionA).OrderBy(O => O.PosionB).OrderBy(O => O.7C)
                                                                    .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            #region 주석

            //var varlist = DbRequesHandler.GetDTselect("select   [wh_code] as WhCode      ,[Item_code] as ItemCode      ,[qty] as Qty      ,[WH_POSITION] as WhPosition from VI_WHPOSITON_QTY_D1");
            //    DetailGridBindingSource.DataSource=varlist.where()
            //  List<VI_WHPOSITON_QTY_D1> var = ModelService.GetChildList<VI_WHPOSITON_QTY_D1>(p=>1 == 1).ToList();
            #endregion

            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_WHPOSITION_QTY_D1>(p => (string.IsNullOrEmpty(WhCode) ? true : p.WhCode == WhCode)
                                                                                                   && (string.IsNullOrEmpty(Item) ? true : p.ItemCode == Item))
                                                                                                      .OrderBy(o => o.WhCode)
                                                                                                      .OrderBy(o => o.ItemCode)
                                                                                                      .ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            //SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            //SubDetailGridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            if (MasterGridExControl.MainGrid.MainView.RowCount == 0)
            {
                DetailGridExControl.MainGrid.Clear();
                SubDetailGridExControl.MainGrid.Clear();
                SubDetailGridExControl.DataSource = null;
            }
            else
            {
                TN_WMS2000 obj = MasterGridBindingSource.Current as TN_WMS2000;
                //  DetailGridExControl.MainGrid.Clear();
                SubDetailGridExControl.MainGrid.Clear();

                //SubDetailGridBindingSource.DataSource = ModelService.GetChildList<VI_WHPOSITION_QTY_D2>(p => true)
                //                                                                                        //.OrderBy(o => o.ItemCode)
                //                                                                                        .ToList();

                SubDetailGridBindingSource.DataSource = ModelService.GetChildList<VI_WHPOSITION_QTY_D2>(p => (p.WhCode == obj.WhCode)
                                                                                                          && (p.WhPosition == obj.WhPositionCode))
                                                                                                        .OrderBy(o => o.ItemCode)
                                                                                                        .ToList();

                //int a = ModelService.GetChildList<VI_WHPOSITION_QTY_D2>(p => true).ToList().Count();

                SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
                SubDetailGridExControl.BestFitColumns();
            }
        }
    }
}
