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
        private static string password = GlobalVariable.DBPasswd == null ? Properties.Settings.Default.Password : GlobalVariable.DBPasswd;
        private static string userId = GlobalVariable.DBuser == null ? Properties.Settings.Default.UserId : GlobalVariable.DBuser;
        // Server가 여러개일 경우 선택할때 Globalbariable.LoingServer등으로 해서구분해야한다.
        //private static readonly string dataSource = Properties.Settings.Default.DefaultServerName;
        private static string dataSource = GlobalVariable.DatabaseIP == null ? Properties.Settings.Default.DefaultServerName : GlobalVariable.DatabaseIP;

        // DB name은 여러 서버에서 동일하게 사용하도록 한다.
        private static string applicationDatabase = GlobalVariable.DBName == null ? Properties.Settings.Default.DefaultDatabaseName : GlobalVariable.DBName;
        private static string productionDatabase = GlobalVariable.DBName == null ? Properties.Settings.Default.DefaultDatabaseName : GlobalVariable.DBName;





        public static string Password { get { return password; } set { password = value; } }
        public static string UserId { get { return userId; } set { userId = value; } }
        public static string Database { get { return applicationDatabase; } set { applicationDatabase = value; } }
        public static string Server { get { return dataSource; } set { dataSource = value; } }
        //public static string Server { get { return dataSource; } }
        public static string ProductionDatabase { get { return productionDatabase; } set { productionDatabase = value; } }





        private static Dictionary<Enum.DatabaseCategory, string> ConnectStringList = new Dictionary<Enum.DatabaseCategory, string>()
        {
            {Enum.DatabaseCategory.DefaultApplication, string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", dataSource, applicationDatabase, userId, password)},
            
            {Enum.DatabaseCategory.DefaultProduction, string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", dataSource, productionDatabase, userId, password)},
        
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
