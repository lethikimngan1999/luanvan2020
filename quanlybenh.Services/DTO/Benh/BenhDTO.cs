using quanlybenh.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.Benh
{
    public class BenhDTO 
    {
        public Guid MaBenh { get; set; }

        public string TenBenh { get; set; }

        public string NguyenNhan { get; set; }

        public string CachDieuTri { get; set; }

        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public List<ThuocDTO> ListThuocs { get; set; }
        public IEnumerable<string> MaThuocs { get; set; }

        public List<TrieuChungDTO> ListTrieuChungs { get; set; }
        public IEnumerable<string> MaTrieuChungs { get; set; }



    }
}
