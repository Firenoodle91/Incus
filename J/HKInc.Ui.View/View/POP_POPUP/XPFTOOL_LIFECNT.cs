using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraEditors.Mask;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 설비별 공구 리스트 등록
    /// </summary>
    public partial class XPFTOOL_LIFECNT : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_TOOL1100> ModelService = (IService<TN_TOOL1100>)ProductionFactory.GetDomainService("TN_TOOL1100");
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        BindingSource GridBindings = new BindingSource();
        int cnt = 0;
        string ToolUseFlag = "";

        #endregion

        public XPFTOOL_LIFECNT()
        {
            InitializeComponent();

            //lup_Machine.EditValueChanged += lup_Machine_EditValueChanged();

            //this.lup_Machine.EditValueChanged += new System.EventHandler(this.lup_Machine_EditValueChanged);

            btn_WorkStart.Click += btn_WorkStart_Click;
            btn_Exit.Click += btn_Exit_Click;

            gridEx1.MainGrid.MainView.RowCellClick += View_RowCellClick;

        }
        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        public XPFTOOL_LIFECNT(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
        }
        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);

            gridEx1.MainGrid.AddColumn("ToolCode", "공구명");
            gridEx1.MainGrid.AddColumn("BaseCNT", "기본수명", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("LifeCNT", "남은수명", HorzAlignment.Far, FormatType.Numeric, "n0");

            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 25;
            gridEx1.MainGrid.MainView.RowHeight = 37;


            //if (!GlobalVariable.KeyPad)
            gridEx1.MainGrid.SetEditable("LifeCNT");
        }
        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolCode", ModelService.GetChildList<TN_TOOL1000>(p => p.UseFlag == "Y").ToList(), "ToolCode", "ToolName");
        }

        protected override void InitControls()
        {
            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)PopupParam.GetValue(PopupParameter.KeyValue);
            ToolUseFlag = TEMP_XFPOP1000_Obj.ToolUseFlag.GetNullToN();//공정의 공구사용여부 확인
        }

        protected override void InitCombo()
        {
            var machinegroup = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToEmpty();

            lup_Machine.SetDefault(false, "MachineMCode", "MachineName", ModelService.GetChildList<TN_MEA1000>
                                                                            (p => p.UseFlag == "Y"
                                                                         && (string.IsNullOrEmpty(machinegroup) ? true : p.MachineGroupCode == machinegroup)
                                                                            )
                                                                        .OrderBy(o => o.MachineName)
                                                                        .ToList()
                                                                        , DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            var machine = PopupParam.GetValue(PopupParameter.Value_3);
            lup_Machine.EditValue = machine;
            //lup_Machine.EditValue = "123123";
        }

        protected override void DataLoad()
        {
            var itemcode = PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            var processcode = PopupParam.GetValue(PopupParameter.Value_2).GetNullToEmpty();

            GridBindings.DataSource = ModelService.GetList(p => p.ItemCode == itemcode && p.ProcessCode == processcode).ToList();

            gridEx1.DataSource = GridBindings.DataSource;
        }

        //private void lup_Machine_EditValueChanged(object sender, EventArgs e)
        //{
        //    //공구관리 조회
        //    var machinecode = lup_Machine.GetNullToEmpty();
        //    var itemcode = PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty();
        //    var processcode = PopupParam.GetValue(PopupParameter.Value_2).GetNullToEmpty();
        //
        //    GridBindings.DataSource = ModelService.GetList(p => p.ItemCode == itemcode && p.ProcessCode == processcode).ToList();
        //
        //    gridEx1.DataSource = GridBindings.DataSource;
        //
        //}

        private void btn_WorkStart_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gv = gridEx1.MainGrid.MainView as GridView;
                int rowcnt = 0;


                string Machinecode = lup_Machine.EditValue.GetNullToEmpty();
                string MSG = string.Empty;

                if (ToolUseFlag == "Y")
                {
                    if (Machinecode.IsNullOrEmpty())
                    {
                        MessageBoxHandler.Show("설비 미선택", "");
                        return;
                    }

                    //작업중인 설비는 시작 불가
                    string sql = @"
                            SELECT A.WORK_NO, B.JOB_STATES
                            FROM TN_MPS1201T A
                            LEFT JOIN TN_MPS1200T B on (A.WORK_NO = B.WORK_NO AND A.PROCESS_CODE = B.PROCESS_CODE)
                            WHERE B.JOB_STATES <> 'W04' AND (A.RESULT_END_DATE is null or A.RESULT_END_DATE = '')
                            AND A.MACHINE_CODE = '" + Machinecode + "' ORDER BY A.WORK_NO";

                    DataTable DT = DbRequestHandler.GetDataTableSelect(sql);

                    string _workno = "";

                    if(DT.Rows.Count > 0)
                    {
                        for(int i = 0 ; i < DT.Rows.Count; i++)
                        {
                            _workno = _workno + DT.Rows[i]["WORK_NO"].ToString() + ", ";
                        }

                        MessageBoxHandler.Show("이미 작업중인 설비,  \n WorkNo : " + _workno);
                        return;
                    }


                    //설비별 공구 리스트 추가, 교체 이력 추가

                    List<TN_TOOL1100> List = GridBindings.DataSource as List<TN_TOOL1100>;

                    if (List.Count == 0)
                    {
                        MessageBoxHandler.Show("제품/공정 공구수명관리 미등록", "");
                        return;
                    }


                    foreach (var v in List)
                    {
                        using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                        {
                            try
                            {
                                var WorkNo = new SqlParameter("@WorkNo", TEMP_XFPOP1000_Obj.WorkNo);
                                var MachineCode = new SqlParameter("@MachineCode", Machinecode);
                                var ItemCode = new SqlParameter("@ItemCode", TEMP_XFPOP1000_Obj.ItemCode);
                                var ProcessCode = new SqlParameter("@ProcessCode", TEMP_XFPOP1000_Obj.ProcessCode);
                                var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                                var ToolCode = new SqlParameter("@ToolCode", v.ToolCode);
                                var BaseCNT = new SqlParameter("@BaseCNT", v.BaseCNT);
                                var LifeCNT = new SqlParameter("@LifeCNT", v.LifeCNT);
                                //var ChangeCNT = new SqlParameter("@ChangeCNT", 0);


                                MSG = context.Database.SqlQuery<string>
                                    ("USP_INS_MACHINE_TOOL @WorkNo, @MachineCode, @ItemCode, @ProcessCode, @ToolCode, @BaseCNT, @LifeCNT, @LoginId",
                                                             WorkNo, MachineCode, ItemCode, ProcessCode, ToolCode, BaseCNT, LifeCNT, LoginId).SingleOrDefault();

                                if (MSG != "OK")
                                {
                                    MessageBoxHandler.Show(MSG, "");
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBoxHandler.Show(ex.Message, "");
                                return;
                            }
                        }
                    }

                }

                
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, Machinecode);

                //if (tempArr == null)
                //    param.SetValue(PopupParameter.Value_2, "NO_ONE");
                //else
                //    param.SetValue(PopupParameter.Value_2, "HAVE");

                //param.SetValue(PopupParameter.ReturnObject, tempArr);
                //param.SetValue(PopupParameter.Value_3, tx_Moldmcode.EditValue.GetNullToEmpty()); 
                ReturnPopupArgument = new PopupArgument(param);
                

                ActClose();

            }
            catch (Exception ex)
            {
                MessageBoxHandler.Show(ex.Message, "");
            }


        }


        private void RepositoryLifeCNTItemSpinEdit_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            var spinEidt = sender as SpinEdit;
            var keyPad = new XFCNUMPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEidt.EditValue = keyPad.returnval;
            }

            // 20210619 오세완 차장 값을 입력하고 포커스를 이동해야만 판정이 나와서 아예 자동으로 이동처리 추가
            gridEx1.MainGrid.MainView.FocusedColumn = gridEx1.MainGrid.MainView.Columns["Judge"];
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            GridView gridview = sender as GridView;

            string fieldName = e.Column.FieldName;

            if (fieldName != "LifeCNT") return;

            var keyPad = new XFCNUMPAD();

            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                gridview.SetFocusedRowCellValue(fieldName, keyPad.returnval);
            }
        }
    }
}
