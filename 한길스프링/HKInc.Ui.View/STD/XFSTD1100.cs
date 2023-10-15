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
using HKInc.Service.Handler;

namespace HKInc.Ui.View.STD
{
    //품목기준정보화면
    public partial class XFSTD1100 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        public XFSTD1100()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CustomUnboundColumnData += MainView_CustomUnboundColumnData;
        }

        private void MainView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "ItemPicture" && e.IsGetData)
            {
                var Temp6 = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "Temp6").GetNullToEmpty();

                if (Temp6.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + Temp6);
                e.Value = img;
            }
        }

        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
            lupCustomer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }
        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            GridExControl.MainGrid.AddUnboundColumn("ItemPicture", "제품사진", DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            GridExControl.MainGrid.AddColumn("Temp6", "제품사진URL", false);
            GridExControl.MainGrid.AddColumn("ItemCode","품목코드");
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("ItemNm","품명");
            GridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            GridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            GridExControl.MainGrid.AddColumn("BottomCategory", "소분류");
            GridExControl.MainGrid.AddColumn("Temp3", "SET품여부");
            GridExControl.MainGrid.AddColumn("MoldCode", "금형");
            GridExControl.MainGrid.AddColumn("KnifeCode", "칼");
            GridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            GridExControl.MainGrid.AddColumn("Unit", "단위");
            GridExControl.MainGrid.AddColumn("SrcQty", "원소재 소요량", HorzAlignment.Far, FormatType.Numeric, "n3");
            GridExControl.MainGrid.AddColumn("StdPackQty", "박스당수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SafeQty", "안전재고수(중)량", HorzAlignment.Far, true);
            GridExControl.MainGrid.AddColumn("Weight", "수(중)량", HorzAlignment.Far, true);
            GridExControl.MainGrid.AddColumn("ProcCnt", "단가", HorzAlignment.Far, FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("MainMc", "메인설비");
            //GridExControl.MainGrid.AddColumn("Spec1", "규격1");
            //GridExControl.MainGrid.AddColumn("Spec2", "규격2");
            //GridExControl.MainGrid.AddColumn("Spec3", "규격3");
            //GridExControl.MainGrid.AddColumn("Spec4", "규격4");
            GridExControl.MainGrid.AddColumn("Spec1", "원재료");
            GridExControl.MainGrid.AddColumn("Spec2", "색상");
            GridExControl.MainGrid.AddColumn("Spec3", "재료규격");
            GridExControl.MainGrid.AddColumn("Temp2", "실리콘농도");
            GridExControl.MainGrid.AddColumn("Spec4", "제품규격");            
            GridExControl.MainGrid.AddColumn("Memo");
            GridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");
            GridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
            GridExControl.MainGrid.AddColumn("UseYn", "사용구분");
            //GridExControl.MainGrid.AddColumn("ItemGbn");
            //GridExControl.MainGrid.AddColumn("Lctype","기종");
            //GridExControl.MainGrid.AddColumn("StockInspyn");
            //GridExControl.MainGrid.AddColumn("ProcInspyn");
            //GridExControl.MainGrid.AddColumn("SrcCode", "원소재");
            GridExControl.MainGrid.Columns["ItemPicture"].MinWidth = 50;
            GridExControl.MainGrid.Columns["ItemPicture"].MaxWidth = 80;
            GridExControl.MainGrid.Columns["ItemPicture"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }
        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemPictureEdit("ItemPicture");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp3", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Lctype", DbRequesHandler.GetCommCode(MasterCodeSTR.lctype, 1), "Mcode", "Codename");
            //   GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Suppvencode", ModelService.GetChildList<TN_STD1400>(p => 1 == 1).ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode", ModelService.GetList(p => p.UseYn == "Y" ).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMc", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldCode", ModelService.GetChildList<TN_MOLD001>(p => p.UseYN == "Y").ToList(), "MoldCode", "MoldName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("KnifeCode", ModelService.GetChildList<TN_KNIFE001>(p => p.UseYN == "Y").ToList(), "KnifeCode", "KnifeName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Spec1", DbRequesHandler.GetCommCode(MasterCodeSTR.ITEM_MATERIAL), "Mcode", "Codename");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Spec2", DbRequesHandler.GetCommCode(MasterCodeSTR.ITEM_COLOR), "Mcode", "Codename");
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            //GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            ////refresh 초기화
            //PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            GridRowLocator.GetCurrentRow("ItemCode");
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            string Customer = lupCustomer.EditValue.GetNullToEmpty();
            if (chk_UseYN.Checked == true)
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) 
                                                                      && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta)
                                                                      && (string.IsNullOrEmpty(Customer) ? true : p.MainCust == Customer)
                                                                   )
                                                            .OrderBy(p => p.ItemNm1)
                                                            .ToList();
            }
            else
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) 
                                                                      && (p.UseYn == "Y") 
                                                                      && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta)
                                                                      && (string.IsNullOrEmpty(Customer) ? true : p.MainCust == Customer)
                                                                    )
                                                            .OrderBy(p => p.ItemNm1)
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
                DialogResult result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage(29), "품목정보"), HelperFactory.GetLabelConvert().GetLabelText("Confirm"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.No)
                {
                    GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYn", "N");
                    //obj.UseYn = "N";
                    //ModelService.Update(obj);
                    return;
                }

                ModelService.Delete(obj);
                GridBindingSource.RemoveCurrent();
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