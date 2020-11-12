using quanlybenh.Services.DTO.HinhAnh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
   public interface IHinhAnhBienTheService
    {
        bool Create(HinhAnhBienTheDTO hinhanhDto);
        bool CheckExists(string tenhinhanh);

        List<HinhAnhBienTheDTO> GetAll();
    }
}
