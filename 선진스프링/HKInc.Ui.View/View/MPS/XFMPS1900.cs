﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using System.Data.SqlClient;
using HKInc.Utils.Enum;
using DevExpress.XtraReports.UI;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.View.REPORT;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 실적수량변경
    /// </summary>
    public partial class XFMPS1900 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MPS1202> ModelService = (IService<TN_MPS1202>)ProductionFactory.GetDomainService("TN_MPS1202");
        List<TN_STD1000> processList;

        public XFMPS1900()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }

        protected override void InitCombo()
        {
            processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            MasterGridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "N0", false);
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            MasterGridExControl.MainGrid.AddColumn("TN_MPS1201.TN_MPS1200.OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"), false);
            MasterGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_MPS1201.TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"));
            MasterGridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"));
            MasterGridExControl.MainGrid.AddColumn("BadType", LabelConvert.GetLabelText("BadType"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OkQty", "BadQty", "BadType");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", processList, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("OkQty");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("BadQty");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BadType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            MasterGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("RowId");

            MasterGridExControl.MainGrid.Clear();

            var workNo = tx_WorkNo.EditValue.GetNullToEmpty();
            var itemMoveNo = tx_ItemMoveNo.EditValue.GetNullToEmpty();

            if (workNo.IsNullOrEmpty() && itemMoveNo.IsNullOrEmpty())
            {
                //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WorkNo")));
                MessageBoxHandler.Show("작업지시번호 또는 이동표번호 필수입니다.");
                return;
            }

            if (!workNo.IsNullOrEmpty() && itemMoveNo.IsNullOrEmpty())
                MasterGridBindingSource.DataSource = ModelService.GetList(p => p.WorkNo == workNo).OrderBy(p => p.RowId).ToList();
            else if(workNo.IsNullOrEmpty() && !itemMoveNo.IsNullOrEmpty())
                MasterGridBindingSource.DataSource = ModelService.GetList(p => p.ItemMoveNo == itemMoveNo).OrderBy(p => p.RowId).ToList();
            else
                MasterGridBindingSource.DataSource = ModelService.GetList(p => p.WorkNo == workNo && p.ItemMoveNo == itemMoveNo).OrderBy(p => p.RowId).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();

            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var list = MasterGridBindingSource.List as List<TN_MPS1202>;
                if (list != null)
                {
                    var editList = list.Where(p => p.EditRowFlag == "Y").ToList();
                    var updateId = GlobalVariable.LoginId;
                    var updateTime = DateTime.Now;
                                       
                    foreach (var v in editList)
                    {
                        v.UpdateTime = updateTime;
                        v.UpdateId = updateId;
                        v.ResultQty = v.OkQty.GetDecimalNullToZero() + v.BadQty.GetDecimalNullToZero();

                        var list2 = list.Where(p => p.WorkNo == v.WorkNo
                                                    && p.ProcessCode == v.ProcessCode
                                                    && p.ProcessSeq == v.ProcessSeq
                                                    && p.ProductLotNo == v.ProductLotNo).ToList();
                        //마스터 UPD
                        var sumOkQty = list2.Sum(p => p.OkQty).GetDecimalNullToZero();
                        var sumBadQty = list2.Sum(p => p.BadQty).GetDecimalNullToZero();
                        var sumResultQty = sumOkQty + sumBadQty;
                        v.TN_MPS1201.ResultSumQty = sumResultQty;
                        v.TN_MPS1201.OkSumQty = sumOkQty;
                        v.TN_MPS1201.BadSumQty = sumBadQty;
                        v.TN_MPS1201.UpdateTime = updateTime;
                        v.TN_MPS1201.UpdateId = updateId;

                        if (!v.ItemMoveNo.IsNullOrEmpty())
                        {
                            var list3 = list.Where(p => p.WorkNo == v.WorkNo
                                                        && p.ProcessCode == v.ProcessCode
                                                        && p.ProcessSeq == v.ProcessSeq
                                                        && p.ProductLotNo == v.ProductLotNo
                                                        && p.ItemMoveNo == v.ItemMoveNo).ToList();
                            var sumOkQty2 = list3.Sum(p => p.OkQty).GetDecimalNullToZero();
                            var sumBadQty2 = list3.Sum(p => p.BadQty).GetDecimalNullToZero();
                            var sumResultQty2 = sumOkQty2 + sumBadQty2;

                            //이동표정보 UPD
                            var itemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == v.ItemMoveNo
                                                                                        && p.WorkNo == v.WorkNo
                                                                                        && p.ProcessCode == v.ProcessCode
                                                                                        && p.ProcessSeq == v.ProcessSeq
                                                                                        && p.ProductLotNo == v.ProductLotNo).FirstOrDefault();
                            itemMoveObj.UpdateTime = updateTime;
                            itemMoveObj.UpdateId = updateId;
                            itemMoveObj.OkQty = sumOkQty2;
                            itemMoveObj.BadQty = sumBadQty2;
                            itemMoveObj.ResultQty = sumResultQty2;
                            ModelService.UpdateChild(itemMoveObj);
                        }
                    }
                }
            }

            MasterGridBindingSource.EndEdit();
            ModelService.Save();
            DataLoad();
            
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var list = MasterGridBindingSource.List as List<TN_MPS1202>;
                if (list != null)
                {
                    var editWorkNoArray = list.Where(p => p.EditRowFlag == "Y").Select(p=>p.WorkNo).Distinct().ToArray();
                    foreach (var v in editWorkNoArray)
                    {
                        DbRequestHandler.USP_UPD_XFMPS1900(v);
                    }
                }
            }
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ProcessCode")
            {
                //var outProcFlag = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "OutProcFlag").GetNullToEmpty();
                //if (outProcFlag == "Y")
                //{
                //    e.DisplayText += "(" + LabelConvert.GetLabelText("Outsourcing") + ")";
                //}
                var processCode = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProcessCode").GetNullToEmpty();
                var processNameObj = processList.Where(p => p.CodeVal == processCode).FirstOrDefault();
                var outProcFlag = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "OutProcFlag").GetNullToEmpty();
                if (processNameObj != null && outProcFlag == "Y")
                {
                    e.DisplayText = processNameObj.CodeName + "(" + LabelConvert.GetLabelText("Outsourcing") + ")";
                }
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var obj = MasterGridBindingSource.Current as TN_MPS1202;
            obj.EditRowFlag = "Y";
            var updateId = GlobalVariable.LoginId;
            var updateTime = DateTime.Now;
            obj.UpdateTime = updateTime;
            obj.UpdateId = updateId;
        }

    }
}