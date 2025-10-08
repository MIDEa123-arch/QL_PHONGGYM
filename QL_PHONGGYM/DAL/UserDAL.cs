using Oracle.ManagedDataAccess.Client;
using System;
using System.CodeDom;
using System.Collections.Generic;
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

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"create user {username} identified by {password}";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"grant create session to {username}";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"grant app_user_role to {username}";
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
            using (var adminConn = ConnectionHelper.GetConnection())
            {
                adminConn.Open();

                using (var cmd = adminConn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT sid, serial# FROM v$session WHERE username = '{username.ToUpper()}'";//bảng session của user
                    using (var r = cmd.ExecuteReader()) // đọc từng dòng trong bảng
                    {
                        List<string> sessions = new List<string>();

                        while (r.Read()) //khi dòng còn dữ liệu
                        {
                            sessions.Add($"{r["SID"]},{r["SERIAL#"]}");
                        }

                        foreach (var s in sessions)
                        {
                            using (var killCmd = adminConn.CreateCommand())
                            {
                                killCmd.CommandText = $"ALTER SYSTEM KILL SESSION '{s}' IMMEDIATE";
                                killCmd.ExecuteNonQuery();
                            }
                        }
                    }    
                }

            }    
        }
    }
}
