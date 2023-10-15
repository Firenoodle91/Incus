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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.ORD
{
    public partial class XFORD2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD2000> ModelService = (IService<TN_ORD2000>)ProductionFactory.GetDomainService("TN_ORD2000");
       
        public XFORD2000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dateOrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dateOrderDate.DateToEdit.DateTime = DateTime.Today;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
           // MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
         
        }

 

    
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
         

            MasterGridExControl.MainGrid.AddColumn("OutprtNo", "출고번호");         
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            //MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종", true, HorzAlignment.Center);
            //MasterGridExControl.MainGrid.AddColumn("OrderQty", "요청수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            //MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            //MasterGridExControl.MainGrid.AddColumn("OutState", "상태");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustCode", "Memo");
            MasterGridExControl.MainGrid.MainView.Columns["OutprtNo"].Width = 200;
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "출고내역참조");

            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("OutprtNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seqprt", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutPrice", "출고단가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutAmt", "출고가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "OutQty", "OutPrice","OutDate", "Memo");

        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
         //   DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
        }
        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string customerCode = lupCustcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutprtDate >= dateOrderDate.DateFrEdit.DateTime &&
                                                                                  p.OutprtDate <= dateOrderDate.DateToEdit.DateTime) &&
                                                                                  (string.IsNullOrEmpty(customerCode) ? true : p.CustCode == customerCode))
                                                                   .OrderBy(p => p.OutprtDate)
                                                                   .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            GridRowLocator.SetCurrentRow();
            LoadDetail();

            IsFormControlChanged = false;
        }
        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
            if (obj == null) return;
          
            DetailGridBindingSource.DataSource = obj.ORD2001List.OrderBy(o => o.OutDate).OrderBy(o => o.Seqprt).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

        }
        private void LoadDetail()
        {
            try
            {
              DetailGridExControl.MainGrid.Clear();

                TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
                if (obj == null) return;
       //       if (obj.ORD2001List.Count == 0) return;
                DetailGridBindingSource.DataSource = obj.ORD2001List.OrderBy(o => o.OutDate).OrderBy(o=>o.Seqprt).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
            }
            finally
            {
                //SetRefreshMessage("OrderList", MasterGridExControl.MainGrid.RecordCount,
                //                  "DetailList", DetailGridExControl.MainGrid.RecordCount);
            }
        }
        protected override void DataSave()
        {




            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            ModelService.Save();
          //  ModelService2.Save();

            DataLoad();
        }
        protected override void AddRowClicked()
        {
            TN_ORD2000 obj = new TN_ORD2000()
            {
                OutprtNo = DbRequesHandler.GetRequestNumber("PRTOUT"),
                OutprtDate = DateTime.Today
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
        protected override void DeleteRow()
        {
            TN_ORD2000 tn = MasterGridBindingSource.Current as TN_ORD2000;
            if (tn.ORD2001List.Count > 0)
            {
                MessageBox.Show("거래명세서 발행내역이 있어서 삭제 불가합니다.");
            }
            else {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
            }


        }
        protected override void DetailAddRowClicked()
        {
            TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
            if (obj == null) return;
            TN_ORD2001 newobj = new TN_ORD2001()
            {
                OutprtNo = obj.OutprtNo,
                Seqprt = obj.ORD2001List.Count==0?1: obj.ORD2001List.OrderBy(o => o.Seqprt).LastOrDefault().Seqprt + 1

            //      OutDate = DateTime.Today

        };
            DetailGridBindingSource.Add(newobj);
            obj.ORD2001List.Add(newobj);






        }
     
     
        protected override void DeleteDetailRow()
        {
            TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
            TN_ORD2001 delobj = DetailGridBindingSource.Current as TN_ORD2001;
            obj.ORD2001List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                ModelService.Save();
                TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;

                REPORT.XRORD2001 report = new REPORT.XRORD2001(obj.CustCode, obj.OutprtNo);
            //    report.DataSource = ModelService.GetChildList<TN_ORD2001>(p => p.OutprtNo == obj.OutprtNo);
                report.CreateDocument();


                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        protected override void DetailFileChooseClicked()
        {
            TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;
            if (obj.CustCode.GetNullToEmpty() == "") return;
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            param.SetValue(PopupParameter.Value_1, obj.CustCode);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTOUTLIST, param, AddOrd2001);
            form.ShowPopup(true);
        }
        private void AddOrd2001(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;


            List<VI_OUTLIST> partList = (List<VI_OUTLIST>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_ORD2000 oldobj = MasterGridBindingSource.Current as TN_ORD2000;
            foreach (var returnedPart in partList)
            {
                //if (oldobj.ORD2001List.Where(p => p.ItemCode == returnedPart.ItemCode).ToList().Count == 0)
                //{

                    TN_ORD2001 obj = (TN_ORD2001)DetailGridBindingSource.AddNew();
                    obj.OutprtNo = oldobj.OutprtNo;
                    obj.Seqprt = oldobj.ORD2001List.Count == 0 ? 1 : oldobj.ORD2001List.OrderBy(o => o.Seqprt).LastOrDefault().Seqprt + 1;
                    //oldobj.PUR1301List.Count + 1;
                    obj.ItemCode = returnedPart.ItemCode;
                    obj.OutQty = returnedPart.OutQty;
                    obj.OutNo = returnedPart.OutNo;
                    obj.Seq = returnedPart.Seq;                    
                    obj.Memo = returnedPart.Memo;
                    obj.OutDate = returnedPart.OutDate;
                    oldobj.ORD2001List.Add(obj);
             //   }
               
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

    }
}
