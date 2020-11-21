using quanlybenh.Services.DTO.Benh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
   public interface ITrieuChungBenhService
    {
        // get danh sach triệu chứng theo benh
        List<TrieuChungBenhDTO> GetListByMaBenh(string mabenh);
        List<TrieuChungBenhDTO> GetAll();

        bool Add(List<TrieuChungBenhDTO> trieuchungbenhDtos);

        bool AddTrieuChungBenh(List<TrieuChungBenhDTO> trieuchungbenhDtos);
        List<TrieuChungBenhDTO> GetAllTRieuChungBenhByType(List<SearchDTO> searchString);
     //   List<TrieuChungBenhDTO> GetAllTrieuChungActive(Guid matrieuchung);
        //  List<TrieuChungBenhDTO> GetAllBenhByType(List<string> matrieuchung);
    }
}
