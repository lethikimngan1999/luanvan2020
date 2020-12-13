using quanlybenh.Services.DTO.TaiKhoanKhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
   public interface IKhachHangService
    {
        KhachHangDTO GetById(string makhachhang);
    }
}
