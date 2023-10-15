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
    /// 다른품목의 검사항목 Select 팝업
    /// </summary>
    public partial class XSFQCT_ITEM_COPY : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_QCT1001> ModelService = (IService<TN_QCT1001>)ProductionFactory.GetDomainService("TN_QCT1001");
        private bool IsmultiSelect = true;

        public XSFQCT_ITEM_COPY()
        {
            InitializeComponent();
        }

        public XSFQCT_ITEM_COPY(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("OtherItemCopy");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            lup_Item.EditValueChanged += Lup_ItemCode_EditValueChenged;         // 2021-06-28 김진우 주임 추가         기존에 리비전값 변경이벤트로 되어잇어서 아이템코드변경 이벤트로 변경
        }

        protected override void InitBindingSource() { }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
        }
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowId", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            GridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));
            GridExControl.MainGrid.AddColumn("CheckDivision", LabelConvert.GetLabelText("InspectionDivision"));
            GridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            GridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessCode"));
            GridExControl.MainGrid.AddColumn("InstrumentCode", LabelConvert.GetLabelText("InstrumentCode"));
            GridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"));       
            GridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"));
            GridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"));
            GridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), false);
            GridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), false);
            GridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), false);
            //GridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"));
            //GridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"));
            //GridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"));
            GridExControl.MainGrid.AddColumn("InspectionReportFlag", LabelConvert.GetLabelText("InspectionReportFlag"));
            GridExControl.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"));
            GridExControl.MainGrid.AddColumn("SpcFlag", LabelConvert.GetLabelText("SpcFlag"));
            GridExControl.MainGrid.AddColumn("SpcDivision", LabelConvert.GetLabelText("SpcDivision"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("InspectionReportFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("SpcFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SpcDivision", DbRequestHandler.GetCommTopCode(MasterCodeSTR.SpcDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDivision", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InstrumentCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad() { }

        /// <summary>
        /// 2021-06-28 김진우 주임 수정
        /// 아이템코드값을 변경해도 리비전번호가 변경이 안되고 조회조건이 이상하여 수정
        /// </summary>
        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            string RevNo = lup_RevNo.EditValue.GetNullToEmpty();

            ModelBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == itemCode && p.RevNo == RevNo).ToList();

            if (ModelBindingSource.Count == 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("RevList")));
            }

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        /// <summary>
        /// 2021-06-28 김진우 주임
        /// 기존에 리비전값이 변경되면 데이터를 로드하는데 아이템코드값이 변경되어도 같은 리비전 값을 가지고 잇으면 조회가 되지 않아서 수정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lup_ItemCode_EditValueChenged(object sender, EventArgs e)
        {
            string ItemCode = lup_Item.EditValue.GetNullToEmpty();
            string RevNo = lup_RevNo.EditValue.GetNullToEmpty();

            var list = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == ItemCode && p.UseFlag == "Y").OrderBy(p => p.RevNo).ToList();

            if (ItemCode.IsNullOrEmpty())
            {
                GridExControl.MainGrid.Clear();
                lup_RevNo.Clear();
            }
            else
            {
                lup_RevNo.SetDefault(false, "RevNo", "RevNo");
                lup_RevNo.DataSource = list;
                lup_RevNo.Columns[0].Visible = false;
                lup_RevNo.EditValue = list.Count == 0 ? null : list.Last().RevNo;
            }
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<TN_QCT1001>();
                var dataList = ModelBindingSource.List as List<TN_QCT1001>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in checkList)
                {
                    returnList.Add(ModelService.Detached(v));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_QCT1001)ModelBindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var obj = (TN_QCT1001)ModelBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_QCT1001>();
                    if (obj != null)
                        returnList.Add(ModelService.Detached(obj));
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(obj));
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}