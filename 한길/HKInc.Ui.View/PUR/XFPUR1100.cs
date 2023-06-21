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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.PUR
{
    /// <summary>
    /// 자재발주관리화면
    /// </summary>
    public partial class XFPUR1100 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1100> ModelService = (IService<TN_PUR1100>)ProductionFactory.GetDomainService("TN_PUR1100");
        public XFPUR1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DatailMainView_CellValueChanged;
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
        }

        protected override void InitCombo()
        {
            lupUser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주서출력[F10]", IconImageList.GetIconImage("print/printer"));
            MasterGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("ReqDate", "발주일");            
            MasterGridExControl.MainGrid.AddColumn("DueDate", "납기예정일");
            MasterGridExControl.MainGrid.AddColumn("ReqId", "발주자");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("Temp2","발주확정");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReqDate", "DueDate", "ReqId", "CustomerCode", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("ReqNo",false);
            DetailGridExControl.MainGrid.AddColumn("ReqSeq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("ReqQty", "발주수량");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "단가");
            DetailGridExControl.MainGrid.AddColumn("Amt", "금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "ReqQty", "Temp1", "Memo");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "ItemCode", "ReqQty");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqId", ModelService.GetChildList<UserView>(p=>p.Active=="Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("DueDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp2", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y" && (p.TopCategory=="P03" || p.TopCategory=="P02")).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1", false, true, ItemCode_NotProduction_Ellipsis);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ReqQty", DefaultBoolean.Default, "n0", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Temp1", DefaultBoolean.Default, "n0", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ReqNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string luser = lupUser.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ReqDate >= dp_date.DateFrEdit.DateTime.Date && p.ReqDate <= dp_date.DateToEdit.DateTime.Date)
                                                                       && (string.IsNullOrEmpty(luser) ? true : p.ReqId == luser)).OrderBy(o => o.ReqDate).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.PUR1200List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            TN_PUR1100 newobj = new TN_PUR1100()
            {
                ReqNo = DbRequesHandler.GetRequestNumber("PUR"),
                ReqDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(20)
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
        }

        protected override void FileChooseClicked()
        {
            TN_PUR1100 masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;
            if (DetailGridBindingSource == null) return;
            var listCount = masterObj.PUR1200List.Count;
            if (listCount == 0) return;

            var valueCount = listCount / 10;
            var modCount = listCount % 10;

            if (modCount == 0)
            {
                ReportCreateToPrint(masterObj);
            }
            else
            {
                var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * 10;
                while (true)
                {
                    masterObj.PUR1200List.Add(new TN_PUR1200()
                    {
                        ReqNo = masterObj.ReqNo,
                        ReqSeq = -1
                    });
                    if (masterObj.PUR1200List.Count == checkCount) break;
                }
                ReportCreateToPrint(masterObj);
            }            
        }

        protected override void DetailAddRowClicked()
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            if (obj.Temp2 == "Y")
            {
                MessageBoxHandler.Show("발주 확정 건은 추가할 수 없습니다.", "경고");
                return;
            }
            
            TN_PUR1200 newobj = new TN_PUR1200()
            {
                ReqNo = obj.ReqNo,
                ReqSeq = (obj.PUR1200List.Count == 0) ? 1 : obj.PUR1200List.OrderBy(o => o.ReqSeq).LastOrDefault().ReqSeq + 1
                //obj.PUR1200List.Count + 1
            };
            DetailGridBindingSource.Add(newobj);
            obj.PUR1200List.Add(newobj);
        }

        protected override void DeleteRow()
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            if (obj.PUR1200List.Count >= 1) {
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다");

            }
            else
            {
                if (obj.Temp2 == "Y")
                {
                    MessageBox.Show("발주확정건은 삭제할수 없습니다.");

                }
                else
                {
                    MasterGridBindingSource.RemoveCurrent();
                    ModelService.Delete(obj);
                }
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            if (obj.Temp2 == "Y")
            { MessageBoxHandler.Show("발주 확정 건은 삭제할 수 없습니다.", "경고"); }
            else {
                TN_PUR1200 dtlobj = DetailGridBindingSource.Current as TN_PUR1200;
                DetailGridBindingSource.RemoveCurrent();
                obj.PUR1200List.Remove(dtlobj);
                //ModelService.Update(obj);
            }
            
        }
        
        private void ItemCode_NotProduction_Ellipsis(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            if (edit == null) return;

            var DetailObj = DetailGridBindingSource.Current as TN_PUR1200;
            if (DetailObj == null) return;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                PopupDataParam param = new PopupDataParam();
                IPopupForm form;
                param.SetValue(PopupParameter.IsMultiSelect, false);
                param.SetValue(PopupParameter.Constraint, MasterCodeSTR.ItemCode_NotProduction);
                form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTSTD1100, param, SetDetailGridView_ItemCode);
                form.ShowPopup(true);
            }
        }

        private void SetDetailGridView_ItemCode(object sender, Utils.Common.PopupArgument e)
        {
            var DetailObj = DetailGridBindingSource.Current as TN_PUR1200;
            if (DetailObj == null) return;

            if (e != null)
            {
                PopupDataParam param = e.Map;
                if (param.Keys.Contains(PopupParameter.ReturnObject))
                {
                    var obj = param.GetValue(PopupParameter.ReturnObject) as TN_STD1100;
                    if (obj != null)
                    {
                        DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("ItemCode", obj.ItemCode);
                    }
                }
            }
        }

        private void DatailMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.FieldName.ToString() == "ItemCode")
            {
                string itm = gv.GetFocusedRowCellValue(e.Column.FieldName).ToString();
                int rowid = e.RowHandle;
                int cnt = 0;
                for (int i = 0; i < gv.RowCount; i++)
                {
                    if (i != rowid)
                    {
                        if (itm == gv.GetRowCellValue(i, e.Column.FieldName).GetNullToEmpty())
                        {
                            cnt++;
                        }
                    }
                }
                if (cnt >= 1)
                {
                    TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
                    MessageBox.Show("이미 등록된 품목입니다.");
                    TN_PUR1200 delobj = DetailGridBindingSource.Current as TN_PUR1200;
                    if (delobj.ReqQty >= 1) return;
                    DetailGridBindingSource.RemoveCurrent();
                    obj.PUR1200List.Remove(delobj);
                    //ModelService.Update(obj);
                }
                else
                {
                    var DetailObj = DetailGridBindingSource.Current as TN_PUR1200;
                    DetailObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itm).First();
                    DetailGridExControl.MainGrid.MainView.RefreshData();
                    DetailGridExControl.BestFitColumns();
                }
            }
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;

            if (obj.Temp2 == "Y")
            {
                e.Cancel = true;
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj.Temp2 == "Y")
            {
                e.Cancel = true;
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //발주일에 따라 납기일 자동 갱신
            GridView gv = sender as GridView;
            if (e.Column.Name.ToString() == "ReqDate")
            {
                DateTime dt = Convert.ToDateTime(gv.GetFocusedRowCellValue(gv.Columns["ReqDate"]).ToString());
                gv.SetFocusedRowCellValue(gv.Columns["DueDate"], dt.AddDays(10));
            }

        }

        // 발주취소 이벤트
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            obj.Temp2 = "N";
            MasterGridExControl.MainGrid.BestFitColumns();
            //ModelService.Update(obj);

        }

        // 발주확정 이벤트
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            obj.Temp2 = "Y";
            MasterGridExControl.MainGrid.BestFitColumns();
            //ModelService.Update(obj);
        }

        private void ReportCreateToPrint(TN_PUR1100 masterObj)
        {
            try
            {
                WaitHandler.ShowWait();
                var poUserObj = ModelService.GetChildList<UserView>(p => p.LoginId == masterObj.ReqId).FirstOrDefault();
                var poUserName = poUserObj == null ? string.Empty : poUserObj.UserName;
                var poCustomer = ModelService.GetChildList<TN_STD1400>(p => p.DefaultCompanyPlag == "Y").FirstOrDefault();
                var report = new REPORT.RPUR1100(masterObj, poUserName, poCustomer, masterObj.TN_STD1400);
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                foreach (var v in masterObj.PUR1200List.Where(p => p.ReqSeq == -1).ToList())
                    masterObj.PUR1200List.Remove(v);
                WaitHandler.CloseWait();
            }
        }
    }
}
