using System;
using System.Linq;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using DevExpress.XtraEditors.Mask;
using HKInc.Service.Helper;
using System.Windows.Forms;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.View.PUR_Popup
{
    /// <summary>
    /// 자재재공재고조정 팝업 창
    /// </summary>
    public partial class XPFPUR_STOCK : HKInc.Service.Base.ListEditFormTemplate
    {
        #region 전역변수
        IService<TN_PUR2000> ModelService = (IService<TN_PUR2000>)ProductionFactory.GetDomainService("TN_PUR2000");
        private string isnew;
        DateTime  preinoutdate;
        private string typeflag;
        private string ItemCode;
        #endregion

        public XPFPUR_STOCK(string itemcode, string inoutlotno, string division, decimal qty, DateTime inoutdate)
        {
            InitializeComponent();

            var iteminfo = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemcode).FirstOrDefault();

            isnew = division;           
            preinoutdate = inoutdate;
            ItemCode = itemcode;

            if(isnew == "본사재고조정")
            {
                typeflag = "본사재고조정";
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                dp_dt.DateTime = preinoutdate;
                dp_dt.ReadOnly = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;

            }
            else if (isnew == "SCM재고조정")
            {
                typeflag = "SCM재고조정";
                radioButton2.Checked = true;
                radioButton1.Checked = false;
                dp_dt.DateTime = preinoutdate;
                dp_dt.ReadOnly = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
            else
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                dp_dt.DateTime = DateTime.Today;
                dp_dt.ReadOnly = false;
                typeflag = "본사재고조정";
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }

            this.Text = "재고조정";
           
            tx_itemCode.Text = itemcode;

            if(iteminfo != null)
            {
                tx_itemName.Text = iteminfo.ItemNm;
                tx_ItemName1.Text = iteminfo.ItemNm1;
            }
            
            tx_qty.Text = qty.GetNullToZero().ToString();
            tx_lotno.Text = inoutlotno.GetNullToEmpty();

            if (inoutlotno.GetNullToEmpty() != "")
                if (inoutlotno.IndexOf("IN") > 0)
                    lcAdjustQty.Text = "입고조정수량";
                else
                    lcAdjustQty.Text = "사용조정수량";

            tx_itemCode.ReadOnly = true;
            tx_itemName.ReadOnly = true;
            tx_ItemName1.ReadOnly = true;
            tx_lotno.ReadOnly = true;

            radioButton1.Click += RadioButton1_Click;
            radioButton2.Click += RadioButton2_Click;
        }

        private void RadioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            typeflag = "SCM재고조정";
        }

        private void RadioButton1_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            typeflag = "본사재고조정";
        }

        protected override void InitCombo()
        {
            tx_qty.Properties.Mask.EditMask = "n2";
            tx_qty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            tx_qty.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();
            SetToolbarButtonVisible(ToolbarButton.Confirm, false);
        }

        protected override void DataSave()
        {
            string qty = tx_qty.Text == "" ? "0" : tx_qty.EditValue.GetNullToEmpty();
            string sql = "";

            if(isnew.Contains("재고조정") == true)
            {
                // 20220415 오세완 차장 key값을 변경해야 해서 entity가 아니라 procedure로 처리
                sql = "exec USP_UPD_PUR2000 '" + typeflag + "', '" + preinoutdate.ToShortDateString() + "', '" + tx_lotno.EditValue.GetNullToEmpty() + "', " + 
                    qty + ", '" + GlobalVariable.LoginId + "', '" + Convert.ToDateTime(dp_dt.EditValue).ToShortDateString() + "' ";
                
                int i = DbRequestHandler.SetDataQury(sql);
            }
            else
            {
                DateTime inoutdt = Convert.ToDateTime(dp_dt.EditValue);
                string lotno = tx_lotno.EditValue.GetNullToEmpty();

                ModelService.ReLoad();

                var cnt = ModelService.GetList(p => p.StockType == typeflag && 
                                                    p.InoutDate == inoutdt && 
                                                    p.LotNo == lotno).ToList();

                bool bInsert = false;
                if (cnt == null)
                    bInsert = true;
                else if (cnt.Count == 0)
                    bInsert = true;

                if(bInsert)
                {
                    TN_PUR2000 newobj = new TN_PUR2000()
                    {
                        StockType = typeflag,
                        InoutDate = Convert.ToDateTime(dp_dt.Text),
                        LotNo = tx_lotno.EditValue.GetNullToEmpty(),
                        ItemCode = ItemCode,
                        Qty = Convert.ToDecimal(qty),
                        Memo = DateTime.Now.ToString("yyyy년 MM월 dd일 재고조정"),
                        StockUser = GlobalVariable.LoginId,
                    };

                    ModelService.Insert(newobj);
                    ModelService.Save();
                }
                else
                {
                    MessageBox.Show("해당날짜에 재고조정이 존재합니다. 일자를 다르게 하여 주십시오.");
                }
            }

            this.Close();
        }
    }
}