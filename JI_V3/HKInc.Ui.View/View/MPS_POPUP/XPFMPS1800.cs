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
using HKInc.Ui.Model.Domain;
using DevExpress.XtraEditors.Mask;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.MPS_POPUP
{
    /// <summary>
    /// 창고위치변경 팝업
    /// </summary>
    public partial class XPFMPS1800 : Service.Base.PopupCallbackFormTemplate
    {
        private IService<TN_MPS1300> ModelService;
        private VI_MPS1800_LIST VI_MPS1800_Obj;

        public XPFMPS1800()
        {
            InitializeComponent();            
        }

        public XPFMPS1800(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WhPositionChange");

            VI_MPS1800_Obj = (VI_MPS1800_LIST)PopupParam.GetValue(PopupParameter.KeyValue);

            lup_ChagneWhCode.EditValueChanged += Lup_ChagneWhCode_EditValueChanged;
            lup_ChangePosition.Popup += PositionCodeEdit_Popup;
            spin_BoxInQty.EditValueChanging += Spin_BoxInQty_EditValueChanging;
            spin_BoxQty.EditValueChanging += Spin_BoxQty_EditValueChanging;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            ModelService = (IService<TN_MPS1300>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            lcWhName1.Text = lcWhName.Text;
            lcPositionName1.Text = lcPositionName.Text;

            var WhCodeList = ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_WAN || p.Temp == null) && p.UseFlag == "Y").ToList();
            var PositionList = ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList();

            lup_WhCode.SetDefault(false, "WhCode", DataConvert.GetCultureDataFieldName("WhName"), WhCodeList);
            lup_Position.SetDefault(false, "PositionCode", "PositionName", PositionList);

            lup_ChagneWhCode.SetDefault(false, "WhCode", DataConvert.GetCultureDataFieldName("WhName"), WhCodeList.ToList());
            lup_ChangePosition.SetDefault(false, "PositionCode", "PositionName", PositionList);
            
            spin_StockQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_StockQty.Properties.Mask.EditMask = "n0";
            spin_StockQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_StockQty.Properties.Buttons[0].Visible = false;

            spin_BoxInQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BoxInQty.Properties.Mask.EditMask = "n0";
            spin_BoxInQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BoxInQty.Properties.Buttons[0].Visible = false;

            spin_BoxQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BoxQty.Properties.Mask.EditMask = "n0";
            spin_BoxQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BoxQty.Properties.Buttons[0].Visible = false;

            spin_ChangeQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ChangeQty.Properties.Mask.EditMask = "n0";
            spin_ChangeQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ChangeQty.Properties.Buttons[0].Visible = false;

            lup_WhCode.EditValue = VI_MPS1800_Obj.WhCode;
            lup_Position.EditValue = VI_MPS1800_Obj.PositionCode;
            spin_StockQty.EditValue = VI_MPS1800_Obj.StockQty;
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            var changeWhCode = lup_ChagneWhCode.EditValue.GetNullToEmpty();
            var changePosition = lup_ChangePosition.EditValue.GetNullToEmpty();
            var changeQty = spin_ChangeQty.EditValue.GetDecimalNullToZero();

            if (changeWhCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("ChangeWhName")));
            }
            else if (changePosition.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("ChangePositionName")));
            }
            else if (VI_MPS1800_Obj.WhCode == changeWhCode && VI_MPS1800_Obj.PositionCode == changePosition)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_88));
                //MessageBoxHandler.Show("현 창고위치와 동일합니다. 확인 부탁드립니다.");
            }
            else if (changeQty == 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("ChangeQty2")));
            }
            else if (VI_MPS1800_Obj.StockQty < changeQty)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_69), LabelConvert.GetLabelText("ChangeQty2"), LabelConvert.GetLabelText("StockQty")));
            }
            else
            {
                SetSaveMessageCheck = true;

                var TN_MPS1300_Obj = ModelService.GetList(p => p.WorkNo == VI_MPS1800_Obj.WorkNo && p.ProductLotNo == VI_MPS1800_Obj.ProductLotNo && p.WhCode == VI_MPS1800_Obj.WhCode && p.PositionCode == VI_MPS1800_Obj.PositionCode).FirstOrDefault();

                TN_MPS1300_Obj.InQty -= changeQty;

                var updateObj = TN_MPS1300_Obj.TN_MPS1201.TN_MPS1300List.Where(p => p.WhCode == changeWhCode && p.PositionCode == changePosition).FirstOrDefault();
                if (updateObj != null)
                {
                    updateObj.InQty += changeQty;
                }
                else
                {
                    var newObj = new TN_MPS1300();
                    newObj.WhCode = changeWhCode;
                    newObj.PositionCode = changePosition;
                    newObj.ItemCode = TN_MPS1300_Obj.ItemCode;
                    newObj.CustomerCode = TN_MPS1300_Obj.CustomerCode;
                    newObj.InQty = changeQty;
                    TN_MPS1300_Obj.TN_MPS1201.TN_MPS1300List.Add(newObj);
                }

                TN_MPS1300_Obj.UpdateTime = DateTime.Now;
                ModelService.UpdateChild(TN_MPS1300_Obj);
                ModelService.Save();
                if (TN_MPS1300_Obj.InQty == 0)
                {
                    ModelService.Delete(TN_MPS1300_Obj);
                    ModelService.Save();
                }
                ActClose();
            }
        }

        private void Spin_BoxInQty_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var changeBoxInQty = e.NewValue.GetDecimalNullToZero();
            var changeBoxQty = spin_BoxQty.EditValue.GetDecimalNullToZero();

            var currentStockQty = spin_StockQty.EditValue.GetDecimalNullToZero();
            var changeStockQty = changeBoxInQty * changeBoxQty;

            spin_ChangeQty.EditValue = changeStockQty;
            //if (currentStockQty < changeBoxQty)
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_69), LabelConvert.GetLabelText("ChangeQty"), LabelConvert.GetLabelText("StockQty")));
            //    spin_BoxInQty.EditValue = e.OldValue;
            //}
            //else
            //{
            //    spin_ChangeQty.EditValue = changeStockQty;
            //}
        }

        private void Spin_BoxQty_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var changeBoxQty = e.NewValue.GetDecimalNullToZero();
            var changeBoxInQty = spin_BoxInQty.EditValue.GetDecimalNullToZero();

            var currentStockQty = spin_StockQty.EditValue.GetDecimalNullToZero();
            var changeStockQty = changeBoxInQty * changeBoxQty;

            spin_ChangeQty.EditValue = changeStockQty;
        }

        private void Lup_ChagneWhCode_EditValueChanged(object sender, EventArgs e)
        {
            lup_ChangePosition.EditValue = null;
        }

        private void PositionCodeEdit_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var whCodeValue = lup_ChagneWhCode.EditValue.GetNullToEmpty();

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + whCodeValue + "'";
        }
    }
}
