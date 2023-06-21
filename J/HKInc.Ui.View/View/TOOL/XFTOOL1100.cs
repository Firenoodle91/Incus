using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.TOOL
{
    /// <summary>
    /// 
    /// </summary>
    public partial class XFTOOL1100 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_TOOL1100> ModelService = (IService<TN_TOOL1100>)ProductionFactory.GetDomainService("TN_TOOL1100");
        IService<TN_STD1100> ItemService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_TOOL1000> ToolService = (IService<TN_TOOL1000>)ProductionFactory.GetDomainService("TN_TOOL1000");

        public XFTOOL1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            MasterGridExControl.MainGrid.MainView.ShowingEditor += MasterView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DeteilView_ShowingEditor;
        }

        protected override void InitCombo()
        {
            lup_Tool.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ItemService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());
        }

        protected override void InitGrid()
        {
            //MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            //MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessCode"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "ProcessCode");

            DetailGridExControl.MainGrid.AddColumn("ToolCode", LabelConvert.GetLabelText("ToolName"));
            //DetailGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ToolName"));
            DetailGridExControl.MainGrid.AddColumn("ToolPosition", LabelConvert.GetLabelText("ToolPosition"));
            DetailGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            DetailGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            DetailGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            DetailGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            DetailGridExControl.MainGrid.AddColumn("LifeQty", LabelConvert.GetLabelText("ToolLifeQty"), HorzAlignment.Far, FormatType.Numeric, "n0");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ToolCode", "ToolPosition", "LifeQty");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_TOOL1100>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_TOOL1100>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ItemService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && p.TopCategory == MasterCodeSTR.TopCategory_WAN).ToList(), "ItemCode", "ItemName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemName", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemcCode", "ItemName");
            MasterGridExControl.MainGrid.Columns["ItemCode"].MinWidth = 200;




            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolCode", ToolService.GetChildList<TN_TOOL1000>(p => p.UseFlag == "Y" ).ToList(), "ToolCode", "ToolName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ToolPosition), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("LifeQty", DefaultBoolean.Default, "n2");


        }

        protected override void DataLoad()
        {
            
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            var toolCode = lup_Tool.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(toolCode) ? true : p.ItemCode == toolCode)
                                                                    //&& (p.UseFlag == "Y")
                                                                    //&& (p.TopCategory == MasterCodeSTR.TopCategory_TOOL)
                                                               )
                                                               .OrderBy(p => p.ItemCode).ThenBy(p => p.ProcessCode)
                                                               .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            
        }

        protected override void AddRowClicked()
        {
            TN_TOOL1100 newObj = new TN_TOOL1100()
            {
                //PoNo = DbRequestHandler.GetSeqMonth("PO"),
                //PoDate = DateTime.Today,
                //PoId = GlobalVariable.LoginId,
                //DueDate = DateTime.Today.AddDays(20),
                //PoFlag = "N"
            };

            MasterGridBindingSource.Add(newObj);
            //ModelService.Insert(newObj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_TOOL1100;
            if (masterObj == null)
            {
                
                return;
            }

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode
                                                                        && p.ProcessCode == masterObj.ProcessCode
                                                                        && p.ToolCode != null)
                                                                    //.OrderBy(p => p.CreateTime)
                                                                    .ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_TOOL1100;
            if (masterObj == null) return;

            var newObj = new TN_TOOL1100()
            {
                ItemCode = masterObj.ItemCode,
                ProcessCode = masterObj.ProcessCode
            };

            //masterObj.TN_TOOL1001List.Add(newObj);
            DetailGridBindingSource.Add(newObj);
            DetailGridExControl.BestFitColumns();

            
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_TOOL1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_TOOL1100;
            if (detailObj == null) return;

            /*
            masterObj.TN_TOOL1001List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
            */
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();

            var List = DetailGridBindingSource.Current as TN_TOOL1100;

            var newObj = new TN_TOOL1100
            {
                ItemCode = List.ItemCode,
                ProcessCode = List.ProcessCode,
                ToolCode = List.ToolCode,
                BaseCNT = List.BaseCNT
            };

            ModelService.Insert(newObj);
            ModelService.Save();
            DataLoad();
        }

        private void MasterView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tttt = sender.GetType();
            try
            {
                //거래처 등록이 되어 있으면 수정 불가 처리

                var Obj = MasterGridBindingSource.Current as TN_TOOL1100;

                if (Obj.NewRowFlag != "Y")
                    e.Cancel = true;
            }
            catch { }

        }

        private void DeteilView_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GridView gridview = sender as GridView;
            //string fieldName = e.Column.FieldName;


            try
            {
                //거래처 등록이 되어 있으면 수정 불가 처리

                var Obj = DetailGridBindingSource.Current as TN_TOOL1100;

                if (Obj.NewRowFlag != "Y" ) 
                    e.Cancel = true;
            }
            catch { }

        }
    }
}