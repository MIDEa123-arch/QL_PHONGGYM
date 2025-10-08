using Oracle.ManagedDataAccess.Client;
using QL_PHONGGYM.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
       
        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            string userName = UsernameInput.Text.Trim();
            string password = PassInput.Text.Trim();
            string confirmPass = RepassInput.Text.Trim();      
          

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

            // Thêm tài khoản
            try
            {
                string EncrypPassword = MaHoa.MaHoaNhan(password, 23);
                UserDAL userDAL = new UserDAL();
                userDAL.CreateUser(userName, EncrypPassword);

                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // quay về login
            }
            catch (OracleException ex)
            {
                if (ex.Number == 1920) // ORA-01920 user exists
                    MessageBox.Show("Tên tài khoản này đã được đăng ký!");
                else
                    MessageBox.Show("Lỗi: " + ex.Message);
            }

        }
    }
}

