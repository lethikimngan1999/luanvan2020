using quanlybenh.Services.DTO.Benh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
  public interface IBenhService
    {
        List<BenhDTO> GetAll();
        bool CheckExistsTenbenh(string ten);
        bool Create(BenhDTO benhDto);

        bool Update(BenhDTO benhDto);
        BenhDTO GetById(string mabenh);
        bool Delete(string mabenh);
        bool InsertAll(BenhDTO userRoleDataPopups);
    }
}
