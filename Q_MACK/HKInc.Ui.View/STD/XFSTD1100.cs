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

namespace HKInc.Ui.View.STD
{
    public partial class XFSTD1100 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        public XFSTD1100()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
         //   luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            GridExControl.MainGrid.AddColumn("ItemCode","품목코드");
            GridExControl.MainGrid.AddColumn("ItemNm","품목명");
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번");       // 2021-12-21 김진우 주임 FALSE 제거
            GridExControl.MainGrid.AddColumn("fullName1", false);
            GridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            GridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");
            GridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
            //GridExControl.MainGrid.AddColumn("ItemGbn");
          //  GridExControl.MainGrid.AddColumn("Temp5", "팀");
            GridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            GridExControl.MainGrid.AddColumn("MiddleCategory","중분류");
            GridExControl.MainGrid.AddColumn("BottomCategory","차종");
            GridExControl.MainGrid.AddColumn("Spec1","규격1");
            GridExControl.MainGrid.AddColumn("Spec2","규격2");
            GridExControl.MainGrid.AddColumn("Spec3","규격3");
            GridExControl.MainGrid.AddColumn("Spec4", "규격4");
            GridExControl.MainGrid.AddColumn("Unit","단위");
            GridExControl.MainGrid.AddColumn("Weight","수량");
            GridExControl.MainGrid.AddColumn("SafeQty","안전재고수량");
            //GridExControl.MainGrid.AddColumn("Lctype","기종");
            GridExControl.MainGrid.AddColumn("Temp4","수입검사");
            //GridExControl.MainGrid.AddColumn("ProcInspyn");
            GridExControl.MainGrid.AddColumn("SrcCode", "원소재");
            GridExControl.MainGrid.AddColumn("SrcQty", "원소재사용량(mm)");
            GridExControl.MainGrid.AddColumn("MainMc", "메인설비");           
            GridExControl.MainGrid.AddColumn("ProcCnt", "공정수");

            GridExControl.MainGrid.AddColumn("UseYn","사용구분");
            GridExControl.MainGrid.AddColumn("Memo");           
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype,1), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
           // GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
         //   GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Lctype", DbRequesHandler.GetCommCode(MasterCodeSTR.lctype, 1), "Mcode", "Codename");
            //   GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Suppvencode", ModelService.GetChildList<TN_STD1400>(p => 1 == 1).ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp4", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode", ModelService.GetList(p => p.UseYn == "Y" ).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMc", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").OrderBy(o => o.MachineName).ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitGrid();                     // 2022-06-22 김진우 추가
            InitRepository();               // 2022-06-22 김진우 추가

            string cta = lupItemtype.EditValue.GetNullToEmpty();
            string tem = tx_itemname.EditValue.GetNullToEmpty();

            #region 이전소스
            //if (chk_UseYN.Checked == true)
            //{
            //    GridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta)&&(string.IsNullOrEmpty(tem)?true:p.Temp5==tem))
            //                                                .OrderBy(p => p.ItemNm)
            //                                              .ToList();
            //}
            //else
            //{
            //    GridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text)|| p.ItemNm1.Contains(tx_itemname.Text)) &&
            //                                                              (p.UseYn == "Y") && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta) && (string.IsNullOrEmpty(tem) ? true : p.Temp5 == tem))
            //                                              .OrderBy(p => p.ItemNm)
            //                                            .ToList();
            //}

            //if (chk_UseYN.Checked == true)
            //{
            //    GridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta))
            //                                                .OrderBy(p => p.ItemNm)
            //                                              .ToList();
            //}
            //else
            //{
            //    GridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) &&
            //                                                              (p.UseYn == "Y") && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta))
            //                                              .OrderBy(p => p.ItemNm)
            //                                            .ToList();
            //}
            #endregion

            string useyn = "";
            if (chk_UseYN.Checked == false)
                useyn = "Y";

            GridBindingSource.DataSource = ModelService.GetList(p
                => (string.IsNullOrEmpty(tem) ? true : (p.ItemCode.Contains(tem) || (p.ItemNm.Contains(tem) || p.ItemNm1.Contains(tem) )))
                && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta)
                && (string.IsNullOrEmpty(useyn) ? true : p.UseYn == useyn)

                ).OrderBy(p => p.ItemNm).ToList();


            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            TN_STD1100 obj = GridBindingSource.Current as TN_STD1100;

            if (obj != null)
            {

                GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYn", "N");
                obj.UseYn = "N";



                ModelService.Update(obj);


            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            //return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1100, param, PopupRefreshCallback);
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1100_V2, param, PopupRefreshCallback); // 20220426 오세완 차장 소재사용량때문에 팝업 변경 
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

    }
}