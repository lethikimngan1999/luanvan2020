using quanlybenh.Services.DTO.BienThe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface IBienTheService
    {
        List<BienTheDTO> GetAll();
        List<BienTheDTO> GetListAll();
        bool Create(BienTheDTO bientheDto);
        bool Update(BienTheDTO benhDto);
        bool CheckExistsTen(string ten);
        BienTheDTO GetById(string mabienthe);
        List<BienTheDTO> GetListOfChungLoai(string machungloai);
    }
}
