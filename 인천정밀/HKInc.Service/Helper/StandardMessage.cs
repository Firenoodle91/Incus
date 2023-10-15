using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Context;
using HKInc.Service.Repository;
using HKInc.Utils.Interface.Repository;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Class;

namespace HKInc.Service.Helper
{
    class MessageHelper : IStandardMessage
    {
        private static List<StandardMessage> StandardMessageList = new List<StandardMessage>();

        public static void GetStandardMessageList()
        {
            try
            {
                StandardMessageList.Clear();

                IRepository<StandardMessage> repository = new SystemRepository<StandardMessage>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
                IEnumerable<StandardMessage> idr = repository.GetAll();
                foreach (StandardMessage message in idr)
                    StandardMessageList.Add(repository.Detached(message));
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }

        public void Reset() { GetStandardMessageList(); }
        public static void ResetCode() { GetStandardMessageList(); }

        public string GetStandardMessage(int messageId)
        {
            if (StandardMessageList.Count == 0) GetStandardMessageList();

            IEnumerable<StandardMessage> idr = StandardMessageList.Where(p => p.MessageId == messageId);

            if (idr.Count() > 0)
                return GlobalVariable.IsDefaultCulture ? idr.First().Message : (GlobalVariable.IsSecondCulture ? idr.First().Message2 : idr.First().Message3);
            else
                return string.Empty;
        }
    }
}
