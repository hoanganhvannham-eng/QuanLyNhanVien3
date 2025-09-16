using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using DocumentFormat.OpenXml.Wordprocessing;
using ZXing;
using AForge.Video.DirectShow;
using AForge.Video;
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
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using DocumentFormat.OpenXml.Spreadsheet;

namespace QuanLyNhanVien3
{
    public partial class F_ChamCong : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

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


        // ===== FORM LOAD =====
        private void F_ChamCong_Load(object sender, EventArgs e)
        {
            LoadcomboBox();
            LoadDataChamCong();
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            isChamCongMode = true;  // Chế độ chấm công
            StartCamera();          // Bật camera
        }

        // ======== Nút Chọn Ảnh QR từ file - Chỉ quét không chấm công ========
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh QR Code";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Hiển thị ảnh vừa chọn lên PictureBox
                        pictureBoxChamCong.Image = Image.FromFile(ofd.FileName);

                        // Giải mã QR
                        BarcodeReader reader = new BarcodeReader();
                        var result = reader.Decode((Bitmap)pictureBoxChamCong.Image);

                        if (result != null)
                        {
                            string maNV = result.Text.Trim();

                            // Kiểm tra mã NV có tồn tại trong CSDL không
                            cn.connect();
                            string query = "SELECT MaNV, HoTen FROM tblNhanVien WHERE MaNV = @MaNV AND DeletedAt = 0";
                            using (SqlCommand cmd = new SqlCommand(query, cn.conn))
                            {
                                cmd.Parameters.AddWithValue("@MaNV", maNV);

                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    // Load dữ liệu vào ComboBox
                                    ccBoxMaNV.DataSource = dt;
                                    ccBoxMaNV.DisplayMember = "HoTen";   // Hiển thị tên nhân viên
                                    ccBoxMaNV.ValueMember = "MaNV";      // Giá trị là mã nhân viên
                                    ccBoxMaNV.SelectedValue = maNV;

                                    MessageBox.Show("Đã quét thành công mã nhân viên: " + maNV,
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Mã nhân viên không tồn tại hoặc đã nghỉ việc!",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không nhận diện được mã QR!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message,
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cn.disconnect();
                    }
                }
            }
        }



        // tesst cam ==========================================
        private bool isChamCongMode = false;  // true = Chấm công, false = Chỉ quét mã
        private string scannedMaNV = null;    // Lưu mã nhân viên quét được
        private void LoadNhanVienToComboBox(string maNV)
        {
            try
            {
                cn.connect();
                string query = "SELECT MaNV, HoTen FROM tblNhanVien WHERE MaNV = @MaNV AND DeletedAt = 0";

                using (SqlCommand cmd = new SqlCommand(query, cn.conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        ccBoxMaNV.DataSource = dt;
                        ccBoxMaNV.DisplayMember = "HoTen";
                        ccBoxMaNV.ValueMember = "MaNV";
                        ccBoxMaNV.SelectedValue = maNV;
                        MessageBox.Show($"Đã quét thành công!\nMã nhân viên: {scannedMaNV}",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên hoặc nhân viên đã nghỉ việc!",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.disconnect();
            }
        }

        private void StartCamera()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy camera!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chọn camera đầu tiên
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();

                timer1.Start(); // Timer quét QR liên tục
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi bật camera: " + ex.Message);
            }
        }
        // sop camenra 
        private void StopCamera()
        {
            try
            {
                // 🔹 Dừng camera nếu đang chạy
                if (videoSource != null)
                {
                    if (videoSource.IsRunning)
                    {
                        videoSource.SignalToStop();  // Yêu cầu camera dừng
                        videoSource.WaitForStop();   // Đợi camera dừng hẳn
                    }

                    videoSource.NewFrame -= VideoSource_NewFrame; // Gỡ sự kiện frame
                    videoSource = null; // Giải phóng đối tượng
                }

                // 🔹 Dừng Timer quét QR
                if (timer1.Enabled)
                    timer1.Stop();

                // 🔹 Giải phóng hình ảnh trong PictureBox
                if (pictureBoxChamCong.Image != null)
                {
                    pictureBoxChamCong.Image.Dispose();
                    pictureBoxChamCong.Image = null;
                }

                GC.Collect();       // Thu gom rác .NET
                GC.WaitForPendingFinalizers(); // Đảm bảo giải phóng xong
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tắt camera: " + ex.Message,
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra camera có ảnh chưa
                if (pictureBoxChamCong.Image == null) return;

                Bitmap snapshot;

                // 🔹 Tạo bản sao an toàn từ ảnh hiện tại
                lock (pictureBoxChamCong.Image)
                {
                    snapshot = new Bitmap(pictureBoxChamCong.Image);
                }

                // Khởi tạo BarcodeReader
                BarcodeReader reader = new BarcodeReader
                {
                    Options = new ZXing.Common.DecodingOptions
                    {
                        CharacterSet = "UTF-8"
                    }
                };

                // Decode từ bản sao
                var result = reader.Decode(snapshot);

                snapshot.Dispose(); // Giải phóng bộ nhớ sau khi decode

                if (result != null)
                {
                    timer1.Stop(); // Dừng quét để xử lý dữ liệu
                    StopCamera();  // Dừng camera tạm thời

                    //string maNV = result.Text.Trim();

                    scannedMaNV = result.Text.Trim(); // Lưu mã nhân viên quét được
                    if (!string.IsNullOrEmpty(scannedMaNV))
                    {
                        if (isChamCongMode)
                        {
                            // Chế độ chấm công → Quét và chấm công luôn
                            ChamCong(scannedMaNV);
                        }
                        else
                        {
                            // Chế độ chỉ quét mã → Hiển thị thông tin nhân viên
                            LoadNhanVienToComboBox(scannedMaNV);
                        }
                    }
                    //MessageBox.Show("Quét thành công: " + maNV);

                    //ChamCong(maNV); // Hàm lưu dữ liệu chấm công

                    //StartCamera(); // Mở lại camera
                    //timer1.Start();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi quét QR: " + ex.Message);
            }
        }
        private string GenerateMaChamCong()
        {
            string newMa = "CC0001"; // Mặc định nếu chưa có dữ liệu
            string query = "SELECT TOP 1 MaChamCong FROM tblChamCong ORDER BY Id DESC";

            using (SqlConnection conn = new SqlConnection(cn.conn.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string lastMa = result.ToString();  // VD: "CC0005"
                    int number = int.Parse(lastMa.Substring(2)); // Lấy phần số: 0005 -> 5
                    number++;
                    newMa = "CC" + number.ToString("D4"); // Format về 4 chữ số: 0006
                }
            }
            return newMa;
        }


        // ===== Hiển thị video từ camera lên PictureBox =====
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                if (pictureBoxChamCong.Image != null)
                {
                    var oldImage = pictureBoxChamCong.Image;
                    pictureBoxChamCong.Image = null;
                    oldImage.Dispose();
                }

                pictureBoxChamCong.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi camera: " + ex.Message);
            }
        }

        // ======== Hàm Chấm Công Check-in / Check-out ========
        public void ChamCong(string maNV)
        {
            try
            {
                cn.connect();

                // ========================== 🔹 BƯỚC 1: KIỂM TRA NHÂN VIÊN ==========================
                string checkNVQuery = @"
                                        SELECT DeletedAt 
                                        FROM tblNhanVien 
                                        WHERE MaNV = @MaNV";

                using (SqlCommand cmdNV = new SqlCommand(checkNVQuery, cn.conn))
                {
                    cmdNV.Parameters.AddWithValue("@MaNV", maNV);
                    object result = cmdNV.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show($"Không tìm thấy nhân viên",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // ⛔ Dừng luôn
                    }

                    int deletedAt = Convert.ToInt32(result);

                    if (deletedAt != 0)
                    {
                        // 🔹 Nhân viên đã nghỉ việc
                        MessageBox.Show($"Không tìm thấy nhân viên hoặc nhân viên đã nghỉ việc!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return; // ⛔ Dừng luôn
                    }
                }

                // ========================== 🔹 BƯỚC 2: XỬ LÝ CHẤM CÔNG ==========================
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

                    // ==== LẦN QUÉT THỨ 1: CHECK-IN ====
                    if (dt.Rows.Count == 0)
                    {
                        // Sinh mã tự động: CC0001, CC0002,...
                        string maChamCong = GenerateMaChamCong();

                        string insertQuery = @"
                    INSERT INTO tblChamCong (MaChamCong, MaNV, Ngay, GioVao, GioVe, Ghichu)
                    VALUES (@MaChamCong, @MaNV, CAST(GETDATE() AS DATE), 
                            CONVERT(TIME, GETDATE()), CONVERT(TIME, GETDATE()), N'Đi làm')";

                        using (SqlCommand cmdInsert = new SqlCommand(insertQuery, cn.conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@MaChamCong", maChamCong);
                            cmdInsert.Parameters.AddWithValue("@MaNV", maNV);

                            if (cmdInsert.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show($"Nhân viên {maNV} đã **check-in** thành công!\nThời gian vào: {DateTime.Now:HH:mm:ss}",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        // ==== LẦN QUÉT THỨ 2: CHECK-OUT ====
                        DataRow row = dt.Rows[0];

                        if (row["GioVao"].ToString() == row["GioVe"].ToString()) // Lần quét đầu: GioVao = GioVe
                        {
                            string updateQuery = @"
                        UPDATE tblChamCong
                        SET GioVe = CONVERT(TIME, GETDATE()), 
                            Ghichu = N'Hoàn thành ngày làm việc'
                        WHERE Id = @Id";

                            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, cn.conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@Id", row["Id"]);

                                if (cmdUpdate.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show($"Nhân viên {maNV} đã **check-out** thành công!\nThời gian ra: {DateTime.Now:HH:mm:ss}",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Nhân viên {maNV} hôm nay đã check-in và check-out đầy đủ.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    LoadDataChamCong(); // Refresh danh sách chấm công
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


        private void btnrestar_Click(object sender, EventArgs e)
        {
            LoadDataChamCong();
        }

        private void btnDungQuetCam_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        private void btnQuetma_Click_1(object sender, EventArgs e)
        {
            isChamCongMode = false; // Chế độ chỉ quét mã
            StartCamera();          // Bật camera
        }
    }
}
