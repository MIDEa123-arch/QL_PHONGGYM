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
            string updateSession = "UPDATE ADMIN123.ACCOUNTS SET SESSIONACTIVE = 0 WHERE USERNAME ='" + userName + "'";
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
            string checkSession = "SELECT SESSIONACTIVE FROM ADMIN123.ACCOUNTS WHERE USERNAME ='" + userName + "'";
            Modify modify = new Modify();
            int session = modify.CheckSession(checkSession);
            if (session == 0)
            {
                MessageBox.Show("Phiên làm việc đã hết hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogoutBtn.PerformClick();
            }
        }

        private void AddCustomer_Click(object sender, EventArgs e)
        {
            string id = IdKH.Text;
            string name = TenKH.Text;
            string cccd = CCCD.Text;
            string phone = SdtKH.Text;
            string gender = GioiTinh.Text;
            DateTime birth = NgaySinh.Value;
            Modify modify = new Modify();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng nhập ID khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cccd = MaHoa.MaHoaNhan(cccd, 23);
            phone = MaHoa.MaHoaNhan(cccd, 23);

            string query = "INSERT INTO KHACHHANG (ID_KH, TENKH, CCCD, SDT, GIOITINH, NGAYSINH) " +
                "VALUES ('" + id + "', '" + name + "', '" + cccd + "', '" + phone + "', '" + gender +
                "', TO_DATE('" + birth.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'))";

            try
            {
                modify.AddCustomer(query);
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
