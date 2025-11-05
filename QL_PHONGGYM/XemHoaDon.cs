using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using QL_PHONGGYM.BLL;

namespace QL_PHONGGYM
{
    public partial class XemHoaDon : Form
    {
        public XemHoaDon()
        {
            InitializeComponent();
            LoadEncryptedFiles();
        }

        private void LoadEncryptedFiles()
        {
            string projectPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName);
            string hoaDonPath = Path.Combine(projectPath, "HoaDon");
            try
            {
                cboFileHoaDon.Items.Clear();

                if (!Directory.Exists(hoaDonPath))
                    return;

                // Lấy tất cả file .enc
                var encryptedFiles = Directory.GetFiles(hoaDonPath, "*.enc")
                    .Select(Path.GetFileName)
                    .ToArray();

                if (encryptedFiles.Length > 0)
                {
                    cboFileHoaDon.Items.AddRange(encryptedFiles);
                    cboFileHoaDon.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy file hóa đơn đã mã hóa.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemHoaDon_Click(object sender, EventArgs e)
        {
            string projectPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName);
            string hoaDonPath = Path.Combine(projectPath, "HoaDon");
            string keyFilePath = Path.Combine(hoaDonPath, "key.txt");

            if (cboFileHoaDon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn file hóa đơn!", "Thông báo");
                return;
            }

            string selectedFile = cboFileHoaDon.SelectedItem.ToString();
            string encryptedFilePath = Path.Combine(hoaDonPath, selectedFile);
            string decryptedFilePath = Path.Combine(hoaDonPath, "temp_decrypted.xlsx");

            try
            {
                // Kiểm tra file key
                if (!File.Exists(keyFilePath))
                {
                    MessageBox.Show("Không tìm thấy file key.txt!\nVui lòng kiểm tra lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] key = Convert.FromBase64String(File.ReadAllText(keyFilePath).Trim());
            
                DesCrypto.DecryptFile(encryptedFilePath, decryptedFilePath, key);

                // Đọc dữ liệu từ Excel và hiển thị lên DataGridView
                using (var workbook = new XLWorkbook(decryptedFilePath))
                {
                    var worksheet = workbook.Worksheets.First();
                    DataTable dt = new DataTable();

                    // Đọc header
                    var firstRow = worksheet.FirstRowUsed();
                    foreach (var cell in firstRow.CellsUsed())
                    {
                        dt.Columns.Add(cell.Value.ToString());
                    }

                    // Đọc dữ liệu
                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        DataRow dataRow = dt.NewRow();
                        int i = 0;
                        foreach (var cell in row.CellsUsed())
                        {
                            if (i < dt.Columns.Count)
                                dataRow[i] = cell.Value.ToString();
                            i++;
                        }
                        dt.Rows.Add(dataRow);
                    }

                    dgvHoaDon.DataSource = dt;
                }

                // Tự động mở Excel
                if (chkMoExcel.Checked)
                {
                    System.Diagnostics.Process.Start(decryptedFilePath);
                }               
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                MessageBox.Show("Giải mã thất bại! Key không đúng hoặc file bị hỏng.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadEncryptedFiles();
            dgvHoaDon.DataSource = null;
            MessageBox.Show("Đã làm mới danh sách!", "Thông báo");
        }

        private void btnXoaFileTam_Click(object sender, EventArgs e)
        {
            string projectPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName);
            string hoaDonPath = Path.Combine(projectPath, "HoaDon");
            try
            {
                string tempFile = Path.Combine(hoaDonPath, "temp_decrypted.xlsx");
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                    MessageBox.Show("Đã xóa file tạm thành công!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Không có file tạm để xóa.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa file tạm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            string projectPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName);
            string hoaDonPath = Path.Combine(projectPath, "HoaDon");
            // Tự động xóa file tạm khi đóng form
            try
            {
                string tempFile = Path.Combine(hoaDonPath, "temp_decrypted.xlsx");
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
            catch { }
        }
    }
}