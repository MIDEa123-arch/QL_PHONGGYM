using Oracle.ManagedDataAccess.Client;
using QL_PHONGGYM.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QL_PHONGGYM
{
    public partial class Home : Form
    {
        private string currentUser;
        private OracleConnection conn;        

        public Home(string userName, OracleConnection connection)
        {
            InitializeComponent();
            currentUser =  userName;
            conn = connection;
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {

            UserDAL userDAL = new UserDAL();
            try
            {
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
