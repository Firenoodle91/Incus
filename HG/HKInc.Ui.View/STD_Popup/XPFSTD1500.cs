using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

using HKInc.Ui.View.Handler;

namespace HKInc.Ui.View.STD_Popup
{
    public partial class XPFSTD1500 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD1500> Std1500Service;
        public XPFSTD1500(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
        }
        //protected override void AddControlList() // abstract함수 구현
        //{
        //    //ControlEnableList.Add("BomId", textBomId);
        //    ////ControlEnableList.Add("ParentBomId", lupParentBomId);
        //    //////ControlEnableList.Add("Level", textLevel);
        //    ////ControlEnableList.Add("ItemCode", lupItemCode);
        //    //////ControlEnableList.Add("ChildItemName", textItemName);
        //    //////ControlEnableList.Add("ChildItemCodeSeven", textItemCodeSeven);
        //    //////ControlEnableList.Add("ChildItemTopCategory", textTopCategory);
        //    ////ControlEnableList.Add("ProcessCode", lupProcessCode);
        //    ////ControlEnableList.Add("UseQty", textUseQty);
        //    ////ControlEnableList.Add("ItemWeightA", textItemWeightA);
        //    ////ControlEnableList.Add("ItemWeightB", textItemWeightB);

        //    ////ControlEnableList.Add("Memo", memoMemo);

        //    LayoutControlHandler.SetRequiredLabelText<TN_STD1500>(new TN_STD1500(), ControlEnableList, this.Controls);
        //}
        protected override void InitControls()
        {
            base.InitControls();
            lupItemCode.EditValueChanged += LupItemCode_EditValueChanged;
        }

        private void LupItemCode_EditValueChanged(object sender, System.EventArgs e)
        {
            string ItemCode = lupItemCode.EditValue.GetNullToEmpty();
            TN_STD1100 ItemMaster = Std1500Service.GetChildList<TN_STD1100>(p => p.ItemCode == ItemCode).FirstOrDefault();
            if (ItemMaster == null) return;

            textItemName.Text = ItemMaster.ItemNm;
            tx_spec1.Text = ItemMaster.Spec1;
            tx_spec2.Text = ItemMaster.Spec2;
            tx_spec3.Text = ItemMaster.Spec3;
            tx_spec4.Text = ItemMaster.Spec4;
            lupUnit.EditValue = ItemMaster.Unit;
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            Std1500Service = (IService<TN_STD1500>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
          //  lupProcessCode.SetDefault(false, "CodeId", "CodeName", MasterCode.GetMasterCode((int)MasterCodeEnum.ProcessCategory).ToList());

            List<TN_STD1500> BomList = Std1500Service.GetList(p => 1 == 1);
            
            lupParentBomId.ParentFieldName = "ParentBomId";
            lupParentBomId.KeyFieldName = "BomId";
            lupParentBomId.DisplayMember = "TN_STD1100.ItemNm1";
            lupParentBomId.ValueMember = "BomId";
            lupParentBomId.ShowColumns = false;
            lupParentBomId.AddColumn("TN_STD1100.ItemNm1");
            lupParentBomId.AddColumn("TN_STD1100.ItemNm");
            lupParentBomId.AddColumn("BomId", false);
            lupParentBomId.DataSource = BomList;
            lupParentBomId.ExpandAll();

            lupItemCode.SetDefault(true, "ItemCode", "ItemNm1", Std1500Service.GetChildList<TN_STD1100>(p => 1 == 1).ToList());
            lupItemCode.AddButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis);
            lupItemCode.ButtonClick += new LookUpButtonHandler<TN_STD1100>(lupItemCode, PopupFactory.ProductionPopupView.SELECTSTD1100).ButtonClick;
            lupUnit.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit));
              
        }

        protected override void DataLoad()
        {
            tx_BomId.Enabled = false;
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                ModelBindingSource.Add(new TN_STD1500() { UseYn="Y"});
                ModelBindingSource.MoveLast();                
            }
            else
            {  // Update
                ModelBindingSource.DataSource = (TN_STD1500)PopupParam.GetValue(PopupParameter.KeyValue);
            }
        }

        protected override void DataSave()
        {
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            TN_STD1500 obj = (TN_STD1500)ModelBindingSource.Current;

            if (EditMode == PopupEditMode.New)
                ModelBindingSource.DataSource = Std1500Service.Insert(obj);
            else
                ModelBindingSource.DataSource = Std1500Service.Update(obj);

            Std1500Service.Save();

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }

    }
}
