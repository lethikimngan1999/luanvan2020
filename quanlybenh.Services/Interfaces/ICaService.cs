using quanlybenh.Services.DTO.Ca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
   public interface ICaService
    {
        List<CaDTO> GetAll();
    }
}
