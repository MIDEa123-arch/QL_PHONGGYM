using ClosedXML.Excel;
using Oracle.ManagedDataAccess.Client;
using QL_PHONGGYM.BLL;
using QL_PHONGGYM.DAL;
using QL_PHONGGYM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_PHONGGYM
{
    public partial class XuatHoaDon : Form
    {

        private OracleConnection conn;
        private GetData dataGetter;

        public XuatHoaDon(OracleConnection connection)
        {
            InitializeComponent();
            conn = connection;
            dataGetter = new GetData();            
        }                    
        private void btnLoc_Click(object sender, EventArgs e)
        {
            int thang = dtpThangNam.Value.Month;
            int nam = dtpThangNam.Value.Year;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                DoanhThu result = dataGetter.GetHoaDonByThang(conn, thang, nam);

                dgvHoaDon.DataSource = result.HoaDonList;
                lblTongDoanhThu.Text = result.TongDoanhThu.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc hóa đơn: " + ex.Message);
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                return;
            }

            DataTable dt = (DataTable)dgvHoaDon.DataSource;

            try
            {
                string projectPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName);
                string hoaDonPath = Path.Combine(projectPath, "HoaDon");

                // Tên file
                string fileName = "HoaDon_" + dtpThangNam.Value.Month + "" + dtpThangNam.Value.Year;
                string tempExcelPath = Path.Combine(hoaDonPath, fileName + "_temp.xlsx");
                string encryptedFilePath = Path.Combine(hoaDonPath, fileName + ".enc");

                // 1. Xuất DataTable ra file Excel tạm
                using (var workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(dt, "DoanhThu");
                    workbook.Worksheets.First().Columns().AdjustToContents();
                    workbook.SaveAs(tempExcelPath);
                }

                string keyFile = Path.Combine(hoaDonPath, "key.txt");
                byte[] key = Convert.FromBase64String(File.ReadAllText(keyFile));


                DesCrypto.EncryptFile(tempExcelPath, encryptedFilePath, key);
                File.Delete(tempExcelPath);
                File.WriteAllText(keyFile, Convert.ToBase64String(key));

                MessageBox.Show("Xuất và mã hóa file thành công!", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất và mã hóa file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}