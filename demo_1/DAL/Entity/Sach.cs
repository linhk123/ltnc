using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace demo_1.DAL.Entity
{
    public class Sach
    {
        [Key]
        public string MaSach { get; set; }

        [Required]
        public string TenSach { get; set; }

        public string TacGia { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }

        // Khóa ngoại liên kết tới LoaiSach
        public string MaLoaiSach { get; set; }
        [ForeignKey("MaLoaiSach")]
        public virtual LoaiSach LoaiSach { get; set; }
    }
}
