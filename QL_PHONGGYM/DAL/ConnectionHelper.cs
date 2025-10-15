using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace QL_PHONGGYM.DAL
{
    public static class ConnectionHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
        
        public static OracleConnection GetConnection()
        {
            OracleConnection conn = new OracleConnection(connStr);

            return conn;
        }

        public static OracleConnection GetConnectionUser(string username, string password)
        {
            string userConnStr = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=orcl2)));User Id={username};Password={password};";
            OracleConnection conn = new OracleConnection(userConnStr);

            return conn;
        }       
    }
}
