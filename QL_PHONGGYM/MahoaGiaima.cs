using Oracle.ManagedDataAccess.Client;
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
    public partial class MahoaGiaima : Form
    {
        private OracleConnection conn;
        public MahoaGiaima()
        {
            InitializeComponent();
        }
        public MahoaGiaima(OracleConnection connection)
        {
            InitializeComponent();
            conn = connection;
            CenterToScreen();

            cboMethod.Items.Add("CỘNG");
            cboMethod.Items.Add("NHÂN");
            cboMethod.SelectedIndex = 0;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string plaintext = txtPlain.Text;
                int k = int.Parse(txtKey.Text);
                string mode = cboMethod.SelectedItem.ToString();
                string result = "";

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = (mode == "CỘNG") ? "PROC_MAHOA_CONG" : "PROC_MAHOA_NHAN";

                    cmd.Parameters.Add("p_input", OracleDbType.Varchar2).Value = plaintext;
                    cmd.Parameters.Add("p_key", OracleDbType.Int32).Value = k;
                    cmd.Parameters.Add("p_output", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["p_output"].Value.ToString();
                }

                txtResult.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mã hóa: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string ciphertext = txtPlain.Text;
                int k = int.Parse(txtKey.Text);
                string mode = cboMethod.SelectedItem.ToString();
                string result = "";

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = (mode == "CỘNG") ? "PROC_GIAIMA_CONG" : "PROC_GIAIMA_NHAN";

                    cmd.Parameters.Add("p_input", OracleDbType.Varchar2).Value = ciphertext;
                    cmd.Parameters.Add("p_key", OracleDbType.Int32).Value = k;
                    cmd.Parameters.Add("p_output", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["p_output"].Value.ToString();
                }

                txtResult.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi giải mã: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
