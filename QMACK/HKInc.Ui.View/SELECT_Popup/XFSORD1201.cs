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
    public partial class XFSORD1201 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<VI_PRODQTYMSTLOT> ModelService = (IService<VI_PRODQTYMSTLOT>)ProductionFactory.GetDomainService("VI_PRODQTYMSTLOT");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        private string lsItemCode = string.Empty;
        #endregion

        public XFSORD1201()
        {
            InitializeComponent();
            
        }
        public XFSORD1201(PopupDataParam parameter, PopupCallback callback) :this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);
            
            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            if (parameter.ContainsKey(PopupParameter.Value_1))
                lsItemCode = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
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
            this.Text = "재고현황";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.MainGrid.AddColumn("ItemCode", "품목코드");
            gridEx1.MainGrid.AddColumn("ItemNm", "품목"); // 20220219 오세완 차장 품목 출력 추가
            gridEx1.MainGrid.AddColumn("ItemNm1", "품명"); // 20220219 오세완 차장 품번 출력 추가
            gridEx1.MainGrid.AddColumn("LotNo", "LOTNO");
            gridEx1.MainGrid.AddColumn("Inqty", "입고량");

            gridEx1.MainGrid.AddColumn("Outqty", "출고량");
            gridEx1.MainGrid.AddColumn("Stockqty", "재고량");
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y"&&p.TopCategory!="P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");

        }
        protected override void InitCombo()
        {
            // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략                     2022-02-23 김진우 ItemCodeColumnSetting 수정하여 원복            
            lupItem.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && 
                                                                                                       p.TopCategory != MasterCodeSTR.Topcategory_Material).OrderBy(o => o.ItemNm1).ToList());
            lupItem.EditValue = lsItemCode;
        }
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            string item = lupItem.EditValue.GetNullToEmpty();
            string lot = tx_LotNo.Text.GetNullToEmpty();

            bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(item) ? true : p.ItemCode == item) 
                                                              && (string.IsNullOrEmpty(lot) ? true : p.LotNo == lot) 
                                                              && p.Stockqty > 0)
                                                                 .OrderBy(o => o.LotNo).ToList();
            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<VI_PRODQTYMSTLOT> Pur1100List = new List<VI_PRODQTYMSTLOT>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    string LotNo = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "LotNo").GetNullToEmpty();

                    VI_PRODQTYMSTLOT pur1100 = ModelService.GetList(p => p.LotNo==LotNo).FirstOrDefault();
                    Pur1100List.Add(ModelService.Detached(pur1100));
                }
                param.SetValue(PopupParameter.ReturnObject, Pur1100List);
            }
            else 
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_PRODQTYMSTLOT)bindingSource.Current));
            }

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                VI_PRODQTYMSTLOT Pur1100 = (VI_PRODQTYMSTLOT)bindingSource.Current;

                if (IsmultiSelect)
                {
                    List<VI_PRODQTYMSTLOT> Pur1100List = new List<VI_PRODQTYMSTLOT>();
                    if (Pur1100 != null)
                        Pur1100List.Add(ModelService.Detached(Pur1100));

                    param.SetValue(PopupParameter.ReturnObject, Pur1100List);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(Pur1100));
                }

                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

