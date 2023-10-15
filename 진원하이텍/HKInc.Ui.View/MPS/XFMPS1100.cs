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
namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS1100 :  HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_MPSPLAN_LIST> ModelService = (IService<VI_MPSPLAN_LIST>)ProductionFactory.GetDomainService("VI_MPSPLAN_LIST");
        IService<TN_MPS1300> ModelServicePlan = (IService<TN_MPS1300>)ProductionFactory.GetDomainService("TN_MPS1300");

        public XFMPS1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            dt_deliv.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dt_deliv.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_MPS1300 obj = DetailGridBindingSource.Current as TN_MPS1300;

            if (obj.WorkorderYn == "Y")
            {
                MessageBox.Show("작업지시 완료된 계획은 수정 할 수 없습니다.");
                e.Cancel = true;
            }
        }

        protected override void InitCombo()
        {
         //   luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
            //    lupitemCode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => (p.TopCategory != "P03") && p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList());
        }

       
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("DelivSeq", "납품계획번호");
            MasterGridExControl.MainGrid.AddColumn("DelivDate", "납품계획일");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");            
          //  MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Temp5", "팀");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Lctype", "기종");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            MasterGridExControl.MainGrid.AddColumn("DelivQty", "납품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("VI_PRODQTY_MST.Stockqty", "재고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("DelivId", "납품계획자");

            MasterGridExControl.MainGrid.AddColumn("PlanYn", "생산계획완료여부", true, HorzAlignment.Center);
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("DelivSeq", false);
            DetailGridExControl.MainGrid.AddColumn("PlanNo", "계획번호");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            DetailGridExControl.MainGrid.AddColumn("PlanQty", "계획수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("PlanStartdt", "계획시작일");
            DetailGridExControl.MainGrid.AddColumn("PlanEnddt", "계획종료일");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            DetailGridExControl.MainGrid.AddColumn("WorkorderYn", "작업지시여부",true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanQty", "PlanStartdt", "PlanEnddt", "Memo");

        }
        protected override void InitRepository()
        {
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanStartdt");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanEnddt");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
          //  MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
       //     MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("DelivSeq");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            

            ModelService.ReLoad();
            ModelServicePlan.ReLoad();
            string item=tx_item.EditValue.GetNullToEmpty();
          //  string tem = luptem.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.DelivDate >= dt_deliv.DateFrEdit.DateTime &&
                                                                                  p.DelivDate <= dt_deliv.DateToEdit.DateTime)&&
                                                                                  (string.IsNullOrEmpty(item) ? true :( p.ItemCode.Contains(item)||p.TN_STD1100.ItemNm.Contains(item)||p.TN_STD1100.ItemNm1.Contains(item)  )))
                                                                   .OrderBy(p => p.DelivDate)
                                                                   .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
        protected override void MasterFocusedRowChanged()
        {
            VI_MPSPLAN_LIST obj = MasterGridBindingSource.Current as VI_MPSPLAN_LIST;
            if (obj == null) return;
            ModelServicePlan.Save();
            DetailGridBindingSource.DataSource = ModelServicePlan.GetList(p => p.DelivSeq == obj.DelivSeq).OrderBy(o => o.PlanNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DetailAddRowClicked()
        {
            VI_MPSPLAN_LIST obj = MasterGridBindingSource.Current as VI_MPSPLAN_LIST;
            if (obj == null) return;
            if (obj.PlanYn != "Y")
            {
                TN_MPS1300 newobj = new TN_MPS1300()
                {
                    DelivDate = obj.DelivDate,
                    DelivSeq = obj.DelivSeq,
                    PlanNo = DbRequesHandler.GetRequestNumber("WP"),
                    PlanQty=obj.DelivQty,
                    ItemCode = obj.ItemCode,
                    OrderNo=obj.OrderNo,
                    WorkorderYn = "N",
                    PlanStartdt = DateTime.Today,
                    PlanEnddt = DateTime.Today
                };
                DetailGridBindingSource.Add(newobj);
                ModelServicePlan.Insert(newobj);
            }
            else
            {
                MessageBox.Show("이미 납품계획수량 만큼 생산계획에 반영되었습니다.");
            }
        }
        protected override void DeleteDetailRow()
        {
            TN_MPS1300 obj = DetailGridBindingSource.Current as TN_MPS1300;
            if (obj == null) return;
            if (obj.WorkorderYn == "Y")
            {
                MessageBox.Show("작업지시 완료된 계획은 삭제할수 없습니다.");

            }
            else {
                DetailGridBindingSource.RemoveCurrent();
                ModelServicePlan.Delete(obj);
            }
        }
        protected override void DataSave()
        {
            ModelServicePlan.Save();
            DataLoad();
        }

    }
}
