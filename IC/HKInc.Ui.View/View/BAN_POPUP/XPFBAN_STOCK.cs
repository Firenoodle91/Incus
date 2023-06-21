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
using System.Collections.Generic;

namespace HKInc.Ui.View.View.BAN_POPUP
{
    /// <summary>
    /// 20211222 오세완 차장
    /// 반제품재고조정 팝업 창
    /// </summary>
    public partial class XPFBAN_STOCK : HKInc.Service.Base.ListEditFormTemplate
    {
        #region 전역변수
        IService<TN_BAN_STOCK_ADJUST> ModelService = (IService<TN_BAN_STOCK_ADJUST>)ProductionFactory.GetDomainService("TN_BAN_STOCK_ADJUST");
        private string gs_Type;
        #endregion

        public XPFBAN_STOCK(string itemcode, string inoutlotno, string division, decimal? qty, DateTime inoutdate)
        {
            InitializeComponent();

            var iteminfo = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemcode).FirstOrDefault();

            
            dp_dt.DateTime = DateTime.Today;
            dp_dt.ReadOnly = false;
            

            this.Text = LabelConvert.GetLabelText("StockAdjust");
           
            tx_itemCode.Text = itemcode;
            tx_itemName.Text = iteminfo.ItemName;
            tx_qty.Text = qty.GetNullToZero().ToString();
            tx_lotno.Text = inoutlotno.GetNullToEmpty();

            tx_itemCode.ReadOnly = true;
            tx_itemName.ReadOnly = true;
            tx_lotno.ReadOnly = true;

            if (division != null)
                gs_Type = division;
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
            DateTime inoutdt = Convert.ToDateTime(dp_dt.EditValue);
            string lotno = tx_lotno.EditValue.GetNullToEmpty();
            string sItemcode = tx_itemCode.EditValue.GetNullToEmpty();
            decimal dQty = tx_qty.EditValue.GetDecimalNullToZero();

            ModelService.ReLoad();

            if (gs_Type == "IN" || gs_Type == "OUT")
            {
                var cnt = ModelService.GetList(p => p.StockType == gs_Type &&
                                                    p.InoutDate == inoutdt &&
                                                    p.LotNo == lotno).ToList();

                if (cnt.Count > 0)
                {
                    //MessageBox.Show("해당날짜에 재고조정이 존재합니다. 일자를 다르게 하여 주십시오.");
                    string sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_176);
                    MessageBoxHandler.Show(sMessage);
                    return;
                }

                TN_BAN_STOCK_ADJUST newobj = new TN_BAN_STOCK_ADJUST()
                {
                    StockType = gs_Type,
                    InoutDate = inoutdt,
                    LotNo = lotno,
                    ItemCode = sItemcode,
                    Qty = dQty,
                    Memo = DateTime.Now.ToString("yyyy년 MM월 dd일 재고조정"),
                    StockUser = GlobalVariable.LoginId,
                };

                ModelService.Insert(newobj);
            }
            else
            {
                // 기입력한 재고고정 수정
                List<TN_BAN_STOCK_ADJUST> updArr = ModelService.GetList(p => p.InoutDate == inoutdt &&
                                                                             p.LotNo == lotno).ToList();

                if(updArr != null)
                    if(updArr.Count > 0)
                    {
                        TN_BAN_STOCK_ADJUST updObj = updArr.FirstOrDefault();
                        if(updObj != null)
                        {
                            updObj.Qty = dQty;
                            ModelService.Update(updObj);
                        }
                    }
            }

            ModelService.Save();
            this.Close();
        }

      
    }
}