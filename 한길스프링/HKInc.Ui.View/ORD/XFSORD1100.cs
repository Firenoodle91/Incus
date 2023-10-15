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

namespace HKInc.Ui.View.ORD
{ 
    /// <summary>
    /// 개발납품계획관리화면
    /// </summary>
    public partial class XFSORD1100 : HKInc.Service.Base.ListMasterDetailDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");

        public XFSORD1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
        }

        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호");
            MasterGridExControl.MainGrid.AddColumn("OrderDate", "수주일");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "수주처");
            MasterGridExControl.MainGrid.AddColumn("CustOrderid", "고객사담당자");
            MasterGridExControl.MainGrid.AddColumn("PeriodDate", "납기일");
            MasterGridExControl.MainGrid.AddColumn("OrderId", "담당자");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("OrderNo", "수주번호", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "소분류");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            DetailGridExControl.MainGrid.AddColumn("OrderQty", "수주수량");
            DetailGridExControl.MainGrid.AddColumn("PeriodDate", "납기일");
            DetailGridExControl.BestFitColumns();

            SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.MainGrid.AddColumn("DelivDate", "계획일자");
            SubDetailGridExControl.MainGrid.AddColumn("DelivQty", "계획수량");
            SubDetailGridExControl.MainGrid.AddColumn("DelivId", "담당자");
            SubDetailGridExControl.MainGrid.AddColumn("ProdYn", "생산의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("OutConfirmflag", "출고의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DelivDate", "DelivQty", "ProdYn", "OutConfirmflag", "Memo");
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OrderDate");       
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag != "N"), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderQty", DefaultBoolean.Default, "n0", true, false);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PeriodDate");

            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("DelivQty", DefaultBoolean.Default, "n0", true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProdYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutConfirmflag", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo");
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            
            string CustCode = lupCustcode.EditValue.GetNullToEmpty();
            string Itemcode = tx_item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OrderType == "개발")
                                                                        && (string.IsNullOrEmpty(CustCode) ? true : p.CustomerCode == CustCode)
                                                                        && (string.IsNullOrEmpty(Itemcode) ? true : (p.TN_ORD1002List.Any(c => c.TN_STD1100.ItemNm1.Contains(Itemcode)) || p.TN_ORD1002List.Any(c => c.TN_STD1100.ItemNm.Contains(Itemcode))))
                                                                        && (p.OrderDate >= datePeriodEditEx1.DateFrEdit.DateTime
                                                                            && p.OrderDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                     )
                                                                     .OrderBy(p => p.OrderNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            var MasterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (MasterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                SubDetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = MasterObj.TN_ORD1002List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();

            if (MasterObj.TN_ORD1002List.Count > 0) DetailFocusedRowChanged();
        }

        protected override void DetailFocusedRowChanged()
        {
            var DetailObj = DetailGridBindingSource.Current as TN_ORD1002;
            if (DetailObj == null)
            {
                SubDetailGridExControl.MainGrid.Clear();
                return;
            }
            SubDetailGridBindingSource.DataSource = DetailObj.TN_ORD1100List.OrderBy(o => o.DelivSeq).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
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
                DelivQty = 0,
                DelivId = Utils.Common.GlobalVariable.LoginId,
                ProdYn = "N",
                OutConfirmflag = "N",
                Temp=Dobj.Temp
            };

            SubDetailGridBindingSource.Add(obj);
            Dobj.TN_ORD1100List.Add(obj);
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteSubDetailRow()
        {
            if (!UserRight.HasEdit) return;

            var DetailObj = DetailGridBindingSource.Current as TN_ORD1002;
            if (DetailObj == null) return;

            var SubObj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (SubObj == null) return;
            DetailObj.TN_ORD1100List.Remove(SubObj);
            SubDetailGridBindingSource.RemoveCurrent();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
    }
}
}
