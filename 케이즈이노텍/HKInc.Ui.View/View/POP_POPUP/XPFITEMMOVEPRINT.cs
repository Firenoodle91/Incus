using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using System.Data.SqlClient;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Collections.Generic;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 이동표 출력
    /// </summary>
    public partial class XPFITEMMOVEPRINT : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<VI_PUR_STOCK_IN_LOT_NO> ModelService = (IService<VI_PUR_STOCK_IN_LOT_NO>)ProductionFactory.GetDomainService("VI_PUR_STOCK_IN_LOT_NO");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        private string ProductLotNo = null;
        private decimal ResultSumQty = 0; //누적생산수량

        #endregion
        public XPFITEMMOVEPRINT()
        {
            InitializeComponent();
        }

        public XPFITEMMOVEPRINT(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ItemMovePrint");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = new Size(this.Size.Width, this.Size.Height - 40);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            ProductLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            ResultSumQty = parameter.GetValue(PopupParameter.Value_2).GetDecimalNullToZero();
            //Rest_ResultSumQty = parameter.GetValue(PopupParameter.Value_4).GetDecimalNullToZero();
            
            btn_Print.Click += Btn_Print_Click;
            btn_RePrint.Click += Btn_RePrint_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_ReA4.Click += Btn_ReType_Click;
            btn_ReBar.Click += Btn_ReType_Click;

            layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", TEMP_XFPOP1000_Obj.WorkNo);

                var result = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).ToList();

                lup_ItemMoveNo.SetDefault(false, "ItemMoveNo", "ItemMoveNo", result, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

                lup_ItemMoveNo.Properties.View.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
                lup_ItemMoveNo.Properties.View.Appearance.Row.TextOptions.HAlignment = HorzAlignment.Center;

                lup_ItemMoveNo.Properties.View.OptionsView.ShowColumnHeaders = true;
                lup_ItemMoveNo.Properties.View.Columns[0].Visible = false;
                lup_ItemMoveNo.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "WorkNo",
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("WorkNo"),
                    VisibleIndex = 1,
                    Visible = true
                });
                lup_ItemMoveNo.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ProductLotNo",
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("ProductLotNo"),
                    VisibleIndex = 2,
                    Visible = true
                });
                lup_ItemMoveNo.Properties.PopupFormSize = new Size(600, 300);

                if (result.Count == 0)
                {
                    btn_RePrint.Enabled = false;
                    lup_ItemMoveNo.Enabled = false;
                }
            }

            lup_ItemMoveNo.SetFontSize(new Font("맑은 고딕", 12f));
            lup_ItemMoveNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            // 20210820 오세완 차장 이렇게 부품이동표번호를 전달하는 곳이 없어서 생략 처리
            //var itemMoveNo = PopupParam.GetValue(PopupParameter.Value_2).GetNullToEmpty();
            //if (!itemMoveNo.IsNullOrEmpty())
            //    lup_ItemMoveNo.EditValue = itemMoveNo;

            btn_Print.Text = LabelConvert.GetLabelText("NewPrint");
            btn_RePrint.Text = LabelConvert.GetLabelText("RePrint");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            if (TEMP_XFPOP1000_Obj.ProcessSeq > 1)
            {
                btn_Print.Enabled = false;
            }

        }

        /// <summary>
        /// 20210820 오세완 차장
        /// 재발행시 부품이동표 양식 선택
        /// </summary>
        private void Btn_ReType_Click(object sender, EventArgs e)
        {
            SimpleButton sb = sender as SimpleButton;
            //if(sb.Name == "btn_ReBar")
            //{
            //    gs_ItemMoveType = "Bar";
            //    btn_ReA4.Enabled = false;
            //}
            //else
            //{
            //    gs_ItemMoveType = "A4";
            //    btn_ReA4.Enabled = false;
            //}

            btn_Print.Enabled = false;

            RePrint();
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            var TN_MPS1201 = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();

            if (TN_MPS1201.TN_MPS1202List == null)
            {
                MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_115));
                return;
            }

            // 20211104 오세완 차장 공정포장 수량 팝업 출력이 누락되이서 추가 처리
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, obj.ProcessPackQty);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVEPRINT_BOX, param, NewPrintCallback);
            form.ShowPopup(true);

            //NewPrint();
        }

        private void NewPrint()
        {
            var obj = TEMP_XFPOP1000_Obj;
            var TN_MPS1201 = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            var itemMoveNo = DbRequestHandler.GetItemMoveSeq(obj.WorkNo);

            TN_MPS1201.ItemMoveNo = itemMoveNo;

            decimal resultQty = 0;
            decimal okQty = 0;
            decimal badQty = 0;

            foreach (var s in TN_MPS1201.TN_MPS1202List)
            {
                if (s.ItemMoveNo.IsNullOrEmpty())
                {
                    resultQty += s.ResultQty.GetDecimalNullToZero();
                    okQty += s.OkQty.GetDecimalNullToZero();
                    badQty += s.BadQty.GetDecimalNullToZero();
                    s.ItemMoveNo = itemMoveNo;
                }
            }

            TN_MPS1201.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(TN_MPS1201);

            var newItemMoveNo = new TN_ITEM_MOVE();
            newItemMoveNo.ItemMoveNo = itemMoveNo;
            newItemMoveNo.WorkNo = TN_MPS1201.WorkNo;
            newItemMoveNo.ProcessCode = TN_MPS1201.ProcessCode;
            newItemMoveNo.ProcessSeq = TN_MPS1201.ProcessSeq;
            newItemMoveNo.ProductLotNo = TN_MPS1201.ProductLotNo;
            newItemMoveNo.ResultSumQty = TN_MPS1201.ResultSumQty;
            newItemMoveNo.OkSumQty = TN_MPS1201.OkSumQty;
            newItemMoveNo.BadSumQty = TN_MPS1201.BadSumQty;
            newItemMoveNo.ResultQty = resultQty;
            newItemMoveNo.OkQty = okQty;
            newItemMoveNo.BadQty = badQty;

            ModelService.InsertChild(newItemMoveNo);
            ModelService.Save();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, TN_MPS1201.ItemMoveNo);
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        /// <summary>
        /// 이동표 새 출력 CallBack
        /// </summary>
        private void NewPrintCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var BoxInQty = e.Map.GetValue(PopupParameter.Value_1).GetDecimalNullToZero();

            var obj = TEMP_XFPOP1000_Obj;
            var TN_MPS1201 = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();

            var itemMoveNo = DbRequestHandler.GetItemMoveSeq(obj.WorkNo);

            TN_MPS1201.ItemMoveNo = itemMoveNo;

            decimal resultQty = 0;
            decimal okQty = 0;
            decimal badQty = 0;

            foreach (var v in TN_MPS1201.TN_MPS1202List)
            {
                if (v.ItemMoveNo.IsNullOrEmpty())
                {
                    resultQty += v.ResultQty.GetDecimalNullToZero();
                    okQty += v.OkQty.GetDecimalNullToZero();
                    badQty += v.BadQty.GetDecimalNullToZero();
                    v.ItemMoveNo = itemMoveNo;
                }
            }

            TN_MPS1201.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(TN_MPS1201);

            var newItemMoveNo = new TN_ITEM_MOVE();
            newItemMoveNo.ItemMoveNo = itemMoveNo;
            newItemMoveNo.WorkNo = TN_MPS1201.WorkNo;
            newItemMoveNo.ProcessCode = TN_MPS1201.ProcessCode;
            newItemMoveNo.ProcessSeq = TN_MPS1201.ProcessSeq;
            newItemMoveNo.ProductLotNo = TN_MPS1201.ProductLotNo;
            newItemMoveNo.BoxInQty = BoxInQty;
            newItemMoveNo.ResultSumQty = TN_MPS1201.ResultSumQty;
            newItemMoveNo.OkSumQty = TN_MPS1201.OkSumQty;
            newItemMoveNo.BadSumQty = TN_MPS1201.BadSumQty;
            newItemMoveNo.ResultQty = resultQty;
            newItemMoveNo.OkQty = okQty;
            newItemMoveNo.BadQty = badQty;

            ModelService.InsertChild(newItemMoveNo);

            ModelService.Save();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, TN_MPS1201.ItemMoveNo);

            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_RePrint_Click(object sender, EventArgs e)
        {
            if(lup_ItemMoveNo.EditValue.GetNullToEmpty() == "")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
            }
            else
            {
                RePrint();
            }
        }

        /// <summary>
        /// 20210820 오세완 차장
        /// 기존에 출력 스타일을 구현
        /// </summary>
        private void RePrint()
        {
            var itemMoveNo = lup_ItemMoveNo.EditValue.GetNullToEmpty();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, "Re");
            param.SetValue(PopupParameter.Value_1, itemMoveNo);

            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }
    }
}
