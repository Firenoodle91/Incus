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
using System.Data.SqlClient;

namespace HKInc.Ui.View.ORD

{
    public partial class XFSTRY1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
       
        public XFSTRY1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-15);
            dp_dt.DateToEdit.DateTime = DateTime.Today;
        }


     

        protected override void InitCombo()
        {
            lupWHCODE.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetList(p=>p.UseYn=="Y"&&p.TopCategory!="P03").ToList());
            lup_process.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Process));

        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
         
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품목명");            
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "차종");
            
            MasterGridExControl.MainGrid.AddColumn("Designfile", "도면");
            MasterGridExControl.MainGrid.AddColumn("Designmap", false);
            

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
           
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode","공정");
            DetailGridExControl.MainGrid.AddColumn("TryDate", "발생일");
            DetailGridExControl.MainGrid.AddColumn("ReqMemo", "발생사항");
            DetailGridExControl.MainGrid.AddColumn("RtnMemo", "조치사항");
            DetailGridExControl.MainGrid.AddColumn("RtnDate", "조치예정일");
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고");
          
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "TryDate", "ReqMemo", "RtnMemo", "RtnDate", "Memo");


        }
        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.MainView.Columns["Designfile"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(gridEx1, "Designmap", "Designfile");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["ReqMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["RtnMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("TryDate");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("RtnDate");
          
        }
        protected override void DataLoad()
        {

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupWHCODE.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetChildList<VI_TRY001>(p=>string.IsNullOrEmpty(cta)?true: p.ItemCode== cta).OrderBy(o => o.ItemCode).ToList();


            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
        }

        protected override void MasterFocusedRowChanged()
        {
            VI_TRY001 obj = MasterGridBindingSource.Current as VI_TRY001;
       
            string process = lup_process.EditValue.GetNullToEmpty();
            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_TRY001>(p => p.ItemCode == obj.ItemCode&&(string.IsNullOrEmpty(process)?true:p.ProcessCode==process) &&(p.TryDate>=dp_dt.DateFrEdit.DateTime&&p.TryDate<=dp_dt.DateToEdit.DateTime)).OrderBy(o => o.TryDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
           // GridRowLocator.SetCurrentRow();
            DetailGridExControl.BestFitColumns();
          //  lupWHCODE.EditValue = obj.ItemCode;
        }
        protected override void DetailAddRowClicked()
        {
            //    if (DetailGridExControl.MainGrid.MainView.RowCount >= 1) return;
            VI_TRY001 obj = MasterGridBindingSource.Current as VI_TRY001;

            TN_TRY001 new_obj = new TN_TRY001() { ItemCode=obj.ItemCode};
            DetailGridBindingSource.Add(new_obj);
            ModelService.InsertChild<TN_TRY001>(new_obj);
            //DetailGridBindingSource.EndEdit();

        }

        protected override void DeleteDetailRow()
        {
            TN_TRY001 obj = DetailGridBindingSource.Current as TN_TRY001;
            DetailGridBindingSource.Remove(obj);
            ModelService.RemoveChild<TN_TRY001>(obj);
            
        }
      
        protected override void DataSave()
        {
           
            ModelService.Save();
            
            DataLoad();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            VI_TRY001 obj = MasterGridBindingSource.Current as VI_TRY001;

            string process = lup_process.EditValue.GetNullToEmpty();
            List<TN_TRY001> dtllist = DetailGridBindingSource.DataSource as List<TN_TRY001>;
            if (dtllist==null) return;
            DetailGridBindingSource.DataSource = dtllist.Where(p => p.ItemCode == obj.ItemCode && (string.IsNullOrEmpty(process) ? true : p.ProcessCode == process) && (p.TryDate >= dp_dt.DateFrEdit.DateTime && p.TryDate <= dp_dt.DateToEdit.DateTime)).OrderBy(o => o.TryDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            // GridRowLocator.SetCurrentRow();
            DetailGridExControl.BestFitColumns();
         
          
        }
    }
}
