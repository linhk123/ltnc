using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo_1.DAL.Entity
{
    public class HoaDon
    {
        [Key]
        public string ma_hoa_don { get; set; }
        public DateTime ngay_lap_hoa_don { get; set; }
        public string ten_khach_hang { get; set; }
        public string sdt_khach_hang { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
