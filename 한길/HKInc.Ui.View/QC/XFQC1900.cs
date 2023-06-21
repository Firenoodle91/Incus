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
using HKInc.Service.Handler;

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// LOT 추적관리화면
    /// </summary>
    public partial class XFQC1900 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOTTRACKING> ModelService = (IService<VI_LOTTRACKING>)ProductionFactory.GetDomainService("VI_LOTTRACKING");
        public XFQC1900()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;

        }
        
        protected override void InitCombo()
        {
            lupItem.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {

            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "포장라벨생성[F3]");
            GridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, false);
            GridExControl.MainGrid.AddColumn("WorkNo","작업지시번호");
            GridExControl.MainGrid.AddColumn("Seq",false);
            GridExControl.MainGrid.AddColumn("ItemCode","품목코드", false);
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("ProcessCode","공정");
            GridExControl.MainGrid.AddColumn("ProcessTurn", "공정순서", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("LotNo", "생산 LOT NO");
            GridExControl.MainGrid.AddColumn("PackLotNo", "포장 LOT NO");
            GridExControl.MainGrid.AddColumn("StartDate", "작업시작일시");
            GridExControl.MainGrid.AddColumn("EndDate", "작업종료일시");
            GridExControl.MainGrid.AddColumn("ResultDate","마지막 작업일");
            GridExControl.MainGrid.AddColumn("ResultQty","생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("OkQty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FailQty","불량수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("PackQty", "포장수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("WorkId","작업자");            
            GridExControl.MainGrid.AddColumn("McCode","설비");
            GridExControl.MainGrid.AddColumn("SrcCode","원소재");
            GridExControl.MainGrid.AddColumn("SrcLot","원소재 LOT NO");
            //GridExControl.MainGrid.AddColumn("SrcCode1", "원소재2");
            //GridExControl.MainGrid.AddColumn("SrcLot1", "원소재LOT2");
            //GridExControl.MainGrid.AddColumn("SrcCode2", "원소재3");
            //GridExControl.MainGrid.AddColumn("SrcLot2", "원소재LOT3");
            //GridExControl.MainGrid.AddColumn("SrcCode3", "원소재4");
            //GridExControl.MainGrid.AddColumn("SrcLot3", "원소재LOT4");
            GridExControl.MainGrid.AddColumn("KnifeCode1", "칼");
            //GridExControl.MainGrid.AddColumn("KnifeCode2", "칼2");
            //GridExControl.MainGrid.AddColumn("KnifeCode3", "칼3");
            //GridExControl.MainGrid.AddColumn("KnifeCode4", "칼4");
            //GridExControl.MainGrid.AddColumn("SrcCode4", "원소재5");
            //GridExControl.MainGrid.AddColumn("SrcLot4", "원소재LOT5");
            //GridExControl.MainGrid.AddColumn("SrcCode5", "원소재6");
            //GridExControl.MainGrid.AddColumn("SrcLot5", "원소재LOT6");
            //GridExControl.MainGrid.AddColumn("SrcCode6", "원소재7");
            //GridExControl.MainGrid.AddColumn("SrcLot6", "원소재LOT7");
            //GridExControl.MainGrid.AddColumn("SrcCode7", "원소재8");
            //GridExControl.MainGrid.AddColumn("SrcLot7", "원소재LOT8");
            GridExControl.BestFitColumns();
        }

        protected override void GridRowDoubleClicked()
        {
            
        }
        protected override void InitRepository()
        {
            var ItemList = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList();
            var KnifeList = ModelService.GetChildList<TN_KNIFE001>(p => p.UseYN == "Y").OrderBy(o => o.KnifeName).ToList();
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StartDate");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("EndDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode", ItemList, "ItemCode", "ItemNm1");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode1", ItemList, "ItemCode", "ItemNm1");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode2", ItemList, "ItemCode", "ItemNm1");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode3", ItemList, "ItemCode", "ItemNm1");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("KnifeCode1", KnifeList, "KnifeCode", "KnifeName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("KnifeCode2", KnifeList, "KnifeCode", "KnifeName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("KnifeCode3", KnifeList, "KnifeCode", "KnifeName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("KnifeCode4", KnifeList, "KnifeCode", "KnifeName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode4", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode5", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode6", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode7", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("McCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");


        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            //string item = lupItem.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            string lot = tx_LotNo.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = ModelService.GetList(p => (p.TN_STD1100.ItemNm.Contains(Item) || p.TN_STD1100.ItemNm1.Contains(Item)) //(string.IsNullOrEmpty(item) ? true : p.ItemCode == item) 
                                                                  && (string.IsNullOrEmpty(lot) ? true : p.LotNo == lot)
                                                                  && (p.ResultDate >= dp_dt.DateFrEdit.DateTime 
                                                                   && p.ResultDate <= dp_dt.DateToEdit.DateTime)
                                                               )
                                                               .OrderBy(o => o.ItemCode)
                                                               .ThenBy(o => o.ProcessTurn)
                                                               .ThenBy(o => o.LotNo)
                                                               .ThenBy(p => p.PackLotNo.IsNullOrEmpty() ? 0 : p.PackLotNo.Length)
                                                               .ThenBy(p => p.PackLotNo)
                                                               .ToList();        
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }               

        protected override void AddRowClicked()
        {
            var obj = GridBindingSource.Current as VI_LOTTRACKING;
            if (obj == null) return;
            var objList = ((List<VI_LOTTRACKING>)GridBindingSource.List).Where(p => p.LotNo == obj.LotNo).ToList();

            int SumOkQty = 0;
            int SumPackQty = 0;
            foreach (var v in objList)
            {
                SumOkQty = v.OkQty.GetIntNullToZero();
                SumPackQty += v.PackQty.GetIntNullToZero();
            }

            int PossiblePackQty = SumOkQty - SumPackQty;
            if(PossiblePackQty <= 0)
            {
                MessageBoxHandler.Show("생성할 수 있는 양품수량이 없습니다.", "경고");
                return;
            }
            
            var tn1401 = ModelService.GetChildList<TN_MPS1401>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode).OrderBy(o => o.Seq).LastOrDefault();
            if (tn1401 == null)
            {
                MessageBoxHandler.Show("생성할 수 있는 실적이 없습니다.", "경고");
                return;
            }

            if (tn1401.LotNo.GetNullToEmpty() == "")
            {
                MessageBoxHandler.Show("생성할 수 있는 LOT NO가 없습니다.", "경고");
                return;
            }
            else
            {
                var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).First();

                POP_Popup.XPFPACKLABEL form = new POP_Popup.XPFPACKLABEL(PossiblePackQty, tn1401.LotNo, ItemObj.ItemNm1, ItemObj.ItemNm, tn1401, ItemObj.StdPackQty.GetIntNullToZero(), false);
                var value = form.ShowDialog();
                if (value == DialogResult.OK) ActRefresh();
            }
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var obj = GridBindingSource.Current as VI_LOTTRACKING;
            if (obj == null) return;

            if (obj.ProcessCode == MasterCodeSTR.ProcessPacking || obj.ProcessCode == MasterCodeSTR.ProcessCutToPacking || obj.ProcessCode == MasterCodeSTR.ProcessMakeToCutToPacking)
            {
                GridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, true);                
            }
            else
            {
                GridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, false);
            }
        }
    }
}