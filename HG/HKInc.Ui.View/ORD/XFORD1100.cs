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
using HKInc.Service.Handler;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 납품계획관리화면
    /// </summary>
    public partial class XFORD1100 : HKInc.Service.Base.ListMasterDetailDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");
        public XFORD1100()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today.AddDays(+30);

            SubDetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
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
            DetailGridExControl.MainGrid.AddColumn("DelivFlag", "계획여부", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "소분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            DetailGridExControl.MainGrid.AddColumn("OrderQty", "수주수량");
            DetailGridExControl.MainGrid.AddColumn("StockQty", "재고수량", HorzAlignment.Far, true);
            DetailGridExControl.BestFitColumns();

            SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.MainGrid.AddColumn("DelivDate", "납기일");
            SubDetailGridExControl.MainGrid.AddColumn("DelivQty", "납기수량");
            SubDetailGridExControl.MainGrid.AddColumn("DelivId", "담당자");
            SubDetailGridExControl.MainGrid.AddColumn("ProdYn", "생산의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("OutConfirmflag", "출고의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("TurnKeyFlag", "외주생산", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DelivDate", "DelivQty", "ProdYn", "OutConfirmflag", "Memo", "TurnKeyFlag", "DelivId");
            SubDetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "DelivDate", "DelivQty");
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
            //DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PeriodDate");
            
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("DelivQty", DefaultBoolean.Default, "n0", true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProdYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutConfirmflag", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TurnKeyFlag", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");

            SubDetailGridExControl.MainGrid.Columns["DelivQty"].RealColumnEdit.EditValueChanged += RealColumnEdit_EditValueChanged;

        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo");
            DetailGridRowLocator.GetCurrentRow("Seq");
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            
            string CustCode = lupCustcode.EditValue.GetNullToEmpty();
            string Itemcode = tx_item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OrderType == "양산")
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
            DetailGridRowLocator.SetCurrentRow();

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

            var DetailObj = DetailGridBindingSource.Current as TN_ORD1002;
            if (DetailObj == null) return;

            if (DetailObj.OrderQty <= DetailObj.TN_ORD1100List.Sum(p => p.DelivQty).GetDecimalNullToZero())
            {
                MessageBoxHandler.Show("수주수량과 계획수량이 같습니다.", "경고");
                return;
            }

            var obj = new TN_ORD1100()
            {
                DelivSeq = DbRequesHandler.GetRequestNumber("DLV"),
                OrderNo = DetailObj.OrderNo,
                Seq = DetailObj.Seq,
                ItemCode = DetailObj.ItemCode,
                DelivDate = DetailObj.TN_ORD1000.PeriodDate,
                DelivQty = DetailObj.OrderQty.GetIntNullToZero() - DetailObj.TN_ORD1100List.Sum(p => p.DelivQty).GetIntNullToZero(),
                DelivId = Utils.Common.GlobalVariable.LoginId,
                ProdYn = "Y",
                OutConfirmflag = "Y",
                TurnKeyFlag = "N",
                Temp = DetailObj.Temp

            };
          
            SubDetailGridBindingSource.Add(obj);
            DetailObj.TN_ORD1100List.Add(obj);
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteSubDetailRow()
        {
            if (!UserRight.HasEdit) return;

            var DetailObj = DetailGridBindingSource.Current as TN_ORD1002;
            if (DetailObj == null) return;

            var SubObj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (SubObj == null) return;

            if (SubObj.TN_ORD1200List.Count > 0)
            {
                MessageBoxHandler.Show("제품 출고 데이터가 있으므로 삭제가 불가합니다.");
                return;
            }
            else if (SubObj.TN_PUR2000List.Count > 0) 
            {
                MessageBoxHandler.Show("턴키 발주 데이터가 있으므로 삭제가 불가합니다.");
                return;
            }
            else
            {
                var check1 = ModelService.GetChildList<TN_MPS1300>(p => p.DelivSeq == SubObj.DelivSeq).FirstOrDefault();
                if(check1 != null)
                {
                    MessageBoxHandler.Show("생산 계획 데이터가 있으므로 삭제가 불가합니다.");
                    return;
                }
            }
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

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            var FieldName = view.FocusedColumn.FieldName;
            var obj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (FieldName == "ProdYn")
            {
                if(obj.TurnKeyFlag == "Y")
                {
                    MessageBoxHandler.Show("외주생산이 'Y' 입니다. 'N'으로 변경 후 의뢰해 주시기 바랍니다.", "경고");
                    e.Cancel = true;
                }
            }
            else if (FieldName == "TurnKeyFlag")
            {
                if (obj.ProdYn == "Y")
                {
                    MessageBoxHandler.Show("생산의뢰가 'Y' 입니다. 'N'으로 변경 후 의뢰해 주시기 바랍니다.", "경고");
                    e.Cancel = true;
                }
                if (ModelService.GetChildList<TN_PUR2000>(p=>p.OrderNo == obj.OrderNo && p.Seq == obj.Seq && p.DelivSeq == obj.DelivSeq).FirstOrDefault() != null)
                {
                    MessageBoxHandler.Show("외주 발주가 이미 존재하여 변경이 불가능 합니다.", "경고");
                    e.Cancel = true;
                }
            }
        }

        private void RealColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            var view = SubDetailGridExControl.MainGrid.MainView;
            var FieldName = view.FocusedColumn.FieldName;
            var Detailobj = DetailGridBindingSource.Current as TN_ORD1002;
            var obj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (FieldName == "DelivQty")
            {
                obj.DelivQty = view.ActiveEditor.EditValue.GetIntNullToZero();
                if (Detailobj.TN_ORD1100List.Sum(p => p.DelivQty).GetDecimalNullToZero() > Detailobj.OrderQty)
                {
                    MessageBoxHandler.Show("계획수량은 수주수량보다 많을 수 없습니다.", "경고");
                    //obj.DelivQty = view.ActiveEditor.OldEditValue.GetIntNullToZero();
                    //view.RefreshData();
                    view.ActiveEditor.EditValue = view.ActiveEditor.OldEditValue;
                }
            }
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var ItemCode = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                var stock = ModelService.GetChildList<VI_PRODQTY_MST>(p => p.ItemCode == ItemCode).FirstOrDefault();
                //DetailGridExControl.MainGrid.BestFitColumns();
                if (stock != null)
                {
                    e.DisplayText = stock.Stockqty == null ? "0" : stock.Stockqty.GetIntNullToZero().ToString("n0");
                }
            }
        }
    }
}
