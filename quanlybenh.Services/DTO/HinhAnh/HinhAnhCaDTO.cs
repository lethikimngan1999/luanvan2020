using quanlybenh.Services.DTO.Ca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.HinhAnh
{
    public class HinhAnhCaDTO
    {
        public Guid MaHinhAnhCa { get; set; }

        public Guid MaCa { get; set; }

        public string DuongDan { get; set; }

        public bool? ChonAvt { get; set; }

        public DateTime? NgayTao { get; set; }

        public string TenHinhAnh { get; set; }

        public virtual CaDTO Ca { get; set; }
    }
}
