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
using HKInc.Service.Base;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Common;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.MPS_Popup
{
    public partial class XPFMPS1100 : PopupCallbackFormTemplate
    {
        IService<TN_MPS1300> ModelService = (IService<TN_MPS1300>)ProductionFactory.GetDomainService("TN_MPS1300");
        List<VI_MPSPLAN_LIST> VI_MPSPLAN_LIST_List;
        public XPFMPS1100()
        {
            InitializeComponent();
        }
        public XPFMPS1100(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            VI_MPSPLAN_LIST_List = (List<VI_MPSPLAN_LIST>)PopupParam.GetValue(PopupParameter.KeyValue);
        }


        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            datePlanStartDate.Properties.ShowClear = false;
            //datePlanStartDate.EditValue = DateTime.Today;
            datePlanEndDate.Properties.ShowClear = false;
            //datePlanStartDate.EditValue = DateTime.Today;
        }
        
        protected override void DataSave()
        {
            if(datePlanStartDate.DateTime.GetDateTimeToNullCheck() || datePlanEndDate.DateTime.GetDateTimeToNullCheck())
            {
                MessageBoxHandler.Show("생산계획시작일 또는 종료일은 필수 사항 입니다.", "경고");
                return;
            }

            var PlanStartDate = datePlanStartDate.DateTime;
            var PlanEndDate = datePlanEndDate.DateTime;
            var Memo = memoEdit1.EditValue.GetNullToEmpty();

            foreach(var v in VI_MPSPLAN_LIST_List)
            {
                ModelService.Insert(new TN_MPS1300()
                {
                    DelivDate = v.DelivDate,
                    DelivSeq = v.DelivSeq,
                    PlanNo = DbRequesHandler.GetRequestNumber("WP"),
                    PlanQty = v.DelivQty - v.TN_MPS1300List.Sum(p => p.PlanQty).GetIntNullToZero(),
                    ItemCode = v.ItemCode,
                    OrderNo = v.OrderNo,
                    WorkorderYn = "N",
                    PlanStartdt = PlanStartDate,
                    PlanEnddt = PlanEndDate,
                    Memo = Memo
                });
            }
            ModelService.Save();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.ReturnObject, "SAVE");
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        protected override void ActClose()
        {
            base.ActClose();
            ModelService.Dispose();
        }
    }
}