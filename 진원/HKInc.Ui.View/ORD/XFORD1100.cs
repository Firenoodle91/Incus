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
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.DataAccess.Excel;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.ORD
{
 
    public partial class XFORD1100 : HKInc.Service.Base.ListMasterDetailDetailFormTemplate
    {
        IService<TN_ORD1100> ModelService = (IService<TN_ORD1100>)ProductionFactory.GetDomainService("TN_ORD1100");
        IService<TN_ORD1000> ModelService1 = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");
        IService<TN_ORD1002> ModelService2 = (IService<TN_ORD1002>)ProductionFactory.GetDomainService("TN_ORD1002");
        public XFORD1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today.AddDays(+30);

            SubDetailGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }


        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_ORD1100 obj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (e.Column.Name.ToString() == "ProdYn")
            {
                int cnt = DbRequesHandler.GetCellValue("SELECT COUNT(*)  FROM[TN_MPS1300T] where DELIV_SEQ = '" + obj.DelivSeq + "' and WORKORDER_YN = 'Y'", 0).GetIntNullToZero();
                if (cnt >= 1) { MessageBox.Show("작업지시가 내려간 판매계획은 생산의뢰를 수정 할 수 없습니다.");
                    obj.ProdYn = "Y";
                    return; }
            }
        }

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {

        
                object ProdYn = View.GetRowCellValue(e.RowHandle, View.Columns["ProdYn"]);
                object OutConfirmflag = View.GetRowCellValue(e.RowHandle, View.Columns["OutConfirmflag"]).GetNullToEmpty();

                if (e.Column.Name.ToString() == "ProdYn")
                {
                    if (ProdYn.ToString() == "Y")
                    {
                        e.Appearance.BackColor = Color.Blue;
                        e.Appearance.ForeColor = Color.White;

                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
                if (e.Column.Name.ToString() == "OutConfirmflag") { 
                    if (OutConfirmflag.ToString() == "Y")
                    {
                        e.Appearance.BackColor = Color.Blue;
                        e.Appearance.ForeColor = Color.White;

                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }

        }

        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
           // lupitemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(p => p.ItemNm1).ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("OrderDate"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("OrderCust"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustOrderNo", LabelConvert.GetLabelText("CustOrderNo"), HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("CustOrderid", LabelConvert.GetLabelText("VanEmp"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("PeriodDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("EmpCode"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo1"));

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderDate" , "CustOrderid", "PeriodDate", "OrderId", "Memo");
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "불러오기");
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), HorzAlignment.Center, false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("No"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("차종"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Lctype", LabelConvert.GetLabelText("기종"), HorzAlignment.Center, false);
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Temp5", LabelConvert.GetLabelText("팀"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", LabelConvert.GetLabelText("품번"), HorzAlignment.Center, true);
            //DetailGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            //DetailGridExControl.MainGrid.AddColumn("ItemNm1", LabelConvert.GetLabelText("품번"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("Qty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            
            DetailGridExControl.MainGrid.AddColumn("Cost", "단가", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Amt", "금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("VI_PRODQTY_MST.Stockqty", "재고량", HorzAlignment.Far, FormatType.Numeric, "N0");
            DetailGridExControl.MainGrid.AddColumn("PeriodDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"), HorzAlignment.Center, true);

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderQty", "Cost", "PeriodDate");
            DetailGridExControl.BestFitColumns();

            SubDetailGridExControl.MainGrid.Init();
            //   IsSubDetailGridButtonExportEnabled = true;
            SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", LabelConvert.GetLabelText("품번"), HorzAlignment.Center, true);            
            SubDetailGridExControl.MainGrid.AddColumn("DelivDate", LabelConvert.GetLabelText("DelivPlanDate"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("DelivQty", LabelConvert.GetLabelText("DelivPlanQty"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            SubDetailGridExControl.MainGrid.AddColumn("DelivId", LabelConvert.GetLabelText("DelivId"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("ProdYn", "생산의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("OutConfirmflag", "출고의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DelivDate", "DelivQty", "ProdYn", "OutConfirmflag", "Memo");

            SubDetailGridExControl.BestFitColumns();
        }
        protected override void InitRepository()
        {

         
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OrderDate");       
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag != "N"), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            //    DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PeriodDate");

            
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("DelivQty", 0, DefaultBoolean.Default, MaskType.Numeric, "n0", true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProdYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutConfirmflag", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
          
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            DetailGridRowLocator.GetCurrentRow("Seq", PopupDataParam.GetValue(PopupParameter.GridRowId_2));
            SubDetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_3));
            

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_2, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_3, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            ModelService1.ReLoad(); ModelService2.ReLoad();

            string CustCode = lupCustcode.EditValue.GetNullToEmpty();
            string Itemcode = tx_item.EditValue.GetNullToEmpty();
            if (Itemcode == "")
            {
                MasterGridBindingSource.DataSource = ModelService1.GetList(p => (p.OrderType == "양산") && (string.IsNullOrEmpty(CustCode) ? true : p.CustomerCode == CustCode) &&


                                                                       (p.OrderDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                        p.OrderDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                ).GroupBy(g => g.OrderNo).Select(s => s.First()).ToList();
            }
            else
            {
                MasterGridBindingSource.DataSource = ModelService.GetChildList<VI_ORD1100>(p => (p.OrderType == "양산") && (string.IsNullOrEmpty(CustCode) ? true : p.CustomerCode == CustCode) &&

                                                                               (string.IsNullOrEmpty(Itemcode) ? true : (p.ItemCode.Contains(Itemcode) || p.TN_STD1100.ItemNm.Contains(Itemcode) || p.TN_STD1100.ItemNm1.Contains(Itemcode))) &&
                                                                               (p.OrderDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                                p.OrderDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                        ).GroupBy(g => g.OrderNo).Select(s => s.First()).ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

        }
        protected override void MasterFocusedRowChanged()
        {
            string itemcode = tx_item.EditValue.GetNullToEmpty();
            if (itemcode == "")
            {
                TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;
                DetailGridExControl.MainGrid.Clear();
                SubDetailGridExControl.MainGrid.Clear();
                if (obj == null) return;
                DetailGridBindingSource.DataSource = ModelService2.GetList(p => p.OrderNo == obj.OrderNo).OrderBy(o => o.Seq).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.MainGrid.BestFitColumns();
                DetailGridRowLocator.SetCurrentRow();

                SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
            }
            else
            {
                VI_ORD1100 obj1 = MasterGridBindingSource.Current as VI_ORD1100;
                DetailGridExControl.MainGrid.Clear();
                SubDetailGridExControl.MainGrid.Clear();
                if (obj1 == null) return;
                DetailGridBindingSource.DataSource = ModelService2.GetList(p => p.OrderNo == obj1.OrderNo&&(string.IsNullOrEmpty(itemcode) ? true : (p.ItemCode.Contains(itemcode) || p.TN_STD1100.ItemNm.Contains(itemcode) || p.TN_STD1100.ItemNm1.Contains(itemcode)))).OrderBy(o => o.Seq).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.MainGrid.BestFitColumns();

                SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
            }
        }
        protected override void DetailFocusedRowChanged()
        {
            TN_ORD1002 obj = DetailGridBindingSource.Current as TN_ORD1002;
            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.DataSource = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == obj.OrderNo&&p.Seq==obj.Seq).OrderBy(o => o.DelivSeq).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
            SubDetailGridRowLocator.SetCurrentRow();

            SetRefreshMessage(SubDetailGridExControl.MainGrid.RecordCount);
        }
        protected override void SubDetailAddRowClicked()
        {
            if (!UserRight.HasEdit) return;
            var Dobj = DetailGridBindingSource.Current as TN_ORD1002;
            if (Dobj == null) return;
            TN_ORD1100 obj = new TN_ORD1100()
            {
                DelivSeq = DbRequesHandler.GetRequestNumber("DLV"),
                OrderNo = Dobj.OrderNo,
                Seq = Dobj.Seq,
                ItemCode=Dobj.ItemCode,
                DelivDate = DateTime.Today,
                DelivQty = Dobj.OrderQty.GetIntNullToZero(),
                DelivId = Utils.Common.GlobalVariable.LoginId,
                ProdYn = "Y",
                OutConfirmflag = "Y",
                Temp=Dobj.Temp,
                Memo=Dobj.Memo

            };

            //외주가공품은 생산을 안함. => 발주 입고 =>출고
            if (Dobj.TN_STD1100.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT)
                obj.ProdYn = "N";
          
            SubDetailGridBindingSource.Add(obj);
            ModelService.Insert(obj);
            SubDetailGridExControl.MainGrid.BestFitColumns();
            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_3, obj);
            #endregion
        }
        protected override void DeleteSubDetailRow()
        {
            if (!UserRight.HasEdit) return;
            var Dobj = DetailGridBindingSource.Current as TN_ORD1002;
            if (Dobj == null) return;

            var obj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (obj != null)
            {
                int cnt = DbRequesHandler.GetCellValue("SELECT COUNT(*)  FROM[TN_MPS1300T] where DELIV_SEQ = '"+obj.DelivSeq+"' and WORKORDER_YN = 'Y'", 0).GetIntNullToZero();
                if (cnt >= 1) { MessageBox.Show("작업지시가 내려간 판매계획은 삭제할수 없습니다."); return; }


                if (Dobj.TN_STD1100.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT)
                {
                    int cnt2 = DbRequesHandler.GetCellValue("SELECT COUNT(*) FROM[TN_PUR2100T] WHERE DELIV_SEQ = '" + obj.DelivSeq + "'", 0).GetIntNullToZero();
                    if (cnt2 >= 1)
                    {
                        MessageBox.Show("외주가공품 발주가 내려간 판매계획은 삭제할 수 없습니다.");
                        return;
                    }
                }

                ModelService.Delete(obj);
                SubDetailGridBindingSource.RemoveCurrent();
            }
        }
        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();
            ModelService1.Save();
            ModelService2.Save();
            ModelService.Save();
            DataLoad();
    }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService1);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1, "양산");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, PopupRefreshCallback);

            form.ShowPopup(true);
            //GridRowLocator.SetCurrentRow();
            //GridRowLocator.GetCurrentRow("OrderNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
        }
        protected override void DeleteRow()
        {
            TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;

            if (obj != null)
            {
                if (ModelService2.GetList(p => p.OrderNo == obj.OrderNo).ToList().Count > 0)
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show(MessageHelper.GetStandardMessage(10), HKInc.Service.Factory.HelperFactory.GetLabelConvert().GetLabelText("Confirm"));
                    return;
                }
                TN_ORD1000 nobj = ModelService1.GetList(p => p.OrderNo == obj.OrderNo).OrderBy(o => o.OrderNo).FirstOrDefault();
                ModelService1.Delete(nobj);
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
                if (!(returnedPart.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT || returnedPart.TopCategory == MasterCodeSTR.ITEM_TYPE_WAN))
                {
                    MessageBox.Show("완제품 및 외주가공품만 추가 됩니다.");
                    continue;
                }


                if (ModelService2.GetList(p => p.OrderNo == OrderMaster.OrderNo && p.ItemCode == returnedPart.ItemCode).Count == 0)
                {
                    TN_ORD1002 obj = (TN_ORD1002)DetailGridBindingSource.AddNew();
                    TN_STD1100 item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnedPart.ItemCode).FirstOrDefault();
                    List<TN_ORD1002> oldobj = DetailGridBindingSource.DataSource as List<TN_ORD1002>;
                    int iseq = oldobj.OrderByDescending(o => o.Seq).FirstOrDefault().Seq + 1;
                 //   obj.TN_STD1100 = item as TN_STD1100;
                    obj.OrderNo = OrderMaster.OrderNo;
                    obj.Seq = iseq;
                    obj.ItemCode = item.ItemCode;
                    obj.PeriodDate = OrderMaster.PeriodDate;
                    //obj.ItemNm = item.ItemNm;
                    //obj.ItemNm1 = item.ItemNm1;
                    obj.Temp = OrderMaster.CustomerCode;
                   

                    ModelService2.Insert(obj);
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            //ModelService2.Save();
            DataSave();
            ////    LoadDetail();
            //    DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD1002 obj = DetailGridBindingSource.Current as TN_ORD1002;
            if(SubDetailGridExControl.MainGrid.MainView.RowCount>=1)
            {
                MessageBox.Show("납품계획이 작성된  판매계획은 삭제할수 없습니다."); return;
            }
            if (obj != null)
            {
                try
                {
                    DetailGridBindingSource.RemoveCurrent();
                }
                catch { }
                ModelService2.Delete(obj);

             
            }
        }
        protected override void DetailFileChooseClicked()
        {
            TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;
            if (obj == null) return;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "xlsx파일(*.xlsx)|*.xlsx|xls파일(*.xls)|*.xls";
            openFileDialog1.Title = "엑셀 저장";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog1.FileName != string.Empty)
                    {

                     
                    DataTable dt = HKInc.Service.Helper.ExcelImport.GetTableFromExcel(openFileDialog1.FileName.ToString(), 0);

                        foreach (DataRow r in dt.Rows)
                        {
                            string itemcode = r[5].GetNullToEmpty();
                            var item = ModelService.GetChildList<TN_STD1100>(p => p.CustItemCode == itemcode).FirstOrDefault();
                            if (item == null) {
                                MessageBox.Show("품번이 존재하지 않습니다");
                                return;

                            }
                            TN_ORD1002 newobj = new TN_ORD1002();
                            newobj.OrderNo = obj.OrderNo;
                            newobj.Seq = r[0].GetIntNullToZero();
                            newobj.ItemCode = item.ItemCode;
                            newobj.OrderQty = r[9].GetNullToZero();
                            newobj.Cost = r[10].GetNullToZero();
                            newobj.Memo = r[13].ToString();
                            newobj.PeriodDate = obj.PeriodDate;
                            ModelService2.Insert(newobj);
                            DetailGridBindingSource.Add(newobj);
                        }
                        DataSave();
                    }
                }
                catch (Exception ex)
                {
                 //   MessageBox.Show("엑셀 파일 드라이버가 잘못되었거나 엑셀파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }



            }

        }
    }
}
