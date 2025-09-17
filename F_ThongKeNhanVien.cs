using ClosedXML.Excel;
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
    public partial class F_ThongKeNhanVien: Form
    {
        public F_ThongKeNhanVien()
        {
            InitializeComponent();
        }

        connectData c = new connectData();

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rdbTheoNgay.Checked && !rdbTheoThang.Checked)
                {
                    MessageBox.Show("Vui lòng chọn kiểu thống kê theo Ngày hoặc Tháng!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = "";
                c.connect();

                // ============= TRƯỜNG HỢP THỐNG KÊ LƯƠNG =============
                if (rdbLuong.Checked)
                {
                    sql = @"
                SELECT nv.MaNV, nv.HoTen, nv.MaPB, nv.MaCV, 
                       l.MaLuong, l.LuongCoBan, l.PhuCap, l.KhauTru, l.TongLuong
                FROM tblNhanVien nv
                INNER JOIN tblLuong l ON nv.MaNV = l.MaNV
                WHERE l.DeletedAt = 0
            ";

                    // lọc theo ngày
                    if (rdbTheoNgay.Checked)
                    {
                        sql += " AND l.Ngay BETWEEN @FromDate AND @ToDate";
                    }
                    // lọc theo tháng
                    else if (rdbTheoThang.Checked)
                    {
                        if (numThang.Value == 0 || numNam.Value == 0)
                        {
                            MessageBox.Show("Vui lòng nhập Tháng và Năm hợp lệ!",
                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        sql += " AND l.Thang = @Thang AND l.Nam = @Nam";
                    }

                    // lọc theo mã nhân viên
                    if (!string.IsNullOrEmpty(txtMaNV.Text.Trim()))
                    {
                        sql += " AND nv.MaNV = @MaNV";
                    }
                }
                // ============= TRƯỜNG HỢP THỐNG KÊ CHẤM CÔNG =============
                else if (rdbChamCong.Checked)
                {
                    sql = @"
                SELECT nv.MaNV, nv.HoTen, nv.MaPB, nv.MaCV, 
                       cc.MaChamCong, cc.Ngay, cc.GioVao, cc.GioVe,
                       CASE 
                           WHEN DATEDIFF(HOUR, cc.GioVao, cc.GioVe) >= 8 
                           THEN N'Đủ' ELSE N'Không đủ' 
                       END AS TrangThai
                FROM tblNhanVien nv
                INNER JOIN tblChamCong cc ON nv.MaNV = cc.MaNV
                WHERE cc.DeletedAt = 0
            ";

                    if (rdbTheoNgay.Checked)
                    {
                        sql += " AND cc.Ngay BETWEEN @FromDate AND @ToDate";
                    }
                    else if (rdbTheoThang.Checked)
                    {
                        if (numThang.Value == 0 || numNam.Value == 0)
                        {
                            MessageBox.Show("Vui lòng nhập Tháng và Năm hợp lệ!",
                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        sql += " AND MONTH(cc.Ngay) = @Thang AND YEAR(cc.Ngay) = @Nam";
                    }

                    if (!string.IsNullOrEmpty(txtMaNV.Text.Trim()))
                    {
                        sql += " AND nv.MaNV = @MaNV";
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại thống kê (Lương / Chấm công)",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlCommand cmd = new SqlCommand(sql, c.conn))
                {
                    // Tham số ngày
                    if (rdbTheoNgay.Checked)
                    {
                        cmd.Parameters.AddWithValue("@FromDate", dtpFromDate.Value.Date);
                        cmd.Parameters.AddWithValue("@ToDate", dtpToDate.Value.Date);
                    }

                    // Tham số tháng/năm
                    if (rdbTheoThang.Checked)
                    {
                        cmd.Parameters.AddWithValue("@Thang", Convert.ToInt32(numThang.Value));
                        cmd.Parameters.AddWithValue("@Nam", Convert.ToInt32(numNam.Value));
                    }

                    // Tham số mã NV
                    if (!string.IsNullOrEmpty(txtMaNV.Text.Trim()))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text.Trim());
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtGridViewThongKe.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                c.disconnect();
            }
        }

        private void F_ThongKeNhanVien_Load(object sender, EventArgs e)
        {
            numThang.Value = DateTime.Now.Month;
            numNam.Value = DateTime.Now.Year;
            rdbTheoNgay.Checked = true; // mặc định theo ngày
                                        // Gắn sự kiện
            rdbLuong.CheckedChanged += rdbLuong_CheckedChanged;
            rdbChamCong.CheckedChanged += rdbChamCong_CheckedChanged;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dtGridViewThongKe.Rows.Count > 0)
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                var ws = wb.Worksheets.Add("Luong");

                                // Ghi header
                                for (int i = 0; i < dtGridViewThongKe.Columns.Count; i++)
                                {
                                    ws.Cell(1, i + 1).Value = dtGridViewThongKe.Columns[i].HeaderText;
                                }

                                // Ghi dữ liệu
                                for (int i = 0; i < dtGridViewThongKe.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dtGridViewThongKe.Columns.Count; j++)
                                    {
                                        ws.Cell(i + 2, j + 1).Value = dtGridViewThongKe.Rows[i].Cells[j].Value?.ToString();
                                    }
                                }

                                // Thêm border
                                var range = ws.Range(1, 1, dtGridViewThongKe.Rows.Count + 1, dtGridViewThongKe.Columns.Count);
                                range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                                // Tự động co giãn cột
                                ws.Columns().AdjustToContents();

                                // Lưu file
                                wb.SaveAs(sfd.FileName);
                            }

                            MessageBox.Show("Xuất Excel bảng Lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbLuong_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLuong.Checked)
            {
                // Khi chọn Lương -> mặc định theo tháng
                rdbTheoThang.Checked = true;

                numThang.Value = DateTime.Now.Month;
                numNam.Value = DateTime.Now.Year;
            }
        }

        private void rdbChamCong_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbChamCong.Checked)
            {
                // Khi chọn Chấm công -> mặc định theo ngày
                rdbTheoNgay.Checked = true;

                dtpFromDate.Value = DateTime.Now.AddDays(-7); // mặc định từ 7 ngày trước
                dtpToDate.Value = DateTime.Now;               // đến hôm nay
            }
        }


    }
}
