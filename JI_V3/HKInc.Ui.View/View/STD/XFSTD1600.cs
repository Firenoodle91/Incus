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
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Service;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;


namespace HKInc.Ui.View.View.STD

{
    public partial class XFSTD1600 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_STD1600> ModelServicestd1600 = (IService<TN_STD1600>)ProductionFactory.GetDomainService("TN_STD1600");

        public XFSTD1600()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (MasterGridExControl.MainGrid.MainView.RowCount == 0)
            {
                DetailGridExControl.MainGrid.Clear();      
                ModelServicestd1600.ReLoad();
            }
            else
            {
                TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
                DetailGridExControl.MainGrid.Clear();                
                ModelServicestd1600.ReLoad();
                DetailGridBindingSource.DataSource = ModelServicestd1600.GetList(p => p.ItemCode == obj.ItemCode).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;

                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ItemType));
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemName", "품목명");
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", "주거래처");
            MasterGridExControl.MainGrid.AddColumn("CustomerItemCode", "고객사품번");
            MasterGridExControl.MainGrid.AddColumn("CustomerItemName", "고객사품명");

            MasterGridExControl.MainGrid.AddColumn("CarType", "차종");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");      
            MasterGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
          
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("DesignFile", "도면");
            DetailGridExControl.MainGrid.AddColumn("DesignMap", false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DesignFile");

        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => 1 == 1), "CustomerCode", "CustomerName");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustomerType || p.CustomerType == null)).ToList(), "CustomerCode", "CustomerName");
            DetailGridExControl.MainGrid.MainView.Columns["DesignFile"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(gridEx2, "DesignMap", "DesignFile");

        }
        protected override void DataLoad()
        {

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string cta = lupItemtype.EditValue.GetNullToEmpty();
            var itemCode = lup_Item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                    && (string.IsNullOrEmpty(cta) ? true : p.TopCategory==cta) )
                                                                    .OrderBy(p => p.ItemName)
                                                                    .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
        }
        protected override void DetailAddRowClicked()
        {
            if (DetailGridExControl.MainGrid.MainView.RowCount >= 1) return;
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            TN_STD1600 new_obj = new TN_STD1600() { ItemCode = obj.ItemCode };

            DetailGridBindingSource.Add(new_obj);
            ModelServicestd1600.Insert(new_obj);
            DetailGridBindingSource.EndEdit();

        }
        protected override void DataSave()
        {
            ModelServicestd1600.Save();
            DataLoad();
        }
    }
}