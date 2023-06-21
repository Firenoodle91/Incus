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
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.PUR
{
    //외주입고관리화면
    public partial class XFPUR1800 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1800> ModelService = (IService<TN_PUR1800>)ProductionFactory.GetDomainService("TN_PUR1800");
        public XFPUR1800()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;
        }

        private void DetailView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "InQty")
            {
              
                TN_PUR1801 obj = DetailGridBindingSource.Current as TN_PUR1801;
                if (obj.InQty == 0) obj.InSre = "40";
                if (obj.InQty != 0 && obj.InQty < obj.PoQty) obj.InSre = "41";
                if (obj.InQty != 0 && obj.InQty >= obj.PoQty) obj.InSre = "42";
                TN_PUR1800 hobj = MasterGridBindingSource.Current as TN_PUR1800;
                GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
                int i41cnt = 0;
                int i42cnt = 0;
                int i40cnt = 0;
                int rowcont = gv.RowCount;
                for (int i = 0; i < gv.RowCount; i++)
                {
                    switch (gv.GetRowCellValue(i, gv.Columns["InSre"]).ToString())
                    {
                        case "40": i40cnt++; break;
                        case "41": i41cnt++; break;
                        case "42": i42cnt++; break;
                    }
                }
                if (i42cnt == rowcont) { hobj.InSre = "42"; }
                else if (i41cnt >= 1 && i41cnt <= rowcont) { hobj.InSre = "41"; }
                else if (i40cnt < rowcont) { hobj.InSre = "41"; }
                else { hobj.InSre = "40"; }
                MasterGridExControl.MainGrid.BestFitColumns();

            }
            if (e.Column.Name == "InSre")
            {
                TN_PUR1800 hobj = MasterGridBindingSource.Current as TN_PUR1800;
                GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
                int i41cnt = 0;
                int i42cnt = 0;
                int i40cnt = 0;
                int rowcont = gv.RowCount;
                for (int i = 0; i < gv.RowCount; i++)
                {
                    switch (gv.GetRowCellValue(i, gv.Columns["InSre"]).ToString())
                    {
                        case "40": i40cnt++; break;
                        case "41": i41cnt++; break;
                        case "42": i42cnt++; break;
                    }
                }
                if (i42cnt == rowcont) { hobj.InSre = "42"; }
                else if (i41cnt >= 1 && i41cnt <= rowcont) { hobj.InSre = "41"; }
                else if(i40cnt<rowcont) { hobj.InSre = "41"; }
                else { hobj.InSre = "40"; }
                MasterGridExControl.MainGrid.BestFitColumns();
            }
           
        }

        protected override void InitCombo()
        {

            lupcustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(p => p.CustomerName).ToList());
        }
        protected override void InitGrid()
        {
      

            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("InNo", "입고번호", HorzAlignment.Center,true);
            MasterGridExControl.MainGrid.AddColumn("InDate", "입고일", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("InId", "입고자", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("PoNo", "발주번호", HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("PoId", "발주자", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("InSre", "입고완료", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Temp2", "기타", HorzAlignment.Center, false);

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InDate", "InId", "InSre", "Memo");
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarButtonVisible(false);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);


            DetailGridExControl.MainGrid.AddColumn("InNo", "입고번호", HorzAlignment.Default, false);            
            DetailGridExControl.MainGrid.AddColumn("InSeq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InLotno", "입고LOT", HorzAlignment.Default, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("PoQty", "발주수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Cost", "입고단가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Amt", "입고금액", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("LotNo", "제조LOTNO", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("PoNo", "발주번호", HorzAlignment.Center, false);
            DetailGridExControl.MainGrid.AddColumn("PoSeq", "발주순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("InSre", "입고상태", HorzAlignment.Center, true);


            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InQty", "Cost", "Memo", "InSre");

            DetailGridExControl.BestFitColumns();
        }
        protected override void InitRepository()
        {
            
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode",  ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(),"CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDuedate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InSre", MasterCode.GetMasterCode((int)MasterCodeEnum.INPUTSTATUS).ToList());

            DetailGridExControl.MainGrid.SetRepositoryItemTextEdit("PoQty", 0, DefaultBoolean.Default, MaskType.Numeric, "n0", true);            
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InSre", MasterCode.GetMasterCode((int)MasterCodeEnum.INPUTSTATUS).ToList());


        }
        protected override void DataLoad()
        {

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string cust = lupcustcode.EditValue.GetNullToEmpty();
            string pono = tx_pono.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                            p.InDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                            &&(string.IsNullOrEmpty(cust)?true:p.CustCode==cust)
                                                                            &&(string.IsNullOrEmpty(pono)?true:p.PoNo==pono)                                                                           
                                                                            
                                                                            ).OrderBy(o => o.InNo).ToList();
                                                                    //).GroupBy(g => g.OrderNo).Select(s => s.First()).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

        }
        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1800 obj = MasterGridBindingSource.Current as TN_PUR1800;
            DetailGridBindingSource.DataSource = obj.PUR1801List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();

            // SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
        }
        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1800M, param, AddOrderList);
            form.ShowPopup(true);
        }
        private void AddOrderList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

      
            List<VI_PUR1600_LIST> partList = (List<VI_PUR1600_LIST>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {

                TN_PUR1800 newobj = new TN_PUR1800();
                newobj.InNo = DbRequesHandler.GetRequestNumber("OIN");
                newobj.InDate  =DateTime.Today;
                newobj.InId    ="";
                newobj.CustCode = returnedPart.CustCode;
                newobj.Memo    = returnedPart.Memo;
                newobj.PoNo    = returnedPart.PoNo;
                newobj.PoId    = returnedPart.PoId;
                newobj.InSre   ="40";//40 대기,41 입고중,42완료
                newobj.Temp2 ="";

                List<TN_PUR1700> pur1700 = ModelService.GetChildList<TN_PUR1700>(p => p.PoNo == returnedPart.PoNo).ToList();
                if (pur1700.Count >= 1)
                {
                    for (int i = 0; i < pur1700.Count; i++)
                    {
                        TN_PUR1801 pobj = new TN_PUR1801();
                        pobj.InNo = newobj.InNo;
                        pobj.InSeq=newobj.PUR1801List.Count==0?1: newobj.PUR1801List.OrderBy(o => o.InSeq).LastOrDefault().InSeq + 1;
                        pobj.InLotno = pobj.InNo + pobj.InSeq.ToString().PadLeft(2, '0');
                        pobj.ItemCode = pur1700[i].ItemCode;
                        pobj.PoQty = Convert.ToInt32(pur1700[i].PoQty);
                        pobj.LotNo = pur1700[i].LotNo;
                        pobj.Memo = pur1700[i].Memo;
                        pobj.PoNo = pur1700[i].PoNo;
                        pobj.PoSeq = pur1700[i].PoSeq;
                        pobj.InSre = "40";
                        newobj.PUR1801List.Add(pobj);
                        DetailGridBindingSource.Add(pobj);
                    }



                }

               MasterGridBindingSource.Add(newobj);
                ModelService.Insert(newobj);

            }

            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteRow()
        {
            TN_PUR1800 obj = MasterGridBindingSource.Current as TN_PUR1800;
            MasterGridBindingSource.Remove(obj);
            ModelService.Delete(obj);
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

    }
    
}
