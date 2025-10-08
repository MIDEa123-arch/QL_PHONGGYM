namespace QL_PHONGGYM
{
    partial class Home
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.Button ViewListBtn;
        private System.Windows.Forms.DataGridView LopHocTable;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.ViewListBtn = new System.Windows.Forms.Button();
            this.LopHocTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.LopHocTable)).BeginInit();
            this.SuspendLayout();
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LogoutBtn.BackColor = System.Drawing.Color.OrangeRed;
            this.LogoutBtn.ForeColor = System.Drawing.Color.White;
            this.LogoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogoutBtn.Location = new System.Drawing.Point(850, 20);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(140, 35);
            this.LogoutBtn.TabIndex = 0;
            this.LogoutBtn.Text = "Đăng xuất";
            this.LogoutBtn.UseVisualStyleBackColor = false;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // ViewListBtn
            // 
            this.ViewListBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ViewListBtn.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ViewListBtn.ForeColor = System.Drawing.Color.White;
            this.ViewListBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewListBtn.Location = new System.Drawing.Point(700, 20);
            this.ViewListBtn.Name = "ViewListBtn";
            this.ViewListBtn.Size = new System.Drawing.Size(140, 35);
            this.ViewListBtn.TabIndex = 1;
            this.ViewListBtn.Text = "Xem danh sách";
            this.ViewListBtn.UseVisualStyleBackColor = false;
            this.ViewListBtn.Click += new System.EventHandler(this.ViewListBtn_Click);
            // 
            // LopHocTable
            // 
            this.LopHocTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LopHocTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LopHocTable.Location = new System.Drawing.Point(0, 70);
            this.LopHocTable.Name = "LopHocTable";
            this.LopHocTable.RowHeadersWidth = 51;
            this.LopHocTable.RowTemplate.Height = 24;
            this.LopHocTable.Size = new System.Drawing.Size(1010, 450);
            this.LopHocTable.TabIndex = 2;
            this.LopHocTable.BackgroundColor = System.Drawing.Color.White;
            this.LopHocTable.GridColor = System.Drawing.Color.LightGray;
            this.LopHocTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LopHocTable.ReadOnly = true;
            this.LopHocTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.LopHocTable.Visible = false; // Ẩn ban đầu
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 520);
            this.Controls.Add(this.ViewListBtn);
            this.Controls.Add(this.LogoutBtn);
            this.Controls.Add(this.LopHocTable);
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LopHocTable)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
