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
        private IService<TN_STD1100> Std1100Service;
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
            lupBottomCategory.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE));
            lup_ltype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.lctype));
            lupTopCategory.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", Std1100Service.GetChildList<TN_STD1400>(p=>p.UseFlag=="Y").OrderBy(o=>o.CustomerName).ToList());
            lupmc.SetDefault(true, "MachineCode", "MachineName", Std1100Service.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").OrderBy(o => o.MachineName).ToList());
            //lupsrccode.SetDefault(true, "ItemCode", "ItemNm1", Std1100Service.GetChildList<TN_STD1100>(p => p.UseYn == "Y"&&(p.TopCategory=="P03"|| p.TopCategory == "P02")).OrderBy(o => o.ItemNm1).ToList());
            lupsrccode.SetDefault(true, "ItemCode", "ItemNm1", Std1100Service.GetChildList<TN_STD1100>(p => p.UseYn == "Y"&&(p.TopCategory=="P03"|| p.TopCategory == "P02")).OrderBy(o => o.ItemNm1).ToList());
            gridLookUpEditEx4.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit));
            lup_MoldCode.SetDefault(true, "MoldCode", "MoldName", Std1100Service.GetChildList<TN_MOLD001>(x => x.UseYN == "Y").OrderBy(o => o.MoldMcode).ToList());
            //luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
            lup_labelTyp.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.InspectLabelTyp, 1));
        }

        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {

                ModelBindingSource.Add(new TN_STD1100 { UseYn = "Y", ItemCode = DbRequesHandler.GetRequestNumberNew("ITEM") });


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
                lupMiddleCategory.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, lupTopCategory.EditValue.ToString(), 2));
        }

        private void lupMiddleCategory_EditValueChanged(object sender, EventArgs e)
        {
                //if (lupMiddleCategory.EditValue.GetNullToEmpty() == "") return;
                //lupBottomCategory.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, lupMiddleCategory.EditValue.ToString(), 3));
        }
    }
}