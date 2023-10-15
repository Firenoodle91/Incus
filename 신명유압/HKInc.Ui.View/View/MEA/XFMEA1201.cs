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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using DevExpress.Utils;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 스페어파트 입출고관리
    /// </summary>
    public partial class XFMEA1201 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFMEA1201()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            lup_Spare.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_SPARE)).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("SpareCode"));
            MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("SpareName"));
            MasterGridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("SpareNameENG"));
            MasterGridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("SpareNameCHN"));
            MasterGridExControl.MainGrid.AddColumn("SpareStockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            MasterGridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"));
            MasterGridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"));
            MasterGridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            MasterGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", LabelConvert.GetLabelText("Date"));
            DetailGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            DetailGridExControl.MainGrid.AddColumn("Qty", LabelConvert.GetLabelText("Qty"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("InOutId", LabelConvert.GetLabelText("CreateId"));
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InOutDate", "Division", "Qty", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1201>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_ProdImage, "ProdFileName", "ProdFileUrl", false);
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", false);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InOutDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InOutDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Qty");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var spareCode = lup_Spare.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(spareCode) ? true : p.ItemCode == spareCode)
                                                                    && (p.UseFlag == "Y")
                                                                    && (p.TopCategory == MasterCodeSTR.TopCategory_SPARE)
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MEA1201List.OrderBy(p => p.CreateTime).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            if (!masterObj.TN_MEA1201List.Any(p => p.NewRowFlag == "Y"))
            {
                var newObj = new TN_MEA1201()
                {
                    Seq = masterObj.TN_MEA1201List.Count == 0 ? 1 : masterObj.TN_MEA1201List.Max(p => p.Seq) + 1,
                    InOutDate = DateTime.Today,
                    Qty = 0,
                    InOutId = GlobalVariable.LoginId
                };
                masterObj.TN_MEA1201List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_MEA1201;
            if (detailObj == null) return;

            masterObj.TN_MEA1201List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }
    }
}