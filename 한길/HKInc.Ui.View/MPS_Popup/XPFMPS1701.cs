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

namespace HKInc.Ui.View.MPS_Popup
{
    public partial class XPFMPS1701 : PopupCallbackFormTemplate
    {
        IService<TN_MPS1700> ModelService = (IService<TN_MPS1700>)ProductionFactory.GetDomainService("TN_MPS1700");
        List<VI_MPS1700_MASTER> VI_MPS1700_MASTER_List;
        public XPFMPS1701()
        {
            InitializeComponent();
            lupWhCode.EditValueChanged += LupWhCode_EditValueChanged;
        }

        public XPFMPS1701(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            VI_MPS1700_MASTER_List = (List<VI_MPS1700_MASTER>)PopupParam.GetValue(PopupParameter.KeyValue);
        }


        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            dateInDate.Properties.ShowClear = false;
            dateInDate.EditValue = DateTime.Today;

            lupInId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lupInId.EditValue = GlobalVariable.LoginId;

            lupWhCode.SetDefault(false, "WhCode", "WhName", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList());

            //lupWhPosition.SetDefault(false, "PosionCode", "PosionName", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList());
        }

        private void LupWhCode_EditValueChanged(object sender, EventArgs e)
        {
            string WhCode = lupWhCode.EditValue.GetNullToEmpty();
            if (WhCode.IsNullOrEmpty())
            {
                return;
            }
            lupWhPosition.SetDefault(false, "PosionCode", "PosionName", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == WhCode).ToList());
        }

        protected override void DataSave()
        {
            var InDate = dateInDate.DateTime;
            var InId = lupInId.EditValue.GetNullToNull();
            var InWhCode = lupWhCode.EditValue.GetNullToNull();
            var InPosition = lupWhPosition.EditValue.GetNullToNull();

            foreach(var v in VI_MPS1700_MASTER_List)
            {
                ModelService.Insert(new TN_MPS1700()
                {
                    InNo = DbRequesHandler.GetRequestNumber("W-IN"),
                    PackLotNo = v.PackLotNo,
                    InQty = v.RemainQty,
                    InDate = InDate,
                    InId = InId,
                    WhCode = InWhCode,
                    WhPosition = InPosition
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