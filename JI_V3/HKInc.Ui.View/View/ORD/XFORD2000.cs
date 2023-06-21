using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Ui.Model.BaseDomain;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Base;
using System.Collections.Generic;
using HKInc.Utils.Common;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.View.REPORT;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 제품출고관리
    /// </summary>
    public partial class XFORD2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD2000> ModelService = (IService<TN_ORD2000>)ProductionFactory.GetDomainService("TN_ORD2000");

        public XFORD2000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dateOrderDate.SetTodayIsWeek();

            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;
        }
        
        protected override void InitCombo()
        {   
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("OutprtNo", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.MainView.Columns["OutprtNo"].Width = 200;
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustomerCode", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);

            DetailGridExControl.MainGrid.AddColumn("OutprtNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seqprt", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutPrice", "출고단가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutAmt", "출고가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "OutQty", "OutPrice", "OutDate", "Memo");


            var barButtonTradingStatePrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonTradingStatePrint.Id = 4;
            barButtonTradingStatePrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonTradingStatePrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.O));
            barButtonTradingStatePrint.Name = "barButtonTradingStatePrint";
            barButtonTradingStatePrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonTradingStatePrint.ShortcutKeyDisplayString = "Alt+O";
            barButtonTradingStatePrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonTradingStatePrint.Caption = LabelConvert.GetLabelText("TradingStatePrint") + "[Alt+O]";
            barButtonTradingStatePrint.Alignment = BarItemLinkAlignment.Right;
            barButtonTradingStatePrint.ItemClick += BarButtonTradingStatePrint_ItemClick;
            MasterGridExControl.BarTools.AddItem(barButtonTradingStatePrint);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit
                ("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);


            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit
                ("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", Service.Helper.DataConvert.GetCultureDataFieldName("ItemName"));
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("RowId");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            string customerCode = lup_CustomerCode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutprtDate >= dateOrderDate.DateFrEdit.DateTime 
                                                                        &&  p.OutprtDate <= dateOrderDate.DateToEdit.DateTime) 
                                                                        && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                     )
                                                                     .OrderBy(p => p.OutprtDate)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
            if (obj == null) return;

            DetailGridBindingSource.DataSource = obj.TN_ORD2001List.OrderBy(o => o.Seqprt).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            ModelService.Save();         

            DataLoad();
        }

        protected override void AddRowClicked()
        {
            TN_ORD2000 obj = new TN_ORD2000()
            {
                OutprtNo = DbRequestHandler.GetSeqStandard("PRTOUT"),
                OutprtDate = DateTime.Today
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }


        protected override void DeleteRow()
        {

            TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
            if (obj == null) return;

            if (obj.TN_ORD2001List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OutMasterInfo"), LabelConvert.GetLabelText("OutDetailInfo"), LabelConvert.GetLabelText("OutDetailInfo")));
            }
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(obj);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
            if (obj == null) return;
            TN_ORD2001 newobj = new TN_ORD2001()
            {
                OutprtNo = obj.OutprtNo,
                Seqprt = obj.TN_ORD2001List.Count == 0 ? 1 : obj.TN_ORD2001List.OrderBy(o => o.Seqprt).LastOrDefault().Seqprt + 1,
                OutDate = DateTime.Today
            };

            DetailGridBindingSource.Add(newobj);
            obj.TN_ORD2001List.Add(newobj);
        }

        private void DetailMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var Obj = MasterGridBindingSource.Current as TN_ORD2000;
            if (Obj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD2001;
            if (detailObj == null) return;


            var date = detailObj.OutDate.GetNullToEmpty();
            if (e.Column.FieldName == "ItemCode" || e.Column.FieldName == "OutDate")
            {
                detailObj.OutPrice = DbRequestHandler.GetCustItemCost(Obj.CustomerCode, detailObj.ItemCode, date, "");
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
            if (obj == null) return;

            TN_ORD2001 delobj = DetailGridBindingSource.Current as TN_ORD2001;
            if (delobj == null) return;

            obj.TN_ORD2001List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.BestFitColumns();
        }



        //거래명세서출력
        private void BarButtonTradingStatePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ModelService.Save();
                TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;

                REPORT.XRORD2001 report = new REPORT.XRORD2001(obj.CustomerCode, obj.OutprtNo);
                report.CreateDocument();

                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
        }

    }
}
