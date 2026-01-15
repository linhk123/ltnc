using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.BLL.DTO
{
    public class ChiTietPhieuNhapDTO
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public decimal gia_nhap { get; set; } // Dùng GiaNhap thay vì DonGia để phân biệt rõ luồng Nhập/Xuất
        public decimal ThanhTien => SoLuong * gia_nhap;
    }
}
