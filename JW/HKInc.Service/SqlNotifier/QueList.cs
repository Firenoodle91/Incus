using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Enum;

namespace HKInc.Service.SqlNotifier
{
    public class QueList
    {
        private readonly Dictionary<QueName, string> DicServiceList = new Dictionary<QueName, string>
        {
            {QueName.CodeCache, "SELECT QueId, EntityName, QueType, QueTime FROM dbo.QueTable"}
        };

        public bool GetNotificationService(QueName queNameEnum, out string registryQuery)
        {
            return DicServiceList.TryGetValue(queNameEnum, out registryQuery);
        }
    }
}
