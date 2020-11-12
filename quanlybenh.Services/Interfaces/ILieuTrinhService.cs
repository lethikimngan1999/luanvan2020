using quanlybenh.Services.DTO.Benh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface ILieuTrinhService
    {
        List<LieuTrinhDTO> GetAll();

        LieuTrinhDTO GetByMaLieuTrinh(string malieutrinh);
        bool Update(LieuTrinhDTO lieutrinhDto);
        bool Create(LieuTrinhDTO lieutrinhDto);
        bool CheckExistsTenLieuTrinh(string tenlieutrinh);
        bool Delete(string malieutrinh);
    }
}
