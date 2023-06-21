using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;

namespace HKInc.Ui.View.SELECT_Popup
{
    /// <summary>
    /// 완제품입고관리 추가팝업창 (우선사용안함)
    /// </summary>
    public partial class XFSMPS1700 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_MPS1700_MASTER> ModelService = (IService<VI_MPS1700_MASTER>)ProductionFactory.GetDomainService("VI_MPS1700_POPUP");
        
        public XFSMPS1700()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }
        public XFSMPS1700(PopupDataParam parameter, PopupCallback callback) :this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            GridExControl = gridEx1;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            dp_date.SetTodayIsWeek();
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.Init();
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.MultiSelect = true;
            GridExControl.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            GridExControl.MainGrid.AddColumn("ResultDate", "생산일", HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            GridExControl.MainGrid.AddColumn("MachineCode", "설비");
            GridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("LotNo", "LOT NO");
            GridExControl.MainGrid.AddColumn("TotalOkQty", "생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("RemainQty", "잔량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
        }
        protected override void InitCombo()
        {
            lupMachineCode.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
            lupItemCode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
        }
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string ItemCode = lupItemCode.EditValue.GetNullToEmpty();
            string MachineCode = lupMachineCode.EditValue.GetNullToEmpty();
            
            ModelBindingSource.DataSource = ModelService.GetList(p => (p.ResultDate >= dp_date.DateFrEdit.DateTime && p.ResultDate <= dp_date.DateToEdit.DateTime) && 
                                                                      (string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode == ItemCode) &&
                                                                      (string.IsNullOrEmpty(MachineCode) ? true : p.MachineCode == MachineCode)
                                                                 ).OrderBy(o => o.ResultDate).ToList();

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();
            
            List<VI_MPS1700_MASTER> AddListObj = new List<VI_MPS1700_MASTER>();
            var ListObj = ModelBindingSource.List as List<VI_MPS1700_MASTER>;
            foreach (var rowHandle in GridExControl.MainGrid.MainView.GetSelectedRows())
            {
                string WorkNo = GridExControl.MainGrid.MainView.GetRowCellValue(rowHandle, "WorkNo").GetNullToEmpty();                    
                AddListObj.Add(ModelService.Detached(ListObj.Where(p => p.WorkNo == WorkNo).First()));
            }
            param.SetValue(PopupParameter.ReturnObject, AddListObj);
            
            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                VI_MPS1700_MASTER obj = (VI_MPS1700_MASTER)ModelBindingSource.Current;

                List<VI_MPS1700_MASTER> AddListObj = new List<VI_MPS1700_MASTER>();
                if (obj != null)
                    AddListObj.Add(ModelService.Detached(obj));

                param.SetValue(PopupParameter.ReturnObject, AddListObj);
                
                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

