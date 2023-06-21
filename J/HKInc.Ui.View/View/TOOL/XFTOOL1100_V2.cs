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
using HKInc.Ui.View.PopupFactory;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Service.Handler;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.TOOL
{
    /// <summary>
    /// 
    /// </summary>
    public partial class XFTOOL1100_V2 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_TOOL1100> ModelService = (IService<TN_TOOL1100>)ProductionFactory.GetDomainService("TN_TOOL1100");

        IService<TN_STD1100> ItemService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_TOOL1000> ToolService = (IService<TN_TOOL1000>)ProductionFactory.GetDomainService("TN_TOOL1000");

        public XFTOOL1100_V2()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += DeteilView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DeteilView_CellValueChanged;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ItemService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));

            IsDetailGridButtonFileChooseEnabled = UserRight.HasEdit;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("TypeAdd") + "[Alt+R]", IconImageList.GetIconImage("spreadsheet/grouprows"));

            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessCode"));
            DetailGridExControl.MainGrid.AddColumn("ToolCode", LabelConvert.GetLabelText("ToolName"));
            //DetailGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ToolName"));
            DetailGridExControl.MainGrid.AddColumn("ToolPosition", LabelConvert.GetLabelText("ToolPosition"), false);
            //DetailGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            //DetailGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            //DetailGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            //DetailGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            //DetailGridExControl.MainGrid.AddColumn("LifeQty", LabelConvert.GetLabelText("ToolLifeQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("BaseCNT", LabelConvert.GetLabelText("ToolLifeQty"));

            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ToolCode", "ToolPosition", "LifeQty");
            DetailGridExControl.MainGrid.SetEditable("ProcessCode", "ToolCode", "ToolPosition", "BaseCNT");

            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_TOOL1100>(MasterGridExControl);
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_TOOL1100>(DetailGridExControl);
        }

        protected override void InitRepository()
        {

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolCode", ToolService.GetChildList<TN_TOOL1000>(p => p.UseFlag == "Y" ).ToList(), "ToolCode", "ToolName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ToolPosition), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("BaseCNT", DefaultBoolean.Default, "n2");


        }

        protected override void DataLoad()
        {
            
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            var itemcode = lup_Item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ItemService.GetList(p => (string.IsNullOrEmpty(itemcode) ? true : p.ItemCode == itemcode)
                                                                       && (p.UseFlag == "Y")
                                                                       && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                                                    )
                                                                    .OrderBy(p => p.ItemCode)
                                                                    //.ThenBy(p => p.ProcessCode)
                                                                    .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
            {
                
                return;
            }

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode
                                                                        //&& p.ProcessCode == masterObj.ProcessCode
                                                                        //&& p.ToolCode != null
                                                                        )
                                                                    .OrderBy(p => p.ProcessCode)
                                                                    .ThenBy(p => p.ToolCode)
                                                                    .ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var newObj = new TN_TOOL1100()
            {
                ItemCode = masterObj.ItemCode,
                //ProcessCode = masterObj.ProcessCode
            };

            //masterObj.EditRowFlag = "Y";

            DetailGridBindingSource.Add(newObj);
            ModelService.Insert(newObj);            // 2022-10-14 김진우 추가
            //masterObj.TN_TOOL1100List.Add(newObj);
            
            DetailGridExControl.BestFitColumns();
        }

        private void DeteilView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //GridView gridview = sender as GridView;
            //string fieldName = e.Column.FieldName;

            var detailObj = DetailGridBindingSource.Current as TN_TOOL1100;

            if (detailObj == null) return;

            //if (detailObj.NewRowFlag != "Y") return;

            if (e.Column.FieldName == "ToolCode" && detailObj.NewRowFlag == "Y")
            {
                string toolcode = detailObj.ToolCode;
                int baseCNT = Convert.ToInt16(DbRequestHandler.GetCellValue("select BASE_CNT from TN_TOOL1000T where TOOL_CODE = '" + toolcode + "'", 0));

                detailObj.BaseCNT = baseCNT;
                detailObj.LifeCNT = baseCNT;
            }

            if(e.Column.FieldName == "BaseCNT")
            {
                detailObj.LifeCNT = detailObj.BaseCNT;
            }



        }

        private void DeteilView_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GridView gridview = sender as GridView;
            //string fieldName = e.Column.FieldName;

            /*
            try
            {
                var Obj = DetailGridBindingSource.Current as TN_TOOL1100;

                if (Obj.NewRowFlag != "Y" ) 
                    e.Cancel = true;
            }
            catch { }
            */

        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_TOOL1100;
            if (detailObj == null) return;

            /*
            masterObj.TN_TOOL1001List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
            */

            //masterObj.EditRowFlag = "Y";

            DetailGridBindingSource.Remove(detailObj);
            //masterObj.TN_TOOL1100List.Remove(detailObj);
            ModelService.Delete(detailObj);

        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            var List = DetailGridBindingSource.Current as TN_TOOL1100;

            List<TN_TOOL1100> DetailList= DetailGridBindingSource.List as List<TN_TOOL1100>;

            foreach (var v in DetailList)
            {
                //추가
                if (v.NewRowFlag == "Y")
                {
                    ModelService.Insert(v);
                }

                //수정
                else if (v.NewRowFlag == "N" && v.EditRowFlag == "Y")
                {
                    ModelService.Update(v);
                }

                //else if (v.DeleteRowFlag == "Y")
                //{
                //    ModelService.Delete(v);
                //}
            }
            

            ModelService.Save();
            DataLoad();
        }

        protected override void DetailFileChooseClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFTOOL1004, param, AddTypeCallBack);
            form.ShowPopup(true);
        }

        private void AddTypeCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            TN_STD1100 MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (MasterObj == null) return;
            if (e == null) return;

            var typeCode = e.Map.GetValue(PopupParameter.ReturnObject).GetNullToEmpty();
            var toolList = ModelService.GetChildList<TN_TOOL1005>(p => p.TypeCode == typeCode).OrderBy(p => p.ProcessCode).ThenBy(o => o.ToolCode).ToList();

            foreach (var v in toolList)
            {
                var newObj = new TN_TOOL1100()
                {
                    ItemCode = MasterObj.ItemCode
                    , ProcessCode = v.ProcessCode
                    , ToolCode = v.ToolCode
                    , BaseCNT = v.BaseCNT
                    , LifeCNT = v.BaseCNT
                    , NewRowFlag = "Y"
                };
                DetailGridBindingSource.Add(newObj);
            }

            if (toolList.Count > 0)
                SetIsFormControlChanged(true);

            DetailGridExControl.MainGrid.BestFitColumns();
        }

    }
}