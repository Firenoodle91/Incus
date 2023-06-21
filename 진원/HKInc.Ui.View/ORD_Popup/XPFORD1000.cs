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

namespace HKInc.Ui.View.ORD_Popup
{
    public partial class XPFORD1000 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_ORD1000> ORD1000Service;
        public XPFORD1000(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
           
            this.Text = "수주등록";
        }


        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            ORD1000Service = (IService<TN_ORD1000>)PopupParam.GetValue(PopupParameter.Service);
        }
        protected override void InitCombo()
        {
            base.InitCombo();

            lupcustcode.SetDefault(false, "CustomerCode", "CustomerName", ORD1000Service.GetChildList<TN_STD1400>(p =>p.UseFlag=="Y").ToList());
            lupempid.SetDefault(true, "LoginId", "UserName", ORD1000Service.GetChildList<UserView>(p=>p.Active=="Y").ToList());
        }
        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                
                ModelBindingSource.Add(new TN_ORD1000 { OrderNo = DbRequesHandler.GetRequestNumber("ORD"),OrderDate=DateTime.Today ,OrderType= PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty() });


                ModelBindingSource.DataSource = ORD1000Service.Insert((TN_ORD1000)ModelBindingSource.Current);



                this.Refresh();

            }
            else
            {  // Update
                TN_ORD1000 obj = (TN_ORD1000)PopupParam.GetValue(PopupParameter.KeyValue);

                ModelBindingSource.DataSource = obj;// (SaleNote)PopupParam.GetValue(PopupParameter.KeyValue);

            }
            tx_orderno.ReadOnly = true;
            // tx_ItemCode.ReadOnly = true;
        }

        protected override void DataSave()
        {
            //      base.DataSave();
            //   Std1100Service.Save();

            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_ORD1000 obj = ModelBindingSource.Current as TN_ORD1000;

            if (EditMode == PopupEditMode.New)
            {
                //   ((OrderMaster)ModelBindingSource.Current).OrderNo = Gunsol.InjectionMaster.View.Helper.RequestNumberHandler.GetRequestNumber("ORD");

                //if(lupcustcode.Text)

                if(obj.CustomerCode.IsNullOrEmpty())
                {
                    //HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(16));
                    HKInc.Service.Handler.MessageBoxHandler.Show("거래처 미선택");
                    return;
                }

                if (obj.PeriodDate.IsNullOrEmpty())
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("납품일 미선택");
                    return;
                }


                ModelBindingSource.DataSource = ORD1000Service.Insert(obj);// (SaleNote)ModelBindingSource.Current);
            }
            else
            {
                ORD1000Service.Update(obj);// (SaleNote)ModelBindingSource.Current);
            }

            ORD1000Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.RowId);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            this.Close();
        }

      
    }
}