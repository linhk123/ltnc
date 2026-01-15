using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.BLL.DTO
{
    public class PhieuNhapDTO
    {
        public required string MaPhieuNhap { get; set; }
        public DateTime NgayLap { get; set; }
        public required string TenNhaCungCap { get; set; }
        public decimal TongTienNhap { get; set; } // Tính toán từ danh sách chi tiết
    }
}