using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.NhanVien;
using quanlybenh.Services.DTO.TaiKhoanKhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.User
{
    public class UserDTO : BaseTableDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string PasswordHash { get; set; }

        public Guid MaNhanVien { get; set; }
        public Guid MaKhachHang { get; set; }
        public string Status { get; set; }
        public virtual NhanVienDTO Nhanvien { get; set; }
        public virtual KhachHangDTO Khachhang { get; set; }
        public List<RoleDTO> ListRoles { get; set; }
        public IEnumerable<string> RoleIds { get; set; }
    }

}
