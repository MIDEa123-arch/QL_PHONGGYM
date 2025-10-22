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
        //1. LOAD
        private void Home_Load(object sender, EventArgs e)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                LoadComboBoxes();
                LoadData();
            }
            else
            {
                MessageBox.Show("LỖI: Kết nối 'conn' bị NULL hoặc đang ĐÓNG.", "Lỗi Kết Nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Load += (s, ev) => this.Close();
            }

            // Khởi tạo/Reset form
            originalSdtForUpdate = null;
            ClearInputFields();
            cbo_gt.Items.Clear();
            cbo_gt.Items.AddRange(new object[] {
                    "Nam",
                    "Nữ",
                });
        }

        // 2. HÀM TẢI DATAGRIDVIEW
        private void LoadData()
        {
            try
            {
                DataTable dtLoaiKH = cbo_lkh.DataSource as DataTable;
                DataTable dtKhachHang = GetData.GetKhachHangWithLoaiKH(conn, dtLoaiKH);

                dataGridView1.DataSource = dtKhachHang;

                // Ẩn các cột gốc
                if (dataGridView1.Columns.Contains("MaKH"))
                    dataGridView1.Columns["MaKH"].Visible = false;
                if (dataGridView1.Columns.Contains("MaLoaiKH"))
                    dataGridView1.Columns["MaLoaiKH"].Visible = false;

                // Thiết lập header
                if (dataGridView1.Columns.Contains("STT"))
                {
                    dataGridView1.Columns["STT"].HeaderText = "STT";
                    dataGridView1.Columns["STT"].DisplayIndex = 0;
                    dataGridView1.Columns["STT"].Width = 60;
                }
                if (dataGridView1.Columns.Contains("TenLoaiKH"))
                    dataGridView1.Columns["TenLoaiKH"].HeaderText = "Loại Khách Hàng";
                if (dataGridView1.Columns.Contains("TenKH"))
                    dataGridView1.Columns["TenKH"].HeaderText = "Tên Khách Hàng";
                if (dataGridView1.Columns.Contains("NgaySinh"))
                    dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                if (dataGridView1.Columns.Contains("SDT"))
                    dataGridView1.Columns["SDT"].HeaderText = "Số Điện Thoại";
                if (dataGridView1.Columns.Contains("GioiTinh"))
                    dataGridView1.Columns["GioiTinh"].HeaderText = "Giới Tính";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // 3. HÀM TẢI COMBOBOX
        private void LoadComboBoxes()
        {
            try
            {
                DataTable dtLoaiKH = GetData.LayDuLieuLoaiKH(this.conn);

                DataRow dr = dtLoaiKH.NewRow();
                dr["TENLOAI"] = "-- Không chọn --";
                dr["MALOAIKH"] = DBNull.Value;
                dtLoaiKH.Rows.InsertAt(dr, 0);

                cbo_lkh.DataSource = dtLoaiKH;
                cbo_lkh.DisplayMember = "TENLOAI";
                cbo_lkh.ValueMember = "MALOAIKH";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Giới tính/Loại KH: " + ex.Message);
            }
        }

        // 4. HÀM CELLCLICK 
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    // Gán dữ liệu (đã giải mã) lên các control
                    txt_ten.Text = row.Cells["TenKH"].Value.ToString();
                    txt_sdt.Text = row.Cells["SDT"].Value != DBNull.Value ? row.Cells["SDT"].Value.ToString() : "";
                    txt_email.Text = row.Cells["Email"].Value != DBNull.Value ? row.Cells["Email"].Value.ToString() : "";

                    // Gán cho ComboBox Giới tính
                    cbo_gt.SelectedValue = row.Cells["GioiTinh"].Value.ToString();

                    // Gán cho ComboBox Loại KH
                    if (row.Cells["MaLoaiKH"].Value != DBNull.Value)
                    {
                        cbo_lkh.SelectedValue = Convert.ToInt32(row.Cells["MaLoaiKH"].Value);
                    }
                    else
                    {
                        cbo_lkh.SelectedValue = DBNull.Value; // Chọn dòng "Không chọn"
                    }

                    // Gán cho DateTimePicker
                    if (row.Cells["NgaySinh"].Value != DBNull.Value)
                    {
                        dateTimePicker1.Checked = true;
                        dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    }
                    else
                    {
                        dateTimePicker1.Checked = false;
                    }

                    // Lưu SĐT gốc (plaintext) vào biến tạm
                    originalSdtForUpdate = txt_sdt.Text;
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                catch (OracleException ex)
                {
                    MessageBox.Show("Lỗi Oracle: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // 5. HÀM NÚT THÊM
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra bắt buộc
                if (string.IsNullOrEmpty(txt_ten.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên Khách Hàng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_ten.Focus();
                    return;
                }

                // Sửa: Kiểm tra ComboBox 'cbo_gt' thay vì TextBox
                if (cbo_gt.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn Giới Tính.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbo_gt.Focus();
                    return;
                }

                // 2. Thu thập dữ liệu
                string tenKH = txt_ten.Text;
                string sdt = txt_sdt.Text;
                string email = txt_email.Text; // Sửa lỗi: Đọc từ txt_email

                // Sửa: Lấy Giới tính từ ComboBox
                string gioiTinh = cbo_gt.SelectedItem.ToString();

                DateTime? ngaySinh = dateTimePicker1.Checked ? (DateTime?)dateTimePicker1.Value : null;

                // Lấy MaLoaiKH từ ComboBox
                int? maLoaiKH = null;
                if (cbo_lkh.SelectedValue != null && cbo_lkh.SelectedValue != DBNull.Value)
                {
                    maLoaiKH = Convert.ToInt32(cbo_lkh.SelectedValue);
                }

                // 3. Gọi hàm AddData
                bool success = AddData.ThemKhachHang(
                    tenKH, gioiTinh, ngaySinh, sdt, email, maLoaiKH, this.conn
                );

                // 4. Thông báo
                if (success)
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputFields(); // Xóa trắng các ô
                }
                else
                {
                    MessageBox.Show("Thêm thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi Oracle: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 6. HÀM NÚT XÓA
        private void btn_delete_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra SĐT
            if (string.IsNullOrEmpty(txt_sdt.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng (hoặc nhập SĐT) cần xóa.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_sdt.Focus();
                return;
            }

            string sdtCanXoa = txt_sdt.Text;

            // 2. Xác nhận
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa khách hàng có SĐT: " + sdtCanXoa + "?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // 3. Gọi hàm XoaKhachHang
                    bool success = AddData.XoaKhachHang(sdtCanXoa, this.conn);

                    if (success)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputFields(); // Xóa trắng các ô
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại. (Không tìm thấy SĐT hoặc lỗi CSDL).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (ApplicationException ex)
                {                    
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                }
                catch (OracleException ex)
                {
                    MessageBox.Show("Lỗi Oracle: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 7. HÀM NÚT SỬA
        private void btn_repair_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn KH chưa
            if (string.IsNullOrEmpty(originalSdtForUpdate))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa từ danh sách.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Lấy TẤT CẢ thông tin MỚI từ Form
                string tenKH = txt_ten.Text;
                string newSDT = txt_sdt.Text; // Đây là SĐT MỚI
                string newEmail = txt_email.Text; // Đây là Email MỚI
                if (cbo_gt.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn Giới Tính.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbo_gt.Focus();
                    return;
                }
                string gioiTinh = cbo_gt.SelectedItem.ToString();
                DateTime? ngaySinh = dateTimePicker1.Checked ? (DateTime?)dateTimePicker1.Value : null;
                int? maLoaiKH = null;
                if (cbo_lkh.SelectedValue != null && cbo_lkh.SelectedValue != DBNull.Value)
                {
                    maLoaiKH = Convert.ToInt32(cbo_lkh.SelectedValue);
                }

                // 3. Kiểm tra bắt buộc
                if (string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(gioiTinh) || string.IsNullOrEmpty(newSDT))
                {
                    MessageBox.Show("Tên KH, Giới tính và SĐT không được để trống.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Gọi hàm SuaKhachHang
                bool success = AddData.SuaKhachHang(
                    originalSdtForUpdate, // SĐT GỐC (để tìm)
                    tenKH,                // Dữ liệu mới
                    gioiTinh,             // Dữ liệu mới
                    ngaySinh,             // Dữ liệu mới
                    newSDT,               // Dữ liệu mới
                    newEmail,             // Dữ liệu mới
                    maLoaiKH,             // Dữ liệu mới
                    this.conn
                );

                if (success)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputFields(); // Xóa trắng
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. (Không tìm thấy SĐT gốc hoặc lỗi CSDL).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ApplicationException ex)
            {            
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi Oracle: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 8. HÀM DỌN DẸP FORM
        private void ClearInputFields()
        {
            txt_ten.Text = "";
            txt_sdt.Text = "";
            txt_email.Text = "";
            dateTimePicker1.Checked = false;

            // Reset ComboBox về "chưa chọn"
            cbo_gt.SelectedIndex = -1;
            cbo_lkh.SelectedValue = DBNull.Value; // Chọn dòng "Không chọn"

            originalSdtForUpdate = null;
        }

        // 9. HÀM LOGOUT
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