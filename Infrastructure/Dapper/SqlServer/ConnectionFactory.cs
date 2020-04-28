using Infrastructure.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Dapper.SqlServer
{
    public class ConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection
        {
            get
            {
                var connStr = _configuration.GetConnectionString("SampleNetCoreSlnDB");
                if (string.IsNullOrEmpty(connStr))
                {
                    throw new InvalidProgramException("Database connection string SampleNetCoreSlnDB not initialized");
                }
                 return new SqlConnection(connStr);
            }
        }
    }
}
