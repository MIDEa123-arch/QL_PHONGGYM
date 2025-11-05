using Oracle.ManagedDataAccess.Client;
using QL_PHONGGYM.Models;
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
        public DoanhThu GetHoaDonByThang(OracleConnection conn, int thang, int nam)
        {            
            DoanhThu result = new DoanhThu();
            result.HoaDonList = new DataTable();
            result.TongDoanhThu = 0;

            try
            {
                using (OracleCommand cmd = new OracleCommand("ADMIN123.sp_GetHoaDonByThang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
 
                    cmd.Parameters.Add("p_Thang", OracleDbType.Int32).Value = thang;
                    cmd.Parameters.Add("p_Nam", OracleDbType.Int32).Value = nam;

                    cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                    var p_TongDoanhThu = new OracleParameter("p_TongDoanhThu", OracleDbType.Decimal, ParameterDirection.Output);
                    cmd.Parameters.Add(p_TongDoanhThu);

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(result.HoaDonList);
                    }
                    result.TongDoanhThu = Convert.ToDecimal(p_TongDoanhThu.Value.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message, "Lỗi Database");
            }
            return result;
        }
        public DataTable GetGoiTapList(OracleConnection conn)
        {
            DataTable dt = new DataTable();
            using (OracleCommand cmd = new OracleCommand("ADMIN123.sp_GetGoiTapList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                conn.Close();
            }
            return dt;
        }

        public DataTable GetLopHocList(OracleConnection conn)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OracleCommand cmd = new OracleCommand("ADMIN123.sp_GetLopHocList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                    if (conn.State != ConnectionState.Open) conn.Open();
                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải Lớp Học: " + ex.Message, "Lỗi Database");
            }
            return dt;
        }

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
