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
using Oracle.ManagedDataAccess.Client; 

namespace QL_PHONGGYM
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void ForgetPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgetPassword forgetPassword = new ForgetPassword();
            forgetPassword.ShowDialog();
        }

        private void Register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }
     
        private void Loginbtn_Click(object sender, EventArgs e)
        {
            string userName = UsernameInput.Text;
            string password = PasswordInput.Text;

            if (userName.Trim() == "") { MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (password.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                UserDAL userDAL = new UserDAL();
                try
                {
                    string EncrypPassword = MaHoa.MaHoaNhan(password, 23);
                    var conn = userDAL.LoginUser(userName, EncrypPassword);
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Home home = new Home(userName, conn);
                    this.Hide();
                    home.ShowDialog();
                    this.Close();
                }
                catch (OracleException ex)
                {
                    if (ex.Number == 1017) // ORA-01017: invalid username/password
                        MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (ex.Number == 28000) // ORA-28000: user locked
                        MessageBox.Show("Tài khoản đang bị khóa. Liên hệ admin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Lỗi Oracle: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }        
    }
}
