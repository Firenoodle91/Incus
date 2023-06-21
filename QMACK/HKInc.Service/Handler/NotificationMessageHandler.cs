using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Alerter;

using HKInc.Utils.Interface.Handler;

namespace HKInc.Service.Handler
{
    class NotificationMessageHandler
    {
        private IMainFormMessage form;
        private string messageCaption;
        private int alertDeplayTime;
        private bool isNoticeMessage;

        public NotificationMessageHandler(string caption, IMainFormMessage form, int delayTime = 3000, bool isNoticeMessage = false)
        {
            this.messageCaption = caption;
            this.form = form;
            this.alertDeplayTime = delayTime;
            this.isNoticeMessage = isNoticeMessage;
        }

        public void SetMessage(string msg)
        {
            if (IsMessageBar())
            {
                // MessageBar of MineForm
                this.form.SetMessage(string.Format("{0} {1}", this.messageCaption, msg));
            }
            else
            {
                this.form.SetMessage(string.Empty);
                try
                {
                    if (this.isNoticeMessage)
                    {
                        SetFlyoutPanel(this.messageCaption, msg);
                    }
                    else
                    {
                        AlertInfo info = new AlertInfo(this.messageCaption, msg);
                        SetAlertWindow(info);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "MessageHandler.SetMessage");
                }
            }
        }

        void SetAlertWindow(AlertInfo info)
        {
            Action action = () =>
            {
                AlertControl alert = new AlertControl();
                alert.AutoFormDelay = this.alertDeplayTime;
                alert.Show(this.form.FlyoutMainForm, info);
            };

            this.form.FlyoutMainForm.Invoke(action);
        }

        void SetFlyoutPanel(string title, string content)
        {
            FlyoutPanel flyout = this.form.FlyMsg;
            
            Action action = () =>
            {
                this.form.LabelTitle.Text = title;
                this.form.LabelText.Text = content;

                flyout.ShowPopup(false);
            };

            this.form.FlyoutMainForm.Invoke(action);
        }

        bool IsMessageBar()
        {
            string registryPath = string.Format(@"{0}\{1}", HKInc.Utils.Common.GlobalVariable.ServerConfigPath, HKInc.Utils.Common.GlobalVariable.LoginId);
            string key = "Notification";

            string notificationPath = RegistryHandler.GetValue(registryPath, key);

            if (!String.IsNullOrEmpty(notificationPath))
            {
                if (notificationPath.Equals("BAR"))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
