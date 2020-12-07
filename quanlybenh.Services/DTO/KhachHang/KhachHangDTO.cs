using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.KhachHang
{
    public class KhachHangDTO
    {
        public Guid MaKhachHang { get; set; }

        public string HoLot { get; set; }

    
        public string TenKhachhang { get; set; }

       
        public string DiaChi { get; set; }

      
        public string TinhTP { get; set; }


        public string Sdt { get; set; }


        public string Email { get; set; }

        public string GhiChu { get; set; }
    }
}
