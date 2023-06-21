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
    public partial class XFBAN1200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<VI_BAN_QTYSUM> ModelService = (IService<VI_BAN_QTYSUM>)ProductionFactory.GetDomainService("VI_BAN_QTYSUM");
        public XFBAN1200()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
         
         

        }

        protected override void InitCombo()
        {
            lupitemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype,2));
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");            
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", "안전재고", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("StockQty", "재고수량", HorzAlignment.Far, FormatType.Numeric, "n0");

            DetailGridExControl.SetToolbarVisible(false);
     //       DetailGridExControl.MainGrid.AddColumn("Num",false);
            DetailGridExControl.MainGrid.AddColumn("InputDate", "입출고일");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("InputQty", "입고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InputId", "입출고자");       
            
        }
        protected override void InitRepository()
        {
           
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
           // MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");


            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InputId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InputDate");
         
        }
        protected override void DataLoad()
        {
            ModelService.ReLoad();
          //  DetailGridExControl.DataSource = null;
            string itemtype = lupitemtype.EditValue.GetNullToEmpty();
            string item = tx_Item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemtype) ? true : p.TN_STD1100.TopCategory == itemtype)
                                                                         &&(string.IsNullOrEmpty(item) ?true: (p.ItemCode.Contains(item) || p.TN_STD1100.ItemNm1.Contains(item) || p.TN_STD1100.ItemNm.Contains(item)))).ToList();                                                  
            //MasterGridBindingSource.DataSource = ModelService.GetList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void MasterFocusedRowChanged()
        {
            VI_BAN_QTYSUM obj = MasterGridBindingSource.Current as VI_BAN_QTYSUM;
            if (obj == null)
            {
                DetailGridExControl.DataSource = null;
                return;
            }
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_BAN_QTYM>(p => p.ItemCode == obj.ItemCode).OrderBy(o => o.InputDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }
    

  
    }
}
