using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Repository;

namespace HKInc.Interface.Helper
{
    public interface IUserValidator : IIpAddress
    {
        bool IsValidUser(string userId, string password, DatabaseCategory databaseCategory = DatabaseCategory.DefaultApplication);

        User UserInfo { get;}
    }
}
