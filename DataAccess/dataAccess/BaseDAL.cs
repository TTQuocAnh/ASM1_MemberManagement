﻿using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


namespace DataAccess
{
        public class BaseDAL
        {
        public StockDataProvider dataProvider { get; set; } = null;
        public SqlConnection connection = null;

        public BaseDAL()
        {
            var connectionString = GetConnectionString();
            dataProvider = new StockDataProvider(connectionString);
        }
        public string GetConnectionString()
        {
            string connectionString ;
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsetting.json", true, true)
                                    .Build();
            connectionString = config["ConnectionString:FStoreDB"];
            return connectionString;                        }
        public void CloseConnection() => dataProvider.CloseConnection(connection);
    }
   
}




