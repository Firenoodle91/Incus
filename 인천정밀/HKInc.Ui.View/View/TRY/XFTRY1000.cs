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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.View.TRY
{
    /// <summary>
    /// TRY관리
    /// </summary>
    public partial class XFTRY1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    { 
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFTRY1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList()); // 20210524 오세완 차장 반제품을 자사 / 타사로 나눠서 타사를 추가 처리
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1")); // 20210824 오세완 차장 품목명 누락 추가
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemCode", LabelConvert.GetLabelText("CustomerItemCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemName", LabelConvert.GetLabelText("CustomerItemName"));
            MasterGridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));

            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"));
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("OccurDate", LabelConvert.GetLabelText("OccurDate"));
            DetailGridExControl.MainGrid.AddColumn("OccurMemo", LabelConvert.GetLabelText("OccurMemo"));
            DetailGridExControl.MainGrid.AddColumn("ActionMemo", LabelConvert.GetLabelText("ActionMemo"));
            DetailGridExControl.MainGrid.AddColumn("ActionDate", LabelConvert.GetLabelText("ActionDate"));
            DetailGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            DetailGridExControl.MainGrid.AddColumn("TryId", LabelConvert.GetLabelText("TryId"));
            DetailGridExControl.MainGrid.AddColumn("TryQty", LabelConvert.GetLabelText("TryQty"));
            DetailGridExControl.MainGrid.AddColumn("TryTime", LabelConvert.GetLabelText("TryTime"));
            DetailGridExControl.MainGrid.AddColumn("Qc1", LabelConvert.GetLabelText("Qc1"));
            DetailGridExControl.MainGrid.AddColumn("Qc2", LabelConvert.GetLabelText("Qc2"));
            DetailGridExControl.MainGrid.AddColumn("TryResult", LabelConvert.GetLabelText("TryResult"));
            DetailGridExControl.MainGrid.AddColumn("ConfirmFlag", LabelConvert.GetLabelText("ConfirmFlag"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "OccurDate", "OccurMemo", "ActionMemo", "ActionDate", "Memo", "CustomerCode", "MachineCode", "TryId"
                                                                    , "TryQty","TryTime", "Qc1", "Qc2", "TryResult", "ConfirmFlag");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_TRY1000>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OccurDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["OccurMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "OccurMemo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["ActionMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "ActionMemo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("ActionDate");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("ConfirmFlag", "N");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TryResult", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TryId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("TryQty", DevExpress.Utils.DefaultBoolean.Default, "n2");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var processCode = lup_Process.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (string.IsNullOrEmpty(processCode) ? true : p.TN_TRY1000List.Any(c => c.ProcessCode == processCode))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).OrderBy(o => o.ItemCode).ToList(); // 20210524 오세완 차장 반제품을 자사 / 타사로 구분해서 조회도 같이 되게 설정처리
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            var processCode = lup_Process.EditValue.GetNullToEmpty();

            DetailGridBindingSource.DataSource = masterObj.TN_TRY1000List.Where(p => (string.IsNullOrEmpty(processCode) ? true : p.ProcessCode == processCode)).OrderBy(o => o.OccurDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            var newObj = new TN_TRY1000()
            {
                ItemCode = masterObj.ItemCode,
                Seq = masterObj.TN_TRY1000List.Count == 0 ? 1 : masterObj.TN_TRY1000List.Max(p => p.Seq) + 1
            };

            DetailGridBindingSource.Add(newObj);
            masterObj.TN_TRY1000List.Add(newObj);
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            var detailObj = DetailGridBindingSource.Current as TN_TRY1000;
            if (detailObj == null)
                return;

            DetailGridBindingSource.Remove(detailObj);
            masterObj.TN_TRY1000List.Remove(detailObj);
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }
    }
}