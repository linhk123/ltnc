using System.Collections.Generic;
using System.Threading.Tasks;
using demo_1.DAL.Entity;

namespace demo_1.BLL.Interfaces
{
    public interface ILoaiSachRepository
    {
        Task<List<LoaiSach>> GetAllAsync();
        Task<bool> AddAsync(LoaiSach loai);
        Task<bool> UpdateAsync(LoaiSach loai);
        Task<bool> DeleteAsync(string maLoai);
    }
}