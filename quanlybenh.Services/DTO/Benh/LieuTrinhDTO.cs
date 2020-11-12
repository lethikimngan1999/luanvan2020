using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Benh
{
    public class LieuTrinhDTO
    {
        public Guid MaLieuTrinh { get; set; }

        public Guid MaThuoc { get; set; }

  
        public string TenLieuTrinh { get; set; }

        public string MoTaLieuTrinh { get; set; }

        public ThuocDTO Thuoc { get; set; }
    }
}
