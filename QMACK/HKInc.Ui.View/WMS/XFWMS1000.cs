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

            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;

            rbo_UseFlag.SetLabelText("사용", "미사용", "전체");
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
            MasterGridExControl.MainGrid.AddColumn("WhPosition", "창고위치");
            MasterGridExControl.MainGrid.AddColumn("UseYn", "사용여부");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WhCode", "WhName","WhPosition", "UseYn");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "추가");
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, "미사용");
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택", false);
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고코드", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("PositionA", "라인");
            DetailGridExControl.MainGrid.AddColumn("PositionB", "행");
            DetailGridExControl.MainGrid.AddColumn("PositionC", "열");
            DetailGridExControl.MainGrid.AddColumn("PositionD", "열1");
            DetailGridExControl.MainGrid.AddColumn("WhPositionCode", "창고위치코드");
            DetailGridExControl.MainGrid.AddColumn("WhPositionName", "창고위치명");
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "PositionA", "PositionB", "PositionC", "PositionD", "WhPositionName", "UseYn");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_WMS1000>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_WMS2000>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionA", DbRequestHandler.GetCommCode(MasterCodeSTR.WHPOSITION, 1), "Mcode", "Codename");   //      추후 변경       STD1000     CODE_MAIN = 'W002'
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionB", DbRequestHandler.GetCommCode(MasterCodeSTR.WHPOSITION, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionC", DbRequestHandler.GetCommCode(MasterCodeSTR.WHPOSITION, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionD", DbRequestHandler.GetCommCode(MasterCodeSTR.WHPOSITION, 2), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string RadioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            string WhCode = lupWHCODE.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(WhCode) ? true : p.WhCode == WhCode) 
                                                                        && (RadioValue == "A" ? true : p.UseYn == RadioValue))
                                                                     .OrderBy(p => p.WhCode)
                                                                     .ToList();
        
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {

            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            #region 주석

            //              Position 전부 주석처리함   DB 에서 필수값 제거             추후 확인

            //for (int rowHandle = 0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++)
            //{
            //    string _PositionA = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PositionA").GetNullToEmpty());
            //    string _PositionB = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PositionB").GetNullToEmpty());
            //    string _PositionC = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PositionC").GetNullToEmpty());


            //    if (_PositionA == null || _PositionA == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("라인" + Convert.ToInt32(rowHandle + 1) + "행은 라인은 필수입력 사항입니다.");
            //        return;
            //    }
            //    if (_PositionB == null || _PositionB == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("행" + Convert.ToInt32(rowHandle + 1) + "행의 행는 필수입력 사항입니다.");
            //        return;
            //    }
            //    if (_PositionC == null || _PositionC == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("렬" + Convert.ToInt32(rowHandle + 1) + "행의 렬는 필수입력 사항입니다.");
            //        return;
            //    }
            //}
            #endregion

            ModelService.Save();

            DataLoad();
        }

        protected override void AddRowClicked()
        {
            TN_WMS1000 MasterObj = new TN_WMS1000();

            MasterObj.WhCode = DbRequestHandler.GetRequestNumberNew("WH");
            MasterObj.UseYn = "Y";

            MasterGridBindingSource.Add(MasterObj);
            ModelService.Insert(MasterObj);
        }

        protected override void DeleteRow()
        {
            TN_WMS1000 MasterObj = MasterGridBindingSource.Current as TN_WMS1000;
            MasterObj.UseYn = "N";

            MasterGridExControl.MainGrid.UpdateCurrentRow();
        }

        protected override void DetailAddRowClicked()
        {
            TN_WMS1000 MasterObj = MasterGridBindingSource.Current as TN_WMS1000;
            TN_WMS2000 DetailObj = new TN_WMS2000();

            DetailObj.WhCode = MasterObj.WhCode;
            DetailObj.UseYn = "Y";
            DetailObj.Seq = MasterObj.TN_WMS2000List.Count == 0 ? 1 : MasterObj.TN_WMS2000List.Max(p => p.Seq) + 1;

            DetailGridBindingSource.Add(DetailObj);
            MasterObj.TN_WMS2000List.Add(DetailObj);
        }

        protected override void DeleteDetailRow()
        {
            TN_WMS2000 DetailObj = DetailGridBindingSource.Current as TN_WMS2000;
            DetailObj.UseYn = "N";
            //DetailGridExControl.BestFitColumns();
        }

        //protected override void DetailFileChooseClicked()
        //{
        //    try
        //    {
        //        ModelService.Save();
        //        WaitHandler.ShowWait();
        //        if (DetailGridBindingSource == null) return;
        //        var PrintList = DetailGridBindingSource.List as List<TN_WMS2000>;
        //        if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;

        //        var FirstReport = new REPORT.RWHPOSITION();
        //        foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
        //        {
        //            var report = new REPORT.RWHPOSITION(v);
        //            report.CreateDocument();
        //            FirstReport.Pages.AddRange(report.Pages);

        //            v._Check = "N";
        //        }
        //        FirstReport.ShowPrintStatusDialog = false;
        //        FirstReport.ShowPreview();//.Print();
        //        //DataLoad();
        //        DetailGridExControl.MainGrid.MainView.RefreshData();
        //    }
        //    catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
        //    finally { WaitHandler.CloseWait(); }
        //}
        
        protected override void MasterFocusedRowChanged()
        {
            if (MasterGridExControl.MainGrid.MainView.RowCount == 0)
                DetailGridExControl.MainGrid.Clear();
            else
            {
                string RadioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

                TN_WMS1000 MasterObj = MasterGridBindingSource.Current as TN_WMS1000;
                DetailGridExControl.MainGrid.Clear();

                DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_WMS2000>(p => (p.WhCode == MasterObj.WhCode)
                                                                                                && (RadioValue == "A" ? true : p.UseYn == RadioValue))
                                                                                          .OrderBy(o => o.Seq).ToList();

                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
            }
        }

        private void DetailView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "WhPositionName" || e.Column.Name == "UseYn") return;
            TN_WMS2000 obj = DetailGridBindingSource.Current as TN_WMS2000;
            string Code = obj.WhCode.Right(2) + "-" + obj.PositionA + "-" + obj.PositionB + "-" + obj.PositionC;

            if (obj.PositionD.GetNullToEmpty() != "")
            { Code = Code + '-' + obj.PositionD; }

            obj.WhPositionCode = Code;
            obj.WhPositionName = Code;          // 2021-11-30 김진우 주임 추가
        }
    }
}
