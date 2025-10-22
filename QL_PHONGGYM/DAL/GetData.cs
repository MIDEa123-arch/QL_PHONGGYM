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
        public static DataTable GetKhachHangWithLoaiKH(OracleConnection conn, DataTable dtLoaiKH)
        {
            try
            {
                string procName = "ADMIN123.SP_XEMKHACHHANG";
                OracleParameter[] parameters = null;
                DataTable dt = GetData.GetDataTableFromProcedure(procName, parameters, conn);

                // Tạo bảng tra cứu TenLoaiKH
                var loaiKhLookup = new Dictionary<int, string>();
                if (dtLoaiKH != null)
                {
                    foreach (DataRow dr in dtLoaiKH.Rows)
                    {
                        if (dr["MALOAIKH"] != DBNull.Value)
                        {
                            loaiKhLookup[Convert.ToInt32(dr["MALOAIKH"])] = dr["TENLOAI"].ToString();
                        }
                    }
                }

                // Thêm cột STT và TenLoaiKH
                dt.Columns.Add("STT", typeof(int));
                dt.Columns.Add("TenLoaiKH", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    row["STT"] = i + 1;

                    string tenLoai = "Chưa có";
                    if (row["MaLoaiKH"] != DBNull.Value)
                    {
                        int maLoai = Convert.ToInt32(row["MaLoaiKH"]);
                        if (loaiKhLookup.ContainsKey(maLoai))
                            tenLoai = loaiKhLookup[maLoai];
                        else
                            tenLoai = "Lỗi tra cứu";
                    }
                    row["TenLoaiKH"] = tenLoai;
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải dữ liệu khách hàng: " + ex.Message);
            }
        }
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
