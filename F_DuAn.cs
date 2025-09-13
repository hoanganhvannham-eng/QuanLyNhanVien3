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
    public partial class F_DuAn: Form
    {
        public F_DuAn()
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

        private void LoadDataDuAn()
        {
            try
            {
                cn.connect();

                string sqlLoadDataNhanVien = @" SELECT MaDA, TenDA, MoTa, NgayBatDau, NgayKetThuc, Ghichu FROM tblDuAn WHERE DeletedAt = 0 ORDER BY MaDA";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlLoadDataNhanVien, cn.conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtGridViewDA.DataSource = dt;
                }
                cn.disconnect();
                ClearAllInputs(this);
                tbMKKhoiPhuc.UseSystemPasswordChar = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Dự Án: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                cn.connect();
                if (
                    string.IsNullOrWhiteSpace(tbmaDA.Text) ||
                    string.IsNullOrWhiteSpace(tbTenDA.Text) ||
                    string.IsNullOrWhiteSpace(tbMota.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //kiem tra dieu kien hop li cua ngay bat dau voi ngay ket thuc
                string checkDateSql = "SELECT COUNT(*) FROM tblDuAn WHERE NgayBatDau > NgayKetThuc AND DeletedAt = 0";
                using (SqlCommand cmdcheckDate = new SqlCommand(checkDateSql, cn.conn))
                {
                    int invalidDateCount = (int)cmdcheckDate.ExecuteScalar();
                    if (invalidDateCount > 0)
                    {
                        MessageBox.Show("Tồn tại dự án có ngày bắt đầu lớn hơn ngày kết thúc!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cn.disconnect();
                        return;
                    }
                }


                // check ma du an
                string checkMaDASql = "SELECT COUNT(*) FROM tblDuAn  WHERE MaDA  = @MaDA  AND DeletedAt = 0";
                using (SqlCommand cmdcheckMaDASql = new SqlCommand(checkMaDASql, cn.conn))
                {
                    cmdcheckMaDASql.Parameters.AddWithValue("@MaPB", tbmaDA.Text);
                    int MaPBCount = (int)cmdcheckMaDASql.ExecuteScalar();

                    if (MaPBCount != 0)
                    {
                        MessageBox.Show("Mã dự án đã tồn tại trong hệ thống!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cn.disconnect();
                        return;
                    }
                }

                string checkTenDA = "SELECT COUNT(*) FROM tblDuAn  WHERE TenDA  = @TenDA";
                using (SqlCommand cmd = new SqlCommand(checkTenDA, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@TenDA", tbTenDA.Text.Trim());
                    int MaDACount = (int)cmd.ExecuteScalar();

                    if (MaDACount > 0)
                    {
                        MessageBox.Show("dự án này đã tồn tại trong hệ thống!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cn.disconnect();
                        return;
                    }
                }

                string sqltblDuAn = @"INSERT INTO tblDuAn 
                           (MaDA, TenDA,  MoTa, NgayBatDau, NgayKetThuc, Ghichu, DeletedAt)
                           VALUES ( @MaDA, @TenDA,  @MoTa, @NgayBatDau, @NgayKetThuc, @GhiChu, 0)";

                using (SqlCommand cmd = new SqlCommand(sqltblDuAn, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@MaDA", tbmaDA.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenDA", tbTenDA.Text.Trim());
                    cmd.Parameters.AddWithValue("@MoTa", tbMota.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgayBatDau", DatePickerNgayBatDau.Value);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", DatePickerNgayKetThuc.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", tbGhiChu.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Thêm dự án thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cn.disconnect();
                        ClearAllInputs(this);
                        LoadDataDuAn();
                    }
                    else
                    {
                        cn.disconnect();
                        MessageBox.Show("Thêm dự án thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                //MessageBox.Show("Chi tiết lỗi: " + ex.ToString(), "Lỗi hệ thống",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbmaDA.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập mã dự án cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa dự án này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    cn.connect();
                    string query = "UPDATE tblDuAn SET DeletedAt = 1 WHERE MaDA = @MaDA";
                    using (SqlCommand cmd = new SqlCommand(query, cn.conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDA", tbmaDA.Text);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa dự án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cn.disconnect();
                            LoadDataDuAn();
                            ClearAllInputs(this);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dự án để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cn.disconnect();
                        }
                    }
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
                if (string.IsNullOrEmpty(tbmaDA.Text))
                {
                    MessageBox.Show("Vui lòng chọn dự án cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (
                    string.IsNullOrWhiteSpace(tbTenDA.Text) ||
                    string.IsNullOrWhiteSpace(tbMota.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //kiem tra dieu kien hop li cua ngay bat dau voi ngay ket thuc
                string checkDateSql = "SELECT COUNT(*) FROM tblDuAn WHERE NgayBatDau > NgayKetThuc AND DeletedAt = 0";
                using (SqlCommand cmdcheckDate = new SqlCommand(checkDateSql, cn.conn))
                {
                    int invalidDateCount = (int)cmdcheckDate.ExecuteScalar();
                    if (invalidDateCount > 0)
                    {
                        MessageBox.Show("Tồn tại dự án có ngày bắt đầu lớn hơn ngày kết thúc!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cn.disconnect();
                        return;
                    }

                    DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn sửa dự án này không?",
                    "Xác nhận sửa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                    if (confirm == DialogResult.Yes)
                    {
                        cn.connect();
                        string sql = @"UPDATE tblDuAn SET TenDA = @TenDA, MoTa = @MoTa, NgayBatDau = @NgayBatDau, NgayKetThuc = @NgayKetThuc, GhiChu = @GhiChu, DeletedAt = 0 WHERE MaDA = @MaDA";
                        using (SqlCommand cmd = new SqlCommand(sql, cn.conn))
                        {
                            cmd.Parameters.AddWithValue("@MaPB", tbmaDA.Text.Trim());
                            cmd.Parameters.AddWithValue("@TenPB", tbTenDA.Text.Trim());
                            cmd.Parameters.AddWithValue("@MoTa", tbMota.Text.Trim());
                            cmd.Parameters.AddWithValue("@NgayBatDau", DatePickerNgayBatDau.Value);
                            cmd.Parameters.AddWithValue("@NgayKetThuc", DatePickerNgayKetThuc.Value);
                            cmd.Parameters.AddWithValue("@GhiChu", tbGhiChu.Text.Trim());

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Cập nhật thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cn.disconnect();
                                LoadDataDuAn();
                                ClearAllInputs(this);
                            }
                            else
                            {
                                MessageBox.Show("Sửa dự án thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cn.disconnect();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbmaDA.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã dự án để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); // hoặc mã
                    return;
                }
                cn.connect();
                string MaDAtimkiem = tbmaDA.Text.Trim();
                string sql = @" SELECT MaDA, TenDA, MoTa, NgayBatDau, NgayKetThuc, Ghichu
                                FROM tblDuAn
                                WHERE DeletedAt = 0 AND MaDA LIKE @MaDA
                                ORDER BY MaDA";
                using (SqlCommand cmd = new SqlCommand(sql, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@MaDA", "%" + MaDAtimkiem + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtGridViewDA.DataSource = dt;
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataDuAn();
        }

        private void btnXemDAdaKetThuc_Click(object sender, EventArgs e)
        {
            try
            {
                cn.connect();
                string query = @" SELECT MaDA, TenDA, MoTa, NgayBatDau, NgayKetThuc, Ghichu FROM tblDuAn WHERE DeletedAt =1 ORDER BY MaDA";
                using (SqlDataAdapter da = new SqlDataAdapter(query, cn.conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dtGridViewDA.DataSource = dt;
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnKhoiPhucDA_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbmaDA.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập mã Dự Án cần khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.connect();
                string query = "SELECT COUNT(*) FROM tblDuAn WHERE MaDA = @MaDA AND DeletedAt = 1";
                using (SqlCommand cmdcheckPB = new SqlCommand(query, cn.conn))
                {
                    cmdcheckPB.Parameters.AddWithValue("@MaDA", tbmaDA.Text.Trim());
                    int emailCount = (int)cmdcheckPB.ExecuteScalar();

                    if (emailCount == 0)
                    {
                        MessageBox.Show("Mã Dự Án này đã tồn tại trong hệ thống!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cn.disconnect();
                        return;
                    }
                }

                //
                if (tbMKKhoiPhuc.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu để khôi phục", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Question);
                    return;
                }

                string sqMKKhoiPhuc = "SELECT * FROM tblTaiKhoan WHERE Quyen = @Quyen AND MatKhau = @MatKhau";
                SqlCommand cmdkhoiphuc = new SqlCommand(sqMKKhoiPhuc, cn.conn);
                cmdkhoiphuc.Parameters.AddWithValue("@Quyen", "Admin");
                cmdkhoiphuc.Parameters.AddWithValue("@MatKhau", tbMKKhoiPhuc.Text);
                SqlDataReader reader = cmdkhoiphuc.ExecuteReader();

                if (reader.Read() == false)
                {
                    MessageBox.Show("mật khẩu không đúng? Vui lòng nhập lại mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    tbMKKhoiPhuc.Text = "";
                    reader.Close();
                    cn.disconnect();
                    return;
                }
                reader.Close();


                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn khôi phục dự án này không?",
                    "Xác nhận khôi phục",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );


                if (confirm == DialogResult.Yes)
                {
                    tbMKKhoiPhuc.Text = "";
                    string querytblPhongBan = "UPDATE tblDuAn SET DeletedAt = 0 WHERE MaDA = @MaDA";
                    using (SqlCommand cmd = new SqlCommand(querytblPhongBan, cn.conn))
                    {
                        // DELETE FROM tblNhanVien WHERE MaNV = @MaNV / UPDATE tblNhanVien SET DeletedAt = 1 WHERE MaNV = @MaNV
                        cmd.Parameters.AddWithValue("@MaDA", tbmaDA.Text);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("khôi phục dự án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cn.disconnect();
                            ClearAllInputs(this);
                            LoadDataDuAn();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dự án để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cn.disconnect();
                        }
                    }
                    //cn.disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
                //MessageBox.Show("Chi tiết lỗi: " + ex.ToString(), "Lỗi hệ thống",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHienMK.Checked)
            {
                tbMKKhoiPhuc.UseSystemPasswordChar = false;
            }
            else
            {
                tbMKKhoiPhuc.UseSystemPasswordChar = true;
            }
        }

        private void dtGridViewDA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                tbmaDA.Text = dtGridViewDA.Rows[i].Cells[0].Value.ToString();
                tbTenDA.Text = dtGridViewDA.Rows[i].Cells[1].Value.ToString();
                tbMota.Text = dtGridViewDA.Rows[i].Cells[2].Value.ToString();
                DatePickerNgayBatDau.Value = Convert.ToDateTime(dtGridViewDA.Rows[i].Cells[3].Value); 
                DatePickerNgayKetThuc.Value = Convert.ToDateTime(dtGridViewDA.Rows[i].Cells[4].Value);
                tbGhiChu.Text = dtGridViewDA.Rows[i].Cells[5].Value.ToString();
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {

        }

        private void F_DuAn_Load_1(object sender, EventArgs e)
        {
            LoadDataDuAn();
        }
    }
}
