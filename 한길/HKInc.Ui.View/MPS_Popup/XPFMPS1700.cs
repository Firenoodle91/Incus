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
        int UpdateRowId = -1;
        public XPFMPS1700(string item)
        {
            InitializeComponent();

            isnew = "NEW";
            this.Text = "재공재고조정";
            dp_dt.DateTime = DateTime.Today;
            lupitemcode.EditValue = item;
            tx_itemcode.Text = item;
            lupitemcode.ReadOnly = true;
            tx_itemcode.ReadOnly = true;
        }

        public XPFMPS1700(VI_SRC_STOCK_SUM item)
        {
            InitializeComponent();

            isnew = "NEW";
            this.Text = "재공재고조정";
            dp_dt.DateTime = DateTime.Today;
            lupitemcode.EditValue = item.SrcCode;
            tx_itemcode.Text = item.TN_STD1100.ItemNm;
            lupitemcode.ReadOnly = true;
            tx_itemcode.ReadOnly = true;
            tx_inqty.Text = "0";
            tx_qty.Text = "0";
        }

        public XPFMPS1700(bool newFlag, VI_SRC_USE item)
        {
            InitializeComponent();
            this.Text = "재공재고조정";
            lupitemcode.ReadOnly = true;
            tx_itemcode.ReadOnly = true;
            textLotNo.ReadOnly = true;

            if (newFlag)
            {
                isnew = "NEW";
                dp_dt.DateTime = DateTime.Today;
                lupitemcode.EditValue = item.SrcCode;
                tx_itemcode.Text = item.TN_STD1100.ItemNm;
                tx_inqty.EditValue = "0";
                tx_qty.EditValue = "0";
                textLotNo.EditValue = item.SrcLotNo;
            }
            else
            {
                isnew = "UPDATE";
                dp_dt.ReadOnly = true;
                dp_dt.DateTime = item.ResultDate.Value;
                lupitemcode.EditValue = item.SrcCode;
                tx_itemcode.EditValue = item.TN_STD1100.ItemNm;
                tx_inqty.EditValue = item.OutQty.GetNullToZero().ToString();
                tx_qty.EditValue = item.UseQty.GetNullToZero().ToString();
                UpdateRowId = item.Memo.Substring(item.Memo.IndexOf('[') + 1, item.Memo.Length - item.Memo.IndexOf('[') - 2).GetIntNullToZero();
                tx_inqty.EditValue = item.OutQty.ToString();
                tx_qty.EditValue = item.UseQty.ToString();
                textLotNo.EditValue = item.SrcLotNo;
            }
        }
        
        protected override void InitCombo()
        {
            base.InitCombo();

            lupitemcode.SetDefault(false, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p =>p.UseYn=="Y").OrderBy(o => o.ItemNm1).ToList());
            
            tx_inqty.Properties.Mask.EditMask = "n0";
            tx_qty.Properties.Mask.EditMask = "n0";

            tx_inqty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            tx_qty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            tx_inqty.Properties.Mask.UseMaskAsDisplayFormat = true;
            tx_qty.Properties.Mask.UseMaskAsDisplayFormat = true;

        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();
            SetToolbarButtonVisible(ToolbarButton.Confirm, false);
        }

        protected override void DataLoad()
        {
        

        }
        protected override void DataSave()
        {
            string inqty = tx_inqty.Text == "" ? "0" : tx_inqty.EditValue.GetNullToEmpty();
            string outqty = tx_qty.Text == "" ? "0" : tx_qty.EditValue.GetNullToEmpty();
            string sql = "";
            if (isnew == "NEW")
            {
                sql = "INSERT INTO TN_SRC001T (RESULT_DATE, SRC_CODE, IN_QTY, USE_QTY, INS_DATE, INS_ID, MEMO, SRC_LOT_NO)" +
                      "VALUES ( '" + dp_dt.DateTime.Date.ToString().Substring(0, 10) + "', " +
                      "'" + lupitemcode.EditValue.GetNullToEmpty() + "', " +
                      "'" + inqty + "', " +
                      "'" + outqty + "', " +
                      "'" + DateTime.Now.Date.ToString().Substring(0, 10) + "', " +
                      "'" + HKInc.Utils.Common.GlobalVariable.LoginId + "', " +
                      "'재고조정,ROWID:', " +
                      "'" + textLotNo.EditValue.GetNullToEmpty() + "')";
            }
            else {
                sql = "UPDATE TN_SRC001T " +
                      "SET IN_QTY = '" + inqty + "' " +
                      ", USE_QTY = '" + outqty + "' " +
                      "WHERE ROW_ID = " + UpdateRowId.ToString();
            }
            int i = DbRequesHandler.SetDataQury(sql);
            this.Close();
        }

      
    }
}