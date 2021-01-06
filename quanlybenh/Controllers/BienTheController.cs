using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.BienThe;
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
    [RoutePrefix("api/BienThe")]
    public class BienTheController : ApiController
    {
        private IBienTheService _bientheService;

        public BienTheController(
               IBienTheService bientheService)
        {
            _bientheService = bientheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<BaseResponse<List<BienTheDTO>>> GetAll()
        {
            try
            {
                var result = _bientheService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpGet]
        [Route("GetById")]

        public async Task<BaseResponse<BienTheDTO>> GetById(string mabienthe)
        {
            try
            {
                var result = _bientheService.GetById(mabienthe);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<BienTheDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<BienTheDTO>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<BienTheDTO>(Message.GetDataNotSuccess));
            }
        }

        [HttpGet]
        [Route("GetListAll")]
        public async Task<BaseResponse<List<BienTheDTO>>> GetListAll()
        {
            try
            {
                var result = _bientheService.GetListAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpGet]
        [Route("GetListOfChungLoai")]
        public async Task<BaseResponse<List<BienTheDTO>>> GetListOfChungLoai(string machungloai)
        {
            try
            {
                var result = _bientheService.GetListOfChungLoai(machungloai);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<BienTheDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpPost]
 
        public async Task<BaseResponse> InsertAll(BienTheDTO entity)
        {
            try
            {
                var result = _bientheService.Create(entity);
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
        public async Task<BaseResponse> Update(BienTheDTO bientheDTO)
        {
            try
            {
                var result = _bientheService.Update(bientheDTO);
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

        public async Task<BaseResponse> Delete(string mabienthe)
        {
            try
            {
                var result = _bientheService.Delete(mabienthe);
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
