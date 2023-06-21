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
    public partial class XPFPUR1400 : PopupCallbackFormTemplate
    {
        public XPFPUR1400()
        {
            InitializeComponent();

            textEdit1.KeyDown += TextEdit1_KeyDown;
            textEdit1.ImeMode = ImeMode.Disable;
        }
        public XPFPUR1400(PopupDataParam parameter, PopupCallback callback) : this()
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
                    string outf = DbRequesHandler.GetCellValue("exec SP_STOPINOUT '" + InLotNo + "'", 0);
                    if (outf == "Y")
                    {
                        MessageBox.Show("이전 LOT가 있습니다. 선입선출 확인하세요", "경고");
                        textEdit1.EditValue = string.Empty;
                        textEdit1.Focus();
                    }
                    else
                    {
                        var ModelService = (IService<TN_PUR1500>)ProductionFactory.GetDomainService("TN_PUR1500");
                        var stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.Temp2 == InLotNo).OrderBy(o => o.ItemCode).FirstOrDefault();
                        if (stock == null)
                        {
                            MessageBox.Show("정보가 없습니다.");
                            textEdit1.EditValue = string.Empty;
                            textEdit1.Focus();
                        }
                        else
                        {
                            PopupDataParam param = new PopupDataParam();
                            param.SetValue(PopupParameter.ReturnObject, ModelService.DetachChild(stock));
                            ReturnPopupArgument = new PopupArgument(param);
                            ActClose();
                        }
                    }
                }
            }
        }
    }
}