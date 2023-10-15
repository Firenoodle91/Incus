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
using HKInc.Service.Handler;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 생산계획관리화면
    /// </summary>
    public partial class XFMPS1100 :  HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_MPSPLAN_LIST> ModelService = (IService<VI_MPSPLAN_LIST>)ProductionFactory.GetDomainService("VI_MPSPLAN_LIST");
        //IService<TN_MPS1300> ModelServicePlan = (IService<TN_MPS1300>)ProductionFactory.GetDomainService("TN_MPS1300");

        public XFMPS1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            dt_deliv.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dt_deliv.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
            checkEdit1.EditValue = "N";
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "일괄 생산계획 생성[F10]", IconImageList.GetIconImage("actions/addfile"));
            MasterGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "DelivSeq", true);
            MasterGridExControl.MainGrid.AddColumn("_Check", "선택");
            MasterGridExControl.MainGrid.AddColumn("DelivSeq", "납품계획번호");
            MasterGridExControl.MainGrid.AddColumn("DelivDate", "납기일", HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("DelivQty", "납기수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("DelivId", "납품계획자");
            MasterGridExControl.MainGrid.AddColumn("DelivInsDate", "납품계획생성일", HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderDate", "수주일", HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderCust", "수주처");
            MasterGridExControl.MainGrid.AddColumn("PlanYn", "생산계획완료여부", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("DelivSeq", false);
            DetailGridExControl.MainGrid.AddColumn("PlanNo", "생산계획번호");
            //DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드번");
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("PlanQty", "생산계획수량");
            DetailGridExControl.MainGrid.AddColumn("PlanStartdt", "생산계획시작일");
            DetailGridExControl.MainGrid.AddColumn("PlanEnddt", "생산계획종료일");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            DetailGridExControl.MainGrid.AddColumn("WorkorderYn", "작업지시여부", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanQty", "PlanStartdt", "PlanEnddt", "Memo");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "PlanQty", "PlanStartdt", "PlanEnddt");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanStartdt");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanEnddt");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PlanQty");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");

            DetailGridExControl.MainGrid.Columns["PlanQty"].RealColumnEdit.EditValueChanged += RealColumnEdit_EditValueChanged;
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("DelivSeq");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            //ModelServicePlan.ReLoad();

            string item=tx_item.EditValue.GetNullToEmpty();
            string PlanYnCheck = checkEdit1.EditValue.GetNullToEmpty();
            if (PlanYnCheck == "Y")
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.DelivDate >= dt_deliv.DateFrEdit.DateTime
                                                                                && p.DelivDate <= dt_deliv.DateToEdit.DateTime)
                                                                            && (p.TN_STD1100.ItemNm.Contains(item) || p.TN_STD1100.ItemNm1.Contains(item))
                                                                         )
                                                                         .OrderBy(p => p.DelivDate)
                                                                         .ToList();
            }
            else
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.DelivDate >= dt_deliv.DateFrEdit.DateTime
                                                                                && p.DelivDate <= dt_deliv.DateToEdit.DateTime)
                                                                            && (p.TN_STD1100.ItemNm.Contains(item) || p.TN_STD1100.ItemNm1.Contains(item))
                                                                            && (p.PlanYn == PlanYnCheck)
                                                                         )
                                                                         .OrderBy(p => p.DelivDate)
                                                                         .ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            VI_MPSPLAN_LIST obj = MasterGridBindingSource.Current as VI_MPSPLAN_LIST;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            //ModelServicePlan.Save();
            //DetailGridBindingSource.DataSource = ModelServicePlan.GetList(p => p.DelivSeq == obj.DelivSeq).OrderBy(o => o.PlanNo).ToList();
            DetailGridBindingSource.DataSource = obj.TN_MPS1300List.OrderBy(o => o.PlanNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            //ModelServicePlan.Save();
            ModelService.Save();
            DataLoad();
        }

        //일괄 생산계획 생성
        protected override void FileChooseClicked()
        {
            if (MasterGridBindingSource == null) return;
            var MasterObjList = MasterGridBindingSource.List as List<VI_MPSPLAN_LIST>;
            var CheckList = MasterObjList.Where(p => p._Check == "Y").ToList();
            if (CheckList.Count == 0) return;

            PopupDataParam param = new PopupDataParam();
            IPopupForm form;
            param.SetValue(PopupParameter.KeyValue, CheckList);
            form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFMPS1100, param, AddDetailRowCallBack);
            form.ShowPopup(true);
        }

        private void AddDetailRowCallBack(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            else ActRefresh();
        }

        protected override void DetailAddRowClicked()
        {
            VI_MPSPLAN_LIST obj = MasterGridBindingSource.Current as VI_MPSPLAN_LIST;
            if (obj == null) return;

            if (obj.PlanYn != "Y")
            {
                var DetailList = DetailGridBindingSource.List as List<TN_MPS1300>;
                var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();

                if(DetailList.Sum(p => p.PlanQty).GetIntNullToZero() >= obj.DelivQty)
                {
                    MessageBoxHandler.Show("이미 납품계획수량 만큼 생산계획에 반영되었습니다.");
                    return;
                }

                TN_MPS1300 newobj = new TN_MPS1300()
                {
                    DelivDate = obj.DelivDate,
                    DelivSeq = obj.DelivSeq,
                    PlanNo = DbRequesHandler.GetRequestNumber("WP"),
                    PlanQty = obj.DelivQty - DetailList.Sum(p=>p.PlanQty).GetIntNullToZero(),
                    ItemCode = obj.ItemCode,
                    OrderNo = obj.OrderNo,
                    WorkorderYn = "N",
                    TN_STD1100 = ItemObj
                };
                DetailGridBindingSource.Add(newobj);
                //ModelServicePlan.Insert(newobj);
                obj.TN_MPS1300List.Add(newobj);
            }
            else
            {
                MessageBoxHandler.Show("이미 납품계획수량 만큼 생산계획에 반영되었습니다.");
            }
        }

        protected override void DeleteDetailRow()
        {
            VI_MPSPLAN_LIST MasterObj = MasterGridBindingSource.Current as VI_MPSPLAN_LIST;
            if (MasterObj == null) return;
            TN_MPS1300 obj = DetailGridBindingSource.Current as TN_MPS1300;
            if (obj == null) return;

            if (obj.WorkorderYn == "Y")
            {
                MessageBoxHandler.Show("작업지시 완료된 계획은 삭제할수 없습니다.");

            }
            else if(obj.MPS1400List.Count > 0)
            {
                MessageBoxHandler.Show("작업지시 항목이 존재합니다. 작업지시 삭제 후 진행해 주시기 바랍니다.");
            }
            else
            {
                DetailGridBindingSource.RemoveCurrent();
                //ModelServicePlan.Delete(obj);
                MasterObj.TN_MPS1300List.Remove(obj);
            }
        }
        
        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_MPS1300 obj = DetailGridBindingSource.Current as TN_MPS1300;
            
            if (obj.WorkorderYn == "Y")
            {
                //MessageBoxHandler.Show("작업지시 완료된 계획은 수정 할 수 없습니다.");
                //e.Cancel = true;
            }
        }

        private void RealColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            var view = DetailGridExControl.MainGrid.MainView;
            var FieldName = view.FocusedColumn.FieldName;
            var MasterObj = MasterGridBindingSource.Current as VI_MPSPLAN_LIST;
            var DetailObj = DetailGridBindingSource.Current as TN_MPS1300;
            if (FieldName == "PlanQty")
            {
                DetailObj.PlanQty = view.ActiveEditor.EditValue.GetIntNullToZero();
                var DetailList = DetailGridBindingSource.List as List<TN_MPS1300>;

                if (DetailList.Sum(p => p.PlanQty).GetDecimalNullToZero() > MasterObj.DelivQty)
                {
                    MessageBoxHandler.Show("생산계획수량은 납품계획수량보다 많을 수 없습니다.", "경고");
                    view.ActiveEditor.EditValue = view.ActiveEditor.OldEditValue;
                }
            }
        }

    }
}
