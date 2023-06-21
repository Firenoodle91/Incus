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
using HKInc.Service.Base;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using DevExpress.XtraGrid.Views.Base;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.View.REPORT;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS1700 : ListMasterDetailFormTemplate
    {
        IService<TN_MPS1700> ModelService = (IService<TN_MPS1700>)ProductionFactory.GetDomainService("TN_MPS1700");

        public XFMPS1700()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitControls()
        {
            base.InitControls();
            datePeriodEditEx1.SetTodayIsWeek();
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShownEditor += MainView_ShownEditor;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "포장라벨출력[F3]", IconImageList.GetIconImage("print/printer"));
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "일괄 입고[F10]", IconImageList.GetIconImage("actions/addfile"));
            MasterGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "PackLotNo", true);
            MasterGridExControl.MainGrid.AddColumn("_Check", "선택");
            MasterGridExControl.MainGrid.AddColumn("ResultDate", "생산일", HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("LotNo", "생산 LOT NO");
            MasterGridExControl.MainGrid.AddColumn("PackLotNo", "포장 LOT NO");
            MasterGridExControl.MainGrid.AddColumn("TotalOkQty", "수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("RemainQty", "잔량", HorzAlignment.Far, FormatType.Numeric, "n0", false);
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
            MasterGridExControl.BestFitColumns();
            
            DetailGridExControl.MainGrid.AddColumn("InNo", "입고번호");
            DetailGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InDate", "입고일");
            DetailGridExControl.MainGrid.AddColumn("InId", "입고자");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고코드");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치코드");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InDate", "InId", "WhCode", "WhPosition");
            DetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }

        protected override void InitCombo()
        {
            lupMachineCode.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList());
            lupItemCode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            //string ItemCode = lupItemCode.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            string MachineCode = lupMachineCode.EditValue.GetNullToEmpty();
            string WorkNo = textWorkNo.EditValue.GetNullToEmpty();
            string LotNo = textLotNo.EditValue.GetNullToEmpty();
            string PackLotNo = textPackLotNo.EditValue.GetNullToEmpty();

            if (checkEdit1.EditValue.ToString() == "False")
            {
                MasterGridBindingSource.DataSource = ModelService.GetChildList<VI_MPS1700_MASTER>(p => (p.ResultDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                                                        p.ResultDate <= datePeriodEditEx1.DateToEdit.DateTime) &&
                                                                                                        //(string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode == ItemCode) &&
                                                                                                        (p.ItemNm.Contains(Item) || p.ItemNm1.Contains(Item)) &&
                                                                                                        (string.IsNullOrEmpty(MachineCode) ? true : p.MachineCode == MachineCode) &&
                                                                                                        (p.WorkNo.Contains(WorkNo)) &&
                                                                                                        (p.LotNo.Contains(LotNo)) &&
                                                                                                        (p.PackLotNo.Contains(PackLotNo)) &&
                                                                                                        p.RemainQty > 0
                                                                                                   )
                                                                                                   .OrderBy(p => p.ResultDate)
                                                                                                   .ThenBy(p => p.LotNo)
                                                                                                   .ThenBy(p => p.PackLotNo.Length)
                                                                                                   .ThenBy(p => p.PackLotNo)
                                                                                                   .ToList();
            }
            else
            {

                MasterGridBindingSource.DataSource = ModelService.GetChildList<VI_MPS1700_MASTER>(p => (p.ResultDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                                                        p.ResultDate <= datePeriodEditEx1.DateToEdit.DateTime) &&
                                                                                                        //(string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode == ItemCode) &&
                                                                                                        (p.ItemNm.Contains(Item) || p.ItemNm1.Contains(Item)) &&
                                                                                                        (string.IsNullOrEmpty(MachineCode) ? true : p.MachineCode == MachineCode) &&
                                                                                                        (p.WorkNo.Contains(WorkNo)) &&
                                                                                                        (p.LotNo.Contains(LotNo)) &&
                                                                                                        (p.PackLotNo.Contains(PackLotNo))
                                                                                                   )
                                                                                                   .OrderBy(p => p.ResultDate)
                                                                                                   .ThenBy(p => p.LotNo)
                                                                                                   .ThenBy(p => p.PackLotNo.Length)
                                                                                                   .ThenBy(p => p.PackLotNo)
                                                                                                   .ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            var MasterObj = MasterGridBindingSource.Current as VI_MPS1700_MASTER;
            if (MasterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            //DetailGridBindingSource.DataSource = ModelService.GetList(p => p.PackLotNo == MasterObj.PackLotNo).OrderBy(o => o.InNo).ToList();
            DetailGridBindingSource.DataSource = MasterObj.TN_MPS1700List.OrderBy(o => o.InNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();
            ModelService.Save();            
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            var MasterObjList = MasterGridBindingSource.List as List<VI_MPS1700_MASTER>;
            var CheckList = MasterObjList.Where(p => p._Check == "Y").OrderBy(p => p.ResultDate).ThenBy(p => p.LotNo).ThenBy(p => p.PackLotNo.Length).ThenBy(p => p.PackLotNo).ToList();
            if (CheckList.Count == 0) return;

            var FirstReport = new RPACKLABLE();

            foreach (var v in CheckList)
            {
                //라벨출력
                var prt = new PRT_OUTLABLE()
                {
                    ItemCode = v.ItemCode,
                    ItemNm = v.ItemNm,
                    Qty = v.TotalOkQty,
                    LotNo = v.PackLotNo
                };
                var report = new RPACKLABLE(prt);
                report.CreateDocument();
                FirstReport.Pages.AddRange(report.Pages);
            }

            FirstReport.PrintingSystem.ShowMarginsWarning = false;
            FirstReport.ShowPrintStatusDialog = false;
            FirstReport.ShowPreviewDialog();
        }

        protected override void FileChooseClicked()
        {
            if (MasterGridBindingSource == null) return;
            var MasterObjList = MasterGridBindingSource.List as List<VI_MPS1700_MASTER>;
            var CheckList = MasterObjList.Where(p => p._Check == "Y").ToList();
            if (CheckList.Count == 0) return;

            if (CheckList.Any(p => p.TN_MPS1700List.Count > 0))
            {
                MessageBoxHandler.Show("이미 입고 항목이 존재하는 항목이 있습니다.", "경고");
                return;
            }

            PopupDataParam param = new PopupDataParam();
            IPopupForm form;
            param.SetValue(PopupParameter.KeyValue, CheckList);
            form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFMPS1701, param, AddDetailRowCallBack);
            form.ShowPopup(true);
        }

        private void AddDetailRowCallBack(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            else ActRefresh();
        }


        protected override void DetailAddRowClicked()
        {
            var MasterObj = MasterGridBindingSource.Current as VI_MPS1700_MASTER;
            if (MasterObj == null) return;
            if (MasterObj.TN_MPS1700List.Count > 0) return;

            var DetailNewObj = new TN_MPS1700()
            {
                InNo = DbRequesHandler.GetRequestNumber("W-IN"),
                InQty = MasterObj.RemainQty,
                InDate = DateTime.Today,
                InId = GlobalVariable.LoginId
            };
            DetailGridBindingSource.Add(DetailNewObj);
            DetailGridBindingSource.MoveLast();
            MasterObj.TN_MPS1700List.Add(DetailNewObj);
            DetailGridExControl.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var MasterObj = MasterGridBindingSource.Current as VI_MPS1700_MASTER;
            if (MasterObj == null) return;
            var DetailObj = DetailGridBindingSource.Current as TN_MPS1700;
            var CheckObj = ModelService.GetChildList<TN_ORD1201>(p => p.LotNo == DetailObj.PackLotNo).FirstOrDefault();
            if(CheckObj != null)
            {
                MessageBoxHandler.Show("이미 출고된 데이터가 존재하여 삭제가 불가합니다.", "경고");
                return;
            }
            ModelService.RemoveChild(DetailObj);
            //MasterObj.TN_MPS1700List.Remove(DetailObj);
            DetailGridBindingSource.RemoveCurrent();
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var MasterObj = MasterGridBindingSource.Current as VI_MPS1700_MASTER;
            var DetailObj = DetailGridBindingSource.Current as TN_MPS1700;
            if (e.Column.FieldName == "InQty")
            {
                if(MasterObj.TotalOkQty < MasterObj.TN_MPS1700List.Sum(p => p.InQty))
                {
                    MessageBoxHandler.Show("입고할 수 있는 잔량을 벗어났습니다.", "경고");
                    DetailObj.InQty = 0;
                    DetailGridExControl.MainGrid.MainView.RefreshData();
                    return;
                }
            }            
        }

        private void MainView_ShownEditor(object sender, EventArgs e)
        {
            ColumnView view = (ColumnView)sender;
            if (view.FocusedColumn.FieldName == "WhPosition")
            {
                SearchLookUpEdit editor = (SearchLookUpEdit)view.ActiveEditor;
                string WhCode = view.GetFocusedRowCellValue("WhCode").GetNullToEmpty();
                editor.Properties.DataSource = ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == WhCode).ToList();
            }
            else if (view.FocusedColumn.FieldName == "WhPosition")
            {
                SearchLookUpEdit editor = (SearchLookUpEdit)view.ActiveEditor;
                string WhCode = view.GetFocusedRowCellValue("WhCode").GetNullToEmpty();
                editor.Properties.DataSource = ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == WhCode).ToList();
            }
        }

    }
}