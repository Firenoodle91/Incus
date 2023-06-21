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

namespace HKInc.Ui.View.View.MOLD_POPUP
{
    /// <summary>
    /// 다른품목의 검사항목 Select 팝업
    /// </summary>
    public partial class XPFMOLD1400_COPY : HKInc.Service.Base.PopupCallbackFormMasterDetailTemplate
    {
        IService<TN_MOLD1100> ModelService = (IService<TN_MOLD1100>)ProductionFactory.GetDomainService("TN_MOLD1100");
        IService<TN_MOLD1400> ModeldtlService = (IService<TN_MOLD1400>)ProductionFactory.GetDomainService("TN_MOLD1400");
        private bool IsmultiSelect = true;
        private string itemcode;
        
        public XPFMOLD1400_COPY()
        {
            InitializeComponent();
        }

        public XPFMOLD1400_COPY(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("MoldInspectionCopy");


            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Value_1))
                itemcode = (string)parameter.GetValue(PopupParameter.Value_1);

            lup_MoldCode.EditValue = itemcode;

            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            //DetailGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            //lup_RevNo.EditValueChanged += Lup_RevNo_EditValueChanged;
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
            lup_MoldCode.SetDefault(true, "MoldMCode", DataConvert.GetCultureDataFieldName("MoldName"), ModelService.GetChildList<TN_MOLD1100>(p => p.UseYN == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMCode"), LabelConvert.GetLabelText("MoldMCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldName"), LabelConvert.GetLabelText("MoldName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemCode"), LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMakerCust"), LabelConvert.GetLabelText("MoldMakerCust"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("TransferDate"), LabelConvert.GetLabelText("TransferDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MainMachineCode"), LabelConvert.GetLabelText("MainMachineCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Cavity"), LabelConvert.GetLabelText("Cavity"), HorzAlignment.Far, FormatType.Numeric, "n0");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "MoldMCode", IsmultiSelect);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMCode"), LabelConvert.GetLabelText("MoldMcode"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Seq"), LabelConvert.GetLabelText("Seq"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ReqType"), LabelConvert.GetLabelText("ReqType"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("FileName"), LabelConvert.GetLabelText("FileName"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("FileUrl"), LabelConvert.GetLabelText("FileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckPosition"), LabelConvert.GetLabelText("CheckPosition"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckList"), LabelConvert.GetLabelText("CheckList"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckWay"), LabelConvert.GetLabelText("CheckWay"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckCycle"), LabelConvert.GetLabelText("CheckCycle"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckStandardDate"), LabelConvert.GetLabelText("CheckStandardDate"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ManagementStandard"), LabelConvert.GetLabelText("ManagementStandard"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DisplayOrder"), LabelConvert.GetLabelText("DisplayOrder"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldMakerCust", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldMakercust), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ReqType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldReqType));
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_Inspection_IN_File, "FileName", "FileUrl");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckPosition), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckList), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("CheckStandardDate");
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, DetailGridExControl, MasterCodeSTR.FtpFolder_MoldImage, "FileName", "FileUrl", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", false);
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var MoldCode = lup_MoldCode.EditValue.GetNullToEmpty();


            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(MoldCode) ? true : p.MoldMCode == MoldCode)).OrderBy(p => p.MoldMCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
        }



        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MOLD1400List.OrderBy(p => p.DisplayOrder).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (DetailGridBindingSource == null || DetailGridBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<TN_MOLD1400>();
                var dataList = DetailGridBindingSource.List as List<TN_MOLD1400>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in checkList)
                {
                    returnList.Add(ModeldtlService.Detached(v));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModeldtlService.Detached((TN_MOLD1400)DetailGridBindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (DetailGridBindingSource == null || DetailGridBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var obj = (TN_MOLD1400)DetailGridBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_MOLD1400>();
                    if (obj != null)
                        returnList.Add(ModeldtlService.Detached(obj));
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModeldtlService.Detached(obj));
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}