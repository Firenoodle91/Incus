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

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 표준타입 추가 시 팝업 창
    /// </summary>
    public partial class XFSMPS1000 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1002> ModelService = (IService<TN_MPS1002>)ProductionFactory.GetDomainService("TN_MPS1002");
        BindingSource bindingSource = new BindingSource();

        public XFSMPS1000()
        {
            InitializeComponent();
        }

        public XFSMPS1000(PopupDataParam parameter, PopupCallback callback) :this()
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
            lupTypeCode.SetDefault(false, "TypeCode", "TypeName", ModelService.GetChildList<TN_MPS1001>(p => true).OrderBy(p => p.TypeCode).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lupTypeCode.Columns[0].Visible = false;
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            gridEx1.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"));
            gridEx1.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            gridEx1.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            gridEx1.MainGrid.AddColumn("ToolUseFlag", LabelConvert.GetLabelText("ToolUseFlag"));
            gridEx1.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"));

            // 20210520 오세완 차장 작업설정검사여부를 재가동TO여부로 변경
            //gridEx1.MainGrid.AddColumn("JobSettingFlag", LabelConvert.GetLabelText("JobSettingFlag"));
            //gridEx1.MainGrid.AddColumn("RestartToFlag", LabelConvert.GetLabelText("RestartToFlag")); // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략

            gridEx1.MainGrid.AddColumn("StdWorkDay", LabelConvert.GetLabelText("StdWorkDay"));
            gridEx1.MainGrid.AddColumn("RowId", false);
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("ProcessSeq");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("ToolUseFlag", "N");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");

            // 20210520 오세완 차장 작업설정검사여부를 재가동TO여부로 변경
            //gridEx1.MainGrid.SetRepositoryItemCheckEdit("RestartToFlag", "N"); // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략
            //gridEx1.MainGrid.SetRepositoryItemCheckEdit("JobSettingFlag", "N");

            // 20210520 오세완 차장 설비사용여부 및 툴사용여부 생략처리
            //gridEx1.MainGrid.SetRepositoryItemCheckEdit("MachineFlag", "N");
            
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("StdWorkDay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.StdWorkDay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

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
                                                           .OrderBy(o => o.ProcessSeq)
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

