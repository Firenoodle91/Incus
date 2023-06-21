﻿using System;
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
    /// 출하검사(성적서) 대상 Select 팝업
    /// </summary>
    public partial class XSFQCT_SHIPMENT : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_INSP_SHIPMENT_OBJECT> ModelService = (IService<VI_INSP_SHIPMENT_OBJECT>)ProductionFactory.GetDomainService("VI_INSP_SHIPMENT_OBJECT");
        private bool IsmultiSelect = true;

        public XSFQCT_SHIPMENT()
        {
            InitializeComponent();
        }

        public XSFQCT_SHIPMENT(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("Inspection_SHIPMENT_Object");

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
            dt_WorkDate.DateFrEdit.EditValue = null;
            dt_WorkDate.DateToEdit.EditValue = null;

            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Sales || p.CustomerType == null)).ToList());

            if (PopupParam.ContainsKey(PopupParameter.KeyValue))
            {
                var TN_ORD1101 = PopupParam.GetValue(PopupParameter.KeyValue) as TN_ORD1101;
                if (TN_ORD1101 != null)
                {
                    lup_Item.EditValue = TN_ORD1101.ItemCode;
                    lup_Customer.EditValue = TN_ORD1101.CustomerCode;

                    lup_Item.ReadOnly = true;
                    lup_Customer.ReadOnly = true;
                }
            }
        }
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowIndex", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"), false);
            GridExControl.MainGrid.AddColumn("RowIndex", LabelConvert.GetLabelText("RowIndex"), HorzAlignment.Far, FormatType.Numeric, "#,#.##", false);
            GridExControl.MainGrid.AddColumn("RevNo", LabelConvert.GetLabelText("RevNo"));
            GridExControl.MainGrid.AddColumn("FinalInspNo", LabelConvert.GetLabelText("FinalInspNo"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("CustomerLotNo", LabelConvert.GetLabelText("CustomerLotNo"));
            GridExControl.MainGrid.AddColumn("Temp1", LabelConvert.GetLabelText("ItemMoveNo"));
            
            GridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CheckDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId"));
            GridExControl.MainGrid.AddColumn("CheckResult", LabelConvert.GetLabelText("CheckResult"));
            GridExControl.MainGrid.AddColumn("Memo");

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckResult", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
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
            var customerCode = lup_Customer.EditValue.GetNullToEmpty();

            if (PopupParam.ContainsKey(PopupParameter.Constraint))
            {
                //var constraintList = (List<TN_QCT1500>)PopupParam.GetValue(PopupParameter.Constraint);
                //var checkList = constraintList.Select(p => p.WorkNo + "_" + p.WorkSeq).ToList();

                if (!IsFirstLoaded)
                {
                    ModelBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                            && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                        && p.ProductLotNo.Contains(lotNo)
                                                                    )
                                                                    .OrderBy(p=>p.RowIndex)
                                                                    .Take(10)
                                                                    .ToList();
                }
                else
                {
                    ModelBindingSource.DataSource = ModelService.GetList(p => (p.CheckDate >= dt_WorkDate.DateFrEdit.DateTime && p.CheckDate <= dt_WorkDate.DateToEdit.DateTime)
                                                                        && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                        && p.ProductLotNo.Contains(lotNo)
                                                                    //&& p.TN_STD1100.StockInspFlag == "Y"
                                                                    )
                                                                    //.Where(p => constraintList == null ? true : !checkList.Contains(p.WorkNo + "_" + p.ProcessSeq))
                                                                    .OrderBy(o => o.CheckDate)
                                                                    .ThenBy(o => o.WorkNo)
                                                                    .ToList();
                }
            }
            else
            {
                if (!IsFirstLoaded)
                {
                    ModelBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                        && p.ProductLotNo.Contains(lotNo)
                                                                    )
                                                                    .OrderBy(p => p.RowIndex)
                                                                    .Take(10)
                                                                    .ToList();
                }
                else
                {
                    ModelBindingSource.DataSource = ModelService.GetList(p => (p.CheckDate >= dt_WorkDate.DateFrEdit.DateTime && p.CheckDate <= dt_WorkDate.DateToEdit.DateTime)
                                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                    && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                    && p.ProductLotNo.Contains(lotNo)
                                                                //&& p.TN_STD1100.StockInspFlag == "Y"
                                                                )
                                                                .OrderBy(o => o.CheckDate)
                                                                .ThenBy(o => o.WorkNo)
                                                                .ToList();
                }
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
            //    var returnList = new List<VI_INSP_SHIPMENT_OBJECT>();
            //    var dataList = ModelBindingSource.List as List<VI_INSP_SHIPMENT_OBJECT>;
            //    var checkList = dataList.Where(p => p._Check == "Y").ToList();
            //    foreach (var v in checkList)
            //    {
            //        returnList.Add(ModelService.Detached(v));
            //    }
            //    param.SetValue(PopupParameter.ReturnObject, returnList);
            //}
            //else
            //{
            //    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_INSP_SHIPMENT_OBJECT)ModelBindingSource.Current));
            //}
            //ReturnPopupArgument = new PopupArgument(param);
            //ActClose();

            var obj = (VI_INSP_SHIPMENT_OBJECT)ModelBindingSource.Current;
            if (IsmultiSelect)
            {
                var returnList = new List<VI_INSP_SHIPMENT_OBJECT>();
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
                var obj = (VI_INSP_SHIPMENT_OBJECT)ModelBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<VI_INSP_SHIPMENT_OBJECT>();
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