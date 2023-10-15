using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Ui.Model.Context;
using HKInc.Utils.Common;

namespace HKInc.Service.Handler
{
    public static class MailHandler
    {
        public static void SendMail(string toMailAddress, string content, string title, string replyTo = "", string fromMailAddress = "", string fileAttachments = "")
        {
            using (ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase)))
            {
                db.SendDatabaseMail(toMailAddress, 
                                    content, 
                                    title,
                                    string.IsNullOrEmpty(fromMailAddress) ? GlobalVariable.FromMailAddress : fromMailAddress,
                                    string.IsNullOrEmpty(replyTo) ? GlobalVariable.ReplyMailAddress : replyTo, 
                                    fileAttachments);
            }
        }
    }
}
