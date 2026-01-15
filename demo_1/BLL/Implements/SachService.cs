using demo_1.BLL.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.Entity;
using demo_1.DAO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_1.BLL.Implements
{
    public class SachService : ISachRepository
    {
        private readonly ISachRepository repo = new SachRepository();

        public async Task<List<SachDTO>> LayDanhSachSachAsync()
        {
            return await repo.GetSachesAsync();
        }

        public async Task<List<SachDTO>> GetSachesAsync()
        {
            return await repo.GetSachesAsync();
        }

        public async Task<bool> Add(SachDTO sach)
        {
            return await repo.Add(sach);
        }

        public async Task<bool> Update(SachDTO sach)
        {
            return await repo.Update(sach);
        }

        public async Task<bool> Delete(string id)
        {
            return await repo.Delete(id);
        }

        public async Task<bool> SaveHoaDonTransaction(HoaDon hoaDon, List<ChiTietHoaDon> chiTiets)
        {
            return await repo.SaveHoaDonTransaction(hoaDon, chiTiets);
        }

        public async Task<List<SachDTO>> Search(string keyword)
        {
            return await repo.Search(keyword);
        }
    }
}