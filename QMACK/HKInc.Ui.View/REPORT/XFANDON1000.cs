using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 안돈
    /// </summary>
    public partial class XFANDON1000 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_QCT1600> ModelService = (IService<TN_QCT1600>)ProductionFactory.GetDomainService("TN_QCT1600");

        public XFANDON1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            //dt_Date.SetMonth(1);
            dt_Date.SetTolerance(1);

            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellvalueChanged;

        }

        protected override void InitCombo()
        {
            //lup_Division.SetDefault(true, "DepartmentCode", "DepartmentName", ModelService.GetChildList<TN_STD1200>(p => p.UseFlag == "Y"
            //                                                                        && (p.DepartmentCode == "DEPT02" || p.DepartmentCode == "DEPT01")).ToList());
            lup_Division.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.ANDONTYPE));
            lup_MachineName.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
            lup_WorkId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            //MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("DepDivision", "구분");
            MasterGridExControl.MainGrid.AddColumn("CallDate", "발생일자");
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("ItemNum", "품번(도번)", false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품명");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비명");
            MasterGridExControl.MainGrid.AddColumn("CreateId", "작업자");

            DetailGridExControl.SetToolbarVisible(false);
            //DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", false);
            DetailGridExControl.MainGrid.AddColumn("CreateTime", "호출일시");
            DetailGridExControl.MainGrid.AddColumn("CheckDate", "점검일자");
            DetailGridExControl.MainGrid.AddColumn("CheckContent", "점검내용");
            DetailGridExControl.MainGrid.AddColumn("CheckId", "점검자");
            DetailGridExControl.MainGrid.AddColumn("ConfirmFlag", "완료", false);
            DetailGridExControl.MainGrid.SetEditable("CheckDate", "CheckContent", "CheckId");
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DepDivision", ModelService.GetChildList<TN_STD1200>(p => true).ToList(), "DepartmentCode", "DepartmentName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DepDivision", DbRequestHandler.GetCommCode(MasterCodeSTR.ANDONTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemNm");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineCode", "MachineName");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["CheckContent"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["CheckContent"].MinWidth = 300;
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("WorkNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            InitCombo();
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                var startdate = new SqlParameter("@StartDate", dt_Date.DateFrEdit.DateTime);
                var enddate = new SqlParameter("@EndDate", dt_Date.DateToEdit.DateTime);
                var workno = new SqlParameter("@WorkNo", tx_WorkNo.EditValue.GetNullToEmpty());
                var departmentCode = new SqlParameter("@DepartmentCode", lup_Division.EditValue.GetNullToEmpty());
                var machineCode = new SqlParameter("@MachineCode", lup_MachineName.EditValue.GetNullToEmpty());
                var createId = new SqlParameter("@CreateId", lup_WorkId.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XFANDON1000_MASTER>
                    ("USP_GET_XFANDON1000_MASTER @StartDate, @EndDate, @WorkNo, @DepartmentCode, @MachineCode, @CreateId",
                                                   startdate, enddate, workno, departmentCode, machineCode, createId).ToList();

                MasterGridBindingSource.DataSource = result.ToList();
                MasterGridExControl.DataSource = MasterGridBindingSource;
                MasterGridExControl.BestFitColumns();
            }
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            TEMP_XFANDON1000_MASTER Obj = MasterGridBindingSource.Current as TEMP_XFANDON1000_MASTER;
            if (Obj == null) return;

            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_QCT1600>(p => p.WorkNo == Obj.WorkNo
                                                                      && p.ProcessCode == Obj.ProcessCode
                                                                      && p.DepDivision == Obj.DepDivision
                                                                      && p.CallDate == Obj.CallDate
                                                                      && p.Seq == Obj.Seq
                                                                    ).OrderBy(p => p.Seq).ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
        
        protected override void DeleteDetailRow()
        {
            // 미사용 주석       2022-07-15 김진우
            //var masterObj = MasterGridBindingSource.Current as TN_QCT1600;
            //if (masterObj == null) return;

            //var detailObj = DetailGridBindingSource.Current as TN_QCT1600;
            //if (detailObj == null) return;


            /*
            masterObj.TN_TOOL1001List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
            */
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();

            List<TN_QCT1600> DetailList= DetailGridBindingSource.List as List<TN_QCT1600>;
            List<TN_QCT1600> List = DetailList.Where(p => p.EditRowFlag == "Y").ToList();
            if (List == null) return;

            foreach (var v in List)
            {
                if(v.CheckDate.GetNullToEmpty() == "" || v.CheckContent.GetNullToEmpty() == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("점검 일자 및 내용 확인");
                    return;
                }

                v.ConfirmFlag = "Y";
                v.EditRowFlag = "N";
                ModelService.Update(v);
            }
            ModelService.Save();
            DataLoad();
        }

        private void DetailMainView_CellvalueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView gv = sender as GridView;

                var obj= DetailGridBindingSource.Current as TN_QCT1600;

                //string fieldName = e.Column.FieldName;
                //dynamic checkDate = null;
                //dynamic rowid = null;
                //
                //if (fieldName != "StartDate" && fieldName != "EndDate")
                //    return;
                //
                //checkDate = gv.GetRowCellValue(e.RowHandle, gv.Columns[fieldName]);
                //rowid = gv.GetRowCellValue(e.RowHandle, gv.Columns["RowId"]);//수정 대상은 비교 제외

                obj.EditRowFlag = "Y";
                //ModelService.Update(obj);

            }
            catch { }
        }
    }
}