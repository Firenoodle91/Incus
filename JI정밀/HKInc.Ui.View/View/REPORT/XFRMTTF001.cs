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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;
using HKInc.Service.Handler;




namespace HKInc.Ui.View.View.REPORT
{
    public partial class XFRMTTF001 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        public XFRMTTF001()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dp_dt.DateTime = DateTime.Today;

        }
        protected override void InitCombo()
        {
            lup_MachineCode.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
        }
        protected override void InitGrid()
        {

            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("MachineCode", "설비");
            GridExControl.MainGrid.AddColumn("PType", "구분");
            GridExControl.MainGrid.AddColumn("M01", "1월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M02", "2월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M03", "3월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M04", "4월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M05", "5월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M06", "6월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M07", "7월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M08", "8월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M09", "9월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M10", "10월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("M11", "12월", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");
            GridExControl.MainGrid.AddColumn("MTOTAL", "누계", HorzAlignment.Far, FormatType.Numeric, "#,###,##0.#");

            GridExControl.BestFitColumns();
        }


        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            string mc = lup_MachineCode.EditValue.GetNullToEmpty();
            string dt = dp_dt.DateTime.ToShortDateString();
            if (mc == "")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_49), LabelConvert.GetLabelText("MachineCode"), LabelConvert.GetLabelText("MachineName"), LabelConvert.GetLabelText("MachineCode")));
                return;
            }
                
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {


                var fdt = new SqlParameter("@YYYY", dt);
                var lmc = new SqlParameter("@MACHINE", mc);



                var result = context.Database
                      .SqlQuery<TP_MTTF001>("SP_MTTF_DATA  @YYYY,@MACHINE", fdt, lmc).ToList();
                GridBindingSource.DataSource = result.Where(P => 1 == 1).ToList();

            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {


                var fdt = new SqlParameter("@YYYY", dt);
                var lmc = new SqlParameter("@MACHINE", mc);



                var result = context.Database
                      .SqlQuery<TP_MTTFCHART>("SP_MTTF_DATA_CHART  @YYYY,@MACHINE", fdt, lmc).ToList();
                List<TP_MTTFCHART> ds = result as List<TP_MTTFCHART>;


                Chart1.Series.Clear();
                Series LCL = new Series("MTBF", ViewType.ScatterLine);
                LCL.DataSource = ds;
                LCL.ArgumentDataMember = "MM";
                LCL.ValueDataMembers.AddRange("MTBF");




                Series UCL = new Series("MTTR", ViewType.ScatterLine);
                UCL.DataSource = ds;
                UCL.ArgumentDataMember = "MM";
                UCL.ValueDataMembers.AddRange("MTTR");


                //Series CL = new Series("CL", ViewType.ScatterLine);
                //CL.DataSource = ds;
                //CL.ArgumentDataMember = "Checkdate";
                //CL.ValueDataMembers.AddRange("Std");

                //Series TestVal = new Series("측정치", ViewType.ScatterLine);
                //TestVal.DataSource = ds;
                //TestVal.ArgumentDataMember = "Checkdate";
                //TestVal.ValueDataMembers.AddRange("Testval");
                LCL.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                UCL.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                //CL.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                //TestVal.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                Chart1.Series.Add(LCL);
                Chart1.Series.Add(UCL);
                //Chart1.Series.Add(CL);
                //Chart1.Series.Add(TestVal);


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
            }
            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }




    }
}