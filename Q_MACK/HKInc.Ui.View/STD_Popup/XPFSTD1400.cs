﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;

namespace HKInc.Ui.View.STD_Popup
{
    public partial class XPFSTD1400 :  HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD1400> Std1400Service;
        public XPFSTD1400(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
        }


        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            Std1400Service = (IService<TN_STD1400>)PopupParam.GetValue(PopupParameter.Service);
        }
        protected override void InitCombo()
        {
            base.InitCombo();

            gridLookUpEditEx1.SetDefault(false, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.CustType, "", "", ""));
            lupItemNationalCode.SetDefault(false, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.NationalCode, "", "", ""));
            lupItemCustomerCategoryType.SetDefault(false, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.BusinessType, "", "", ""));
            lupItemCustomerCategoryCode.SetDefault(false, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.BusinessCode, "", "", ""));
        }
        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {

                ModelBindingSource.Add(new TN_STD1400 { UseFlag = "Y", DefaultCompanyPlag = "N", CustomerCode = DbRequestHandler.GetRequestNumberNew("CUST") });


                ModelBindingSource.DataSource = Std1400Service.Insert((TN_STD1400)ModelBindingSource.Current);



                this.Refresh();

            }
            else
            {  // Update
                TN_STD1400 obj = (TN_STD1400)PopupParam.GetValue(PopupParameter.KeyValue);

                ModelBindingSource.DataSource = obj;// (SaleNote)PopupParam.GetValue(PopupParameter.KeyValue);

            }
            tx_custcode.ReadOnly = true;
            // tx_ItemCode.ReadOnly = true;
        }

        protected override void DataSave()
        {
            //      base.DataSave();
            //   Std1100Service.Save();

            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_STD1400 obj = ModelBindingSource.Current as TN_STD1400;

            if (EditMode == PopupEditMode.New)
            {
                //   ((OrderMaster)ModelBindingSource.Current).OrderNo = Gunsol.InjectionMaster.View.Helper.RequestNumberHandler.GetRequestNumber("ORD");

                ModelBindingSource.DataSource = Std1400Service.Insert(obj);// (SaleNote)ModelBindingSource.Current);
            }
            else
            {
                Std1400Service.Update(obj);// (SaleNote)ModelBindingSource.Current);
            }
            Std1400Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.RowId);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }
    }
}