using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace QuanLyNhanVien3
{
    public partial class F_ChamCong : Form
    {
        public F_ChamCong()
        {
            InitializeComponent();
        }

        connectData cn = new connectData();
        private FilterInfoCollection videoDevices; // Danh sách camera
        private VideoCaptureDevice videoSource;    // Camera được chọn

        // Xóa toàn bộ dữ liệu các control input
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

        // Load dữ liệu chấm công ra DataGridView
        private void LoadDataChamCong()
        {
            try
            {
                cn.connect();

                string sql = @"
                    SELECT Id, MaChamCong, MaNV, Ngay, GioVao, GioVe, Ghichu
                    FROM tblChamCong
                    WHERE DeletedAt = 0 OR DeletedAt IS NULL
                    ORDER BY Ngay DESC";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, cn.conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtGridViewChamCong.DataSource = dt;
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu chấm công: " + ex.Message,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load danh sách nhân viên vào ComboBox
        private void LoadcomboBox()
        {
            try
            {
                cn.connect();
                string sql = "SELECT MaNV, HoTen FROM tblNhanVien WHERE DeletedAt = 0 OR DeletedAt IS NULL";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn.conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ccBoxMaNV.DataSource = dt;
                    ccBoxMaNV.DisplayMember = "HoTen";
                    ccBoxMaNV.ValueMember = "MaNV";
                }
                cn.disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load mã nhân viên: " + ex.Message);
            }
        }

        // Bật camera và quét QR
        private void btnQuetma_Click(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
                videoSource.Start();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Không tìm thấy camera!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Hiển thị camera lên PictureBox
        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBoxChamCong.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        // Timer quét QR liên tục
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBoxChamCong.Image == null)
            {
                MessageBox.Show("Chưa có hình ảnh từ camera.");
                return;
            }

            MessageBox.Show("Có hình ảnh, đang giải mã...");

            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode((Bitmap)pictureBoxChamCong.Image);

            if (result != null)
            {
                timer1.Stop();
                MessageBox.Show("Mã quét được: " + result.Text);
            }
            else
            {
                MessageBox.Show("Chưa nhận diện được QR.");
            }
        }

        // Hàm xử lý chấm công
        public void ChamCong(string maNV)
        {
            try
            {
                cn.connect();

                string checkQuery = @"
                    SELECT TOP 1 *
                    FROM tblChamCong
                    WHERE MaNV = @MaNV AND Ngay = CAST(GETDATE() AS DATE)
                    ORDER BY Id DESC";

                using (SqlCommand cmdCheck = new SqlCommand(checkQuery, cn.conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaNV", maNV);

                    SqlDataAdapter da = new SqlDataAdapter(cmdCheck);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        // Chưa có bản ghi hôm nay -> Check-in
                        string maChamCong = "CC" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        string insertQuery = @"
                            INSERT INTO tblChamCong (MaChamCong, MaNV, Ngay, GioVao, Ghichu)
                            VALUES (@MaChamCong, @MaNV, CAST(GETDATE() AS DATE), CONVERT(TIME, GETDATE()), N'Đi làm')";

                        SqlCommand cmdInsert = new SqlCommand(insertQuery, cn.conn);
                        cmdInsert.Parameters.AddWithValue("@MaChamCong", maChamCong);
                        cmdInsert.Parameters.AddWithValue("@MaNV", maNV);

                        if (cmdInsert.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show($"Nhân viên {maNV} đã check-in thành công!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Đã có bản ghi hôm nay
                        DataRow row = dt.Rows[0];
                        if (row["GioVe"] == DBNull.Value)
                        {
                            // Update giờ về (Check-out)
                            string updateQuery = @"
                                UPDATE tblChamCong
                                SET GioVe = CONVERT(TIME, GETDATE()), Ghichu = N'Hoàn thành ngày làm việc'
                                WHERE Id = @Id";

                            SqlCommand cmdUpdate = new SqlCommand(updateQuery, cn.conn);
                            cmdUpdate.Parameters.AddWithValue("@Id", row["Id"]);

                            if (cmdUpdate.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show($"Nhân viên {maNV} đã check-out thành công!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Nhân viên {maNV} hôm nay đã check-in và check-out đầy đủ.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chấm công: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.disconnect();
            }
        }

        private void F_ChamCong_Load(object sender, EventArgs e)
        {
            LoadcomboBox();
            LoadDataChamCong();
        }

        private void F_ChamCong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Chọn ảnh QR Code";
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // Hiển thị ảnh lên PictureBox
                        pictureBoxChamCong.Image = Image.FromFile(ofd.FileName);

                        // Giải mã QR từ ảnh
                        BarcodeReader reader = new BarcodeReader();
                        var result = reader.Decode((Bitmap)pictureBoxChamCong.Image);

                        if (result != null)
                        {
                            string maNV = result.Text; // Nội dung QR code
                            MessageBox.Show("Mã QR quét được: " + maNV, "Kết quả");

                            // TODO: Gọi hàm chấm công
                            ChamCong(maNV);
                        }
                        else
                        {
                            MessageBox.Show("Không nhận diện được mã QR!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi quét QR từ ảnh: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
