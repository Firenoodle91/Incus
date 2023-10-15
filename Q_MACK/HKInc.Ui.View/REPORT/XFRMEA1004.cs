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
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 20220405 오세완 차장 설비비가동현황
    /// </summary>
    public partial class XFRMEA1004 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        #endregion

        public XFRMEA1004()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            
            dpe_Stop.DateFrEdit.DateTime = DateTime.Now.AddMonths(-1);
            dpe_Stop.DateToEdit.DateTime = DateTime.Now;
        }

        protected override void InitCombo()
        {
            lupMC.SetDefault(true, "MachineCode", "MachineName", ModelService.GetList(p => p.UseYn == "Y" && 
                                                                                           p.SerialNo != "ETC").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.AddColumn("MACHINE_NAME", "설비명");
            GridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            GridExControl.MainGrid.AddColumn("ITEM_NM", "품번");
            GridExControl.MainGrid.AddColumn("ITEM_NM1", "품목명");
            GridExControl.MainGrid.AddColumn("STOP_TIME", "비가동시간(분)", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");

            GridExControl.MainGrid.AddColumn("STOP_CODE_NAME", "비가동유형");
            GridExControl.MainGrid.AddColumn("ROW_ID", false); // 20220405 오세완 차장 없어도 될 듯 한데 그리드 포커스 용으로
            GridExControl.BestFitColumns();
        }
     
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("ROW_ID", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            InitCombo();

            string sMachinecode = lupMC.EditValue.GetNullToEmpty();

            if (sMachinecode == "")
            {
                MessageBoxHandler.Show("설비를 선택하여 주십시오.");       // 2022-01-26 김진우 대리 추가
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", dpe_Stop.DateFrEdit.EditValue);
                SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", dpe_Stop.DateToEdit.EditValue);
                SqlParameter sp_Machinecode = new SqlParameter("@MACHINE_CODE", sMachinecode);

                var result = context.Database.SqlQuery<TP_XFRMEA1004_LIST>("USP_GET_XFRMEA1004_LIST @DATE_FROM, @DATE_TO, @MACHINE_CODE", sp_Datefrom, sp_Dateto, sp_Machinecode).ToList();
                if (result == null)
                    GridBindingSource.Clear();
                else if (result.Count == 0)
                    GridBindingSource.Clear();
                else
                {
                    result = result.OrderBy(o => o.MACHINE_CODE).ThenBy(t => t.ITEM_CODE).ToList();
                    GridBindingSource.DataSource = result;
                }
            }
           
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            
            List<TP_XFRMEA1004_LIST> chart_Arr = GridBindingSource.DataSource as List<TP_XFRMEA1004_LIST>;

            Chart1.DataSource = chart_Arr;
            Chart1.Series.Clear();
            Chart1.SeriesDataMember = "STOP_CODE_NAME";
            Chart1.SeriesTemplate.ArgumentDataMember = "ITEM_NM";
            Chart1.SeriesTemplate.ValueDataMembers.AddRange("STOP_TIME");
            Chart1.SeriesTemplate.Label.TextPattern = "{S}:{V:n0}";
            Chart1.CrosshairEnabled = DefaultBoolean.True;
            Chart1.CrosshairOptions.ShowValueLabels = true;

            XYDiagram diagram = (XYDiagram)Chart1.Diagram;

            diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            diagram.AxisY.Label.TextPattern = "{V:n0}";

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisX.VisualRange.Auto = true;
            diagram.AxisY.VisualRange.Auto = true;

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }
    }
}