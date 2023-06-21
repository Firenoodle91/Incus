using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Utils.Enum;

namespace HKInc.Utils.Common
{
    public static class ServerInfo
    {
        // DB Login UserId / Password
        //private static readonly string password = Encrypt.AESEncrypt256.Decrypt(Properties.Settings.Default.Password, HKInc.Utils.Common.GlobalVariable.EncryptionKey);
        //private static readonly string userId = Encrypt.AESEncrypt256.Decrypt(Properties.Settings.Default.UserId, HKInc.Utils.Common.GlobalVariable.EncryptionKey);
        private static readonly string password = Properties.Settings.Default.Password;
        private static readonly string userId = Properties.Settings.Default.UserId;
        // Server가 여러개일 경우 선택할때 Globalbariable.LoingServer등으로 해서구분해야한다.
        //private static readonly string dataSource = Properties.Settings.Default.DefaultServerName;
        private static string dataSource = GlobalVariable.DatabaseIP == null ? Properties.Settings.Default.DefaultServerName : GlobalVariable.DatabaseIP;

        // DB name은 여러 서버에서 동일하게 사용하도록 한다.
        private static readonly string applicationDatabase = Properties.Settings.Default.DefaultDatabaseName;
        private static readonly string productionDatabase = Properties.Settings.Default.DefaultDatabaseName;
     
        

        public static string Password { get { return password; } set { Password = value; } }
        public static string UserId { get { return userId; } set { UserId = value; } }
        public static string Database { get { return applicationDatabase; } }
        public static string Server { get { return dataSource; } set { dataSource = value; } }
        //public static string Server { get { return dataSource; } }
        public static string ProductionDatabase { get { return productionDatabase; } }
  
        

        private static Dictionary<Enum.DatabaseCategory, string> ConnectStringList = new Dictionary<Enum.DatabaseCategory, string>()
        {
            {Enum.DatabaseCategory.DefaultApplication, string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", dataSource, applicationDatabase, userId, password)},
            // 20210716 오세완 차장 하단처럼 하면 다른 스레드에서 실행되고 있어서 안된다는 메시지가 나온다. 
            //{Enum.DatabaseCategory.DefaultApplication, string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}; MultipleActiveResultSets=True", dataSource, applicationDatabase, userId, password)}, 

            {Enum.DatabaseCategory.DefaultProduction, string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", dataSource, productionDatabase, userId, password)},
            //{Enum.DatabaseCategory.DefaultProduction, string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}; MultipleActiveResultSets=True", dataSource, productionDatabase, userId, password)},

        };

        public static string GetConnectString(DatabaseCategory databaseCategory, object p)
        {
            throw new NotImplementedException();
        }

        public static void ConnectStringListChange()
        {
            ConnectStringList[Enum.DatabaseCategory.DefaultApplication] = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", dataSource, applicationDatabase, userId, password);
            
            ConnectStringList[Enum.DatabaseCategory.DefaultProduction] = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", dataSource, productionDatabase, userId, password);
           
        }

        private static readonly Dictionary<string, Enum.DatabaseCategory> RegistryList = new Dictionary<string, Enum.DatabaseCategory>()
        {
            {"DefaultApplication", Enum.DatabaseCategory.DefaultApplication}            
        };

        private static readonly Dictionary<Enum.ServerCategory, string> ServerList = new Dictionary<Enum.ServerCategory, string>()
        {
           {Enum.ServerCategory.DefaultServer,  Properties.Settings.Default.DefaultServerName}
        };

        public static string GetConnectString(Enum.DatabaseCategory category = Enum.DatabaseCategory.DefaultApplication)
        {
            return ConnectStringList[category];            
        }

        public static Enum.DatabaseCategory GetDatabaseCategory(string registryValue = "DefaultApplication")
        {
            return RegistryList[registryValue];
        }

        public static string GetServerName(Enum.ServerCategory serverCategory = Enum.ServerCategory.DefaultServer)
        {
            return ServerList[serverCategory];
        }
    }
}
