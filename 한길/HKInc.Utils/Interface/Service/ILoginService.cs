using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Utils.Interface.Repository;

namespace HKInc.Utils.Interface.Service
{
    public interface ILoginService : IIpAddress
    {
        bool IsValidLogin(string userId, string password, Enum.DatabaseCategory databaseCategory);

        HKInc.Ui.Model.Domain.User GetUserInfo();        
    }
}
