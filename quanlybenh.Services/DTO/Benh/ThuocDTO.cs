using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Benh
{
    public class ThuocDTO
    {
        public Guid MaThuoc { get; set; }

    
        public string TenThuoc { get; set; }

        public string CongDung { get; set; }

        public string CachDung { get; set; }

        public string LuuY { get; set; }
        public string HinhAnh { get; set; }
        public List<BenhDTO> ListBenhs { get; set; }
        public IEnumerable<string> MaBenhs { get; set; }

        public List<LieuTrinhDTO> ListLieuTrinhs { get; set; }
        public IEnumerable<string> MaLieuTrinhs { get; set; }

    }
}
