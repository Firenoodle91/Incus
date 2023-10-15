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

        /// <summary>
        /// 20210902 오세완 차장 
        /// 박스에 포장되지 못한 제품 수량
        /// </summary>
        private decimal Rest_ResultSumQty = 0;

        /// <summary>
        /// 20210819 오세완 차장 프레스 POP에서 부품이동표 출력 요청을 알기 위함
        /// </summary>
        private string gs_ItemMoveType = "";

        private bool gs_PLC_Pop = false;

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
            Rest_ResultSumQty = parameter.GetValue(PopupParameter.Value_4).GetDecimalNullToZero();

            // 20210819 오세완 차장 프레스 POP에서 부품이동표 출력 요청을 알기 위함
            string sParam_3 = parameter.GetValue(PopupParameter.Value_3).GetNullToEmpty();
            if (sParam_3 != "")
                gs_PLC_Pop = true;
            
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
                var _ProcessSeq = new SqlParameter("@ProcessSeq", TEMP_XFPOP1000_Obj.ProcessSeq);

                var result = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).ToList();
                //var result = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V2 @WorkNo", _workNo).ToList(); // 20210616 오세완 차장 수동으로 만든 반제품 작업지시의 부품이동표 출력을 위해서 수정처리

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

            // 20211122 오세완 차장 대영은 공정순번 상관없이 부품이동표를 분할할 수 있어야 한다. 
            //if (TEMP_XFPOP1000_Obj.ProcessSeq > 1)
            //{
            //    btn_Print.Enabled = false;
            //}

        }

        /// <summary>
        /// 20210820 오세완 차장
        /// 재발행시 부품이동표 양식 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ReType_Click(object sender, EventArgs e)
        {
            SimpleButton sb = sender as SimpleButton;
            if(sb.Name == "btn_ReBar")
            {
                gs_ItemMoveType = "Bar";
                btn_ReA4.Enabled = false;
            }
            else
            {
                gs_ItemMoveType = "A4";
                btn_ReA4.Enabled = false;
            }

            btn_Print.Enabled = false;

            RePrint();
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            var TN_MPS1201 = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();

            bool bDont_Exists_Mps1202 = false;
            if (TN_MPS1201.TN_MPS1202List == null)
                bDont_Exists_Mps1202 = true;
            else if (TN_MPS1201.TN_MPS1202List.Count == 0)
                bDont_Exists_Mps1202 = true;

            if (bDont_Exists_Mps1202)
            {
                MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_115));
                return;
            }
            else if(ResultSumQty <= 0)
            {
                bool bNotResult = false;
                if (obj.ProcessSeq > 1)
                {
                    TN_MPS1202 last_Result = TN_MPS1201.TN_MPS1202List.LastOrDefault();
                    if (last_Result != null)
                        if (last_Result.ResultQty <= 0)
                            bNotResult = true;
                }
                else
                    bNotResult = true;

                if(bNotResult)
                {
                    // 20211001 오세완 차장 신부장님 요청으로 실적이 없는 경우는 이동표 발행을 못하게 막음
                    MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_115));
                    return;
                }
            }

            // 20210829 오세완 차장 검증방법이 이상한 것 같아서 변경
            //if (!TN_MPS1201.TN_MPS1202List.Any(p => p.ItemMoveNo.IsNullOrEmpty()))
            //{
            //    MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_115));
            //    //MessageBoxHandler.Show("출력할 수 있는 실적이 없습니다. 실적 등록 후 출력해 주시기 바랍니다.");
            //    return;
            //}

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, obj.ProcessPackQty);

            if(gs_PLC_Pop)
            {
                // 20210829 오세완 차장 입력할 수 있는 최대치를 지정
                //decimal dMaxQty = ResultSumQty + TN_MPS1201.WorkerInputResultQty.GetDecimalNullToZero() + Rest_ResultSumQty;
                // 20210907 오세완 차장 수동입력 수치는 이미 반영되어 있음, 또한 나머지 수량을 더해서 발행하는 로직 제거, 대신 나머지 수량을 +1하여 남는 수량으로 발행하는 로직으로 변경(김이사님 지시)
                decimal dMaxQty = ResultSumQty;
                param.SetValue(PopupParameter.Value_2, dMaxQty);

                // 20210819 오세완 차장 프레스 POP에서 부품이동표 출력 요청을 알기 위함
                //IPopupForm form1 = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVEPRINT_BOX_V2, param, NewPrintCallback);
                IPopupForm form1 = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVEPRINT_BOX_V2, param, NewPrintCallback_Press);
                form1.ShowPopup(true);
            }
            else
            {
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVEPRINT_BOX, param, NewPrintCallback);
                form.ShowPopup(true);
            }
        }

        /// <summary>
        /// 20210901 오세완 차장 
        /// 대영은 프레스 공정은 이동표 발행이 실적 입력으로 간주한다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPrintCallback_Press(object sender, PopupArgument e)
        {
            if (e == null)
                return;
            else
            {
                // 20211001 오세완 차장 사용자가 발행하지 않고 취소처리한 경우 이동표 발행을 하지 않기 위해 추가
                string sKeyValue = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty();
                if(sKeyValue != "")
                {
                    if (sKeyValue == "Cancel")
                        return;
                }
            }

            decimal dBoxInQty = e.Map.GetValue(PopupParameter.Value_1).GetDecimalNullToZero();
            decimal dPerBoxQty = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero();

            string sNew_Item_move_no = DbRequestHandler.GetItemMoveSeq(TEMP_XFPOP1000_Obj.WorkNo);
            int iItem_move_no_seq;
            int.TryParse( sNew_Item_move_no.Right(3), out iItem_move_no_seq);

            TN_MPS1201 temp_Result_1201 = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && 
                                                                                     p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode && 
                                                                                     p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq && 
                                                                                     p.ResultEndDate == null).LastOrDefault();

            if(temp_Result_1201 != null)
            {
                temp_Result_1201.ItemMoveNo = sNew_Item_move_no;
                TN_MPS1202 mps_Last_result;

                if (iItem_move_no_seq > 1)
                {
                    // 20210901 오세완 차장 공정이동표 발생시 계산되었던 실적으로 tn_item_move후에 mps1202에 insert
                    if (temp_Result_1201.TN_MPS1202List != null)
                    {
                        if (temp_Result_1201.TN_MPS1202List.Count > 0)
                        {
                            foreach (TN_MPS1202 each in temp_Result_1201.TN_MPS1202List)
                            {
                                if (each.ItemMoveNo.IsNullOrEmpty())
                                {
                                    each.ItemMoveNo = sNew_Item_move_no;
                                }
                            }

                            temp_Result_1201.UpdateTime = DateTime.Now;
                            temp_Result_1201.UpdateId = GlobalVariable.LoginId;
                            ModelService.UpdateChild<TN_MPS1201>(temp_Result_1201);

                            // 20210907 오세완 차장 수동실적이 이미 포함이 되어 있기 때문에 생락
                            //decimal dMaxQty = ResultSumQty + temp_Result_1201.WorkerInputResultQty.GetDecimalNullToZero();

                            // 부품 이동표 생성
                            TN_ITEM_MOVE new_Itemmove = new TN_ITEM_MOVE()
                            {
                                ItemMoveNo = sNew_Item_move_no,
                                WorkNo = temp_Result_1201.WorkNo,
                                ProcessCode = temp_Result_1201.ProcessCode,
                                ProcessSeq = temp_Result_1201.ProcessSeq,
                                ProductLotNo = temp_Result_1201.ProductLotNo,
                                BoxInQty = dBoxInQty,
                                ResultSumQty = temp_Result_1201.ResultSumQty,
                                OkSumQty = temp_Result_1201.OkSumQty,
                                BadSumQty = temp_Result_1201.BadSumQty,
                                ResultQty = ResultSumQty,
                                OkQty = ResultSumQty,
                                //ResultQty = dMaxQty, // 20210907 오세완 차장 수동실적이 이미 포함이 되어 있기 때문에 생락
                                //OkQty = dMaxQty,
                                Temp = dPerBoxQty.ToString()
                            };
                            ModelService.InsertChild<TN_ITEM_MOVE>(new_Itemmove);

                            // 신규 실적 등록
                            mps_Last_result = temp_Result_1201.TN_MPS1202List.OrderBy(p => p.ResultSeq).LastOrDefault();
                            if (mps_Last_result != null)
                            {
                                TN_MPS1202 mps_New_result = new TN_MPS1202()
                                {
                                    WorkNo = mps_Last_result.WorkNo,
                                    ProcessCode = mps_Last_result.ProcessCode,
                                    ProcessSeq = mps_Last_result.ProcessSeq,
                                    ProductLotNo = mps_Last_result.ProductLotNo,
                                    ResultSeq = mps_Last_result.ResultSeq + 1,
                                    ItemCode = temp_Result_1201.ItemCode,
                                    CustomerCode = temp_Result_1201.CustomerCode,
                                    ResultInsDate = DateTime.Today,
                                    ResultQty = 0,
                                    OkQty = 0,
                                    BadQty = 0,
                                    MachineCode = mps_Last_result.MachineCode,
                                    WorkId = GlobalVariable.LoginId
                                };

                                ModelService.InsertChild<TN_MPS1202>(mps_New_result);
                            }

                            ModelService.Save();
                        }
                    }
                }
                else
                {
                    // 20210901 오세완 차장 tn_item_move에 생성 후 mps1202에 insert
                    decimal dTemp_ResultQty = 0m;
                    decimal dTemp_OkQty = 0m;
                    decimal dTemp_BadQty = 0m;

                    if (temp_Result_1201.TN_MPS1202List != null)
                    {
                        if (temp_Result_1201.TN_MPS1202List.Count > 0)
                        {
                            foreach (TN_MPS1202 each in temp_Result_1201.TN_MPS1202List)
                            {
                                if (each.ItemMoveNo.IsNullOrEmpty())
                                {
                                    dTemp_ResultQty += each.ResultQty.GetDecimalNullToZero();
                                    dTemp_OkQty += each.OkQty.GetDecimalNullToZero();
                                    dTemp_BadQty += each.BadQty.GetDecimalNullToZero();
                                    each.ItemMoveNo = sNew_Item_move_no;
                                }
                            }

                            temp_Result_1201.UpdateTime = DateTime.Now;
                            temp_Result_1201.UpdateId = GlobalVariable.LoginId;
                            ModelService.UpdateChild<TN_MPS1201>(temp_Result_1201);

                            // 부품 이동표 생성
                            TN_ITEM_MOVE new_Itemmove = new TN_ITEM_MOVE()
                            {
                                ItemMoveNo = sNew_Item_move_no,
                                WorkNo = temp_Result_1201.WorkNo,
                                ProcessCode = temp_Result_1201.ProcessCode,
                                ProcessSeq = temp_Result_1201.ProcessSeq,
                                ProductLotNo = temp_Result_1201.ProductLotNo,
                                BoxInQty = dBoxInQty,
                                ResultSumQty = temp_Result_1201.ResultSumQty,
                                OkSumQty = temp_Result_1201.OkSumQty,
                                BadSumQty = temp_Result_1201.BadSumQty,
                                ResultQty = dTemp_ResultQty,
                                OkQty = dTemp_OkQty,
                                BadQty = dTemp_BadQty,
                                Temp = dPerBoxQty.ToString()
                            };
                            ModelService.InsertChild<TN_ITEM_MOVE>(new_Itemmove);

                            // 신규 실적 등록
                            mps_Last_result = temp_Result_1201.TN_MPS1202List.OrderBy(p => p.ResultSeq).LastOrDefault();
                            if (mps_Last_result != null)
                            {
                                TN_MPS1202 mps_New_result = new TN_MPS1202()
                                {
                                    WorkNo = mps_Last_result.WorkNo,
                                    ProcessCode = mps_Last_result.ProcessCode,
                                    ProcessSeq = mps_Last_result.ProcessSeq,
                                    ProductLotNo = mps_Last_result.ProductLotNo,
                                    ResultSeq = mps_Last_result.ResultSeq + 1,
                                    ItemCode = temp_Result_1201.ItemCode,
                                    CustomerCode = temp_Result_1201.CustomerCode,
                                    ResultInsDate = DateTime.Today,
                                    ResultQty = 0,
                                    OkQty = 0,
                                    BadQty = 0,
                                    MachineCode = mps_Last_result.MachineCode,
                                    WorkId = GlobalVariable.LoginId
                                };

                                ModelService.InsertChild<TN_MPS1202>(mps_New_result);
                            }

                            ModelService.Save();
                        }
                    }
                }

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, temp_Result_1201.ItemMoveNo);

                gs_ItemMoveType = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty(); 
                param.SetValue(PopupParameter.Value_2, gs_ItemMoveType);
                param.SetValue(PopupParameter.Value_3, dPerBoxQty);

                ReturnPopupArgument = new PopupArgument(param);
                ActClose();
            }
        }

        /// <summary>
        /// 이동표 새 출력 CallBack
        /// </summary>
        private void NewPrintCallback(object sender, PopupArgument e)
        {
            if (e == null)
                return;
            else
            {
                // 20211123 오세완 차장 사용자가 발행하지 않고 취소처리한 경우 이동표 발행을 하지 않기 위해 추가
                string sKeyValue = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty();
                if (sKeyValue != "")
                {
                    if (sKeyValue == "Cancel")
                        return;
                }
            }

            var BoxInQty = e.Map.GetValue(PopupParameter.Value_1).GetDecimalNullToZero();

            var obj = TEMP_XFPOP1000_Obj;
            //var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
            var TN_MPS1201 = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();

            // 20211123 오세완 차장 2공정 이후에 이동표를 새로 발행하면 발행하기 전에 대한 실적을 출력해야 한다. 
            string sBefore_ItemMoveNo = "";
            if(obj.ProcessSeq > 1)
                if(TN_MPS1201 != null)
                   sBefore_ItemMoveNo = TN_MPS1201.ItemMoveNo;

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

            // 20210830 오세완 차장 프레스 공정에서 공정박스당 상품수량 수량 저장
            if(gs_PLC_Pop)
            {
                decimal dPerBoxQty = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero();
                newItemMoveNo.Temp = dPerBoxQty.ToString();

                // 20210831 오세완 차장 이전 부품이동표에 입력한 실적에서 총 실적을 제외한 수량
                decimal dMaxQty = ResultSumQty + TN_MPS1201.WorkerInputResultQty.GetDecimalNullToZero();
                newItemMoveNo.ResultQty = dMaxQty;
                newItemMoveNo.OkQty = dMaxQty;
                newItemMoveNo.ResultSumQty = dMaxQty;
                newItemMoveNo.OkSumQty = dMaxQty;
            }

            ModelService.InsertChild(newItemMoveNo);

            ModelService.Save();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, TN_MPS1201.ItemMoveNo);

            // 20211123 오세완 차장 2공정 이후에 이동표를 새로 발행하면 발행하기 전에 대한 실적을 출력해야 한다. 
            if (obj.ProcessSeq > 1)
            {
                param.SetValue(PopupParameter.Value_2, sBefore_ItemMoveNo);
                param.SetValue(PopupParameter.Value_3, BoxInQty); // 20211124 오세완 차장 기존에 입력한 박스 수량이 아니라 새로 입력한 박스의 수량이 되어야 한다. 대신 이동표의 수치에는 변화없이 출력 수량만 영향주는 걸로.

                List<TN_ITEM_MOVE> updItem_Arr = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == sBefore_ItemMoveNo && p.ProcessCode == obj.ProcessCode).ToList();
                if(updItem_Arr != null)
                    if(updItem_Arr.Count > 0)
                    {
                        TN_ITEM_MOVE updItem = updItem_Arr.FirstOrDefault();
                        if (updItem != null)
                        {
                            updItem.Temp = BoxInQty.ToString();
                            ModelService.UpdateChild<TN_ITEM_MOVE>(updItem);
                            ModelService.Save();
                        }
                    }
            }
                

            // 20210819 오세완 차장 프레스 POP에서 부품이동표 출력 요청을 알기 위함
            if (gs_PLC_Pop)
            {
                gs_ItemMoveType = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty(); // 20210829 오세완 차장 VALUE_1 -> VALUE_2로 변경
                decimal dBoxPerQty = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero();
                param.SetValue(PopupParameter.Value_2, gs_ItemMoveType);
                param.SetValue(PopupParameter.Value_3, dBoxPerQty); // 20210830 오세완 차장 공정박스당 상품수량 출력할 수 있게 전달
            }

            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_RePrint_Click(object sender, EventArgs e)
        {
            #region 기존 구현
            //var itemMoveNo = lup_ItemMoveNo.EditValue.GetNullToEmpty();

            //if (itemMoveNo.IsNullOrEmpty())
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
            //    return;
            //}

            //PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.KeyValue, "Re");
            //param.SetValue(PopupParameter.Value_1, itemMoveNo);
            //ReturnPopupArgument = new PopupArgument(param);

            //ActClose();
            #endregion 

            // 20210820 오세완 차장 PLC에서 요청을 분리해야 해서 처리
            if(lup_ItemMoveNo.EditValue.GetNullToEmpty() == "")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
            }
            else
            {
                if (gs_PLC_Pop)
                {
                    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    RePrint();
                }
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

            if(gs_PLC_Pop)
                param.SetValue(PopupParameter.Value_2, gs_ItemMoveType);

            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }
    }
}
