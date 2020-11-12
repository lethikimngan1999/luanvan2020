using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.NhanVien
{
    public class NhanVienDTO
    {
        public Guid MaNhanVien { get; set; }

     
        public string HoLot { get; set; }

        public string TenNhanVien { get; set; }

        public DateTime? NgaySinh { get; set; }

        public bool? GioiTinh { get; set; }


        public string CMND { get; set; }

 
        public string Sdt { get; set; }

        public string DiaChi { get; set; }

        public string Email { get; set; }
    }
}
