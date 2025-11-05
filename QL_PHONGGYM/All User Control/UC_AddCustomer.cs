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
using QL_PHONGGYM.DAL;
namespace QL_PHONGGYM.All_User_Control
{
    public partial class UC_AddCustomer : UserControl
    {
        private OracleConnection conn ;
        private GetData dataReader = new GetData();
        private AddData dataWriter = new AddData();
        public UC_AddCustomer(OracleConnection connection) // Constructor mới
        {
            InitializeComponent();
            this.conn = connection; // Gán đối tượng kết nối được truyền vào
        }
        public UC_AddCustomer()
        {
            InitializeComponent();
        }
        private void LoadKhachHangGrid()
        {
            
            DataTable dtKhachHang = dataReader.LoadKhachHangData(conn);
            if (dtKhachHang != null)
            {
                dataGridView1.DataSource = dtKhachHang; // Gán dữ liệu lên Grid bên trái
            }
            if (dataGridView1.Columns.Contains("MaLoaiKH"))
            {
                dataGridView1.Columns["MaLoaiKH"].Visible = false;

            }
            if (dataGridView1.Columns.Contains("MaKH"))
            {
                dataGridView1.Columns["MaKH"].Visible = false;
            }

            if (dataGridView1.Columns.Contains("TenKH"))
            {
                dataGridView1.Columns["TenKH"].HeaderText = "Tên Khách Hàng";
            }

            if (dataGridView1.Columns.Contains("NgaySinh"))
            {
                dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
             
                dataGridView1.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dataGridView1.Columns.Contains("SDT"))
            {
                dataGridView1.Columns["SDT"].HeaderText = "SĐT";
            }
            if (dataGridView1.Columns.Contains("Email"))
            {
                dataGridView1.Columns["Email"].HeaderText = "Email";
            }
            if (dataGridView1.Columns.Contains("GioiTinh"))
            {
                dataGridView1.Columns["GioiTinh"].HeaderText = "Giới Tính";
            }
            if (dataGridView1.Columns.Contains("TENLOAIKHACHHANG"))
            {
                dataGridView1.Columns["TENLOAIKHACHHANG"].HeaderText = "Loại Khách Hàng";
            }
        }
        // Hàm tải dữ liệu ban đầu (Load cho ComboBox và Grid)
        private void UC_AddCustomer_Load_1(object sender, EventArgs e)
        {
            txt_gioitinh.Items.Clear();
            txt_gioitinh.Items.Add("Nam");
            txt_gioitinh.Items.Add("Nữ");
            txt_gioitinh.SelectedIndex = 0;
            DataTable dtLoaiKH = dataReader.GetLoaiKhachHangList(conn);
            if (dtLoaiKH != null)
            {
                txt_loaikh.DataSource = dtLoaiKH;
                txt_loaikh.DisplayMember = "TENLOAI";
                txt_loaikh.ValueMember = "MALOAIKH";
            }

            // 2. Tải dữ liệu Khách Hàng lên DataGridView
            LoadKhachHangGrid();
        }


        private void btn_addkh_Click(object sender, EventArgs e)
        {
            DateTime ngaySinh = dtp_ngaysinh.Value;

            if (txt_loaikh.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Loại Khách Hàng.", "Lỗi nhập liệu");
                return;
            }

            int maLoaiKH;
            if (!int.TryParse(txt_loaikh.SelectedValue.ToString(), out maLoaiKH))
            {
                MessageBox.Show("Mã Loại Khách Hàng không hợp lệ (Không phải số).", "Lỗi hệ thống");
                return;
            }

            int? maKHMoi = dataWriter.InsertNewCustomer(
                 conn,
                 txt_tenkh.Text.Trim(),
                 txt_gioitinh.Text,
                 ngaySinh,
                 txt_sdt.Text.Trim(),
                 txt_email.Text.Trim(),
                 maLoaiKH
             );

            if (maKHMoi != null)
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                string tenKHMoi = txt_tenkh.Text.Trim();
                LoadKhachHangGrid();
                ClearInputFields();                

                TaoHoaDon hd = new TaoHoaDon(maKHMoi.Value, tenKHMoi, conn);
                hd.ShowDialog();
            }
        }

        private void ClearInputFields()
        {
            txt_tenkh.Clear();
            txt_sdt.Clear();
            txt_email.Clear();

               if (txt_gioitinh.Items.Count > 0)
            {
                txt_gioitinh.SelectedIndex = 0;
            }
            
            if (txt_loaikh.Items.Count > 0)
            {
                txt_loaikh.SelectedIndex = 0;
            }

          
            txt_tenkh.Focus();
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void txt_tenkh_TextChanged(object sender, EventArgs e)
        {

        }
    }
}