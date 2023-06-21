using System;
using System.Data;
using System.Linq;
using DevExpress.Utils;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using HKInc.Service.Service;
using System.Collections.Generic;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 재공재고현황
    /// </summary>
    public partial class XFMPS1700 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public DataTable gDt = new DataTable(); // 전역 dataTable 변수


        public XFMPS1700()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dateEx1.SetFormat(Utils.Enum.DateFormat.Month);
            dateEx1.EditValue = DateTime.Now.Year + "-" + DateTime.Now.Month;
        }

        protected override void InitCombo()
        {
            //품목 설정
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN )).ToList()); 
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            InitCombo();

            //조건
            string sItemcode = lup_Item.EditValue.GetNullToEmpty();
            string sMonth = dateEx1.EditValue.ToString();

            string sql = string.Format("EXEC USP_GET_XFMPS1700_LIST '{0}', '{1}'", sItemcode, sMonth);
            DataTable rtnDt = DbRequestHandler.GetDataTableSelect(sql);
            GridExControl.MainGrid.DataSource = null;
            GridExControl.MainGrid.MainView.Columns.Clear();
            int columnCount = 0;
            if (rtnDt == null) return;

            //공정별 재공 수량을 계산한다            
            GridExControl.MainGrid.DataSource = setGridCalc(rtnDt);

            //GridExControl.MainGrid.DataSource = rtnDt;
            GridExControl.MainGrid.BestFitColumns();
            gDt.Clear();                       
        }


        /// <summary>
        /// 그리드 계산
        /// </summary>
        /// <param name="dataTable"></param>
        private DataTable setGridCalc(DataTable dt)
        {
            int columnCount = dt.Columns.Count - 1; //컬럼 수
            DataTable calcDt = dt.Copy();
            for (int i = 0; i < calcDt.Rows.Count; i++)
            {
                for (int k = 0; k <= columnCount; k++)
                {
                    //해당컬럼이 숫자 인지 확인
                    int prevProcessQty = 0;
                    int ProcessQty = 0;
                    
                    if (k < columnCount && calcDt.Rows[i][k+1] != null)
                    {
                        if (IsNumeric(calcDt.Rows[i][k].ToString()) && IsNumeric(calcDt.Rows[i][k+1].ToString()))
                        {
                            prevProcessQty = Convert.ToInt32(calcDt.Rows[i][k].ToString());
                            ProcessQty = Convert.ToInt32(calcDt.Rows[i][k + 1].ToString());

                            calcDt.Rows[i][k] = prevProcessQty - ProcessQty;

                            if (Convert.ToInt32(calcDt.Rows[i][k].ToString()) <= 0)
                                calcDt.Rows[i][k] = 0;
                        }
                    }
                    
                }
            }

            

            return calcDt;
        }

        /// <summary>
        /// 숫자 형태인지 확인 한다
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private bool IsNumeric(string v)
        {
            int number = 0;
            return Int32.TryParse(v, out number);
        }
    }
}
