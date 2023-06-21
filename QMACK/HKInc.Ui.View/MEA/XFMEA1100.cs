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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.MEA
{
    /// <summary>
    /// 설비이력관리
    /// </summary>
    public partial class XFMEA1100 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate // 2022-04-14 김진우 변경  ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
       
        public XFMEA1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;       // 2022-04-14 김진우 추가

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비명");
            MasterGridExControl.MainGrid.AddColumn("ModelNo", "모델번호");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조회사");
            MasterGridExControl.MainGrid.AddColumn("InstallDate", "설치일");
            MasterGridExControl.MainGrid.AddColumn("SerialNo", "일련번호");
            MasterGridExControl.MainGrid.AddColumn("CheckTurn", "점검주기", DevExpress.Utils.HorzAlignment.Far, true);
            MasterGridExControl.MainGrid.AddColumn("NextCheck", "다음점검일", DevExpress.Utils.HorzAlignment.Far, true);

            // 2022-04-18 김진우 수정
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.MainGrid.AddColumn("MachineCode", "MachineMCode", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("CheckDate", "검사일");
            DetailGridExControl.MainGrid.AddColumn("CheckDivision", "점검구분");
            DetailGridExControl.MainGrid.AddColumn("RepairScript", "수리내역");
            DetailGridExControl.MainGrid.AddColumn("Price", "금액");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번(도번)");
            DetailGridExControl.MainGrid.AddColumn("CheckId", "점검자");
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckDivision", "RepairScript", "Price", "ItemCode", "CheckId", "Memo");

            // 2022-04-18 김진우 추가
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            SubDetailGridExControl.MainGrid.AddColumn("MachineCode", "MachineCode", false);
            SubDetailGridExControl.MainGrid.AddColumn("MSeq", "MSeq", false);
            SubDetailGridExControl.MainGrid.AddColumn("CheckSeq", "CheckSeq", false);
            SubDetailGridExControl.MainGrid.AddColumn("CheckDate", "CheckDate", false);
            SubDetailGridExControl.MainGrid.AddColumn("TN_MEA1002.CheckPosition", "점검위치");
            SubDetailGridExControl.MainGrid.AddColumn("TN_MEA1002.CheckList", "점검항목");
            SubDetailGridExControl.MainGrid.AddColumn("TN_MEA1002.CheckWay", "점검방법");
            SubDetailGridExControl.MainGrid.AddColumn("TN_MEA1002.Temp", "TN_MEA1002.Temp", false);
            SubDetailGridExControl.MainGrid.AddColumn("TN_MEA1002.CheckCycle", "점검주기");
            SubDetailGridExControl.MainGrid.AddColumn("TN_MEA1002.ManagementStandard", "관리기준");
            SubDetailGridExControl.MainGrid.AddColumn("CheckId", "CheckId", false);
            SubDetailGridExControl.MainGrid.AddColumn("PreventSeq", "PreventSeq", false);
            SubDetailGridExControl.MainGrid.AddColumn("CheckValue", "점검값");
            SubDetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckValue", "Memo");
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("NextCheck");
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Maker", MasterCode.GetMasterCode((int)MasterCodeEnum.Maker).ToList());
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("CheckTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());

            // 2022-04-19 김진우 추가
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDivision", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineStandardCheckDivision), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["RepairScript"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Price", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03" || p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
            //DetailGridExControl.MainGrid.MainView.Columns["ItemCode"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            // 기존 팝업에서 데이터 추가하는 방식에서 변경      2022-06-27 김진우
            //DetailGridExControl.MainGrid.MainView.Columns["ItemCode"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "ItemCode", UserRight.HasEdit);
            //DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            //DetailGridExControl.MainGrid.MainView.Columns["RepairScript"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "ItemCode", UserRight.HasEdit);

            // 2022-04-18 김진우 추가
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MEA1002.CheckPosition", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckPosition), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MEA1002.CheckList", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckList), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MEA1002.CheckWay", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckWay), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MEA1002.CheckCycle", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(SubDetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("MachineSeq");
            MasterGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.Clear();     // 2022-04-18 김진우 추가

            ModelService.ReLoad();
          
            string lMachinecode = tx_MachineCode.Text;
          
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(lMachinecode) ? true : (p.MachineName.Contains(lMachinecode) || p.MachineCode==lMachinecode))
                                                                         && p.SerialNo != "ETC").ToList();          //  ETC??

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        // 2022-04-19 김진우 수정
        protected override void MasterFocusedRowChanged()
        {
            TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (MasterObj == null) return;

            DetailGridBindingSource.DataSource = MasterObj.TN_MEA1001List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

            // 2022-04-18 김진우 추가
            if (MasterObj.TN_MEA1001List.Count == 0)
                SubDetailGridBindingSource.Clear();
            #region 이전 소스
            //TN_MEA1000 obj = MasterGridBindingSource.Current as TN_MEA1000;
            //DetailGridBindingSource.DataSource = obj.TN_MEA1100List.OrderBy(o=>o.CheckDate).ToList();
            //DetailGridExControl.DataSource = DetailGridBindingSource;
            //DetailGridExControl.BestFitColumns();
            //DetailGridRowLocator.SetCurrentRow();
            #endregion
        }

        protected override void DetailFocusedRowChanged()
        {
            TN_MEA1001 DetailObj = DetailGridBindingSource.Current as TN_MEA1001;
            if (DetailObj == null) return;

            // 2022-04-19 김진우 수정
            SubDetailGridBindingSource.DataSource = ModelService.GetChildList<TN_MEA1003>(p => p.MachineCode == DetailObj.MachineCode
                                                                                            && p.CheckDate == DetailObj.CheckDate
                                                                                            //&& p.MSeq == DetailObj.Seq // 20220510 오세완 차장 키삭제로 인하여 생략
                                                                                            && p.TN_MEA1002.Division == MasterCodeSTR.Machine_Preventive_Maintenance
                                                                                            ).ToList();

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        // 2022-04-19 김진우 수정
        protected override void DetailAddRowClicked()
        {
            TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (MasterObj == null) return;

            TN_MEA1001 Obj = new TN_MEA1001();
            Obj.MachineCode = MasterObj.MachineCode;
            Obj.Seq = MasterObj.TN_MEA1001List.Count == 0 ? 1 : MasterObj.TN_MEA1001List.Max(m => m.Seq) + 1;
            Obj.CheckDate = DateTime.Today;
            Obj.CheckId = HKInc.Utils.Common.GlobalVariable.LoginId;

            DetailGridBindingSource.Add(Obj);
            MasterObj.TN_MEA1001List.Add(Obj);
            DetailGridExControl.BestFitColumns();

            TN_MEA1001 tn1 = DetailGridBindingSource.Current as TN_MEA1001;
            DateTime dt = Convert.ToDateTime(tn1.CheckDate);
            switch (MasterObj.CheckTurn)
            {
                case "18":
                    MasterObj.NextCheck = dt.AddDays(1);
                    break;
                case "19":
                    MasterObj.NextCheck = dt.AddDays(7);
                    break;
                case "20":
                    MasterObj.NextCheck = dt.AddMonths(1);
                    break;
                case "21":
                    MasterObj.NextCheck = dt.AddYears(1);
                    break;
                case "22":
                    MasterObj.NextCheck = dt.AddMonths(2);
                    break;
                case "23":
                    MasterObj.NextCheck = dt.AddMonths(3);
                    break;
                case "24":
                    MasterObj.NextCheck = dt.AddMonths(4);
                    break;
                case "25":
                    MasterObj.NextCheck = dt.AddMonths(5);
                    break;
                case "26":
                    MasterObj.NextCheck = dt.AddMonths(6);
                    break;

            }
            MasterGridExControl.MainGrid.BestFitColumns();

            #region 이전소스
            //    if (!UserRight.HasEdit) return;

            //    TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            //    if (MasterObj != null)
            //    {
            //        TN_MEA1100 obj = new TN_MEA1100();
            //        obj.MachineSeq = MasterObj.TN_MEA1100List.Count == 0 ? 1 : MasterObj.TN_MEA1100List.Count + 1;
            //        obj.CheckDate = DateTime.Today;
            //        obj.MachineCode = MasterObj.MachineCode;

            //        DetailGridBindingSource.Add(obj);
            //        MasterObj.TN_MEA1100List.Add(obj);
            //        DetailGridBindingSource.MoveLast();
            //    }

            //    TN_MEA1100 tn1 = DetailGridBindingSource.Current as TN_MEA1100;
            //    DateTime dt = Convert.ToDateTime(tn1.CheckDate);
            //    switch (MasterObj.CheckTurn)
            //    {
            //        case "18":
            //            MasterObj.NextCheck = dt.AddDays(1);
            //            break;
            //        case "19":
            //            MasterObj.NextCheck = dt.AddDays(7);
            //            break;
            //        case "20":
            //            MasterObj.NextCheck = dt.AddMonths(1);
            //            break;
            //        case "21":
            //            MasterObj.NextCheck = dt.AddYears(1);
            //            break;
            //        case "22":
            //            MasterObj.NextCheck = dt.AddMonths(2);
            //            break;
            //        case "23":
            //            MasterObj.NextCheck = dt.AddMonths(3);
            //            break;
            //        case "24":
            //            MasterObj.NextCheck = dt.AddMonths(4);
            //            break;
            //        case "25":
            //            MasterObj.NextCheck = dt.AddMonths(5);
            //            break;
            //        case "26":
            //            MasterObj.NextCheck = dt.AddMonths(6);
            //            break;

            //    }
            //    MasterGridExControl.MainGrid.BestFitColumns();
            #endregion
        }

        // 2022-04-19 김진우 수정
        protected override void DeleteDetailRow()
        {
            TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEA1001 DetailObj = DetailGridBindingSource.Current as TN_MEA1001;
            if (MasterObj == null || DetailObj == null) return;

            //bool CheckMEA1003 = ModelService.GetChildList<TN_MEA1003>(p => p.MachineCode == DetailObj.MachineCode).Any(p => p.MSeq == DetailObj.Seq);
            bool CheckMEA1003 = ModelService.GetChildList<TN_MEA1003>(p => p.MachineCode == DetailObj.MachineCode).Any(p => p.CheckDate == DetailObj.CheckDate); // 20220510 오세완 차장 키조건을 삭제하여 수정처리

            if (CheckMEA1003)
            {
                MessageBoxHandler.Show("예방보전목록이 존재하여 삭제할수 없습니다.");
                return;
            }

            ModelService.RemoveChild<TN_MEA1001>(DetailObj);
            DetailGridBindingSource.Remove(DetailObj);
            //MasterObj.TN_MEA1001List.Remove(DetailObj);
            //DetailGridBindingSource.RemoveCurrent();

            #region 이전소스
            //TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            //TN_MEA1100 DetailObj = DetailGridBindingSource.Current as TN_MEA1100;

            //if (MasterObj == null || DetailObj == null) return;

            //MasterObj.TN_MEA1100List.Remove(DetailObj);
            //DetailGridBindingSource.RemoveCurrent();
            #endregion
        }

        /// <summary>
        /// 2022-04-18 김진우 추가
        /// </summary>
        protected override void SubDetailAddRowClicked()
        {
            TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEA1001 DetailObj = DetailGridBindingSource.Current as TN_MEA1001;
            if (MasterObj == null || DetailObj == null) return;

            if (DetailObj.CheckDivision != MasterCodeSTR.Machine_Preventive_Maintenance)
            {
                MessageBoxHandler.Show("설비이력목록에서 점검구분이 예방보전이 아닙니다.");
                return;
            }

            List<TN_MEA1002> MEA1002List = ModelService.GetChildList<TN_MEA1002>(p => p.MachineCode == DetailObj.MachineCode && p.Division == MasterCodeSTR.Machine_Preventive_Maintenance).ToList();

            foreach (var v in MEA1002List)
            {
                TN_MEA1003 SubObj = new TN_MEA1003();

                SubObj.MachineCode = MasterObj.MachineCode;
                SubObj.CheckSeq = v.CheckSeq;
                //SubObj.MSeq = DetailObj.Seq; // 20220510 오세완 차장 키조건을 삭제하여 수정 처리
                SubObj.CheckDate = (DateTime)DetailObj.CheckDate;
                SubObj.CheckId = DetailObj.CheckId;

                if (SubObj.TN_MEA1002 == null)
                    SubObj.TN_MEA1002 = ModelService.GetChildList<TN_MEA1002>(p => p.MachineCode == DetailObj.MachineCode
                                                                                && p.CheckSeq == SubObj.CheckSeq
                                                                                && p.Division == MasterCodeSTR.Machine_Preventive_Maintenance
                                                                                   ).FirstOrDefault();

                ModelService.InsertChild<TN_MEA1003>(SubObj);
                SubDetailGridBindingSource.Add(SubObj);
            }
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 2022-04-18 김진우 추가
        /// </summary>
        protected override void DeleteSubDetailRow()
        {
            TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEA1001 DetailObj = DetailGridBindingSource.Current as TN_MEA1001;
            TN_MEA1003 SubObj = SubDetailGridBindingSource.Current as TN_MEA1003;
            if (MasterObj == null || DetailObj == null | SubObj == null) return;

            ModelService.RemoveChild<TN_MEA1003>(SubObj);
            SubDetailGridBindingSource.Remove(SubObj);
        }

        protected override void DataSave()
        {
            // 2022-04-19 김진우 수정
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region 제거
            //foreach (var rowHandle in gridEx2.MainGrid.MainView.GetSelectedRows())
            //{
            //    string _ProcessCode = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckDate").GetNullToEmpty());
            //    string _CheckMemo = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckMemo").GetNullToEmpty();
            //    string _CheckId = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckId").GetNullToEmpty();

            //    if (_ProcessCode == null || _ProcessCode == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트" + Convert.ToInt32(rowHandle + 1) + "행의 점검일은 필수입력 사항입니다.");
            //        return;
            //    }

            //    if (_CheckMemo == null || _CheckMemo == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트 " + Convert.ToInt32(rowHandle + 1) + "행의 점검내용은 필수입력 사항입니다.");
            //        return;
            //    }

            //    if (_CheckId == null || _CheckId == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트 " + Convert.ToInt32(rowHandle + 1) + "행의 담당자는 필수입력 사항입니다.");
            //        return;
            //    }
            //}
            #endregion

            ModelService.Save();
            DataLoad();
        }

        #region 이벤트
        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                DateTime InstallDate = Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, View.Columns["InstallDate"]));
                object NextCheck = View.GetRowCellValue(e.RowHandle, View.Columns["NextCheck"]);
                object checkturn = View.GetRowCellValue(e.RowHandle, View.Columns["CheckTurn"]).GetNullToEmpty();
                //if (NextCheck == null)
                //{
                //    switch (checkturn.ToString())
                //    {
                //        case "18":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddDays(1));
                //            NextCheck = InstallDate.AddDays(1);
                //            break;
                //        case "19":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddDays(7));
                //            NextCheck = InstallDate.AddDays(7);
                //            break;
                //        case "20":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(1));
                //            NextCheck = InstallDate.AddMonths(1);
                //            break;
                //        case "21":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddYears(1));
                //            NextCheck = InstallDate.AddYears(1);
                //            break;
                //        case "22":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(2));
                //            NextCheck = InstallDate.AddMonths(2);
                //            break;
                //        case "23":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(3));
                //            NextCheck = InstallDate.AddMonths(3);
                //            break;
                //        case "24":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(4));
                //            NextCheck = InstallDate.AddMonths(4);
                //            break;
                //        case "25":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(5));
                //            NextCheck = InstallDate.AddMonths(5);
                //            break;
                //        case "26":
                //            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(6));
                //            NextCheck = InstallDate.AddMonths(6);
                //            break;
                //    }
                //}
                if (NextCheck.GetNullToEmpty() != "")
                {
                    if (Convert.ToDateTime(NextCheck).AddDays(-14) <= DateTime.Today)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.Name != "CheckDate") return;
            TN_MEA1000 tn = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEA1001 tn1 = DetailGridBindingSource.Current as TN_MEA1001;
            DateTime dt = Convert.ToDateTime(tn1.CheckDate);
            switch (tn.CheckTurn)
            {
                case "18":
                    tn.NextCheck = dt.AddDays(1);
                    break;
                case "19":
                    tn.NextCheck = dt.AddDays(7);
                    break;
                case "20":
                    tn.NextCheck = dt.AddMonths(1);
                    break;
                case "21":
                    tn.NextCheck = dt.AddYears(1);
                    break;
                case "22":
                    tn.NextCheck = dt.AddMonths(2);
                    break;
                case "23":
                    tn.NextCheck = dt.AddMonths(3);
                    break;
                case "24":
                    tn.NextCheck = dt.AddMonths(4);
                    break;
                case "25":
                    tn.NextCheck = dt.AddMonths(5);
                    break;
                case "26":
                    tn.NextCheck = dt.AddMonths(6);
                    break;
            }
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        #endregion
    }
}