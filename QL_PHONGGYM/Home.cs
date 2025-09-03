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
        public Home()
        {
            InitializeComponent();
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            string userName = CurrentAccount.Username;
            string updateSession = "UPDATE ADMIN123.ACCOUNTS SET SessionActive = 0 WHERE USERNAME ='" + userName + "'";
            Modify modify = new Modify();
            modify.AddSession(updateSession);

            MessageBox.Show("Đăng xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Login login = new Login();
            login.Show();
            this.Close();
        }

        private Timer sessionTimer;
        private void Home_Load(object sender, EventArgs e)
        {
            sessionTimer = new Timer();
            sessionTimer.Interval = 1000; // 1 giây check 1 lần
            sessionTimer.Tick += SessionTimer_Tick;
            sessionTimer.Start();
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            string userName = CurrentAccount.Username;
            string checkSession = "SELECT SessionActive FROM ADMIN123.ACCOUNTS WHERE USERNAME ='" + userName + "'";
            Modify modify = new Modify();
            int session = modify.CheckSession(checkSession);
            if (session == 0)
            {
                MessageBox.Show("Phiên làm việc đã hết hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogoutBtn.PerformClick();
            }
        }
    }
}
