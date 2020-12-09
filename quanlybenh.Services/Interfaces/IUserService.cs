using quanlybenh.Services.DTO.TaiKhoanKhachHang;
using quanlybenh.Services.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO GetById(string id);
        UserDTO GetByMaNhanVien(string manhanvien);
        UserDTO GetByUserName(string username);

        bool CreateNhanVienAccount(UserDTO registerUserDto);
        bool CreateKhachHangAccount(TaiKhoanKhachHangDTO registerUserDto);
        bool Update(UserDTO userDto);

        bool Delete(string userId);

        bool IsExistedUserById(string userId);

        bool IsExistedUserByMaNhanVien(string manhanvien);

        bool IsExistedUserName(string UserName);

        bool UpdateStatus(string userId, string status);
        List<UserDTO> GetAllUser();

        List<UserDTO> GetAllUserAccountActive();
        List<UserDTO> GetAllUserAccountLocked();

        Task<bool> ChangePassword(string userId, string newPassword, string oldPassword);

        Task<bool> CheckPassword(string UserName, string Password);

        bool ResetPassword(string userId);
    }

}
