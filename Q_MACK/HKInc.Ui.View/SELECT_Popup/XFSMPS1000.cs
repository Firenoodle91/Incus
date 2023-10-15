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

using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.SELECT_Popup
{
    /// <summary>
    /// 표준타입 추가 시 팝업 창
    /// </summary>
    public partial class XFSMPS1000 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1011> ModelService = (IService<TN_MPS1011>)ProductionFactory.GetDomainService("TN_MPS1011");
        //BindingSource bindingSource = new BindingSource();        // 미사용 주석
        #endregion

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

        protected override void InitCombo()
        {
            lupTypeCode.SetDefault(false, "TypeCode", "TypeName", ModelService.GetChildList<TN_MPS1010>(p => true).OrderBy(p => p.TypeCode).ToList(), 
                DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("Seq", false);
            gridEx1.MainGrid.AddColumn("ProcessSeq", "공정순번");
            gridEx1.MainGrid.AddColumn("ProcessCode", "공정명");
            gridEx1.MainGrid.AddColumn("MachineGroupCode", "설비그룹");
            gridEx1.MainGrid.AddColumn("OutProcFlag", "외주여부");
            gridEx1.MainGrid.AddColumn("StdWorkDay", "표준작업소요일");
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineGroup), "Mcode", "Codename", true);
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("ProcessSeq");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("StdWorkDay", DbRequestHandler.GetCommCode(MasterCodeSTR.STD), "Mcode", "Codename");

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string typeCode = lupTypeCode.EditValue.GetNullToEmpty();
            ModelBindingSource.DataSource = ModelService.GetList(p => p.TypeCode == typeCode).OrderBy(o => o.ProcessSeq).ToList();
            gridEx1.DataSource = ModelBindingSource;
            //bindingSource.DataSource = ModelService.GetList(p => p.TypeCode == typeCode).OrderBy(o => o.ProcessSeq).ToList();     // 미사용 주석           2022-07-08 김진우
            //gridEx1.DataSource = bindingSource;                   // 미사용 주석           2022-07-08 김진우
            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string typeCode = lupTypeCode.EditValue.GetNullToEmpty();
            if (typeCode.IsNullOrEmpty())
            {
                string sMessage = "선택된 타입이 없습니다.";
                MessageBoxHandler.Show(sMessage, "경고");
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

