using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using System.Data.SqlClient;

namespace HKInc.Ui.View.REPORT
{
    public partial class XFR6000 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        #endregion

        public XFR6000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            this.Text = "설비가동현황";

            slup_Machine_group_code.EditValueChanged += Slup_Machine_group_code_EditValueChanged;
        }

        private void Slup_Machine_group_code_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit slup_temp = sender as SearchLookUpEdit;
            if(slup_temp != null)
            {
                string sGroupcode = slup_temp.EditValue.GetNullToEmpty();
                List<TN_MEA1000> mea_Arr = ModelService.GetList(p => p.MachineGroupCode == sGroupcode &&
                                                                     p.UseYn == "Y");
                slup_Machine_code.SetDefault(true, "MachineCode", "MachineName", mea_Arr);
            }
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("MACHINE_GROUP_NAME", "설비그룹명");
            GridExControl.MainGrid.AddColumn("MACHINE_CODE", "설비코드");
            GridExControl.MainGrid.AddColumn("MACHINE_NAME", "설비명");
            GridExControl.MainGrid.AddColumn("WORK_TIME", "근무시간(분)", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SUM_STOP_TIME", "비가동시간(분)", HorzAlignment.Far, FormatType.Numeric, "n0");

            GridExControl.MainGrid.AddColumn("MACHINE_RUN_TIME", "가동시간(분)", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            GridExControl.MainGrid.AddColumn("ITEM_NM", "품명");
            GridExControl.MainGrid.AddColumn("ITEM_NM1", "품번");
            GridExControl.MainGrid.AddColumn("CAR_TYPE", "차종");

            GridExControl.MainGrid.AddColumn("SPEC_1", "규격1");
            GridExControl.MainGrid.AddColumn("SPEC_2", "규격2");
            GridExControl.MainGrid.AddColumn("SPEC_3", "규격3");
            GridExControl.MainGrid.AddColumn("SPEC_4", "규격4");
            GridExControl.MainGrid.AddColumn("CUSTOMER_NAME", "거래처명");

            GridExControl.MainGrid.AddColumn("WORK_NO", "작업지시번호");
            GridExControl.MainGrid.AddColumn("PROCESS_NAME", "공정명");
            GridExControl.MainGrid.AddColumn("LOT_NO", "생산LOTNO");
            GridExControl.MainGrid.AddColumn("START_DATE", "작업시작일", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("OK_QTY", "누적양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###");

            GridExControl.MainGrid.AddColumn("FAIL_QTY", "누적불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###");
        }

        protected override void InitCombo()
        {
            slup_Machine_group_code.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineGroup));
            List<TN_MEA1000> mea_Arr = ModelService.GetList(p => p.UseYn == "Y");
            slup_Machine_code.SetDefault(true, "MachineCode", "MachineName", mea_Arr);
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            string sMachine_groupcode = slup_Machine_group_code.EditValue.GetNullToEmpty();
            string sMachine_code = slup_Machine_code.EditValue.GetNullToEmpty();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Machine_group_code = new SqlParameter("@MACHINE_GROUP_CODE", sMachine_groupcode);
                SqlParameter sp_Machine_code = new SqlParameter("@MACHINE_CODE", sMachine_code);

                var vResult = context.Database.SqlQuery<TP_XFR6000_LIST>("USP_GET_XFR6000_LIST @MACHINE_GROUP_CODE, @MACHINE_CODE", 
                    sp_Machine_group_code, sp_Machine_code).OrderBy(o => o.START_DATE).ToList();

                GridBindingSource.DataSource = vResult;
                
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }
    }
}