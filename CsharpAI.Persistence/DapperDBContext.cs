using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CsharpAI.Persistence
{
    public class DapperDBContext : IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public DapperDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Returns an open database connection
        /// </summary>
        public IDbConnection CreateConnection()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }
            return _connection;
        }

        /// <summary>
        /// Disposes of the database connection
        /// </summary>
        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}