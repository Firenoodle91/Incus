using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 체크 항목 검사구분 변경 팝업
    /// </summary>
    public partial class XSFQCT_CHECK_DIVISION_COPY : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_QCT1001> ModelService = (IService<TN_QCT1001>)ProductionFactory.GetDomainService("TN_QCT1001");

        public XSFQCT_CHECK_DIVISION_COPY()
        {
            InitializeComponent();
        }

        public XSFQCT_CHECK_DIVISION_COPY(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("CheckDivisionCopy");
        }

        protected override void InitBindingSource() { }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            lup_InspectionDivision.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDivision));
        }
        
        protected override void Confirm()
        {
            var value = lup_InspectionDivision.EditValue.GetNullToEmpty();
            if (value.IsNullOrEmpty()) return;

            PopupDataParam param = new PopupDataParam();

            param.SetValue(PopupParameter.ReturnObject, value);

            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }
    }
}