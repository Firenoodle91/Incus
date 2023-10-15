using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Service.Service;
namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 20220404 오세완 차장
    /// 비가동관리 데이블 변경한 버전
    /// </summary>
    public partial class XFMPS1600_V2 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MEA1004> ModelService = (IService<TN_MEA1004>)ProductionFactory.GetDomainService("TN_MEA1004");
        #endregion

        public XFMPS1600_V2()
        {
            InitializeComponent();
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-5);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today;
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            lupMachine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
            lupstoptype.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.STOPTYPE, "", "", ""));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            GridExControl.MainGrid.AddColumn("MachineCode", "설비", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopCode", "비가동사유", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopStartTime", "비가동시작시간", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopEndTime", "비가동종료시간", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopM", "비가동분", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MachineCode", "StopCode", "StopStartTime", "StopEndTime");
        }

        protected override void GridRowDoubleClicked()
        {
            
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StopStartTime");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StopEndTime");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StopCode", DbRequestHandler.GetCommCode(MasterCodeSTR.STOPTYPE, "", "", ""), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            InitGrid();
            InitRepository();

            string mc = lupMachine.EditValue.GetNullToEmpty();
            string stoptype = lupstoptype.EditValue.GetNullToEmpty();

            /*
            List<TN_MEA1004> tempArr = ModelService.GetList(p => (p.StopStartTime >= datePeriodEditEx1.DateFrEdit.DateTime && p.StopEndTime <= datePeriodEditEx1.DateToEdit.DateTime) &&
                                                                 (string.IsNullOrEmpty(mc) ? true : p.MachineCode == mc) && 
                                                                 (string.IsNullOrEmpty(stoptype) ? true : p.StopCode == stoptype));

            if (tempArr == null)
                GridBindingSource.Clear();
            else if(tempArr.Count == 0)
                GridBindingSource.Clear();
            else
            {
                tempArr = tempArr.OrderBy(o => o.MachineCode).ThenBy(t => t.StopStartTime).ToList();
                GridBindingSource.DataSource = tempArr;
            }
            */

            DateTime dtTo = datePeriodEditEx1.DateToEdit.DateTime;
            DateTime endTime = new DateTime(dtTo.Year, dtTo.Month, dtTo.Day, 23, 59, 59);

            //GridBindingSource.DataSource = ModelService.GetList(p => (p.StopStartTime >= datePeriodEditEx1.DateFrEdit.DateTime && p.StopStartTime <= datePeriodEditEx1.DateToEdit.DateTime) 
            GridBindingSource.DataSource = ModelService.GetList(p => (p.StopStartTime >= datePeriodEditEx1.DateFrEdit.DateTime && p.StopStartTime <= endTime) 
                                                                   && (string.IsNullOrEmpty(mc) ? true : p.MachineCode == mc) 
                                                                   &&(string.IsNullOrEmpty(stoptype) ? true : p.StopCode == stoptype)
                                                                ).OrderBy(o => o.MachineCode).ThenBy(t => t.StopStartTime).ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            TN_MEA1004 newobj = new TN_MEA1004()
            {
                StopStartTime = DateTime.Today
            };
         
            GridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
        }

        protected override void DeleteRow()
        {
            GridBindingSource.RemoveCurrent();
        }

        protected override void DataSave()
        {
            GridBindingSource.EndEdit();
            ModelService.Save();
        }
    }
}
