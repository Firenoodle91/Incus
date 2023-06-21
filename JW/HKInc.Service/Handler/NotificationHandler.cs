using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Handler;

namespace HKInc.Service.Handler
{
    public class NotificationHandler
    {
        private SqlNotifier.SqlNotifier sqlNotifier;

        private readonly DatabaseCategory dbName;
        private readonly QueName queNameEnum;
        private string queName;

        private SqlNotifier.ActionList ActionList;
        private SqlNotifier.QueList QueList = new SqlNotifier.QueList();
        private NotificationMessageHandler messageHandler;

        public NotificationHandler(DatabaseCategory dbName, QueName queNameEnum, IMainFormMessage form, string messageCaption = "[Cache Handler]", int alertDeplayTime = 3000)
        {
            this.dbName = dbName;
            this.queNameEnum = queNameEnum;
            this.queName = queNameEnum.ToString();

            this.ActionList = new SqlNotifier.ActionList((IReloadDashboard)form);
            this.messageHandler = new NotificationMessageHandler(messageCaption, form, alertDeplayTime);

            //InitQueryNitification();
        }

        void InitQueryNitification()
        {
            //string registryQuery;

            //if (this.QueList.GetNotificationService(this.queNameEnum, out registryQuery))
            //{
            //    sqlNotifier = new SqlNotifier.SqlNotifier(this.dbName, registryQuery);
            //    sqlNotifier.NewMessage += sqlNotifier_NewMessage;
            //    DataTable dtQue = sqlNotifier.RegisterDependency();

            //    messageHandler.SetMessage(string.Format("Query Notification for {0} initialized at {1}", this.queName, DateTime.Now));
            //}
            //else
            //{
            //    messageHandler.SetMessage(string.Format("No query definition for queue name of {0}", this.queName));
            //}
        }

        // async로 동시수행하게 한다.
        //protected virtual async void ProcessNotification(DataTable dtQue)
        //{
        //    //System.Threading.Thread.Sleep(1000);  // delay for multifle update

        //    //foreach (DataRow drQue in dtQue.AsEnumerable())
        //    //{
        //    //    Action ResetAction;

        //    //    if (this.ActionList.GetResetAction(drQue["EntityName"].GetNullToEmpty(), out ResetAction))
        //    //    {
        //    //        ResetAction();
        //    //        messageHandler.SetMessage(string.Format("Reset cache {0}, Que ID of {1} at {2}", "Que Table name", drQue["QueId"].GetIntNullToZero(), DateTime.Now));
        //    //    }
        //    //}
        //    //sqlNotifier.MaxQueId = dtQue.Rows.Count > 0 ? dtQue.AsEnumerable().Max(p => p["QueId"].GetIntNullToZero()) : sqlNotifier.MaxQueId;
        //    //sqlNotifier.RegisterDependency();
        //}

        #region Event Hanlder for Query Notification from QueryNotifier
        void sqlNotifier_NewMessage(object sender, SqlNotifier.SQLNotifierArgument e)
        {
            //SqlNotificationEventArgs notification = e.Notification;
            //if (notification.Info == SqlNotificationInfo.Insert || notification.Info == System.Data.SqlClient.SqlNotificationInfo.Update)
            //{
            //    // asyn 로 동시실행이므로  RegisterDependecy()를 CacheReset 에서 call해야 된다
            //    this.ProcessNotification(e.QueTable);
            //}
            //else
            //{
            //    if (notification.Info == SqlNotificationInfo.Error && notification.Type == SqlNotificationType.Change && notification.Source == SqlNotificationSource.Timeout)
            //        messageHandler.SetMessage(string.Format("Timeout Error for queue name of {0} at {1}", this.queName, DateTime.Now));

            //    sqlNotifier.RegisterDependency();
            //    return;
            //}
        }
        #endregion
    }
}
