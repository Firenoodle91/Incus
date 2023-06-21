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
    public partial class XFSTD1400 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");
        public XFSTD1400()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }
        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCd"));
            GridExControl.MainGrid.AddColumn("CustomerName");
            GridExControl.MainGrid.AddColumn("DefaultCompanyPlag");
            GridExControl.MainGrid.AddColumn("CustType","거래처구분");
            GridExControl.MainGrid.AddColumn("CustomerCategoryCode","업태");
            GridExControl.MainGrid.AddColumn("CustomerCategoryType","업종");

            GridExControl.MainGrid.AddColumn("RepresentativeName");
            GridExControl.MainGrid.AddColumn("RegistrationNo");
            GridExControl.MainGrid.AddColumn("CorporationNo");
            GridExControl.MainGrid.AddColumn("CustomerBankCode");
            GridExControl.MainGrid.AddColumn("AccountNumber"); // 10
            GridExControl.MainGrid.AddColumn("NationalCode");
            GridExControl.MainGrid.AddColumn("SDate", false);
            GridExControl.MainGrid.AddColumn("CreaditRating",false);
            GridExControl.MainGrid.AddColumn("ZipCode");
            GridExControl.MainGrid.AddColumn("Address"); // 15
            GridExControl.MainGrid.AddColumn("Address2"); // 15
            GridExControl.MainGrid.AddColumn("Telephone");
            GridExControl.MainGrid.AddColumn("Fax");
            GridExControl.MainGrid.AddColumn("Email", LabelConvert.GetLabelText("Email"));
            GridExControl.MainGrid.AddColumn("EmpCode");
            GridExControl.MainGrid.AddColumn("EmpInTelephone", false); // 20
            GridExControl.MainGrid.AddColumn("EmpTelephone");
            GridExControl.MainGrid.AddColumn("Memo");
            GridExControl.MainGrid.AddColumn("UseFlag");
            GridExControl.MainGrid.AddColumn("CreateDate", false);
            GridExControl.MainGrid.AddColumn("CreateId", false);
            GridExControl.MainGrid.AddColumn("UpdateDate", false);
            GridExControl.MainGrid.AddColumn("UpdateId", false);

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
           
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("DefaultCompanyPlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateDate");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateDate");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustType", DbRequestHandler.GetCommCode(MasterCodeSTR.CustType), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustomerCategoryCode", DbRequestHandler.GetCommCode(MasterCodeSTR.BusinessType), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustomerCategoryType", DbRequestHandler.GetCommCode(MasterCodeSTR.BusinessCode), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("NationalCode", DbRequestHandler.GetCommCode(MasterCodeSTR.NationalCode), "Mcode", "Codename");

            //lupItemCustomerCategoryType.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.BusinessType, "", "", ""));
            //lupItemCustomerCategoryCode.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.BusinessCode, "", "", ""));

            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
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

            if (chk_UseYN.Checked == true)
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.CustomerName.Contains(tx_custnm.Text) || (p.CustomerCode == tx_custnm.Text)))
                                                            .OrderBy(p => p.CustomerName)
                                                          .ToList();
            }
            else
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.CustomerName.Contains(tx_custnm.Text) || (p.CustomerCode == tx_custnm.Text)) &&
                                                                          (p.UseFlag == "Y"))
                                                          .OrderBy(p => p.CustomerName)
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
            TN_STD1400 obj = GridBindingSource.Current as TN_STD1400;

            if (obj != null)
            {
             
                GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseFlag", "N");
                obj.UseFlag = "N";



                ModelService.Update(obj);
         

            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1400, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }
    }
}