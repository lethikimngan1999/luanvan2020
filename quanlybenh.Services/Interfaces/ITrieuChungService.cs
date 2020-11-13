using quanlybenh.Services.DTO.Benh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
   public interface ITrieuChungService
    {
        List<TrieuChungDTO> GetAll();
        bool Update(TrieuChungDTO trieuchungDto);
        TrieuChungDTO GetByMaTrieuChung(string matrieuchung);

        bool Create(TrieuChungDTO trieuchungDto);
        bool CheckExistsTenTrieuChung(string tentrieuchung);
        bool Delete(string matrieuchung);
    }
}
