using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;

namespace HKInc.Ui.View.MPS_Popup
{
    public partial class XPFMPS1700 : HKInc.Service.Base.ListEditFormTemplate
    {
        IService<VI_SRC_STOCK_SUM> ModelService = (IService<VI_SRC_STOCK_SUM>)ProductionFactory.GetDomainService("VI_SRC_STOCK_SUM");
        string isnew;
        public XPFMPS1700(string item)
        {
            InitializeComponent();

            isnew = "NEW";
            this.Text = "재공재고조정";
            dp_dt.DateTime = DateTime.Today;
            lupitemcode.EditValue = item;
            tx_itemcode.Text = item;
        }

        public XPFMPS1700(VI_SRC_USE item)
        {
            InitializeComponent();

            isnew = "UPDATE";
            this.Text = "재공재고조정";
            dp_dt.DateTime = item.ResultDate.Value;
            lupitemcode.EditValue = item.SrcCode;
            tx_itemcode.Text = item.SrcCode;
            tx_inqty.Text = item.OutQty.GetNullToZero().ToString();
            tx_qty.Text = item.UseQty.GetNullToZero().ToString();
            dp_dt.ReadOnly = true;
            lupitemcode.ReadOnly = true;
            tx_itemcode.ReadOnly = true;
        }

        protected override void InitCombo()
        {
            base.InitCombo();

            lupitemcode.SetDefault(false, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p =>p.UseYn=="Y").OrderBy(o => o.ItemNm1).ToList());
          
        }
        protected override void DataLoad()
        {
        

        }
        protected override void DataSave()
        {
            string inqty = tx_inqty.Text == "" ? "0" : tx_inqty.Text;
            string outqty = tx_qty.Text == "" ? "0" : tx_qty.Text;
            string sql = "";
            if (isnew == "NEW")
            {
                sql = "  insert into [TN_SRC001T]( [RESULT_DATE],[SRC_CODE],[in_qty],[USE_QTY],[INS_DATE],[INS_ID],[MEMO]) values ("
                      + "  '" + dp_dt.DateTime.Date.ToString().Substring(0, 10) + "','" + tx_itemcode.Text + "','" + inqty + "','" + outqty + "','" + DateTime.Now.Date.ToString().Substring(0, 10) + "','" + HKInc.Utils.Common.GlobalVariable.LoginId + "','재고조정')";
            }
            else {
                sql = "update [TN_SRC001T] set in_qty='" + inqty + "', use_qty='" + outqty + "'  where RESULT_DATE='" + dp_dt.DateTime.Date.ToString().Substring(0, 10) + "' and SRC_CODE='" + tx_itemcode.Text + "'";
            }
            int i = DbRequestHandler.SetDataQury(sql);
            this.Close();
        }

      
    }
}