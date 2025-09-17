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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnXuuatExcel = new System.Windows.Forms.Button();
            this.btnThongKeChamCong = new System.Windows.Forms.Button();
            this.btnThongKeLuong = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtGridViewThongKe = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewThongKe)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1282, 164);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(526, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Báo Cáo - Thống Kê";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnXuuatExcel);
            this.panel2.Controls.Add(this.btnThongKeChamCong);
            this.panel2.Controls.Add(this.btnThongKeLuong);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 164);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1282, 217);
            this.panel2.TabIndex = 1;
            // 
            // btnXuuatExcel
            // 
            this.btnXuuatExcel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnXuuatExcel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuuatExcel.Location = new System.Drawing.Point(921, 84);
            this.btnXuuatExcel.Name = "btnXuuatExcel";
            this.btnXuuatExcel.Size = new System.Drawing.Size(253, 48);
            this.btnXuuatExcel.TabIndex = 2;
            this.btnXuuatExcel.Text = "Xuất ra Excel";
            this.btnXuuatExcel.UseVisualStyleBackColor = true;
            // 
            // btnThongKeChamCong
            // 
            this.btnThongKeChamCong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnThongKeChamCong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeChamCong.Location = new System.Drawing.Point(504, 84);
            this.btnThongKeChamCong.Name = "btnThongKeChamCong";
            this.btnThongKeChamCong.Size = new System.Drawing.Size(253, 48);
            this.btnThongKeChamCong.TabIndex = 1;
            this.btnThongKeChamCong.Text = "Thống Kê Chấm Công";
            this.btnThongKeChamCong.UseVisualStyleBackColor = true;
            // 
            // btnThongKeLuong
            // 
            this.btnThongKeLuong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnThongKeLuong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeLuong.Location = new System.Drawing.Point(118, 84);
            this.btnThongKeLuong.Name = "btnThongKeLuong";
            this.btnThongKeLuong.Size = new System.Drawing.Size(253, 48);
            this.btnThongKeLuong.TabIndex = 0;
            this.btnThongKeLuong.Text = "Thống Kê Lương";
            this.btnThongKeLuong.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtGridViewThongKe);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 381);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1282, 384);
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
            this.dtGridViewThongKe.Size = new System.Drawing.Size(1282, 384);
            this.dtGridViewThongKe.TabIndex = 121;
            // 
            // F_ThongKeNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 765);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "F_ThongKeNhanVien";
            this.Text = "F_ThongKeNhanVien";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewThongKe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtGridViewThongKe;
        private System.Windows.Forms.Button btnXuuatExcel;
        private System.Windows.Forms.Button btnThongKeChamCong;
        private System.Windows.Forms.Button btnThongKeLuong;
    }
}