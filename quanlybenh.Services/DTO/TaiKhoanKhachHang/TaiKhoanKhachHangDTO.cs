using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.NhanVien;
using quanlybenh.Services.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.TaiKhoanKhachHang
{
   public class TaiKhoanKhachHangDTO : BaseTableDTO
    {
        //khachhang
        public Guid MaKhachHang { get; set; }

        public string HoLot { get; set; }

        public string TenKhachhang { get; set; }

   
        public string DiaChi { get; set; }

   
        public string TinhTP { get; set; }

        public string Sdt { get; set; }

        public string Email { get; set; }

        public string GhiChu { get; set; }
        //end

        // tai khoan

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string PasswordHash { get; set; }

        public Guid? MaNhanVien { get; set; }
      
        public string Status { get; set; }
        public virtual NhanVienDTO Nhanvien { get; set; }

        public List<RoleDTO> ListRoles { get; set; }
        public IEnumerable<string> RoleIds { get; set; }
    }
}
