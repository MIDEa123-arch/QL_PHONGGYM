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

        Modify modify = new Modify();        
        private void Loginbtn_Click(object sender, EventArgs e)
        {
            string userName = UsernameInput.Text;
            string password = PasswordInput.Text;

            if(userName.Trim() == "") { MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);}
            else if (password.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);}
            else
            {
                string query = "SELECT USERNAME, PASSWORD FROM ADMIN123.ACCOUNTS WHERE USERNAME ='" + userName + "'AND PASSWORD='" + password + "'";
                if (modify.Accounts(query).Count != 0)
                {
                    string updateSession = "UPDATE ADMIN123.ACCOUNTS SET SessionActive = 1 WHERE USERNAME ='" + userName + "'";
                    CurrentAccount.Username = userName;//Lưu tên tài khoản hiện tại
                    modify.AddSession(updateSession);
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }
                else
                {                    
                    MessageBox.Show("Đăng nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            try
            {
                string query = $"SELECT PASSWORD FROM ADMIN123.ACCOUNTS WHERE USERNAME = '{userName}'";
                var accounts = modify.Accounts(query);

                if (accounts.Count == 0)
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string passwordEncrypted = accounts[0].PassWord;

                // Giải mã mật khẩu
                int k = 23;
                string passwordDecrypted = GiaiMa.GiaiMaNhan(passwordEncrypted, k);

                if (PasswordInput.Text == passwordDecrypted)
                {
                    string updateSession = $"UPDATE ADMIN123.ACCOUNTS SET SessionActive = 1 WHERE USERNAME = '{userName}'";
                    CurrentAccount.Username = userName;
                    modify.AddSession(updateSession);

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi đăng nhập: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
