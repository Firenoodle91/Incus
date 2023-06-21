using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HKInc.Service.Handler
{
    public static class MessageBoxHandler
    {
        public static void Show(string text)
        {
            Show(text, HKInc.Service.Factory.HelperFactory.GetLabelConvert().GetLabelText("Confirm"));
        }

        public static void Show(string text, string caption)
        {
            Show(text, caption, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return Show(text, caption, buttons, MessageBoxIcon.Information);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return XtraMessageBox.Show(text, caption, buttons, icon);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return XtraMessageBox.Show(text, caption, buttons, icon, defaultButton);
        }

        public static void ErrorShow(string title, string message, string stacktrace)
        {
            using (Forms.ErrorMsgBox msgbox = new Forms.ErrorMsgBox())
            {
                msgbox.Text = title;
                msgbox.ErrorMessage = message;
                msgbox.ErrorStackTrace = stacktrace;
                msgbox.ShowDialog();
            }
        }

        public static void ErrorShow(string msg)
        {
            ErrorShow("Error!!", msg, "");
        }

        public static void ErrorShow(string title, string msg)
        {
            ErrorShow(title, msg, "");
        }

        public static void ErrorShow(Exception ex)
        {
            ErrorShow("", ex);
        }

        public static void ErrorShow(string message, Exception ex)
        {
            string errMessage = "";
            string stackTrace = "";

            do
            {
                errMessage += ex.Message + Environment.NewLine;
                stackTrace += String.Format("=====[Stack Trace]===== {0}{1}{0}{0}", Environment.NewLine, ex.StackTrace);
                stackTrace += String.Format("=====[Source]===== {0}{1}{0}{0}", Environment.NewLine, ex.Source);
                stackTrace += String.Format("=====[Target Site]===== {0}{1}{0}{0}{0}", Environment.NewLine, ex.TargetSite);
                ex = ex.InnerException;
            }
            while (ex != null);

            if (!String.IsNullOrEmpty(errMessage))
                message = errMessage;

            message = string.Format("{0}{1}{2}{3}", message, Environment.NewLine, Environment.NewLine, ""); 

            if (!String.IsNullOrEmpty(stackTrace))
                ErrorShow("ERROR!!", message, stackTrace);
            else
                Show("ERROR!!", message);
        }
    }
}
