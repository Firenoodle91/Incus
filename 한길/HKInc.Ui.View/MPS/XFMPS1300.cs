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
using HKInc.Service.Handler;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 작업지시관리화면
    /// </summary>
    public partial class XFMPS1300 : HKInc.Service.Base.ListMasterDetailDetailFormTemplate
    {
        IService<TN_MPS1300> ModelService = (IService<TN_MPS1300>)ProductionFactory.GetDomainService("TN_MPS1300");
   
        protected BindingSource temp = new BindingSource();
        public XFMPS1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            dp_plan.EditValue = DateTime.Today;
            dp_workstart.EditValue = DateTime.Today;
            dateDayMachine.SetFormat(DateFormat.Month);
            dateDayMachine.EditValue = DateTime.Today;
            dateDayMachine.EditValueChanged += DateDayMachine_EditValueChanged;

            btnWorkDisplayOrder.Click += BtnWorkDisplayOrder_Click;
        }
        
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WorkorderYn", "작업지시여부", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("PlanNo", "계획번호");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("PlanQty", "계획수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DelivDate", "납기일");
            MasterGridExControl.MainGrid.AddColumn("PlanStartdt", "계획시작일");
            MasterGridExControl.MainGrid.AddColumn("PlanEnddt", "계획종료일");
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고");
            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호", false);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "작업완료->작업중[Alt+R]");            
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            DetailGridExControl.MainGrid.AddColumn("JobStates", "상태");
            DetailGridExControl.MainGrid.AddColumn("PlanNo", "생산계획번호", false);
            DetailGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            DetailGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일");            
            DetailGridExControl.MainGrid.AddColumn("PSeq", "공정순서", HorzAlignment.Far, true);
            DetailGridExControl.MainGrid.AddColumn("Process", "공정");
            DetailGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            DetailGridExControl.MainGrid.AddColumn("PlanQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            //DetailGridExControl.MainGrid.AddColumn("OutProc", "외주여부", false);
            DetailGridExControl.MainGrid.AddColumn("DelivDate", "납기일", false);
            DetailGridExControl.MainGrid.AddColumn("OrderNo", "수주번호", false);
            DetailGridExControl.MainGrid.AddColumn("WorkId", "작업자", false);
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WorkDate","MachineCode", "WorkId", "Memo");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "WorkDate", "MachineCode");
       
            SubDetailGridExControl.SetToolbarVisible(false);
            gridEx4.SetToolbarVisible(false);

            SubDetailGridExControl.MainGrid.MainView.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gridEx4.MainGrid.MainView.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

        }
        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanStartdt");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanEnddt");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            //var MachineLookup = DetailGridExControl.MainGrid.Columns["MachineCode"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            //MachineLookup.View.OptionsView.ShowColumnHeaders = true;

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProc", "N");// DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory == "P03").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("PlanNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            gridEx4.MainGrid.Clear();
            ModelService.ReLoad();

            string dt = dp_plan.DateTime.ToString("yyyy-MM-dd").Replace("-","").Substring(0,6);
            string item = tx_item.EditValue.GetNullToEmpty();

            List<TN_MPS1300> mps1300 = ModelService.GetList(p => string.IsNullOrEmpty(item) ? true : (p.TN_STD1100.ItemNm.Contains(item) || p.TN_STD1100.ItemNm1.Contains(item))).ToList();
            MasterGridBindingSource.DataSource = mps1300.Where(p => p.PlanYYMM == dt).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            DataSet ds = DbRequesHandler.GetDataQury("exec SP_MPS1400_LIST '" + dt + "'");
            SubDetailGridBindingSource.DataSource = ds.Tables[0];
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();

            dateDayMachine.DateTime = dp_plan.DateTime;
            DataSet ds2 = DbRequesHandler.GetDataQury("exec USP_GET_MPS1400_MACHINE_DAY '" + dateDayMachine.DateTime.ToString("yyyy-MM-dd") + "'");
            gridEx4.DataSource = ds2.Tables[0];
            gridEx4.MainGrid.BestFitColumns();

            if (!IsFirstLoaded)
            {
                SubDetailGridExControl.MainGrid.MainView.Columns["설비코드"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                SubDetailGridExControl.MainGrid.MainView.Columns["설비명"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                SubDetailGridExControl.MainGrid.MainView.Columns["품번"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                SubDetailGridExControl.MainGrid.MainView.Columns["품명"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                SubDetailGridExControl.MainGrid.MainView.Columns["공정"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                gridEx4.MainGrid.MainView.Columns["설비코드"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridEx4.MainGrid.MainView.Columns["설비명"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridEx4.MainGrid.MainView.Columns["일일생산기준량"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                foreach (var col in SubDetailGridExControl.MainGrid.Columns.ToList())
                {
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.GroupFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n0";
                    col.GroupFormat.FormatString = "n0";
                }

                foreach (var col in gridEx4.MainGrid.Columns.ToList())
                {
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.GroupFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n0";
                    col.GroupFormat.FormatString = "n0";
                }
                SubDetailGridExControl.MainGrid.BestFitColumns();
                gridEx4.MainGrid.BestFitColumns();
            }
        }
        protected override void MasterFocusedRowChanged()
        {
            TN_MPS1300 obj = MasterGridBindingSource.Current as TN_MPS1300;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.MPS1400List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailFocusedRowChanged();
        }
        protected override void DetailFocusedRowChanged()
        {
            var obj = DetailGridBindingSource.Current as TN_MPS1400;
            if (obj == null) return;

            if (obj.JobStates == ((int)MasterCodeEnum.POP_Status_End).ToString())
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
            else
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
        }
        protected override void DetailAddRowClicked()
        {
            TN_MPS1300 obj = MasterGridBindingSource.Current as TN_MPS1300;
            if (obj == null) return;
            List<TN_MPS1000> process = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode && p.UseYn == "Y").OrderBy(o => o.ProcessSeq).ToList();
            if(process.Count == 0)
            {
                MessageBoxHandler.Show("제품별 표준공정이 등록되어 있지 않습니다.", "경고");
                return;
            }

            DateTime dt =Convert.ToDateTime(dp_workstart.EditValue);
            string workno = DbRequesHandler.GetRequestNumber("WNO");
            for (int i = 0; i < process.Count; i++)
            {   if (i != 0)
                {
                    dt = dt.AddDays(Convert.ToInt32(process[i].STD));
                }
                TN_MPS1400 newobj = new TN_MPS1400()
                {
                    WorkDate=dt,
                    WorkNo= workno,
                    PlanNo =obj.PlanNo,
                    PSeq=Convert.ToInt32(process[i].ProcessSeq),
                    Process= process[i].ProcessCode,
                    ItemCode=obj.ItemCode,
                    PlanQty=obj.PlanQty,
                    DelivDate=obj.DelivDate,
                    OrderNo=obj.OrderNo,
                    DelivSeq=obj.DelivSeq,
                    OutProc=process[i].OutProc,
                    JobStates=((int)MasterCodeEnum.POP_Status_Wait).ToString()

                    

                };
                DetailGridBindingSource.Add(newobj);
                obj.WorkorderYn = "Y";
                obj.MPS1400List.Add(newobj);
                
            }
          
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {
            TN_MPS1300 Tobj = MasterGridBindingSource.Current as TN_MPS1300;
            TN_MPS1400 obj = DetailGridBindingSource.Current as TN_MPS1400;

            if (Tobj == null || obj == null) return;
           
            List<TN_MPS1401> mps1401 = ModelService.GetChildList<TN_MPS1401>(p => p.WorkNo == obj.WorkNo).ToList();
            if (mps1401.Count >= 1)
            {
                MessageBox.Show("생산이 시작된 작업은 삭제할수 없습니다.");
            }
            else
            {
                var delList = new List<TN_MPS1400>();
                delList.AddRange(Tobj.MPS1400List.Where(p => p.WorkNo == obj.WorkNo).ToList());
                foreach(var v in delList)
                {
                    ModelService.RemoveChild<TN_MPS1400>(v);    // 2022-05-02 김진우 추가        작지삭제후 POP에 남아서 추가
                    //Tobj.MPS1400List.Remove(v);               // 2022-05-02 김진우 주석        MPS1400에서 삭제되지 않음
                    DetailGridBindingSource.Remove(v);
                    //ModelService.Update(Tobj);
                    //ModelService.RemoveChild<TN_MPS1400>(v);
                }


                if (Tobj.MPS1400List.Count == 0)
                {
                    Tobj.WorkorderYn = "N";
                }

                MasterGridExControl.MainGrid.BestFitColumns();
                DetailGridExControl.MainGrid.BestFitColumns();
            }

        }
        protected override void DetailFileChooseClicked()
        {
            var obj = DetailGridBindingSource.Current as TN_MPS1400;
            if (obj == null) return;
            obj.JobStates = ((int)MasterCodeEnum.POP_Status_Start).ToString();
            DetailGridExControl.MainGrid.MainView.RefreshData();
        }
        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            ModelService.Save();
            DataLoad();
        }
        private void DateDayMachine_EditValueChanged(object sender, EventArgs e)
        {
            gridEx4.MainGrid.Clear();
            DataSet ds2 = DbRequesHandler.GetDataQury("exec USP_GET_MPS1400_MACHINE_DAY '" + dateDayMachine.DateTime.ToString("yyyy-MM-dd") + "'");
            gridEx4.DataSource = ds2.Tables[0];
            gridEx4.MainGrid.BestFitColumns();
        }

        private void BtnWorkDisplayOrder_Click(object sender, EventArgs e)
        {
            MPS_Popup.XPFMPS1301 fm = new MPS_Popup.XPFMPS1301();
            fm.UserRight = UserRight;
            fm.ShowDialog();
        }
    }
}
