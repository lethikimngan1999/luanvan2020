using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.Benh;
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
    [RoutePrefix("api/Thuoc")]
    public class ThuocController : ApiController
    {
        private IThuocService  _thuocService;

        public ThuocController(
               IThuocService thuocService)
        {
            _thuocService = thuocService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<BaseResponse<List<ThuocDTO>>> GetAll()
        {
            try
            {
                var result = _thuocService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<ThuocDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<ThuocDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<ThuocDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpPost]
        public async Task<BaseResponse> Create(ThuocDTO thuocDto)
        {
            try
            {
                var result = _thuocService.Create(thuocDto);
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

        [HttpGet]
        [Route("GetById")]
        public async Task<BaseResponse<ThuocDTO>> GetById(string mathuoc)
        {
            try
            {
                var result = _thuocService.GetById(mathuoc);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<ThuocDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<ThuocDTO>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<ThuocDTO>(Message.GetDataNotSuccess));
            }
        }

        [HttpPut]
        public async Task<BaseResponse> Update(ThuocDTO thuocDTO)
        {
            try
            {
                var result = _thuocService.Update(thuocDTO);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpDelete]
        public async Task<BaseResponse> Delete(string mathuoc)
        {
            try
            {
                var result = _thuocService.Delete(mathuoc);
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
    }
}
