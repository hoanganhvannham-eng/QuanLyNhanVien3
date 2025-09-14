
CREATE DATABASE QuanLyNhanVien3;
GO

USE QuanLyNhanVien3;
GO

-- 2. Các bảng

-- ===== Bảng Phòng Ban =====
CREATE TABLE tblPhongBan (
    Id INT PRIMARY KEY IDENTITY(1,1),          -- Khóa chính tự tăng
    MaPB VARCHAR(10) UNIQUE NOT NULL,          -- Mã phòng ban duy nhất
    TenPB NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    SoDienThoai VARCHAR(20),
    Ghichu NVARCHAR(255),
    DeletedAt INT NOT NULL DEFAULT 0            -- 0: chưa xóa, 1: đã xóa
);

-- ===== Bảng Chức Vụ =====
CREATE TABLE tblChucVu (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaCV VARCHAR(10) UNIQUE NOT NULL,
    TenCV NVARCHAR(100) NOT NULL,
    Ghichu NVARCHAR(255),
    DeletedAt INT NOT NULL DEFAULT 0
);

-- ===== Bảng Nhân Viên =====
CREATE TABLE tblNhanVien (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaNV VARCHAR(10) UNIQUE NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(200),
    SoDienThoai VARCHAR(20),
    Email VARCHAR(100) UNIQUE,                  -- Email không được trùng 
    MaPB VARCHAR(10) NOT NULL,                  -- Liên kết theo mã phòng ban
    MaCV VARCHAR(10) NOT NULL,                  -- Liên kết theo mã chức vụ
    Ghichu NVARCHAR(255),
    DeletedAt INT NOT NULL DEFAULT 0,
    FOREIGN KEY (MaPB) REFERENCES tblPhongBan(MaPB),
    FOREIGN KEY (MaCV) REFERENCES tblChucVu(MaCV)
);

-- ===== Bảng Hợp Đồng =====
CREATE TABLE tblHopDong (
    Id INT PRIMARY KEY IDENTITY(1,1),   
	MaHopDong VARCHAR(10) UNIQUE NOT NULL,   -- Mã hợp đồng duy nhất
    MaNV VARCHAR(10) NOT NULL, 
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE,
    LoaiHopDong NVARCHAR(50),
    LuongCoBan DECIMAL(18,2) NOT NULL,
    Ghichu NVARCHAR(255),
    DeletedAt INT NOT NULL DEFAULT 0,
    FOREIGN KEY (MaNV) REFERENCES tblNhanVien(MaNV)
);

-- ===== Bảng Lương =====
CREATE TABLE tblLuong (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaLuong VARCHAR(10) UNIQUE NOT NULL,
    MaNV VARCHAR(10) NOT NULL,
    Thang INT CHECK (Thang BETWEEN 1 AND 12),
    Nam INT,
    LuongCoBan DECIMAL(18,2) NOT NULL,         -- ########.## (tối đa 8 số nguyên + 2 số thập phân)
    SoNgayCong INT,
    PhuCap DECIMAL(18,2) DEFAULT 0,
    KhauTru DECIMAL(18,2) DEFAULT 0,
    Ghichu NVARCHAR(255),
    TongLuong AS (LuongCoBan + PhuCap - KhauTru) PERSISTED, -- cột tính toán
    DeletedAt INT NOT NULL DEFAULT 0,
    FOREIGN KEY (MaNV) REFERENCES tblNhanVien(MaNV)
);

-- ===== Bảng Dự Án =====
CREATE TABLE tblDuAn (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaDA VARCHAR(10) UNIQUE NOT NULL,
    TenDA NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(500),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    Ghichu NVARCHAR(255),
    DeletedAt INT NOT NULL DEFAULT 0
);

