using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.BLL.DTO
{
    public class SachDTO
    {
        public string MaSach { get; set; } = string.Empty;
        public string TenSach { get; set; } = string.Empty;
        public string TacGia { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public string TenLoaiSach { get; set; } = string.Empty;
        // Added: carry the foreign key value for category
        public string MaLoaiSach { get; set; } = string.Empty;
    }
}