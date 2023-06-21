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

namespace HKInc.Ui.View.PUR_Popup
{
    public partial class XPFPUR1502 : PopupCallbackFormTemplate
    {
        IService<TN_PUR1502> ModelService = (IService<TN_PUR1502>)ProductionFactory.GetDomainService("TN_PUR1502");
        VI_PURINOUT VI_PURINOUT;
        int UseQty = 0;
        public XPFPUR1502()
        {
            InitializeComponent();
        }
        public XPFPUR1502(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            VI_PURINOUT = (VI_PURINOUT)PopupParam.GetValue(PopupParameter.KeyValue);
        }
        
        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            textReturnInQty.Properties.Mask.EditMask = "n0";
            textReturnInQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textReturnInQty.Properties.Mask.UseMaskAsDisplayFormat = true;

            dateReturnDate.Properties.ShowClear = false;
            dateReturnDate.EditValue = DateTime.Today;

            lupReturnId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lupReturnWhCode.SetDefault(true, "WhCode", "WhName", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList());
            lupReturnWhPosition.SetDefault(true, "PosionCode", "PosionName", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList());
        }

        protected override void InitDataLoad()
        {
            DataTable dt = DbRequesHandler.GetDTselect("exec SP_SRCIN '" + VI_PURINOUT.OutLotNo + "'");

            if (dt == null)
            {
                MessageBox.Show("사용 가능한 원소재가 없습니다.", "경고");
                Close();
            }

            if (dt.Rows.Count >= 1)
            {
                UseQty = dt.Rows[0]["qty"].GetIntNullToZero();
                SetMessage(string.Format("재입고 가능한 수량 : {0:n0}", UseQty));
            }
            else
            {
                MessageBox.Show("사용 가능한 원소재가 없습니다.", "경고");
                Close();
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = true;
            //VI_PURINOUT obj = (VI_PURINOUT)PopupParam.GetValue(PopupParameter.KeyValue);
            var TN_PUR1501 = ModelService.GetChildList<TN_PUR1501>(p => p.Temp1 == VI_PURINOUT.OutLotNo).First();

            if(UseQty < textReturnInQty.EditValue.GetIntNullToZero())
            {
                MessageBox.Show("재입고 가능한 수량을 벗어났습니다.", "경고");
                SetSaveMessageCheck = false;
                return;
            }

            ModelService.Insert(new TN_PUR1502()
            {
                ReturnInQty = textReturnInQty.EditValue.GetIntNullToZero()
                , ReturnDate = dateReturnDate.DateTime
                , ReturnId = lupReturnId.EditValue.GetNullToNull()
                , WhCode = lupReturnWhCode.EditValue.GetNullToNull()
                , WhPosition = lupReturnWhPosition.EditValue.GetNullToNull()
                , TN_PUR1501 = TN_PUR1501
                , ItemCode = TN_PUR1501.ItemCode
                , InLotNo = TN_PUR1501.Temp2
                , OutLotNo = TN_PUR1501.Temp1
            });

            ModelService.Save();

            Close();
        }
    }
}