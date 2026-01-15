using demo_1.BLL.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.DAO;
using demo_1.DAL.Entity;
using demo_1.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_1.BLL.Implements
{
    public class LoaiSachService : ILoaiSachRepository
    {
        // Khởi tạo đối tượng DAO để tương tác trực tiếp với Database
        private readonly LoaiSachRepository repo = new LoaiSachRepository();

        // Phương thức riêng cho Service để UI gọi
        public async Task<List<LoaiSachDTO>> LayDanhSachLoaiSachAsync()
        {
            return await repo.GetLoaiSachsAsync();
        }

        // Thực thi các phương thức từ Interface
        public async Task<List<LoaiSachDTO>> GetLoaiSachsAsync()
        {
            return await repo.GetLoaiSachsAsync();
        }

        // Implementing ILoaiSachRepository.GetAllAsync
        public async Task<List<LoaiSach>> GetAllAsync()
        {
            var dtos = await repo.GetLoaiSachsAsync();
            var result = new List<LoaiSach>();
            foreach (var dto in dtos)
            {
                result.Add(new LoaiSach
                {
                    // Assign properties here
                    // Example:
                    // Id = dto.Id,
                    // Name = dto.Name
                });
            }
            return result;
        }

        // Implement interface method AddAsync
        public async Task<bool> AddAsync(LoaiSach loai)
        {
            return await repo.Add(loai);
        }

        public async Task<bool> Add(LoaiSach loai)
        {
            return await repo.Add(loai);
        }

        // Implement interface method UpdateAsync
        public async Task<bool> UpdateAsync(LoaiSach loai)
        {
            return await repo.Update(loai);
        }

        // Implement interface method DeleteAsync
        public async Task<bool> DeleteAsync(string maLoai)
        {
            return await repo.Delete(maLoai);
        }
        public async Task<List<LoaiSachDTO>> Search(string keyword)
        {
            return await repo.Search(keyword);
        }
    }
}