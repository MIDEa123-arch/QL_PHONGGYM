using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace QL_PHONGGYM
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        public bool checkAccount(string Acc)//Kiểm tra mk vs tk
        {
            return Regex.IsMatch(Acc,"^[a-zA-Z0-9]{6,30}$");
        }

        public bool checkEmail(string email)//check email
        {
            return Regex.IsMatch(email, @"^[\w]{3,30}@gmail.com(.vn|)$");
        }  

        Modify modify = new Modify();
        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            string userName = UsernameInput.Text;
            string password = PassInput.Text;
            string confirmPass = RepassInput.Text;
            string email = EmailInput.Text;

            if (!checkAccount(userName))
            {
                MessageBox.Show("Tên tài khoản không hợp lệ! (6-30 ký tự, không chứa ký tự đặc biệt)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!checkAccount(password))
            {
                MessageBox.Show("Mật khẩu không hợp lệ! (6-30 ký tự, không chứa ký tự đặc biệt)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (confirmPass != password)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!checkEmail(email))
            {
                MessageBox.Show("Email không hợp lệ! (ví dụ: abc@gmail.com)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (modify.Accounts("SELECT * FROM ADMIN123.ACCOUNTS WHERE EMAIL = '" + email + "'").Count != 0)
            {
                MessageBox.Show("Email đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                string query = "INSERT INTO ADMIN123.ACCOUNTS (USERNAME, PASSWORD, EMAIL) VALUES ('" + userName + "','" + password + "','" + email + "')";
                modify.Command(query);

                if (MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Tên tài khoản này đã được đăng ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
    }
}
