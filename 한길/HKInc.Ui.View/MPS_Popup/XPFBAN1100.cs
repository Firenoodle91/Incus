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
    public partial class XPFBAN1100 : PopupCallbackFormTemplate
    {
        IService<TN_BAN1001> ModelService = (IService<TN_BAN1001>)ProductionFactory.GetDomainService("TN_BAN1001");

        public XPFBAN1100()
        {
            InitializeComponent();

            textEdit1.KeyDown += TextEdit1_KeyDown;
        }
        public XPFBAN1100(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
        }


        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        private void TextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                var InLotNo = textEdit1.EditValue.GetNullToEmpty();
                if (!InLotNo.IsNullOrEmpty())
                {
                    var stock = ModelService.GetList(p => p.Temp2 == InLotNo && p.TN_BAN1000.Temp1 == "Y").OrderBy(o => o.ItemCode).FirstOrDefault();
                    if (stock == null)
                    {
                        MessageBox.Show("정보가 없습니다.");
                        textEdit1.EditValue = string.Empty;
                        textEdit1.Focus();
                    }
                    else
                    {
                        PopupDataParam param = new PopupDataParam();
                        param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(stock));
                        ReturnPopupArgument = new PopupArgument(param);
                        ActClose();
                    }                    
                }
            }
        }
    }
}