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
    public partial class TaoHoaDon : Form
    {
        private int? _maKHMoi = null;
        OracleConnection conn;
        private GetData dataReader = new GetData();
        AddData dataWriter = new AddData();
        decimal tongTien;
        public TaoHoaDon(int maKH, string tenKH, OracleConnection connection)
        {
            InitializeComponent();
            Ten_khach.Text = tenKH;
            conn = connection;
            _maKHMoi = maKH;
            this.cboLoaiSanPham.SelectedIndexChanged += new System.EventHandler(this.cboLoaiSanPham_SelectedIndexChanged);
            this.cboTenSanPham.SelectedIndexChanged += new System.EventHandler(this.cboTenSanPham_SelectedIndexChanged);
        }
        private void cboTenSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTenSanPham.SelectedItem is DataRowView row)
            {
                string selectedType = cboLoaiSanPham.SelectedItem?.ToString();

                if (selectedType == "Gói Tập")
                {
                    if (row["GIA"] != DBNull.Value)
                    {
                        decimal gia = Convert.ToDecimal(row["GIA"]);
                        tongTien = gia;
                        txtDonGia.Text = gia.ToString("N0");
                        lblTongTien.Text = gia.ToString("N0");
                        lblThanhTien.Text = lblTongTien.Text;
                    }
                }

                else if (selectedType == "Lớp học")
                {
                    decimal hocPhi = Convert.ToDecimal(row["HOCPHI"]);
                    int soBuoi = Convert.ToInt32(row["SOBUOI"]);

                    txtDonGia.Text = hocPhi.ToString("N0");

                    tongTien = hocPhi;
                    lblTongTien.Text = tongTien.ToString("N0");
                    lblThanhTien.Text = lblTongTien.Text;
                }
            }
        }


        private void cboLoaiSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cboLoaiSanPham.SelectedItem.ToString();

            if (selectedType == "Gói Tập")
            {
                LoadDanhSachGoiTap();
            }
            else if (selectedType == "Lớp học")
            {
                LoadDanhSachLopHoc();
            }
            else
            {
                cboTenSanPham.DataSource = null;
            }
        }

        private void LoadDanhSachGoiTap()
        {
            try
            {                
                DataTable dtGoiTap = dataReader.GetGoiTapList(conn);

                cboTenSanPham.DataSource = dtGoiTap;
                cboTenSanPham.DisplayMember = "TENGOI";
                cboTenSanPham.ValueMember = "MAGOITAP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Gói Tập: " + ex.Message);
            }
        }

        private void LoadDanhSachLopHoc()
        {
            try
            {
                DataTable dtLopHoc = dataReader.GetLopHocList(conn);

                cboTenSanPham.DataSource = dtLopHoc;
                cboTenSanPham.DisplayMember = "TenLop"; 
                cboTenSanPham.ValueMember = "MaLop"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Lớp Học: " + ex.Message);
            }
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                if (_maKHMoi == null)
                {
                    MessageBox.Show("Mã khách hàng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboTenSanPham.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }               
                dataWriter.TaoHoaDon(conn, _maKHMoi.Value, tongTien, 0);

                MessageBox.Show("Lập hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lập hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}