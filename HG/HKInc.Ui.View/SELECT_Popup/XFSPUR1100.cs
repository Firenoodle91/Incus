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
using DevExpress.XtraEditors.Controls;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;

namespace HKInc.Ui.View.SELECT_Popup
{
    public partial class XFSPUR1100 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_PUR1100> ModelService = (IService<TN_PUR1100>)ProductionFactory.GetDomainService("TN_PUR1100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        public XFSPUR1100()
        {
            InitializeComponent();
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(20);
        }
        public XFSPUR1100(PopupDataParam parameter, PopupCallback callback) :this()
        {
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(20);
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
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            this.Text = "발주정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
            gridEx1.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column.FieldName == "ItemNm1")
            //{
            //    var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //    var PoNo = view.GetRowCellValue(e.ListSourceRowIndex, "ReqNo").ToString();
            //    var objList = bindingSource.List as List<TN_PUR1100>;
            //    if (objList != null)
            //    {
            //        var obj = objList.Where(p => p.ReqNo == PoNo).FirstOrDefault();
            //        if (obj != null)
            //        {
            //            var detailObj = obj.PUR1200List.OrderBy(p => p.ReqSeq).FirstOrDefault();
            //            if (detailObj != null)
            //            {
            //                e.DisplayText = detailObj.TN_STD1100.ItemNm1;
            //            }
            //        }
            //    }
            //}
            //else if (e.Column.FieldName == "ItemNm")
            //{
            //    var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //    var PoNo = view.GetRowCellValue(e.ListSourceRowIndex, "ReqNo").ToString();
            //    var objList = bindingSource.List as List<TN_PUR1100>;
            //    if (objList != null)
            //    {
            //        var obj = objList.Where(p => p.ReqNo == PoNo).FirstOrDefault();
            //        if (obj != null)
            //        {
            //            var detailObj = obj.PUR1200List.OrderBy(p => p.ReqSeq).FirstOrDefault();
            //            if (detailObj != null)
            //            {
            //                e.DisplayText = detailObj.TN_STD1100.ItemNm;
            //            }
            //        }
            //    }
            //}
        }

        protected override void InitGrid()
        {
            
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.MainGrid.AddColumn("ReqNo", "발주번호");
            gridEx1.MainGrid.AddColumn("ItemList", "발주품목");
            //gridEx1.MainGrid.AddColumn("ItemNm1", "품번");
            //gridEx1.MainGrid.AddColumn("ItemNm", "품명");
            gridEx1.MainGrid.AddColumn("ReqDate", "발주일");
            gridEx1.MainGrid.AddColumn("DueDate", "납기예정일");
            gridEx1.MainGrid.AddColumn("ReqId", "발주자");
            gridEx1.MainGrid.AddColumn("CustomerCode", "거래처");
            gridEx1.MainGrid.AddColumn("Memo");
            gridEx1.MainGrid.AddColumn("Temp2", "발주확정");
            gridEx1.MainGrid.SetEditable(true, "ItemList");

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("DueDate");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Temp2", "N");

            gridEx1.MainGrid.SetRepositoryItemButtonEdit("ItemList", ItemList_ButtonClick, "보기", gridEx1.MainGrid.Appearance.Font, DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor);
        }
        protected override void InitCombo()
        {
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());

        }
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            gridEx1.MainGrid.Clear();
            ModelService.ReLoad();
            string cust = lupcust.EditValue.GetNullToEmpty();
            int radio = radioGroup1.EditValue.GetIntNullToZero();

            if(radio == 1) //전체
            {
                bindingSource.DataSource = ModelService.GetList(p => (p.ReqDate >= dp_date.DateFrEdit.DateTime.Date && p.ReqDate <= dp_date.DateToEdit.DateTime.Date) 
                                                                    && (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust) 
                                                                    && (p.Temp2 == "Y")
                                                               )
                                                               .OrderBy(o => o.CustomerCode)
                                                               .ThenBy(o => o.ReqDate)
                                                               .ToList();
            }
            else if(radio == 2) //입고완료
            {
                bindingSource.DataSource = ModelService.GetList(p => (p.ReqDate >= dp_date.DateFrEdit.DateTime.Date && p.ReqDate <= dp_date.DateToEdit.DateTime.Date)
                                                                    && (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust)
                                                                    && (p.Temp2 == "Y")
                                                                    && (p.PUR1200List.Any(c=>c.TN_PUR1301List.Any(v=>v.TN_PUR1300.Temp1 == "Y")))
                                                               )
                                                               .OrderBy(o => o.CustomerCode)
                                                               .ThenBy(o => o.ReqDate)
                                                               .ToList();
            }
            else //입고미완료
            {
                bindingSource.DataSource = ModelService.GetList(p => (p.ReqDate >= dp_date.DateFrEdit.DateTime.Date && p.ReqDate <= dp_date.DateToEdit.DateTime.Date)
                                                                    && (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust)
                                                                    && (p.Temp2 == "Y")
                                                                    && (p.PUR1200List.Any(c => c.TN_PUR1301List.Any(v => v.TN_PUR1300.Temp1 != "Y")) 
                                                                        || (p.PUR1200List.Any(c=>c.TN_PUR1301List.Count == 0)))
                                                               )
                                                               .OrderBy(o => o.CustomerCode)
                                                               .ThenBy(o => o.ReqDate)
                                                               .ToList();
            }

            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<TN_PUR1100> Pur1100List = new List<TN_PUR1100>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    string Reqno = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ReqNo").GetNullToEmpty();

                    TN_PUR1100 pur1100 = ModelService.GetList(p => p.ReqNo == Reqno).FirstOrDefault();
                    Pur1100List.Add(ModelService.Detached(pur1100));
                }
                param.SetValue(PopupParameter.ReturnObject, Pur1100List);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_PUR1100)bindingSource.Current));
            }

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                TN_PUR1100 Pur1100 = (TN_PUR1100)bindingSource.Current;

                if (IsmultiSelect)
                {
                    List<TN_PUR1100> Pur1100List = new List<TN_PUR1100>();
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
        private void ItemList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            TN_PUR1100 obj = bindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, obj.ReqNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1101, param, ItemListCallback);
            form.ShowPopup(true);
        }

        private void ItemListCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            if (!e.Map.ContainsKey(PopupParameter.ReturnObject)) return;
            TN_PUR1100 obj = bindingSource.Current as TN_PUR1100;
            if (obj == null) return;

            List<TN_PUR1200> returnList = (List<TN_PUR1200>)e.Map.GetValue(PopupParameter.ReturnObject);

            PopupDataParam param = new PopupDataParam();

            List<TN_PUR1100> Pur1100List = new List<TN_PUR1100>();
            if (obj != null)
                Pur1100List.Add(ModelService.Detached(obj));

            param.SetValue(PopupParameter.ReturnObject, Pur1100List);

            param.SetValue(PopupParameter.Value_1, returnList);
            ReturnPopupArgument = new PopupArgument(param);
            base.ActClose();
        }

    }
}

