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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 작업지시관리
    /// </summary>
    public partial class XFMPS1300 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1300> ModelService = (IService<TN_MPS1300>)ProductionFactory.GetDomainService("TN_MPS1300");
        private bool Click_DetailGridDelete;        // 2022-03-11 김진우 추가
        #endregion

        public XFMPS1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            dp_plan.EditValue = DateTime.Today;
            dp_workstart.EditValue = DateTime.Today;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailView_ShowingEditor;            // 2022-03-15 김진우 추가

            Click_DetailGridDelete = false;             // 2022-03-11 김진우 추가
        }

        /// <summary>
        /// 2022-03-15 김진우 추가
        /// 대기인 상태와 외주인 작업지시만 수정가능하도록 수정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView GV = sender as GridView;

            if (Convert.ToInt32(GV.GetFocusedRowCellValue("JobStates")) != 32 && GV.GetFocusedRowCellValue("OutProc").GetNullToEmpty() != "Y")
            {
                e.Cancel = true;
                SetPopupFormMessage("대기상태와 외주인 작업지시만 수정가능합니다.");
            }
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            // 20220427 오세완 차장 이차장님 요청으로 추가 
            MasterGridExControl.MainGrid.AddColumn("DelivSeq", "납품계획번호");
            MasterGridExControl.MainGrid.AddColumn("PlanNo", "계획번호");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");

            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("PlanQty", "계획수량");
            MasterGridExControl.MainGrid.AddColumn("PlanStartdt", "계획시작일");
            MasterGridExControl.MainGrid.AddColumn("PlanEnddt", "계획종료일");
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고");

            MasterGridExControl.MainGrid.AddColumn("WorkorderYn", "작업지시여부");
            // 20220427 오세완 차장 필요 없어 보여서 생략 처리 
            //MasterGridExControl.MainGrid.AddColumn("OrderNo", false);
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanStartdt", "PlanEnddt");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일");
            DetailGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            DetailGridExControl.MainGrid.AddColumn("JobStates", "상태");           // 2022-03-15 김진우 false처리
            DetailGridExControl.MainGrid.AddColumn("PlanNo", false);
            DetailGridExControl.MainGrid.AddColumn("PSeq", "공정순서");
            DetailGridExControl.MainGrid.AddColumn("Process", "공정");
            DetailGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("PlanQty", "지시수량");
            DetailGridExControl.MainGrid.AddColumn("OutProc", "외주여부");
            DetailGridExControl.MainGrid.AddColumn("DelivDate", false);
            DetailGridExControl.MainGrid.AddColumn("OrderNo", false);
            DetailGridExControl.MainGrid.AddColumn("WorkId", "작업자");
            DetailGridExControl.MainGrid.AddColumn("EMType", LabelConvert.GetLabelText("EMType"));
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고");
            
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WorkDate", "OutProc", "MachineCode", "WorkId", "Memo", "EMType");

            SubDetailGridExControl.SetToolbarVisible(false);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanStartdt");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanEnddt");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("EMType", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProc", "N");// DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("JobStates", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            ModelService.ReLoad();

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            string item = tx_item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(item) ? true : (p.ItemCode.Contains(item) || p.TN_STD1100.ItemNm.Contains(item) || p.TN_STD1100.ItemNm1.Contains(item)))
                                                                        && ((p.PlanStartdt.Month == dp_plan.DateTime.Month && p.PlanStartdt.Year == dp_plan.DateTime.Year))
                                                                        ).ToList();

            string dt = dp_plan.EditValue.ToString().Replace("-", "").Substring(0, 6);
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();

            DataSet ds = DbRequestHandler.GetDataQury("exec  SP_MPS1400_LIST '" + dt + "'");
            SubDetailGridBindingSource.DataSource = ds.Tables[0];
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();

            GridRowLocator.SetCurrentRow();
            
            Click_DetailGridDelete = false;             // 2022-03-11 김진우 추가
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_MPS1300 obj = MasterGridBindingSource.Current as TN_MPS1300;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.MPS1400List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            TN_MPS1300 obj = MasterGridBindingSource.Current as TN_MPS1300;
            if (obj == null) return;

            List<TN_MPS1000> process = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode && p.UseYn == "Y").OrderBy(o => o.ProcessSeq).ToList();
            DateTime dt =Convert.ToDateTime(dp_workstart.EditValue);
            string workno = DbRequestHandler.GetRequestNumber("WNO");

            for (int i = 0; i < process.Count; i++)
            {
                if (i != 0)
                    dt = dt.AddDays(Convert.ToInt32(process[i].STD));

                TN_MPS1400 newobj = new TN_MPS1400()
                {
                    WorkDate    = dt,
                    WorkNo      = workno,
                    PlanNo      = obj.PlanNo,
                    PSeq        = Convert.ToInt32(process[i].ProcessSeq),
                    Process     = process[i].ProcessCode,
                    ItemCode    = obj.ItemCode,
                    PlanQty     = obj.PlanQty,
                    DelivDate   = obj.DelivDate,
                    OrderNo     = obj.OrderNo,
                    DelivSeq    = obj.DelivSeq,
                    OutProc     = process[i].OutProc,
                    JobStates   = ((int)MasterCodeEnum.POP_Status_Wait).ToString()
                };

                DetailGridBindingSource.Add(newobj);
                obj.WorkorderYn = "Y";
                obj.MPS1400List.Add(newobj);
            }
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_MPS1400 obj = DetailGridBindingSource.Current as TN_MPS1400;
            if (obj == null) return;
           
            TN_MPS1300 Tobj = MasterGridBindingSource.Current as TN_MPS1300;
            if (DialogResult.Yes == MessageBox.Show("일괄삭제인가요?", "알림", MessageBoxButtons.YesNo))
            {
                List<TN_MPS1401> mps1401 = ModelService.GetChildList<TN_MPS1401>(p => p.WorkNo == obj.WorkNo).ToList();
                if (mps1401.Count >= 1)
                {
                    MessageBox.Show("생산이 시작된 작업은 삭제할수 없습니다.");
                }
                else
                {
                    int j = Tobj.MPS1400List.Count;
                    for (int i = 0; i < j; i++)
                    {
                        TN_MPS1400 delobj = Tobj.MPS1400List.Last();
                        
                        Tobj.MPS1400List.Remove(delobj);
                        DetailGridBindingSource.Remove(delobj);
                        ModelService.Update(Tobj);
                        try
                        {
                            ModelService.RemoveChild<TN_MPS1400>(delobj);
                        }
                        catch { }
                    }
                    Tobj.WorkorderYn = "N";

                    ModelService.Update(Tobj);

                    MasterGridExControl.MainGrid.BestFitColumns();
                    DetailGridExControl.MainGrid.BestFitColumns();
                }
            }
            else
            {
                List<TN_MPS1401> mps1401 = ModelService.GetChildList<TN_MPS1401>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process).ToList();
                if (mps1401.Count >= 1)
                {
                    MessageBox.Show("생산이 시작된 공정 작업은 삭제할수 없습니다.");
                }
                else
                {
                    Tobj.MPS1400List.Remove(obj);
                    DetailGridBindingSource.Remove(obj);
                    ModelService.Update(Tobj);
                    try
                    {
                        ModelService.RemoveChild<TN_MPS1400>(obj);
                    }
                    catch { }
                }
                Tobj.WorkorderYn = "N";

                ModelService.Update(Tobj);

                MasterGridExControl.MainGrid.BestFitColumns();
                DetailGridExControl.MainGrid.BestFitColumns();
                Click_DetailGridDelete = true;          // 2022-03-11 김진우 추가
            }
        }

        protected override void DataSave()
        {
            ModelService.Save();
            Check_DetailGrid_ProcessSeq();              // 2022-03-11 김진우 추가
            DataLoad();
        }

        /// <summary>
        /// 20210521 오세완 차장                  2022-03-11 김진우 대영에 있던거 가져옴         MPS1200 => MPS1400으로 변경
        /// 작업지시현황에서 1개 이상의 작업을 삭제하였을때 공정순번을 순차적으로 맞춰주기 위한 프로시저 실행, entity는 key를 바꿀수가 없기 때문
        /// </summary>
        private void Check_DetailGrid_ProcessSeq()
        {
            var DetailObj = DetailGridBindingSource.Current as TN_MPS1400;
            if (DetailObj == null)
                return;

            if (Click_DetailGridDelete)
            {
                string sWorkno = DetailObj.WorkNo.GetNullToEmpty();
                if (sWorkno != "")
                {
                    string sSql = "EXEC USP_UPD_MPS1400_PROCESS_SEQ '" + sWorkno + "'";
                    DbRequestHandler.GetCellValue(sSql, 0);
                }
            }
        }


    }
}
