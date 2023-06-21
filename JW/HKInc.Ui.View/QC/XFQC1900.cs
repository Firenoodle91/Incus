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

namespace HKInc.Ui.View.QC
{
    public partial class XFQC1900 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOTTRACKING> ModelService = (IService<VI_LOTTRACKING>)ProductionFactory.GetDomainService("VI_LOTTRACKING");
        public XFQC1900()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;

        }
        protected override void InitCombo()
        {
            lupItem.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P01" || p.TopCategory == "P05")).OrderBy(o=>o.ItemNm1).ToList());
        }
        protected override void InitGrid()
        {

            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("WorkNo","작업지시번호");
            GridExControl.MainGrid.AddColumn("Seq",false);
            GridExControl.MainGrid.AddColumn("ItemCode","품목");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            GridExControl.MainGrid.AddColumn("ProcessCode","공정");
            GridExControl.MainGrid.AddColumn("ProcessTurn","공정순서");
            GridExControl.MainGrid.AddColumn("LotNo","LOTNO");
            GridExControl.MainGrid.AddColumn("StartDate", "작업시작일시");
            GridExControl.MainGrid.AddColumn("EndDate", "작업종료일시");
            GridExControl.MainGrid.AddColumn("ResultDate","마지막 작업일");
            GridExControl.MainGrid.AddColumn("ResultQty","생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FailQty","불량수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("OkQty","양품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("WorkId","작업자");            
            GridExControl.MainGrid.AddColumn("McCode","설비");
            GridExControl.MainGrid.AddColumn("SrcCode","원소재1");
            GridExControl.MainGrid.AddColumn("SrcLot","원소재LOT1");
            GridExControl.MainGrid.AddColumn("SrcCode1", "원소재2");
            GridExControl.MainGrid.AddColumn("SrcLot1", "원소재LOT2");
            GridExControl.MainGrid.AddColumn("SrcCode2", "원소재3");
            GridExControl.MainGrid.AddColumn("SrcLot2", "원소재LOT3");
            GridExControl.MainGrid.AddColumn("SrcCode3", "원소재4");
            GridExControl.MainGrid.AddColumn("SrcLot3", "원소재LOT4");
            GridExControl.MainGrid.AddColumn("SrcCode4", "원소재5");
            GridExControl.MainGrid.AddColumn("SrcLot4", "원소재LOT5");
            GridExControl.MainGrid.AddColumn("SrcCode5", "원소재6");
            GridExControl.MainGrid.AddColumn("SrcLot5", "원소재LOT6");
            GridExControl.MainGrid.AddColumn("SrcCode6", "원소재7");
            GridExControl.MainGrid.AddColumn("SrcLot6", "원소재LOT7");
            GridExControl.MainGrid.AddColumn("SrcCode7", "원소재8");
            GridExControl.MainGrid.AddColumn("SrcLot7", "원소재LOT8");
            GridExControl.BestFitColumns();
        }
        protected override void GridRowDoubleClicked()
        {
            
        }
        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StartDate");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("EndDate");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode2", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode3", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode4", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode5", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode6", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode7", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
          //  GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("McCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");


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
            string lot = tx_LotNo.EditValue.GetNullToEmpty();
                GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(item)?true:p.ItemCode==item)&&(string.IsNullOrEmpty(lot)?true:p.LotNo==lot)
                                                           &&(p.ResultDate>=dp_dt.DateFrEdit.DateTime&&p.ResultDate<=dp_dt.DateToEdit.DateTime))                
                                                           .OrderBy(o=>o.ItemCode).OrderBy(o => o.ProcessTurn).OrderBy(o=>o.LotNo)
                                                          .ToList();
        
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

      
    }
}