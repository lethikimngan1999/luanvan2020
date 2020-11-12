using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.User;
using quanlybenh.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using static quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Controllers
{

    [RoutePrefix("api/UserRole")]

    public class UserRoleController : ApiController
    {
        private IUserRoleService _userRoleService;
        private IRoleService _roleService;


        public UserRoleController(IUserRoleService userRoleService, IRoleService roleService)
        {
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        [HttpGet]
        [Route("GetRolesOfUser")]
        public async Task<BaseResponse<List<RoleDTO>>> GetRolesOfUser(string userId)
        {
            try
            {
                List<UserRoleDTO> lstUserRoles = _userRoleService.GetListByUserId(userId);
                List<RoleDTO> userRoleModel = new List<RoleDTO>();

                foreach (var userRole in lstUserRoles)
                {
                    //role ID của Framework
                    var role = _roleService.GetById(userRole.RoleId.ToString());
                    userRoleModel.Add(role);
                }

                return await Task.FromResult(new BaseResponse<List<RoleDTO>>(userRoleModel, true)).ConfigureAwait(false);
            }
            catch
            {
                return await Task.FromResult(new BaseResponse<List<RoleDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<UserRoleDTO>>> GetAll()
        {
            try
            {
                var result = _userRoleService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<UserRoleDTO>>(result, true)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<List<UserRoleDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch
            {
                return await Task.FromResult(new BaseResponse<List<UserRoleDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetListByUserId")]

        public async Task<BaseResponse<List<UserRoleDTO>>> GetListByUserId(string userId)
        {
            try
            {
                var result = _userRoleService.GetListByUserId(userId);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<UserRoleDTO>>(result, true)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<List<UserRoleDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch
            {
                return await Task.FromResult(new BaseResponse<List<UserRoleDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetByRoleId")]

        public async Task<BaseResponse<RoleDTO>> GetByRoleId(string roleId)
        {
            try
            {
                var result = _userRoleService.GetByRoleId(roleId);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<RoleDTO>(result, true)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<RoleDTO>(Message.RoleNotExistsWithUserID, false)).ConfigureAwait(false);
            }
            catch
            {
                return await Task.FromResult(new BaseResponse<RoleDTO>(Message.RoleNotExistsWithUserID, false)).ConfigureAwait(false);
            }
        }

        [HttpPost]
        public async Task<BaseResponse> CreateUserRole(List<UserRoleDTO> entity)
        {
            try
            {
                var result = _userRoleService.Add(entity);

                if (result)
                {
                    return await Task.FromResult(new BaseResponse<UserRoleDTO>(result)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<UserRoleDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                return await Task.FromResult(new BaseResponse<UserRoleDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
        }
    }

}

