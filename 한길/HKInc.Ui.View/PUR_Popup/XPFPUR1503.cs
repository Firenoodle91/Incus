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
using HKInc.Service.Base;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Common;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.PUR_Popup
{
    public partial class XPFPUR1503 : PopupCallbackFormTemplate
    {
        IService<VI_PURINOUT> ModelService = (IService<VI_PURINOUT>)ProductionFactory.GetDomainService("VI_PURINOUT");
        VI_PURINOUT VI_PURINOUT;

        List<ReturnInLabelTemp> gridList = new List<ReturnInLabelTemp>();

        public XPFPUR1503()
        {
            InitializeComponent();
        }
               
        public XPFPUR1503(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            VI_PURINOUT = (VI_PURINOUT)PopupParam.GetValue(PopupParameter.KeyValue);
            GridExControl = gridEx1;

            GridExControl.ActAddRowClicked += GridExControl_ActAddRowClicked;
            GridExControl.ActDeleteRowClicked += GridExControl_ActDeleteRowClicked;
        }
        
        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonCaption(ToolbarButton.Refresh, "초기화[F1]");
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Print, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            textDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            textDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            textDate.Properties.Mask.UseMaskAsDisplayFormat = true;

            textQty.Properties.Mask.EditMask = "n0";
            textQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textQty.Properties.Mask.UseMaskAsDisplayFormat = true;

            var userObj = ModelService.GetChildList<UserView>(p => p.LoginId == VI_PURINOUT.Work).FirstOrDefault();
            var whObj = ModelService.GetChildList<TN_WMS1000>(p => p.WhCode == VI_PURINOUT.WhCode).FirstOrDefault();
            var positionObj = ModelService.GetChildList<TN_WMS2000>(p => p.PosionCode == VI_PURINOUT.WhPosition).FirstOrDefault();

            textDate.EditValue = VI_PURINOUT.InOutDate;
            textId.EditValue = userObj == null ? string.Empty : userObj.UserName;
            textLotNo.EditValue = VI_PURINOUT.InLotNo;
            textQty.EditValue = VI_PURINOUT.InputQty;
            textWhCode.EditValue = whObj == null ? string.Empty : whObj.WhName;
            textWhPosition.EditValue = positionObj == null ? string.Empty : positionObj.PosionName;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            GridExControl.MainGrid.AddColumn("Qty", "수량");
            GridExControl.MainGrid.AddColumn("Date", "입고일");
            GridExControl.MainGrid.AddColumn("WhCode", "입고창고");
            GridExControl.MainGrid.AddColumn("WhPosition", "입고위치");
            GridExControl.MainGrid.SetEditable(true, "Qty", "Date", "WhCode", "WhPosition");
        }
        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("Qty", DefaultBoolean.Default, "n0", true, true);
            GridExControl.MainGrid.SetRepositoryItemDateEdit("Date");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            gridList.Clear();
            GridExControl.MainGrid.Clear();

            ModelBindingSource.DataSource = gridList;
            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        private void GridExControl_ActAddRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ModelBindingSource == null) return;
            GridExControl.MainGrid.PostEditor();
            ModelBindingSource.Add(new ReturnInLabelTemp()
            {
                Date = VI_PURINOUT.InOutDate,
                WhCode = VI_PURINOUT.WhCode,
                WhPosition = VI_PURINOUT.WhPosition
            });
            ModelBindingSource.MoveLast();
            GridExControl.BestFitColumns();
        }

        private void GridExControl_ActDeleteRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ModelBindingSource == null) return;

            GridExControl.MainGrid.PostEditor();
            ModelBindingSource.RemoveCurrent();
            GridExControl.BestFitColumns();
        }

        protected override void DataPrint()
        {
            if (ModelBindingSource == null) return;
            GridExControl.MainGrid.PostEditor();
            var printList = ModelBindingSource.List as List<ReturnInLabelTemp>;
            if (printList.Count > 0)
            {
                try
                {
                    WaitHandler.ShowWait();
                    var FirstReport = new REPORT.RINPUTLABLE();
                    foreach (var v in printList.ToList())
                    {
                        var report = new REPORT.RINPUTLABLE(v, VI_PURINOUT);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }

                    FirstReport.PrintingSystem.ShowMarginsWarning = false;
                    FirstReport.ShowPrintStatusDialog = false;
                    FirstReport.ShowPreview();
                    //FirstReport.Print();
                }
                catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
                finally { WaitHandler.CloseWait(); }
            }
        }

        protected override void ActClose()
        {
            SetIsFormControlChanged(false);
            base.ActClose();
        }

        protected override void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetIsFormControlChanged(false);
            base.BaseForm_FormClosing(sender, e);
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            if (e.Column.FieldName == "WhCode")
            {
                ReturnInLabelTemp dtlobj = ModelBindingSource.Current as ReturnInLabelTemp;
                GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList(), "PosionCode", "PosionName");
            }
            else if (e.Column.FieldName == "WhPosition")
            {
                GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
            }
        }
    }
}