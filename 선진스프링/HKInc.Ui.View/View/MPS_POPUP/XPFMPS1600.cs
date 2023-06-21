using System;
using System.Linq;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using DevExpress.XtraEditors.Mask;
using HKInc.Service.Helper;
using System.Windows.Forms;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.View.View.MPS_POPUP
{
    /// <summary>
    /// 자재재공재고조정 팝업 창
    /// </summary>
    public partial class XPFMPS1600 : HKInc.Service.Base.ListEditFormTemplate
    {
        IService<VI_SRC_STOCK_SUM> ModelService = (IService<VI_SRC_STOCK_SUM>)ProductionFactory.GetDomainService("VI_SRC_STOCK_SUM");
        string isnew;
        int UpdateRowId = -1;
        public XPFMPS1600(string item)
        {
            InitializeComponent();

            isnew = "NEW";
            this.Text = LabelConvert.GetLabelText("SrcStockAdjust");
            dp_dt.DateTime = DateTime.Today;
            tx_itemCode.Text = item;
            tx_itemName.Text = item;
            tx_itemCode.ReadOnly = true;
            tx_itemName.ReadOnly = true;
        }

        public XPFMPS1600(VI_SRC_STOCK_SUM item)
        {
            InitializeComponent();

            isnew = "NEW";
            this.Text = LabelConvert.GetLabelText("SrcStockAdjust");
            dp_dt.DateTime = DateTime.Today;
            tx_itemCode.Text = item.SrcItemCode;
            tx_itemName.Text = item.TN_STD1100.ItemName;
            tx_itemCode.ReadOnly = true;
            tx_itemName.ReadOnly = true;
            tx_inqty.Text = "0";
            tx_qty.Text = "0";
        }

        public XPFMPS1600(VI_SRC_USE item)
        {
            InitializeComponent();

            isnew = "UPDATE";
            this.Text = LabelConvert.GetLabelText("SrcStockAdjust");
            dp_dt.DateTime = item.ResultDate.Value;
            tx_itemCode.Text = item.SrcItemCode;
            tx_itemName.Text = item.TN_STD1100.ItemName;
            tx_inqty.Text = item.OutQty.GetNullToZero().ToString();
            tx_qty.Text = item.UseQty.GetNullToZero().ToString();
            dp_dt.ReadOnly = true;
            tx_itemCode.ReadOnly = true;
            tx_itemName.ReadOnly = true;
            UpdateRowId = item.Memo.Substring(item.Memo.IndexOf('[') + 1, item.Memo.Length - item.Memo.IndexOf('[') - 2).GetIntNullToZero();
            tx_inqty.Text = item.OutQty.ToString();
            tx_qty.Text = item.UseQty.ToString();
        }
        
        protected override void InitCombo()
        {
            base.InitCombo();
            
            tx_inqty.Properties.Mask.EditMask = "n2";
            tx_qty.Properties.Mask.EditMask = "n2";

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

        protected override void DataSave()
        {
            string inqty = tx_inqty.Text == "" ? "0" : tx_inqty.EditValue.GetNullToEmpty();
            string outqty = tx_qty.Text == "" ? "0" : tx_qty.EditValue.GetNullToEmpty();
            string sql = "";
            if (isnew == "NEW")
            {
                sql = "INSERT INTO TN_SRC1001T (STOCK_ADJUST_DATE, SRC_CODE, IN_QTY, USE_QTY, INS_DATE, INS_ID)" +
                      "VALUES ( '" + dp_dt.DateTime.Date.ToString().Substring(0, 10) + "', " +
                      "'" + tx_itemCode.EditValue.GetNullToEmpty() + "', " +
                      "'" + inqty + "', " +
                      "'" + outqty + "', " +
                      "'" + DateTime.Now.Date.ToString().Substring(0, 10) + "', " +
                      "'" + GsValue.loginId + "')";
            }
            else {
                sql = "UPDATE TN_SRC1001T " +
                      "SET IN_QTY = '" + inqty + "' " +
                      ", USE_QTY = '" + outqty + "' " +
                      "WHERE ROW_ID = " + UpdateRowId.ToString();
            }
            int i = DbRequestHandler.SetDataQury(sql);
            this.Close();
        }

      
    }
}