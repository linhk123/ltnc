using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace demo_1.DAL.Entity
{
    public class ChiTietHoaDon
    {
        [Key, Column(Order = 0)]
        public string ma_hoa_don { get; set; } = string.Empty;

        [Key, Column(Order = 1)]
        public string ma_sach { get; set; } = string.Empty;

        public int so_luong { get; set; }

        [ForeignKey("ma_hoa_don")]
        public virtual HoaDon? HoaDon { get; set; }

        [ForeignKey("ma_sach")]
        public virtual Sach? Sach { get; set; }
        public decimal gia_ban_luc_do { get; set; } // Giá bán thực tế
        public decimal gia_nhap_luc_do { get; set; } // Để tính lãi (Lợi nhuận = gia_ban - gia_nhap)
    }
}