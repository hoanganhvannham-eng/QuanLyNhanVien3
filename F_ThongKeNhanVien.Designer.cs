namespace QuanLyNhanVien3
{
    partial class F_ThongKeNhanVien
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtGridViewThongKe = new System.Windows.Forms.DataGridView();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbChamCong = new System.Windows.Forms.RadioButton();
            this.rdbLuong = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbTheoNgay = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.rdbTheoThang = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.numNam = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewThongKe)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1442, 130);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(527, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Báo Cáo - Thống Kê";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtGridViewThongKe);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 583);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1442, 373);
            this.panel3.TabIndex = 2;
            // 
            // dtGridViewThongKe
            // 
            this.dtGridViewThongKe.AllowUserToAddRows = false;
            this.dtGridViewThongKe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridViewThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridViewThongKe.Location = new System.Drawing.Point(0, 0);
            this.dtGridViewThongKe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtGridViewThongKe.Name = "dtGridViewThongKe";
            this.dtGridViewThongKe.ReadOnly = true;
            this.dtGridViewThongKe.RowHeadersWidth = 51;
            this.dtGridViewThongKe.RowTemplate.Height = 24;
            this.dtGridViewThongKe.Size = new System.Drawing.Size(1442, 373);
            this.dtGridViewThongKe.TabIndex = 121;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnThongKe.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.Location = new System.Drawing.Point(35, 373);
            this.btnThongKe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(285, 60);
            this.btnThongKe.TabIndex = 0;
            this.btnThongKe.Text = "Thống Kê ";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(770, 373);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(285, 60);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnXuatExcel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatExcel.Location = new System.Drawing.Point(414, 373);
            this.btnXuatExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(285, 60);
            this.btnXuatExcel.TabIndex = 2;
            this.btnXuatExcel.Text = "Xuất ra Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnXuatExcel);
            this.panel2.Controls.Add(this.btnThoat);
            this.panel2.Controls.Add(this.btnThongKe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 130);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1442, 453);
            this.panel2.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numNam);
            this.groupBox3.Controls.Add(this.numThang);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.rdbTheoThang);
            this.groupBox3.Controls.Add(this.dtpToDate);
            this.groupBox3.Controls.Add(this.dtpFromDate);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.rdbTheoNgay);
            this.groupBox3.Location = new System.Drawing.Point(583, 84);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(669, 282);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bộ lọc thời gian";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbChamCong);
            this.groupBox2.Controls.Add(this.rdbLuong);
            this.groupBox2.Location = new System.Drawing.Point(3, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(534, 221);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Loại thống kê";
            // 
            // rdbChamCong
            // 
            this.rdbChamCong.AutoSize = true;
            this.rdbChamCong.Location = new System.Drawing.Point(40, 58);
            this.rdbChamCong.Name = "rdbChamCong";
            this.rdbChamCong.Size = new System.Drawing.Size(182, 24);
            this.rdbChamCong.TabIndex = 1;
            this.rdbChamCong.TabStop = true;
            this.rdbChamCong.Text = "Thống kê chấm công";
            this.rdbChamCong.UseVisualStyleBackColor = true;
            // 
            // rdbLuong
            // 
            this.rdbLuong.AutoSize = true;
            this.rdbLuong.Location = new System.Drawing.Point(40, 28);
            this.rdbLuong.Name = "rdbLuong";
            this.rdbLuong.Size = new System.Drawing.Size(143, 24);
            this.rdbLuong.TabIndex = 0;
            this.rdbLuong.TabStop = true;
            this.rdbLuong.Text = "Thống kê lương";
            this.rdbLuong.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaNV);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1439, 66);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin nhân viên";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(144, 31);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(251, 26);
            this.txtMaNV.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã nhân viên ";
            // 
            // rdbTheoNgay
            // 
            this.rdbTheoNgay.AutoSize = true;
            this.rdbTheoNgay.Location = new System.Drawing.Point(46, 25);
            this.rdbTheoNgay.Name = "rdbTheoNgay";
            this.rdbTheoNgay.Size = new System.Drawing.Size(110, 24);
            this.rdbTheoNgay.TabIndex = 2;
            this.rdbTheoNgay.TabStop = true;
            this.rdbTheoNgay.Text = "Theo Ngày";
            this.rdbTheoNgay.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Từ ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tới ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(187, 50);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(200, 26);
            this.dtpFromDate.TabIndex = 5;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(187, 86);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(200, 26);
            this.dtpToDate.TabIndex = 6;
            // 
            // rdbTheoThang
            // 
            this.rdbTheoThang.AutoSize = true;
            this.rdbTheoThang.Location = new System.Drawing.Point(46, 114);
            this.rdbTheoThang.Name = "rdbTheoThang";
            this.rdbTheoThang.Size = new System.Drawing.Size(119, 24);
            this.rdbTheoThang.TabIndex = 7;
            this.rdbTheoThang.TabStop = true;
            this.rdbTheoThang.Text = "Theo Tháng";
            this.rdbTheoThang.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tháng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(103, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Năm";
            // 
            // numThang
            // 
            this.numThang.Location = new System.Drawing.Point(187, 145);
            this.numThang.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numThang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThang.Name = "numThang";
            this.numThang.Size = new System.Drawing.Size(200, 26);
            this.numThang.TabIndex = 10;
            this.numThang.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // numNam
            // 
            this.numNam.Location = new System.Drawing.Point(187, 182);
            this.numNam.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numNam.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numNam.Name = "numNam";
            this.numNam.Size = new System.Drawing.Size(200, 26);
            this.numNam.TabIndex = 11;
            this.numNam.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // F_ThongKeNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 956);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "F_ThongKeNhanVien";
            this.Text = "F_ThongKeNhanVien";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.F_ThongKeNhanVien_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewThongKe)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtGridViewThongKe;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbChamCong;
        private System.Windows.Forms.RadioButton rdbLuong;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdbTheoNgay;
        private System.Windows.Forms.NumericUpDown numNam;
        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdbTheoThang;
    }
}