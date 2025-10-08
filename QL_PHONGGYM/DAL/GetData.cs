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

                using (OracleDataAdapter da = new OracleDataAdapter("Select * from  ADMIN123.LOPHOC", conn))
                {
                    da.Fill(dt);
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
