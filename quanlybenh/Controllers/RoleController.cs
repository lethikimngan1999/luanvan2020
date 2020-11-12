using quanlybenh.DataModels.Repositories;
using quanlybenh.Services.DTO.Base;
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
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        private IRoleService _roleService;
        public RoleController(IRoleService roleService, ApplicationRoleManager roleManager)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<RoleDTO>>> GetAll()
        {
            try
            {
                var result = _roleService.GetAll().ToList();
                if(result.Count() > 0 )
                {
                    return await Task.FromResult(new BaseResponse<List<RoleDTO>>(result, true)).ConfigureAwait(false);
                }
                return await Task.FromResult(new BaseResponse<List<RoleDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<RoleDTO>>(e.Message, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        // [FeatureAuthentication((int)QLKSConstants.Action.CanRead)]
        [Route("GetByRoleId")]

        public async Task<BaseResponse<RoleDTO>> GetByRoleId(string roleId)
        {
            try
            {
                var result = _roleService.GetById(roleId);

                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<RoleDTO>(result, true)).ConfigureAwait(false);
                }
                return await Task.FromResult(new BaseResponse<RoleDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<RoleDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        //[HttpGet]
        //[Route("GetByRoleName")]

        //public async Task<BaseResponse<RoleDTO>> GetByRoleName(string rolename)
        //{
        //    try
        //    {
        //        var result = _roleService.GetByRoleName(rolename);
        //        if (result != null)
        //        {
        //            return await Task.FromResult(new BaseResponse<RoleDTO>(result));
        //        }
        //        return await Task.FromResult(new BaseResponse<RoleDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Task.FromResult(new BaseResponse<RoleDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
        //    }
        //}

        [HttpGet]
        [Route("CheckExistsRoleName")]
        public async Task<BaseResponse<RoleDTO>> CheckExistsRoleName(string roleName)
        {
            try
            {
                var result = _roleService.CheckExistsRoleName(roleName);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse<RoleDTO>(result)).ConfigureAwait(false);
                }
                return await Task.FromResult(new BaseResponse<RoleDTO>(Message.RoleNameNotExists, false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse<RoleDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPost]
        public async Task<BaseResponse> CreateRole(RoleDTO entity)
        {
            try
            {
                var result = _roleService.Create(entity);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result)).ConfigureAwait(false);
                }
                return await Task.FromResult(new BaseResponse(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPut]
        public async Task<BaseResponse> UpdateRole(RoleDTO entity)
        {
            try
            {
                var result = _roleService.Update(entity);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result)).ConfigureAwait(false);
                }
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpDelete]
        public async Task<BaseResponse> Delete(string roleId)
        {
            try
            {
                var result = _roleService.Delete(new Guid(roleId));
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
    }
}
