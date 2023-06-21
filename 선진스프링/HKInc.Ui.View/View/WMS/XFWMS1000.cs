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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.WMS
{
    /// <summary>
    /// 창고관리
    /// </summary>
    public partial class XFWMS1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XFWMS1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            MasterGridExControl.MainGrid.MainView.ValidatingEditor += MainView_ValidatingEditor;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));

        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhCode"));
            MasterGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("WhCodeDivision"));
            MasterGridExControl.MainGrid.AddColumn("WhName", LabelConvert.GetLabelText("WhName"));
            MasterGridExControl.MainGrid.AddColumn("WhNameENG", LabelConvert.GetLabelText("WhNameENG"));
            MasterGridExControl.MainGrid.AddColumn("WhNameCHN", LabelConvert.GetLabelText("WhNameCHN"));
            //MasterGridExControl.MainGrid.AddColumn("WhPosition", LabelConvert.GetLabelText("WhPosition"));
            MasterGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WhCode", "Temp", "WhName", "WhNameENG", "WhNameCHN", "UseFlag", "Memo");

            //IsDetailGridButtonFileChooseEnabled = true;
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, string.Format("{0}[Alt+R]", LabelConvert.GetLabelText("BarcodePrint")), IconImageList.GetIconImage("print/printer"));

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(true, "Seq", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhCode"), false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            DetailGridExControl.MainGrid.AddColumn("PositionA", LabelConvert.GetLabelText("Line"));
            DetailGridExControl.MainGrid.AddColumn("PositionB", LabelConvert.GetLabelText("Row"));
            DetailGridExControl.MainGrid.AddColumn("PositionC", LabelConvert.GetLabelText("Column"));
            DetailGridExControl.MainGrid.AddColumn("PositionD", LabelConvert.GetLabelText("Column1"));
            DetailGridExControl.MainGrid.AddColumn("PositionCode", LabelConvert.GetLabelText("PositionCode"));
            DetailGridExControl.MainGrid.AddColumn("PositionName", LabelConvert.GetLabelText("PositionName"));
            DetailGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "PositionName", "PositionA", "PositionB", "PositionC", "PositionD", "UseFlag");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_WMS1000>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_WMS2000>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp", DbRequestHandler.GetCommTopCode(MasterCodeSTR.WhCodeDivision), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            var PositionColumn4List = new List<string>();
            PositionColumn4List.Add("1");
            PositionColumn4List.Add("2");
            PositionColumn4List.Add("3");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionA", DbRequestHandler.GetCommTopCode(MasterCodeSTR.PositionColumn1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionB", DbRequestHandler.GetCommTopCode(MasterCodeSTR.PositionColumn2), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionC", DbRequestHandler.GetCommTopCode(MasterCodeSTR.PositionColumn3_4), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionD", DbRequestHandler.GetCommTopCode(MasterCodeSTR.PositionColumn3_4).Where(p => PositionColumn4List.Contains(p.CodeVal)).ToList(), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("WhCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string whCodeName = tx_WhCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(whCodeName) ? true : p.WhCode.Contains(whCodeName) || p.WhName.Contains(whCodeName))
                                                                        && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                    )
                                                                .OrderBy(p => p.WhCode)
                                                                .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_WMS1000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            DetailGridBindingSource.DataSource = masterObj.TN_WMS2000List.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue))
                                                                        .OrderBy(o => o.PositionA)
                                                                        .ThenBy(o => o.PositionB)
                                                                        .ThenBy(o => o.PositionC != null ? o.PositionC.Length : 1)
                                                                        .ThenBy(o => o.PositionC)
                                                                        .ThenBy(o => o.PositionD)
                                                                        .ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            var newObj = new TN_WMS1000()
            {
                WhCode = DbRequestHandler.GetSeqStandard("WH"),
                UseFlag = "Y",
                NewRowFlag = "Y",
            };
            MasterGridBindingSource.Add(newObj);
            ModelService.Insert(newObj);
        }

        protected override void DeleteRow()
        {
            var obj = MasterGridBindingSource.Current as TN_WMS1000;

            if (obj != null)
            {
                if (obj.NewRowFlag == "Y")
                {
                    if (obj.TN_WMS2000List.Count > 0 && obj.TN_WMS2000List.Any(p => p.UseFlag == "Y"))
                    {
                        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_10));
                        return;
                    }
                    MasterGridBindingSource.RemoveCurrent();
                    ModelService.Delete(obj);
                    MasterGridExControl.BestFitColumns();
                }
                else
                {
                    var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("WhInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        obj.UseFlag = "N";
                        MasterGridExControl.BestFitColumns();
                    }
                }
            }
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_WMS1000;
            if (masterObj == null)
                return;

            MasterGridExControl.MainGrid.PostEditor();
            if (masterObj.WhCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WhCode")));
                return;
            }

            TN_WMS2000 new_obj = new TN_WMS2000()
            {
                WhCode = masterObj.WhCode,
                Seq = masterObj.TN_WMS2000List.Count == 0 ? 1 : masterObj.TN_WMS2000List.Max(p => p.Seq) + 1,
                UseFlag = "Y",
                NewRowFlag = "Y",
                TN_WMS1000 = masterObj,
            };

            DetailGridBindingSource.Add(new_obj);
            masterObj.TN_WMS2000List.Add(new_obj);
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_WMS1000;
            if (masterObj == null)
                return;

            var obj = DetailGridBindingSource.Current as TN_WMS2000;

            if (obj != null)
            {
                if (obj.NewRowFlag == "Y")
                {
                    DetailGridBindingSource.RemoveCurrent();
                    masterObj.TN_WMS2000List.Remove(obj);
                    DetailGridExControl.BestFitColumns();
                }
                else
                {
                    var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("PositionInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        obj.UseFlag = "N";
                        DetailGridExControl.BestFitColumns();
                    }
                }
            }
        }

        protected override void DetailFileChooseClicked()
        {
            try
            {
                if (DetailGridBindingSource == null) return;
                var detailList = DetailGridBindingSource.List as List<TN_WMS2000>;
                var printList = detailList.Where(p => p._Check == "Y").ToList();
                if (printList.Where(p => p._Check == "Y").ToList().Count == 0) return;
                
                WaitHandler.ShowWait();

                //var FirstReport = new REPORT.XRWMS1000();
                //foreach (var v in printList.OrderByDescending(p => p.CreateTime).ToList())
                //{
                //    var report = new REPORT.XRWMS1000(v);
                //    report.CreateDocument();
                //    FirstReport.Pages.AddRange(report.Pages);
                //}
                var FirstReport = new REPORT.XRWMS2000();
                foreach (var v in printList.OrderByDescending(p => p.CreateTime).ToList())
                {
                    var report = new REPORT.XRWMS2000(v);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);
                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }


        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.Name == "PosionName" || e.Column.Name == "UseFlag") return;

            var detailObj = DetailGridBindingSource.Current as TN_WMS2000;
            bool positionNameFlag = false;
            if (detailObj.PositionCode == detailObj.PositionName || detailObj.PositionName.IsNullOrEmpty())
                positionNameFlag = true;

            string p = detailObj.WhCode.Right(2);
            if (!detailObj.PositionA.IsNullOrEmpty())
                p = p + '-' + detailObj.PositionA;
            if (!detailObj.PositionB.IsNullOrEmpty())
                p = p + '-' + detailObj.PositionB;
            if (!detailObj.PositionC.IsNullOrEmpty())
                p = p + '-' + detailObj.PositionC;
            if (!detailObj.PositionD.IsNullOrEmpty())
                p = p + '-' + detailObj.PositionD;

            detailObj.PositionCode = p;

            if (positionNameFlag)
            {
                detailObj.PositionName = p;
            }           
        }

        private void MainView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            e.Valid = true;
        }
    }
}