using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo_1.DAL.Entity
{
    public class PhieuNhap
    {
        [Key]
        public string ma_phieu_nhap { get; set; } = string.Empty;
        public DateTime ngay_lap_phieu_nhap { get; set; }
        public string ten_nha_cung_cap { get; set; }
        public decimal tong_tien_nhap { get; set; }
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}
