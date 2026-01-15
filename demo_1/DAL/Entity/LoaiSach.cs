using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo_1.DAL.Entity
{
    public class LoaiSach
    {
        [Key]
        public string MaLoaiSach { get; set; }
        [Required, StringLength(100)]
        public string TenLoaiSach { get; set; }

        public virtual ICollection<Sach> Sachs { get; set; }

    }
}
