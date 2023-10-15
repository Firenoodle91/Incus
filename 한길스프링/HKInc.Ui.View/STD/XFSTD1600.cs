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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;

namespace HKInc.Ui.View.STD
{
    //도면관리화면
    public partial class XFSTD1600 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_STD1600> ModelServicestd1600 = (IService<TN_STD1600>)ProductionFactory.GetDomainService("TN_STD1600");
        public XFSTD1600()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            //MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
        }

        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));

        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("MainCust", "주거래처", false);
            MasterGridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번", false);
            MasterGridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명", false);
        
            //GridExControl.MainGrid.AddColumn("ItemGbn");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "소분류");
            //MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            //MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            //MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            //MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "원재료)");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "색상)");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "재료규격");
            MasterGridExControl.MainGrid.AddColumn("Temp2", "실리콘농도");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "제품규격");
            MasterGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
          
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("DesignFile", "도면");
            DetailGridExControl.MainGrid.AddColumn("DesignMap", false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DesignFile");

        }
        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["DesignFile"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(gridEx2, "DesignMap", "DesignFile");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta))
                                                         .OrderBy(p => p.ItemNm)
                                                       .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            GridRowLocator.SetCurrentRow();
        }
        protected override void DetailAddRowClicked()
        {
        //    if (DetailGridExControl.MainGrid.MainView.RowCount >= 1) return;
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;

            TN_STD1600 new_obj = new TN_STD1600();// { ItemCode = obj.ItemCode };
            obj.STD1600List.Add(new_obj);
            DetailGridBindingSource.Add(new_obj);
            //ModelServicestd1600.Insert(new_obj);
            //DetailGridBindingSource.EndEdit();

        }
        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            //ModelServicestd1600.Save();
            ModelService.Save();
            DataLoad();
        }
        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            DetailGridBindingSource.DataSource = masterObj.STD1600List.OrderByDescending(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
    }
}