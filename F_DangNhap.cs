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
    public partial class F_DangNhap: Form
    {
        public F_DangNhap()
        {
            InitializeComponent();
        }
        connectData cn = new connectData();
        private void DangNhap_Load(object sender, EventArgs e)
        {
            tbpassword.UseSystemPasswordChar = true;
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {

            cn.connect();
            string username = tbusename.Text.Trim();
            string password = tbpassword.Text.Trim();
            string query = "select * from tblTaiKhoan where DeletedAt = 3 AND TenDangNhap = '" + username + "' " + "and MatKhau = '" + password + "'";
            SqlCommand cmd = new SqlCommand(query, cn.conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (tbusename.Text == "" || tbpassword.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu để đăng nhâp", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Question);
            }
            else if (reader.Read() == true)
            {
                this.Hide();
                F_FormMain f_Main = new F_FormMain();
                //MessageBox.Show("Đăng nhập thành công!",
                //                            "Thông báo");
                f_Main.ShowDialog();
                f_Main = null;
                tbpassword.Text = "";
                this.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng? Vui lòng nhập lại tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                tbpassword.Text = "";
            }
            cn.disconnect();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn thoat khong?", "tieu de thoat",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void checkshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkshowpassword.Checked)
            {
                // Hiển thị mật khẩu
                tbpassword.UseSystemPasswordChar = false;
            }
            else
            {
                // Ẩn mật khẩu
                tbpassword.UseSystemPasswordChar = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbusename_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
