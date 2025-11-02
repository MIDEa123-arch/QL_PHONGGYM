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

        public bool InsertNewCustomer(
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

                    // Tham số p_TenKH NVARCHAR2
                    cmd.Parameters.Add("p_TenKH", OracleDbType.NVarchar2).Value = tenKhachHang;

                    // Tham số p_GioiTinh NVARCHAR2
                    cmd.Parameters.Add("p_GioiTinh", OracleDbType.NVarchar2).Value = gioiTinh;

                    // Tham số p_NgaySinh DATE
                    cmd.Parameters.Add("p_NgaySinh", OracleDbType.Date).Value = ngaySinh;

                    // Tham số p_SDT NVARCHAR2
                    cmd.Parameters.Add("p_SDT", OracleDbType.NVarchar2).Value = soDienThoai;

                    // Tham số p_Email NVARCHAR2
                    cmd.Parameters.Add("p_Email", OracleDbType.NVarchar2).Value = email;

                    // Tham số p_MaLoaiKH INT
                    cmd.Parameters.Add("p_MaLoaiKH", OracleDbType.Int32).Value = maLoaiKhachHang;

                    if (conn.State != ConnectionState.Open) conn.Open();
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
