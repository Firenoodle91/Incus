using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Utils.Class;
using System.Data.SqlClient;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 20220404 오세완 차장
    /// 비가동관리(상세)
    /// </summary>
    public partial class XFMPS1610 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_MEA1004> ModelService = (IService<TN_MEA1004>)ProductionFactory.GetDomainService("TN_MEA1004");
        #endregion

        public XFMPS1610()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dpe_Stop.DateFrEdit.EditValue = DateTime.Now.AddDays(-5);
            dpe_Stop.DateToEdit.EditValue = DateTime.Now;
        }

        protected override void InitCombo()
        {
            //List<TN_MEA1000> machine_Arr = ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y");        // 주석처리     2022-07-06 김진우
            lup_Machinecode.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y"));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("STOP_DATE", "일자", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("MACHINE_NAME", "설비명");
            MasterGridExControl.MainGrid.AddColumn("WORK_NO", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("START_DATE_MIN", "작업시작시간", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd hh:mm:dd");
            MasterGridExControl.MainGrid.AddColumn("END_DATE_MAX", "작업종료시간", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd hh:mm:dd");
            MasterGridExControl.MainGrid.AddColumn("WORK_TIME", "작업분", HorzAlignment.Near, FormatType.Numeric, "n0");
            
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.MainGrid.AddColumn("STOP_CODE_NAME", "비가동(일지정지)사유");
            DetailGridExControl.MainGrid.AddColumn("STOP_START_TIME", "시작시간", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd hh:mm:dd");
            DetailGridExControl.MainGrid.AddColumn("STOP_END_TIME", "종료시간", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd hh:mm:dd");
            DetailGridExControl.MainGrid.AddColumn("STOP_TIME", "비가동(일시정지)분", HorzAlignment.Near, FormatType.Numeric, "n0");
        }
        
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();       // 2022-07-06 김진우 추가

            ModelService.ReLoad();
            InitCombo();

            string sMachinecode = lup_Machinecode.EditValue.GetNullToEmpty();
            string sWorkno = tx_Workno.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                //SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", dpe_Stop.DateFrEdit.DateTime);
                //SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", dpe_Stop.DateToEdit.DateTime);
                //SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", dpe_Stop.DateFrEdit.EditValue);         // 날짜 변경 후 조회시 조회가 안되서 수정    2022-07-06 김진우   
                //SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", dpe_Stop.DateToEdit.EditValue);             // 날짜 변경 후 조회시 조회가 안되서 수정    2022-07-06 김진우   

                DateTime strDt = dpe_Stop.DateFrEdit.DateTime;
                DateTime endDt = dpe_Stop.DateToEdit.DateTime;

                DateTime strDateTime = new DateTime(strDt.Year, strDt.Month, strDt.Day, 00, 00, 00);
                DateTime endDateTime = new DateTime(endDt.Year, endDt.Month, endDt.Day, 23, 59, 59);

                //SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", dpe_Stop.DateFrEdit.EditValue);
                //SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", dpe_Stop.DateToEdit.EditValue);
                SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", strDateTime);
                SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", endDateTime);
                SqlParameter sp_Machinecode = new SqlParameter("@MACHINE_CODE", sMachinecode);
                SqlParameter sp_Workno = new SqlParameter("@WORK_NO", sWorkno);

                MasterGridBindingSource.DataSource = 
                    context.Database.SqlQuery<TP_XFMPS1610_MASTER>("USP_GET_XFMPS1610_MASTER @DATE_FROM, @DATE_TO, @MACHINE_CODE, @WORK_NO",
                    sp_Datefrom, sp_Dateto, sp_Machinecode, sp_Workno).OrderBy(o => o.STOP_DATE).ThenBy(t => t.MACHINE_NAME).ToList();

                #region 이전소스 
                //var vResult = context.Database.SqlQuery<TP_XFMPS1610_MASTER>("USP_GET_XFMPS1610_MASTER @DATE_FROM, @DATE_TO, @MACHINE_CODE, @WORK_NO", 
                //    sp_Datefrom, sp_Dateto, sp_Machinecode, sp_Workno).OrderBy(o => o.STOP_DATE).ThenBy(t => t.MACHINE_NAME).ToList();
                //MasterGridBindingSource.DataSource = vResult;
                #endregion
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            //SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);    // 2022-07-06 김진우 마스터와 디테일 같이 사용해서 주석
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TP_XFMPS1610_MASTER obj = MasterGridBindingSource.Current as TP_XFMPS1610_MASTER;
            if (obj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                //SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", obj.STOP_DATE);
                //SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", obj.END_DATE_MAX);
                //SqlParameter sp_Machinecode = new SqlParameter("@MACHINE_CODE", obj.MACHINE_CODE);

                SqlParameter WorkNo = new SqlParameter("@WORK_NO", obj.WORK_NO);

                //var vResult = context.Database.SqlQuery<TP_XFMPS1610_DETAIL>("USP_GET_XFMPS1610_DETAIL @DATE_FROM, @DATE_TO, @MACHINE_CODE", 
                //    sp_Datefrom, sp_Dateto, sp_Machinecode).OrderBy(o => o.STOP_START_TIME).ToList();

                //var vResult = context.Database.SqlQuery<TP_XFMPS1610_DETAIL>("USP_GET_XFMPS1610_DETAIL @WORK_NO", WorkNo)
                //    .OrderBy(o => o.STOP_START_TIME).ToList();    // 2022-07-06 김진우 주석처리

                DetailGridBindingSource.DataSource = context.Database.SqlQuery<TP_XFMPS1610_DETAIL>("USP_GET_XFMPS1610_DETAIL @WORK_NO", WorkNo)
                    .OrderBy(o => o.STOP_START_TIME).ToList();
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();

            //SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);      // 2022-07-06 김진우 마스터와 디테일 같이 사용해서 주석
        }

    }
    
}
