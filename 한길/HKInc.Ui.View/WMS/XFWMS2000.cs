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
    /// 위치별 재고현황화면
    /// </summary>
    public partial class XFWMS2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_WMS2000> ModelService = (IService<TN_WMS2000>)ProductionFactory.GetDomainService("TN_WMS2000");

        protected HKInc.Service.Controls.GridEx subDetailGridExControl;

        public XFWMS2000()
        {
            InitializeComponent();
            
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            subDetailGridExControl = gridEx3;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;    
        }

        protected override void InitCombo()
        {
            lupWHCODE.SetDefault(true, "WhCode", "WhName", ModelService.GetChildList<TN_WMS1000>(p=>p.UseYn=="Y").ToList());
            lup_item.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);           
            MasterGridExControl.MainGrid.AddColumn("WhCode", "창고코드");
            MasterGridExControl.MainGrid.AddColumn("TN_WMS1000.WhName", "창고명");
            MasterGridExControl.MainGrid.AddColumn("PosionCode", "창고위치");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("whcode", "창고명");
            //DetailGridExControl.MainGrid.AddColumn("WhName", "창고명");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "창고위치");
            DetailGridExControl.MainGrid.AddColumn("Itemcode", "품목코드", false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("qty", "수량", HorzAlignment.Far, FormatType.Numeric, "n0");

            subDetailGridExControl.SetToolbarVisible(false);
            subDetailGridExControl.MainGrid.AddColumn("WhCode", "창고명");
            //subDetailGridExControl.MainGrid.AddColumn("WhName", "창고코드");
            subDetailGridExControl.MainGrid.AddColumn("WhPosition", "창고위치");
            subDetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            subDetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            subDetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            subDetailGridExControl.MainGrid.AddColumn("LotNo", "LOT NO");
            subDetailGridExControl.MainGrid.AddColumn("Qty", "수량", HorzAlignment.Far, FormatType.Numeric, "n0");
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("whcode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            subDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
        }

        protected override void DataLoad()
        {            
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            subDetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string cta = lupWHCODE.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(cta) ? true : p.WhCode == cta)
                                                             .OrderBy(O => O.PosionC)
                                                             .ThenBy(O => O.PosionB)
                                                             .ThenBy(O => O.PosionA)
                                                             .ThenBy(p => p.WhCode)
                                                             .ToList();        
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
           
            //string Item = lup_item.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_WHPOSITON_QTY_D1>(p => (string.IsNullOrEmpty(cta) ? true : p.whcode == cta) 
                                                                                                  && (p.TN_STD1100.ItemNm.Contains(Item) || p.TN_STD1100.ItemNm1.Contains(Item))
                                                                                               )
                                                                                               .OrderBy(o => o.whcode)
                                                                                               .ThenBy(o => o.Itemcode)
                                                                                               .ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (MasterGridExControl.MainGrid.MainView.RowCount == 0)
            {
                subDetailGridExControl.MainGrid.Clear();
            }
            else
            {
                TN_WMS2000 obj = MasterGridBindingSource.Current as TN_WMS2000;
                subDetailGridExControl.MainGrid.Clear();
                bindingSource1.DataSource = ModelService.GetChildList<VI_WHPOSITON_QTY_D2>(p => p.WhCode == obj.WhCode && p.WhPosition == obj.PosionCode).OrderBy(o => o.ItemCode).ToList();
                subDetailGridExControl.DataSource = bindingSource1;
                subDetailGridExControl.BestFitColumns();
            }
        }
    }
}
