using quanlybenh.Services.DTO.BienThe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface IGiongService
    {
        List<GiongDTO> GetAll();
    }
}
