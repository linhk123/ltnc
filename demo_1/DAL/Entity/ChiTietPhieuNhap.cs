using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace demo_1.DAL.Entity
{
    public class ChiTietPhieuNhap
    {
        [Key, Column(Order = 0)]
        public string ma_phieu_nhap { get; set; } = string.Empty;

        [Key, Column(Order = 1)]
        public string ma_sach { get; set; } = string.Empty;

        public int so_luong { get; set; }

        [ForeignKey("ma_phieu_nhap")]
        public virtual PhieuNhap PhieuNhap { get; set; }

        [ForeignKey("ma_sach")]
        public virtual Sach Sach { get; set; }
        public decimal gia_nhap { get; set; }
    }
}