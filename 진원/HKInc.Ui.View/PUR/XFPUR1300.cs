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
    public partial class XFPUR1300 :  HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1300> ModelService = (IService<TN_PUR1300>)ProductionFactory.GetDomainService("TN_PUR1300");
      //  IService<TN_PUR1301> DtlModelService = (IService<TN_PUR1301>)ProductionFactory.GetDomainService("TN_PUR1301");
        public XFPUR1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+10);
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GridView gv = sender as GridView;
            //string aa = DbRequesHandler.GetCellValue("SELECT top 1 [no] FROM [TN_QCT1200T] where  temp1='" + gv.GetFocusedRowCellValue("Temp2").GetNullToEmpty() + "'", 0);
            //if (aa != null)
            //{
            //    e.Cancel = true;
            //}
        




        }

    private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
    {
        GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
        //if (e.Column.Name == "WhCode")
        //{
        //    TN_PUR1301 dtlobj = DetailGridBindingSource.Current as TN_PUR1301;
        //    //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList(), "PosionCode", "PosionName");
        //    DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
        //}
    }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);            
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주내역참조");

            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.MainGrid.AddColumn("InputNo", "입고번호");
            MasterGridExControl.MainGrid.AddColumn("InputDate", "입고일");
            MasterGridExControl.MainGrid.AddColumn("InputId", "입고자");
            MasterGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("ReqDate", "발주일");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("Temp1", "입고완료");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InputDate", "InputId", "CustomerCode", "Memo", "Temp1");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주내역참조");
            IsDetailGridButtonFileChooseEnabled = true;

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "InputSeq", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("InputNo", false);
            DetailGridExControl.MainGrid.AddColumn("InputSeq", "입고순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            DetailGridExControl.MainGrid.AddColumn("ReqSeq", "발주순번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            DetailGridExControl.MainGrid.AddColumn("ReqQty", "발주수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Cost","발주단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("ReqAmt","발주금액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InputQty","입고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InCost","입고단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InputAmt","입고금액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Lqty", "라벨수", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InYn",false);
            DetailGridExControl.MainGrid.AddColumn("Temp2", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");
         //   DetailGridExControl.MainGrid.AddColumn("inqcf");




            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "ItemCode", "InputQty", "InCost", "Memo", "Temp2", "WhCode", "Lqty", "WhPosition");
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InputId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InputDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp1", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03" || p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03"||p.TopCategory=="P02" || p.TopCategory == "P07" || p.TopCategory == "P06")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("InYn", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");

            var WhPositionEdit = DetailGridExControl.MainGrid.Columns["WhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            WhPositionEdit.Popup += WhPositionEdit_Popup;
        }

        private void WhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_PUR1301;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.WhCode + "'";
        }

        protected override void InitCombo()
        {
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InputNo");
            string cust = lupcust.EditValue.GetNullToEmpty();
            string inputNo = tx_ReqNo.Text.GetNullToEmpty();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
         
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InputDate >= dp_date.DateFrEdit.DateTime.Date && p.InputDate <= dp_date.DateToEdit.DateTime.Date)
              && (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust) && (string.IsNullOrEmpty(inputNo) ? true : p.InputNo == inputNo)).OrderBy(o => o.InputDate).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();

        }
        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03" || p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
            DetailGridBindingSource.DataSource = obj.PUR1301List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
       
        }
        protected override void AddRowClicked()
        {
            TN_PUR1300 newobj = new TN_PUR1300()
            {
                InputNo = DbRequesHandler.GetRequestNumber("IN"),
                InputDate = DateTime.Today,
                Temp1 = "N"
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            
        }
        protected override void DeleteRow()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
          
            if (obj.PUR1301List.Count>= 1)
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
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
            if (obj.Temp1 == "Y")
            {
                MessageBox.Show("입고완료건은 변경할수 없습니다.");
            }
            else {

                //var cntdata = DtlModelService.GetList(p => p.InputNo == obj.InputNo).ToList();
                //int cnt = cntdata.Count;  
                if (obj.CustomerCode.GetNullToEmpty() == "")
                {
                    MessageBox.Show("거래처는 필수입니다.");
                    return;
                }
                List<TN_STD1100> itemlist = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03" || p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06") && p.MainCust == obj.CustomerCode).OrderBy(o => o.ItemNm).ToList();
                if (itemlist.Count() == 0)
                {
                    MessageBox.Show("해당 거래처에 등록된 품목이 없습니다.");
                    return;
                }
                DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03" || p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06")&&p.MainCust==obj.CustomerCode).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
                TN_PUR1301 newobj = new TN_PUR1301()
                {
                    InputNo=obj.InputNo,
                    InputSeq= obj.PUR1301List.Count == 0? 1: obj.PUR1301List.Count + 1,
                    ReqNo=obj.ReqNo,
                    Temp2= obj.InputNo.ToString()+ (obj.PUR1301List.Count == 0 ? 1 : obj.PUR1301List.Count + 1).ToString()
                   

                };
                DetailGridBindingSource.Add(newobj);
                obj.PUR1301List.Add(newobj);
            
            }
        }
        protected override void DeleteDetailRow()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
            if (obj.Temp1 == "Y")
            { MessageBox.Show("발주확정건은 삭제할수 없습니다."); }
            else
            {
                TN_PUR1301 dtlobj = DetailGridBindingSource.Current as TN_PUR1301;
                DetailGridBindingSource.RemoveCurrent();
                obj.PUR1301List.Remove(dtlobj);
            }
        }

        protected override void FileChooseClicked()
        {
            DataLoad();
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
           

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1100, param, AddPur1300);
            form.ShowPopup(true);
        }
        private void AddPur1300(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

          
            List<TN_PUR1100> partList = (List<TN_PUR1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {
                if (ModelService.GetList(p => p.ReqNo == returnedPart.ReqNo).Count == 0)
                {
                    TN_PUR1300 obj = (TN_PUR1300)MasterGridBindingSource.AddNew();


                    obj.InputNo = DbRequesHandler.GetRequestNumber("IN");
                    obj.InputDate = DateTime.Today;
                    obj.CustomerCode = returnedPart.CustomerCode;
                    obj.DueDate = returnedPart.DueDate;
                    obj.ReqNo = returnedPart.ReqNo;
                    obj.ReqDate = returnedPart.ReqDate;

                    ModelService.Insert(obj);
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailFileChooseClicked()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj.ReqNo.GetNullToEmpty() == "") return;
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            param.SetValue(PopupParameter.Value_1, obj.ReqNo);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1200, param, AddPur1301);
            form.ShowPopup(true);
        }
        private void AddPur1301(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;


            List<TN_PUR1200> partList = (List<TN_PUR1200>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_PUR1300 oldobj = MasterGridBindingSource.Current as TN_PUR1300;
            foreach (var returnedPart in partList)
            {
                if (oldobj.PUR1301List.Where(p => p.ItemCode == returnedPart.ItemCode).ToList().Count == 0)
                {

                    TN_PUR1301 obj = (TN_PUR1301)DetailGridBindingSource.AddNew();
                    obj.InputNo = oldobj.InputNo;
                    obj.InputSeq = oldobj.PUR1301List.Count == 0 ? 1 : oldobj.PUR1301List.OrderBy(o => o.InputSeq).LastOrDefault().InputSeq + 1;
                        //oldobj.PUR1301List.Count + 1;
                    obj.ItemCode = returnedPart.ItemCode;
                    obj.ReqNo = returnedPart.ReqNo;
                    obj.ReqSeq = returnedPart.ReqSeq;
                    obj.ReqQty = returnedPart.ReqQty;
                    obj.InputQty = returnedPart.ReqQty;
                    obj.InCost = returnedPart.Temp1;
                    obj.Cost = returnedPart.Temp1;                   
                    obj.Memo = returnedPart.Memo;
                    obj.Temp2 = oldobj.InputNo.ToString() + obj.InputSeq.ToString();
                    oldobj.PUR1301List.Add(obj);
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {

            for (int rowHandle = 0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++)
            {

                string inqty = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "InputQty").GetNullToEmpty());
                if (inqty == "0") { HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(40)); return; }
                if (inqty == "") {HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(40)); return;    }
                




            }

            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
         
            DataLoad();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int rowHandle = 0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++)
                {
                    string inqty = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "InputQty").GetNullToEmpty());
                    if (inqty == "0") { HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(40)); return; }
                    if (inqty == "") { HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(40)); return; }
                }
                ModelService.Save();
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_PUR1301>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;

                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {
                    
                    decimal iold = Convert.ToDecimal(v.InputQty);
                    int i = Convert.ToInt32(v.InputQty / (v.Lqty==0?1:v.Lqty));
                    decimal ii = Convert.ToDecimal( v.InputQty / (v.Lqty == 0 ? 1 : v.Lqty));
                    v.InputQty = ii;
                    for (int j = 0; j < (v.Lqty == 0 ? 1 : v.Lqty); j++)
                    {
                        var report = new REPORT.RINPUTLABLE(v);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                    v._Check = "N";
                    v.InputQty = iold;

                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();//.Print();
                //DataLoad();
           //     DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }
    }
}

