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
<<<<<<< HEAD
        public void Register(string username, string password, int k)
        {
            // Mã hóa mật khẩu
            string encryptedPassword = MaHoa.MaHoaNhan(password, k);

            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();
                string query = "INSERT INTO ACCOUNT (USERNAME, PASSWORD) VALUES (:username, :password)";

                using (OracleCommand command = new OracleCommand(query, oracleConnection))
                {
                    command.Parameters.Add(new OracleParameter(":username", username));
                    command.Parameters.Add(new OracleParameter(":password", encryptedPassword));
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool Login(string username, string password, int k)
        {
            string encryptedPassword = null;

            using (OracleConnection oracleConnection = Connection.GetDBConnection())
            {
                oracleConnection.Open();
                string query = "SELECT PASSWORD FROM ACCOUNT WHERE USERNAME = :username";

                using (OracleCommand command = new OracleCommand(query, oracleConnection))
                {
                    command.Parameters.Add(new OracleParameter(":username", username));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            encryptedPassword = reader.GetString(0);
                        }
                    }
                }
            }

            if (encryptedPassword == null)
                return false;

            // Giải mã mật khẩu
            string decryptedPassword = GiaiMa.GiaiMaNhan(encryptedPassword, k);

            return password == decryptedPassword;
        }
=======

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
>>>>>>> 2de54cce24070f4934deccfd4c19d7e5c0bdcc3b
    }
}
