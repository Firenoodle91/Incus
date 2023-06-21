using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
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
    public partial class XFPUR2100 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR2100> ModelService = (IService<TN_PUR2100>)ProductionFactory.GetDomainService("TN_PUR2100");

        public XFPUR2100()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;

            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+10);

            User ss = ModelService.GetChildList<User>(p => p.UserId == GlobalVariable.UserId).OrderBy(o => o.UserId).FirstOrDefault();
            if (ss.PurMaster == "Y")
            {
                simpleButton1.Enabled = true;
                simpleButton3.Enabled = true;

            }
            else
            {

                simpleButton1.Enabled = false;
                simpleButton3.Enabled = false;
            }
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

            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호", false);
            MasterGridExControl.MainGrid.AddColumn("DelivSeq", "계획번호", false);
            MasterGridExControl.MainGrid.AddColumn("PoNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("PoDate", "발주일");
            MasterGridExControl.MainGrid.AddColumn("DueDate", "납기예정일");
            MasterGridExControl.MainGrid.AddColumn("PoId", "발주자");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명", false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번", false);
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("PoFlag", "발주확정");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoDate", "DueDate", "PoId", "CustomerCode", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("PoNo", "발주번호", false);
            DetailGridExControl.MainGrid.AddColumn("PoSeq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            DetailGridExControl.MainGrid.AddColumn("PoQty", "발주수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("PoCost", "단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Amt", "금액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "PoQty", "PoCost", "Memo");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("PoFlag", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT)).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("PoNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();

            string luser = lupUser.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.PoDate >= dp_date.DateFrEdit.DateTime.Date && p.PoDate <= dp_date.DateToEdit.DateTime.Date)
                                                           && (string.IsNullOrEmpty(luser) ? true : p.PoId == luser))
                                                           .OrderBy(o => o.PoNo).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
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

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR2100;
            if (masterObj == null) return;

            DetailGridBindingSource.DataSource = masterObj.TN_PUR2101List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            //판매계획 참조하는 팝업 (사용x)
            //PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.Service, ModelService);
            //param.SetValue(PopupParameter.UserRight, UserRight);
            //param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            //param.SetValue(PopupParameter.Value_1, ""); //외주가공품일경우만 추가
            //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTORD1200, param, AddOrderList);
            //form.ShowPopup(true);

            TN_PUR2100 newObj = new TN_PUR2100();
            newObj.PoNo = DbRequesHandler.GetRequestNumber("OPO");
            newObj.PoDate = DateTime.Today;
            newObj.DueDate = DateTime.Today;
            newObj.PoFlag = "N";

            MasterGridBindingSource.Add(newObj);
            ModelService.Insert(newObj);

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private void AddOrderList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TN_ORD1100> partList = (List<TN_ORD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var s in partList)
            {
                TN_PUR2100 newObj = new TN_PUR2100();
                newObj.PoNo = DbRequesHandler.GetRequestNumber("OPO");
                newObj.OrderNo = s.OrderNo;
                newObj.DelivSeq = s.DelivSeq;
                newObj.CustomerCode = s.Temp;
                newObj.PoDate = DateTime.Today;
                newObj.DueDate = DateTime.Today;
                newObj.PoFlag = "N";
                newObj.ItemCode = s.ItemCode;

                MasterGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);
            }

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR2100;
            if (masterObj == null) return;
            if (masterObj.PoFlag == "Y") return;
            if (masterObj.CustomerCode.GetNullToEmpty() == "")
            {
                MessageBox.Show("거래처는 필수입니다.");
                return;
            }
            List<TN_STD1100> itemlist = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT) && p.MainCust == masterObj.CustomerCode).OrderBy(o => o.ItemNm).ToList();
            if (itemlist.Count() == 0)
            {
                MessageBox.Show("해당 거래처에 등록된 품목이 없습니다.");
                return;
            }

            TN_PUR2101 newObj = new TN_PUR2101();
            newObj.PoNo = masterObj.PoNo;
            newObj.PoSeq = masterObj.TN_PUR2101List.Count == 0 ? 1 : masterObj.TN_PUR2101List.Max(m => m.PoSeq) + 1;
            //newObj.ItemCode = masterObj.ItemCode;

            masterObj.TN_PUR2101List.Add(newObj);
            DetailGridBindingSource.Add(newObj);            
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR2100;
            if (masterObj == null) return;

            if (masterObj.TN_PUR2101List.Count > 0)
            {
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다");
                return;
            }

            if (masterObj.PoFlag== "Y")
            {
                MessageBox.Show("발주확정건은 삭제할수 없습니다.");
                return;
            }

            if (ModelService.GetChildList<TN_PUR2200>(x => x.PoNo == masterObj.PoNo).ToList().Count > 0)
            {
                MessageBox.Show("외주가공품 입고 내역이 있어 삭제할수 없습니다.");
                return;
            }

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(masterObj);
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR2100;
            if (masterObj == null) return;
            if (masterObj.PoFlag == "Y")
            {
                MessageBox.Show("발주확정건은 삭제할수 없습니다.");
                return;
            }

            var detailObj = DetailGridBindingSource.Current as TN_PUR2101;
            if (detailObj == null) return;

            if (detailObj.TN_PUR2201List.Count > 0)
            {
                MessageBox.Show("입고된 내역이 있어 삭제할수 없습니다.");
                return;
            }

            DetailGridBindingSource.Remove(detailObj);
            masterObj.TN_PUR2101List.Remove(detailObj);
        }

        /// <summary>
        /// 확정
        /// </summary>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var obj = MasterGridBindingSource.Current as TN_PUR2100;
            obj.PoFlag = "Y";

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 확정취소
        /// </summary>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var obj = MasterGridBindingSource.Current as TN_PUR2100;
            obj.PoFlag = "N";

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 발주서출력
        /// </summary>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var obj = MasterGridBindingSource.Current as TN_PUR2100;
            if (obj == null) return;
            if (obj.PoFlag != "Y") return;

            //List<VI_POLISTPRT> prt = ModelService.GetChildList<VI_POLISTPRT>(p => p.Pono == obj.ReqNo);
            var detailList = DetailGridBindingSource.List as List<TN_PUR2101>;

            if (detailList.Count > 0)
            {
                //REPORT.XRPUR1100 report = new REPORT.XRPUR1100(obj, prt);
                REPORT.XRPUR1100 report = new REPORT.XRPUR1100(obj, detailList);
                report.ShowPrintStatusDialog = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();//.Print();
            }
            MasterGridExControl.MainGrid.BestFitColumns();
        }


        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_PUR2100 obj = MasterGridBindingSource.Current as TN_PUR2100;
            if (obj.PoFlag == "Y")
            {
                e.Cancel = true;
            }
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_PUR2100 obj = MasterGridBindingSource.Current as TN_PUR2100;
            if (obj.PoFlag == "Y")
            {
                e.Cancel = true;
            }
        }
    }
}