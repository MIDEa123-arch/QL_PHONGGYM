using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
namespace QL_PHONGGYM.DAL
{
    public class AddData
    {
        public void TaoHoaDon(OracleConnection conn, int maKH, decimal tongTien, decimal giamGia = 0)
        {
            using (var cmd = new OracleCommand("ADMIN123.sp_LapHoaDon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_MaKH", OracleDbType.Int32).Value = maKH;
                cmd.Parameters.Add("p_TongTien", OracleDbType.Decimal).Value = tongTien;
                cmd.Parameters.Add("p_GiamGia", OracleDbType.Decimal).Value = giamGia;

                if (conn.State != ConnectionState.Open) conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int? InsertNewCustomer(
                 OracleConnection conn,
                 string tenKhachHang,
                 string gioiTinh,
                 DateTime ngaySinh,
                 string soDienThoai,
                 string email,
                 int maLoaiKhachHang)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("ADMIN123.sp_ThemKhachHang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_TenKH", OracleDbType.NVarchar2).Value = tenKhachHang;
                    cmd.Parameters.Add("p_GioiTinh", OracleDbType.NVarchar2).Value = gioiTinh;
                    cmd.Parameters.Add("p_NgaySinh", OracleDbType.Date).Value = ngaySinh;

                    cmd.Parameters.Add("p_SDT", OracleDbType.NVarchar2).Value =
                        string.IsNullOrEmpty(soDienThoai) ? (object)DBNull.Value : soDienThoai;

                    cmd.Parameters.Add("p_Email", OracleDbType.NVarchar2).Value =
                        string.IsNullOrEmpty(email) ? (object)DBNull.Value : email;

                    cmd.Parameters.Add("p_MaLoaiKH", OracleDbType.Int32).Value = maLoaiKhachHang;

                    var p_MaKHMoi = new OracleParameter("p_MaKHMoi_out", OracleDbType.Int32, ParameterDirection.Output);
                    cmd.Parameters.Add(p_MaKHMoi);

                    if (conn.State != ConnectionState.Open) conn.Open();
                    cmd.ExecuteNonQuery();

                    try
                    {
                        int maKHMoi = Convert.ToInt32(p_MaKHMoi.Value.ToString());
                        return maKHMoi;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể lấy Mã Khách Hàng mới trả về.", "Lỗi Parameter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}