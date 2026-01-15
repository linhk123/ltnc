using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.BLL.DTO
{
    public class ChiTietHoaDonDTO
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
