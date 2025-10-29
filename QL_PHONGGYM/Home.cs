using Oracle.ManagedDataAccess.Client;
using QL_PHONGGYM.DAL;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
namespace QL_PHONGGYM
{
    public partial class Home : Form
    {
        private string currentUser;
        private OracleConnection conn;

        // Biến tạm để lưu SĐT gốc (dùng cho Sửa/Xóa)
        private string originalSdtForUpdate;

        public Home(string userName, OracleConnection connection)
        {
            InitializeComponent();
            currentUser = userName;
            conn = connection;
        }     
       
        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            try
            {               
                UserDAL userDAL = new UserDAL();
                userDAL.LogOutUser(currentUser);

                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }

                this.Hide();
                Login loginForm = new Login();
                loginForm.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi logout: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}