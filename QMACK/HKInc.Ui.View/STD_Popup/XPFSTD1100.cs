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
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
namespace HKInc.Ui.View.STD_Popup
{
    public partial class XPFSTD1100 : HKInc.Service.Base.ListEditFormTemplate
    {
        #region 전역변수
        private IService<TN_STD1100> Std1100Service;
        #endregion

        public XPFSTD1100(PopupDataParam param, PopupCallback callback)
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
            Std1100Service = (IService<TN_STD1100>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            base.InitCombo();
            lupBottomCategory.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE));
            lup_ltype.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.lctype));
            lupTopCategory.SetDefault(false, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", Std1100Service.GetChildList<TN_STD1400>(p=>p.UseFlag=="Y").OrderBy(o=>o.CustomerName).ToList());
            lupmc.SetDefault(true, "MachineCode", "MachineName", Std1100Service.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").OrderBy(o => o.MachineName).ToList());
            // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략            
            lupsrccode.SetDefault(true, "ItemCode", "ItemNm", Std1100Service.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && 
                                                                                                            (p.TopCategory == MasterCodeSTR.Tpocategory_Outsorcing_Product || p.TopCategory == MasterCodeSTR.Topcategory_Material)).OrderBy(o => o.ItemNm1).ToList());
            gridLookUpEditEx4.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit));
         //       luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
        }
        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {

                ModelBindingSource.Add(new TN_STD1100 { UseYn = "Y", ItemCode = DbRequestHandler.GetRequestNumberNew("ITEM") });


                ModelBindingSource.DataSource = Std1100Service.Insert((TN_STD1100)ModelBindingSource.Current);



                this.Refresh();

            }
            else
            {  // Update
                TN_STD1100 obj = (TN_STD1100)PopupParam.GetValue(PopupParameter.KeyValue);

                ModelBindingSource.DataSource = obj;// (SaleNote)PopupParam.GetValue(PopupParameter.KeyValue);

            }
                tx_itemcode.ReadOnly = true;
            // tx_ItemCode.ReadOnly = true;
        }

        protected override void DataSave()
        {
            //      base.DataSave();
            //   Std1100Service.Save();

            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_STD1100 obj = ModelBindingSource.Current as TN_STD1100;

            if (EditMode == PopupEditMode.New)
            {
                //   ((OrderMaster)ModelBindingSource.Current).OrderNo = Gunsol.InjectionMaster.View.Helper.RequestNumberHandler.GetRequestNumber("ORD");

                ModelBindingSource.DataSource = Std1100Service.Insert(obj);// (SaleNote)ModelBindingSource.Current);
            }
            else
            {
                Std1100Service.Update(obj);// (SaleNote)ModelBindingSource.Current);
            }
            Std1100Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.RowId);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }

         private void lupTopCategory_EditValueChanged(object sender, EventArgs e)
         {
            if (lupTopCategory.EditValue.GetNullToEmpty() == "") return;
                lupMiddleCategory.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, lupTopCategory.EditValue.ToString(), 2));
         }

         private void lupMiddleCategory_EditValueChanged(object sender, EventArgs e)
         {
                //if (lupMiddleCategory.EditValue.GetNullToEmpty() == "") return;
                //lupBottomCategory.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, lupMiddleCategory.EditValue.ToString(), 3));
         }
    }
}