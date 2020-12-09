using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.TaiKhoanKhachHang;
using quanlybenh.Services.DTO.User;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private IUserService _userService;

        public UserController(
               IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<UserDTO>>> GetAll()
        {
            try
            {
                var result = _userService.GetAllUser();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<UserDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<UserDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<UserDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<BaseResponse<UserDTO>> GetById(string userId)
        {
            try
            {
                var result = _userService.GetById(userId);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<UserDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<UserDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<UserDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetByMaNhanVien")]

        public async Task<BaseResponse<UserDTO>> GetByMaNhanVien(string manhanvien)
        {
            try
            {
                var result = _userService.GetByMaNhanVien(manhanvien);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<UserDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<UserDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<UserDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetByUserName")]

        public async Task<BaseResponse<UserDTO>> GetByUserName(string userName)
        {
            try
            {
                var result = _userService.GetByUserName(userName);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<UserDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<UserDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<UserDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetAllUserAccountActive")]
        public async Task<BaseResponse<List<UserDTO>>> GetAllUserAccountActive()
        {
            try
            {
                var result = _userService.GetAllUserAccountActive();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<UserDTO>>(result));

                }
                return await Task.FromResult(new BaseResponse<List<UserDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BaseResponse<List<UserDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetAllUserAccountLocked")]
        public async Task<BaseResponse<List<UserDTO>>> GetAllUserAccountLocked()
        {
            try
            {
                var result = _userService.GetAllUserAccountLocked();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<UserDTO>>(result));

                }
                return await Task.FromResult(new BaseResponse<List<UserDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BaseResponse<List<UserDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPost]

        public async Task<BaseResponse> CreateNhanVienAccount(UserDTO userDTO)
        {
            try
            {
                var result = _userService.CreateNhanVienAccount(userDTO);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPost]
        [Route("CreateKhachHangAccount")]
        public async Task<BaseResponse> CreateKhachHangAccount(TaiKhoanKhachHangDTO userDTO)
        {
            try
            {
                var result = _userService.CreateKhachHangAccount(userDTO);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
        }


        [HttpPut]

        public async Task<BaseResponse> UpdateNhanVienAccount(UserDTO userDTO)
        {
            try
            {
                var result = _userService.Update(userDTO);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess,false)).ConfigureAwait(false);
            }
        }

        [HttpPut]
        [Route("UpdateStatusAccount")]
        public async Task<BaseResponse> UpdateStatusAccount(UserDTO userDTO)
        {
            try
            {
                var result = _userService.UpdateStatus(userDTO.Id.ToString(), userDTO.Status);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));

                }
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpDelete]

        public async Task<BaseResponse> DeleteNhanVienAccount(string userId)
        {
            try
            {
                var result = _userService.Delete(userId);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.DeleteNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse(Message.DeleteNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPut]
        [Route("ResetPassword")]
        public async Task<BaseResponse> ResetPassword(UserDTO userDto)
        {
            try
            {
                var result = _userService.ResetPassword(userDto.Id.ToString());
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result)).ConfigureAwait(false);
                }
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<BaseResponse> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var result = await _userService.ChangePassword(changePasswordDTO.UserId, changePasswordDTO.NewPassword, changePasswordDTO.OldPassword).ConfigureAwait(false);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result)).ConfigureAwait(false);
                }
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
        }

    }
}

