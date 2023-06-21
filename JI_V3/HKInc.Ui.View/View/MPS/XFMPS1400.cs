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
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using DevExpress.Utils;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 비가동관리
    /// </summary>
    public partial class XFMPS1400 : Service.Base.ListFormTemplate
    {
        IService<TN_MEA1004> ModelService = (IService<TN_MEA1004>)ProductionFactory.GetDomainService("TN_MEA1004");

        public XFMPS1400()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dt_StartDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_StartDate.DateToEdit.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {
            lup_MachineCode.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            lup_StopCode.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.StopType));
        }

        protected override void InitGrid()
        {
            //GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            //GridExControl.MainGrid.AddColumn("TN_MEA1000.MachineName", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("StopSeq", LabelConvert.GetLabelText("StopSeq"), false);
            GridExControl.MainGrid.AddColumn("StopCode", LabelConvert.GetLabelText("StopType"));
            GridExControl.MainGrid.AddColumn("StopStartTime", LabelConvert.GetLabelText("StopStartDate"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopEndTime", LabelConvert.GetLabelText("StopEndDate"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopM", LabelConvert.GetLabelText("StopM"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MachineCode", "StopCode", "StopStartTime", "StopEndTime");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1004>(GridExControl);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StopStartTime");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StopEndTime");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StopCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.StopType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            string mc = lup_MachineCode.EditValue.GetNullToEmpty();
            string stopCode = lup_StopCode.EditValue.GetNullToEmpty();

            DateTime dtStr = dt_StartDate.DateFrEdit.DateTime;
            DateTime dtEnd = dt_StartDate.DateToEdit.DateTime.AddDays(1);

            GridBindingSource.DataSource = ModelService.GetList(p => (p.StopStartTime >= dtStr
                                                                    && p.StopStartTime < dtEnd) 
                                                                    && (string.IsNullOrEmpty(mc) ? true : p.MachineCode == mc) 
                                                                    && (string.IsNullOrEmpty(stopCode) ? true : p.StopCode == stopCode))
                                                               .OrderBy(o => o.StopStartTime)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            var newObj = new TN_MEA1004();
            ModelService.Insert(newObj);
            GridBindingSource.Add(newObj);
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_MEA1004;
            if (obj == null) return;
            ModelService.Delete(obj);
            GridBindingSource.RemoveCurrent();
        }

        protected override void DataSave()
        {
            GridBindingSource.EndEdit();
            GridExControl.MainGrid.PostEditor();

            ModelService.Save();
            ActRefresh();
        }

        protected override void GridRowDoubleClicked() { }
    }
}