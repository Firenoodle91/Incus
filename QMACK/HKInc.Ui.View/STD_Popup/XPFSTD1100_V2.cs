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
    /// <summary>
    /// 20220426 오세완 차장
    /// 고도화로 인하여 소재사용량이 단위로 변경된 품목기준정보 팝업
    /// </summary>
    public partial class XPFSTD1100_V2 : HKInc.Service.Base.ListEditFormTemplate
    {
        #region 전역변수
        private IService<TN_STD1100> Std1100Service;
        #endregion

        public XPFSTD1100_V2(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
            lupsrccode.EditValueChanged += Lupsrccode_EditValueChanged;
        }

        /// <summary>
        /// 20220426 오세완 차장
        /// 원소재 코드를 변경시 단위를 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lupsrccode_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit slup_Src = sender as SearchLookUpEdit;
            if(slup_Src != null)
            {
                string sSrccode = slup_Src.EditValue.GetNullToEmpty();
                TN_STD1100 src_std = Std1100Service.GetList(p => p.ItemCode == sSrccode &&
                                                                 p.UseYn == "Y").FirstOrDefault();
                if(src_std != null)
                {
                    List<TN_STD1000> unit_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.Unit);
                    if(unit_Arr != null)
                        if(unit_Arr.Count > 0)
                        {
                            TN_STD1000 unit_temp = unit_Arr.Where(p => p.Mcode == src_std.Unit).FirstOrDefault();
                            if (unit_temp != null)
                                tx_Unit.EditValue = unit_temp.Codename;
                        }
                }
            }
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
            {  
                // Update
                TN_STD1100 obj = (TN_STD1100)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
                SetUnit();
            }

            tx_itemcode.ReadOnly = true;
        }

        /// <summary>
        /// 20220426 오세완 차장 
        /// 원자재 코드가 있는 경우 단위 출력 진행 
        /// </summary>
        private void SetUnit()
        {
            string sSrccode = lupsrccode.EditValue.GetNullToEmpty();
            TN_STD1100 src_std = Std1100Service.GetList(p => p.ItemCode == sSrccode &&
                                                             p.UseYn == "Y").FirstOrDefault();
            if (src_std != null)
            {
                List<TN_STD1000> unit_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.Unit);
                if (unit_Arr != null)
                    if (unit_Arr.Count > 0)
                    {
                        TN_STD1000 unit_temp = unit_Arr.Where(p => p.Mcode == src_std.Unit).FirstOrDefault();
                        if (unit_temp != null)
                            tx_Unit.EditValue = unit_temp.Codename;
                    }
            }
        }

        protected override void DataSave()
        {
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_STD1100 obj = ModelBindingSource.Current as TN_STD1100;

            if (EditMode == PopupEditMode.New)
            {
                ModelBindingSource.DataSource = Std1100Service.Insert(obj);
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
    }
}