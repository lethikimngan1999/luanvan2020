using quanlybenh.Services.DTO.BienThe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface IChungLoaiService
    {
        List<ChungLoaiDTO> GetAll();

        ChungLoaiDTO GetById(string machungloai);

        bool Create(ChungLoaiDTO chungloaiDto);
        bool CheckExistsTenChungLoai(string tenchungloai);

        bool Update(ChungLoaiDTO chungloaiDto);

        bool Delete(string machungloai);
    }
}
