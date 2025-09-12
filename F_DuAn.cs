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

        private void F_DuAn_Load(object sender, EventArgs e)
        {
            LoadDataDuAn();
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
                    cmd.Parameters.AddWithValue("@NgayBatDau", DatePickerNgayBatDau.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgayKetThuc", DatePickerNgayKetThuc.Text.Trim());
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
    }
}
