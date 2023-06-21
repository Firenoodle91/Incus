using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS1900 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<VI_PROCESSING_QTY> ModelService = (IService<VI_PROCESSING_QTY>)ProductionFactory.GetDomainService("VI_PROCESSING_QTY");
        public XFMPS1900()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;


        }

     
        protected override void InitCombo()
        {
            //lupitemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y" && (p.TopCategory == "P01" || p.TopCategory == "P05")).ToList());
            lupitemcode.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P01" || p.TopCategory == "P05")).ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.ShowFooter=true;
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MainCust", "고객사", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("P01", "와이어벤딩", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("P02", false);
            MasterGridExControl.MainGrid.AddColumn("P13", "권선가공", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("P14", "템퍼링", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("P05", false);
            MasterGridExControl.MainGrid.AddColumn("P09", "검사포장", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("P07",false);
            MasterGridExControl.MainGrid.AddColumn("P08", false);
            

            MasterGridExControl.MainGrid.SummaryItemAddNew(8);
            MasterGridExControl.MainGrid.SummaryItemAddNew(9);
            MasterGridExControl.MainGrid.SummaryItemAddNew(10);
            MasterGridExControl.MainGrid.SummaryItemAddNew(11);
            MasterGridExControl.MainGrid.SummaryItemAddNew(12);
            MasterGridExControl.MainGrid.SummaryItemAddNew(13);
            MasterGridExControl.MainGrid.SummaryItemAddNew(14);
            MasterGridExControl.MainGrid.SummaryItemAddNew(15);
            




        }
        protected override void InitRepository()
        {


            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MainCust", ModelService.GetChildList<TN_STD1400>(p => 1 == 1).ToList(), "CustomerCode", "CustomerName");
        }
        protected override void DataLoad()
        {
            ModelService.ReLoad();
            //  DetailGridExControl.DataSource = null;
            MasterGridExControl.MainGrid.Clear();
       
            string itemcode = lupitemcode.EditValue.GetNullToEmpty();
      
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>(string.IsNullOrEmpty(itemcode)?true:p.ItemCode==itemcode )).ToList();                                                  
            //MasterGridBindingSource.DataSource = ModelService.GetList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }


      
    }
}
