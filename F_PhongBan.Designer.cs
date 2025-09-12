namespace QuanLyNhanVien3
{
    partial class F_PhongBan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewPhongBan = new System.Windows.Forms.DataGridView();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.tbGhiChu = new System.Windows.Forms.TextBox();
            this.tbSoDienThoai = new System.Windows.Forms.TextBox();
            this.tbDiaChi = new System.Windows.Forms.TextBox();
            this.tbTenPB = new System.Windows.Forms.TextBox();
            this.tbmaPB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkshowpassword = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbMKkhoiphuc = new System.Windows.Forms.TextBox();
            this.btnxuatExcel = new System.Windows.Forms.Button();
            this.btnrestar = new System.Windows.Forms.Button();
            this.btnKhoiPhucPhongBan = new System.Windows.Forms.Button();
            this.btnHienThiPhongBanCu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPhongBan)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPhongBan
            // 
            this.dataGridViewPhongBan.AllowUserToAddRows = false;
            this.dataGridViewPhongBan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPhongBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPhongBan.Location = new System.Drawing.Point(40, 241);
            this.dataGridViewPhongBan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewPhongBan.Name = "dataGridViewPhongBan";
            this.dataGridViewPhongBan.ReadOnly = true;
            this.dataGridViewPhongBan.RowHeadersWidth = 51;
            this.dataGridViewPhongBan.RowTemplate.Height = 24;
            this.dataGridViewPhongBan.Size = new System.Drawing.Size(814, 288);
            this.dataGridViewPhongBan.TabIndex = 76;
            this.dataGridViewPhongBan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPhongBan_CellClick);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnTimKiem.Location = new System.Drawing.Point(491, 191);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTimKiem.Location = new System.Drawing.Point(716, 235);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(81, 25);
            this.btnTimKiem.TabIndex = 73;
            this.btnTimKiem.Text = "Tìm Kiếm ";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnSua.Location = new System.Drawing.Point(387, 191);
            this.btnSua.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.btnSua.Location = new System.Drawing.Point(577, 235);

            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(81, 25);
            this.btnSua.TabIndex = 72;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnXoa.Location = new System.Drawing.Point(197, 191);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.btnXoa.Location = new System.Drawing.Point(319, 235);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(81, 25);
            this.btnXoa.TabIndex = 71;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnThem.Location = new System.Drawing.Point(93, 191);
            this.btnThem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.btnThem.Location = new System.Drawing.Point(180, 235);

            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(81, 25);
            this.btnThem.TabIndex = 70;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // tbGhiChu
            // 
            this.tbGhiChu.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.tbGhiChu.Location = new System.Drawing.Point(386, 121);
            this.tbGhiChu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.tbGhiChu.Location = new System.Drawing.Point(576, 149);

            this.tbGhiChu.Name = "tbGhiChu";
            this.tbGhiChu.Size = new System.Drawing.Size(187, 23);
            this.tbGhiChu.TabIndex = 69;
            // 
            // tbSoDienThoai
            // 
            this.tbSoDienThoai.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.tbSoDienThoai.Location = new System.Drawing.Point(386, 89);
            this.tbSoDienThoai.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.tbSoDienThoai.Location = new System.Drawing.Point(576, 110);
            this.tbSoDienThoai.Name = "tbSoDienThoai";
            this.tbSoDienThoai.Size = new System.Drawing.Size(187, 23);
            this.tbSoDienThoai.TabIndex = 68;
            // 
            // tbDiaChi
            // 
            this.tbDiaChi.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.tbDiaChi.Location = new System.Drawing.Point(93, 153);
            this.tbDiaChi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.tbDiaChi.Location = new System.Drawing.Point(180, 188);
            this.tbDiaChi.Name = "tbDiaChi";
            this.tbDiaChi.Size = new System.Drawing.Size(187, 23);
            this.tbDiaChi.TabIndex = 67;
            // 
            // tbTenPB
            // 
            this.tbTenPB.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.tbTenPB.Location = new System.Drawing.Point(92, 119);
            this.tbTenPB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.tbTenPB.Location = new System.Drawing.Point(179, 146);
            this.tbTenPB.Name = "tbTenPB";
            this.tbTenPB.Size = new System.Drawing.Size(187, 23);
            this.tbTenPB.TabIndex = 66;
            // 
            // tbmaPB
            // 
            this.tbmaPB.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbmaPB.Location = new System.Drawing.Point(92, 89);
            this.tbmaPB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.tbmaPB.Location = new System.Drawing.Point(179, 110);
            this.tbmaPB.Name = "tbmaPB";
            this.tbmaPB.Size = new System.Drawing.Size(187, 23);
            this.tbmaPB.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.label9.Location = new System.Drawing.Point(289, 119);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);

            this.label9.Location = new System.Drawing.Point(446, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 16);
            this.label9.TabIndex = 64;
            this.label9.Text = "Ghi Chú";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(289, 89);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);

            this.label5.Location = new System.Drawing.Point(446, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "Số Điện Thoại";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.label6.Location = new System.Drawing.Point(38, 155);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);

            this.label6.Location = new System.Drawing.Point(40, 194);

            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 16);
            this.label6.TabIndex = 62;
            this.label6.Text = "Địa Chỉ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.label2.Location = new System.Drawing.Point(37, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);

            this.label2.Location = new System.Drawing.Point(39, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 19);
            this.label2.TabIndex = 61;
            this.label2.Text = "Tên Phòng Ban";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.label7.Location = new System.Drawing.Point(37, 92);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 16);
            this.label7.Location = new System.Drawing.Point(39, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 19);
            this.label7.TabIndex = 60;
            this.label7.Text = "Mã Phòng Ban";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(358, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 36);
            this.label1.Size = new System.Drawing.Size(196, 46);
            this.label1.TabIndex = 59;
            this.label1.Text = "Phòng Ban";
            // 
            // checkshowpassword
            // 
            this.checkshowpassword.AutoSize = true;
            this.checkshowpassword.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.checkshowpassword.Location = new System.Drawing.Point(693, 118);
            this.checkshowpassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkshowpassword.Location = new System.Drawing.Point(985, 145);
            this.checkshowpassword.Name = "checkshowpassword";
            this.checkshowpassword.Size = new System.Drawing.Size(125, 20);
            this.checkshowpassword.TabIndex = 101;
            this.checkshowpassword.Text = "Hiển thị mật khẩu";
            this.checkshowpassword.UseVisualStyleBackColor = true;
            this.checkshowpassword.CheckedChanged += new System.EventHandler(this.checkshowpassword_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(596, 93);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Location = new System.Drawing.Point(855, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 16);
            this.label8.TabIndex = 100;
            this.label8.Text = "MK Khôi Phục";
            // 
            // tbMKkhoiphuc
            // 
            this.tbMKkhoiphuc.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMKkhoiphuc.Location = new System.Drawing.Point(693, 91);
            this.tbMKkhoiphuc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.tbMKkhoiphuc.Location = new System.Drawing.Point(985, 112);
            this.tbMKkhoiphuc.Name = "tbMKkhoiphuc";
            this.tbMKkhoiphuc.Size = new System.Drawing.Size(122, 23);
            this.tbMKkhoiphuc.TabIndex = 99;
            // 
            // btnxuatExcel
            // 
            this.btnxuatExcel.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnxuatExcel.Location = new System.Drawing.Point(724, 191);
            this.btnxuatExcel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.btnxuatExcel.Location = new System.Drawing.Point(1027, 235);

            this.btnxuatExcel.Name = "btnxuatExcel";
            this.btnxuatExcel.Size = new System.Drawing.Size(81, 25);
            this.btnxuatExcel.TabIndex = 98;
            this.btnxuatExcel.Text = "XuatExcel";
            this.btnxuatExcel.UseVisualStyleBackColor = true;
            this.btnxuatExcel.Click += new System.EventHandler(this.btnxuatExcel_Click);
            // 
            // btnrestar
            // 
            this.btnrestar.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnrestar.Location = new System.Drawing.Point(639, 191);
            this.btnrestar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.btnrestar.Location = new System.Drawing.Point(913, 235);
            this.btnrestar.Name = "btnrestar";
            this.btnrestar.Size = new System.Drawing.Size(81, 25);
            this.btnrestar.TabIndex = 97;
            this.btnrestar.Text = "Refresh";
            this.btnrestar.UseVisualStyleBackColor = true;
            this.btnrestar.Click += new System.EventHandler(this.btnrestar_Click);
            // 
            // btnKhoiPhucPhongBan
            // 
            this.btnKhoiPhucPhongBan.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnKhoiPhucPhongBan.Location = new System.Drawing.Point(710, 150);
            this.btnKhoiPhucPhongBan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnKhoiPhucPhongBan.Location = new System.Drawing.Point(954, 184);

            this.btnKhoiPhucPhongBan.Name = "btnKhoiPhucPhongBan";
            this.btnKhoiPhucPhongBan.Size = new System.Drawing.Size(145, 27);
            this.btnKhoiPhucPhongBan.TabIndex = 96;
            this.btnKhoiPhucPhongBan.Text = "Khôi Phục Phòng Ban Cũ";
            this.btnKhoiPhucPhongBan.UseVisualStyleBackColor = true;
            this.btnKhoiPhucPhongBan.Click += new System.EventHandler(this.btnKhoiPhucPhongBan_Click);
            // 
            // btnHienThiPhongBanCu
            // 
            this.btnHienThiPhongBanCu.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnHienThiPhongBanCu.Location = new System.Drawing.Point(573, 150);
            this.btnHienThiPhongBanCu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);

            this.btnHienThiPhongBanCu.Location = new System.Drawing.Point(716, 184);
            this.btnHienThiPhongBanCu.Name = "btnHienThiPhongBanCu";
            this.btnHienThiPhongBanCu.Size = new System.Drawing.Size(133, 27);
            this.btnHienThiPhongBanCu.TabIndex = 95;
            this.btnHienThiPhongBanCu.Text = "Hiển Thị Phòng Ban Cũ";
            this.btnHienThiPhongBanCu.UseVisualStyleBackColor = true;
            this.btnHienThiPhongBanCu.Click += new System.EventHandler(this.btnHienThiPhongBanCu_Click);
            // 
            // F_PhongBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 559);
            this.Controls.Add(this.checkshowpassword);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbMKkhoiphuc);
            this.Controls.Add(this.btnxuatExcel);
            this.Controls.Add(this.btnrestar);
            this.Controls.Add(this.btnKhoiPhucPhongBan);
            this.Controls.Add(this.btnHienThiPhongBanCu);
            this.Controls.Add(this.dataGridViewPhongBan);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.tbGhiChu);
            this.Controls.Add(this.tbSoDienThoai);
            this.Controls.Add(this.tbDiaChi);
            this.Controls.Add(this.tbTenPB);
            this.Controls.Add(this.tbmaPB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "F_PhongBan";
            this.Text = "PhongBan";
            this.Load += new System.EventHandler(this.PhongBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPhongBan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewPhongBan;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox tbGhiChu;
        private System.Windows.Forms.TextBox tbSoDienThoai;
        private System.Windows.Forms.TextBox tbDiaChi;
        private System.Windows.Forms.TextBox tbTenPB;
        private System.Windows.Forms.TextBox tbmaPB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkshowpassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbMKkhoiphuc;
        private System.Windows.Forms.Button btnxuatExcel;
        private System.Windows.Forms.Button btnrestar;
        private System.Windows.Forms.Button btnKhoiPhucPhongBan;
        private System.Windows.Forms.Button btnHienThiPhongBanCu;
    }
}