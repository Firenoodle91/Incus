using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.STD
{
    /// <summary>
    /// 단가이력관리
    /// 2022-06-20 김진우 수정
    /// </summary>
    public partial class XFSTD1120 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1120> ModelService = (IService<TN_STD1120>)ProductionFactory.GetDomainService("TN_STD1120");

        public XFSTD1120()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            rg_CustomerUseflag.SetLabelText("사용", "미사용", "전체");

            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetail_CellValueChanged;
            lup_TopCategory.EditValueChanged += Lup_TopCategory_EditValueChanged;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" &&
                                                                                                      (p.TopCategory != MasterCodeSTR.Topcategory_Consumable &&
                                                                                                       p.TopCategory != MasterCodeSTR.Topcategory_SPARE &&
                                                                                                       p.TopCategory != MasterCodeSTR.Topcategory_Sub_Meterial)).ToList());

            lup_TopCategory.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1).Where(p => p.Codename != MasterCodeSTR.Topcategory_Consumable &&
                                                                                                                                     p.Codename != MasterCodeSTR.Topcategory_SPARE &&
                                                                                                                                     p.Codename != MasterCodeSTR.Topcategory_Sub_Meterial).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품명1");
            MasterGridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            MasterGridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");
            MasterGridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "소분류");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");
            MasterGridExControl.MainGrid.AddColumn("Unit", "단위");
            MasterGridExControl.MainGrid.AddColumn("Weigth", "수량");
            MasterGridExControl.MainGrid.AddColumn("SafeQty", "안전재고수량");
            MasterGridExControl.MainGrid.AddColumn("Temp4", "수입검사");
            MasterGridExControl.MainGrid.AddColumn("SrcCode", "원소재");
            MasterGridExControl.MainGrid.AddColumn("SrcQty", "원소재사용량(mm)");
            MasterGridExControl.MainGrid.AddColumn("MainMc", "메인설비");
            MasterGridExControl.MainGrid.AddColumn("ProcCnt", "공정수");
            MasterGridExControl.MainGrid.AddColumn("UseYn", "사용구분");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.MainGrid.AddColumn("CustomerCode", "거래처코드");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1400.CustomerName", "거래처명");
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "UseYn");

            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            SubDetailGridExControl.MainGrid.AddColumn("Seq", false);
            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            SubDetailGridExControl.MainGrid.AddColumn("CustomerCode", false);
            SubDetailGridExControl.MainGrid.AddColumn("StartDate", "변경시작일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("EndDate", "변경종료일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("CuttingCost", "컷팅비", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            SubDetailGridExControl.MainGrid.AddColumn("ChamferCost", "면취비", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            SubDetailGridExControl.MainGrid.AddColumn("ManufacturingCost", "가공비", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            SubDetailGridExControl.MainGrid.AddColumn("TotalCost", "단가(합계)", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            SubDetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "StartDate", "EndDate", "CuttingCost", "ChamferCost", "ManufacturingCost", "TotalCost", "Memo");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMc", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").OrderBy(o => o.MachineName).ToList(), "MachineCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");

            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("StartDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("EndDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("CuttingCost");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ChamferCost");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ManufacturingCost");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("TotalCost", DefaultBoolean.Default, "n2", true, false);
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx3, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var topCode = lup_TopCategory.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y"
                                                                                         && (itemCode == "" ? true : p.ItemCode == itemCode)
                                                                                         && (topCode == "" ? true : p.TopCategory == topCode)
                                                                                         && (p.TopCategory != MasterCodeSTR.Topcategory_Consumable &&
                                                                                             p.TopCategory != MasterCodeSTR.Topcategory_SPARE &&
                                                                                             p.TopCategory != MasterCodeSTR.Topcategory_Sub_Meterial)).OrderBy(o => o.ItemCode).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridRowLocator.GetCurrentRow();

            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            TN_STD1100 MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (MasterObj == null) return;

            var RadioValue = rg_CustomerUseflag.SelectedValue.GetNullToEmpty();

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == MasterObj.ItemCode
                                                                        && (RadioValue == "A" ? true : p.UseYn == RadioValue)).ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void DetailFocusedRowChanged()
        {
            TN_STD1100 MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            TN_STD1120 DetailObj = DetailGridBindingSource.Current as TN_STD1120;
            if (MasterObj == null || DetailObj == null) return;

            SubDetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1121>(p => p.ItemCode == MasterObj.ItemCode
                                                                                            && p.CustomerCode == DetailObj.CustomerCode)
                                                                                            .OrderBy(o => o.StartDate)
                                                                                            .ToList();

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }


        protected override void DetailAddRowClicked()
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1400, param, STD1400Callback);
            form.ShowPopup(true);
        }

        private void STD1400Callback(object sender, PopupArgument e)
        {
            if (e == null) return;
            var PopupData = (List<TN_STD1400>)e.Map.GetValue(PopupParameter.ReturnObject);

            TN_STD1100 MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            List<TN_STD1120> DetailList = DetailGridBindingSource.DataSource as List<TN_STD1120>;

            foreach (var v in PopupData)
            {
                if (ModelService.GetList(p => p.ItemCode == MasterObj.ItemCode).Any(a => a.CustomerCode == v.CustomerCode) || DetailList.Any(a => a.CustomerCode == v.CustomerCode))
                    MessageBoxHandler.Show("중복된 거래처입니다.");
                else
                {
                    TN_STD1120 DetailObj = new TN_STD1120();

                    DetailObj.ItemCode = MasterObj.ItemCode;
                    DetailObj.CustomerCode = v.CustomerCode;
                    DetailObj.UseYn = "Y";

                    ModelService.Insert(DetailObj);
                    DetailGridBindingSource.Add(DetailObj);
                }
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_STD1120 DetailObj = DetailGridBindingSource.Current as TN_STD1120;
            if (DetailObj == null) return;

            if (DetailObj.STD1121List.Count == 0)
            {
                MessageBoxHandler.Show("사용해제로 변경합니다.");
                DetailObj.UseYn = "N";
            }
        }

        protected override void SubDetailAddRowClicked()
        {
            TN_STD1120 DetailObj = DetailGridBindingSource.Current as TN_STD1120;
            if (DetailObj == null) return;

            TN_STD1121 AddObj = new TN_STD1121();

            AddObj.ItemCode = DetailObj.ItemCode;
            AddObj.Seq = DetailObj.STD1121List.Count == 0 ? 1 : DetailObj.STD1121List.Max(m => m.Seq) + 1;
            AddObj.CustomerCode = DetailObj.CustomerCode;
            AddObj.StartDate = DateTime.Today;
            AddObj.EndDate = Convert.ToDateTime(string.Format("2099-12-31"));       // 2022-06-20 김진우

            SubDetailGridBindingSource.Add(AddObj);
            ModelService.InsertChild(AddObj);
        }

        protected override void DeleteSubDetailRow()
        {
            TN_STD1121 SubObj = SubDetailGridBindingSource.Current as TN_STD1121;
            if (SubObj == null) return;

            ModelService.RemoveChild<TN_STD1121>(SubObj);
            SubDetailGridBindingSource.RemoveCurrent();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            #region 날짜 Check 
            //2022-06-20 김진우
            TN_STD1121 SubObj = SubDetailGridBindingSource.Current as TN_STD1121;
            if (SubObj != null)
            {
                List<TN_STD1121> SubList = SubDetailGridBindingSource.DataSource as List<TN_STD1121>;
                List<TN_STD1121> SubData = SubList.OrderBy(o => o.StartDate).ToList();      // 시간순 배열

                for (int i = 0; i < SubData.Count - 1; i++)
                {
                    // 시작일이 종료일보다 클경우 or 다음시작일이 이전 종료일보다 작을경우
                    if (SubData[i].StartDate > SubData[i].EndDate || SubData[i + 1].StartDate <= SubData[i].EndDate)
                    {
                        MessageBoxHandler.Show("시작일 및 종료일을 확인하여주십시오.");
                        return;
                    }
                    // 시작일 및 종료일이 입력이 지워지거나 입력되지 않은 경우
                    if (SubData[i].StartDate == null || SubData[i].EndDate == null)
                    {
                        MessageBoxHandler.Show("시작일 및 종료일을 입력하여주십시오.");
                        return;
                    }
                }
                // 마지막 날짜의 시작일이 종료일보다 클 경우
                if (SubData[SubData.Count -1].StartDate > SubData[SubData.Count - 1].EndDate)
                {
                    MessageBoxHandler.Show("시작일 및 종료일을 확인하여주십시오.");
                    return;
                }
            }
            #endregion

            ModelService.Save();
            DataLoad();
        }

        /// <summary>
        /// 시작일 종료일 변경된 행에서 검사 및 컷팅비+면취비+가공비 
        /// 2022-06-20 김진우
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubDetail_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_STD1121 SubObj = SubDetailGridBindingSource.Current as TN_STD1121;
            if (SubObj == null) return;

            List<TN_STD1121> SubList = SubDetailGridBindingSource.DataSource as List<TN_STD1121>;

            if (e.Column.Name == "StartDate" || e.Column.Name == "EndDate")     // 시작일 or 종료일
            {
                for (int i = 0; i < SubList.Count; i++)
                {
                    if (SubList[i].EndDate != Convert.ToDateTime(string.Format("2099-12-31")) && (Convert.ToDateTime(e.Value) >= SubList[i].StartDate && Convert.ToDateTime(e.Value) <= SubList[i].EndDate) && (SubList[i].RowId != SubObj.RowId))
                    {
                        MessageBoxHandler.Show("시작일 및 종료일을 확인하여주십시오.");
                        SubObj.StartDate = null;
                        SubObj.EndDate = Convert.ToDateTime(string.Format("2099-12-31"));       // null 값을 넣을경우  마지막날짜를 확인하기 어려워 변경     2022-07-08 김진우
                    }
                }
                if (e.Column.Name == "StartDate")       // 시작일
                {
                    if (SubList.Count != 1)     // 카운트에서 -2를 하는데 오류나서 추가
                    {
                        if (SubList[SubList.Count - 2].EndDate == Convert.ToDateTime(string.Format("2099-12-31")))
                        {
                            SubList[SubList.Count - 2].EndDate = SubList[SubList.Count - 1].StartDate.Value.AddDays(-1);
                        }
                        if (Convert.ToDateTime(e.Value) > SubObj.EndDate)
                        {
                            MessageBoxHandler.Show("시작일을 확인하여주십시오.");
                            SubObj.StartDate = null;
                        }
                    }
                }
                else if (e.Column.Name == "EndDate")        // 종료일
                {
                    if (Convert.ToDateTime(e.Value) < SubObj.StartDate)
                    {
                        MessageBoxHandler.Show("종료일을 확인하여주십시오.");
                        SubObj.EndDate = null;
                    }
                }
            }

            // 컷팅비 + 면취비 + 가공비
            else if (e.Column.FieldName == "CuttingCost" || e.Column.FieldName == "ChamferCost" || e.Column.FieldName == "ManufacturingCost")
                SubObj.TotalCost = SubObj.CuttingCost.GetDecimalNullToZero() + SubObj.ChamferCost.GetDecimalNullToZero() + SubObj.ManufacturingCost.GetDecimalNullToZero();
        }

        private void Lup_TopCategory_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit slup = sender as SearchLookUpEdit;
            if (slup != null)
            {
                List<TN_STD1100> tempArr = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" &&
                                                                                     (p.TopCategory != MasterCodeSTR.Topcategory_Consumable &&
                                                                                      p.TopCategory != MasterCodeSTR.Topcategory_SPARE &&
                                                                                      p.TopCategory != MasterCodeSTR.Topcategory_Sub_Meterial)).ToList();

                if (tempArr != null)
                    if (tempArr.Count > 0)
                    {
                        string sTopcategory = slup.EditValue.GetNullToEmpty();
                        if (sTopcategory != "")
                            tempArr = tempArr.Where(p => p.TopCategory == sTopcategory).ToList();

                        lup_Item.SetDefault(true, "ItemCode", "ItemNm", tempArr);
                    }
            }
        }

        #region 이전소스
 
        /*
        #region 번역변수
        IService<TN_STD1120> ModelService = (IService<TN_STD1120>)ProductionFactory.GetDomainService("TN_STD1120");
 
        /// <summary>
        /// tn_std1120 insert 처리
        /// </summary>
        List<TN_STD1120> ins_std1120_Arr = new List<TN_STD1120>();
        #endregion
 
        public XFSTD1120()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
 
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged; ;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetailMainView_CellvalueChanged;
            lup_TopCategory.EditValueChanged += Lup_TopCategory_EditValueChanged;
        }
 
        private void Lup_TopCategory_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit slup = sender as SearchLookUpEdit;
            if (slup != null)
            {
                List<TN_STD1100> tempArr = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" &&
                                                                                     (p.TopCategory != MasterCodeSTR.Topcategory_Consumable &&
                                                                                      p.TopCategory != MasterCodeSTR.Topcategory_SPARE &&
                                                                                      p.TopCategory != MasterCodeSTR.Topcategory_Sub_Meterial)).ToList();
 
                if (tempArr != null)
                    if (tempArr.Count > 0)
                    {
                        string sTopcategory = slup.EditValue.GetNullToEmpty();
                        if (sTopcategory != "")
                            tempArr = tempArr.Where(p => p.TopCategory == sTopcategory).ToList();
 
                        lup_Item.SetDefault(true, "ItemCode", "ItemNm", tempArr);
                    }
 
            }
        }
 
        private void DetailView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TP_XFSTD1120_CUSTOMER_LIST detailObj = DetailGridBindingSource.Current as TP_XFSTD1120_CUSTOMER_LIST;
            if (detailObj == null)
                return;
 
            if (e.Column.FieldName == "USE_YN")
                detailObj.Type = "Update";
        }
 
 
        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" &&
                                                                                                      (p.TopCategory != MasterCodeSTR.Topcategory_Consumable &&
                                                                                                       p.TopCategory != MasterCodeSTR.Topcategory_SPARE &&
                                                                                                       p.TopCategory != MasterCodeSTR.Topcategory_Sub_Meterial)).ToList());
 
            lup_TopCategory.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1).Where(p => p.Codename != MasterCodeSTR.Topcategory_Consumable &&
                                                                                                                                     p.Codename != MasterCodeSTR.Topcategory_SPARE &&
                                                                                                                                     p.Codename != MasterCodeSTR.Topcategory_Sub_Meterial).ToList());
        }
 
        protected override void InitGrid()
        {
            #region 품목목록
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품명1");
            MasterGridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            MasterGridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");
 
            MasterGridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "소분류");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
 
            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");
            MasterGridExControl.MainGrid.AddColumn("Unit", "단위");
            MasterGridExControl.MainGrid.AddColumn("Weigth", "수량");
 
            MasterGridExControl.MainGrid.AddColumn("SafeQty", "안전재고수량");
            MasterGridExControl.MainGrid.AddColumn("Temp4", "수입검사");
            MasterGridExControl.MainGrid.AddColumn("SrcCode", "원소재");
            MasterGridExControl.MainGrid.AddColumn("SrcQty", "원소재사용량(mm)");
            MasterGridExControl.MainGrid.AddColumn("MainMc", "메인설비");
 
            MasterGridExControl.MainGrid.AddColumn("ProcCnt", "공정수");
            MasterGridExControl.MainGrid.AddColumn("UseYn", "사용구분");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            #endregion
 
            #region 거래처 목록
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.MainGrid.AddColumn("CUSTOMER_CODE", "거래처코드");
            DetailGridExControl.MainGrid.AddColumn("CUSTOMER_NAME", "거래처명");
            DetailGridExControl.MainGrid.AddColumn("USE_YN", "사용여부");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "USE_YN");
            #endregion
 
            #region 단가 목록
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            SubDetailGridExControl.MainGrid.AddColumn("Seq", false);
            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            SubDetailGridExControl.MainGrid.AddColumn("CustomerCode", false);
            SubDetailGridExControl.MainGrid.AddColumn("StartDate", "변경시작일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("EndDate", "변경종료일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
 
            SubDetailGridExControl.MainGrid.AddColumn("CuttingCost", "컷팅비", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            SubDetailGridExControl.MainGrid.AddColumn("ChamferCost", "면취비", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            SubDetailGridExControl.MainGrid.AddColumn("ManufacturingCost", "가공비", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            SubDetailGridExControl.MainGrid.AddColumn("TotalCost", "단가(합계)", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            SubDetailGridExControl.MainGrid.AddColumn("Memo", "메모");
 
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "StartDate", "EndDate", "CuttingCost", "ChamferCost", "ManufacturingCost", "TotalCost", "Memo");
            #endregion
 
        }
 
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMc", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").OrderBy(o => o.MachineName).ToList(), "MachineCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");
 
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("USE_YN", "N");
 
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("StartDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("EndDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("CuttingCost");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ChamferCost");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ManufacturingCost");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("TotalCost");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx3, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }
 
        protected override void InitControls()
        {
            base.InitControls();
            // 20220512 오세완 차장 거래처 사용여부를 사용하기 때문에 추가가 필요 
            rg_CustomerUseflag.SetLabelText("사용", "사용안함", "전부");
            rg_CustomerUseflag.SelectedValue = "A";
        }
 
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
 
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
 
            ModelService.ReLoad();
 
            InitCombo();
            InitRepository();
 
            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var topCode = lup_TopCategory.EditValue.GetNullToEmpty();
 
            List<TN_STD1100> item_Arr = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" &&
                                                                              (p.TopCategory != MasterCodeSTR.Topcategory_Consumable &&
                                                                               p.TopCategory != MasterCodeSTR.Topcategory_SPARE &&
                                                                               p.TopCategory != MasterCodeSTR.Topcategory_Sub_Meterial)).ToList();
            if (item_Arr == null)
                MasterGridBindingSource.Clear();
            else if (item_Arr.Count == 0)
                MasterGridBindingSource.Clear();
            else
            {
                item_Arr = item_Arr.Where(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode) &&
                                              (string.IsNullOrEmpty(topCode) ? true : p.TopCategory == topCode)).OrderBy(p => p.ItemCode).ToList();
                MasterGridBindingSource.DataSource = item_Arr;
            }
 
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            GridRowLocator.SetCurrentRow();
            ins_std1120_Arr.Clear();
        }
 
        protected override void MasterFocusedRowChanged()
        {
            DetailGridRowLocator.GetCurrentRow("CUSTOMER_CODE");
 
            //거래처 검색
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
 
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;
 
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", masterObj.ItemCode.GetNullToEmpty());
                SqlParameter sp_Useflag = new SqlParameter("@USE_FLAG", rg_CustomerUseflag.SelectedValue); // 20220512 오세완 차장 사용여부 추가 
 
                var vResult = context.Database.SqlQuery<TP_XFSTD1120_CUSTOMER_LIST>("USP_GET_XFSTD1120_CUSTOMER_LIST @ITEM_CODE, @USE_FLAG", sp_Itemcode, sp_Useflag).ToList();
                if (vResult == null)
                    DetailGridBindingSource.Clear();
                else if (vResult.Count == 0)
                    DetailGridBindingSource.Clear();
                else
                {
                    vResult = vResult.OrderBy(o => o.CUSTOMER_CODE).ToList();
                    DetailGridBindingSource.DataSource = vResult;
                }
 
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
                DetailGridRowLocator.SetCurrentRow();
            }
        }
 
        protected override void DetailFocusedRowChanged()
        {
            //거래처별 단가 검색
            SubDetailGridExControl.MainGrid.Clear();
 
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;
 
            TP_XFSTD1120_CUSTOMER_LIST detailObj = DetailGridBindingSource.Current as TP_XFSTD1120_CUSTOMER_LIST;
            if (detailObj == null)
                return;
 
            TN_STD1120 std1120 = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                           p.CustomerCode == detailObj.CUSTOMER_CODE &&
                                                           p.UseYn == detailObj.USE_YN).FirstOrDefault();
            if (std1120 == null)
                SubDetailGridBindingSource.Clear();
            else
            {
                if (std1120.STD1121List == null)
                    SubDetailGridBindingSource.Clear();
                else if (std1120.STD1121List.Count == 0)
                    SubDetailGridBindingSource.Clear();
                else
                {
                    List<TN_STD1121> sub_Arr = std1120.STD1121List.ToList();
                    SubDetailGridBindingSource.DataSource = sub_Arr.OrderBy(o => o.Seq).ToList();
                }
 
            }
 
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }
 
 
        protected override void DetailAddRowClicked()
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;
 
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1400, param, STD1400Callback);
            form.ShowPopup(true);
        }
 
        private void STD1400Callback(object sender, PopupArgument e)
        {
            if (e == null)
                return;
 
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;
 
            List<TN_STD1400> partList = (List<TN_STD1400>)e.Map.GetValue(PopupParameter.ReturnObject);
            if (partList != null)
                if (partList.Count > 0)
                {
                    bool bPassed = false;
                    if (rg_CustomerUseflag.SelectedValue == "A")
                    {
                        bPassed = true;
                    }
                    else
                    {
                        List<TN_STD1120> temp_Arr = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode).ToList();
                        int iCheck_Cnt = 0;
                        foreach (TN_STD1400 each in partList)
                        {
                            int iCount = temp_Arr.Where(p => p.CustomerCode == each.CustomerCode).Count();
                            if (iCount > 0)
                            {
                                iCheck_Cnt++;
                                break;
                            }
                        }
 
                        if (iCheck_Cnt == 0)
                            bPassed = true;
                    }
 
                    if (bPassed)
                    {
                        List<TP_XFSTD1120_CUSTOMER_LIST> detail_Arr = DetailGridBindingSource.List as List<TP_XFSTD1120_CUSTOMER_LIST>;
                        foreach (TN_STD1400 each in partList)
                        {
                            TP_XFSTD1120_CUSTOMER_LIST insertObj = new TP_XFSTD1120_CUSTOMER_LIST()
                            {
                                CUSTOMER_CODE = each.CustomerCode,
                                CUSTOMER_NAME = each.CustomerName,
                                USE_YN = "Y",
                                Type = "Insert"
                            };
 
                            int iCnt = 0;
                            if (detail_Arr != null)
                                if (detail_Arr.Count > 0)
                                    iCnt = detail_Arr.Where(p => p.CUSTOMER_CODE == insertObj.CUSTOMER_CODE).Count();
 
                            if (iCnt == 0)
                            {
                                if (detail_Arr == null)
                                    DetailGridBindingSource.DataSource = new List<TP_XFSTD1120_CUSTOMER_LIST>();
 
                                DetailGridBindingSource.Add(insertObj);
                                TN_STD1120 insertObj2 = new TN_STD1120()
                                {
                                    ItemCode = masterObj.ItemCode,
                                    CustomerCode = insertObj.CUSTOMER_CODE,
                                    UseYn = insertObj.USE_YN
                                };
                                // ModelService.Insert(insertObj2);
                                ins_std1120_Arr.Add(insertObj2);
                            }
                        }
 
                        if (partList.Count > 0)
                            SetIsFormControlChanged(true);
 
                        DetailGridExControl.BestFitColumns();
                    }
                    else
                    {
                        MessageBoxHandler.Show("중복되는 거래처가 있습니다.");
                    }
                }
        }
 
        protected override void SubDetailAddRowClicked()
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;
 
            TP_XFSTD1120_CUSTOMER_LIST detailObj = DetailGridBindingSource.Current as TP_XFSTD1120_CUSTOMER_LIST;
            if (detailObj == null)
                return;
 
            TN_STD1121 newObj = new TN_STD1121()
            {
                ItemCode = masterObj.ItemCode,
                NewRowFlag = "Y"
            };
 
            TN_STD1120 insert_std1120 = null;
            if (detailObj.Type == "Insert")
            {
                insert_std1120 = ins_std1120_Arr.Where(p => p.ItemCode == masterObj.ItemCode &&
                                                            p.CustomerCode == detailObj.CUSTOMER_CODE).FirstOrDefault();
            }
 
            else
            {
                insert_std1120 = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                           p.CustomerCode == detailObj.CUSTOMER_CODE).FirstOrDefault();
                detailObj.Type = "Update";
            }
 
 
            if (insert_std1120 != null)
            {
                newObj.CustomerCode = insert_std1120.CustomerCode;
                if (insert_std1120.STD1121List == null)
                    newObj.Seq = 1;
                else if (insert_std1120.STD1121List.Count == 0)
                    newObj.Seq = 1;
                else
                    newObj.Seq = insert_std1120.STD1121List.Max(m => m.Seq) + 1;
 
                if (newObj.Seq == 1)
                    newObj.StartDate = DateTime.Today;
                else if (newObj.Seq > 1)
                {
                    DateTime dtMax = Convert.ToDateTime(insert_std1120.STD1121List.Max(m => m.EndDate));
                    if (dtMax != null)
                        dtMax = dtMax.AddDays(1);
 
                    newObj.StartDate = dtMax;
                }
 
                newObj.TN_STD1120 = insert_std1120;
                insert_std1120.STD1121List.Add(newObj);
 
                if (newObj.Seq == 1)
                    SubDetailGridBindingSource.DataSource = new List<TN_STD1121>();
 
                SubDetailGridBindingSource.Add(newObj);
 
                SetIsFormControlChanged(true);
            }
 
            SubDetailGridExControl.BestFitColumns();
        }
 
        private void SubDetailMainView_CellvalueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;
 
            TP_XFSTD1120_CUSTOMER_LIST detailObj = DetailGridBindingSource.Current as TP_XFSTD1120_CUSTOMER_LIST;
            if (detailObj == null)
                return;
 
            TN_STD1121 subObj = SubDetailGridBindingSource.Current as TN_STD1121;
            if (subObj == null)
                return;
 
            TN_STD1120 std1120 = null;
            if (detailObj.Type == "Insert")
                std1120 = ins_std1120_Arr.Where(p => p.ItemCode == masterObj.ItemCode &&
                                                     p.CustomerCode == detailObj.CUSTOMER_CODE).FirstOrDefault();
            else
                std1120 = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                     p.CustomerCode == detailObj.CUSTOMER_CODE).FirstOrDefault();
 
            if (e.Column.FieldName == "StartDate")
            {
                if (std1120.STD1121List != null)
                    if (std1120.STD1121List.Count > 0)
                    {
                        bool bResult = DataCheck(std1120.STD1121List, subObj);
                        if (bResult)
                        {
                            MessageBoxHandler.Show("시작일이 중복되지 않게 처리");
                            subObj.StartDate = null;
                        }
                    }
            }
            else if (e.Column.FieldName == "EndDate")
            {
                if (subObj.StartDate > subObj.EndDate)
                {
                    MessageBoxHandler.Show("종료일은 시작일보다 전 일수 없음");
                    subObj.EndDate = null;
                }
            }
            else if (e.Column.FieldName == "CuttingCost" || e.Column.FieldName == "ChamferCost" || e.Column.FieldName == "ManufacturingCost")
            {
                subObj.TotalCost = subObj.CuttingCost.GetDecimalNullToZero() + subObj.ChamferCost.GetDecimalNullToZero() + subObj.ManufacturingCost.GetDecimalNullToZero();
            }
 
            if (subObj.NewRowFlag.GetNullToEmpty() != "Y")
                subObj.EditRowFlag = "Y";
 
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }
 
        private bool DataCheck(ICollection<TN_STD1121> list, TN_STD1121 obj)
        {
            bool bResult = false;
            var data = list.Where(x => x.Seq != obj.Seq &&
                                 ((x.EndDate != null && (x.StartDate <= obj.StartDate && obj.StartDate <= x.EndDate)) || (x.EndDate == null && (x.StartDate <= obj.StartDate || obj.StartDate <= x.EndDate)))).ToList();
 
            int cnt = data.Count;
            foreach (var s in data)
            {
                if (s.EndDate == null)
                {
                    DateTime endDt = obj.StartDate.Value.AddDays(-1);
                    if (s.StartDate > endDt)
                        return true;
                    else
                    {
                        s.EndDate = endDt;
                        cnt += -1;
                    }
                }
            }
 
            //과거값을 넣었을경우
            var data2 = list.Where(x => x.Seq != obj.Seq && obj.StartDate < x.StartDate).ToList();
            if (data2.Count > 0)
            {
                obj.EndDate = obj.StartDate;
            }
 
            if (cnt > 0)
                bResult = true;
 
            return bResult;
        }
 
        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
 
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();
 
            bool bResult = CheckData_BeforeSave();
            if (bResult)
            {
                SaveProcess();
                ModelService.Save();
                DataLoad();
            }
        }
 
        private bool CheckData_BeforeSave()
        {
            bool bResult = false;
            bool bArr_Empty = false;
            string sMessage = "";
 
            List<TN_STD1121> sub_Arr = SubDetailGridBindingSource.List as List<TN_STD1121>;
            if (sub_Arr == null)
                bArr_Empty = true;
            else if (sub_Arr.Count == 0)
                bArr_Empty = true;
 
            if (bArr_Empty)
            {
                TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
                if (masterObj != null)
                {
                    TP_XFSTD1120_CUSTOMER_LIST detailObj = DetailGridBindingSource.Current as TP_XFSTD1120_CUSTOMER_LIST;
                    if (detailObj != null)
                    {
                        TN_STD1120 std1120 = null;
                        if (detailObj.Type.GetNullToEmpty() == "Insert")
                            std1120 = ins_std1120_Arr.Where(p => p.ItemCode == masterObj.ItemCode &&
                                                                 p.CustomerCode == detailObj.CUSTOMER_CODE).FirstOrDefault();
                        else
                            std1120 = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                                p.CustomerCode == detailObj.CUSTOMER_CODE).FirstOrDefault();
 
                        if (std1120 != null)
                            if (std1120.STD1121List != null)
                                if (std1120.STD1121List.Count > 0)
                                {
                                    sub_Arr = std1120.STD1121List.ToList();
                                    bArr_Empty = false;
                                }
                    }
                }
            }
 
            if (!bArr_Empty)
            {
                foreach (TN_STD1121 each in sub_Arr)
                {
                    if (!each.StartDate.HasValue)
                    {
                        sMessage = "시작일을 입력하셔야 합니다.";
                        break;
                    }
                    else if (!each.EndDate.HasValue)
                    {
                        sMessage = "종료일을 입력하셔야 합니다.";
                        break;
                    }
                    else if (each.TotalCost.GetDecimalNullToZero() == 0)
                    {
                        sMessage = "단가를 0 이상 입력하셔야 합니다.";
                        break;
                    }
                }
            }
 
            if (sMessage != "")
                MessageBoxHandler.Show(sMessage);
            else
                bResult = true;
 
            return bResult;
        }
 
        private void SaveProcess()
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;
 
            List<TP_XFSTD1120_CUSTOMER_LIST> detail_Arr = DetailGridBindingSource.List as List<TP_XFSTD1120_CUSTOMER_LIST>;
            if (detail_Arr != null)
                if (detail_Arr.Count > 0)
                {
                    foreach (TP_XFSTD1120_CUSTOMER_LIST each in detail_Arr)
                    {
                        string sType = each.Type.GetNullToEmpty();
                        if (sType == "Update")
                        {
                            TN_STD1120 std1120 = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                                           p.CustomerCode == each.CUSTOMER_CODE).FirstOrDefault();
 
                            if (std1120 != null)
                            {
                                std1120.UseYn = each.USE_YN;
                                ModelService.Update(std1120);
                            }
                        }
                        else if (sType == "Insert")
                        {
                            TN_STD1120 find_std = ins_std1120_Arr.Where(p => p.ItemCode == masterObj.ItemCode &&
                                                                             p.CustomerCode == each.CUSTOMER_CODE).FirstOrDefault();
                            if (find_std != null)
                                ModelService.Insert(find_std);
                        }
                    }
                }
 
            bool bSub_Empty = false;
            List<TN_STD1121> sub_Arr = SubDetailGridBindingSource.List as List<TN_STD1121>;
            if (sub_Arr == null)
                bSub_Empty = true;
            else if (sub_Arr.Count == 0)
                bSub_Empty = true;
 
            if (bSub_Empty)
            {
                if (detail_Arr != null)
                    if (detail_Arr.Count > 0)
                    {
                        foreach (TP_XFSTD1120_CUSTOMER_LIST each in detail_Arr)
                        {
                            string sType_1 = each.Type.GetNullToEmpty();
                            TN_STD1120 std1120_1 = null;
                            if (sType_1 == "Insert")
                            {
                                std1120_1 = ins_std1120_Arr.Where(p => p.ItemCode == masterObj.ItemCode &&
                                                                       p.CustomerCode == each.CUSTOMER_CODE).FirstOrDefault();
                            }
                            else if (sType_1 == "Update")
                            {
                                std1120_1 = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                                      p.CustomerCode == each.CUSTOMER_CODE).FirstOrDefault();
                            }
 
                            if (std1120_1 != null)
                                if (std1120_1.STD1121List != null)
                                    if (std1120_1.STD1121List.Count > 0)
                                    {
                                        foreach (TN_STD1121 each_1121 in std1120_1.STD1121List)
                                        {
                                            if (each_1121.EditRowFlag.GetNullToEmpty() == "Y")
                                                ModelService.UpdateChild<TN_STD1121>(each_1121);
                                            else if (each_1121.NewRowFlag.GetNullToEmpty() == "Y")
                                                ModelService.InsertChild<TN_STD1121>(each_1121);
                                        }
                                    }
 
                        }
                    }
            }
            else
            {
                foreach (TN_STD1121 each in sub_Arr)
                {
                    if (each.EditRowFlag.GetNullToEmpty() == "Y")
                        ModelService.UpdateChild<TN_STD1121>(each);
                    else if (each.NewRowFlag.GetNullToEmpty() == "Y")
                        ModelService.InsertChild<TN_STD1121>(each);
                }
            }
 
        }
 
        protected override void DeleteDetailRow()
        {
            TP_XFSTD1120_CUSTOMER_LIST detailObj = DetailGridBindingSource.Current as TP_XFSTD1120_CUSTOMER_LIST;
            if (detailObj == null)
                return;
 
            string sMessage = "삭제가 불가합니다. 사용여부 해제 처리할까요?";
            DialogResult dr = MessageBoxHandler.Show(sMessage, "경고", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                detailObj.USE_YN = "N";
                if (detailObj.Type.GetNullToEmpty() != "Insert")
                    detailObj.Type = "Update";
 
                DetailGridExControl.MainGrid.BestFitColumns();
            }
        }
 
        protected override void DeleteSubDetailRow()
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;
 
            TP_XFSTD1120_CUSTOMER_LIST detailObj = DetailGridBindingSource.Current as TP_XFSTD1120_CUSTOMER_LIST;
            if (detailObj == null)
                return;
 
            TN_STD1121 subObj = SubDetailGridBindingSource.Current as TN_STD1121;
            if (subObj == null)
                return;
 
            TN_STD1120 temp_std1120 = null;
            if (detailObj.Type == "Insert")
                temp_std1120 = ins_std1120_Arr.Where(p => p.ItemCode == masterObj.ItemCode &&
                                                          p.CustomerCode == detailObj.CUSTOMER_CODE).FirstOrDefault();
            else
                temp_std1120 = ModelService.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                         p.CustomerCode == detailObj.CUSTOMER_CODE).FirstOrDefault();
 
            if (temp_std1120 != null)
                if (temp_std1120.STD1121List != null)
                    if (temp_std1120.STD1121List.Count > 0)
                        temp_std1120.STD1121List.Remove(subObj);
 
            SubDetailGridBindingSource.RemoveCurrent();
        }
        */
        #endregion
    }
}
