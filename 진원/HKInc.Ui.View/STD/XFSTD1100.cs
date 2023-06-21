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
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
         //   luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
        }
        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            GridExControl.MainGrid.AddColumn("ItemCode","품목코드");
            GridExControl.MainGrid.AddColumn("ItemNm","품목명");
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("fullName1", false);
            GridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            GridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");
            GridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
            //GridExControl.MainGrid.AddColumn("ItemGbn");
          //  GridExControl.MainGrid.AddColumn("Temp5", "팀");
            GridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            GridExControl.MainGrid.AddColumn("MiddleCategory","중분류");
            GridExControl.MainGrid.AddColumn("BottomCategory","차종");
            GridExControl.MainGrid.AddColumn("Spec1","선경");
            GridExControl.MainGrid.AddColumn("Spec2","외경");
            GridExControl.MainGrid.AddColumn("Spec3","자유고");
            GridExControl.MainGrid.AddColumn("Spec4", "권수");
            GridExControl.MainGrid.AddColumn("Unit","단위");
            GridExControl.MainGrid.AddColumn("Weight","수량");
            GridExControl.MainGrid.AddColumn("SafeQty","안전재고수량");
            GridExControl.MainGrid.AddColumn("Cost", "단가");
            //GridExControl.MainGrid.AddColumn("Lctype","기종");
            //  GridExControl.MainGrid.AddColumn("Temp4","수입검사");
            //GridExControl.MainGrid.AddColumn("ProcInspyn");
            GridExControl.MainGrid.AddColumn("SrcCode", "원소재");
            GridExControl.MainGrid.AddColumn("SrcQty", "원소재사용량(g)");
            GridExControl.MainGrid.AddColumn("MainMc", "메인설비");
            GridExControl.MainGrid.AddColumn("MoldCode", "금형코드");
            GridExControl.MainGrid.AddColumn("ProcCnt", "공정수");
            GridExControl.MainGrid.AddColumn("InspectLabelType", "(유/무)검사구분");

            GridExControl.MainGrid.AddColumn("UseYn","사용구분");
            GridExControl.MainGrid.AddColumn("Memo");           
        }
        protected override void InitRepository()
        {

            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype,1), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
           // GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
         //   GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Lctype", DbRequesHandler.GetCommCode(MasterCodeSTR.lctype, 1), "Mcode", "Codename");
            //   GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Suppvencode", ModelService.GetChildList<TN_STD1400>(p => 1 == 1).ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
         //   GridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp4", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode", ModelService.GetList(p => p.UseYn == "Y" ).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMc", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").OrderBy(o => o.MachineName).ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldCode", ModelService.GetChildList<TN_MOLD001>(x => x.UseYN == "Y").ToList(), "MoldCode", "MoldName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InspectLabelType", DbRequesHandler.GetCommCode(MasterCodeSTR.InspectLabelTyp, 1), "Mcode", "Codename");
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
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            //string tem = luptem.EditValue.GetNullToEmpty();
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
            string tem = luptem.EditValue.GetNullToEmpty();
            if (chk_UseYN.Checked == true)
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta) )
                                                            .OrderBy(p => p.ItemNm)
                                                          .ToList();
            }
            else
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) &&
                                                                          (p.UseYn == "Y") && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta))
                                                          .OrderBy(p => p.ItemNm)
                                                        .ToList();
            }
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
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1100, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

    }
}