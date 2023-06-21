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
    /// <summary>
    /// 수입검사관리 팝업
    /// </summary>
    public partial class XSFQCINSP : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_INSPLIST> ModelService = (IService<VI_INSPLIST>)ProductionFactory.GetDomainService("VI_INSPLIST");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;

        public XSFQCINSP()
        {
            InitializeComponent();
            dp_workdt.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_workdt.DateToEdit.DateTime = DateTime.Today.AddDays(20);
        }
        public XSFQCINSP(PopupDataParam parameter, PopupCallback callback) :this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            if (parameter.ContainsKey(PopupParameter.Constraint))
                DepartmentCode = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
        }

        protected override void InitToolbarButton()
        {
            //base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            this.Text = "수입검사대상";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("Itype");
            gridEx1.MainGrid.AddColumn("InputDate");
            gridEx1.MainGrid.AddColumn("InputNo", "입고번호");
            gridEx1.MainGrid.AddColumn("InputSeq", "입고순번");
            gridEx1.MainGrid.AddColumn("ItemCode");
            gridEx1.MainGrid.AddColumn("ItemNm", "품목");         // 2022-02-14 김진우 추가
            gridEx1.MainGrid.AddColumn("ItemNm1", "품번");        // 2022-02-14 김진우 추가
            gridEx1.MainGrid.AddColumn("InputQty");
            gridEx1.MainGrid.AddColumn("Inlot", "입고상세번호");
            gridEx1.MainGrid.AddColumn("LotNo");
            gridEx1.MainGrid.AddColumn("InputId", "입고자");
            gridEx1.MainGrid.AddColumn("Memo");
            
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("InputId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");     // 2022-02-14 김진우 제거
            gridEx1.MainGrid.SetRepositoryItemDateEdit("InputDate");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string itemName = tx_itemcode.EditValue.GetNullToEmpty();


            bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemName) ? true : p.ItemCode == itemName) && (p.InputDate >= dp_workdt.DateFrEdit.DateTime && p.InputDate <= dp_workdt.DateToEdit.DateTime)
                                               ).OrderBy(p => p.InputDate).ToList();
            
            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();
            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();
            param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_INSPLIST)bindingSource.Current));
           
            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            if (e.Clicks == 2)
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_INSPLIST)bindingSource.Current));
                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }
        }

    }
}

