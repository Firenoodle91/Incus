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

namespace HKInc.Ui.View.ORD_Popup
{
    public partial class XPFORD1200 : PopupCallbackFormTemplate
    {
        IService<VI_PRODQTYMSTLOT> ModelService = (IService<VI_PRODQTYMSTLOT>)ProductionFactory.GetDomainService("VI_PRODQTYMSTLOT");

        public XPFORD1200()
        {
            InitializeComponent();

            textEdit1.KeyDown += TextEdit1_KeyDown;
            textEdit1.ImeMode = ImeMode.Disable;
        }
        public XPFORD1200(PopupDataParam parameter, PopupCallback callback) : this()
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
                var PackLotNo = textEdit1.EditValue.GetNullToEmpty();
                if (!PackLotNo.IsNullOrEmpty())
                {
                    var CheckObj = ModelService.GetList(p => p.PackLotNo == PackLotNo && p.Stockqty > 0).FirstOrDefault();
                    if (CheckObj == null)
                    {
                        MessageBoxHandler.Show("해당 LOT는 재고가 없거나, 존재하지 않은 포장 LOT NO 입니다.", "경고");
                        return;
                    }

                    List<VI_PRODQTYMSTLOT> returnList = new List<VI_PRODQTYMSTLOT>();
                    returnList.Add(ModelService.Detached(CheckObj));

                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                    ReturnPopupArgument = new PopupArgument(param);
                    ActClose();
                }
            }
        }

        protected override void ActClose()
        {
            base.ActClose();
            ModelService.Dispose();
        }
    }
}