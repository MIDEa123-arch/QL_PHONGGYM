using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_PHONGGYM
{
    public class Modify
    {
        public Modify() { }       
        public List<Account> Accounts(string query)
        {
            List<Account> accounts = new List<Account>();

            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();

                OracleCommand command = new OracleCommand(query, oracleConnection);
                using (OracleDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        accounts.Add(new Account(dataReader.GetString(0), dataReader.GetString(1)));
                    }
                }
            }

            return accounts;
        }

        public void Command(string query)//Đăng ký tài khoản
        {
            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();
                OracleCommand command = new OracleCommand(query, oracleConnection);
                command.ExecuteNonQuery();
                oracleConnection.Close();
            }
        }
    }
}

