using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_PHONGGYM.DAL
{
    public static class GetData
    {
        public static DataTable TableLopHoc(OracleConnection conn)
        {
            DataTable dt = new DataTable();

            try
            {
                if (conn == null || conn.State != ConnectionState.Open)
                    throw new Exception("Chưa có kết nối Oracle hoặc connection chưa mở.");

                using (OracleCommand cmd = new OracleCommand("ADMIN123.PROC_GET_LOPHOC", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm parameter RefCursor
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (OracleException ex)
            {
                throw new Exception("Lỗi khi load dữ liệu: " + ex.Message);
            }

            return dt;
        }
    }
}
