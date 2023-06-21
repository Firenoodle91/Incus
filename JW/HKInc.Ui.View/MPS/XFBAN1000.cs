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
    public partial class XFBAN1000 :  HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_BAN1000> ModelService = (IService<TN_BAN1000>)ProductionFactory.GetDomainService("TN_BAN1000");
      //  IService<TN_PUR1301> DtlModelService = (IService<TN_PUR1301>)ProductionFactory.GetDomainService("TN_PUR1301");
        public XFBAN1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+10);
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
            if (e.Column.Name != "WhCode") return;
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

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
         

           // IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.MainGrid.AddColumn("InputNo", "입고번호");
            MasterGridExControl.MainGrid.AddColumn("InputDate", "입고일");
            MasterGridExControl.MainGrid.AddColumn("InputId", "입고자");       
        
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("Temp1", "입고완료");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InputDate", "InputId", "Memo", "Temp1");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

         //   IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("InputNo", false);
            DetailGridExControl.MainGrid.AddColumn("InputSeq", "입고순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("InputQty","입고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
         
            DetailGridExControl.MainGrid.AddColumn("InYn",false);
            DetailGridExControl.MainGrid.AddColumn("Temp2", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");
          
            


            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "ItemCode", "InputQty","Memo", "Temp2", "WhCode", "WhPosition");
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InputId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
          
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InputDate");
          
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp1", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory == "P05").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("InYn", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }
      
        protected override void DataLoad()
        {
          
            string inputNo = tx_ReqNo.Text.GetNullToEmpty();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InputDate >= dp_date.DateFrEdit.DateTime.Date && p.InputDate <= dp_date.DateToEdit.DateTime.Date)
              && (string.IsNullOrEmpty(inputNo) ? true : p.InputNo == inputNo)).OrderBy(o => o.InputDate).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void MasterFocusedRowChanged()
        {
           
            TN_BAN1000 obj = MasterGridBindingSource.Current as TN_BAN1000;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.BAN1001List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
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
            else {
               
                //var cntdata = DtlModelService.GetList(p => p.InputNo == obj.InputNo).ToList();
                //int cnt = cntdata.Count;
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
            if (obj == null) return;
            if (obj.Temp1 == "Y")
            { MessageBox.Show("발주확정건은 삭제할수 없습니다."); }
            else
            {
                TN_BAN1001 dtlobj = DetailGridBindingSource.Current as TN_BAN1001;
                DetailGridBindingSource.RemoveCurrent();
                obj.BAN1001List.Remove(dtlobj);
            }
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

        private void simpleButton1_Click(object sender, EventArgs e)
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
    }
}

