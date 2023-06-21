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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using HKInc.Service.Controls;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using System.Threading;

namespace HKInc.Ui.View.View.REPORT 
{
    public partial class FME_DAYMC_VER2 : HKInc.Service.Base.BaseForm
    {
        public FME_DAYMC_VER2()
        {
            SetToolbarVisible(false);
            InitializeComponent();
            DataLoad();
            timer1.Start();
        }
        protected override void DataLoad()
        {
        
                flowLayoutPanel1.Controls.Clear();
                DataSet ds = DbRequestHandler.GetDataQury("exec SP_GET_DAYMC" );
                if (ds == null) return;
                if (ds.Tables[0].Rows.Count == 0) return;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Uc_MC_State2 f = new Uc_MC_State2();

                    f.lbl_machineNameSetting = ds.Tables[0].Rows[i]["MACHINE_CODE"].GetNullToEmpty();
                    f.lbl_itemNameSetting = ds.Tables[0].Rows[i]["ITEM_CODE"].GetNullToEmpty();
                    f.lbl_prodQtySetting = ds.Tables[0].Rows[i]["RESULT_SUM_QTY"].GetIntNullToZero().ToString("#,##0");
                    f.mcState = ds.Tables[0].Rows[i]["MACHINE_STATE"].GetNullToEmpty();
                    f.andonSetting = ds.Tables[0].Rows[i]["Andon"].GetNullToEmpty();

                    flowLayoutPanel1.Controls.Add(f);

                    //f.lb_mc = ds.Tables[0].Rows[i][0].GetNullToEmpty();
                    //f.lb_itemName = ds.Tables[0].Rows[i][4].GetNullToEmpty();
                    ////f.qty = ds.Tables[0].Rows[i][5].GetIntNullToZero();
                    //f.qty = ds.Tables[0].Rows[i][5].GetIntNullToZero().ToString("#,##0");
                    //f.mcstate = ds.Tables[0].Rows[i][1].GetNullToEmpty();

                ////f.pic_mcimg = null;
                //f.pic_setandon = ds.Tables[0].Rows[i][6].GetNullToEmpty();
                //flowLayoutPanel1.Controls.Add(f);
                }
           
        }

        private  void DataLoad1(string grop)
        {
         
                flowLayoutPanel1.Controls.Clear();
                DataSet ds = DbRequestHandler.GetDataQury("exec SP_GET_DAYMC1 '"+ grop + "'");
                if (ds == null) return;
                if (ds.Tables[0].Rows.Count == 0) return;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Uc_MC_State2 f = new Uc_MC_State2();

                    f.lbl_machineNameSetting = ds.Tables[0].Rows[i]["MACHINE_CODE"].GetNullToEmpty();
                    f.lbl_itemNameSetting = ds.Tables[0].Rows[i]["ITEM_CODE"].GetNullToEmpty();
                    f.lbl_prodQtySetting = ds.Tables[0].Rows[i]["RESULT_SUM_QTY"].GetIntNullToZero().ToString("#,##0");
                    f.mcState = ds.Tables[0].Rows[i]["MACHINE_STATE"].GetNullToEmpty();
                    f.andonSetting = ds.Tables[0].Rows[i]["Andon"].GetNullToEmpty();
                
                    flowLayoutPanel1.Controls.Add(f);

                //f.lb_mc = ds.Tables[0].Rows[i][0].GetNullToEmpty();
                //f.lb_itemName = ds.Tables[0].Rows[i][4].GetNullToEmpty();
                ////f.qty = ds.Tables[0].Rows[i][5].GetIntNullToZero();
                //f.qty = ds.Tables[0].Rows[i][5].GetIntNullToZero().ToString("#,##0");
                //f.mcstate = ds.Tables[0].Rows[i][1].GetNullToEmpty();

                ////f.pic_mcimg = null;
                //f.pic_setandon = ds.Tables[0].Rows[i][6].GetNullToEmpty();
                //flowLayoutPanel1.Controls.Add(f);
            }
            
        }
        string lsmcg="";
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
       
            switch (lsmcg)
            {
                case "": lsmcg = "01";
                        break;
                case "01":
                    lsmcg = "02";
                    break;
                case "02":
                    lsmcg = "03";
                    break;
                case "03":
                    lsmcg = "04";
                    break;
                case "04":
                    lsmcg = "01";
                    break;
                //case "05":
                //    lsmcg = "06";
                //    break;
                //case "06":
                //    lsmcg = "07";
                //    break;
                //case "07":
                //    lsmcg = "01";
                //    break;


            }
            DataLoad1(lsmcg);
            timer1.Start();
        }

    }
}
