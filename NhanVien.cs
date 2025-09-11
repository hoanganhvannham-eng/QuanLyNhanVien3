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
    public partial class NhanVien: Form
    {
        public NhanVien()
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
        ////check
        private bool checknhanvien()
        {
            try
            {
                double a;

                // 1. Kiểm tra số điện thoại
                if (!double.TryParse(tbSoDienThoai.Text.Trim(), out a))
                {
                    MessageBox.Show("Số điện thoại phải là số!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (tbSoDienThoai.Text.Trim().Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải có đúng 10 chữ số!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                 //2. Kiểm tra mã nhân viên(không trùng)
                string checkMaNVSql = "SELECT COUNT(*) FROM tblNhanVien WHERE MaNV = @MaNV AND DeletedAt = 0";
                using (SqlCommand cmd = new SqlCommand(checkMaNVSql, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", tbmaNV.Text.Trim());
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Mã nhân viên này đã tồn tại trong hệ thống!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                // 3. Kiểm tra email
                string checkEmailSql = "SELECT COUNT(*) FROM tblNhanVien WHERE Email = @Email AND DeletedAt = 0";
                using (SqlCommand cmd = new SqlCommand(checkEmailSql, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());
                    int emailCount = (int)cmd.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        MessageBox.Show("Email này đã tồn tại trong hệ thống!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (!tbEmail.Text.Trim().ToLower().EndsWith("@gmail.com"))
                    {
                        MessageBox.Show("Email phải có đuôi @gmail.com!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                // 4. Kiểm tra mã phòng ban
                string checkMaPBSql = "SELECT COUNT(*) FROM tblPhongBan WHERE MaPB = @MaPB AND DeletedAt = 0";
                using (SqlCommand cmd = new SqlCommand(checkMaPBSql, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@MaPB", cbBoxMaPB.SelectedValue);
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Mã phòng ban không tồn tại trong hệ thống!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                // 5. Kiểm tra mã chức vụ
                string checkMaCVSql = "SELECT COUNT(*) FROM tblChucVu WHERE MaCV = @MaCV AND DeletedAt = 0";
                using (SqlCommand cmd = new SqlCommand(checkMaCVSql, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@MaCV", cbBoxChucVu.SelectedValue);
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Mã chức vụ không tồn tại trong hệ thống!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                return true; // Tất cả đều hợp lệ
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //check

        private void LoadDataNhanVien()
        {
            try
            {
                cn.connect();

                string sqlLoadDataNhanVien = @" SELECT MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaPB, MaCV, Ghichu
                                FROM tblNhanVien
                                WHERE DeletedAt = 0  
                                ORDER BY MaNV";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlLoadDataNhanVien, cn.conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtGridViewNhanVien.DataSource = dt;
                }
                cn.disconnect();
                ClearAllInputs(this);
                LoadcomboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadcomboBox()
        {
            try
            {
                cn.connect();
                string sqlLoadcomboBoxtblPhongBan = "SELECT * FROM tblPhongBan WHERE DeletedAt = 0";
                using (SqlDataAdapter da = new SqlDataAdapter(sqlLoadcomboBoxtblPhongBan, cn.conn))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    cbBoxMaPB.DataSource = ds.Tables[0];
                    cbBoxMaPB.DisplayMember = "MaPB";//Xác định cột nào của bảng dữ liệu sẽ được hiển thị lên ComboBox
                    cbBoxMaPB.ValueMember = "MaPB"; // cot gia tri
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load ma PB: " + ex.Message);
            }
            // load chuc vu combobox
            try
            {
                cn.connect();
                string sqsqlLoadcomboBoxttblChucVu = "SELECT * FROM tblChucVu WHERE DeletedAt = 0";
                using (SqlDataAdapter da = new SqlDataAdapter(sqsqlLoadcomboBoxttblChucVu, cn.conn))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    cbBoxChucVu.DataSource = ds.Tables[0];
                    cbBoxChucVu.DisplayMember = "TenCV";//Xác định cột nào của bảng dữ liệu sẽ được hiển thị lên ComboBox
                    cbBoxChucVu.ValueMember = "MaCV"; // cot gia tri
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load ma CV: " + ex.Message);
            }
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            LoadDataNhanVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                
                // Kiểm tra dữ liệu nhập vào    string.IsNullOrWhiteSpace(tbmaNV.Text) ||
                if (
                    string.IsNullOrWhiteSpace(tbmaNV.Text) ||
                    string.IsNullOrWhiteSpace(tbHoTen.Text) ||
                    cbBoxGioiTinh.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(tbDiaChi.Text) ||
                    string.IsNullOrWhiteSpace(tbSoDienThoai.Text) ||
                    string.IsNullOrWhiteSpace(tbEmail.Text) ||
                    cbBoxChucVu.SelectedIndex == -1 ||
                    cbBoxMaPB.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.connect();
                //double a;
                //// cehck sdt
                //if (!double.TryParse(tbSoDienThoai.Text.Trim(), out a))
                //{
                //    MessageBox.Show("Số điện thoại phải là số!", "Thông báo",
                //    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    cn.disconnect();
                //    return;
                //}
                //else if (tbSoDienThoai.Text.Trim().Length != 10)
                //{
                //    MessageBox.Show("Số điện thoại phải có đúng 10 chữ số!", "Thông báo",
                //    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    cn.disconnect();
                //    return;

                //}


                ////  check ma nv
                //string checkMaNVSql = "SELECT COUNT(*) FROM tblNhanVien WHERE MaNV = @MaNV AND DeletedAt = 0";
                //using (SqlCommand cmdcheckEmailSql = new SqlCommand(checkMaNVSql, cn.conn))
                //{
                //    cmdcheckEmailSql.Parameters.AddWithValue("@MaNV", tbmaNV.Text.Trim());
                //    int emailCount = (int)cmdcheckEmailSql.ExecuteScalar();

                //    if (emailCount > 0)
                //    {
                //        MessageBox.Show("Ma NV này đã tồn tại trong hệ thống!", "Thông báo",
                //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        cn.disconnect();
                //        return; // Dừng lại, không thêm nhân viên
                //    }
                //}

                ////  check mail
                //string checkEmailSql = "SELECT COUNT(*) FROM tblNhanVien WHERE Email = @Email AND DeletedAt = 0";
                //using (SqlCommand cmdcheckEmailSql = new SqlCommand(checkEmailSql, cn.conn))
                //{
                //    cmdcheckEmailSql.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());
                //    int emailCount = (int)cmdcheckEmailSql.ExecuteScalar();

                //    if (emailCount > 0)
                //    {
                //        MessageBox.Show("Email này đã tồn tại trong hệ thống!", "Thông báo",
                //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        cn.disconnect();
                //        return; // Dừng lại, không thêm nhân viên
                //    }
                //    else if (!tbEmail.Text.Trim().ToLower().EndsWith("@gmail.com"))
                //    {
                //        MessageBox.Show("Email phải có đuôi @gmail.com!", "Thông báo",
                //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        cn.disconnect();
                //        return;
                //    }
                //}
                //// check ma phong ban
                //string checkMaPBSql = "SELECT COUNT(*) FROM tblPhongBan  WHERE MaPB  = @MaPB  AND DeletedAt = 0";
                //using (SqlCommand cmdcheckMaPBSql = new SqlCommand(checkMaPBSql, cn.conn))
                //{
                //    cmdcheckMaPBSql.Parameters.AddWithValue("@MaPB", cbBoxMaPB.SelectedValue);
                //    int MaPBCount = (int)cmdcheckMaPBSql.ExecuteScalar();

                //    if (MaPBCount == 0)
                //    {
                //        MessageBox.Show("Mã phòng ban không tồn tại trong hệ thống!", "Thông báo",
                //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        cn.disconnect();
                //        return; // Dừng lại, không thêm nhân viên
                //    }

                //}
                //// check ma chuc vu
                //string checkMaCVSql = "SELECT COUNT(*) FROM tblChucVu  WHERE MaCV  = @MaCV  AND DeletedAt = 0";
                //using (SqlCommand cmdcheckMaCVSql = new SqlCommand(checkMaCVSql, cn.conn))
                //{
                //    cmdcheckMaCVSql.Parameters.AddWithValue("@MaCV", cbBoxChucVu.SelectedValue);
                //    int MaCVCount = (int)cmdcheckMaCVSql.ExecuteScalar();

                //    if (MaCVCount == 0)
                //    {
                //        MessageBox.Show("Mã chức vụ không tồn tại trong hệ thống!", "Thông báo",
                //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        cn.disconnect();
                //        return; // Dừng lại, không thêm nhân viên
                //    }
                //}

                if (!checknhanvien())
                {
                    cn.disconnect();
                    return; // Nếu dữ liệu không hợp lệ -> dừng luôn
                }

                // Câu lệnh SQL chèn dữ liệu vào bảng tblNhanVien
                string sqltblNhanVien = @"INSERT INTO tblNhanVien 
                           (MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaPB, MaCV, GhiChu, DeletedAt)
                           VALUES ( @MaNV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @Email, @MaPB, @MaCV, @GhiChu, 0)";

                using (SqlCommand cmd = new SqlCommand(sqltblNhanVien, cn.conn))
                {
                    // Gán giá trị từ các ô nhập liệu vào tham số SQL
                    cmd.Parameters.AddWithValue("@MaNV", tbmaNV.Text.Trim());
                    cmd.Parameters.AddWithValue("@HoTen", tbHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", dateTimePickerNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", cbBoxGioiTinh.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DiaChi", tbDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@SoDienThoai", tbSoDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaPB", cbBoxMaPB.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaCV", cbBoxChucVu.SelectedValue);
                    cmd.Parameters.AddWithValue("@GhiChu", tbGhiChu.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Thêm nhân viên thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAllInputs(this); // Xóa dữ liệu trên form
                        LoadDataNhanVien(); // Hàm load lại dữ liệu DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Thêm nhân viên thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtGridViewNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                tbmaNV.Text = dtGridViewNhanVien.Rows[i].Cells[0].Value.ToString();
                tbHoTen.Text = dtGridViewNhanVien.Rows[i].Cells[1].Value.ToString();
                dateTimePickerNgaySinh.Value = Convert.ToDateTime(dtGridViewNhanVien.Rows[i].Cells[2].Value);
                cbBoxGioiTinh.Text = dtGridViewNhanVien.Rows[i].Cells[3].Value.ToString();
                tbDiaChi.Text = dtGridViewNhanVien.Rows[i].Cells[4].Value.ToString();
                tbSoDienThoai.Text = dtGridViewNhanVien.Rows[i].Cells[5].Value.ToString();
                tbEmail.Text = dtGridViewNhanVien.Rows[i].Cells[6].Value.ToString();
                cbBoxMaPB.SelectedValue = dtGridViewNhanVien.Rows[i].Cells[7].Value.ToString();
                cbBoxChucVu.SelectedValue = dtGridViewNhanVien.Rows[i].Cells[8].Value.ToString();
                tbGhiChu.Text = dtGridViewNhanVien.Rows[i].Cells[9].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem đã chọn nhân viên nào chưa
                if (string.IsNullOrEmpty(tbmaNV.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập mã nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Xác nhận người dùng trước khi xóa
                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa nhân viên này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    cn.connect(); string query = "UPDATE tblNhanVien SET DeletedAt = 1 WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, cn.conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", tbmaNV.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 3. Load lại danh sách sau khi xóa
                            LoadDataNhanVien();

                            // 4. Xóa trắng các ô nhập liệu
                            ClearAllInputs(this);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    cn.disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                cn.connect();
                if (string.IsNullOrEmpty(tbmaNV.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoac nhap ma nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string checkMaNVSql = "SELECT COUNT(*) FROM tblNhanVien WHERE MaNV = @MaNV AND DeletedAt = 0";
                using (SqlCommand cmd = new SqlCommand(checkMaNVSql, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", tbmaNV.Text.Trim());
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Mã nhân viên này khong tồn tại trong hệ thống!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Kiểm tra dữ liệu nhập vào    string.IsNullOrWhiteSpace(tbmaNV.Text) ||
                if (
                    string.IsNullOrWhiteSpace(tbHoTen.Text) ||
                    cbBoxGioiTinh.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(tbDiaChi.Text) ||
                    string.IsNullOrWhiteSpace(tbSoDienThoai.Text) ||
                    string.IsNullOrWhiteSpace(tbEmail.Text) ||
                    cbBoxChucVu.SelectedIndex == -1 ||
                    cbBoxMaPB.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn sửa nhân viên này không?",
                    "Xác nhận sửa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    string sql = @"UPDATE tblNhanVien SET  HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, 
                             Email = @Email, MaPB = @MaPB, MaCV = @MaCV, GhiChu= @GhiChu, DeletedAt = 0 WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(sql, cn.conn))
                    {
                        // Gán giá trị từ các ô nhập liệu vào tham số SQL
                        cmd.Parameters.AddWithValue("@MaNV", tbmaNV.Text.Trim());
                        cmd.Parameters.AddWithValue("@HoTen", tbHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", dateTimePickerNgaySinh.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", cbBoxGioiTinh.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@DiaChi", tbDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoDienThoai", tbSoDienThoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaPB", cbBoxMaPB.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaCV", cbBoxChucVu.SelectedValue);
                        cmd.Parameters.AddWithValue("@GhiChu", tbGhiChu.Text.Trim());

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadDataNhanVien();
                            ClearAllInputs(this);
                        }
                        else
                        {
                            MessageBox.Show("Sửa nhân viên thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi" + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbHoTen.Text) && string.IsNullOrEmpty(tbmaNV.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên hoac ma nhân viên để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string sql = @"SELECT MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaPB, MaCV, GhiChu
                                FROM tblNhanVien
                                WHERE DeletedAt = 0
                                  AND(@TenTimKiem IS NULL OR HoTen LIKE '%' + @TenTimKiem + '%')
                                  OR(@MaNVTimKiem IS NULL OR MaNV LIKE '%' + @MaNVTimKiem + '%')
                                ORDER BY MaNV";
                
                using (SqlCommand cmd = new SqlCommand(sql, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@TenTimKiem", "%" + tbHoTen.Text + "%");
                    cmd.Parameters.AddWithValue("@MaNVTimKiem", "%" + tbmaNV.Text + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtGridViewNhanVien.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi " + ex.Message);
            }
        }

        private void btnrestar_Click(object sender, EventArgs e)
        {
            LoadDataNhanVien();
        }

        private void btnNVDaNghiViec_Click(object sender, EventArgs e)
        {
            try
            {
                cn.connect();
                string query = @"SELECT  MaNV ,HoTen, NgaySinh, GioiTinh, DiaChi,  SoDienThoai,  Email, MaPB, MaCV,  GhiChu
                                FROM tblNhanVien
                                WHERE DeletedAt != 0 ORDER BY MaNV";
                using (SqlDataAdapter da = new SqlDataAdapter(query, cn.conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dtGridViewNhanVien.DataSource = dt;
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnKhoiPhucNhanVien_Click(object sender, EventArgs e)
        {

            try
            {
                // 1. Kiểm tra xem đã chọn nhân viên nào chưa
                if (string.IsNullOrEmpty(tbmaNV.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập mã nhân viên cần khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Xác nhận người dùng trước khi xóa
                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn khôi phục nhân viên này không?",
                    "Xác nhận khôi phục",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    cn.connect();
                    string query = "UPDATE tblNhanVien SET DeletedAt = 0 WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, cn.conn))
                    {
                        // DELETE FROM tblNhanVien WHERE MaNV = @MaNV / UPDATE tblNhanVien SET DeletedAt = 1 WHERE MaNV = @MaNV
                        cmd.Parameters.AddWithValue("@MaNV", tbmaNV.Text);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("khôi phục nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearAllInputs(this);
                            LoadDataNhanVien();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    cn.disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi " + ex.Message);
            }
        }
    }
}
