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
    /// 현 창고위치재고변경 팝업
    /// </summary>
    public partial class XPFMPS1801 : Service.Base.PopupCallbackFormTemplate
    {
        private IService<TN_MPS1300> ModelService;
        private VI_MPS1800_LIST VI_MPS1800_Obj;

        public XPFMPS1801()
        {
            InitializeComponent();            
        }

        public XPFMPS1801(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WhPositionChange");

            VI_MPS1800_Obj = (VI_MPS1800_LIST)PopupParam.GetValue(PopupParameter.KeyValue);
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
            var WhCodeList = ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_WAN || p.Temp == null) && p.UseFlag == "Y").ToList();
            var PositionList = ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList();

            lup_WhCode.SetDefault(false, "WhCode", DataConvert.GetCultureDataFieldName("WhName"), WhCodeList);
            lup_Position.SetDefault(false, "PositionCode", "PositionName", PositionList);

            spin_StockQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_StockQty.Properties.Mask.EditMask = "n0";
            spin_StockQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_StockQty.Properties.Buttons[0].Visible = false;

            lup_WhCode.EditValue = VI_MPS1800_Obj.WhCode;
            lup_Position.EditValue = VI_MPS1800_Obj.PositionCode;
            spin_StockQty.EditValue = VI_MPS1800_Obj.StockQty;
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;

            var changeQty = spin_StockQty.EditValue.GetDecimalNullToZero();

            SetSaveMessageCheck = true;

            var TN_MPS1300_Obj = ModelService.GetList(p => p.WorkNo == VI_MPS1800_Obj.WorkNo && p.ProductLotNo == VI_MPS1800_Obj.ProductLotNo && p.WhCode == VI_MPS1800_Obj.WhCode && p.PositionCode == VI_MPS1800_Obj.PositionCode).FirstOrDefault();

            TN_MPS1300_Obj.InQty = changeQty;
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
}
