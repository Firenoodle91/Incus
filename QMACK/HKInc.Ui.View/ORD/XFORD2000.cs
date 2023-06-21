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
    /// <summary>
    /// 거래명세서
    /// </summary>
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

            DetailGridExControl.MainGrid.MainView.CellValueChanged += Detail_CellValueChanged;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            MasterGridExControl.MainGrid.AddColumn("OutprtNo", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustCode", "Memo");
            MasterGridExControl.MainGrid.MainView.Columns["OutprtNo"].Width = 200;

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "출고내역참조");
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("OutprtNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seqprt", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일");       // 컬럼위치 이동      2022-07-18 김진우
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutPrice", "출고단가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutAmt", "출고가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "OutQty", "OutPrice", "OutDate", "Memo");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");   //  2022-04-07 김진우 품목코드 품목 품번 보이도록 수정
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
                OutprtNo = DbRequestHandler.GetRequestNumber("PRTOUT"),
                OutprtDate = DateTime.Today
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }

        protected override void DeleteRow()
        {
            TN_ORD2000 tn = MasterGridBindingSource.Current as TN_ORD2000;
            if (tn.ORD2001List.Count > 0)
                MessageBox.Show("거래명세서 발행내역이 있어서 삭제 불가합니다.");
            else
            {
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
                Seqprt = obj.ORD2001List.Count == 0 ? 1 : obj.ORD2001List.OrderBy(o => o.Seqprt).LastOrDefault().Seqprt + 1
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

        /// <summary>
        /// 거래명세서 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                ModelService.Save();
                TN_ORD2000 obj = MasterGridBindingSource.Current as TN_ORD2000;

                REPORT.XRORD2001 report = new REPORT.XRORD2001(obj.CustCode, obj.OutprtNo);
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
                decimal Cost = 0;           // 2022-06-22 김진우 추가            판매계획관리의 단가값 가져옴
                TN_ORD1200 ORD1200 = ModelService.GetChildList<TN_ORD1200>(p => p.OutNo == returnedPart.OutNo && p.ItemCode == returnedPart.ItemCode).FirstOrDefault();
                if (ORD1200 != null) {
                    TN_ORD1000 ORD1000 = ModelService.GetChildList<TN_ORD1000>(p => p.OrderNo == ORD1200.OrderNo).FirstOrDefault();
                    if (ORD1000 != null) {
                        TN_ORD1002 ORD1002 = ModelService.GetChildList<TN_ORD1002>(p => p.OrderNo == ORD1000.OrderNo && p.ItemCode == returnedPart.ItemCode).FirstOrDefault();
                        if (ORD1002 != null)
                            Cost = ORD1002.Cost.GetDecimalNullToZero();
                    }
                }

                TN_ORD2001 obj = (TN_ORD2001)DetailGridBindingSource.AddNew();
                obj.OutprtNo = oldobj.OutprtNo;
                obj.Seqprt = oldobj.ORD2001List.Count == 0 ? 1 : oldobj.ORD2001List.OrderBy(o => o.Seqprt).LastOrDefault().Seqprt + 1;
                obj.ItemCode = returnedPart.ItemCode;
                obj.OutQty = returnedPart.OutQty;
                obj.OutNo = returnedPart.OutNo;
                obj.OutPrice = Cost;                // 2022-06-22 김진우 추가
                obj.Seq = returnedPart.Seq;                    
                obj.Memo = returnedPart.Memo;
                obj.OutDate = returnedPart.OutDate;
                oldobj.ORD2001List.Add(obj);
            }

            if (partList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 단가이력관리의 단가값 호출기능 추가
        /// 2022-06-21 김진우 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Detail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_ORD2000 MasterObj = MasterGridBindingSource.Current as TN_ORD2000;
            TN_ORD2001 DetailObj = DetailGridBindingSource.Current as TN_ORD2001;
            if (MasterObj == null || DetailObj == null || MasterObj.CustCode == null) return;

            if ((e.Column.Name == "ItemCode" && DetailObj.OutDate != null) || (e.Column.Name == "OutDate" && DetailObj.ItemCode != null))
            {
                TN_STD1121 STD1121 = ModelService.GetChildList<TN_STD1121>(p => p.ItemCode == DetailObj.ItemCode
                                                             && p.CustomerCode == MasterObj.CustCode
                                                             && (p.StartDate <= DetailObj.OutDate && p.EndDate >= DetailObj.OutDate)
                                                                            ).FirstOrDefault();
                if (STD1121 != null)
                    DetailObj.OutPrice = STD1121.TotalCost;
                else
                {
                    MessageBoxHandler.Show("단가가 존재하지 않습니다.");
                    DetailObj.OutPrice = 0;
                }
            }
        }
    }
}
