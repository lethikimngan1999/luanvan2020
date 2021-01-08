

using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.NhanVien;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using static quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Controllers
{
    [RoutePrefix("api/NhanVien")]
    public class NhanVienController : ApiController
    {



        private INhanVienService _NhanVienService;

        public NhanVienController(
               INhanVienService NhanVienService)
        {
            _NhanVienService = NhanVienService;
        }




        [HttpPost]



        public async Task<BaseResponse> Create(NhanVienDTO NhanVienDto)
        {
            try
            {
                var result = _NhanVienService.Create(NhanVienDto);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.CreateNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse(Message.CreateNotSuccess));
            }
        }


        [HttpPut]

        public async Task<BaseResponse> Update(NhanVienDTO NhanVienDto)
        {
            try
            {
                var result = _NhanVienService.Update(NhanVienDto);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess));
            }
        }


        [HttpDelete]

        public async Task<BaseResponse> Delete(string MaNhanVien)
        {
            try
            {
                var result = _NhanVienService.Delete(MaNhanVien);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.DeleteNotSuccess));
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse(Message.DeleteNotSuccess));
            }
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<NhanVienDTO>>> GetAll()
        {
            try
            {
                var result = _NhanVienService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<NhanVienDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<NhanVienDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<NhanVienDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpGet]
        [Route("GetById")]

        public async Task<BaseResponse<NhanVienDTO>> GetById(string MaNhanVien)
        {
            try
            {
                var result = _NhanVienService.GetById(MaNhanVien);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<NhanVienDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<NhanVienDTO>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<NhanVienDTO>(Message.GetDataNotSuccess));
            }
        }


        [HttpGet]
        [Route("GetByCMND")]

        public async Task<BaseResponse<NhanVienDTO>> GetByCMND(string cmnd)
        {
            try
            {
                var result = _NhanVienService.GetByCMND(cmnd);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<NhanVienDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<NhanVienDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<NhanVienDTO>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetEmployeeNotAccount")]

        public async Task<BaseResponse<List<NhanVienDTO>>> GetEmployeeNotAccount()
        {
            try
            {
                var result = _NhanVienService.GetEmployeeNotAccount();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<NhanVienDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<NhanVienDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<NhanVienDTO>>(Message.GetDataNotSuccess));
            }
        }
    }
}
