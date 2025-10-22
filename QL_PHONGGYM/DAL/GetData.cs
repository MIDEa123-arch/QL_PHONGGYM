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
        public static DataTable GetDataTableFromProcedure(string procedureName, OracleParameter[] inParameters, OracleConnection conn)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OracleCommand cmd = new OracleCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (inParameters != null)
                    {
                        cmd.Parameters.AddRange(inParameters);
                    }
                    cmd.Parameters.Add(new OracleParameter("p_Cursor", OracleDbType.RefCursor, ParameterDirection.Output));

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy dữ liệu từ Proc: " + ex.Message);
            }
            return dt;
        }
     

        public static DataTable LayDuLieuLoaiKH(OracleConnection conn)
        {
            return GetDataTableFromProcedure("ADMIN123.SP_LAYDANHSACHLOAIKH", null, conn);
        }

    }
}
