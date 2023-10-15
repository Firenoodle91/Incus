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

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 반제품입고관리화면
    /// </summary>
    public partial class XFBAN1000 :  HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_BAN1000> ModelService = (IService<TN_BAN1000>)ProductionFactory.GetDomainService("TN_BAN1000");

        public XFBAN1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+10);
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("InputNo", "입고번호");
            MasterGridExControl.MainGrid.AddColumn("InputDate", "입고일");
            MasterGridExControl.MainGrid.AddColumn("InputId", "입고자");        
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("Temp1", "입고완료");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InputDate", "InputId", "Memo", "Temp1");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "현품표출력[Alt+R]", IconImageList.GetIconImage("print/printer"));
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("InputNo", false);
            DetailGridExControl.MainGrid.AddColumn("InputSeq", "입고순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("InputQty", "입고수량");
            DetailGridExControl.MainGrid.AddColumn("InYn", false);
            DetailGridExControl.MainGrid.AddColumn("Temp2", "입고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "ItemCode", "InputQty","Memo", "WhCode", "WhPosition");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InputId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");          
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InputDate");          
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp1", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InputQty");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory == "P05").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1", false, true, ItemCode_NotProduction_Ellipsis);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("InYn", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }
      
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InputNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string inputNo = tx_ReqNo.Text.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InputDate >= dp_date.DateFrEdit.DateTime.Date 
                                                                         && p.InputDate <= dp_date.DateToEdit.DateTime.Date)
                                                                         && (string.IsNullOrEmpty(inputNo) ? true : p.InputNo == inputNo)
                                                                     )
                                                                     .OrderBy(o => o.InputDate)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_BAN1000 obj = MasterGridBindingSource.Current as TN_BAN1000;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.BAN1001List.OrderBy(p => p.InputSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
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
            TN_BAN1000 newobj = new TN_BAN1000()
            {
                InputNo = DbRequesHandler.GetRequestNumber("BIN"),
                InputDate = DateTime.Today,
                Temp1 = "N"
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);            
        }

        protected override void DeleteRow()
        {
            TN_BAN1000 obj = MasterGridBindingSource.Current as TN_BAN1000;
            if (obj == null) return;
          
            if (obj.BAN1001List.Count>= 1)
            {
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다.");
            }
            else
            {
                if (obj.Temp1 == "Y")
                {
                    MessageBox.Show("입고완료 삭제할수 없습니다.");
                }
                else
                {
                    MasterGridBindingSource.RemoveCurrent();
                    ModelService.Delete(obj);
                }             
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_BAN1000 obj = MasterGridBindingSource.Current as TN_BAN1000;
            if (obj == null) return;

            if (obj.Temp1 == "Y")
            {
                MessageBox.Show("입고완료건은 변경할수 없습니다.");
            }
            else
            {
                TN_BAN1001 newobj = new TN_BAN1001()
                {
                    InputNo=obj.InputNo,
                    InputSeq= obj.BAN1001List.Count == 0? 1: obj.BAN1001List.Count + 1,                
                    Temp2= obj.InputNo.ToString()+ (obj.BAN1001List.Count == 0 ? 1 : obj.BAN1001List.Count + 1).ToString()
                };
                DetailGridBindingSource.Add(newobj);
                obj.BAN1001List.Add(newobj);            
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_BAN1000 obj = MasterGridBindingSource.Current as TN_BAN1000;
            TN_BAN1001 dtlobj = MasterGridBindingSource.Current as TN_BAN1001;
            if (obj == null || dtlobj == null) return;

            if (obj.Temp1 == "Y")
            {
                MessageBox.Show("발주확정건은 삭제할수 없습니다.");
            }
            else
            {
                DetailGridBindingSource.RemoveCurrent();
                obj.BAN1001List.Remove(dtlobj);
            }
        }

        protected override void DetailFileChooseClicked()
        {
            try
            {
                ModelService.Save();
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_BAN1001>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;

                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {

                    var report = new REPORT.RINPUTLABLE(v);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);

                    v._Check = "N";
                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();//.Print();
                //DataLoad();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
            if (e.Column.Name == "WhCode")
            {
                TN_BAN1001 dtlobj = DetailGridBindingSource.Current as TN_BAN1001;
                List<TN_WMS2000> wh = ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList();
                if (wh.Count >= 1)
                {
                    DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", wh, "PosionCode", "PosionName");
                }
                else
                {
                    wh.Add(new TN_WMS2000() { PosionCode = "", PosionName = "" });
                    DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", wh, "PosionCode", "PosionName");
                }
            }
            else if (e.Column.FieldName == "ItemCode")
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
                    TN_BAN1000 obj = MasterGridBindingSource.Current as TN_BAN1000;
                    MessageBox.Show("이미 등록된 품목입니다.");
                    TN_BAN1001 delobj = DetailGridBindingSource.Current as TN_BAN1001;
                    if (delobj.InputQty >= 1) return;
                    DetailGridBindingSource.RemoveCurrent();
                    obj.BAN1001List.Remove(delobj);
                    ModelService.Update(obj);
                }
                else
                {
                    var DetailObj = DetailGridBindingSource.Current as TN_BAN1001;
                    DetailObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itm).First();
                    DetailGridExControl.MainGrid.MainView.RefreshData();
                    DetailGridExControl.BestFitColumns();
                }
            }
        }
        private void ItemCode_NotProduction_Ellipsis(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            if (edit == null) return;

            var DetailObj = DetailGridBindingSource.Current as TN_BAN1001;
            if (DetailObj == null) return;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                PopupDataParam param = new PopupDataParam();
                IPopupForm form;
                param.SetValue(PopupParameter.IsMultiSelect, false);
                param.SetValue(PopupParameter.Constraint, MasterCodeSTR.ItemCode_BAN_Production);
                form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTSTD1100, param, SetDetailGridView_ItemCode);
                form.ShowPopup(true);
            }
        }

        private void SetDetailGridView_ItemCode(object sender, Utils.Common.PopupArgument e)
        {
            var DetailObj = DetailGridBindingSource.Current as TN_BAN1001;
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
    }
}

