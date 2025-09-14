using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhanVien3
{
    public partial class F_Luong : Form
    {
        connectData db = new connectData();

        public F_Luong()
        {
            InitializeComponent();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            dgvLuong.CellClick += dataGridView1_CellClick;

        }

        private void F_Luong_Load(object sender, EventArgs e)
        {
            HienThiBangLuong();
        }

        // Hàm hiển thị bảng lương
        private void HienThiBangLuong()
        {
            try
            {
                db.connect();
                string sql = @" SELECT  L.MaLuong, L.MaNV,   L.Thang,  L.Nam, HD.LuongCoBan, L.SoNgayCong,    L.PhuCap,L.KhauTru, L.Ghichu,
                                    (HD.LuongCoBan + L.PhuCap - L.KhauTru) AS TongLuong
                                FROM tblLuong AS L
                                INNER JOIN tblHopDong AS HD ON L.MaNV = HD.MaNV
                                WHERE L.DeletedAt = 0 AND HD.DeletedAt = 0";
                SqlDataAdapter da = new SqlDataAdapter(sql, db.conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLuong.DataSource = dt;
                db.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
            }
        }

        // Nút thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $"INSERT INTO tblLuong(MaLuong, MaNV, Thang, Nam, LuongCoBan, SoNgayCong, PhuCap, KhauTru) " +
                             $"VALUES('{txtMaLuong.Text}', '{txtMaNV.Text}', {txtThang.Text}, {txtNam.Text}, {txtLuongCoBan.Text}, {txtSoNgayCong.Text}, {txtPhuCap.Text}, {txtKhauTru.Text})";

                db.connect();
                if (db.exeSQL(sql))
                {
                    MessageBox.Show("Thêm thành công!");
                    HienThiBangLuong();
                }
                else
                {
                    MessageBox.Show("Không thêm được!");
                }
                db.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        // Nút sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $"UPDATE tblLuong SET " +
                             $"MaNV = '{txtMaNV.Text}', Thang = {txtThang.Text}, Nam = {txtNam.Text}, " +
                             $"LuongCoBan = {txtLuongCoBan.Text}, SoNgayCong = {txtSoNgayCong.Text}, " +
                             $"PhuCap = {txtPhuCap.Text}, KhauTru = {txtKhauTru.Text} " +
                             $"WHERE MaLuong = '{txtMaLuong.Text}'";

                db.connect();
                if (db.exeSQL(sql))
                {
                    MessageBox.Show("Sửa thành công!");
                    HienThiBangLuong();
                }
                else
                {
                    MessageBox.Show("Không sửa được!");
                }
                db.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        // Nút xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $"DELETE FROM tblLuong WHERE MaLuong = '{txtMaLuong.Text}'";

                db.connect();
                if (db.exeSQL(sql))
                {
                    MessageBox.Show("Xóa thành công!");
                    HienThiBangLuong();
                }
                else
                {
                    MessageBox.Show("Không xóa được!");
                }
                db.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        //// Nút tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                string sql = $"SELECT * FROM tblLuong WHERE MaNV LIKE '%{txtTimKiem.Text}%' OR MaLuong LIKE '%{txtTimKiem.Text}%'";
                SqlDataAdapter da = new SqlDataAdapter(sql, db.conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLuong.DataSource = dt;
                db.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // Khi click vào DataGridView thì đổ dữ liệu ra textbox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLuong.Rows[e.RowIndex];
                txtMaLuong.Text = row.Cells["MaLuong"].Value.ToString();
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtThang.Text = row.Cells["Thang"].Value.ToString();
                txtNam.Text = row.Cells["Nam"].Value.ToString();
                txtLuongCoBan.Text = row.Cells["LuongCoBan"].Value.ToString();
                txtSoNgayCong.Text = row.Cells["SoNgayCong"].Value.ToString();
                txtPhuCap.Text = row.Cells["PhuCap"].Value.ToString();
                txtKhauTru.Text = row.Cells["KhauTru"].Value.ToString();
            }
        }

    }
}
