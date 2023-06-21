using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Service.Service;
using System.Data.SqlClient;
using System.Configuration;

namespace HKInc.Ui.View.View.PUR_POPUP
{
    /// <summary>
    /// 자재재고조정 팝업
    /// </summary>
    public partial class XPFPUR_STOCK : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_PUR2000> ModelService = (IService<TN_PUR2000>)ProductionFactory.GetDomainService("TN_PUR2000");

        private string isnew;
        DateTime preinoutdate;
        private string typeflag;

        private string pitemCode;
        private string inOutLotNo;
        private string division;
        private decimal? Qty;
        private DateTime? inOutDate;

        public XPFPUR_STOCK()
        {
            InitializeComponent();
        }        

        public XPFPUR_STOCK(string itemCode, string inOutLotNo, string division, decimal Qty, string inOutDate)
        {
            InitializeComponent();

            var iteminfo = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemCode).FirstOrDefault();

            isnew = division;
            preinoutdate = Convert.ToDateTime(inOutDate);
            pitemCode = itemCode;

            if (isnew == "재고조정")
            {
                typeflag = "본사재고조정";
                dp_dt.DateTime = preinoutdate;
                dp_dt.ReadOnly = true;
            }

            else
            {                
                typeflag = "본사재고조정";             
                dp_dt.DateTime = DateTime.Today;
                dp_dt.ReadOnly = false;
            }
            this.Text = LabelConvert.GetLabelText("StockAdjust");

            tx_itemCode.Text = itemCode;
            tx_itemName.Text = iteminfo.ItemName;
            //tx_ItemName1.Text = iteminfo.ItemName1;          
            tx_qty.Text = Qty.GetNullToZero().ToString();
            tx_lotno.Text = inOutLotNo.GetNullToEmpty();
        }
        
        protected override void InitCombo()
        {
            base.InitCombo();

            //포멧 설정
            tx_qty.Properties.Mask.EditMask = "n0";
            tx_qty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            tx_qty.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();
            SetToolbarButtonVisible(ToolbarButton.Refresh, false);
            SetToolbarButtonVisible(ToolbarButton.Export, false);
            SetToolbarButtonVisible(ToolbarButton.Confirm, false);
            SetToolbarButtonVisible(ToolbarButton.Print, false);                        
        }

        /// <summary>
        /// 저장
        /// </summary>
        protected override void DataSave()
        {
            //입고/출고 재고 수량 변경            
            var inoutDate = new SqlParameter("@DATE", dp_dt.Text);
            var itemCode = new SqlParameter("@ITEMCODE", tx_itemCode.Text);
            var lotno = new SqlParameter("@LOTNO", tx_lotno.Text);
            var qty = new SqlParameter("@QTY", tx_qty.Text == "" ? "0" : tx_qty.EditValue.GetNullToEmpty());
            var flag = new SqlParameter("@FLAG", "S");
            var userid = new SqlParameter("@USERID", GlobalVariable.LoginId);
            
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ServerInfo.GetConnectString(GlobalVariable.DefaultProductionDataBase);
            cmd.Connection = conn;
            conn.Open();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GET_PUR2000_LIST";
            cmd.Parameters.Add(inoutDate);
            cmd.Parameters.Add(itemCode);
            cmd.Parameters.Add(lotno);
            cmd.Parameters.Add(qty);
            cmd.Parameters.Add(flag);
            cmd.Parameters.Add(userid);

            int iResult = cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

            if (iResult == 1)
            {
                //MessageBox.Show("저장되엇습니다.");                
                this.Close();
            }
            else
            {
                MessageBox.Show("저장에 실패하였습니다");
                return;
            }            
        }       
    }
}
