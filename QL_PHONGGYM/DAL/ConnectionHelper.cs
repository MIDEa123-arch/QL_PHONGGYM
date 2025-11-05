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
            string host = "100.115.143.84";
            string port = "1521";
            string sid = "orcl2";

            string connStr; 
            if (username.ToUpper() == "SYS")
            {
                connStr = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SID={sid})));User Id={username};Password={password};DBA Privilege=SYSDBA;";
            }
            else
            {
                connStr = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SID={sid})));User Id={username};Password={password};";
            }

            OracleConnection conn = new OracleConnection(connStr);
            return conn;
        }

    }
}
