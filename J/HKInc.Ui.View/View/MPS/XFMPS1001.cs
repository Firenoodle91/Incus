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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 표준공정타입관리
    /// </summary>
    public partial class XFMPS1001 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MPS1001> ModelService = (IService<TN_MPS1001>)ProductionFactory.GetDomainService("TN_MPS1001");

        public XFMPS1001()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitCombo()
        {
            //lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("TypeCode");
            MasterGridExControl.MainGrid.AddColumn("TypeName");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "TypeName");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1001>(MasterGridExControl);

            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"));
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            DetailGridExControl.MainGrid.AddColumn("ToolUseFlag", LabelConvert.GetLabelText("ToolUseFlag"));
            DetailGridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"));
            //DetailGridExControl.MainGrid.AddColumn("JobSettingFlag", LabelConvert.GetLabelText("JobSettingFlag"));

            DetailGridExControl.MainGrid.AddColumn("StdWorkDay", LabelConvert.GetLabelText("StdWorkDay"));

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessSeq", "MachineGroupCode", "ToolUseFlag", "OutProcFlag", "StdWorkDay");
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1002>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            //공통코드 추가시 화면 재출력 없이 하기 위해서 추가
            //ModelService.ReLoad();
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ProcessSeq");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("ToolUseFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");
            // 20210520 오세완 차장 재가동TO여부로 변경
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("RestartToFlag", "N"); // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("JobSettingFlag", "N");

            // 20210520 오세완 차장 설비사용여부 및 툴사용여부 제외
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("MachineFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StdWorkDay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.StdWorkDay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("TypeCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            MasterGridBindingSource.DataSource = ModelService.GetList(p => true).OrderBy(p => p.TypeCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1001;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = masterObj.TN_MPS1002List.OrderBy(o => o.ProcessSeq).ToList();
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
                var masterList = MasterGridBindingSource.List as List<TN_MPS1001>;
                var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();

                if (editList.Count > 0)
                {
                    if (editList.Any(p => p.TN_MPS1002List.GroupBy(c => c.ProcessCode).Where(c => c.Count() > 1).Count() > 0))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_87), LabelConvert.GetLabelText("Process")));
                        //MessageBoxHandler.Show("공정은 중복될 수 없습니다.", "경고");
                        return;
                    }

                    if (editList.Any(p => p.TN_MPS1002List.GroupBy(c => c.ProcessSeq).Where(c => c.Count() > 1).Count() > 0))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_87), LabelConvert.GetLabelText("ProcessSeq")));
                        //MessageBoxHandler.Show("공정순서는 중복될 수 없습니다.", "경고");
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
            TN_MPS1001 newobj = new TN_MPS1001()
            {
                TypeCode = DbRequestHandler.GetSeqStandard("PTYPE"),
                EditRowFlag = "Y",
            };

            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1001;
            if (masterObj == null) return;

            if (masterObj.TN_MPS1002List.Count > 0)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_10));
                return;
            }

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(masterObj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1001;
            if (masterObj == null) return;

            TN_MPS1002 newobj = new TN_MPS1002()
            {
                TypeCode = masterObj.TypeCode,
                Seq = masterObj.TN_MPS1002List.Count == 0 ? 1 : masterObj.TN_MPS1002List.Max(c => c.Seq) + 1,
                ProcessSeq = masterObj.TN_MPS1002List.Count == 0 ? 1 : masterObj.TN_MPS1002List.Max(c => c.ProcessSeq) + 1,
                ToolUseFlag = "N",
                OutProcFlag = "N",
                //JobSettingFlag = "N",
                //RestartToFlag = "N", // 20210520 오세완 차장 재가동TO여부로 변경
                MachineFlag = "N",
                StdWorkDay = "0"
            };
            masterObj.EditRowFlag = "Y";

            DetailGridBindingSource.Add(newobj);
            masterObj.TN_MPS1002List.Add(newobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1001;            
            var detailObj = DetailGridBindingSource.Current as TN_MPS1002;
            if (masterObj == null || detailObj == null) return;

            masterObj.EditRowFlag = "Y";
            DetailGridBindingSource.Remove(detailObj);
            masterObj.TN_MPS1002List.Remove(detailObj);
        }

        /// <summary>
        /// 디테일 셀 변경 시 마스터 Edit 체크를 위함.
        /// </summary>
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1001;
            if (masterObj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_MPS1002;
            if (detailObj == null) return;

            masterObj.EditRowFlag = "Y";

            if (e.Column.FieldName == "MachineGroupCode")
            {
                if (e.Value.IsNullOrEmpty())
                    detailObj.MachineFlag = "N";
                else
                    detailObj.MachineFlag = "Y";
            }
            else if (e.Column.FieldName == "ProcessCode")
            {
                //DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process)
                string process = gridEx2.MainGrid.MainView.GetRowCellValue(e.RowHandle, "ProcessCode").GetNullToEmpty();
                var toolflag = ModelService.GetChildList<TN_STD1000>(p => p.CodeMain == "P001" && p.CodeVal == process).ToList();

                detailObj.ToolUseFlag = toolflag[0].ToolUseFlag;
            }
        }
    }
}
