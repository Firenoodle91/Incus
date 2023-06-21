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
    public partial class XFSPUR2000 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_ORD1100> ModelService = (IService<TN_ORD1100>)ProductionFactory.GetDomainService("TN_ORD1100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        public XFSPUR2000()
        {
            InitializeComponent();
        }
        public XFSPUR2000(PopupDataParam parameter, PopupCallback callback) :this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = "외주생산계획정보";
            
            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
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
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.MainGrid.AddColumn("OrderNo", "수주번호");
            gridEx1.MainGrid.AddColumn("Seq", "수주순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("DelivSeq", "납품계획번호");
            gridEx1.MainGrid.AddColumn("DelivDate", "납기예정일");
            gridEx1.MainGrid.AddColumn("DelivQty", "납품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("TurnKeyRemainQty", "턴키 잔량", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("Temp", "거래처");
            gridEx1.MainGrid.AddColumn("DelivId", "담당자");
            gridEx1.MainGrid.AddColumn("ItemCode", "품목코드", false);
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
        }
        protected override void InitCombo()
        {
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupItemCode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
        }
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string cust = lupcust.EditValue.GetNullToEmpty();
            //string ItemCode = lupItemCode.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(cust) ? true : p.Temp == cust) &&
                                                                 (p.TN_STD1100.ItemNm.Contains(Item) || p.TN_STD1100.ItemNm1.Contains(Item)) &&
                                                                  p.TurnKeyFlag == "Y"
                                                            )
                                                            .Where(p=>p.TurnKeyRemainQty > 0).OrderBy(o => o.DelivDate).ToList();
            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<TN_ORD1100> Pur1100List = new List<TN_ORD1100>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    string DelivSeq = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "DelivSeq").GetNullToEmpty();

                    TN_ORD1100 pur1100 = ModelService.GetList(p => p.DelivSeq == DelivSeq).FirstOrDefault();
                    Pur1100List.Add(ModelService.Detached(pur1100));
                }
                param.SetValue(PopupParameter.ReturnObject, Pur1100List);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_ORD1100)bindingSource.Current));
            }

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                TN_ORD1100 Pur1100 = (TN_ORD1100)bindingSource.Current;

                if (IsmultiSelect)
                {
                    List<TN_ORD1100> Pur1100List = new List<TN_ORD1100>();
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

