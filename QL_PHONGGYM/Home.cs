﻿using Oracle.ManagedDataAccess.Client;
using QL_PHONGGYM.DAL;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using QL_PHONGGYM.All_User_Control;
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
            CenterToScreen();
            currentUser = userName;
            conn = connection;
        }

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

        private void btn_add_Click(object sender, EventArgs e)
        {
            panelmoving.Left = btn_add.Left = 50;
            panelMain.Controls.Clear(); // Xóa nội dung panel trước đó

            UC_AddCustomer uc = new UC_AddCustomer();
            uc.Dock = DockStyle.Fill; // Cho full panel
            panelMain.Controls.Add(uc);
            uc.BringToFront();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            uC_AddCustomer1.Visible = false;
            btn_add.PerformClick();
        }
    }
}