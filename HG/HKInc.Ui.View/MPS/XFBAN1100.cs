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
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 반제품출고등록화면
    /// </summary>
    public partial class XFBAN1100 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_BAN1200> ModelService = (IService<TN_BAN1200>)ProductionFactory.GetDomainService("TN_BAN1200");

        public XFBAN1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
         
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
            MasterGridExControl.MainGrid.MainView.CellValueChanged += MasterMainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;

        }

        protected override void InitCombo()
        {
            lupUser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
        }

        protected override void InitGrid()
        {
            //   
            MasterGridExControl.SetToolbarVisible(true);
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            MasterGridExControl.MainGrid.AddColumn("ProductItemcode", "생산품번");
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "생산품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "생산품명");
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDate", "ProductItemcode", "OutId", "Memo");

            //       
            DetailGridExControl.SetToolbarVisible(true);
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "현품표출력[Alt+R]", IconImageList.GetIconImage("print/printer"));
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "출고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("Temp2", "입고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check","OutQty", "Memo", "WhCode", "WhPosition");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p=>p.Active=="Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProductItemcode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1", false, true, ItemCode_Production_Ellipsis);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory == "P05").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string luser = lupUser.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dp_date.DateFrEdit.DateTime.Date 
                                                                         && p.OutDate <= dp_date.DateToEdit.DateTime.Date)
                                                                         && (string.IsNullOrEmpty(luser) ? true : p.OutId == luser)
                                                                      )
                                                                      .OrderBy(o => o.OutDate)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_BAN1200 obj = MasterGridBindingSource.Current as TN_BAN1200;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.BAN1201List.ToList();
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
            TN_BAN1200 newobj = new TN_BAN1200()
            {
                OutNo = DbRequesHandler.GetRequestNumber("BOUT"),
                OutDate = DateTime.Today
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
        }

        protected override void DetailAddRowClicked()
        {
            TN_BAN1200 obj = MasterGridBindingSource.Current as TN_BAN1200;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            IPopupForm form;
            param.SetValue(PopupParameter.IsMultiSelect, false);
            form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFBAN1100, param, AddDetailRowCallBack);
            form.ShowPopup(true);
        }

        private void AddDetailRowCallBack(object sender, Utils.Common.PopupArgument e)
        {
            TN_BAN1200 obj = MasterGridBindingSource.Current as TN_BAN1200;
            if (obj == null) return;

            if (e != null)
            {
                PopupDataParam param = e.Map;
                if (param.Keys.Contains(PopupParameter.ReturnObject))
                {
                    var returnObj = param.GetValue(PopupParameter.ReturnObject) as TN_BAN1001;
                    if (returnObj != null)
                    {
                        int seq = (obj.BAN1201List.Count == 0) ? 1 : Convert.ToInt32(obj.BAN1201List.OrderBy(o => o.OutSeq).LastOrDefault().OutSeq) + 1;
                        TN_BAN1201 newobj = new TN_BAN1201()
                        {
                            OutNo = obj.OutNo,
                            OutSeq = seq,
                            Temp1 = obj.OutNo + seq.ToString(),
                            ItemCode = returnObj.ItemCode,
                            Temp2 = returnObj.Temp2,
                            TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnObj.ItemCode).First()
                        };
                        DetailGridBindingSource.Add(newobj);
                        obj.BAN1201List.Add(newobj);
                        DetailGridExControl.BestFitColumns();
                    }
                }
            }
        }

        protected override void DeleteRow()
        {
            TN_BAN1200 obj = MasterGridBindingSource.Current as TN_BAN1200;
            if (obj == null) return;
            if (obj.BAN1201List.Count >= 1)
            {
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다");
            }
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(obj);              
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_BAN1200 obj = MasterGridBindingSource.Current as TN_BAN1200;
            TN_BAN1201 dtlobj = DetailGridBindingSource.Current as TN_BAN1201;
            if (obj == null || dtlobj == null) return;

            DetailGridBindingSource.RemoveCurrent();
            obj.BAN1201List.Remove(dtlobj);
        }

        protected override void DetailFileChooseClicked()
        {
            try
            {
                MasterGridExControl.MainGrid.PostEditor();
                MasterGridBindingSource.EndEdit();
                DetailGridExControl.MainGrid.PostEditor();
                DetailGridBindingSource.EndEdit();
                ModelService.Save();
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_BAN1201>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;
                ModelService.Save();
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

        private void MasterMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.Column.FieldName == "ProductItemcode")
            {
                var MasterObj = MasterGridBindingSource.Current as TN_BAN1200;
                if (MasterObj == null) return;
                MasterObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == MasterObj.ProductItemcode).First();
                MasterGridExControl.MainGrid.MainView.RefreshData();
                MasterGridExControl.BestFitColumns();
            }
        }

        private void DetailMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.Column.FieldName == "Temp2")
            {
                //TN_BAN1201 obji = DetailGridBindingSource.Current as TN_BAN1201;
                ////반제품 제고확인
                //VI_BANTOCK_MST_Lot stock = ModelService.GetChildList<VI_BANTOCK_MST_Lot>(p => p.Temp2 == obji.Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                //obji.ItemCode = stock.ItemCode;
            }
            else if(e.Column.FieldName == "OutQty")
            {
                TN_BAN1201 obj = DetailGridBindingSource.Current as TN_BAN1201;
                if (obj.Temp2 == null)
                {
                    obj.OutQty = 0;
                    MessageBox.Show("입고 LOT는 필수입력값입니다.");
                }
                else
                {
                    //  반제품 제고확인
                    VI_BANTOCK_MST_Lot stock = ModelService.GetChildList<VI_BANTOCK_MST_Lot>(p => p.ItemCode == obj.ItemCode && p.Temp2 == obj.Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                    if (stock == null)
                    {
                        MessageBox.Show("해당 품고에 대한 입고LOT " + obj.Temp2 + "정보가 없습니다.");
                        obj.Temp2 = null;
                        obj.OutQty = null;
                    }
                    else
                    {
                        if (stock.StockQty < obj.OutQty)
                        {
                            MessageBox.Show("재고량이 부족하여 출고할수 없습니다.");

                            obj.OutQty = stock.StockQty <= 0 ? 0 : stock.StockQty;

                        }
                        else
                        {
                            obj.WhCode = stock.WhCode;
                            obj.WhPosition = stock.WhPosition;
                        }
                    }

                }
            }
            else if(e.Column.FieldName == "WhCode")
            {
                TN_BAN1201 dtlobj = DetailGridBindingSource.Current as TN_BAN1201;
                DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList(), "PosionCode", "PosionName");
                DetailGridExControl.MainGrid.BestFitColumns();
            }
            else if (e.Column.FieldName == "_Check")
                SetIsFormControlChanged(false);
        }

        private void ItemCode_Production_Ellipsis(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            if (edit == null) return;

            var MasterObj = MasterGridBindingSource.Current as TN_BAN1200;
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
            var MasterObj = MasterGridBindingSource.Current as TN_BAN1200;
            if (MasterObj == null) return;

            if (e != null)
            {
                PopupDataParam param = e.Map;
                if (param.Keys.Contains(PopupParameter.ReturnObject))
                {
                    var obj = param.GetValue(PopupParameter.ReturnObject) as TN_STD1100;
                    if (obj != null)
                    {
                        MasterGridExControl.MainGrid.MainView.SetFocusedRowCellValue("ProductItemcode", obj.ItemCode);
                    }
                }
            }
        }

    }
   
}
