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
        OracleCommand command; //dùng để thực thi câu lệnh truy vấn
        OracleDataReader reader; //Đọc dữ liệu bảng
        public List<Account> Accounts(string query)
        {
            List<Account> accounts = new List<Account>();
            
            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();
              
                command = new OracleCommand(query, oracleConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    accounts.Add(new Account(reader.GetString(0), reader.GetString(1)));
                }
                oracleConnection.Close();
            }

            return accounts;
        }

        public void AddSession(string query)
        {
            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();

                command = new OracleCommand(query, oracleConnection);
                command.ExecuteNonQuery(); //Thực thi câu lệnh truy vấn
                oracleConnection.Close();
            }    
        }
        public void Command(string query)//Đăng ký tài khoản
        {
            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();
                command = new OracleCommand(query, oracleConnection);
                command.ExecuteNonQuery();
                oracleConnection.Close();
            }
        }

        public int CheckSession(string query)
        {
            int session = 0;
            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();
                command = new OracleCommand(query, oracleConnection);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    session = reader.GetInt32(0);
                }
                oracleConnection.Close();
            }
            return session;
        }

        public void AddCustomer(string query)
        {
            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();

                command = new OracleCommand(query, oracleConnection);
                command.ExecuteNonQuery(); 

                oracleConnection.Close();
            }
        }
    }
}

