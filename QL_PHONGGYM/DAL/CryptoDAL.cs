using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_PHONGGYM.DAL
{
    public class CryptoDAL
    {
        private OracleConnection conn;

        public CryptoDAL(OracleConnection connection)
        {
            conn = connection;
        }

        // ---------------- MÃ HÓA CỘNG ----------------
        public string MaHoaCong(string input, int key)
        {
            using (OracleCommand cmd = new OracleCommand("PROC_MAHOA_CONG", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_input", OracleDbType.Varchar2).Value = input;
                cmd.Parameters.Add("p_key", OracleDbType.Int32).Value = key;
                cmd.Parameters.Add("p_output", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return cmd.Parameters["p_output"].Value.ToString();
            }
        }

        // ---------------- GIẢI MÃ CỘNG ----------------
        public string GiaiMaCong(string input, int key)
        {
            using (OracleCommand cmd = new OracleCommand("PROC_GIAIMA_CONG", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_input", OracleDbType.Varchar2).Value = input;
                cmd.Parameters.Add("p_key", OracleDbType.Int32).Value = key;
                cmd.Parameters.Add("p_output", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return cmd.Parameters["p_output"].Value.ToString();
            }
        }

        // ---------------- MÃ HÓA NHÂN ----------------
        public string MaHoaNhan(string input, int key)
        {
            using (OracleCommand cmd = new OracleCommand("PROC_MAHOA_NHAN", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_input", OracleDbType.Varchar2).Value = input;
                cmd.Parameters.Add("p_key", OracleDbType.Int32).Value = key;
                cmd.Parameters.Add("p_output", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return cmd.Parameters["p_output"].Value.ToString();
            }
        }

        // ---------------- GIẢI MÃ NHÂN ----------------
        public string GiaiMaNhan(string input, int key)
        {
            using (OracleCommand cmd = new OracleCommand("PROC_GIAIMA_NHAN", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_input", OracleDbType.Varchar2).Value = input;
                cmd.Parameters.Add("p_key", OracleDbType.Int32).Value = key;
                cmd.Parameters.Add("p_output", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return cmd.Parameters["p_output"].Value.ToString();
            }
        }

        // ---------------- XUẤT DỮ LIỆU LỚP HỌC ----------------
        public DataTable XuatLopHoc()
        {
            DataTable dt = new DataTable();

            using (OracleCommand cmd = new OracleCommand("SELECT MaLop, TenLop, MaCM, HocPhi FROM LopHoc", conn))
            {
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}
