using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS1700 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<VI_SRC_STOCK_SUM> ModelService = (IService<VI_SRC_STOCK_SUM>)ProductionFactory.GetDomainService("VI_SRC_STOCK_SUM");
        public XFMPS1700()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
         

        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            VI_SRC_USE obj = DetailGridBindingSource.Current as VI_SRC_USE;
            if (obj.Memo.GetNullToEmpty() == "") return;
            if (e.Clicks >= 2)
            {
                MPS_Popup.XPFMPS1700 fm = new MPS_Popup.XPFMPS1700(obj);
                fm.ShowDialog();
                DataLoad();
            }
        }

        protected override void InitCombo()
        {
            lupitemcode.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"||p.TopCategory == "P02")).ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("SrcCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("SrcNm", "품명");                      
            MasterGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Useqty", "사용수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Stockqty", "재고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");


            DetailGridExControl.SetToolbarVisible(false);         
            DetailGridExControl.MainGrid.AddColumn("ResultDate", "입출고일");         
            DetailGridExControl.MainGrid.AddColumn("OutQty", "입고량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("UseQty", "출고량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고");

        }
        protected override void InitRepository()
        {
           
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcNm", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"||p.TopCategory == "P02")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
          


       
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
        }
        protected override void DataLoad()
        {
            ModelService.ReLoad();
            //  DetailGridExControl.DataSource = null;
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            string itemcode = lupitemcode.EditValue.GetNullToEmpty();
            
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>string.IsNullOrEmpty(itemcode)?true:p.SrcCode==itemcode).ToList();                                                  
            //MasterGridBindingSource.DataSource = ModelService.GetList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void MasterFocusedRowChanged()
        {
            VI_SRC_STOCK_SUM obj = MasterGridBindingSource.Current as VI_SRC_STOCK_SUM;
            if (obj == null)
            {
                DetailGridExControl.DataSource = null;
                return;
            }
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_SRC_USE>(p => p.SrcCode == obj.SrcCode).OrderBy(o => o.ResultDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            VI_SRC_STOCK_SUM obj = MasterGridBindingSource.Current as VI_SRC_STOCK_SUM;
            if (obj == null) return;
            MPS_Popup.XPFMPS1700 fm = new MPS_Popup.XPFMPS1700(obj.SrcCode);
            fm.ShowDialog();
            DataLoad();
        }
    }
}
