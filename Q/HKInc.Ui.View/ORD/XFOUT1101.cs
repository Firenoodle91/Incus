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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.ORD
{
    public partial class XFOUT1101 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1101> ModelService = (IService<TN_STD1101>)ProductionFactory.GetDomainService("TN_STD1101");
     
        public XFOUT1101()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
         
        }


        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드",true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품목명", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("MainCust", "구매처", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("SrcCode", "원소재", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("StockQty", "재고", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("SafeQty", "안전재고", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("ItemImg", "이미지", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("Memo1", "비고1", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("Memo2", "비고2", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("Memo3", "비고3", true, HorzAlignment.Center);

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemNm", "MainCust", "Spec1", "Spec2", "Spec3", "Spec4","SrcCode", "SafeQty", "ItemImg", "Memo1", "Memo2", "Memo3" );
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("InoutDt", "년월", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Seq", false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);            
            DetailGridExControl.MainGrid.AddColumn("Inqty", "입고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Retqty", "조정량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고", true, HorzAlignment.Center);

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InoutDt", "Inqty", "Outqty", "Retqty", "Memo");

        }
        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(P=>P.UseFlag=="Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode", ModelService.GetChildList<TN_STD1100>(P => P.UseYn == "Y"&&(P.TopCategory=="P03"||P.TopCategory == "P02")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            MasterGridExControl.MainGrid.MainView.Columns["ItemImg"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(gridEx1, "ItemImg");
            //  DetailGridExControl.MainGrid.SetRepositoryItemMemoEdit("Memo");
            MasterGridExControl.MainGrid.MainView.Columns["Memo1"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.MainView.Columns["Memo2"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.MainView.Columns["Memo3"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
         
        }
        protected override void InitCombo()
        {
          

            lupItemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetList());
         
        }
        protected override void DataLoad()
        {
            
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string item = lupItemcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(item) ? true : p.ItemCode == item)).OrderBy(o => o.ItemCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

            LoadDetail();

            IsFormControlChanged = false;
        }
        protected override void MasterFocusedRowChanged()
        {
            LoadDetail();
        }
        private void LoadDetail()
        {
            try
            {
                DetailGridExControl.MainGrid.Clear();

                TN_STD1101 obj = MasterGridBindingSource.Current as TN_STD1101;
                if (obj == null) return;

                DetailGridBindingSource.DataSource = obj.OUT1101List;
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
            }
            finally
            {
                //SetRefreshMessage("OrderList", MasterGridExControl.MainGrid.RecordCount,
                //                  "DetailList", DetailGridExControl.MainGrid.RecordCount);
            }
        }
        protected override void AddRowClicked()
        {
            TN_STD1101 obj = new TN_STD1101() { ItemCode = DbRequestHandler.GetRequestNumberNew("SITEM") };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
        protected override void DeleteRow()
        {
            TN_STD1101 obj = MasterGridBindingSource.Current as TN_STD1101;
            if (obj.OUT1101List.Count >= 1)
            {
                MessageBox.Show("입출고 내역이 있어서 삭제할수 없습니다.");
            }
            else
            {
                MasterGridBindingSource.Remove(obj);
                ModelService.Delete(obj);
            }
        }
        protected override void DetailAddRowClicked()
        {
            TN_STD1101 obj = MasterGridBindingSource.Current as TN_STD1101;
            if (obj == null) return;
            TN_OUT1101 nobj = new TN_OUT1101() {ItemCode=obj.ItemCode ,InoutDt=DateTime.Now};
            DetailGridBindingSource.Add(nobj);
        }
        protected override void DeleteDetailRow()
        {
            DetailGridBindingSource.RemoveCurrent();
        }
        protected override void DataSave()
        {

            GridView gv = MasterGridExControl.MainGrid.MainView as GridView;
            for (int i = 0; i < gv.RowCount; i++)
            {
                string _file=gv.GetRowCellValue(i, gv.Columns["ItemImg"]).GetNullToEmpty();
                if (_file != "")
                {
                    string[] ChkQcfile1cha = _file.Split('\\');
                    if (ChkQcfile1cha.Length > 2)
                    {
                        FileHandler.UploadFile1(_file, GlobalVariable.FTP_SERVER + "SPART/");
                        gv.SetRowCellValue(i, gv.Columns["ItemImg"], "SPART/" + ChkQcfile1cha[ChkQcfile1cha.Length - 1]);
                    
                    }
                }
            }
            ModelService.Save();
            DataLoad();
        }
    }
}
