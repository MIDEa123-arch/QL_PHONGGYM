using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
namespace QL_PHONGGYM
{
    class Connection
    {
        public static readonly string StringConn = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;

        public static OracleConnection GetDBConnection()
        {
            OracleConnection conn = new OracleConnection(StringConn);
            return conn;
        }
    }
}
