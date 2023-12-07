using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace Dapper.Infrastructure.DapperContext
{
    public class DatabaseContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;
        public DatabaseContext(IConfiguration _config)
        {
            _configuration = _config;
            _connString = _config.GetConnectionString("DefaultConnection");

        }
        public IDbConnection CreateConnection() => new SqlConnection(_connString);
    }
}
