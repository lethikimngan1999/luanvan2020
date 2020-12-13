using System;
using System.Collections.Generic;


namespace quanlybenh.Services.DTO.TaiKhoanKhachHang
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

        public List<TheoDoiThongTinDTO> ListThongTins { get; set; }
        public IEnumerable<string> MaThongTins { get; set; }
    }
}
