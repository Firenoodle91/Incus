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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.ORD_POPUP
{
    /// <summary>
    /// 개발의뢰마스터 추가 팝업
    /// </summary>
    public partial class XPFORD1000_DEV : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_ORD1000> ModelService;
        public XPFORD1000_DEV(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정

            this.Text = LabelConvert.GetLabelText("ReqDevelopmentMasterInfo");
        }

        protected override void AddControlList()
        {
            ControlEnableList.Add("OrderNo", tx_OrderNo);
            ControlEnableList.Add("OrderCustomerCode", lup_OrderCustomerCode);
            ControlEnableList.Add("OrderDate", dt_OrderDate);
            ControlEnableList.Add("OrderDueDate", dt_OrderDueDate);
            ControlEnableList.Add("OrderManagerName", tx_OrderManagerName);
            ControlEnableList.Add("OrderId", lup_OrderId);
            ControlEnableList.Add("Memo", memoEdit1);

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_ORD1000>(new TN_ORD1000(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            ModelService = (IService<TN_ORD1000>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            lup_OrderCustomerCode.SetDefault(false, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_OrderId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());

            tx_OrderNo.ReadOnly = true;
        }

        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var orderType = PopupParam.GetValue(PopupParameter.Value_1).GetNullToNull();

                var newObj = new TN_ORD1000()
                {
                    OrderNo = orderType == "개발" ? DbRequestHandler.GetSeqMonth("ORD_D") : DbRequestHandler.GetSeqMonth("ORD"),
                    OrderType = orderType,
                    OrderDate = DateTime.Today,
                    OrderDueDate = DateTime.Today,
                    OrderId = GlobalVariable.LoginId
                };
                ModelBindingSource.Add(newObj);
                ModelBindingSource.MoveLast();
            }
            else
            {  
                // Update
                TN_ORD1000 obj = (TN_ORD1000)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            dt_OrderDate.DoValidate();
            dt_OrderDueDate.DoValidate();

            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            TN_ORD1000 obj = ModelBindingSource.Current as TN_ORD1000;

            if (EditMode == PopupEditMode.New)
            {
                ModelBindingSource.DataSource = ModelService.Insert(obj);
            }
            else
            {
                ModelService.Update(obj);
            }
            ModelService.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.OrderNo);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }


    }
}