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
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.PUR
{
    /// <summary>
    /// 외주재고관리
    /// </summary>
    public partial class XFPUR1900 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PUR1900V1> ModelService = (IService<VI_PUR1900V1>)ProductionFactory.GetDomainService("VI_PUR1900V1");

        public XFPUR1900()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
        }

        protected override void InitCombo()
        {
            lupcustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(p => p.CustomerName).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarVisible(false);
            //MasterGridExControl.SetToolbarButtonVisible(false);
            //MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("PoNo", "발주번호", HorzAlignment.Center,true);
            MasterGridExControl.MainGrid.AddColumn("PoDate", "발주일", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("PoId", "발주자", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("InDuedate", "입고예정일", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("InNo", "입고번호", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("InDate", "입고일", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("InId", "입고자", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("InSre", "상태", HorzAlignment.Center, true);
       
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.Init();
            //DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("PoNo", "발주번호", HorzAlignment.Center, false);            
            DetailGridExControl.MainGrid.AddColumn("PoSeq", "발주순번", HorzAlignment.Center, false);
            DetailGridExControl.MainGrid.AddColumn("InNo", "입고번호", HorzAlignment.Center, false);
            DetailGridExControl.MainGrid.AddColumn("InSeq", "입고순번", HorzAlignment.Center, false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("PoQty", "발주수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "N0");
            DetailGridExControl.MainGrid.AddColumn("Qty", "미입고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "N0");
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InSre", "상태", HorzAlignment.Center, true);

            DetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode",  ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(),"CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PoDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InSre", DbRequestHandler.GetCommCode(MasterCodeSTR.INPUTSTATUS), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InSre", MasterCode.GetMasterCode((int)MasterCodeEnum.INPUTSTATUS).ToList());          // 상태값 출력 오류 수정 2022-07-14 김진우

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemTextEdit("PoQty", 0, DefaultBoolean.Default, MaskType.Numeric, "n0", true);
            DetailGridExControl.MainGrid.SetRepositoryItemTextEdit("InQty", 0, DefaultBoolean.Default, MaskType.Numeric, "n0", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InSre", DbRequestHandler.GetCommCode(MasterCodeSTR.INPUTSTATUS), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InSre", MasterCode.GetMasterCode((int)MasterCodeEnum.INPUTSTATUS).ToList());          // 상태값 출력 오류 수정 2022-07-14 김진우
        }

        protected override void DataLoad()
        {

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string cust = lupcustcode.EditValue.GetNullToEmpty();
            string pono = tx_pono.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                            p.InDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                            &&(string.IsNullOrEmpty(cust)?true:p.CustCode==cust)
                                                                            &&(string.IsNullOrEmpty(pono)?true:p.PoNo==pono)                                                                           
                                                                            ).OrderBy(o => o.InNo).ToList();
                                                                    //).GroupBy(g => g.OrderNo).Select(s => s.First()).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            VI_PUR1900V1 obj = MasterGridBindingSource.Current as VI_PUR1900V1;

            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_PUR1900V2>(p => p.PoNo == obj.PoNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

    }
}
