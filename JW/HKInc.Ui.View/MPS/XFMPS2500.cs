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
    public partial class XFMPS2500 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<VI_MPS1401V> ModelService = (IService<VI_MPS1401V>)ProductionFactory.GetDomainService("VI_MPS1401V");
        IService<VI_MPS1405V> ModelServiceDtl = (IService<VI_MPS1405V>)ProductionFactory.GetDomainService("VI_MPS1405V");
        public XFMPS2500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today;

        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            VI_MPS1401V obj = MasterGridBindingSource.Current as VI_MPS1401V;
            GridView gv = sender as GridView;
            int qty = 0;
            if (e.Column.Name.ToString() != "ResultQty")
            {
                if (e.Column.Name.ToString() == "OkQty")
                {
                    qty = gv.GetFocusedRowCellValue(gv.Columns["OkQty"]).GetIntNullToZero() + gv.GetFocusedRowCellValue(gv.Columns["FailQty"]).GetIntNullToZero();

                    int rqty = 0;
                    int okqty = 0;
                    int fqty = 0;
                    for (int i = 0; i < gv.RowCount; i++)
                    {
                        rqty += gv.GetRowCellValue(i, gv.Columns["OkQty"]).GetIntNullToZero() + gv.GetRowCellValue(i, gv.Columns["FailQty"]).GetIntNullToZero();
                        okqty += gv.GetRowCellValue(i, gv.Columns["OkQty"]).GetIntNullToZero();
                        fqty += gv.GetRowCellValue(i, gv.Columns["FailQty"]).GetIntNullToZero();
                    }
                    obj.ResultQty = rqty;
                    obj.OkQty = okqty;
                    obj.FailQty = fqty;
                    gv.SetFocusedRowCellValue(gv.Columns["ResultQty"], qty.ToString());
                }
                if (e.Column.Name.ToString() == "FailQty")
                {
                    qty = gv.GetFocusedRowCellValue(gv.Columns["OkQty"]).GetIntNullToZero() + gv.GetFocusedRowCellValue(gv.Columns["FailQty"]).GetIntNullToZero();

                    int rqty = 0;
                    int okqty = 0;
                    int fqty = 0;
                    for (int i = 0; i < gv.RowCount; i++)
                    {
                        rqty += gv.GetRowCellValue(i, gv.Columns["OkQty"]).GetIntNullToZero() + gv.GetRowCellValue(i, gv.Columns["FailQty"]).GetIntNullToZero();
                        okqty +=  gv.GetRowCellValue(i, gv.Columns["OkQty"]).GetIntNullToZero();
                        fqty += gv.GetRowCellValue(i, gv.Columns["FailQty"]).GetIntNullToZero();
                    }
                    obj.ResultQty = rqty;
                    obj.OkQty = okqty;
                    obj.FailQty = fqty;
                    gv.SetFocusedRowCellValue(gv.Columns["ResultQty"], qty.ToString());
                }

                MasterGridBindingSource.EndEdit();
                MasterGridExControl.MainGrid.BestFitColumns();
            }
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
          
        }

        protected override void InitCombo()
        {
            //lupitemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p=>(p.TopCategory=="P03"||p.TopCategory == "P02")).ToList());
            lupitemcode.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p => (p.TopCategory == "P03" || p.TopCategory == "P02")).ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일");
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            MasterGridExControl.MainGrid.AddColumn("LotNo", "LotNo");
            MasterGridExControl.MainGrid.AddColumn("ResultQty", "생산수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");
            MasterGridExControl.MainGrid.AddColumn("OkQty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");
            MasterGridExControl.MainGrid.AddColumn("FailQty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");
            


            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("LotNo", "LotNo", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ResultDate", "작업일", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ResultQty", "생산수량", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("OkQty", "양품수량", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("FailQty", "불량수량", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("WorkId", "작업자", HorzAlignment.Center, true);
    //        DetailGridExControl.MainGrid.AddColumn("TypeCode", "주야간", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OkQty", "FailQty","WorkId");

        }
        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => 1 == 1).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");

            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => 1 == 1).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("TypeCode", DbRequesHandler.GetCommCode(MasterCodeSTR.DNTYPE), "Mcode", "Codename");
        }
        protected override void DataLoad()
        {
            ModelService.ReLoad();
            //  DetailGridExControl.DataSource = null;
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            string itemcode = lupitemcode.EditValue.GetNullToEmpty();
            string workno = TX_WorkNo.EditValue.GetNullToEmpty();
            string lot = tx_lotno.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>(p.WorkDate>=datePeriodEditEx1.DateFrEdit.DateTime&&p.WorkDate<=datePeriodEditEx1.DateToEdit.DateTime )&&(string.IsNullOrEmpty(itemcode)?true:p.ItemCode==itemcode)&&(string.IsNullOrEmpty(workno)?true:p.WorkNo.Contains(workno))
            &&(string.IsNullOrEmpty(lot)?true:p.LotNo==lot)).ToList();                                                  
            //MasterGridBindingSource.DataSource = ModelService.GetList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void MasterFocusedRowChanged()
        {
            VI_MPS1401V obj = MasterGridBindingSource.Current as VI_MPS1401V;
            if (obj == null) return;
            if (DetailGridBindingSource.Count != 0)
            {
                MasterGridBindingSource.EndEdit();
                DetailGridBindingSource.EndEdit();
                ModelService.Save();
                ModelServiceDtl.Save();
            }
            ModelServiceDtl.ReLoad();
            DetailGridBindingSource.DataSource = ModelServiceDtl.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.LotNo == obj.LotNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
            ModelServiceDtl.Save();
            DataLoad();

        }
    }
}
