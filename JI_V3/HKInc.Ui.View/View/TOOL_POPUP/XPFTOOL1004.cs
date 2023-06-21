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
using HKInc.Service.Handler;

namespace HKInc.Ui.View.View.TOOL_POPUP
{
    /// <summary>
    /// 표준타입 추가 시 팝업 창
    /// </summary>
    public partial class XPFTOOL1004 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_TOOL1005> ModelService = (IService<TN_TOOL1005>)ProductionFactory.GetDomainService("TN_TOOL1005");
        BindingSource bindingSource = new BindingSource();

        public XPFTOOL1004()
        {
            InitializeComponent();
        }

        public XPFTOOL1004(PopupDataParam parameter, PopupCallback callback) :this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;

            lupTypeCode.EditValueChanged += LupTypeCode_EditValueChanged;
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);

            SetToolbarButtonCaption(ToolbarButton.Confirm, "추가[F4]");
        }

        protected override void InitControls()
        {
            base.InitControls();
        }

        protected override void InitCombo()
        {
            lupTypeCode.SetDefault(false, "TypeCode", "TypeName", ModelService.GetChildList<TN_TOOL1004>(p => true).OrderBy(p => p.TypeCode).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lupTypeCode.Columns[0].Visible = false;
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            gridEx1.MainGrid.AddColumn("ToolCode", LabelConvert.GetLabelText("ToolName"));
            gridEx1.MainGrid.AddColumn("BaseCNT", LabelConvert.GetLabelText("ToolLifeQty"), HorzAlignment.Far, FormatType.Numeric, "n0");

        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolCode", ModelService.GetChildList<TN_TOOL1000>(p => true).OrderBy(o => o.ToolName).ToList(), "ToolCode", "ToolName");

            gridEx1.MainGrid.BestFitColumns();
        }
        protected override void InitDataLoad()
        {
            //DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string typeCode = lupTypeCode.EditValue.GetNullToEmpty();
            bindingSource.DataSource = ModelService.GetList(p => p.TypeCode == typeCode)
                                                           .OrderBy(o => o.ProcessCode)
                                                           .ThenBy(o => o.ToolCode)
                                                           .ToList();
            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string typeCode = lupTypeCode.EditValue.GetNullToEmpty();
            if (typeCode.IsNullOrEmpty())
            {
                //MessageBoxHandler.Show("선택된 타입이 없습니다.", "경고");
                string sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50);
                MessageBoxHandler.Show(sMessage, LabelConvert.GetLabelText("Warning"));
                return;
            }

            param.SetValue(PopupParameter.ReturnObject, typeCode);

            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void LupTypeCode_EditValueChanged(object sender, EventArgs e)
        {
             ActRefresh();
        }

    }
}

