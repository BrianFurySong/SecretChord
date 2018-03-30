using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SecretChord.Services.Tools
{
    public class AppConfiguration : BaseService
    {
        private static readonly AppConfiguration _instance = new AppConfiguration();
        static AppConfiguration() { }
        private AppConfiguration() { }
        public static AppConfiguration Instance { get { return _instance; } }

        public string AppGetByKey(string key)
        {
            return Adapter.ExecuteScalar("dbo.AppConfig_SelectByKey", new[] { new SqlParameter("@ConfigKey", key) }).ToString();

        }

    }
}