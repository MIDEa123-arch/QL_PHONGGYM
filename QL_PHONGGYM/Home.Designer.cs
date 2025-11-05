namespace QL_PHONGGYM
{
    partial class Home
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button LogoutBtn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_employee = new Guna.UI2.WinForms.Guna2Button();
            this.btn_add = new Guna.UI2.WinForms.Guna2Button();
            this.panelmoving = new System.Windows.Forms.Panel();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.print_hoaDon = new Guna.UI2.WinForms.Guna2Button();
            this.uC_Employee1 = new QL_PHONGGYM.All_User_Control.UC_Employee();
            this.uC_AddCustomer1 = new QL_PHONGGYM.All_User_Control.UC_AddCustomer();
            this.btn_xemHd = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.BackColor = System.Drawing.Color.Crimson;
            this.LogoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogoutBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LogoutBtn.ForeColor = System.Drawing.Color.White;
            this.LogoutBtn.Location = new System.Drawing.Point(1769, 986);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(130, 31);
            this.LogoutBtn.TabIndex = 0;
            this.LogoutBtn.Text = "Đăng xuất";
            this.LogoutBtn.UseVisualStyleBackColor = false;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btn_xemHd);
            this.panel1.Controls.Add(this.print_hoaDon);
            this.panel1.Controls.Add(this.btn_employee);
            this.panel1.Controls.Add(this.btn_add);
            this.panel1.Location = new System.Drawing.Point(263, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1636, 130);
            this.panel1.TabIndex = 1;
            // 
            // btn_employee
            // 
            this.btn_employee.BorderRadius = 15;
            this.btn_employee.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btn_employee.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_employee.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_employee.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_employee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_employee.FillColor = System.Drawing.Color.SlateBlue;
            this.btn_employee.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_employee.ForeColor = System.Drawing.Color.White;
            this.btn_employee.Location = new System.Drawing.Point(385, 16);
            this.btn_employee.Name = "btn_employee";
            this.btn_employee.Size = new System.Drawing.Size(215, 95);
            this.btn_employee.TabIndex = 2;
            this.btn_employee.Text = "Nhân Viên";
            this.btn_employee.Click += new System.EventHandler(this.btn_employee_Click);
            // 
            // btn_add
            // 
            this.btn_add.BorderRadius = 15;
            this.btn_add.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btn_add.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_add.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_add.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_add.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_add.FillColor = System.Drawing.Color.SlateBlue;
            this.btn_add.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.ForeColor = System.Drawing.Color.White;
            this.btn_add.Location = new System.Drawing.Point(63, 16);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(236, 95);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = "Thêm Khách Hàng";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // panelmoving
            // 
            this.panelmoving.BackColor = System.Drawing.Color.SpringGreen;
            this.panelmoving.Location = new System.Drawing.Point(299, 159);
            this.panelmoving.Name = "panelmoving";
            this.panelmoving.Size = new System.Drawing.Size(288, 7);
            this.panelmoving.TabIndex = 0;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(27, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "FITNESS CENTER";
            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMain.Controls.Add(this.uC_Employee1);
            this.panelMain.Controls.Add(this.uC_AddCustomer1);
            this.panelMain.Location = new System.Drawing.Point(33, 180);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1866, 789);
            this.panelMain.TabIndex = 5;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.TargetControl = this;
            // 
            // print_hoaDon
            // 
            this.print_hoaDon.BorderRadius = 15;
            this.print_hoaDon.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.print_hoaDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.print_hoaDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.print_hoaDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.print_hoaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.print_hoaDon.FillColor = System.Drawing.Color.SlateBlue;
            this.print_hoaDon.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.print_hoaDon.ForeColor = System.Drawing.Color.White;
            this.print_hoaDon.Location = new System.Drawing.Point(668, 16);
            this.print_hoaDon.Name = "print_hoaDon";
            this.print_hoaDon.Size = new System.Drawing.Size(215, 95);
            this.print_hoaDon.TabIndex = 3;
            this.print_hoaDon.Text = "Xuất danh sách hóa đơn";
            this.print_hoaDon.Click += new System.EventHandler(this.print_hoaDon_Click);
            // 
            // uC_Employee1
            // 
            this.uC_Employee1.BackColor = System.Drawing.Color.White;
            this.uC_Employee1.Location = new System.Drawing.Point(3, 3);
            this.uC_Employee1.Name = "uC_Employee1";
            this.uC_Employee1.Size = new System.Drawing.Size(1917, 857);
            this.uC_Employee1.TabIndex = 1;
            // 
            // uC_AddCustomer1
            // 
            this.uC_AddCustomer1.BackColor = System.Drawing.Color.Lavender;
            this.uC_AddCustomer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_AddCustomer1.Location = new System.Drawing.Point(4, 3);
            this.uC_AddCustomer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.uC_AddCustomer1.Name = "uC_AddCustomer1";
            this.uC_AddCustomer1.Size = new System.Drawing.Size(1443, 711);
            this.uC_AddCustomer1.TabIndex = 0;
            // 
            // btn_xemHd
            // 
            this.btn_xemHd.BorderRadius = 15;
            this.btn_xemHd.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btn_xemHd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_xemHd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_xemHd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_xemHd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_xemHd.FillColor = System.Drawing.Color.SlateBlue;
            this.btn_xemHd.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xemHd.ForeColor = System.Drawing.Color.White;
            this.btn_xemHd.Location = new System.Drawing.Point(934, 16);
            this.btn_xemHd.Name = "btn_xemHd";
            this.btn_xemHd.Size = new System.Drawing.Size(215, 95);
            this.btn_xemHd.TabIndex = 4;
            this.btn_xemHd.Text = "Xem danh sách hóa đơn";
            this.btn_xemHd.Click += new System.EventHandler(this.btn_xemHd_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelmoving);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LogoutBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button btn_add;
        private System.Windows.Forms.Panel panelmoving;
        private Guna.UI2.WinForms.Guna2Button btn_employee;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelMain;
        private All_User_Control.UC_AddCustomer uC_AddCustomer1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private All_User_Control.UC_Employee uC_Employee1;
        private Guna.UI2.WinForms.Guna2Button print_hoaDon;
        private Guna.UI2.WinForms.Guna2Button btn_xemHd;
    }
}
