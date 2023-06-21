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
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 제품기타입고등록
    /// </summary>
    public partial class XFORD1700 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1700> ModelService = (IService<TN_ORD1700>)ProductionFactory.GetDomainService("TN_ORD1700");
       
        public XFORD1700()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dateOrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dateOrderDate.DateToEdit.DateTime = DateTime.Today;
            MasterGridExControl.MainGrid.MainView.CellValueChanged += MasterMainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;
        }
        
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("InNo", "입고번호");         
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("InQty", "입고수량");
            //MasterGridExControl.MainGrid.AddColumn("InDate", "입고일");
            MasterGridExControl.MainGrid.AddColumn("InId", "입고자");            
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustCode", "ItemCode",  "InId", "Memo");
            MasterGridExControl.MainGrid.SetHeaderColor(Color.Red, "CustCode", "ItemCode");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("InNo", false);
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택"); // 20220405 오세완 차장 바코드 출력 선택기능 때문에 추가 
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "픔목코드", false);
            DetailGridExControl.MainGrid.AddColumn("LotNo", "입고 LOT NO");

            DetailGridExControl.MainGrid.AddColumn("InQty", "입고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InDate", "입고일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");

            // 20220405 오세완 차장 바코드 출력 선택기능 때문에 추가 
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "LotNo", "InQty","InDate", "Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "LotNo", "InQty", "InDate", "Memo", "_Check");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "LotNo", "InQty", "InDate");

            // 20220405 오세완 차장 이차장 요청으로 추가 
            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 5;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.Y));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+Y";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = "현품표출력[Alt+Y]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);

        }

        /// <summary>
        /// 20220405 오세완 차장 입고 바코드 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ModelService.Save();
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null)
                    return;

                var PrintList = DetailGridBindingSource.List as List<TN_ORD1701>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0)
                    return;

                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {
                    var report = new REPORT.RINPUTLABLE(v);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);
                    
                    v._Check = "N";
                }

                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.Print();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1", false, true, ItemCode_Production_Ellipsis);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty", DefaultBoolean.Default, "n0", true, false);
            
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty", DefaultBoolean.Default, "n0", true);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N"); // 20220405 오세완 차장 바코드 출력 때문에 추가 
        }

        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InNo");          
            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string customerCode = lupCustcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= dateOrderDate.DateFrEdit.DateTime 
                                                                            && p.InDate <= dateOrderDate.DateToEdit.DateTime) 
                                                                        && (string.IsNullOrEmpty(customerCode) ? true : p.CustCode == customerCode)
                                                                     )
                                                                     .OrderBy(p => p.InDate)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            LoadDetail();
        }

        private void LoadDetail()
        {
            var obj = MasterGridBindingSource.Current as TN_ORD1700;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.ORD1701List.OrderBy(o => o.Seq).ToList();
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
            TN_ORD1700 obj = new TN_ORD1700()
            {
                InNo = DbRequesHandler.GetRequestNumber("PTIN"),
                InDate = DateTime.Today
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }

        protected override void DeleteRow()
        {
            TN_ORD1700 tn = MasterGridBindingSource.Current as TN_ORD1700;
            if (tn == null) return;

            if (tn.ORD1701List.Count > 0)
            {
                MessageBox.Show("입고내역이 있어서 삭제 불가합니다.");
            }
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            if (obj == null) return;
            TN_ORD1701 newobj = new TN_ORD1701()
            {
                InNo = obj.InNo,
                Seq = obj.ORD1701List.Count == 0 ? 1 : obj.ORD1701List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                ItemCode=obj.ItemCode,
                InDate = DateTime.Today
               
            };
            DetailGridBindingSource.Add(newobj);
            obj.ORD1701List.Add(newobj);
        }
      
        protected override void DeleteDetailRow()
        {
            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            if (obj == null) return;
            TN_ORD1701 delobj = DetailGridBindingSource.Current as TN_ORD1701;
            if (delobj == null) return;

            obj.ORD1701List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void MasterMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (e.Column.FieldName == "ItemCode")
            {
                var MasterObj = MasterGridBindingSource.Current as TN_ORD1700;
                if (MasterObj == null) return;
                MasterObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == MasterObj.ItemCode).First();
                MasterGridExControl.MainGrid.MainView.RefreshData();
                MasterGridExControl.BestFitColumns();
            }
        }

        private void DetailMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "_Check")
                SetIsFormControlChanged(false);

            masterreview();
        }

        private void masterreview()
        {
            var obj = MasterGridBindingSource.Current as TN_ORD1700;
            obj.InDate = obj.ORD1701List.OrderBy(p => p.InDate).FirstOrDefault().InDate;
            obj.InQty = obj.ORD1701List.Sum(s => s.InQty).GetDecimalNullToZero();
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private void ItemCode_Production_Ellipsis(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            if (edit == null) return;

            var MasterObj = MasterGridBindingSource.Current as TN_ORD1700;
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
            var MasterObj = MasterGridBindingSource.Current as TN_ORD1700;
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
