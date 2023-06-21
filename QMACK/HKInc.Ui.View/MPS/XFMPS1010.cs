using System.Collections.Generic;
using System.Data;
using System.Linq;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 표준공정타입관리
    /// </summary>
    public partial class XFMPS1010 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1010> ModelService = (IService<TN_MPS1010>)ProductionFactory.GetDomainService("TN_MPS1010");
        #endregion

        public XFMPS1010()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("TypeCode", "타입코드");
            MasterGridExControl.MainGrid.AddColumn("TypeName", "타입명");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "TypeName");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1010>(MasterGridExControl);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("Seq", false);
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", "공정순번");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정명");
            DetailGridExControl.MainGrid.AddColumn("MachineGroupCode", "설비그룹");
            DetailGridExControl.MainGrid.AddColumn("OutProcFlag", "외주여부");
            
            DetailGridExControl.MainGrid.AddColumn("StdWorkDay", "표준작업소요일");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessSeq", "MachineGroupCode", "OutProcFlag", "StdWorkDay");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1011>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineGroup), "Mcode", "Codename", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ProcessSeq");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StdWorkDay", DbRequestHandler.GetCommCode(MasterCodeSTR.STD), "Mcode", "Codename");

            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("TypeCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            MasterGridBindingSource.DataSource = ModelService.GetList(p => true).OrderBy(p => p.TypeCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1010;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MPS1011_List.OrderBy(o => o.ProcessSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region 공정순서 Check          
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_MPS1010>;
                var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();

                if (editList.Count > 0)
                {
                    if (editList.Any(p => p.TN_MPS1011_List.GroupBy(c => c.ProcessCode).Where(c => c.Count() > 1).Count() > 0))
                    {
                        MessageBoxHandler.Show("공정은 중복될 수 없습니다.", "경고");
                        return;
                    }

                    if (editList.Any(p => p.TN_MPS1011_List.GroupBy(c => c.ProcessSeq).Where(c => c.Count() > 1).Count() > 0))
                    {
                        MessageBoxHandler.Show("공정순서는 중복될 수 없습니다.", "경고");
                        return;
                    }
                }
            }
            #endregion

            ModelService.Save();
            ActRefresh();
        }

        protected override void AddRowClicked()
        {
            TN_MPS1010 newobj = new TN_MPS1010()
            {
                TypeCode = DbRequestHandler.GetRequestNumberNew("PTYPE"),
                EditRowFlag = "Y",
            };

            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1010;
            if (masterObj == null)
                return;

            string sMessage = "";
            if (masterObj.TN_MPS1011_List.Count > 0)
            {
                sMessage = "표준공정타입관리 상세목록이 존재합니다. 삭제를 하기 위해서는 상세목록을 삭제해야 합니다.";
                MessageBoxHandler.Show(sMessage);
                return;
            }

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(masterObj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1010;
            if (masterObj == null)
                return;

            TN_MPS1011 newobj = new TN_MPS1011()
            {
                TypeCode = masterObj.TypeCode,
                Seq = masterObj.TN_MPS1011_List.Count == 0 ? 1 : masterObj.TN_MPS1011_List.Max(c => c.Seq) + 1,
                ProcessSeq = masterObj.TN_MPS1011_List.Count == 0 ? 1 : masterObj.TN_MPS1011_List.Max(c => c.ProcessSeq) + 1,
                OutProcFlag = "N",
                MachineFlag = "N",
                StdWorkDay = "0"
            };
            masterObj.EditRowFlag = "Y";

            DetailGridBindingSource.Add(newobj);
            masterObj.TN_MPS1011_List.Add(newobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1010;            
            var detailObj = DetailGridBindingSource.Current as TN_MPS1011;
            if (masterObj == null || detailObj == null)
                return;

            masterObj.EditRowFlag = "Y";
            DetailGridBindingSource.Remove(detailObj);
            masterObj.TN_MPS1011_List.Remove(detailObj);
        }

        /// <summary>
        /// 디테일 셀 변경 시 마스터 Edit 체크를 위함.
        /// </summary>
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1010;
            if (masterObj == null)
                return;

            masterObj.EditRowFlag = "Y";

            if (e.Column.FieldName == "MachineGroupCode")
            {
                var detailObj = DetailGridBindingSource.Current as TN_MPS1011;
                if (detailObj == null)
                    return;

                if (e.Value.IsNullOrEmpty())
                    detailObj.MachineFlag = "N";
                else
                    detailObj.MachineFlag = "Y";
            }
        }
    }
}
