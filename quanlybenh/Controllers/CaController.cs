using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.Ca;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using static quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Controllers
{
    [RoutePrefix("api/Ca")]
    public class CaController : ApiController
    {
        private ICaService _caService;

        public CaController(
               ICaService caService)
        {
            _caService = caService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<BaseResponse<List<CaDTO>>> GetAll()
        {
            try
            {
                var result = _caService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<CaDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<CaDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<CaDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPost]
        public async Task<BaseResponse> InsertAll(CaDTO entity)
        {
            try
            {
                var result = _caService.Create(entity);
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
        public async Task<BaseResponse> Update(CaDTO caDTO)
        {
            try
            {
                var result = _caService.Update(caDTO);
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

    }
}
