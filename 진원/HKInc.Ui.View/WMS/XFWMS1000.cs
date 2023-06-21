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
    public partial class XFWMS1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");
       
        public XFWMS1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.Name == "PosionName" || e.Column.Name == "UseYn") return;
            TN_WMS2000 obj = DetailGridBindingSource.Current as TN_WMS2000;
            string p = obj.WhCode.Right(2) + "-" + obj.PosionA + "-" + obj.PosionB + "-" + obj.PosionC;

            if (obj.PosionD.GetNullToEmpty() != "")
            { p = p + '-' + obj.PosionD; }
            
            obj.PosionCode = p;
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (MasterGridExControl.MainGrid.MainView.RowCount == 0)
            {
                DetailGridExControl.MainGrid.Clear();      
                
            }
            else
            {
                TN_WMS1000 obj = MasterGridBindingSource.Current as TN_WMS1000;
                DetailGridExControl.MainGrid.Clear();

                DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_WMS2000>(p => p.WhCode == obj.WhCode).OrderBy(o=>o.Seq).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void InitCombo()
        {
            lupWHCODE.SetDefault(true, "WhCode", "WhName", ModelService.GetList(p=>p.UseYn=="Y").ToList());

        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("WhCode", "창고코드");
            MasterGridExControl.MainGrid.AddColumn("WhName", "창고명");            
            MasterGridExControl.MainGrid.AddColumn("Posion", "창고위치");
            MasterGridExControl.MainGrid.AddColumn("UseYn", "사용구분");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WhCode", "WhName", "Posion", "UseYn");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "추가");
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, "미사용");
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "위치코드출력");
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("_Check","선택");
            DetailGridExControl.MainGrid.AddColumn("WhCode", false);
            DetailGridExControl.MainGrid.AddColumn("Seq");
            DetailGridExControl.MainGrid.AddColumn("PosionA", "라인");
            DetailGridExControl.MainGrid.AddColumn("PosionB", "행");
            DetailGridExControl.MainGrid.AddColumn("PosionC", "렬");
            DetailGridExControl.MainGrid.AddColumn("PosionD", "렬1");
            DetailGridExControl.MainGrid.AddColumn("PosionCode", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("PosionName", "위치설명");
            DetailGridExControl.MainGrid.AddColumn("UseYn","사용여부");
            
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "PosionA", "PosionB", "PosionC", "PosionD", "PosionName", "UseYn");


        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PosionA", DbRequesHandler.GetCommCode(MasterCodeSTR.WHPOSITION, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PosionB", DbRequesHandler.GetCommCode(MasterCodeSTR.WHPOSITION, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PosionC",DbRequesHandler.GetCommCode(MasterCodeSTR.WHPOSITION, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PosionD", DbRequesHandler.GetCommCode(MasterCodeSTR.WHPOSITION, 2), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.MainView.Columns["DesignFile"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(gridEx2, "DesignMap", "DesignFile");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");
        }
        protected override void DataLoad()
        {

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupWHCODE.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>string.IsNullOrEmpty(cta)?true:p.WhCode== cta )
                                                         .OrderBy(p => p.WhCode)
                                                       .ToList();
        
        MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
        }
        protected override void AddRowClicked()
        {

            TN_WMS1000 obj = new TN_WMS1000() { WhCode = DbRequesHandler.GetRequestNumberNew("WH"),UseYn="Y" };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
      
        protected override void DetailAddRowClicked()
        {
        //    if (DetailGridExControl.MainGrid.MainView.RowCount >= 1) return;
            TN_WMS1000 obj = MasterGridBindingSource.Current as TN_WMS1000;
            
            TN_WMS2000 new_obj = new TN_WMS2000() { WhCode = obj.WhCode, UseYn = "Y" , Seq=DetailGridBindingSource.Count+1 };
            DetailGridBindingSource.Add(new_obj);
            ModelService.InsertChild<TN_WMS2000>(new_obj);
            //DetailGridBindingSource.EndEdit();

        }

        protected override void DeleteDetailRow()
        {
            TN_WMS2000 obj = DetailGridBindingSource.Current as TN_WMS2000;
            obj.UseYn = "N";
            DetailGridExControl.BestFitColumns();
        }
        protected override void DetailFileChooseClicked()
        {
            try
            {
                ModelService.Save();
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_WMS2000>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;

                var FirstReport = new REPORT.RWHPOSITION();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {

                    var report = new REPORT.RWHPOSITION(v);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);

                    v._Check = "N";
                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();//.Print();
                //DataLoad();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }
        protected override void DataSave()
        {
            for (int rowHandle = 0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++)
            {
                string _PosionA = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PosionA").GetNullToEmpty());
                string _PosionB = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PosionB").GetNullToEmpty());
                string _PosionC = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PosionC").GetNullToEmpty());


                if (_PosionA == null || _PosionA == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("라인" + Convert.ToInt32(rowHandle + 1) + "행은 라인은 필수입력 사항입니다.");
                    return;
                }
                if (_PosionB == null || _PosionB == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("행" + Convert.ToInt32(rowHandle + 1) + "행의 행는 필수입력 사항입니다.");
                    return;
                }
                if (_PosionC == null || _PosionC == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("렬" + Convert.ToInt32(rowHandle + 1) + "행의 렬는 필수입력 사항입니다.");
                    return;
                }


            }
            ModelService.Save();
            
            DataLoad();
        }
    }
}
