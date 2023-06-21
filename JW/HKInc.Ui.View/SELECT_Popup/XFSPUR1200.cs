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
    public partial class XFSPUR1200 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_PUR1200> ModelService = (IService<TN_PUR1200>)ProductionFactory.GetDomainService("TN_PUR1200");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        private string ReqNo= string.Empty;
        public XFSPUR1200()
        {
            InitializeComponent();
            
        }
        public XFSPUR1200(PopupDataParam parameter, PopupCallback callback) :this()
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
            gridEx1.MainGrid.AddColumn("ReqNo", false);
            gridEx1.MainGrid.AddColumn("ReqSeq", "순번");
            gridEx1.MainGrid.AddColumn("ItemCode", "품목");
            gridEx1.MainGrid.AddColumn("ReqQty", "발주수량");
            gridEx1.MainGrid.AddColumn("Temp1", "단가");
            gridEx1.MainGrid.AddColumn("Amt", "금액");
            gridEx1.MainGrid.AddColumn("Memo");


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


            bindingSource.DataSource = ModelService.GetList(p => p.ReqNo == ReqNo).ToList();

            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<TN_PUR1200> Pur1200List = new List<TN_PUR1200>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    string Reqno = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ReqNo").GetNullToEmpty();
                    int Reqseq = Convert.ToInt32(gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ReqSeq").GetNullToEmpty());
                    TN_PUR1200 pur1200 = ModelService.GetList(p => p.ReqNo == Reqno&&p.ReqSeq==Reqseq).FirstOrDefault();
                    Pur1200List.Add(ModelService.Detached(pur1200));
                }
                param.SetValue(PopupParameter.ReturnObject, Pur1200List);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_PUR1200)bindingSource.Current));
            }

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                TN_PUR1200 Pur1200 = (TN_PUR1200)bindingSource.Current;

                if (IsmultiSelect)
                {
                    List<TN_PUR1200> Pur1200List = new List<TN_PUR1200>();
                    if (Pur1200 != null)
                        Pur1200List.Add(ModelService.Detached(Pur1200));

                    param.SetValue(PopupParameter.ReturnObject, Pur1200List);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(Pur1200));
                }

                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

