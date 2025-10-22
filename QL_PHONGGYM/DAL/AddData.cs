using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
namespace QL_PHONGGYM.DAL
{
    public class AddData
    {

        public static bool ExecuteNonQueryProcedure(string procedureName, OracleParameter[] parameters, OracleConnection conn)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    
                    if (conn.State != ConnectionState.Open)
                        throw new ApplicationException("Phiên của bạn đã kết thúc ở 1 nơi khác, vui lòng đăng nhập lại.");

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }        
            catch (OracleException ex)
            {
                switch (ex.Number)
                {                  
                    case 1031: 
                        throw new ApplicationException("Tài khoản của bạn không có quyền thực hiện thao tác này.");
                    default:
                        throw new Exception("Lỗi Oracle: " + ex.Message, ex);
                }
            }
        }



        public static bool ThemKhachHang(string tenKH, string gioiTinh, DateTime? ngaySinh, string sdt, string email, int? maLoaiKH, OracleConnection conn)
        {
            OracleParameter[] parameters = new OracleParameter[]
            {

                new OracleParameter("p_TenKH", OracleDbType.NVarchar2, tenKH, ParameterDirection.Input),
                new OracleParameter("p_GioiTinh", OracleDbType.NVarchar2, gioiTinh, ParameterDirection.Input),


                new OracleParameter("p_NgaySinh", OracleDbType.Date,
                    ngaySinh.HasValue ? (object)ngaySinh.Value : DBNull.Value, ParameterDirection.Input),


                new OracleParameter("p_SDT", OracleDbType.NVarchar2,
                    string.IsNullOrEmpty(sdt) ? DBNull.Value : (object)sdt, ParameterDirection.Input),
                new OracleParameter("p_Email", OracleDbType.NVarchar2,
                    string.IsNullOrEmpty(email) ? DBNull.Value : (object)email, ParameterDirection.Input),


                new OracleParameter("p_MaLoaiKH", OracleDbType.Int32,
                    maLoaiKH.HasValue ? (object)maLoaiKH.Value : DBNull.Value, ParameterDirection.Input)
            };


            return ExecuteNonQueryProcedure("ADMIN123.SP_THEMKHACHHANG", parameters, conn);
        }
        public static bool SuaKhachHang(
            string oldSDT,      // SĐT gốc để TÌM
            string tenKH,       // Dữ liệu mới
            string gioiTinh,    // Dữ liệu mới
            DateTime? ngaySinh, // Dữ liệu mới
            string newSDT,      // Dữ liệu mới
            string newEmail,    // Dữ liệu mới
            int? maLoaiKH,    // Dữ liệu mới
            OracleConnection conn
        )
        {
            OracleParameter[] parameters = new OracleParameter[]
            {

                new OracleParameter("p_OldSDT", OracleDbType.NVarchar2, oldSDT, ParameterDirection.Input),


                new OracleParameter("p_TenKH", OracleDbType.NVarchar2, tenKH, ParameterDirection.Input),
                new OracleParameter("p_GioiTinh", OracleDbType.NVarchar2, gioiTinh, ParameterDirection.Input),

                new OracleParameter("p_NgaySinh", OracleDbType.Date,
                    ngaySinh.HasValue ? (object)ngaySinh.Value : DBNull.Value, ParameterDirection.Input),


                new OracleParameter("p_SDT", OracleDbType.NVarchar2,
                    string.IsNullOrEmpty(newSDT) ? DBNull.Value : (object)newSDT, ParameterDirection.Input),


                new OracleParameter("p_Email", OracleDbType.NVarchar2,
                    string.IsNullOrEmpty(newEmail) ? DBNull.Value : (object)newEmail, ParameterDirection.Input),

                new OracleParameter("p_MaLoaiKH", OracleDbType.Int32,
                    maLoaiKH.HasValue ? (object)maLoaiKH.Value : DBNull.Value, ParameterDirection.Input)
            };


            return ExecuteNonQueryProcedure("ADMIN123.SP_SUAKHACHHANG", parameters, conn);
        }
        public static bool XoaKhachHang(string sdt, OracleConnection conn)
        {
            OracleParameter[] parameters = new OracleParameter[]
            {

            new OracleParameter("p_SDT", OracleDbType.NVarchar2, sdt, ParameterDirection.Input)
            };

            return ExecuteNonQueryProcedure("ADMIN123.SP_XOAKHACHHANG", parameters, conn);
        }
    }
}
