using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_PHONGGYM.DAL
{
    public class GetData
    {

        public DataTable LoadKhachHangData(OracleConnection conn)
        {
            DataTable dt = new DataTable();
            // Khai báo Command ở ngoài try để dùng được trong finally
            OracleCommand cmd = null;

            // Kiểm tra an toàn đối tượng kết nối
            if (conn == null) return null;

            try
            {
                cmd = new OracleCommand("ADMIN123.sp_XemKhachHang", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                // Mở kết nối nếu chưa mở
                if (conn.State != ConnectionState.Open) conn.Open();

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Khách Hàng: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                // *** Đảm bảo đóng kết nối để giải phóng trạng thái (RẤT QUAN TRỌNG) ***
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (cmd != null) cmd.Dispose();
            }
        }

        public DataTable GetLoaiKhachHangList(OracleConnection conn)
        {
            DataTable dt = new DataTable();
            OracleCommand cmd = null;

            // Kiểm tra an toàn đối tượng kết nối
            if (conn == null) return null;

            try
            {
                cmd = new OracleCommand("ADMIN123.sp_LayDanhSachLoaiKH", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                // Mở kết nối nếu chưa mở
                if (conn.State != ConnectionState.Open) conn.Open();

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách Loại Khách Hàng: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                // *** Đảm bảo đóng kết nối để giải phóng trạng thái (RẤT QUAN TRỌNG) ***
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (cmd != null) cmd.Dispose();
            }
        }

    }
}
