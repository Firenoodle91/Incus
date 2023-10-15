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
    /// <summary>
    /// 창고위치관리화면
    /// </summary>
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

        protected override void InitCombo()
        {
            //lupWHCODE.SetDefault(true, "WhCode", "WhName", ModelService.GetList(p=>p.UseYn=="Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("WhCode", "창고코드");
            MasterGridExControl.MainGrid.AddColumn("WhName", "창고명");            
            MasterGridExControl.MainGrid.AddColumn("Posion", "창고위치", false);
            MasterGridExControl.MainGrid.AddColumn("UseYn", "사용구분");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WhCode", "WhName", "Posion", "UseYn");
            MasterGridExControl.MainGrid.SetHeaderColor(Color.Red, "WhCode", "WhName");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, "미사용[Alt+W]");
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "위치코드출력[Alt+R]");
            DetailGridExControl.MainGrid.AddColumn("_Check","선택");
            DetailGridExControl.MainGrid.AddColumn("WhCode", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("PosionA", "위치2");
            DetailGridExControl.MainGrid.AddColumn("PosionB", "위치3");
            DetailGridExControl.MainGrid.AddColumn("PosionC", "위치4");
            DetailGridExControl.MainGrid.AddColumn("PosionD", "렬1", false);
            DetailGridExControl.MainGrid.AddColumn("PosionCode", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("PosionName", "위치명");
            DetailGridExControl.MainGrid.AddColumn("UseYn","사용여부");            
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "PosionA", "PosionB", "PosionC", "PosionD", "PosionName", "UseYn");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "PosionCode", "PosionName");

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
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = textWhCodeName.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => p.WhCode.Contains(cta) || p.WhName.Contains(cta))
                                                             .OrderBy(p => p.WhCode)
                                                             .ToList();        
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }
        
        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TN_WMS1000 obj = MasterGridBindingSource.Current as TN_WMS1000;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.TN_WMS2000List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();

            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            if (SaveCheck()) return;
            //for (int rowHandle = 0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++)
            //{
            //    string _PosionA = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PosionA").GetNullToEmpty());
            //    string _PosionB = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PosionB").GetNullToEmpty());
            //    string _PosionC = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "PosionC").GetNullToEmpty());

            //    if (_PosionA == null || _PosionA == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("라인" + Convert.ToInt32(rowHandle + 1) + "행은 라인은 필수입력 사항입니다.");
            //        return;
            //    }
            //    if (_PosionB == null || _PosionB == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("행" + Convert.ToInt32(rowHandle + 1) + "행의 행는 필수입력 사항입니다.");
            //        return;
            //    }
            //    if (_PosionC == null || _PosionC == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("렬" + Convert.ToInt32(rowHandle + 1) + "행의 렬는 필수입력 사항입니다.");
            //        return;
            //    }
            //}
            ModelService.Save();

            DataLoad();
        }

        protected override void AddRowClicked()
        {
            TN_WMS1000 obj = new TN_WMS1000()
            {
                WhCode = DbRequesHandler.GetRequestNumberNew("WH")
                ,UseYn="Y"
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
      
        protected override void DetailAddRowClicked()
        {
            TN_WMS1000 obj = MasterGridBindingSource.Current as TN_WMS1000;
            if(obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            TN_WMS2000 new_obj = new TN_WMS2000()
            {
                WhCode = obj.WhCode
                ,UseYn = "Y"
                ,Seq = obj.TN_WMS2000List.Count + 1
            };
            DetailGridBindingSource.Add(new_obj);
            obj.TN_WMS2000List.Add(new_obj);
        }

        protected override void DeleteRow()
        {
            TN_WMS1000 Mobj = MasterGridBindingSource.Current as TN_WMS1000;
            if (Mobj == null) return;
            if(Mobj.TN_WMS2000List.Count > 0)
            {
                MessageBoxHandler.Show("위치코드가 등록되어 있으므로 삭제가 불가능합니다.");
                return;
            }
            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(Mobj);
        }

        protected override void DeleteDetailRow()
        {
            TN_WMS1000 Mobj = MasterGridBindingSource.Current as TN_WMS1000;
            if (Mobj == null) return;
            TN_WMS2000 obj = DetailGridBindingSource.Current as TN_WMS2000;
            if (obj == null) return;

            if (obj != null)
            {
                DialogResult result = Service.Handler.MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage(29), "위치코드"), HelperFactory.GetLabelConvert().GetLabelText("Confirm"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.No)
                {
                    DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYn", "N");
                    //obj.UseYn = "N";
                    //ModelService.Update(obj);
                    return;
                }

                Mobj.TN_WMS2000List.Remove(obj);
                DetailGridBindingSource.RemoveCurrent();
            }

            //DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYn", "N");
            //obj.UseYn = "N";
        }

        protected override void DetailFileChooseClicked()
        {
            try
            {
                if (SaveCheck()) return;
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
                //FirstReport.ShowPrintStatusDialog = false;
                //FirstReport.ShowPreview();
                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.Print();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.Name == "PosionName" || e.Column.Name == "UseYn") return;
            TN_WMS2000 obj = DetailGridBindingSource.Current as TN_WMS2000;
            string p = obj.WhCode.Right(2) + "-" + obj.PosionA + "-" + obj.PosionB;

            if (obj.PosionC.GetNullToEmpty() != "")
            { p = p + '-' + obj.PosionD; }

            if (obj.PosionD.GetNullToEmpty() != "")
            { p = p + '-' + obj.PosionD; }

            obj.PosionCode = p;
        }

        private bool SaveCheck()
        {
            var MasterList = MasterGridBindingSource.List as List<TN_WMS1000>;
            if (MasterList != null)
            {
                foreach (var v in MasterList)
                {
                    if (v.TN_WMS2000List.Any(p => p.PosionA.IsNullOrEmpty()))
                    {
                        string Msg = string.Format("창고코드 : {0} 에 라인이 등록되지 않은 항목이 있습니다.", v.WhCode);
                        HKInc.Service.Handler.MessageBoxHandler.Show(Msg);
                        return true;
                    }
                    else if (v.TN_WMS2000List.Any(p => p.PosionB.IsNullOrEmpty()))
                    {

                        string Msg = string.Format("창고코드 : {0} 에 행이 등록되지 않은 항목이 있습니다.", v.WhCode);
                        HKInc.Service.Handler.MessageBoxHandler.Show(Msg);
                        return true;
                    }
                    //else if (v.TN_WMS2000List.Any(p => p.PosionC.IsNullOrEmpty()))
                    //{
                    //    string Msg = string.Format("창고코드 : {0} 에 렬이 등록되지 않은 항목이 있습니다.", v.WhCode);
                    //    HKInc.Service.Handler.MessageBoxHandler.Show(Msg);
                    //    return true;
                    //}
                }
                return false;
            }
            else return false;
        }
    }
}
