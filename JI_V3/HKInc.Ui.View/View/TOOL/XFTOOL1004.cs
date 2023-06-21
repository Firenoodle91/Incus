﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.TOOL
{
    /// <summary>
    /// 표준공구타입관리
    /// </summary>
    public partial class XFTOOL1004 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_TOOL1004> ModelService = (IService<TN_TOOL1004>)ProductionFactory.GetDomainService("TN_TOOL1004");

        public XFTOOL1004()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;
        }

        protected override void InitCombo()
        {
            //lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("TypeCode");
            MasterGridExControl.MainGrid.AddColumn("TypeName");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "TypeName");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_TOOL1004>(MasterGridExControl);

            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("ToolCode", LabelConvert.GetLabelText("ToolName"));
            DetailGridExControl.MainGrid.AddColumn("BaseCNT", LabelConvert.GetLabelText("ToolLifeQty"), HorzAlignment.Far, FormatType.Numeric, "n0");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ToolCode", "BaseCNT");
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1002>(DetailGridExControl);
        }

        protected override void InitRepository()
        {

            //ModelService.ReLoad();
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolCode", ModelService.GetChildList<TN_TOOL1000>(p => p.UseFlag == "Y").OrderBy(o => o.ToolName).ToList(), "ToolCode", "ToolName");

            //DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ProcessSeq");
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("ToolUseFlag", "N");


            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("TypeCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            MasterGridBindingSource.DataSource = ModelService.GetList(p => true).OrderBy(p => p.TypeCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_TOOL1004;
            if (masterObj == null) return;

            DetailGridBindingSource.DataSource = masterObj.TN_TOOL1005List.OrderBy(o => o.ToolCode).ThenBy(o => o.ToolCode).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            /*
            #region 공정순서 Check          
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_MPS1001>;
                var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();

                if (editList.Count > 0)
                {
                    if (editList.Any(p => p.TN_MPS1002List.GroupBy(c => c.ProcessCode).Where(c => c.Count() > 1).Count() > 0))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_87), LabelConvert.GetLabelText("Process")));
                        //MessageBoxHandler.Show("공정은 중복될 수 없습니다.", "경고");
                        return;
                    }

                    if (editList.Any(p => p.TN_MPS1002List.GroupBy(c => c.ProcessSeq).Where(c => c.Count() > 1).Count() > 0))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_87), LabelConvert.GetLabelText("ProcessSeq")));
                        //MessageBoxHandler.Show("공정순서는 중복될 수 없습니다.", "경고");
                        return;
                    }
                }
            }
            #endregion
            */

            ModelService.Save();
            ActRefresh();
        }

        protected override void AddRowClicked()
        {
            TN_TOOL1004 newobj = new TN_TOOL1004()
            {
                TypeCode = DbRequestHandler.GetSeqStandard("TOOLTYPE"),
                EditRowFlag = "Y",
            };

            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_TOOL1004;
            if (masterObj == null) return;

            if (masterObj.TN_TOOL1005List.Count > 0)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_10));
                return;
            }

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(masterObj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_TOOL1004;
            if (masterObj == null) return;

            TN_TOOL1005 newobj = new TN_TOOL1005()
            {
                TypeCode = masterObj.TypeCode,
            };
            masterObj.EditRowFlag = "Y";

            DetailGridBindingSource.Add(newobj);
            masterObj.TN_TOOL1005List.Add(newobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_TOOL1004;            
            var detailObj = DetailGridBindingSource.Current as TN_TOOL1005;
            if (masterObj == null || detailObj == null) return;

            masterObj.EditRowFlag = "Y";
            DetailGridBindingSource.Remove(detailObj);
            masterObj.TN_TOOL1005List.Remove(detailObj);
        }

        private void DetailView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_TOOL1005;

            if (detailObj == null) return;

            if (detailObj.NewRowFlag != "Y") return;

            if (e.Column.FieldName == "ToolCode")
            {
                string toolcode = detailObj.ToolCode;
                int baseCNT = Convert.ToInt16(DbRequestHandler.GetCellValue("select BASE_CNT from TN_TOOL1000T where TOOL_CODE = '" + toolcode + "'", 0));

                detailObj.BaseCNT = baseCNT;
            }

        }

    }
}
