using quanlybenh.Services.DTO.Benh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface IThuocService
    {
        List<ThuocDTO> GetAll();

        bool CheckExistsTenThuoc(string ten);

        bool Create(ThuocDTO thuocDto);
        bool Update(ThuocDTO thuocDto);
        bool Delete(string mathuoc);
        ThuocDTO GetById(string mathuoc);
    }
}
