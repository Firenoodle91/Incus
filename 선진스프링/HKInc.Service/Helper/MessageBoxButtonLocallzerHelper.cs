using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Helper
{
    public class MessageBoxButtonLocallzerHelper : Localizer
    {
        private string YesText;
        private string OkText;
        private string NoText;
        private string CancelText;

        public MessageBoxButtonLocallzerHelper(string YesText = "예(&Y)", string OkText = "확인(&O)", string NoText = "아니오(&N)", string CancelText = "취소(&C)")
        {
            this.YesText = YesText;
            this.OkText = OkText;
            this.NoText = NoText;
            this.CancelText = CancelText;
        }

        public override string GetLocalizedString(StringId id)
        {
            switch (id)
            {
                case StringId.XtraMessageBoxYesButtonText:
                    return YesText;

                case StringId.XtraMessageBoxOkButtonText:
                    return OkText;

                case StringId.XtraMessageBoxNoButtonText:
                    return NoText;

                case StringId.XtraMessageBoxCancelButtonText:
                    return CancelText;

                default:
                    return base.GetLocalizedString(id);

            }   //End the switch() statement            
        }
    }
}
