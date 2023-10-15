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
    /// 수입검사 대상 Select 팝업
    /// </summary>
    public partial class XSFQCT_IN : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_INSP_IN_OBJECT> ModelService = (IService<VI_INSP_IN_OBJECT>)ProductionFactory.GetDomainService("VI_INSP_IN_OBJECT");
        private bool IsmultiSelect = true;

        public XSFQCT_IN()
        {
            InitializeComponent();
        }

        public XSFQCT_IN(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("Inspection_IN_Object");

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
            dt_InDate.SetTodayIsMonth();

            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());            
            lup_StockInspFlag.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.StockInspFlag));
        }
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowIndex", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"), false);
            GridExControl.MainGrid.AddColumn("RowIndex", LabelConvert.GetLabelText("RowIndex"), HorzAlignment.Far, FormatType.Numeric, "#,#.##", false);
            GridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));
            GridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("InDate", LabelConvert.GetLabelText("InDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("InId", LabelConvert.GetLabelText("InId"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("InCustomerLotNo", LabelConvert.GetLabelText("InCustomerLotNo"));            
            GridExControl.MainGrid.AddColumn("Memo");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
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
            string lotNo = textEdit1.EditValue.GetNullToEmpty();
            string stockInspFlag = lup_StockInspFlag.EditValue.GetNullToEmpty();

            if (lotNo.Length > 18)
            {
                lotNo = lotNo.Substring(0, 18);
            }

            if (PopupParam.ContainsKey(PopupParameter.Constraint))
            {
                var constraintList = (List<TN_QCT1100>)PopupParam.GetValue(PopupParameter.Constraint);
                var checkList = constraintList.Select(p => p.WorkNo + "_" + p.WorkSeq).ToList();
                ModelBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= dt_InDate.DateFrEdit.DateTime && p.InDate <= dt_InDate.DateToEdit.DateTime)
                                                                       && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                       && p.InLotNo.Contains(lotNo)
                                                                       && p.StockInspFlag.Contains(stockInspFlag)
                                                                    //&& p.TN_STD1100.StockInspFlag == "Y" //필수만 보이는거 막음 PHS20210622
                                                                    )
                                                                    .Where(p => constraintList == null ? true : !checkList.Contains(p.InNo + "_" + p.InSeq))
                                                                    .OrderBy(o => o.InDate)
                                                                    .ThenBy(o => o.InNo)
                                                                    .ToList();
            }
            else
            {
                ModelBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= dt_InDate.DateFrEdit.DateTime && p.InDate <= dt_InDate.DateToEdit.DateTime)
                                                                       && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                       && p.InLotNo.Contains(lotNo)
                                                                       && p.StockInspFlag.Contains(stockInspFlag)
                                                                    //&& p.TN_STD1100.StockInspFlag == "Y"//필수만 보이는거 막음 PHS20210622
                                                                    )
                                                                    .OrderBy(o => o.InDate)
                                                                    .ThenBy(o => o.InNo)
                                                                    .ToList();
            }
            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            //if (IsmultiSelect)
            //{
            //    var returnList = new List<VI_INSP_IN_OBJECT>();
            //    var dataList = ModelBindingSource.List as List<VI_INSP_IN_OBJECT>;
            //    var checkList = dataList.Where(p => p._Check == "Y").ToList();
            //    foreach (var v in checkList)
            //    {
            //        returnList.Add(ModelService.Detached(v));
            //    }
            //    param.SetValue(PopupParameter.ReturnObject, returnList);
            //}
            //else
            //{
            //    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_INSP_IN_OBJECT)ModelBindingSource.Current));
            //}
            //ReturnPopupArgument = new PopupArgument(param);
            //ActClose();

            var obj = (VI_INSP_IN_OBJECT)ModelBindingSource.Current;
            if (IsmultiSelect)
            {
                var returnList = new List<VI_INSP_IN_OBJECT>();
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
                var obj = (VI_INSP_IN_OBJECT)ModelBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<VI_INSP_IN_OBJECT>();
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