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
    public partial class XSFQCT : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_MPS1401LIST> ModelService = (IService<VI_MPS1401LIST>)ProductionFactory.GetDomainService("VI_MPS1401LIST");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        private string qctype = string.Empty;
        public XSFQCT()
        {
            InitializeComponent();
            dp_workdt.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_workdt.DateToEdit.DateTime = DateTime.Today.AddDays(20);

        }
        public XSFQCT(PopupDataParam parameter, PopupCallback callback) :this()
        {
            dp_workdt.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_workdt.DateToEdit.DateTime = DateTime.Today.AddDays(20);
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Value_1))
                qctype= parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            if (parameter.ContainsKey(PopupParameter.Constraint))
                DepartmentCode = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
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
            this.Text = "실적정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            //gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            //gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("WorkNo", "작업지시번호");
            gridEx1.MainGrid.AddColumn("WorkDate", "작업지시일");
            gridEx1.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("ItemCode", "품목코드", false);
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            gridEx1.MainGrid.AddColumn("ProcessCode", "공정");
            gridEx1.MainGrid.AddColumn("ProcessTurn", "공정순서", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("LotNo", "생산 LOT NO");
            gridEx1.MainGrid.AddColumn("ResultDate", "생산일");
            gridEx1.MainGrid.AddColumn("ResultQty", "생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("OkQty", "양품수량");
            gridEx1.MainGrid.AddColumn("WorkId", "작업자");

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
        
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("ResultDate");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string item = tx_itemcode.EditValue.GetNullToEmpty();


            bindingSource.DataSource = ModelService.GetList(p =>  (string.IsNullOrEmpty(item) ? true : (p.TN_STD1100.ItemNm.Contains(item) || p.TN_STD1100.ItemNm1.Contains(item)))
                                                              && (p.ResultDate >= dp_workdt.DateFrEdit.DateTime 
                                                                  && p.ResultDate <= dp_workdt.DateToEdit.DateTime)
                                                              && (string.IsNullOrEmpty(qctype) ? true : p.ProcessCode == "P06")
                                                           )
                                                           .OrderBy(p => p.ResultDate)
                                                           .ToList();            
            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
            SetRefreshMessage(gridEx1.MainGrid.RecordCount);
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();            
            param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_MPS1401LIST)bindingSource.Current));           
            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_MPS1401LIST)bindingSource.Current));
                ReturnPopupArgument = new PopupArgument(param);
                base.ActClose();
            }

        }
    }
}

