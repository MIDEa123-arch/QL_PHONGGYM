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
            return Regex.IsMatch(Acc, "^[a-zA-Z0-9]{6,30}$");
        }

        public bool checkEmail(string email)//check email
        {
            return Regex.IsMatch(email, @"^[\w]{3,30}@gmail.com(.vn|)$");
        }

        Modify modify = new Modify();
        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            string userName = UsernameInput.Text.Trim();
            string password = PassInput.Text.Trim();
            string confirmPass = RepassInput.Text.Trim();
            string email = EmailInput.Text.Trim();

            // Validate dữ liệu
            if (!checkAccount(userName))
            {
                MessageBox.Show("Tên tài khoản không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!checkAccount(password))
            {
                MessageBox.Show("Mật khẩu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (confirmPass != password)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!checkEmail(email))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string passwordEncrypted = MaHoa.MaHoaNhan(password, 23);
            string emailEncrypted = MaHoa.MaHoaNhan(email, 23);
            // Kiểm tra email tồn tại
            if (modify.Accounts($"SELECT * FROM ADMIN123.ACCOUNTS WHERE EMAIL = '{emailEncrypted}'").Count != 0)
            {
                MessageBox.Show("Email đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm tài khoản
            try
            {
                string query = $"INSERT INTO ADMIN123.ACCOUNTS (USERNAME, PASSWORD, EMAIL, SESSIONACTIVE) VALUES ('{userName}','{passwordEncrypted}','{emailEncrypted}', 0)";
                modify.Command(query);

                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // quay về login
            }
            catch
            {
                MessageBox.Show("Tên tài khoản này đã được đăng ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

