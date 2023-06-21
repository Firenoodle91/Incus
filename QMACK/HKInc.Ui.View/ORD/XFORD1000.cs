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


namespace HKInc.Ui.View.ORD
{
    public partial class XFORD1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");
        IService<TN_ORD1002> ModelService2 = (IService<TN_ORD1002>)ProductionFactory.GetDomainService("TN_ORD1002");
        public XFORD1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dateOrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dateOrderDate.DateToEdit.DateTime = DateTime.Today;
            MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;
            string aa = gv.GetFocusedRowCellValue(gv.Columns["Temp2"]).GetNullToEmpty();
            if (aa != "")
            {
                MessageBox.Show("작업지시를 일괄생성한 수주는 선택할수 없습니다.");
                e.Cancel = true;
            }

        }

        private void MainView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;

                //PopupDataParam param = new PopupDataParam();
                //param.SetValue(PopupParameter.Service, ModelService);
                //param.SetValue(PopupParameter.KeyValue, obj);
                //param.SetValue(PopupParameter.UserRight, UserRight);
                //param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);

                //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFOrd1000, param, PopupRefreshCallback);

                //form.ShowPopup(true);
                //if (!UserRight.HasEdit) return;

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Service, ModelService);
                param.SetValue(PopupParameter.KeyValue, obj);
                param.SetValue(PopupParameter.UserRight, UserRight);
                param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);

                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, PopupRefreshCallback);

                form.ShowPopup(true);
            }
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("_Check", "선택");
            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호");
            MasterGridExControl.MainGrid.AddColumn("OrderDate", "수주일");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "수주처");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400.CustomerName", "수주처");
            MasterGridExControl.MainGrid.AddColumn("CustOrderid", "고객사담당자");
            MasterGridExControl.MainGrid.AddColumn("PeriodDate", "납기일");
            MasterGridExControl.MainGrid.AddColumn("OrderId", "영업담당자");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.AddColumn("Temp2", "비고");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("OrderNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");

            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            DetailGridExControl.MainGrid.AddColumn("OrderQty", "수량");
            DetailGridExControl.MainGrid.AddColumn("Cost", "단가");
            DetailGridExControl.MainGrid.AddColumn("Amt", "금액");
            DetailGridExControl.MainGrid.AddColumn("PeriodDate", "납기일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.AddColumn("Temp2", "PO");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderQty", "ItemCode", "Cost", "Temp2", "Memo");

        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
          //  DetailGridExControl.MainGrid.SetRepositoryItemMemoEdit("Memo");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }
        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OrderNo");
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string customerCode = lupCustcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OrderDate >= dateOrderDate.DateFrEdit.DateTime &&
                                                                                  p.OrderDate <= dateOrderDate.DateToEdit.DateTime) &&
                                                                                  (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode))
                                                                   .OrderBy(p => p.OrderNo)
                                                                   .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

            LoadDetail();

            IsFormControlChanged = false;
        }
        protected override void MasterFocusedRowChanged()
        {
            LoadDetail();
        }
        private void LoadDetail()
        {
            try
            {
                DetailGridExControl.MainGrid.Clear();
                ModelService2.ReLoad();
                TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;
                if (obj == null) return;

                DetailGridBindingSource.DataSource = ModelService2.GetList(p=>p.OrderNo==obj.OrderNo).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
            }
            finally
            {
                SetRefreshMessage("OrderList", MasterGridExControl.MainGrid.RecordCount,
                                  "DetailList", DetailGridExControl.MainGrid.RecordCount);
            }
        }
        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            ModelService.Save();
            ModelService2.Save();

            DataLoad();
        }
        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1,"양산");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, PopupRefreshCallback);

            form.ShowPopup(true);
        }
        protected override void DeleteRow()
        {
            TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;

            if (obj != null)
            {
                if (ModelService2.GetList(p=>p.OrderNo==obj.OrderNo).ToList().Count > 0)
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show(MessageHelper.GetStandardMessage(10), HKInc.Service.Factory.HelperFactory.GetLabelConvert().GetLabelText("Confirm"));
                    return;
                }
                ModelService.Delete(obj);
                MasterGridBindingSource.RemoveCurrent();
            }
        }
        protected override void DetailAddRowClicked()
        {
            TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;
            if (obj == null) return;

            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            param.SetValue(PopupParameter.KeyValue, obj.CustomerCode);
            param.SetValue(PopupParameter.Value_1, "");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTSTD1100, param, AddOrderList);
            form.ShowPopup(true);
        }

        private void AddOrderList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            TN_ORD1000 OrderMaster = (TN_ORD1000)MasterGridBindingSource.Current;
            List<TN_STD1100> partList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {
                if (ModelService2.GetList(p =>p.OrderNo==OrderMaster.OrderNo&& p.ItemCode == returnedPart.ItemCode).Count==0)
                {
                    TN_ORD1002 obj = (TN_ORD1002)DetailGridBindingSource.AddNew();
                    TN_STD1100 item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnedPart.ItemCode).FirstOrDefault();
                    List<TN_ORD1002> oldobj= DetailGridBindingSource.DataSource as List<TN_ORD1002>;
                    int iseq = oldobj.OrderByDescending(o => o.Seq).FirstOrDefault().Seq + 1;
                    obj.OrderNo = OrderMaster.OrderNo;
                    obj.Seq = iseq;
                    obj.ItemCode = item.ItemCode;
                      obj.PeriodDate = OrderMaster.PeriodDate;
                    obj.Temp = OrderMaster.CustomerCode;


                    ModelService2.Insert(obj);
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            ModelService2.Save();
            LoadDetail();
            //DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD1002 obj = DetailGridBindingSource.Current as TN_ORD1002;

            if (obj != null)
            {
              
             ModelService2.Delete(obj);

                DetailGridBindingSource.RemoveCurrent();
            }
        }
        protected override void DetailFileChooseClicked()
        {
            TN_ORD1000 OrderMaster = (TN_ORD1000)MasterGridBindingSource.Current;
            if (OrderMaster == null) return;
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            DataTable dt = HKInc.Service.Helper.ExcelImport.GetTableFromExcel(dlg.FileName.ToString(), 0);
            if (dt.Columns[0].ColumnName != "SEQ"
               || dt.Columns[1].ColumnName != "ITEM_CODE"
               || dt.Columns[3].ColumnName != "COST"
               || dt.Columns[4].ColumnName != "ORDER_QTY"
               || dt.Columns[5].ColumnName != "PERIOD_DATE"
               || dt.Columns[6].ColumnName != "MEMO"
               || dt.Columns[7].ColumnName != "PONO")
            {
                MessageBox.Show("파일형태가 잘못되었습니다.");
                return;
            }



            foreach (DataRow dtrow in dt.Rows)
            {


                TN_ORD1002 row = new TN_ORD1002();


                row.OrderNo = OrderMaster.OrderNo;
                
                row.Seq = Convert.ToInt32( dtrow[0].GetNullToZero());
                row.ItemCode = dtrow[1].GetNullToEmpty().Trim();
                row.Cost = dtrow[3].GetNullToZero();
                row.OrderQty = dtrow[4].GetIntNullToZero();
                row.PeriodDate = dtrow[5].GetNullToEmpty()==""?DateTime.Today:Convert.ToDateTime(dtrow[5].GetNullToEmpty());
                row.Memo = dtrow[6].GetNullToEmpty();
                row.Temp2 = dtrow[7].GetNullToEmpty();



                DetailGridBindingSource.Add(row);
                ModelService2.Insert(row);
                //.Insert(row);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DetailGridBindingSource == null) return;
            var PrintList = MasterGridBindingSource.List as List<TN_ORD1000>;
            if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;


            foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
            {
                int r = DbRequestHandler.GetCellValue("select count(*) from   [TN_ORD1100T] where ORDER_NO = '" + v.OrderNo + "'",0).GetIntNullToZero();
                if (r >= 1) {
                    MessageBox.Show("수주번호  " + v.OrderNo + "는 이미 납품계획이 수립되었습니다.");
                }
                else
                {
                    int i = DbRequestHandler.SetDataQury("exec batchord '" + v.OrderNo + "'");

                    v._Check = "N";
                    v.Temp2 = "작업지시생성완료";
                }
            }
            MessageBox.Show("처리완료");
            ModelService.Save();
            MasterGridExControl.MainGrid.MainView.RefreshData();
        }
    }
}
