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

namespace HKInc.Ui.View.View.PUR_POPUP
{
    /// <summary>
    /// 자재재공재고조정 팝업 창
    /// </summary>
    public partial class XPFPUR_STOCK : HKInc.Service.Base.ListEditFormTemplate
    {
        IService<TN_PUR2000> ModelService = (IService<TN_PUR2000>)ProductionFactory.GetDomainService("TN_PUR2000");
        private string isnew;
        private string  ScmYn;
        DateTime  preinoutdate;
        private string typeflag;
        private string ItemCode;

        public XPFPUR_STOCK(string itemcode, string inoutlotno, string division, decimal? qty, DateTime inoutdate, string scmyn)
        {
            InitializeComponent();

            var iteminfo = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemcode).FirstOrDefault();

            isnew = division;           
            preinoutdate = inoutdate;
            ItemCode = itemcode;
            ScmYn = scmyn;

            //if(scmyn == "Y")
            //{
            //    radioButton2.Checked = true;
            //    typeflag = "SCM재고조정";
            //}
            //else
            //{
            //    radioButton1.Checked = true;
            //    typeflag = "본사재고조정";
            //}

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

            this.Text = LabelConvert.GetLabelText("StockAdjust");
           
            tx_itemCode.Text = itemcode;
            tx_itemName.Text = iteminfo.ItemName;
            //tx_ItemName1.Text = iteminfo.ItemName1;          
            tx_qty.Text = qty.GetNullToZero().ToString();
            tx_lotno.Text = inoutlotno.GetNullToEmpty();

            //dp_dt.ReadOnly = true;
            tx_itemCode.ReadOnly = true;
            tx_itemName.ReadOnly = true;
            tx_ItemName1.ReadOnly = true;
            tx_lotno.ReadOnly = true;

            dp_dt.EditValueChanging += Dp_dt_EditValueChanging;
            radioButton1.Click += RadioButton1_Click;
            radioButton2.Click += RadioButton2_Click;

            //SCM재고조정 숨김 처리
            layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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

        private void Dp_dt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
           
        }

        protected override void InitCombo()
        {
            base.InitCombo();
            
            
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

            //if (isnew == "입고" )
            //{
            //    TN_PUR2000 newobj = new TN_PUR2000()
            //    {
            //        StockType = "IN",
            //        InoutDate = Convert.ToDateTime(dp_dt.Text),
            //        Qty = Convert.ToDecimal(inqty),
            //        ScmFlag = ScmYn,
            //        StockUser = GlobalVariable.LoginId,
            //    };

            //}
            //else if(isnew == "출고")
            //{
            //    TN_PUR2000 newobj = new TN_PUR2000()
            //    {
            //        StockType = "OUT",
            //        InoutDate = Convert.ToDateTime(dp_dt.Text),
            //        Qty = Convert.ToDecimal(inqty),
            //        ScmFlag = ScmYn,
            //        StockUser = GlobalVariable.LoginId,
            //    };
            //}
            //else if (isnew == "재고조정입고")
            //{
            //    sql = "UPDATE TN_PUR2000T " +
            //         "SET IN_QTY = '" + inqty + "' " +                   
            //         ", SRC_IN_LOT_NO = '" + Convert.ToDateTime(dp_dt.Text) + "' " +
            //         ", UPD_ID = '" + GlobalVariable.LoginId + "' " +
            //         ", UPD_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //         "WHERE STOCKTYPE = 'IN'" +
            //           "AND INOUT_DATE = '" +   + "'" +  

            //    int i = DbRequestHandler.SetDataQury(sql);
            //}
            //else if (isnew == "재고조정출고")
            //{
            //    sql = "UPDATE TN_PUR2000T " +
            //        "SET IN_QTY = '" + inqty + "' " +
            //        ", SRC_IN_LOT_NO = '" + Convert.ToDateTime(dp_dt.Text) + "' " +
            //        ", UPD_ID = '" + GlobalVariable.LoginId + "' " +
            //        ", UPD_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //        "WHERE STOCKTYPE = "

            //    int i = DbRequestHandler.SetDataQury(sql);
            //}

            if(isnew.Contains("재고조정") == true)
            {
                sql = "UPDATE TN_PUR2000T " +
                   "SET QTY = '" + qty + "' " +
                   ",INOUT_DATE = '" + dp_dt.Text + "' " +
                   ", UPD_ID = '" + GlobalVariable.LoginId + "' " +
                   ", UPD_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                   "WHERE STOCK_TYPE = '" + typeflag + "' " +
                   "  AND CONVERT(VARCHAR,INOUT_DATE,112) = '" + preinoutdate.ToString("yyyyMMdd") + "' " +
                   "  AND LOTNO = '" + tx_lotno.Text + "'";

                   int i = DbRequestHandler.SetDataQury(sql);

            }
            else
            {
                DateTime inoutdt = Convert.ToDateTime(dp_dt.EditValue);
                string lotno = tx_lotno.EditValue.GetNullToEmpty();

                ModelService.ReLoad();

                var cnt = ModelService.GetList(p => p.StockType == typeflag
                                                 && p.InoutDate == inoutdt
                                                 && p.LotNo == lotno)
                                                 .ToList();

                if(cnt.Count > 0)
                {
                    MessageBox.Show("해당날짜에 재고조정이 존재합니다. 일자를 다르게 하여 주십시오.");
                    return;

                }
                else
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
            }


            this.Close();
        }

      
    }
}