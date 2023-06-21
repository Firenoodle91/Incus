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

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 최종검사 작업완료
    /// </summary>
    public partial class XPFINSP_FINAL_END_RUS : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");

        TEMP_XFPOP_INSP_FINAL TEMP_XFPOP_INSP_FINAL;

        public XPFINSP_FINAL_END_RUS()
        {
            InitializeComponent();
        }

        public XPFINSP_FINAL_END_RUS(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkEnd");

            TEMP_XFPOP_INSP_FINAL = (TEMP_XFPOP_INSP_FINAL)PopupParam.GetValue(PopupParameter.KeyValue);
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonCaption(ToolbarButton.Save, LabelConvert.GetLabelText("WorkEnd") + "[F5]", IconImageList.GetIconImage("actions/apply"));
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
        }

        protected override void DataSave()
        {
            //var customerLotNo = tx_CustomerLotNo.EditValue.GetNullToNull();

            PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.ReturnObject, customerLotNo);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }
    }
}
