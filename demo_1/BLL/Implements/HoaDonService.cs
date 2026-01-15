using demo_1.BLL.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.DAO;
using demo_1.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace demo_1.BLL.Implements
{
    internal class HoaDonService : IHoaDonRepository
    {
            private readonly HoaDonRepository repo = new HoaDonRepository();

            public async Task<List<HoaDonDTO>> GetHoaDonsAsync()
            {
                return await repo.GetHoaDonsAsync();
            }

            public async Task<bool> SaveHoaDonTransaction(HoaDon hoaDon, List<ChiTietHoaDon> chiTiets)
            {
                return await repo.SaveHoaDonTransaction(hoaDon, chiTiets);
            }
        }
    }
