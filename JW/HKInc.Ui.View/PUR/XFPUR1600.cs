using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Interface.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HKInc.Ui.View.PUR
{

    public partial class XFPUR1600 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1600> ModelService = (IService<TN_PUR1600>)ProductionFactory.GetDomainService("TN_PUR1600");
        public XFPUR1600()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;
            User ss = ModelService.GetChildList<User>(p => p.UserId == GlobalVariable.UserId).OrderBy(o => o.UserId).FirstOrDefault();
            if (ss.PurMaster == "Y")
            {
                simpleButton3.Enabled = true;
                simpleButton4.Enabled = true;

            }
            else
            {

                simpleButton3.Enabled = false;
                simpleButton4.Enabled = false;
            }
        }

        private void DetailView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.Name != "MatLotno") return;
            int irow = e.RowHandle;
            string workno = gv.GetRowCellValue(irow, gv.Columns["WorkNo"]).ToString();
            string MachineCode = gv.GetRowCellValue(irow, gv.Columns["MachineCode"]).ToString();
            string itemcode = gv.GetRowCellValue(irow, gv.Columns["ItemCode"]).ToString();
            string sclot = gv.GetRowCellValue(irow, gv.Columns["MatLotno"]).ToString();

            DataSet ds= DbRequesHandler.GetDataQury("exec SP_LOTMAKE_PO @workno = '" +workno+ "'"
                +", @mccode = '" + MachineCode + "'"
                +", @item = '" + itemcode + "', @date = '" + DateTime.Today.ToString("yyyyMMdd") + "', @srclot = '" + sclot + "'");

            if (ds.Tables[0].Rows[0][0].ToString() == "N")
            {
                MessageBox.Show("원소재 입고 LOT를 확인하세요");
            }
            else {
                gv.SetRowCellValue(irow, gv.Columns["LotNo"], ds.Tables[0].Rows[0][0].ToString());
            }
        }

        protected override void InitCombo()
        {
           
            lupcustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p =>  p.UseFlag == "Y").OrderBy(p =>p.CustomerName).ToList());
        }
        protected override void InitGrid()
        {
            //MasterGridExControl.MainGrid.Init();
            //MasterGridExControl.SetToolbarVisible(false);
            //MasterGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일", HorzAlignment.Center, true);
            //MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);
            //MasterGridExControl.MainGrid.AddColumn("PlanNo", "계획번호", HorzAlignment.Center, false);
            //MasterGridExControl.MainGrid.AddColumn("PSeq", "공정순서", HorzAlignment.Center, true);
            //MasterGridExControl.MainGrid.AddColumn("Process", "공정", HorzAlignment.Center, true);
            //MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목", HorzAlignment.Center, true);
            //MasterGridExControl.MainGrid.AddColumn("PlanQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("DelivDate", "출하예정일");
            //MasterGridExControl.MainGrid.AddColumn("OrderNo","수주번호");
            //MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            //MasterGridExControl.MainGrid.AddColumn("JobStates","작업상태");



            //MasterGridExControl.BestFitColumns();

            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("PoNo","발주번호", HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("PoDate", "발주일", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("PoId", "발주담당자", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustCode","거래처", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("InDuedate", "입고예정일", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Memo","비고", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Temp2", "발주확정");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoDate", "PoId", "CustCode", "InDuedate", "Memo");
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("PoNo", "발주번호", HorzAlignment.Center, true);            
            DetailGridExControl.MainGrid.AddColumn("PoSeq", "순번", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("Cost", "단가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("PoQty", "수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Amt", "발주가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("MachineCode", false);
            DetailGridExControl.MainGrid.AddColumn("WorkNo", false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            DetailGridExControl.MainGrid.AddColumn("MatLotno", "원소재LOT");
            DetailGridExControl.MainGrid.AddColumn("LotNo", "제조LOT");
            


            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "Cost", "PoQty", "MatLotno", "Memo");

            DetailGridExControl.BestFitColumns();
        }
        protected override void InitRepository()
        {


            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            //MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").ToList(), "ItemCode", "ItemNm1");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates",  MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());




            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode",  ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(),"CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PoDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDuedate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp2", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemTextEdit("PoQty", 0, DefaultBoolean.Default, MaskType.Numeric, "n0", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();


            string cust = lupcustcode.EditValue.GetNullToEmpty();


            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.PoDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                            p.PoDate <= datePeriodEditEx1.DateToEdit.DateTime)&&(string.IsNullOrEmpty(cust)?true:p.CustCode==cust)).OrderBy(o => o.PoNo).ToList();
                                                                    //).GroupBy(g => g.OrderNo).Select(s => s.First()).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

        }
        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1600 obj = MasterGridBindingSource.Current as TN_PUR1600;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.PUR1700List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();

            // SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
        }
        protected override void AddRowClicked()
        {
            TN_PUR1600 obj = new TN_PUR1600()
            {
                PoNo = DbRequesHandler.GetRequestNumber("PO")
                , PoDate = DateTime.Today
                , InDuedate = DateTime.Today
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
            MasterGridBindingSource.EndEdit();
        }
        protected override void DeleteRow()
        {
            TN_PUR1600 obj = MasterGridBindingSource.Current as TN_PUR1600;

            if (obj.PUR1700List.Count > 0)
            {
                MessageBoxHandler.Show("발주 상세 내역이 있습니다. 확인 해 주세요.");
                return;
            }

            MasterGridBindingSource.Remove(obj);
            ModelService.Delete(obj);
        }
        protected override void DetailAddRowClicked()
        {
            TN_PUR1600 obj = MasterGridBindingSource.Current as TN_PUR1600;
        
            if (obj == null) return;

            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1700, param, AddOrderList);
            form.ShowPopup(true);
        }

        private void AddOrderList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            TN_PUR1600 obj = MasterGridBindingSource.Current as TN_PUR1600;
            List<TP_PUR1700LIST> partList = (List<TP_PUR1700LIST>)e.Map.GetValue(PopupParameter.ReturnObject);

            //    TN_PUR1700 dtl = new TN_PUR1700() {
            //        PoNo=obj.PoNo,
            //        PoSeq=obj.PUR1700List.Count==0?1: obj.PUR1700List.Count+1,
            //    };
            //    DetailGridBindingSource.Add(dtl);
            //    obj.PUR1700List.Add(dtl);
            foreach (var returnedPart in partList)
            {
              
                        TN_PUR1700 newobj = new TN_PUR1700();
                        newobj.PoNo = obj.PoNo;
                        newobj.PoSeq = obj.PUR1700List.Count == 0 ? 1 : obj.PUR1700List.OrderBy(o => o.PoSeq).LastOrDefault().PoSeq + 1;
                        newobj.ItemCode = returnedPart.ItemCode;
                        newobj.PoQty = returnedPart.OkQty;
                        newobj.Memo = returnedPart.Memo;
                        newobj.ProcessCode = returnedPart.Process;
                        newobj.WorkNo = returnedPart.WorkNo;
                        newobj.Pseq = returnedPart.PSeq;
                        newobj.MatLotno = returnedPart.LotNo;
                        newobj.LotNo = returnedPart.LotNo;
                        newobj.MachineCode = returnedPart.MachineCode;
                        obj.PUR1700List.Add(newobj);
                        DetailGridBindingSource.Add(newobj);
                
            }
          
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {
            TN_PUR1600 obj = MasterGridBindingSource.Current as TN_PUR1600;
            TN_PUR1700 delobj = DetailGridBindingSource.Current as TN_PUR1700;

            var tN_PUR1800 = ModelService.GetChildList<TN_PUR1800>(x => x.PoNo == obj.PoNo).ToList();

            if (tN_PUR1800.Count > 0)
            {
                MessageBoxHandler.Show("입고 내역이 있습니다. 확인 해 주세요.");
                return;
            }

            DetailGridBindingSource.Remove(delobj);
            obj.PUR1700List.Remove(delobj);          
        }


        protected override void DataSave()
        {

            GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
            for (int i=0;i<gv.RowCount;i++)
            {
                string _MatLotno = Convert.ToString(gv.GetRowCellValue(i, "MatLotno").GetNullToEmpty());
                string workno = Convert.ToString(gv.GetRowCellValue(i, "WorkNo").GetNullToEmpty());
                string process = Convert.ToString(gv.GetRowCellValue(i, "ProcessCode").GetNullToEmpty());
                string pseq = Convert.ToString(gv.GetRowCellValue(i, "Pseq").GetNullToEmpty());
                if (pseq != "1")
                {
                    if (_MatLotno == null || _MatLotno == "")
                    {
                        HKInc.Service.Handler.MessageBoxHandler.Show("발주리스트" + Convert.ToInt32(i + 1) + "행의 원소재LOT는 필수입력 사항입니다.");
                        return;
                    }
                }
                TN_MPS1400 obj = ModelService.GetChildList<TN_MPS1400>(p => p.WorkNo == workno && p.Process == process).FirstOrDefault();
                if (obj != null)
                {
                    obj.JobStates = "33";
                    ModelService.UpdateChild<TN_MPS1400>(obj);
                }

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
            TN_PUR1600 obj = MasterGridBindingSource.Current as TN_PUR1600;
            if (obj == null) return;
            if (obj.Temp2 != "Y") {
                MessageBox.Show("발주가 확정되지 않았습니다. 승인권자에게 문의하세요");
                return;
            }

            try
            {
                WaitHandler.ShowWait();

                REPORT.XRPUR1600 report = new REPORT.XRPUR1600(obj);
                report.CreateDocument();
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
                DetailGridExControl.MainGrid.MainView.BestFitColumns();
            }
            //REPORT.XRPUR1700 report = new REPORT.XRPUR1700(obj.CustCode,obj.PoId,obj.InDuedate);
            //report.DataSource = ModelService.GetChildList<VI_POLISTPRT>(p => p.Pono == obj.PoNo);
            //report.CreateDocument();


            //report.ShowPrintStatusDialog = false;
            //report.ShowPreview();//.Print();
             
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          

            try
            {
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_PUR1700>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;

                var FirstReport = new POP_Popup.XRITEMMOVE_100X100();
                foreach (var v in PrintList.Where(p => p._Check == "Y").ToList())
                {

                    var report = new POP_Popup.XRITEMMOVE_100X100(v.WorkNo,v.LotNo,"");
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            TN_PUR1600 obj = MasterGridBindingSource.Current as TN_PUR1600;
            obj.Temp2 = "Y";
            MasterGridExControl.MainGrid.BestFitColumns();
            ModelService.Update(obj);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ModelService.Save();
            TN_PUR1600 obj = MasterGridBindingSource.Current as TN_PUR1600;
            obj.Temp2 = "N";
            MasterGridExControl.MainGrid.BestFitColumns();
            ModelService.Update(obj);
        }
    }
    
}
