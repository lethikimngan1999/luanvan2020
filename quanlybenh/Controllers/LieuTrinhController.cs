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
    [RoutePrefix("api/LieuTrinh")]
    public class LieuTrinhController : ApiController
    {
        private ILieuTrinhService _lieutrinhService;

        public LieuTrinhController(
               ILieuTrinhService lieutrinhService)
        {
            _lieutrinhService = lieutrinhService;
        }



        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<LieuTrinhDTO>>> GetAll()
        {
            try
            {
                var result = _lieutrinhService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<LieuTrinhDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<LieuTrinhDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<LieuTrinhDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpPost]

        public async Task<BaseResponse> Create(LieuTrinhDTO lieutrinhDto)
        {
            try
            {
                var result = _lieutrinhService.Create(lieutrinhDto);
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
        public async Task<BaseResponse> Update(LieuTrinhDTO lieutrinhDTO)
        {
            try
            {
                var result = _lieutrinhService.Update(lieutrinhDTO);
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


        [HttpGet]
        [Route("GetById")]

        public async Task<BaseResponse<LieuTrinhDTO>> GetById(string malieutrinh)
        {
            try
            {
                var result = _lieutrinhService.GetByMaLieuTrinh(malieutrinh);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<LieuTrinhDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<LieuTrinhDTO>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<LieuTrinhDTO>(Message.GetDataNotSuccess));
            }
        }

        [HttpDelete]

        public async Task<BaseResponse> Delete(string malieutrinh)
        {
            try
            {
                var result = _lieutrinhService.Delete(malieutrinh);
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
