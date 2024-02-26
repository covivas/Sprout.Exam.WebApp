using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sprout.Exam.DataAccess.Data
{
    public class WebAppContext 
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public WebAppContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
        
        
    }
}
