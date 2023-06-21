using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Domain.TEMP;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 단가이력관리 NEW
    /// </summary>
    public partial class XFSTD1103_NEW : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFSTD1103_NEW()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetail_CellValueChanged;
        }


        private void SubDetail_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_STD1100;
            var subDetail = SubDetailGridBindingSource.Current as TN_STD1103;
            if (detailObj == null || subDetail == null) return;

            string column = e.Column.FieldName;


            if (column == "StartDate")
            {
                if (DataCheck(detailObj.TN_STD1103List, subDetail))
                {
                    MessageBox.Show("시작일이 중복되지 않게 처리");
                    subDetail.StartDate = null;
                }
            }
            else if (column == "EndDate")
            {
                if (subDetail.StartDate > subDetail.EndDate)
                {
                    MessageBox.Show("종료일은 시작일보다 전 일수 없음");
                    subDetail.EndDate = null;
                    return;
                }
            }

            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        private bool DataCheck(ICollection<TN_STD1103> list, TN_STD1103 obj)
        {
            var data = list.Where(x => x.Seq != obj.Seq &&
                                 ((x.EndDate != null && (x.StartDate <= obj.StartDate && obj.StartDate <= x.EndDate)) || (x.EndDate == null && (x.StartDate <= obj.StartDate || obj.StartDate <= x.EndDate)))).ToList();

            int cnt = data.Count;
            foreach (var s in data)
            {
                if (s.EndDate == null)
                {
                    DateTime endDt = obj.StartDate.Value.AddDays(-1);
                    if (s.StartDate > endDt)
                        return true;
                    else
                    {
                        s.EndDate = endDt;
                        cnt += -1;
                    }

                }
            }

            //과거값을 넣었을경우
            var data2 = list.Where(x => x.Seq != obj.Seq && obj.StartDate < x.StartDate).ToList();
            if (data2.Count > 0)
            {
                obj.EndDate = obj.StartDate;
            }

            if (cnt > 0)
                return true;
            else
                return false;
        }

        protected override void InitCombo()
        {
            //lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)).ToList());
            lup_TopCategory.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1).Where(p => p.CodeVal != MasterCodeSTR.TopCategory_SPARE && p.CodeVal != MasterCodeSTR.TopCategory_TOOL).ToList());

            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(x => x.UseFlag == "Y" && (x.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList());
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            //MasterGridExControl.SetToolbarButtonVisible(false);
            //DetailGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);


            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));

            DetailGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));
            DetailGridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));


            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            SubDetailGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));
            SubDetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"));
            SubDetailGridExControl.MainGrid.AddColumn("Unit", "단위");
            SubDetailGridExControl.MainGrid.AddColumn("StartDate", LabelConvert.GetLabelText("StartDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("EndDate", LabelConvert.GetLabelText("EndDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("ItemCost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "n2");

            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "StartDate", "EndDate", "ItemCost");

            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1103_NEW>(SubDetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));

            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("StartDate", false);
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("EndDate", true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("CustomerCode");
            DetailGridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            string customerCode = lup_Customer.EditValue.GetNullToEmpty();
            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            string topCode = lup_TopCategory.EditValue.GetNullToEmpty();


            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1100>(x => x.UseFlag == "Y"
                                                                                    && (x.TopCategory == MasterCodeSTR.TopCategory_BAN
                                                                                        || x.TopCategory == MasterCodeSTR.TopCategory_MAT
                                                                                        || x.TopCategory == MasterCodeSTR.TopCategory_WAN
                                                                                      )
                                                                                    && (string.IsNullOrEmpty(itemCode) ? true : x.ItemCode == itemCode)
                                                                                    && (string.IsNullOrEmpty(topCode) ? true : x.TopCategory == topCode)
                                                                                    ).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();




            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1400>(x => (string.IsNullOrEmpty(customerCode) ? true : x.CustomerCode == customerCode)
                                                                                        && x.UseFlag == "Y")
                                                                                        .OrderBy(o => o.CustomerCode)
                                                                                        .ToList();

            DetailGridExControl.MainGrid.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();


           

        }

        protected override void DataSave()
        {
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();


            ModelService.Save();
            DataLoad();
        }


        /*
        protected override void DetailFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1400;
            var detailObj = DetailGridBindingSource.Current as TN_STD1100;
            if (masterObj == null || detailObj == null) return;

            SubDetailGridExControl.MainGrid.Clear();

            //SubDetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1103_NEW>(x => x.CustomerCode == masterObj.CustomerCode && x.ItemCode == detailObj.ItemCode).ToList();
            SubDetailGridBindingSource.DataSource = detailObj.TN_STD1103NewList.Where(x => x.CustomerCode == masterObj.CustomerCode).OrderBy(o => o.StartDate).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }
        */

        protected override void SubDetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;

            var detailObj = DetailGridBindingSource.Current as TN_STD1400;

            if (masterObj == null || detailObj == null) return;

            TN_STD1103 newObj = new TN_STD1103();
            newObj.CustomerCode = detailObj.CustomerCode;
            newObj.ItemCode = masterObj.ItemCode;
            newObj.Seq = masterObj.TN_STD1103List.Count == 0 ? 1 : masterObj.TN_STD1103List.Max(m => m.Seq) + 1;
            //newObj.StartDate = DateTime.Today;

            masterObj.TN_STD1103List.Add(newObj);
            //ModelService.InsertChild<TN_STD1103_NEW>(newObj);
            SubDetailGridBindingSource.Add(newObj);
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteSubDetailRow()
        {
            var subObj = SubDetailGridBindingSource.Current as TN_STD1103;
            if (subObj == null) return;

            ModelService.RemoveChild(subObj);
            SubDetailGridBindingSource.Remove(subObj);
        }
    }
}