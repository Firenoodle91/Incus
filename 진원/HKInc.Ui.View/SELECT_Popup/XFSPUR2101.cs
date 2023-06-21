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
    public partial class XFSPUR2101 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_PUR2100> ModelService = (IService<TN_PUR2100>)ProductionFactory.GetDomainService("TN_PUR2100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        private string ReqNo= string.Empty;
        public XFSPUR2101()
        {
            InitializeComponent();    
        }

        public XFSPUR2101(PopupDataParam parameter, PopupCallback callback) :this()
        {
            
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            if (parameter.ContainsKey(PopupParameter.Constraint))
                DepartmentCode = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (parameter.ContainsKey(PopupParameter.Value_1))
                ReqNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            tx_reqno.Text = ReqNo;
        }

        protected override void InitCombo()
        {
            tx_reqno.ReadOnly = true;
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
            this.Text = "발주상세정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.MainGrid.AddColumn("PoNo", false);
            gridEx1.MainGrid.AddColumn("PoSeq", "순번");
            gridEx1.MainGrid.AddColumn("ItemCode", "품목");
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx1.MainGrid.AddColumn("PoQty", "발주수량");
            gridEx1.MainGrid.AddColumn("PoCost", "단가");
            gridEx1.MainGrid.AddColumn("Amt", "금액");
            gridEx1.MainGrid.AddColumn("Memo", "메모");


            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");


        }
      
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            ReqNo= tx_reqno.EditValue.GetNullToEmpty();

            bindingSource.DataSource = ModelService.GetChildList<TN_PUR2101>(x => x.PoNo == ReqNo && x.TN_PUR2100.PoFlag == "Y").ToList();

            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            List<TN_PUR2101> rtnList = new List<TN_PUR2101>();

            var list = bindingSource.List as List<TN_PUR2101>;
            var checkList = list.Where(x => x._Check == "Y").ToList();

            rtnList = checkList;

            param.SetValue(PopupParameter.ReturnObject, rtnList);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }
        
        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                PopupDataParam param = new PopupDataParam();

                List<TN_PUR2101> rtnList = new List<TN_PUR2101>();

                var obj = bindingSource.Current as TN_PUR2101;
                if (obj == null) return;

                rtnList.Add(obj);

                param.SetValue(PopupParameter.ReturnObject, rtnList);
                ReturnPopupArgument = new PopupArgument(param);
                ActClose();
            }
        }

    }
}

