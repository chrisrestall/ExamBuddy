using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Providers
{
    public class SqlServerConnectionProvider : ISqlServerConnectionProvider
    {
        private readonly string _connectionString;
        public SqlServerConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public SqlServerConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
