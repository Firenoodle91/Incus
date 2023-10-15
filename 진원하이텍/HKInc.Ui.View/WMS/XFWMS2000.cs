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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.WMS

{
    public partial class XFWMS2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_WMS2000> ModelService = (IService<TN_WMS2000>)ProductionFactory.GetDomainService("TN_WMS2000");
        protected HKInc.Service.Controls.GridEx subDetailGridExControl;
        public XFWMS2000()
        {
            InitializeComponent();
            
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            subDetailGridExControl = gridEx3;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
    
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (MasterGridExControl.MainGrid.MainView.RowCount == 0)
            {
               // DetailGridExControl.MainGrid.Clear();
                subDetailGridExControl.MainGrid.Clear();

            }
            else
            {
                TN_WMS2000 obj = MasterGridBindingSource.Current as TN_WMS2000;
              //  DetailGridExControl.MainGrid.Clear();
                subDetailGridExControl.MainGrid.Clear();
                bindingSource1.DataSource = ModelService.GetChildList<VI_WHPOSITON_QTY_D2>(p => p.WhCode == obj.WhCode&&p.WhPosition==obj.PosionCode).OrderBy(o => o.ItemCode).ToList();
                subDetailGridExControl.DataSource = bindingSource1;
                subDetailGridExControl.BestFitColumns();
            }
        }

        protected override void InitCombo()
        {
            lupWHCODE.SetDefault(true, "WhCode", "WhName", ModelService.GetChildList<TN_WMS1000>(p=>p.UseYn=="Y").ToList());
            lup_item.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList());

        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);           
            MasterGridExControl.MainGrid.AddColumn("WhCode", "창고코드");                     
            MasterGridExControl.MainGrid.AddColumn("PosionCode", "창고위치");
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.MainGrid.AddColumn("whcode", "창고코드");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "창고위치");
            DetailGridExControl.MainGrid.AddColumn("Itemcode", "품번");
            DetailGridExControl.MainGrid.AddColumn("qty", "수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            subDetailGridExControl.SetToolbarButtonVisible(false);
            subDetailGridExControl.MainGrid.AddColumn("WhCode", "창고코드");
            subDetailGridExControl.MainGrid.AddColumn("WhPosition", "창고위치");
            subDetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            subDetailGridExControl.MainGrid.AddColumn("LotNo", "LOT_NO");
            subDetailGridExControl.MainGrid.AddColumn("Qty", "수량", HorzAlignment.Far, FormatType.Numeric, "n0");


            //DetailGridExControl.SetToolbarButtonVisible(false);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "추가");
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, "미사용");
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "위치코드출력");
            //IsDetailGridButtonFileChooseEnabled = true;
            //DetailGridExControl.MainGrid.AddColumn("_Check","선택");
            //DetailGridExControl.MainGrid.AddColumn("WhCode", false);
            //DetailGridExControl.MainGrid.AddColumn("Seq");
            //DetailGridExControl.MainGrid.AddColumn("PosionA", "라인");
            //DetailGridExControl.MainGrid.AddColumn("PosionB", "행");
            //DetailGridExControl.MainGrid.AddColumn("PosionC", "렬");
            //DetailGridExControl.MainGrid.AddColumn("PosionD", "렬1");
            //DetailGridExControl.MainGrid.AddColumn("PosionCode", "위치코드");
            //DetailGridExControl.MainGrid.AddColumn("PosionName", "위치설명");
            //DetailGridExControl.MainGrid.AddColumn("UseYn","사용여부");

            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "PosionA", "PosionB", "PosionC", "PosionD", "PosionName", "UseYn");


        }
        protected override void InitRepository()
        {
            
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("whcode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            subDetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("Itemcode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(),"ItemCode", "ItemNm1");
            subDetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
           
        }
        protected override void DataLoad()
        {

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupWHCODE.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>string.IsNullOrEmpty(cta)?true:p.WhCode== cta )
                 .OrderBy(O => O.PosionC).OrderBy(O => O.PosionB).OrderBy(O => O.PosionA).OrderBy(p => p.WhCode)
                                                       //  .OrderBy(p => p.WhCode).OrderBy(O=>O.PosionA).OrderBy(O => O.PosionB).OrderBy(O => O.PosionC)
                                                       .ToList();
        
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

           
            string Item = lup_item.EditValue.GetNullToEmpty();
       //var varlist = DbRequesHandler.GetDTselect("select   [wh_code] as WhCode      ,[Item_code] as ItemCode      ,[qty] as Qty      ,[WH_POSITION] as WhPosition from VI_WHPOSITON_QTY_D1");
        //    DetailGridBindingSource.DataSource=varlist.where()
          //  List<VI_WHPOSITON_QTY_D1> var = ModelService.GetChildList<VI_WHPOSITON_QTY_D1>(p=>1 == 1).ToList();
          DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_WHPOSITON_QTY_D1>(p => (string.IsNullOrEmpty(cta) ? true : p.whcode == cta)&&(string.IsNullOrEmpty(Item)?true:p.Itemcode==Item)).OrderBy(o=>o.whcode).OrderBy(o=>o.Itemcode).ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();




        }
     
      
     

    }
}
