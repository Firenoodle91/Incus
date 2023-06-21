using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 2021-06-21 김진우 주임 생성
    /// 설비등급평가현황 리포트 2
    /// </summary>
    public partial class XRMEA1301 : DevExpress.XtraReports.UI.XtraReport
    {
        public XRMEA1301()
        {
            InitializeComponent();
            SetCellData();
            DataLoad();
        }

        private void DataLoad()
        {
            int COUNT = 1;
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                List<XFMEA1301_DBDATA> result = context.Database.SqlQuery<XFMEA1301_DBDATA>("EXEC USP_GET_XRMEA1301").ToList();

                List<XFMEA1301_MACHINE> MachineList = new List<XFMEA1301_MACHINE>();
                List<XFMEA1301_DATA> ShowData = new List<XFMEA1301_DATA>();

                foreach (var v in result)
                {
                    XFMEA1301_MACHINE MachineData = new XFMEA1301_MACHINE();
                    if (MachineList.Where(p => p.MachineCode == v.MACHINE_MCODE).ToList().Count == 0)
                    {
                        MachineData.TotalScore = v.TOT_SCR;
                        MachineData.Grade = v.GRADE;
                        MachineData.MachineCode = v.MACHINE_MCODE;
                        MachineData.MachineName = v.MACHINE_NAME;
                        MachineData.Memo = v.Memo;

                        MachineList.Add(MachineData);
                    }
                }
                foreach (var v in MachineList)
                {

                    XFMEA1301_DATA ConvertData = new XFMEA1301_DATA();
                    List<XFMEA1301_DBDATA> DevideMachine = result.Where(p => p.MACHINE_MCODE == v.MachineCode).ToList();

                    ConvertData.TOT_SCR = v.TotalScore;
                    ConvertData.GRADE = v.Grade;
                    ConvertData.MACHINE_MCODE = v.MachineCode;
                    ConvertData.MACHINE_NAME = v.MachineName;
                    ConvertData.Memo = v.Memo;

                    for (int i = 0; i < DevideMachine.Count; i++)
                    {
                        switch (DevideMachine[i].EVALUATION_ITEM)
                        {
                            case "EV01":
                                ConvertData.EV1 = DevideMachine[i].EVALUATION_ITEM;
                                ConvertData.CN1 = DevideMachine[i].CODE_NAME;
                                ConvertData.VAL1 = DevideMachine[i].IN_VALUE;
                                ConvertData.SC1 = DevideMachine[i].SCORE;
                                break;
                            case "EV02":
                                ConvertData.EV2 = DevideMachine[i].EVALUATION_ITEM;
                                ConvertData.CN2 = DevideMachine[i].CODE_NAME;
                                ConvertData.VAL2 = DevideMachine[i].IN_VALUE;
                                ConvertData.SC2 = DevideMachine[i].SCORE;
                                break;
                            case "EV03":
                                ConvertData.EV3 = DevideMachine[i].EVALUATION_ITEM;
                                ConvertData.CN3 = DevideMachine[i].CODE_NAME;
                                ConvertData.VAL3 = DevideMachine[i].IN_VALUE;
                                ConvertData.SC3 = DevideMachine[i].SCORE;
                                break;
                            case "EV04":
                                ConvertData.EV4 = DevideMachine[i].EVALUATION_ITEM;
                                ConvertData.CN4 = DevideMachine[i].CODE_NAME;
                                ConvertData.VAL4 = DevideMachine[i].IN_VALUE;
                                ConvertData.SC4 = DevideMachine[i].SCORE;
                                break;
                            default:
                                break;
                        }
                    }
                    ConvertData.RowNum = COUNT++;
                    bindingSource1.Add(ConvertData);
                }
            }
        }

        private void SetCellData()
        {
            cell_DpmNo.Text = DbRequestHandler.GetSeqStandard("DPM01").ToString();
            cell_PrintDate.Text = DateTime.Today.ToShortDateString();
            cell_Year.Text = DateTime.Today.Year.ToString();
        }
    }

    public class XFMEA1301_DATA
    {
        public long RowNum { get; set; }
        public string MACHINE_MCODE { get; set; }
        public string MACHINE_NAME { get; set; }
        public int TOT_SCR { get; set; }
        public string GRADE { get; set; }
        public string Memo { get; set; }
        public string EV1 { get; set; }
        public string CN1 { get; set; }
        public int VAL1 { get; set; }
        public int SC1 { get; set; }
        public string EV2 { get; set; }
        public string CN2 { get; set; }
        public int VAL2 { get; set; }
        public int SC2 { get; set; }
        public string EV3 { get; set; }
        public string CN3 { get; set; }
        public int VAL3 { get; set; }
        public int SC3 { get; set; }
        public string EV4 { get; set; }
        public string CN4 { get; set; }
        public int VAL4 { get; set; }
        public int SC4 { get; set; }
    }

    public class XFMEA1301_DBDATA
    {
        public string MACHINE_MCODE { get; set; }
        public string MACHINE_NAME { get; set; }
        public int TOT_SCR { get; set; }
        public string GRADE { get; set; }
        public string EVALUATION_ITEM { get; set; }
        public string CODE_NAME { get; set; }
        public int IN_VALUE { get; set; }
        public int SCORE { get; set; }
        public string Memo { get; set; }
    }

    public class XFMEA1301_MACHINE
    {
        public string MachineCode { get; set; }
        public string MachineName { get; set; }
        public int TotalScore { get; set; }
        public string Grade { get; set; }
        public string Memo { get; set; }

    }

}
