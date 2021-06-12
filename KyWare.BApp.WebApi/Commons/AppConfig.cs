using KyWare.BApp.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KyWare.BApp.WebApi.Commons
{
    public static class AppConfig
    {
        public static IConfiguration Configuration { get; set; }
        public static string ConnectionString { get; set; }

        public static EnumDatabaseProvider GetCurrentDbProvider()
        {
            var dbProvider = AppConfig.Configuration.GetValue<String>("DbProvider");
            switch (dbProvider)
            {
                case "sqlserver":
                    return EnumDatabaseProvider.SqlServer;
                case "postgresql":
                    return EnumDatabaseProvider.Postgresql;
                default:
                    return EnumDatabaseProvider.None;
            }
        }
    }
}
