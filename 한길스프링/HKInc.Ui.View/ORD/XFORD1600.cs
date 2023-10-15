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
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 제품기타출고관리
    /// </summary>
    public partial class XFORD1600 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1600> ModelService = (IService<TN_ORD1600>)ProductionFactory.GetDomainService("TN_ORD1600");
       
        public XFORD1600()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dateOrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dateOrderDate.DateToEdit.DateTime = DateTime.Today;
            MasterGridExControl.MainGrid.MainView.CellValueChanged += MasterMainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;            
        }

        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "라벨출력[F10]", IconImageList.GetIconImage("print/printer"));
            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");         
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "요청수량");
            MasterGridExControl.MainGrid.AddColumn("OutQty", "출고수량");
            //MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("OutState", "상태");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustCode", "ItemCode", "OrderQty", "OutId", "Memo");
            MasterGridExControl.MainGrid.SetHeaderColor(Color.Red, "CustCode", "ItemCode", "OrderQty");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "픔목코드", false);
            DetailGridExControl.MainGrid.AddColumn("LotNo", "출고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고수량");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "LotNo", "OutQty","OutDate", "Memo");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "LotNo", "OutQty", "OutDate");

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderQty", DefaultBoolean.Default, "n0", true);
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty", DefaultBoolean.Default, "n0", true, false);
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1", false, true, ItemCode_Production_Ellipsis);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");

            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty", DefaultBoolean.Default, "n0", true);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");
            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string customerCode = lupCustcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dateOrderDate.DateFrEdit.DateTime 
                                                                            && p.OutDate <= dateOrderDate.DateToEdit.DateTime) 
                                                                        && (string.IsNullOrEmpty(customerCode) ? true : p.CustCode == customerCode)
                                                                     )
                                                                     .OrderBy(p => p.OutDate)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            LoadDetail();
        }

        private void LoadDetail()
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.ORD1601List.OrderBy(o => o.Seq).ToList();
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
            TN_ORD1600 obj = new TN_ORD1600()
            {
                OutNo = DbRequesHandler.GetRequestNumber("PTOUT"),
                OutDate = DateTime.Today
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }

        protected override void DeleteRow()
        {
            TN_ORD1600 tn = MasterGridBindingSource.Current as TN_ORD1600;
            if (tn == null) return;
            if (tn.ORD1601List.Count > 0)
            {
                MessageBox.Show("출고내역이 있어서 삭제 불가합니다.");
            }
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            if (obj == null) return;
            TN_ORD1601 newobj = new TN_ORD1601()
            {
                OutNo = obj.OutNo,
                Seq = obj.ORD1601List.Count == 0 ? 1 : obj.ORD1601List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                ItemCode = obj.ItemCode,
                OutDate = DateTime.Today               
            };
            DetailGridBindingSource.Add(newobj);
            obj.ORD1601List.Add(newobj);
        }     
     
        protected override void DeleteDetailRow()
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            if (obj == null) return;
            TN_ORD1601 delobj = DetailGridBindingSource.Current as TN_ORD1601;
            if (delobj == null) return;
            obj.ORD1601List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void FileChooseClicked()
        {
            try
            {
                WaitHandler.ShowWait();
                TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_ORD1601>;
                if (PrintList.Count == 0) return;

                var FirstReport = new REPORT.ROUTLABLE();
                foreach (var v in PrintList.OrderByDescending(p => p.CreateTime).ToList())
                {
                    PRT_OUTLABLE prt = new PRT_OUTLABLE()
                    {
                        ItemCode = v.ItemCode,
                        ItemNm = DbRequesHandler.GetCellValue("SELECT ITEM_NM FROM TN_STD1100T WHERE ITEM_CODE='" + v.ItemCode + "'", 0),
                        PrtDate = v.OutDate.ToString().Substring(0, 10),
                        Qty = v.OutQty,
                        LotNo = v.OutNo,
                        CustLotNo = obj.Memo
                    };
                    var report = new REPORT.ROUTLABLE(prt);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);


                }
                //FirstReport.ShowPrintStatusDialog = false;
                //FirstReport.ShowPreview();//.Print();
                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.Print();
                //DataLoad();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private void MasterMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (e.Column.FieldName == "ItemCode")
            {
                var MasterObj = MasterGridBindingSource.Current as TN_ORD1600;
                if (MasterObj == null) return;
                MasterObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == MasterObj.ItemCode).First();
                MasterGridExControl.MainGrid.MainView.RefreshData();
                MasterGridExControl.BestFitColumns();
            }
        }

        private void DetailMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            masterreview();
        }

        private void masterreview()
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            obj.OutDate = obj.ORD1601List.OrderBy(p => p.OutDate).FirstOrDefault().OutDate;
            obj.OutQty = obj.ORD1601List.Sum(s => s.OutQty).GetDecimalNullToZero();
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private void ItemCode_Production_Ellipsis(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            if (edit == null) return;

            var MasterObj = MasterGridBindingSource.Current as TN_ORD1600;
            if (MasterObj == null) return;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                PopupDataParam param = new PopupDataParam();
                IPopupForm form;
                param.SetValue(PopupParameter.IsMultiSelect, false);
                param.SetValue(PopupParameter.Constraint, MasterCodeSTR.ItemCode_Production);
                form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTSTD1100, param, SetDetailGridView_ItemCode);
                form.ShowPopup(true);
            }
        }

        private void SetDetailGridView_ItemCode(object sender, Utils.Common.PopupArgument e)
        {
            var MasterObj = MasterGridBindingSource.Current as TN_ORD1600;
            if (MasterObj == null) return;

            if (e != null)
            {
                PopupDataParam param = e.Map;
                if (param.Keys.Contains(PopupParameter.ReturnObject))
                {
                    var obj = param.GetValue(PopupParameter.ReturnObject) as TN_STD1100;
                    if (obj != null)
                    {
                        MasterGridExControl.MainGrid.MainView.SetFocusedRowCellValue("ItemCode", obj.ItemCode);
                    }
                }
            }
        }
    }
}
