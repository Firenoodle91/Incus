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
using HKInc.Service.Base;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.PUR
{
    public partial class XFPUR2000 : ListMasterDetailFormTemplate
    {
        IService<TN_PUR2000> ModelService = (IService<TN_PUR2000>)ProductionFactory.GetDomainService("TN_PUR2000");

        public XFPUR2000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitControls()
        {
            base.InitControls();
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }

        protected override void InitCombo()
        {
            base.InitCombo();

            cboCondition.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cboCondition.Properties.Items.Add("발주일");
            cboCondition.Properties.Items.Add("입고일");
            cboCondition.SelectedIndex = 0;

            lupItemCode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
            lupCustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList());

            datePeriodEditEx1.SetTodayIsMonth();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.OrderNo", "수주번호");
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.Seq", "수주순번", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.DelivSeq", "납품계획번호", false);
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.Temp", "거래처");
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("PoNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("PoDate", "발주일");            
            MasterGridExControl.MainGrid.AddColumn("PoRemainQty", "발주잔량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("PoQty", "발주수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("PoCost", "발주단가", HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("PoAmt", "발주금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddUnboundColumn("PoAmt", "발주금액", DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([PoQty],0) * ISNULL([PoCost],0)", FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("PoId", "발주자");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoDate", "PoQty", "PoCost", "PoId", "Memo");
            MasterGridExControl.MainGrid.SetHeaderColor(Color.Red, "PoQty");

            DetailGridExControl.MainGrid.AddColumn("PoNo", "발주번호", false);
            DetailGridExControl.MainGrid.AddColumn("ShipmentYN", "출고상태", HorzAlignment.Center, true); 
            DetailGridExControl.MainGrid.AddColumn("InLotNo", "입고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("InDate", "입고일");
            DetailGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InCost", "입고단가", HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("PoAmt", "발주금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddUnboundColumn("InAmt", "입고금액", DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InId", "입고자");
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");            
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InDate", "InQty", "InCost", "InId", "Memo");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "InQty");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_ORD1100.Temp", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PoDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoQty");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoCost");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");            
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InCost");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
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

            //var ItemCode = lupItemCode.EditValue.GetNullToEmpty();
            var Item = textItemCodeName.EditValue.GetNullToEmpty();
            var CustomerCode = lupCustomerCode.EditValue.GetNullToEmpty();

            if(cboCondition.SelectedIndex == 0)
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.TN_ORD1100.TN_STD1100.ItemNm.Contains(Item) || p.TN_ORD1100.TN_STD1100.ItemNm1.Contains(Item)) && //(string.IsNullOrEmpty(ItemCode) ? true : p.TN_ORD1100.ItemCode == ItemCode) &&
                                                                              (string.IsNullOrEmpty(CustomerCode) ? true : p.TN_ORD1100.Temp == CustomerCode) &&
                                                                              (p.PoDate >= datePeriodEditEx1.DateFrEdit.DateTime && p.PoDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                          ).OrderBy(p => p.PoNo).ToList();
            }
            else
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.TN_ORD1100.TN_STD1100.ItemNm.Contains(Item) || p.TN_ORD1100.TN_STD1100.ItemNm1.Contains(Item)) &&//(string.IsNullOrEmpty(ItemCode) ? true : p.TN_ORD1100.ItemCode == ItemCode) &&
                                                                              (string.IsNullOrEmpty(CustomerCode) ? true : p.TN_ORD1100.Temp == CustomerCode) &&
                                                                               (p.TN_PUR2001List.Any(c => c.InDate >= datePeriodEditEx1.DateFrEdit.DateTime) &&
                                                                                p.TN_PUR2001List.Any(c => c.InDate <= datePeriodEditEx1.DateToEdit.DateTime))
                                                                          ).OrderBy(p => p.PoNo).ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            var MasterObj = MasterGridBindingSource.Current as TN_PUR2000;
            if(MasterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = MasterObj.TN_PUR2001List.OrderBy(p => p.InLotNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XFSPUR2000, param, AddMaster);
            form.ShowPopup(true);
        }

        protected override void DeleteRow()
        {
            var MasterObj = MasterGridBindingSource.Current as TN_PUR2000;
            if (MasterObj == null) return;
            if (MasterObj.TN_PUR2001List.Count > 0) 
            {
                MessageBoxHandler.Show("입고 내역이 존재하여 삭제가 불가합니다.", "경고");
                return;
            }

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(MasterObj);
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void DetailAddRowClicked()
        {
            var MasterObj = MasterGridBindingSource.Current as TN_PUR2000;
            if (MasterObj == null) return;
            if (MasterObj.PoRemainQty <= 0)
            {
                MessageBoxHandler.Show("입고할 수 있는 수량이 없습니다.", "경고");
                return;
            }

            var NewDetailObj = new TN_PUR2001()
            {
                InLotNo = DbRequesHandler.GetRequestNumber("TKIN"),
                PoNo = MasterObj.PoNo,
                InId = GlobalVariable.LoginId,
                InQty = MasterObj.PoRemainQty,
                InCost = 0,
                InDate = DateTime.Today,
                ShipmentYN = "출고대기"
            };

            DetailGridBindingSource.Add(NewDetailObj);
            DetailGridBindingSource.MoveLast();
            DetailGridExControl.BestFitColumns();
            MasterObj.TN_PUR2001List.Add(NewDetailObj);
            IsFormControlChanged = true;
        }

        protected override void DeleteDetailRow()
        {
            var MasterObj = MasterGridBindingSource.Current as TN_PUR2000;
            var DetailObj = DetailGridBindingSource.Current as TN_PUR2001;
            if (MasterObj == null || DetailObj == null) return;
            if (ModelService.GetChildList<TN_ORD1201>(p => p.PackLotNo == DetailObj.InLotNo).FirstOrDefault() != null)
            {
                MessageBoxHandler.Show("이미 출고된 정보가 있어 삭제를 할 수 없습니다.", "경고");
                return;
            }
            DetailGridBindingSource.RemoveCurrent();
            MasterObj.TN_PUR2001List.Remove(DetailObj);
            IsFormControlChanged = true;
        }

        private void AddMaster(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var ReturnValue = (List<TN_ORD1100>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in ReturnValue)
            {
                var NewObj = new TN_PUR2000()
                {
                    TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == v.OrderNo && p.Seq == v.Seq && p.DelivSeq == v.DelivSeq).First(),
                    PoNo = DbRequesHandler.GetRequestNumber("TKPO"),
                    Seq = v.TN_PUR2000List.Count == 0 ? 1 : v.TN_PUR2000List.Count + 1,
                    PoId = GlobalVariable.LoginId,
                    PoQty = v.TurnKeyRemainQty,
                    PoCost = 0,
                    PoDate = DateTime.Today
                };
                MasterGridBindingSource.Add(NewObj);
                MasterGridBindingSource.MoveLast();
                ModelService.Insert(NewObj);
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, NewObj.PoNo);
            }
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ShipmentYN")
            {
                var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;                
                var InLotNo = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "InLotNo").ToString();
                var InQty = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "InQty").GetIntNullToZero();
                var ShipmentList = ModelService.GetChildList<TN_ORD1201>(p => p.PackLotNo == InLotNo).ToList();                
                if (ShipmentList.Count > 0)
                {
                    var SumOutQty = ShipmentList.Sum(p => p.OutQty);
                    if(SumOutQty < InQty)
                        e.DisplayText = string.Format("출고중 잔량:{0:n0}", (InQty - SumOutQty));
                    else e.DisplayText = "출고완료";
                }
                else e.DisplayText = "출고대기";
            }
        }

    }
}