-- ===== Bảng Chi Tiết Dự Án =====
CREATE TABLE tblChiTietDuAn (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaNV VARCHAR(10) NOT NULL,
    MaDA VARCHAR(10) NOT NULL,
    VaiTro NVARCHAR(100),
    Ghichu NVARCHAR(255),
    DeletedAt INT NOT NULL DEFAULT 0,
    UNIQUE(MaNV, MaDA),                         -- Một nhân viên chỉ có 1 vai trò trong dự án
    FOREIGN KEY (MaNV) REFERENCES tblNhanVien(MaNV),
    FOREIGN KEY (MaDA) REFERENCES tblDuAn(MaDA)
);

-- ===== Bảng Chấm Công =====
CREATE TABLE tblChamCong (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaChamCong VARCHAR(10) UNIQUE NOT NULL,
    MaNV VARCHAR(10) NOT NULL,
    Ngay DATE NOT NULL,
    GioVao TIME NOT NULL,                       -- Giờ bắt đầu làm
    GioVe TIME NOT NULL,                        -- Giờ kết thúc làm
    Ghichu NVARCHAR(255),
    DeletedAt INT NOT NULL DEFAULT 0,
    FOREIGN KEY (MaNV) REFERENCES tblNhanVien(MaNV)
);

-- ===== Bảng Tài Khoản =====
CREATE TABLE tblTaiKhoan (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaTK VARCHAR(10) UNIQUE NOT NULL,
    MaNV VARCHAR(10) UNIQUE NOT NULL,
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL, 
    Quyen NVARCHAR(50) DEFAULT 'User',          -- Mặc định quyền User
    Ghichu NVARCHAR(255),
    DeletedAt INT NOT NULL DEFAULT 0,
    FOREIGN KEY (MaNV) REFERENCES tblNhanVien(MaNV)
);

-- ========================================
-- 3. Dữ liệu mẫu
-- ========================================

-- Phòng Ban
INSERT INTO tblPhongBan (MaPB, TenPB, DiaChi, SoDienThoai, Ghichu)
VALUES
('PB001', N'Phòng Hành Chính', N'Hà Nội', '0241234567', N'Quản lý nhân sự & hành chính'),
('PB002', N'Phòng Kỹ Thuật', N'Hồ Chí Minh', '0287654321', N'Phát triển & bảo trì hệ thống'),
('PB003', N'Phòng Kinh Doanh', N'Đà Nẵng', '0236789123', N'Tìm kiếm khách hàng và bán hàng');

-- Chức vụ
INSERT INTO tblChucVu (MaCV, TenCV, Ghichu)
VALUES
('CV001', N'Giám đốc', N'Quản lý toàn công ty'),
('CV002', N'Trưởng phòng', N'Quản lý phòng ban'),
('CV003', N'Nhân viên', N'Thực hiện công việc được giao');

-- Nhân viên
INSERT INTO tblNhanVien (MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaPB, MaCV, Ghichu)
VALUES
('NV001', N'Nguyễn Văn A', '1985-05-20', N'Nam', N'Hà Nội', '0912345678', 'ana@example.com', 'PB001', 'CV001', N'Giám đốc công ty'),
('NV002', N'Trần Thị B', '1990-09-15', N'Nữ', N'Hồ Chí Minh', '0987654321', 'hib@example.com', 'PB002', 'CV002', N'Trưởng phòng kỹ thuật'),
('NV003', N'Lê Văn C', '1995-12-01', N'Nam', N'Đà Nẵng', '0934567890', 'vnc@example.com', 'PB003', 'CV003', N'Nhân viên kinh doanh'),
('NV004', N'Phạm Thị D', '1997-07-07', N'Nữ', N'Hải Phòng', '0978123456', 'thi@example.com', 'PB002', 'CV003', N'Nhân viên kỹ thuật');

