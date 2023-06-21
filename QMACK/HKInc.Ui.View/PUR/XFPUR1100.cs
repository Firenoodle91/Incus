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
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.PUR
{
    /// <summary>
    /// 자재발주관리
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
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;

            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+30);

            User ss = ModelService.GetChildList<User>(p => p.UserId == GlobalVariable.UserId).OrderBy(o => o.UserId).FirstOrDefault();
            if (ss.PurMaster == "Y")
            {
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = true;
            }
            else
            {
                simpleButton1.Enabled = false;
                simpleButton2.Enabled = false;
            }
        }

        protected override void InitCombo()
        {
            lupUser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            MasterGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("ReqDate", "발주일");
            MasterGridExControl.MainGrid.AddColumn("DueDate", "납기예정일");
            MasterGridExControl.MainGrid.AddColumn("ReqId", "발주자");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("Temp2","발주확정");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReqDate", "DueDate", "ReqId", "CustomerCode", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.MainGrid.AddColumn("ReqNo",false);
            DetailGridExControl.MainGrid.AddColumn("ReqSeq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            DetailGridExControl.MainGrid.AddColumn("ReqQty", "발주수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Amt", "금액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "ReqQty", "Temp1", "Memo");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqId", ModelService.GetChildList<UserView>(p=>p.Active=="Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("DueDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp2", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03" || p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Temp1", DefaultBoolean.Default, "n2", true, false);
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            //GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            GridRowLocator.GetCurrentRow("ReqNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1)); // 20220217 오세완 차장 공통사항으로 있는 그리드 포커스 기능 수정 

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion
            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string luser = lupUser.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ReqDate >= dp_date.DateFrEdit.DateTime.Date && p.ReqDate <= dp_date.DateToEdit.DateTime.Date)
                                                                       && (string.IsNullOrEmpty(luser) ? true : p.ReqId == luser)
                                                                       ).OrderBy(o => o.ReqNo).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.PUR1200List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            TN_PUR1100 newobj = new TN_PUR1100()
            {
                ReqNo = DbRequestHandler.GetRequestNumber("PUR"),
                ReqDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(20)
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
        }

        protected override void DeleteRow()
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            if (obj.PUR1200List.Count >= 1)
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다");
            else
            {
                if (obj.Temp2 == "Y")
                    MessageBox.Show("발주확정건은 삭제할수 없습니다.");
                else
                {
                    MasterGridBindingSource.RemoveCurrent();
                    ModelService.Delete(obj);
                }
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            if (obj.Temp2 == "Y") return;
            TN_PUR1200 newobj = new TN_PUR1200()
            {
                ReqNo = obj.ReqNo,
                ReqSeq = (obj.PUR1200List.Count == 0) ? 1 : obj.PUR1200List.OrderBy(o => o.ReqSeq).LastOrDefault().ReqSeq + 1,
                TN_STD1100 = new TN_STD1100()
                // 2022-03-15 김진우 품목코드 추가시 처음의 STD1100의 형태가 없어서 오류가 남. 
                // 그래서 STD1100을 통째로 넣었더니 ITEM_CODE 중복된다고 저장이 안되서 추가
                // CellValueChange 때문에 추가
            };

            DetailGridBindingSource.Add(newobj);
            obj.PUR1200List.Add(newobj);
        }

        protected override void DeleteDetailRow()
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            if (obj.Temp2 == "Y")
                MessageBox.Show("발주확정건은 삭제할수 없습니다."); 
            else
            {
                TN_PUR1200 dtlobj = DetailGridBindingSource.Current as TN_PUR1200;
                DetailGridBindingSource.RemoveCurrent();
                obj.PUR1200List.Remove(dtlobj);
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

        /// <summary>
        /// 확정취소
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;

            // 2022-03-15 김진우 추가        입고시 확정취소 불가하도록 추가
            bool CheckIn = ModelService.GetChildList<TN_PUR1300>(p => p.ReqNo == obj.ReqNo).Any();
            if (CheckIn)
            {
                HKInc.Service.Handler.MessageBoxHandler.Show("입고된 제품은 확정취소가 불가능합니다.");
                return;
            }

            obj.Temp2 = "N";
            MasterGridExControl.MainGrid.BestFitColumns();
            ModelService.Update(obj);
        }

        /// <summary>
        /// 발주확정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ModelService.Save();
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            obj.Temp2 = "Y";
            MasterGridExControl.MainGrid.BestFitColumns();
            ModelService.Update(obj);
        }

        /// <summary>
        /// 발주서 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            if (obj == null) return;
            if (obj.Temp2 != "Y") return;           // 발주확정 여부
            REPORT.XRPUR1700 report = new REPORT.XRPUR1700(obj.CustomerCode, obj.DueDate);      // 2022-03-28 김진우 수정
            //REPORT.XRPUR1700 report = new REPORT.XRPUR1700(obj.CustomerCode, obj.ReqNo, obj.DueDate);
            report.DataSource = ModelService.GetChildList<VI_PO_LIST_PRT>(p => p.Pono == obj.ReqNo);
            report.CreateDocument();


            report.ShowPrintStatusDialog = false;
            report.ShowPreview();//.Print();
        }

        private void DetailMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_PUR1100 masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            TN_PUR1200 detailObj = DetailGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null || detailObj == null) return;

            GridView gv = sender as GridView;
            if (e.Column.Name.ToString() != "ItemCode") return;
            
            string itm = gv.GetFocusedRowCellValue(gv.Columns["ItemCode"]).ToString();
            int rowid = e.RowHandle;
            int cnt = 0;

            for (int i = 0; i < gv.RowCount; i++)
            {
                if (i != rowid)
                {
                    if (itm == gv.GetRowCellValue(i, gv.Columns["ItemCode"]).GetNullToEmpty())
                    {
                        cnt++;
                    }
                }
            }
            if (cnt >= 1)
            {
                MessageBox.Show("이미등록된 품목코드입니다.");

                if (detailObj.ReqQty == 0)
                {
                    DetailGridBindingSource.RemoveCurrent();
                    masterObj.PUR1200List.Remove(detailObj);
                    ModelService.Update(masterObj);
                }
            }
            else
            {
                TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itm).FirstOrDefault();

                detailObj.TN_STD1100 = STD1100;

                // 20220412 오세완 차장 단가이력관리에 입력된 단가로 출력처리
                decimal dCost = DbRequestHandler.GetManagedCost(itm, masterObj.CustomerCode, masterObj.DueDate.ToShortDateString());
                detailObj.Temp1 = dCost.ToString();

                DetailGridExControl.BestFitColumns();
            }
            
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;

            if (obj.Temp2 == "Y")
                e.Cancel = true;
        }
        
        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_PUR1100 obj = MasterGridBindingSource.Current as TN_PUR1100;
            bool CheckIn = ModelService.GetChildList<TN_PUR1300>(p => p.ReqNo == obj.ReqNo).Any();      // 2022-03-15 김진우 추가       입고시 수정 불가 

            if (obj.Temp2 == "Y" || CheckIn)
                e.Cancel = true;
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
    }
}
