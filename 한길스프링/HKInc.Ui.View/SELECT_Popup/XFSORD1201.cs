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
        IService<VI_ORD1200_DETAIL_ADD> ModelService = (IService<VI_ORD1200_DETAIL_ADD>)ProductionFactory.GetDomainService("VI_ORD1200_DETAIL_ADD");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        private string lsItemCode = string.Empty;
        public XFSORD1201()
        {
            InitializeComponent();
            
        }
        public XFSORD1201(PopupDataParam parameter, PopupCallback callback) :this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);

            if (parameter.ContainsKey(PopupParameter.UserRight))
                UserRight = (Utils.Interface.Helper.IUserRight)parameter.GetValue(PopupParameter.UserRight);

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
            //gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            //gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowNum", true);
            gridEx1.MainGrid.AddColumn("_Check", "선택");
            gridEx1.MainGrid.AddColumn("RowNum", "Row번호", false);
            gridEx1.MainGrid.AddColumn("ItemCode", "품목코드", false);
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            gridEx1.MainGrid.AddColumn("LotNo", "생산 LOT NO");
            gridEx1.MainGrid.AddColumn("PackLotNo", "포장 LOT NO");
            gridEx1.MainGrid.AddColumn("Inqty", "입고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
        }
        protected override void InitCombo()
        {
            lupItem.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y"&&p.TopCategory!="P03").OrderBy(o=>o.ItemNm1).ToList());
            lupItem.EditValue = lsItemCode;

            textItemCodeName.Enabled = false;
            textItemCodeName.EditValue = lsItemCode;
        }
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            //string item = lupItem.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            string lot = tx_LotNo.Text.GetNullToEmpty();
            string PackLotNo = tx_PackLotNo.EditValue.GetNullToEmpty();
            bindingSource.DataSource = ModelService.GetList(p => (p.TN_STD1100.ItemNm.Contains(Item) || p.TN_STD1100.ItemNm1.Contains(Item)) && //(string.IsNullOrEmpty(item) ? true : p.ItemCode == item) &&
                                                                 (string.IsNullOrEmpty(lot) ? true : p.LotNo == lot) &&
                                                                 (string.IsNullOrEmpty(PackLotNo) ? true : p.PackLotNo == PackLotNo) &&
                                                                 //p.Stockqty > 0 &&
                                                                 p.Outqty == 0
                                                            )
                                                            .OrderBy(o => o.LotNo)
                                                            .ThenBy(p => p.PackLotNo.Length)
                                                            .ThenBy(p => p.PackLotNo)
                                                            .ToList();
            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            var list = bindingSource.List as List<VI_ORD1200_DETAIL_ADD>;
            var checkList = list.Where(p => p._Check == "Y").ToList();
            List<VI_ORD1200_DETAIL_ADD> Pur1100List = new List<VI_ORD1200_DETAIL_ADD>();

            foreach (var v in checkList)
            {
                Pur1100List.Add(ModelService.Detached(v));
            }

            param.SetValue(PopupParameter.ReturnObject, Pur1100List);

            //List<VI_PRODQTYMSTLOT> Pur1100List = new List<VI_PRODQTYMSTLOT>();

            //string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            //if (IsmultiSelect)
            //{

            //    foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
            //    {
            //        string LotNo = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "LotNo").GetNullToEmpty();
            //        string PackLotNo = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "PackLotNo").GetNullToEmpty();

            //        VI_PRODQTYMSTLOT pur1100 = ModelService.GetList(p => p.LotNo == LotNo && p.PackLotNo == PackLotNo).FirstOrDefault();
            //        Pur1100List.Add(ModelService.Detached(pur1100));
            //    }
            //    param.SetValue(PopupParameter.ReturnObject, Pur1100List);
            //}
            //else
            //{
            //    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_PRODQTYMSTLOT)bindingSource.Current));
            //}

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                VI_ORD1200_DETAIL_ADD Pur1100 = (VI_ORD1200_DETAIL_ADD)bindingSource.Current;

                List<VI_ORD1200_DETAIL_ADD> Pur1100List = new List<VI_ORD1200_DETAIL_ADD>();
                Pur1100List.Add(ModelService.Detached(Pur1100));

                param.SetValue(PopupParameter.ReturnObject, Pur1100List);
                //VI_PRODQTYMSTLOT Pur1100 = (VI_PRODQTYMSTLOT)bindingSource.Current;

                //if (IsmultiSelect)
                //{
                //    List<VI_PRODQTYMSTLOT> Pur1100List = new List<VI_PRODQTYMSTLOT>();
                //    if (Pur1100 != null)
                //        Pur1100List.Add(ModelService.Detached(Pur1100));

                //    param.SetValue(PopupParameter.ReturnObject, Pur1100List);
                //}
                //else
                //{
                //    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(Pur1100));
                //}

                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