-- Hợp đồng
INSERT INTO tblHopDong (MaHopDong, MaNV, NgayBatDau, NgayKetThuc, LoaiHopDong, LuongCoBan, Ghichu)
VALUES
('HD001', 'NV001', '2020-01-01', NULL, N'Không xác định thời hạn', 30000000, N'Hợp đồng Giám đốc'),
('HD002', 'NV002', '2021-03-01', '2026-03-01', N'Xác định thời hạn 5 năm', 20000000, N'Hợp đồng Trưởng phòng'),
('HD003', 'NV003', '2022-06-15', '2025-06-15', N'Xác định thời hạn 3 năm', 12000000, N'Hợp đồng nhân viên KD'),
('HD004', 'NV004', '2023-02-01', '2026-02-01', N'Xác định thời hạn 3 năm', 10000000, N'Hợp đồng nhân viên KT');

-- Lương
INSERT INTO tblLuong (MaLuong, MaNV, Thang, Nam, LuongCoBan, SoNgayCong, PhuCap, KhauTru, Ghichu)
VALUES
('L001', 'NV001', 7, 2025, 30000000, 22, 5000000, 1000000, N'Lương tháng 7 Giám đốc'),
('L002', 'NV002', 7, 2025, 20000000, 22, 3000000, 500000, N'Lương tháng 7 Trưởng phòng'),
('L003', 'NV003', 7, 2025, 12000000, 21, 2000000, 200000, N'Lương tháng 7 NV KD'),
('L004', 'NV004', 7, 2025, 10000000, 20, 1500000, 100000, N'Lương tháng 7 NV KT');

-- Dự án
INSERT INTO tblDuAn (MaDA, TenDA, MoTa, NgayBatDau, NgayKetThuc, Ghichu)
VALUES
('DA001', N'Hệ thống ERP', N'Xây dựng hệ thống quản lý doanh nghiệp', '2023-01-01', '2025-12-31', N'Dự án quan trọng'),
('DA002', N'Website TMĐT', N'Phát triển website thương mại điện tử', '2024-03-01', '2025-03-01', N'Dự án thương mại'),
('DA003', N'Ứng dụng Mobile', N'Ứng dụng di động bán hàng', '2025-01-01', NULL, N'Dự án mới');

-- Chi tiết dự án
INSERT INTO tblChiTietDuAn (MaNV, MaDA, VaiTro, Ghichu)
VALUES
('NV001', 'DA001', N'Quản lý dự án', N'Giám sát toàn bộ'),
('NV002', 'DA001', N'Chủ trì kỹ thuật', N'Thiết kế hệ thống'),
('NV003', 'DA002', N'Kinh doanh', N'Tìm khách hàng'),
('NV004', 'DA003', N'Lập trình viên', N'Phát triển ứng dụng');

-- Chấm công
INSERT INTO tblChamCong (MaChamCong, MaNV, Ngay, GioVao, GioVe, Ghichu)
VALUES
('CC001', 'NV001', '2025-08-01', '08:00:00', '17:00:00', N'Đi làm đúng giờ'),
('CC002', 'NV002', '2025-08-01', '08:15:00', '17:30:00', N'Đi muộn 15 phút'),
('CC003', 'NV003', '2025-08-01', '08:00:00', '17:00:00', N'Làm việc đầy đủ'),
('CC004', 'NV004', '2025-08-01', '08:30:00', '17:00:00', N'Đi muộn 30 phút');

-- Tài khoản
INSERT INTO tblTaiKhoan (MaTK, MaNV, TenDangNhap, MatKhau, Quyen, Ghichu)
VALUES
('TK001', 'NV001', 'admin', '123456', N'Admin', N'Tài khoản quản trị'),
('TK002', 'NV002', 'thib', '123456', N'Manager', N'Tài khoản trưởng phòng'),
('TK003', 'NV003', 'vanc', '123456', N'User', N'Tài khoản nhân viên KD'),
('TK004', 'NV004', 'a', '1', N'Admin', N'Tài khoản nhân viên KT'),
('TK005', 'NV005', 'k', '1', N'Admin', N'Tài khoản nhân viên KT'),
('TK006', 'NV006', 't', '1', N'Admin', N'Tài khoản nhân viên KT'),
('TK007', 'NV007', 'a', '1', N'Admin', N'Tài khoản nhân viên KT');



