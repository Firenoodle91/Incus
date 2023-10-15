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
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Domain.TEMP;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 품목/단가 이력관리(외주)
    /// </summary>
    public partial class XFSTD1106 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        #region 전역변수
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        #endregion

        #region 생성자 
        public XFSTD1106()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += Detail_CellValueChanged;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetail_CellValueChanged;
        }
        #endregion

        #region 초기화
        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)).ToList());
            lup_TopCategory.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1).Where(p => p.CodeVal != MasterCodeSTR.TopCategory_SPARE && p.CodeVal != MasterCodeSTR.TopCategory_TOOL).ToList());
        }

        protected override void InitGrid()
        {

            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "n2");
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"), false);
            MasterGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            DetailGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));
            DetailGridExControl.MainGrid.AddColumn("CustomCustomerCode", LabelConvert.GetLabelText("CustomerName"), false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정코드", HorzAlignment.Near, true); 
            DetailGridExControl.MainGrid.AddColumn("StartDate", LabelConvert.GetLabelText("StartDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            DetailGridExControl.MainGrid.AddColumn("ItemCost", LabelConvert.GetLabelText("Cost"), false);
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            SubDetailGridExControl.MainGrid.AddColumn("NewRowFlag", "N", false);
            SubDetailGridExControl.MainGrid.AddColumn("EditRowFlag", "E", false);
            SubDetailGridExControl.MainGrid.AddColumn("DeleteRowFlag", "D", false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustomerCode", "ProcessCode");

            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", "ItemCode", false);
            SubDetailGridExControl.MainGrid.AddColumn("CustomerCode", "CustomerCode", false);

            SubDetailGridExControl.MainGrid.AddColumn("StartDate", "변경시작일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("EndDate", "변경종료일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정코드", HorzAlignment.Near, false);
            SubDetailGridExControl.MainGrid.AddColumn("ItemCost", "단가", HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            SubDetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            SubDetailGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            SubDetailGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "StartDate", "EndDate", "ItemCost", "Memo");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", ModelService.GetChildList<TN_STD1000>(p => p.CodeMain == "P001" && p.UseYN == "Y").ToList(), "ProcessCode", "ProcessName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("StartDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ItemCost");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            //SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", ModelService.GetChildList<TN_STD1000>(p => p.CodeMain == "P001" && p.UseYN == "Y").ToList(), "ProcessCode", "ProcessName");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));



            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
        }
        #endregion

        #region 이벤트
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitGrid();
            InitRepository();

            var topCode = lup_TopCategory.EditValue.GetNullToEmpty();
            var itemCode = lup_Item.EditValue.GetNullToEmpty();


            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y"
                                        && (p.TopCategory == MasterCodeSTR.TopCategory_WAN
                                            || p.TopCategory == MasterCodeSTR.TopCategory_BAN
                                            || p.TopCategory == MasterCodeSTR.TopCategory_MAT)
                                        && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                        && (string.IsNullOrEmpty(topCode) ? true : p.TopCategory == topCode)
                                        ).OrderBy(p => p.ItemCode).ToList();


            //MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
            //                                                            && (string.IsNullOrEmpty(customerCode) ? true : p.TN_STD1103List.Any(c => c.CustomerCode == customerCode))
            //                                                            && (p.UseFlag == "Y")
            //                                                            && (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)
            //                                                         )
            //                                                         .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var itemCode = masterObj.ItemCode;

            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1106_MASTER>(p => p.ItemCode == itemCode).OrderBy(p => p.ItemCode).ToList();

            //DetailGridBindingSource.DataSource = masterObj.TN_STD1106List.Where(p => string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
            //                                                             .GroupBy(p => new { p.CustomerCode})
            //                                                             .Select(p => new TN_STD1106_MASTER() { CustomerCode = p.Key.CustomerCode})
            //                                                             .ToList();
            //DetailGridBindingSource.DataSource = masterObj.TN_STD1106_MASTERList.Where(x => string.IsNullOrEmpty(itemCode) ? true : x.ItemCode == itemCode)
            //                                                                    .ToList();


            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

            SetRefreshMessage(DetailGridExControl);
        }

        protected override void DetailFocusedRowChanged()
        {
            SubDetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_STD1106_MASTER;
            if (detailObj == null) return;

            string itemcode = detailObj.ItemCode;
            string customercode = detailObj.CustomerCode;
            string processcode = detailObj.ProcessCode;

            SubDetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1106>(p => p.ItemCode == itemcode && 
                                                                                          p.CustomerCode == customercode &&
                                                                                          p.ProcessCode == processcode
                                                                           ).OrderBy(p => p.StartDate).ToList();

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
            SubDetailGridRowLocator.SetCurrentRow();
        }

        /// <summary>
        /// 상세내역 추가 이벤트
        /// </summary>
        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            List<TN_STD1106_MASTER> detailList = DetailGridBindingSource.DataSource as List<TN_STD1106_MASTER>;

            var newObj = new TN_STD1106_MASTER();

            newObj.NewRowFlag = "Y";
            //newObj.StartDate = DateTime now;
            newObj.ItemCode = masterObj.ItemCode;
            newObj.CustomerCode = "";

            masterObj.TN_STD1106_MASTERList.Add(newObj);
            DetailGridBindingSource.Add(newObj);
        }

        /// <summary>
        /// 단가 추가 이벤트
        /// </summary>
        protected override void SubDetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            var detailObj = DetailGridBindingSource.Current as TN_STD1106_MASTER;
            if (detailObj == null) return;

            List<TN_STD1106> subDetilList = SubDetailGridBindingSource.List as List<TN_STD1106>;

            //이전에 등록된 단가이력 있으면 종료일자 갱신 (현재일자로)
            var newObj = new TN_STD1106();
            newObj.Seq = subDetilList.Count == 0 ? 1 : subDetilList.Max(m => m.Seq) + 1;
            newObj.ItemCode = detailObj.ItemCode;
            newObj.ProcessCode = detailObj.ProcessCode;
            newObj.CustomerCode = detailObj.CustomerCode;
            newObj.ItemCost = 0;
            newObj.StartDate = null;
            newObj.EndDate = Convert.ToDateTime("2999-12-31");
            newObj.NewRowFlag = "Y";

            SubDetailGridBindingSource.Add(newObj);
            detailObj.TN_STD1106List.Add(newObj);
            SubDetailGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 저장처리
        /// </summary>
        protected override void DataSave()
        {
            List<TN_STD1106_MASTER> ListMst = DetailGridBindingSource.DataSource as List<TN_STD1106_MASTER>;
            List<TN_STD1106> List = SubDetailGridBindingSource.DataSource as List<TN_STD1106>;
            var masterList = MasterGridBindingSource.List as List<TN_STD1100>;

            DetailGridExControl.MainGrid.PostEditor();
            SubDetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridBindingSource.EndEdit();

            ModelService.Save();

            DataLoad();

            GridRowLocator.SetCurrentRow();
        }

        /// <summary>
        /// 상세내역 삭제
        /// </summary>
        protected override void DeleteDetailRow()
        {
            //거래처 모두 삭제
            var DetailObj = DetailGridBindingSource.Current as TN_STD1106_MASTER;
            if (DetailObj == null) return;

            DetailGridBindingSource.RemoveCurrent();
            ModelService.RemoveChild(DetailObj);
        }

        /// <summary>
        /// 단가이력 삭제
        /// </summary>
        protected override void DeleteSubDetailRow()
        {
            //거래처별 단가 삭제
            var DetailObj = DetailGridBindingSource.Current as TN_STD1106_MASTER;
            if (DetailObj == null) return;

            var SubDetailObj = SubDetailGridBindingSource.Current as TN_STD1106;
            if (SubDetailObj == null) return;
            GridView gv = SubDetailGridExControl.MainGrid.MainView as GridView;
            try
            {
                int irow = gv.FocusedRowHandle;
                gv.SetRowCellValue(irow - 1, gv.Columns["EndDate"], null);
            }
            catch { }
            SubDetailGridBindingSource.RemoveCurrent();
            ModelService.RemoveChild(SubDetailObj);
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                //거래처 등록이 되어 있으면 수정 불가
                var detailObj = DetailGridBindingSource.Current as TN_STD1106_MASTER;

                if (detailObj.NewRowFlag != "Y")
                {
                    e.Cancel = true;
                }
            }
            catch { }
        }

        private void Detail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            int iDupleCount = 0;

            List<TN_STD1106_MASTER> detailList = DetailGridBindingSource.DataSource as List<TN_STD1106_MASTER>;

            string fieldName = e.Column.FieldName;
            string processFieldname = view.Columns["ProcessCode"].FieldName;
            var checkCustomerCode = view.GetRowCellValue(view.FocusedRowHandle, view.Columns[fieldName]).GetNullToEmpty();
            var checkProcessCode = view.GetRowCellValue(view.FocusedRowHandle, view.Columns[processFieldname].GetNullToEmpty());
            int curRow = view.FocusedRowHandle;

            // 거래처가 - 공정이 없는 경우 return
            if (checkCustomerCode == "" && checkProcessCode == "") return;

            // 거래처 - 공정 중복 등록 확인
            foreach (var v in detailList)
            {
                if(checkCustomerCode == v.CustomerCode && checkProcessCode == v.ProcessCode && v.RowId == 0)
                {
                    iDupleCount++;
                }
            }
            if (iDupleCount > 1)
            {
                MessageBox.Show("한 품목에 동일 거래처, 동일 공정은 등록이 불가합니다");
                view.SetFocusedRowCellValue(fieldName, null);
                return;
            }
        }

        private void SubDetail_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            var DetailObj = DetailGridBindingSource.Current as TN_STD1106_MASTER;
            var SubDetailObj = SubDetailGridBindingSource.Current as TN_STD1106;
            if (MasterObj == null || SubDetailObj == null) return;
            GridView gv = sender as GridView;
            int irow = e.RowHandle;
            string column = e.Column.FieldName;

            if (column == "StartDate")
            {
                if (DataCheck(DetailObj.TN_STD1106List, SubDetailObj))
                {
                    MessageBox.Show("시작일은 중복이 되지 않습니다.");
                    SubDetailObj.StartDate = null;
                    return;
                }
                DateTime dt = Convert.ToDateTime(SubDetailObj.StartDate).AddDays(-1);

                for (int i = 0; i < irow; i++)
                {
                    if (gv.GetRowCellValue(i, gv.Columns["EndDate"]) == null)
                    {
                        gv.SetRowCellValue(i, gv.Columns["EndDate"], dt);

                    }
                }
            }
            else if (column == "EndDate")
            {
                if (SubDetailObj.StartDate > SubDetailObj.EndDate)
                {
                    MessageBox.Show("종료일은 시작일보다 이전일로 선택할 수 없습니다");
                    SubDetailObj.EndDate = null;
                    return;
                }
            }
            else if (column == "ItemCost")
            {
                DateTime dt = Convert.ToDateTime(SubDetailObj.StartDate).AddDays(-1);

                for (int i = 0; i < irow; i++)
                {
                    if (gv.GetRowCellValue(i, gv.Columns["EndDate"]) == null)
                    {
                        gv.SetRowCellValue(i, gv.Columns["EndDate"], dt);

                    }
                }
            }

            SubDetailGridExControl.MainGrid.BestFitColumns();
        }


        #endregion

        #region 사용자 정의 함수
        private bool DataCheck(ICollection<TN_STD1106> list, TN_STD1106 obj)
        {
            var data = list.Where(x => x.Seq != obj.Seq &&
                                 ((x.EndDate != null &&
                                 (x.StartDate <= obj.StartDate && obj.StartDate <= x.EndDate))
                                 || (x.EndDate == null
                                 && (x.StartDate <= obj.StartDate
                                 || obj.StartDate <= x.EndDate
                                 && obj.NewRowFlag == "Y"
                                 || obj.EditRowFlag == "Y")))).ToList();

            int cnt = data.Count;
            foreach (var s in data)
            {
                if (s.EndDate == null || s.EndDate == Convert.ToDateTime("2999-12-31"))
                {
                    DateTime endDt = obj.StartDate.Value.AddDays(-1);
                    if (s.StartDate > endDt)
                        return true;
                    else
                    {
                        s.EndDate = endDt;
                        cnt += -1;
                        s.EditRowFlag = "Y";
                    }
                }
            }

            //과거값을 넣었을경우
            var data2 = list.Where(x => x.Seq != obj.Seq && obj.StartDate < x.StartDate).ToList();
            if (data2.Count > 0)
            {
                obj.EndDate = obj.StartDate;
                obj.EditRowFlag = "Y";
            }

            if (cnt > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}