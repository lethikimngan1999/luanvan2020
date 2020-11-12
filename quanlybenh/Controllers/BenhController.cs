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
    [RoutePrefix("api/Benh")]
    public class BenhController : ApiController
    {



        private IBenhService _benhService;

        public BenhController(
               IBenhService benhService)
        {
            _benhService = benhService;
        }

        [HttpPost]

        public async Task<BaseResponse> Create(BenhDTO benhDto)
        {
            try
            {
                var result = _benhService.Create(benhDto);
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

        [HttpPost]
        [Route("InsertAll")]
        public async Task<BaseResponse> InsertAll(BenhDTO entity)
        {
            try
            {
                var result = _benhService.InsertAll(entity);
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
        [Route("GetAll")]

        public async Task<BaseResponse<List<BenhDTO>>> GetAll()
        {
            try
            {
                var result = _benhService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<BenhDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<BenhDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<BenhDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpGet]
        [Route("GetById")]

        public async Task<BaseResponse<BenhDTO>> GetById(string mabenh)
        {
            try
            {
                var result = _benhService.GetById(mabenh);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<BenhDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<BenhDTO>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<BenhDTO>(Message.GetDataNotSuccess));
            }
        }

        [HttpPut]

        public async Task<BaseResponse> Update(BenhDTO benhDTO)
        {
            try
            {
                var result = _benhService.Update(benhDTO);
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

        public async Task<BaseResponse> Delete(string mabenh)
        {
            try
             {
                var result = _benhService.Delete(mabenh);
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
