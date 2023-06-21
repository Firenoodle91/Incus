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

using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;

namespace HKInc.Ui.View.SELECT_Popup
{
    public partial class XSFSTD1100 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string Constraint = string.Empty;
        
        public XSFSTD1100()
        {
            InitializeComponent();
        
        }
        protected override void InitCombo()
        {
            lupItemType.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1));
            lupMid.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2));
            lupItemType.EditValue = "P01";
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p=>p.UseFlag=="Y").ToList());
        }
        public XSFSTD1100(PopupDataParam parameter, PopupCallback callback) :this()
        {
         
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            if (parameter.ContainsKey(PopupParameter.Constraint))
                Constraint = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
            if (parameter.ContainsKey(PopupParameter.KeyValue))
                lupcust.EditValue = parameter.GetValue(PopupParameter.KeyValue).GetNullToEmpty();
            if (parameter.ContainsKey(PopupParameter.Value_1))
                lupMid.EditValue = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            this.Text = "품목정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;

            if (Constraint == MasterCodeSTR.ItemCode_Production 
                || Constraint == MasterCodeSTR.ItemCode_NotProduction 
                || Constraint == MasterCodeSTR.ItemCode_BAN_Production)
            {
                layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            gridEx1.MainGrid.AddColumn("ItemCode","품목코드", false);
            gridEx1.MainGrid.AddColumn("ItemNm1","품번");
            gridEx1.MainGrid.AddColumn("ItemNm", "품명");
            gridEx1.MainGrid.AddColumn("TopCategory","대분류");
            gridEx1.MainGrid.AddColumn("MiddleCategory","중분류");
            gridEx1.MainGrid.AddColumn("BottomCategory","소분류");
            gridEx1.MainGrid.AddColumn("Unit","단위");
            gridEx1.MainGrid.AddColumn("ProcCnt", "단가", HorzAlignment.Far, FormatType.Numeric, "#,###,###,##0.##");
            gridEx1.MainGrid.AddColumn("MainCust", "주거래처");
            //gridEx1.MainGrid.AddColumn("Spec1","규격1");
            //gridEx1.MainGrid.AddColumn("Spec2","규격2");
            //gridEx1.MainGrid.AddColumn("Spec3","규격3");
            //gridEx1.MainGrid.AddColumn("Spec4","규격4");
            gridEx1.MainGrid.AddColumn("Spec1", "원재료");
            gridEx1.MainGrid.AddColumn("Spec2", "색상");
            gridEx1.MainGrid.AddColumn("Spec3", "재료규격");
            gridEx1.MainGrid.AddColumn("Temp2", "실리콘농도");
            gridEx1.MainGrid.AddColumn("Spec4", "제품규격");

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Spec1", DbRequesHandler.GetCommCode(MasterCodeSTR.ITEM_MATERIAL), "Mcode", "Codename");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string itemName = tx_itemcode.EditValue.GetNullToEmpty();
            string ItemType = lupItemType.EditValue.GetNullToEmpty();
            string maincust = lupcust.EditValue.GetNullToEmpty();
            string itemtypemid = lupMid.EditValue.GetNullToEmpty();

            if (Constraint == MasterCodeSTR.ItemCode_NotProduction)
            {
                bindingSource.DataSource = ModelService.GetList(p => (p.UseYn == "Y" || string.IsNullOrEmpty(p.UseYn))
                                                                     && (p.ItemNm.Contains(itemName) || p.ItemNm1.Contains(itemName))
                                                                     && (p.TopCategory == "P03" || p.TopCategory == "P02")
                                                                )
                                                        .OrderBy(p => p.ItemNm1)
                                                        .ToList();
            }
            else if (Constraint == MasterCodeSTR.ItemCode_Production)
            {
                bindingSource.DataSource = ModelService.GetList(p => (p.UseYn == "Y" || string.IsNullOrEmpty(p.UseYn))
                                                                     && (p.ItemNm.Contains(itemName) || p.ItemNm1.Contains(itemName))
                                                                     && p.TopCategory != "P03"
                                                                )
                                                        .OrderBy(p => p.ItemNm1)
                                                        .ToList();
            }
            else if (Constraint == MasterCodeSTR.ItemCode_BAN_Production)
            {
                bindingSource.DataSource = ModelService.GetList(p => (p.UseYn == "Y" || string.IsNullOrEmpty(p.UseYn))
                                                                     && (p.ItemNm.Contains(itemName) || p.ItemNm1.Contains(itemName))
                                                                     && p.TopCategory == "P05"
                                                                )
                                                        .OrderBy(p => p.ItemNm1)
                                                        .ToList();
            }
            else
            {
                bindingSource.DataSource = ModelService.GetList(p => (p.UseYn == "Y" || string.IsNullOrEmpty(p.UseYn))
                                                                    && (string.IsNullOrEmpty(ItemType) ? true : p.TopCategory == ItemType)
                                                                    && (string.IsNullOrEmpty(itemtypemid) ? true : p.MiddleCategory == itemtypemid)
                                                                    && (p.ItemNm.Contains(itemName) || p.ItemNm1.Contains(itemName))
                                                                    && (string.IsNullOrEmpty(maincust) ? true : p.MainCust == maincust))
                                                        .OrderBy(p => p.ItemNm1)
                                                        .ToList();
            }
            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
            SetRefreshMessage(gridEx1.MainGrid.RecordCount);
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();
      
            if (IsmultiSelect)
            {
                List<TN_STD1100> itemMasterList = new List<TN_STD1100>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    string itemCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ItemCode").GetNullToEmpty();
                    var itemMaster = ModelService.GetList(p => p.ItemCode == itemCode).FirstOrDefault();
                    if(itemMaster != null)
                        itemMasterList.Add(ModelService.Detached(itemMaster));
                }
                param.SetValue(PopupParameter.ReturnObject, itemMasterList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_STD1100)bindingSource.Current));
            }           
            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var itemMaster = (TN_STD1100)bindingSource.Current;
                if (IsmultiSelect)
                {
                    var itemMasterList = new List<TN_STD1100>();
                    if (itemMaster != null)
                        itemMasterList.Add(ModelService.Detached(itemMaster));
                    param.SetValue(PopupParameter.ReturnObject, itemMasterList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(itemMaster));
                }
                
                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

