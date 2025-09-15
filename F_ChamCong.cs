using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using DocumentFormat.OpenXml.Wordprocessing;
using ZXing;

namespace QuanLyNhanVien3
{
    public partial class F_ChamCong : Form
    {
        private FilterInfoCollection videoDevices;  // Danh sách camera
        private VideoCaptureDevice videoSource;     // Camera đang chạy
        private bool isChamCongMode = false;        // true = chế độ chấm công, false = chỉ quét mã

        connectData cn = new connectData(); // Class kết nối SQL của bạn

        public F_ChamCong()
        {
            InitializeComponent();
        }

        // ===== XÓA INPUT =====
        //private void ClearAllInputs(Control parent)
        //{
        //    foreach (Control ctl in parent.Controls)
        //    {
        //        if (ctl is TextBox)
        //            ((TextBox)ctl).Clear();
        //        else if (ctl is ComboBox)
        //            ((ComboBox)ctl).SelectedIndex = -1;
        //        else if (ctl is DateTimePicker)
        //            ((DateTimePicker)ctl).Value = DateTime.Now;
        //        else if (ctl.HasChildren)
        //            ClearAllInputs(ctl);
        //    }
        //}

        // ===== LOAD DỮ LIỆU CHẤM CÔNG =====
        private void LoadDataChamCong()
        {
            try
            {
                cn.connect();

                string sql = @"
                    SELECT Id, MaChamCong, MaNV, Ngay, GioVao, GioVe, Ghichu
                    FROM tblChamCong
                    WHERE DeletedAt = 0
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

        // ===== LOAD NHÂN VIÊN VÀO COMBOBOX =====
        private void LoadcomboBox()
        {
            try
            {
                cn.connect();
                string sql = "SELECT MaNV, HoTen FROM tblNhanVien WHERE DeletedAt = 0";
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


        //private void btnThem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cn.connect();
        //        string maChamCong = "CC" + DateTime.Now.ToString("yyyyMMddHHmmss");

        //        string sql = @"
        //            INSERT INTO tblChamCong (MaChamCong, MaNV, Ngay, GioVao, GioVe, Ghichu)
        //            VALUES (@MaChamCong, @MaNV, @Ngay, @GioVao, @GioVe, @Ghichu)";

        //        SqlCommand cmd = new SqlCommand(sql, cn.conn);
        //        cmd.Parameters.AddWithValue("@MaChamCong", maChamCong);
        //        cmd.Parameters.AddWithValue("@MaNV", ccBoxMaNV.SelectedValue);
        //        cmd.Parameters.AddWithValue("@Ngay", dateTimeNgayChamCong.Value.Date);
        //        cmd.Parameters.AddWithValue("@GioVao", tbGioVao.Text);
        //        cmd.Parameters.AddWithValue("@GioVe", tbGioVe.Text);
        //        cmd.Parameters.AddWithValue("@Ghichu", tbGhiChu.Text);

        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("Thêm thành công!");
        //        LoadDataChamCong();
        //        ClearAllInputs(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi thêm dữ liệu: " + ex.Message);
        //    }
        //    finally
        //    {
        //        cn.disconnect();
        //    }
        //}

        //private void btnSua_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ccBoxMaNV.SelectedIndex == -1)
        //        {
        //            MessageBox.Show("Vui lòng chọn bản ghi cần sửa!");
        //            return;
        //        }

        //        cn.connect();
        //        string sql = @"
        //            UPDATE tblChamCong
        //            SET MaNV = @MaNV, Ngay = @Ngay, GioVao = @GioVao, GioVe = @GioVe, Ghichu = @Ghichu
        //            WHERE Id = @Id";

        //        SqlCommand cmd = new SqlCommand(sql, cn.conn);
        //        cmd.Parameters.AddWithValue("@Id", txtId.Text);
        //        cmd.Parameters.AddWithValue("@MaNV", ccBoxMaNV.SelectedValue);
        //        cmd.Parameters.AddWithValue("@Ngay", dtpNgay.Value.Date);
        //        cmd.Parameters.AddWithValue("@GioVao", dtpGioVao.Value.TimeOfDay);
        //        cmd.Parameters.AddWithValue("@GioVe", dtpGioVe.Value.TimeOfDay);
        //        cmd.Parameters.AddWithValue("@Ghichu", txtGhichu.Text);

        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("Sửa thành công!");
        //        LoadDataChamCong();
        //        ClearAllInputs(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi sửa dữ liệu: " + ex.Message);
        //    }
        //    finally
        //    {
        //        cn.disconnect();
        //    }
        //}

        //private void btnXoa_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtId.Text == "")
        //        {
        //            MessageBox.Show("Vui lòng chọn bản ghi cần xóa!");
        //            return;
        //        }

        //        cn.connect();
        //        string sql = "UPDATE tblChamCong SET DeletedAt = 1 WHERE Id = @Id";
        //        SqlCommand cmd = new SqlCommand(sql, cn.conn);
        //        cmd.Parameters.AddWithValue("@Id", txtId.Text);

        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("Xóa thành công!");
        //        LoadDataChamCong();
        //        ClearAllInputs(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi xóa dữ liệu: " + ex.Message);
        //    }
        //    finally
        //    {
        //        cn.disconnect();
        //    }
        //}

        //private void btnTimKiem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cn.connect();
        //        string keyword = txtTimKiem.Text.Trim();

        //        string sql = @"
        //            SELECT Id, MaChamCong, MaNV, Ngay, GioVao, GioVe, Ghichu
        //            FROM tblChamCong
        //            WHERE DeletedAt = 0 
        //            AND (MaNV LIKE @keyword OR MaChamCong LIKE @keyword)";

        //        SqlDataAdapter da = new SqlDataAdapter(sql, cn.conn);
        //        da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        dtGridViewChamCong.DataSource = dt;

        //        cn.disconnect();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
        //    }
        //}

        //private void btnrestar_Click(object sender, EventArgs e)
        //{
        //    ClearAllInputs(this);
        //    LoadDataChamCong();
        //}

        // ===== FORM LOAD =====
        private void F_ChamCong_Load(object sender, EventArgs e)
        {
            LoadcomboBox();
            LoadDataChamCong();
        }
        // ======== Hàm Bắt Đầu Camera ========
        private void StartCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("Không tìm thấy camera!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ======== Hàm Tắt Camera ========
        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }

        // ======== Xử lý hình ảnh từ Camera ========
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                //    Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                //    pictureBoxChamCong.Image = bitmap;

                //    BarcodeReader reader = new BarcodeReader();
                //    var result = reader.Decode(bitmap);

                //    if (result != null)
                //    {
                //        this.Invoke(new Action(() =>
                //        {
                //            txtMaNV.Text = result.Text;
                //            cbMaNV.Items.Clear();
                //            cbMaNV.Items.Add(result.Text);
                //            cbMaNV.SelectedIndex = 0;

                //            if (isChamCongMode)
                //            {
                //                // Nếu chế độ chấm công thì thực hiện luôn
                //                ChamCong(result.Text);
                //            }

                //            StopCamera(); // Dừng camera sau khi quét xong
                //        }));
                //    }
            }
            catch { }
        }

        // ======== Nút Chấm Công - Bật camera và chấm công ========
        private void btnChamCong_Click(object sender, EventArgs e)
        {
            isChamCongMode = true; // chế độ chấm công
            StartCamera();
        }

        // ======== Nút Quét Mã - Chỉ quét không chấm công ========
        private void btnQuetMa_Click(object sender, EventArgs e)
        {
            isChamCongMode = false; // chỉ quét
            StartCamera();
        }

        // ======== Nút Chọn Ảnh QR từ file - Chỉ quét không chấm công ========
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            //    using (OpenFileDialog ofd = new OpenFileDialog())
            //    {
            //        ofd.Title = "Chọn ảnh QR Code";
            //        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            //        if (ofd.ShowDialog() == DialogResult.OK)
            //        {
            //            pictureBoxChamCong.Image = Image.FromFile(ofd.FileName);

            //            BarcodeReader reader = new BarcodeReader();
            //            var result = reader.Decode((Bitmap)pictureBoxQR.Image);

            //            if (result != null)
            //            {
            //                txtMaNV.Text = result.Text;
            //                cbMaNV.Items.Clear();
            //                cbMaNV.Items.Add(result.Text);
            //                cbMaNV.SelectedIndex = 0;
            //            }
            //            else
            //            {
            //                MessageBox.Show("Không nhận diện được mã QR!", "Thông báo",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //        }
            //    }
        }

        // ======== Hàm Chấm Công Check-in / Check-out ========
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
                        // === Check-in ===
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
                        // === Check-out ===
                        DataRow row = dt.Rows[0];
                        if (row["GioVe"] == DBNull.Value)
                        {
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
                MessageBox.Show("Lỗi chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.disconnect();
            }
        }

        // ======== Đóng Form dừng camera ========
        private void F_ChamCong_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
