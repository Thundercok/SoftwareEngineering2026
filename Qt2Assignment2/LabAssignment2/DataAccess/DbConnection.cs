using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DbConnection
    {
        private static string _connStr =
            ConfigurationManager.ConnectionStrings["LabDB"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connStr);
        }
    }
}