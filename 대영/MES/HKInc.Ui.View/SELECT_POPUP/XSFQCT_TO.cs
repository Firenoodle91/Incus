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

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 재가동TO검사 대상 Select 팝업
    /// </summary>
    public partial class XSFQCT_TO : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_INSP_FREQNTLY_OBJECT> ModelService = (IService<VI_INSP_FREQNTLY_OBJECT>)ProductionFactory.GetDomainService("VI_INSP_FREQNTLY_OBJECT");
        private bool IsmultiSelect = true;

        public XSFQCT_TO()
        {
            InitializeComponent();
        }

        public XSFQCT_TO(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("Inspection_SETTING_Object");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
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
            dt_WorkDate.SetTodayIsMonth();

            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowIndex", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"), false);
            GridExControl.MainGrid.AddColumn("RowIndex", LabelConvert.GetLabelText("RowIndex"), HorzAlignment.Far, FormatType.Numeric, "#,#.##", false);
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            //GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("Memo");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var lotNo = textEdit1.EditValue.GetNullToEmpty();


            ModelBindingSource.DataSource = ModelService.GetList(p => (p.WorkDate >= dt_WorkDate.DateFrEdit.DateTime && p.WorkDate <= dt_WorkDate.DateToEdit.DateTime)
                                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                    && (string.IsNullOrEmpty(lotNo) ? true : p.ProductLotNo.Contains(lotNo))
                                                                )
                                                                .OrderBy(o => o.WorkDate)
                                                                .ThenBy(o => o.WorkNo)
                                                                .ToList();

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            //if (IsmultiSelect)
            //{
            //    var returnList = new List<VI_INSP_SETTING_OBJECT>();
            //    var dataList = ModelBindingSource.List as List<VI_INSP_SETTING_OBJECT>;
            //    var checkList = dataList.Where(p => p._Check == "Y").ToList();
            //    foreach (var v in checkList)
            //    {
            //        returnList.Add(ModelService.Detached(v));
            //    }
            //    param.SetValue(PopupParameter.ReturnObject, returnList);
            //}
            //else
            //{
            //    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_INSP_SETTING_OBJECT)ModelBindingSource.Current));
            //}
            //ReturnPopupArgument = new PopupArgument(param);
            //ActClose();

            var obj = (VI_INSP_FREQNTLY_OBJECT)ModelBindingSource.Current;
            if (IsmultiSelect)
            {
                var returnList = new List<VI_INSP_FREQNTLY_OBJECT>();
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

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var obj = (VI_INSP_FREQNTLY_OBJECT)ModelBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<VI_INSP_FREQNTLY_OBJECT>();
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