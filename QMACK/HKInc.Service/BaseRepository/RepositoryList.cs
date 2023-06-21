using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Utils.Interface.Service;

namespace HKInc.Service.BaseRepository
{
    public static class RepositoryList
    {
        private static Dictionary<decimal, IDisposable> RepositoryDic = new Dictionary<decimal, IDisposable>();

        public static decimal MenuId;

        public static void AddService(IDisposable obj)
        {
            AddService(MenuId, obj);
        }

        public static void RemoveService(decimal key)
        {
            IDisposable obj;

            if (RepositoryDic.TryGetValue(key, out obj))
            {
                obj.Dispose();
                RemoveWithKey(key);
            }
        }

        private static void AddService(decimal key, IDisposable obj)
        {
            IDisposable testObj;

            if (RepositoryDic.TryGetValue(key, out testObj))
                RemoveWithKey(key);

            RepositoryDic.Add(key, obj);
        }

        private static void RemoveWithKey(decimal key)
        {
            RepositoryDic.Remove(key);
        }
    }
}