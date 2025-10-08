using Oracle.ManagedDataAccess.Client;
using QL_PHONGGYM.DAL;
using System;
using System.Data;
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
            currentUser = userName;
            conn = connection;
        }

        private void Home_Load(object sender, EventArgs e)
        {            
            LopHocTable.Visible = false;
        }
        
        private void LoadLopHoc()
        {
            try
            {                                               
                DataTable dt = GetData.TableLopHoc(conn);
              
                LopHocTable.DataSource = null;   
                LopHocTable.DataSource = dt;     
                LopHocTable.ClearSelection();   
                LopHocTable.Refresh();           
                LopHocTable.Visible = true;      
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewListBtn_Click(object sender, EventArgs e)
        {
            LoadLopHoc(); // Click nút hiển thị bảng
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
