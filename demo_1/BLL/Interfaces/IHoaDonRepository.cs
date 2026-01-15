using demo_1.BLL.DTO;
using demo_1.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.BLL.Interfaces
{
    public interface IHoaDonRepository
    {
        Task<List<HoaDonDTO>> GetHoaDonsAsync();
        Task<bool> SaveHoaDonTransaction(HoaDon hoaDon, List<ChiTietHoaDon> chiTiets);
    }
}
