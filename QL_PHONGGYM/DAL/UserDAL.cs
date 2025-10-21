using Oracle.ManagedDataAccess.Client;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_PHONGGYM.DAL
{
    public class UserDAL
    {
        public void CreateUser(string username, string password)
        {
            using (var conn = ConnectionHelper.GetConnection())
            {
                conn.Open();

                using (var cmd = new OracleCommand("PROC_CREATE_USER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public OracleConnection LoginUser(string username, string password)
        {
            try
            {
                var conn = ConnectionHelper.GetConnectionUser(username, password);
                conn.Open();
                return conn;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public void LogOutUser(string username)
        {           
            using (var conn = ConnectionHelper.GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PROC_LOGOUT_USER";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }

}

