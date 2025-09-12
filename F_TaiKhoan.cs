using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanVien3
{
    public partial class F_TaiKhoan: Form
    {
        public F_TaiKhoan()
        {
            InitializeComponent();
        }

        connectData cn = new connectData();
        private void ClearAllInputs(Control parent)
        {
            foreach (Control ctl in parent.Controls)
            {
                if (ctl is TextBox)
                    ((TextBox)ctl).Clear();
                else if (ctl is ComboBox)
                    ((ComboBox)ctl).SelectedIndex = -1;
                else if (ctl is DateTimePicker)
                    ((DateTimePicker)ctl).Value = DateTime.Now;
                else if (ctl.HasChildren)
                    ClearAllInputs(ctl);
            }
        }
        private void LoadDataTaiKhoan()
        {
            try
            {
                cn.connect();

                string sqlLoadDataNhanVien = @" SELECT MaTK, MaNV, TenDangNhap, MatKhau, Quyen, Ghichu
                                FROM tblTaiKhoan
                                WHERE DeletedAt = 0  
                                ORDER BY MaTK";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlLoadDataNhanVien, cn.conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewTaiKhoan.DataSource = dt;
                }
                cn.disconnect();
                ClearAllInputs(this);
                LoadcomboBox();
                tbMKkhoiphuc.UseSystemPasswordChar = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu tai khoan nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        
        private void LoadcomboBox()
        {
            try
            {
                cn.connect();
                string sqlLoadcomboBoxtblPhongBan = "SELECT * FROM tblNhanVien WHERE DeletedAt = 0";
                using (SqlDataAdapter da = new SqlDataAdapter(sqlLoadcomboBoxtblPhongBan, cn.conn))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    cbBoxMaNV.DataSource = ds.Tables[0];
                    cbBoxMaNV.DisplayMember = "HoTen";//Xác định cột nào của bảng dữ liệu sẽ được hiển thị lên ComboBox
                    cbBoxMaNV.ValueMember = "MaNV"; // cot gia tri
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load ma NV: " + ex.Message);
            }
        }

        private void F_TaiKhoan_Load(object sender, EventArgs e)
        {
            LoadDataTaiKhoan();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
    try
    {
        // 1. Kiểm tra đầu vào
        if (
            string.IsNullOrWhiteSpace(tbmaTK.Text) ||
            string.IsNullOrWhiteSpace(tbTenDangNhap.Text) ||
            string.IsNullOrWhiteSpace(tbMatKhau.Text) ||
            cbBoxMaNV.SelectedIndex == -1 ||
            cbBoxQuyen.SelectedIndex == -1)
        {
            MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        cn.connect();

        // 2. Kiểm tra MaTK đã tồn tại chưa
        string checkMaTKNVSql = "SELECT COUNT(*) FROM tblTaiKhoan WHERE MaTK = @MaTK AND DeletedAt = 0";
        using (SqlCommand cmdCheckMaTK = new SqlCommand(checkMaTKNVSql, cn.conn))
        {
            cmdCheckMaTK.Parameters.AddWithValue("@MaTK", tbmaTK.Text.Trim());
            int maTKCount = (int)cmdCheckMaTK.ExecuteScalar();

            if (maTKCount > 0)
            {
                MessageBox.Show("Mã tài khoản này đã tồn tại trong hệ thống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cn.disconnect();
                return;
            }
        }

        // 3. Kiểm tra MaNV đã có tài khoản chưa
        string checkNVSql = "SELECT COUNT(*) FROM tblTaiKhoan WHERE MaNV = @MaNV AND DeletedAt = 0";
        using (SqlCommand cmdCheckNV = new SqlCommand(checkNVSql, cn.conn))
        {
            cmdCheckNV.Parameters.AddWithValue("@MaNV", cbBoxMaNV.SelectedValue);
            int countNV = (int)cmdCheckNV.ExecuteScalar();

            if (countNV > 0)
            {
                MessageBox.Show("Nhân viên này đã có tài khoản trong hệ thống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cn.disconnect();
                return;
            }
        }

        // 4. Câu lệnh Insert
        string sqltblTaiKhoan = @"
            INSERT INTO tblTaiKhoan 
                (MaTK, MaNV, TenDangNhap, MatKhau, Quyen, Ghichu, DeletedAt)
            VALUES 
                (@MaTK, @MaNV, @TenDangNhap, @MatKhau, @Quyen, @GhiChu, 0)";

        using (SqlCommand cmd = new SqlCommand(sqltblTaiKhoan, cn.conn))
        {
            cmd.Parameters.AddWithValue("@MaTK", tbmaTK.Text.Trim());
            cmd.Parameters.AddWithValue("@MaNV", cbBoxMaNV.SelectedValue);  // cbBoxMaNV dùng ValueMember
            cmd.Parameters.AddWithValue("@TenDangNhap", tbTenDangNhap.Text.Trim());
            cmd.Parameters.AddWithValue("@MatKhau", tbMatKhau.Text.Trim());
            cmd.Parameters.AddWithValue("@Quyen", cbBoxQuyen.Text.Trim());  // cbBoxQuyen chỉ hiển thị text
            cmd.Parameters.AddWithValue("@GhiChu", tbGhiChu.Text.Trim());

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.disconnect();

                ClearAllInputs(this);  // Xóa dữ liệu nhập
                LoadDataTaiKhoan();    // Load lại DataGridView
            }
            else
            {
                cn.disconnect();
                MessageBox.Show("Thêm tài khoản thất bại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                tbmaTK.Text = dataGridViewTaiKhoan.Rows[i].Cells[0].Value.ToString();
                cbBoxMaNV.Text = dataGridViewTaiKhoan.Rows[i].Cells[1].Value.ToString();
                tbTenDangNhap.Text = dataGridViewTaiKhoan.Rows[i].Cells[2].Value.ToString(); ;
                tbMatKhau.Text = dataGridViewTaiKhoan.Rows[i].Cells[3].Value.ToString();
                //cbBoxQuyen.SelectedItem = dataGridViewTaiKhoan.Rows[i].Cells[4].Value.ToString();

                // --- Fix lỗi cbBoxQuyen ---
                string quyenValue = dataGridViewTaiKhoan.Rows[i].Cells[4].Value.ToString().Trim();
                if (!string.IsNullOrEmpty(quyenValue))
                {
                    foreach (var item in cbBoxQuyen.Items)
                    {
                        if (item.ToString().Equals(quyenValue, StringComparison.OrdinalIgnoreCase))
                        {
                            cbBoxQuyen.SelectedItem = item;
                            break;
                        }
                    }
                }
                // 

                tbGhiChu.Text = dataGridViewTaiKhoan.Rows[i].Cells[5].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void btnrestar_Click(object sender, EventArgs e)
        {

        }

        private void btnHienThiPhongBanCu_Click(object sender, EventArgs e)
        {

        }

        private void btnKhoiPhucPhongBan_Click(object sender, EventArgs e)
        {

        }

        private void checkshowpassword_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
