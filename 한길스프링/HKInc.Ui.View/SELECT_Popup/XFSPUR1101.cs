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
    public partial class XFSPUR1101 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_PUR1200> ModelService = (IService<TN_PUR1200>)ProductionFactory.GetDomainService("TN_PUR1200");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string ReqNo = string.Empty;
        public XFSPUR1101()
        {
            InitializeComponent();
        }

        public XFSPUR1101(PopupDataParam parameter, PopupCallback callback) :this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            
            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            if (parameter.ContainsKey(PopupParameter.Constraint))
                ReqNo = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonCaption(ToolbarButton.Confirm, "추가[F4]");
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.CheckBoxMultiSelect(true, "ReqSeq", true);
            gridEx1.MainGrid.AddColumn("ReqNo", false);
            gridEx1.MainGrid.AddColumn("_Check", "선택");
            gridEx1.MainGrid.AddColumn("ReqSeq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("ItemCode", "품번");
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            gridEx1.MainGrid.AddColumn("ReqQty", "발주수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("Temp1", "단가", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("Amt", "금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("Memo");
            gridEx1.MainGrid.SetEditable(true, "_Check");
            gridEx1.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            gridEx1.MainGrid.Clear();

            ModelService.ReLoad();

            bindingSource.DataSource = ModelService.GetList(p => p.ReqNo == ReqNo).OrderBy(o => o.ReqSeq).ToList();

            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            List<TN_PUR1200> TN_PUR1200List = new List<TN_PUR1200>();
            var CheckList = ((List<TN_PUR1200>)bindingSource.List).Where(p => p._Check == "Y").ToList();
            foreach (var v in CheckList)
            {
                TN_PUR1200List.Add(ModelService.Detached(v));
            }
            if(TN_PUR1200List.Count > 0)
                param.SetValue(PopupParameter.ReturnObject, TN_PUR1200List);

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

    }
}

