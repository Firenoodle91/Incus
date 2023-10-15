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
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using HKInc.Utils.Enum;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 20210610 오세완 차장
    /// 설비 비가동 정보 조회 및 관리
    /// </summary>
    public partial class XFMPS1400_V2 : Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MEA1004> ModelService = (IService<TN_MEA1004>)ProductionFactory.GetDomainService("TN_MEA1004");
        List<TEMP_MPS1400_LIST> DeleteList;
        #endregion

        public XFMPS1400_V2()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dt_StartDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_StartDate.DateToEdit.DateTime = DateTime.Today;

            GridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            GridExControl.MainGrid.MainView.RowStyle += MainView_RowStyle;
            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 품질여부를 체크해야 하는 row인 경우는 색상을 다르게 출력처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if(e.RowHandle >= 0)
            {
                GridView View = sender as GridView;
                //string sValue = View.GetRowCellValue(e.RowHandle, View.Columns["TEMP1"]).GetNullToEmpty();
                //if (sValue != "")
                string sValue = View.GetRowCellValue(e.RowHandle, View.Columns["STOP_CODE"]).GetNullToEmpty();
                if (sValue == "07")
                {
                    e.Appearance.BackColor = Color.DarkViolet;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
            }
        }

        /// <summary>
        /// 20210611 오세완 차장
        /// 1. 설비등록관리에서 비가동 품질확인 여부가 Y인 설비로 바꾸는 경우 비가동 품질 관리에 관련된 컬럼 값을 바꿀수 있게 처리
        /// 2. 비가동 유형이 품질확인 경우 비가동 품질 관리에 관련된 컬럼 값을 바꿀수 있게 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TEMP_MPS1400_LIST vObj = GridBindingSource.Current as TEMP_MPS1400_LIST;
            if (vObj == null)
                return;

            bool bUpdate = false;
            if (e.Column.FieldName == "MACHINE_CODE")
            {
                List<TN_MEA1000> tempArr = ModelService.GetChildList<TN_MEA1000>(p => p.MachineMCode == vObj.MACHINE_CODE &&
                                                                                      p.UseFlag == "Y").ToList();
                if(tempArr != null)
                    if(tempArr.Count > 0)
                    {
                        TN_MEA1000 temp = tempArr.FirstOrDefault();
                        if (temp.MachineStopCheckManageFlag.GetNullToEmpty() == "Y")
                            vObj.TEMP1 = "N";
                        else
                            vObj.TEMP1 = null;
                    }

                bUpdate = true;
            }
            else if (e.Column.FieldName == "STOP_CODE")
            {
                if (vObj.STOP_CODE == "07")
                    vObj.TEMP1 = "N";
                else
                {
                    List<TN_MEA1000> tempArr = ModelService.GetChildList<TN_MEA1000>(p => p.MachineMCode == vObj.MACHINE_CODE &&
                                                                                      p.UseFlag == "Y").ToList();
                    if (tempArr != null)
                        if (tempArr.Count > 0)
                        {
                            TN_MEA1000 temp = tempArr.FirstOrDefault();
                            if (temp.MachineStopCheckManageFlag.GetNullToEmpty() == "Y")
                                vObj.TEMP1 = "N";
                            else
                                vObj.TEMP1 = null;
                        }
                }
                
                bUpdate = true;
            }
                
            else if (e.Column.FieldName == "STOP_START_TIME")
                bUpdate = true;
            else if (e.Column.FieldName == "STOP_END_TIME")
                bUpdate = true;

            if (bUpdate)
            {
                // 20210629 오세완 차장 GETNULLTOEMPTY가 없으면 NULL처리를 못한다. 
                if(vObj.Flag.GetNullToEmpty() != "Insert")
                    vObj.Flag = "Update";
            }

            bool bTempUpdate = false;
            GridView gv = sender as GridView;
            if (vObj.TEMP1.GetNullToEmpty() != "")
                bTempUpdate = true;

            gv.Columns[7].OptionsColumn.AllowEdit = bTempUpdate;
            gv.Columns[8].OptionsColumn.AllowEdit = bTempUpdate;
        }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 품질 확인이 필요한 경우는 색상을 변경해 줘야 한다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            TEMP_MPS1400_LIST vObj = GridBindingSource.Current as TEMP_MPS1400_LIST;
            if (vObj == null)
                return;

            if(e.Column.FieldName == "TEMP1")
            {
                if(vObj.TEMP1.GetNullToEmpty() == "Y")
                {
                    e.Appearance.BackColor = Color.Violet;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        /// <summary>
        /// 20210610 오세완 차장
        /// 일단 비가동을 완료까지 했다면 더이상은 수정 못하게 막는다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TEMP_MPS1400_LIST vObj = GridBindingSource.Current as TEMP_MPS1400_LIST;
            if (vObj == null)
                return;

            if (vObj.STOP_END_TIME != null)
                e.Cancel = true;
            else
            {
                // 20210628 오세완 차장 품질확인건은 비가동완료 시간을 건드리지 못하게 설정
                if(vObj.STOP_CODE == "07")
                {
                    GridView gv = sender as GridView;
                    if(gv.FocusedColumn.FieldName.Contains("STOP_END_TIME"))
                    {
                        e.Cancel = true;
                    }
                    else if(vObj.TEMP1.GetNullToEmpty() != "Y")
                    {
                        // 20210628 오세완 차장 품질확인건은 비가동품질해제를 설정했을 때만 값을 수정하게 설정
                        if (gv.FocusedColumn.FieldName.Contains("ACT_DATE") || gv.FocusedColumn.FieldName.Contains("WORK_ID"))
                            e.Cancel = true;
                    }
                }
            }
            
        }

        protected override void InitCombo()
        {
            lup_MachineCode.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            lup_StopCode.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.StopType));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(true);
            this.IsGridButtonFileChooseEnabled = UserRight.HasEdit;
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("StopQC_Confirm") + "[F10]", IconImageList.GetIconImage("actions/apply"));
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);

            GridExControl.MainGrid.AddColumn("MACHINE_CODE", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("STOP_CODE", LabelConvert.GetLabelText("StopType"));
            GridExControl.MainGrid.AddColumn("STOP_START_TIME", LabelConvert.GetLabelText("StopStartDate"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("STOP_END_TIME", LabelConvert.GetLabelText("StopEndDate"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("CALCULATE_MINITE", LabelConvert.GetLabelText("StopM"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");

            GridExControl.MainGrid.AddColumn("TEMP1", LabelConvert.GetLabelText("StopQCManagement"));
            GridExControl.MainGrid.AddColumn("ROW_ID", "비가동 고유순번", false);
            GridExControl.MainGrid.AddColumn("ACT_DATE", LabelConvert.GetLabelText("StopQC_ConfirmDate"));
            GridExControl.MainGrid.AddColumn("WORK_ID", LabelConvert.GetLabelText("StopQC_WorkId"));

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MACHINE_CODE", "STOP_CODE", "STOP_START_TIME", "STOP_END_TIME", "ACT_DATE", "WORK_ID");
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1004>(GridExControl);

            
        }

        protected override void FileChooseClicked()
        {
            TEMP_MPS1400_LIST vObj = GridBindingSource.Current as TEMP_MPS1400_LIST;
            if (vObj == null)
                return;

            if (vObj.TEMP1 == "N")
            {
                vObj.TEMP1 = "Y";
                vObj.Flag = "Update";
                vObj.ACT_DATE = DateTime.Now;
                vObj.WORK_ID = GlobalVariable.LoginId;
            }
            else if (vObj.TEMP1 == "Y")
            {
                // 20210610 오세완 차장 비가동을 생산에서 종료하지 않았을 때만 수정이 가능하게 처리
                if (vObj.STOP_END_TIME == null)
                {
                    vObj.TEMP1 = "N";
                    vObj.Flag = "Update";
                    vObj.ACT_DATE = null;
                    vObj.WORK_ID = "";
                }
            }

            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MACHINE_CODE", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("STOP_START_TIME");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("STOP_END_TIME");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("STOP_CODE", DbRequestHandler.GetCommTopCode(MasterCodeSTR.StopType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WORK_ID", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            GridExControl.MainGrid.SetRepositoryItemCheckEdit("TEMP1", "N");

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            GridExControl.MainGrid.Clear();

            DeleteList = new List<TEMP_MPS1400_LIST>();

            ModelService.ReLoad();
            // 20210624 오세완 차장 김이사님 지시로 공통코드 추가시 화면 재출력 없이 하기 위해서 추가
            InitRepository();
            InitCombo();

            string mc = lup_MachineCode.EditValue.GetNullToEmpty();
            string stopCode = lup_StopCode.EditValue.GetNullToEmpty();

            DateTime dtStr = dt_StartDate.DateFrEdit.DateTime;
            DateTime dtEnd = dt_StartDate.DateToEdit.DateTime.AddDays(1);

            using (var context = new Model.Context.ProductionContext(Utils.Common.ServerInfo.GetConnectString(Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", dtStr);
                SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", dtEnd);
                SqlParameter sp_Machinecode = new SqlParameter("@MACHINE_CODE", mc);
                SqlParameter sp_Stopcode = new SqlParameter("@STOP_CODE", stopCode);

                var vResult = context.Database.SqlQuery<TEMP_MPS1400_LIST>("USP_GET_MPS1400_LIST @DATE_FROM, @DATE_TO, @MACHINE_CODE, @STOP_CODE", 
                    sp_Datefrom, sp_Dateto, sp_Machinecode, sp_Stopcode).ToList();
                if (vResult != null)
                    GridBindingSource.DataSource = vResult;
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            TEMP_MPS1400_LIST temp = new TEMP_MPS1400_LIST() {
                Flag = "Insert"
            };
            GridBindingSource.Add(temp);
        }

        protected override void DeleteRow()
        {
            TEMP_MPS1400_LIST vObj = GridBindingSource.Current as TEMP_MPS1400_LIST;
            if (vObj == null)
                return;

            //List<TN_MEA1004> mea_Arr = ModelService.GetList(p => p.MachineCode == vObj.MACHINE_CODE && p.StopStartTime == vObj.STOP_START_TIME).ToList();
            //if (mea_Arr != null)
            //    if (mea_Arr.Count > 0)
            //    {
            //        TN_MEA1004 mea_Delete = mea_Arr.FirstOrDefault();

            //        ModelService.Delete(mea_Delete);
            //        GridBindingSource.RemoveCurrent();
            //    }

            if (vObj.ROW_ID != 0)
            {
                TN_MEA1004 tN_MEA1004 = ModelService.GetList(x => x.MachineCode == vObj.MACHINE_CODE && x.RowId == vObj.ROW_ID).FirstOrDefault();

                if (tN_MEA1004 != null)
                {
                    ModelService.Delete(tN_MEA1004);
                    DeleteList.Add(vObj);
                }
            }

            GridBindingSource.RemoveCurrent();
        }

        /// <summary>
        /// 20210611 오세완 차장
        /// 1. 추가를 한 경우는 설비명, 비가동유형, 비가동 시작시간을 필수로 입력할 수 있게 처리
        /// 2. 비가동 품질 관리 여부를 클릭한 경우는 품질확인 처리일과 확인자를 필수로 입력할 수 있게 처리
        /// </summary>
        /// <returns></returns>
        private bool Check_BeforeSave()
        {
            bool bReturn = false;
            List<TEMP_MPS1400_LIST> tempArr = GridBindingSource.List as List<TEMP_MPS1400_LIST>;
            foreach (TEMP_MPS1400_LIST each in tempArr)
            {
                string sMachinecode = each.MACHINE_CODE;
                string sMessage = "";
                if (each.MACHINE_CODE.GetNullToEmpty() == "")
                {
                    sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_143), LabelConvert.GetLabelText("MachineName"));
                    MessageBoxHandler.Show(sMessage);
                    //MessageBoxHandler.Show("설비명을 입력해 주세요.");
                    bReturn = true;
                }
                else if(each.STOP_CODE.GetNullToEmpty() == "")
                {
                    sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_143), LabelConvert.GetLabelText("StopType"));
                    MessageBoxHandler.Show(sMessage);
                    //MessageBoxHandler.Show("비가동 유형을 입력해 주세요.");
                    bReturn = true;
                }
                else if(each.STOP_START_TIME == null)
                {
                    sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_143), LabelConvert.GetLabelText("StopStartDate"));
                    MessageBoxHandler.Show(sMessage);
                    //MessageBoxHandler.Show("비가동 시작 시간을 입력해 주세요.");
                    bReturn = true;
                }
                else if (each.TEMP1.GetNullToEmpty() == "Y")
                {
                    if(each.WORK_ID.GetNullToEmpty() == "")
                    {
                        sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_143), LabelConvert.GetLabelText("StopQC_WorkId"));
                        MessageBoxHandler.Show(sMessage);
                        //MessageBoxHandler.Show("비가동 확인자를 입력해 주세요.");
                        bReturn = true;
                    }
                    else if(each.ACT_DATE == null)
                    {
                        sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_143), LabelConvert.GetLabelText("StopQC_ConfirmDate"));
                        MessageBoxHandler.Show(sMessage);
                        //MessageBoxHandler.Show("비가동 품질확인 일자를 입력해 주세요.");
                        bReturn = true;
                    }
                }
                else if(each.TEMP1.GetNullToEmpty() == "N")
                {
                    // 20210627 오세완 차장 비가동품질여부를 설정했다가 해지하는 경우 주체를 남겨놓고 저장하는 경우를 방지처리하기 위해 고지 없이 값을 없애버리는 걸로
                    if (each.WORK_ID.GetNullToEmpty() != "")
                    {
                        each.WORK_ID = null;
                    }

                    if (each.ACT_DATE != null)
                    {
                        each.ACT_DATE = null;
                    }
                }
            }

            return bReturn;
        }

        protected override void DataSave()
        {
            GridBindingSource.EndEdit();
            GridExControl.MainGrid.PostEditor();

            bool bResult = Check_BeforeSave();
            if (bResult)
                return;

            List<TEMP_MPS1400_LIST> tempArr = GridBindingSource.List as List<TEMP_MPS1400_LIST>;
            // 20210611 오세완 차장 값을 정확하게 입력한 것으로 간주하고 저장 처리
            foreach (TEMP_MPS1400_LIST each in tempArr)
            {
                if(each.Flag == "Insert")
                {
                    if(each.TEMP1.GetNullToEmpty() == "" || each.TEMP1.GetNullToEmpty() == "N")
                    {
                        TN_MEA1004 mea = new TN_MEA1004()
                        {
                            MachineCode = each.MACHINE_CODE,
                            StopCode = each.STOP_CODE,
                            StopStartTime = each.STOP_START_TIME,
                            StopEndTime = each.STOP_END_TIME,
                            Temp1 = each.TEMP1
                        };

                        ModelService.Insert(mea);
                    }
                    else if(each.TEMP1.GetNullToEmpty() == "Y")
                    {
                        TN_MPS1400 mps = new TN_MPS1400()
                        {
                            MachineCode = each.MACHINE_CODE,
                            Mea1400RowId = each.ROW_ID,
                            ActDate = DateTime.Now,
                            WorkId = each.WORK_ID
                        };

                        ModelService.InsertChild<TN_MPS1400>(mps);
                    }
                }
                else if(each.Flag == "Update")
                {
                    List<TN_MEA1004> mea_Arr = ModelService.GetList(p => p.MachineCode == each.MACHINE_CODE &&
                                                                         p.RowId == each.ROW_ID).ToList(); // 20210629 오세완 차장 DATETIME으로 비교하면 나오질 않아서 ROWID로 비교, 참고할 것
                    if(mea_Arr != null)
                        if(mea_Arr.Count > 0)
                        {
                            TN_MEA1004 mea_Update = mea_Arr.FirstOrDefault();
                            mea_Update.StopEndTime = each.STOP_END_TIME;
                            mea_Update.StopCode = each.STOP_CODE;
                            mea_Update.Temp1 = each.TEMP1;

                            ModelService.Update(mea_Update);
                        }

                    if (each.TEMP1.GetNullToEmpty() == "Y")
                    {
                        TN_MPS1400 mps = new TN_MPS1400()
                        {
                            MachineCode = each.MACHINE_CODE,
                            Mea1400RowId = each.ROW_ID,
                            ActDate = DateTime.Now,
                            WorkId = each.WORK_ID
                        };

                        ModelService.InsertChild<TN_MPS1400>(mps);
                    }
                }

            }

            ModelService.Save();
            
            tempArr.AddRange(DeleteList);

            //저장후에 처리해야함
            foreach (TEMP_MPS1400_LIST each in tempArr)
            {
                if (each.Flag != null)
                {
                    //작업상태 변경
                    //각 설비별 전체를 찾아 비가동 유형이 없을 경우 작업상태를 변경한다.
                    using (var context = new Model.Context.ProductionContext(Utils.Common.ServerInfo.GetConnectString(Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        SqlParameter param1 = new SqlParameter("@MACHINE_MCODE", each.MACHINE_CODE);

                        context.Database.ExecuteSqlCommand("EXEC USP_SET_MPS1400_LIST_JOBSTATE @MACHINE_MCODE", param1);
                    }
                }
                
            }

            ActRefresh();
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void GridRowDoubleClicked() { }
    }
}