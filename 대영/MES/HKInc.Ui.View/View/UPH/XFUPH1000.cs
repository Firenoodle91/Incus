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
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using HKInc.Ui.Model.Domain.VIEW;

using HKInc.Service.Handler;
using HKInc.Service.Helper;

using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;


namespace HKInc.Ui.View.UPH
{
    public partial class XFUPH1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        public XFUPH1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitControls()
        {
            base.InitControls();
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            tx_MachineMcode.SetDefault(true, "MachineMCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineMCode", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("MachineGroupCode", "설비그룹");
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비명");
            MasterGridExControl.MainGrid.AddColumn("ModelNo", "모델NO");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조사");
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고");
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.Init();
            IsDetailGridButtonExportEnabled = true;
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);
            DetailGridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineCode"), false);            
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemName", "품명");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            DetailGridExControl.MainGrid.AddColumn("UPH", "UPH(시간당 생산량)", HorzAlignment.Far, true);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "UPH", "ProcessCode");
            DetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));            
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMaker, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));            
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            // 2021-07-15 김진우 주임 SetRepositoryItemSearchLookUpEdit 에서 SetRepositoryItemCodeLookUpEdit 로 변경
            DetailGridExControl.MainGrid.SetRepositoryItemCodeLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList());
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("UPH", DefaultBoolean.True, "n0", true);
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineMCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.MachineName.Contains(tx_MachineMcode.Text) || (p.MachineMCode == tx_MachineMcode.Text)) &&
                                                                            (p.UseFlag == "Y"))
                                                            .OrderBy(p => p.MachineName)
                                                            .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void MasterFocusedRowChanged()
        {
            var Mobj = MasterGridBindingSource.Current as TN_MEA1000;
            if (Mobj == null) { DetailGridExControl.MainGrid.Clear(); return; }

            DetailGridBindingSource.DataSource = Mobj.TN_UPH1000List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var Mobj = MasterGridBindingSource.Current as TN_MEA1000;
            if (Mobj == null) return;

            var NewDobj = new TN_UPH1000()
            {
                MachineMCode = Mobj.MachineMCode,
                Uph = 0
            };
            Mobj.TN_UPH1000List.Add(NewDobj);
            DetailGridBindingSource.Add(NewDobj);
            DetailGridBindingSource.MoveLast();
        }

        protected override void DeleteDetailRow()
        {
            var Mobj = MasterGridBindingSource.Current as TN_MEA1000;
            var Dobj = DetailGridBindingSource.Current as TN_UPH1000;
            if (Mobj == null || Dobj == null) return;

            Mobj.TN_UPH1000List.Remove(Dobj);
            DetailGridBindingSource.RemoveCurrent();
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            var Dobj = DetailGridBindingSource.Current as TN_UPH1000;
            if (e.Column.FieldName == "ItemCode" && Dobj != null)
            {
                string ItemCode = e.Value.GetNullToEmpty();
                var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == ItemCode).FirstOrDefault();
                if (ItemObj != null)
                {
                    Dobj.TN_STD1100 = ItemObj;
                    DetailGridExControl.BestFitColumns();
                }
            }
        }

    }
}