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
    [RoutePrefix("api/TrieuChung")]
    public class TrieuChungController : ApiController
    {
        private ITrieuChungSevice _TrieuChungService;

        public TrieuChungController(
               ITrieuChungSevice TrieuChungService)
        {
            _TrieuChungService = TrieuChungService;
        }



        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<TrieuChungDTO>>> GetAll()
        {
            try
            {
                var result = _TrieuChungService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<TrieuChungDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<TrieuChungDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<TrieuChungDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpPost]

        public async Task<BaseResponse> Create(TrieuChungDTO trieuchungDto)
        {
            try
            {
                var result = _TrieuChungService.Create(trieuchungDto);
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
        public async Task<BaseResponse> Update(TrieuChungDTO trieuchungDTO)
        {
            try
            {
                var result = _TrieuChungService.Update(trieuchungDTO);
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

        public async Task<BaseResponse<TrieuChungDTO>> GetById(string matrieuchung)
        {
            try
            {
                var result = _TrieuChungService.GetByMaTrieuChung(matrieuchung);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<TrieuChungDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<TrieuChungDTO>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<TrieuChungDTO>(Message.GetDataNotSuccess));
            }
        }

        [HttpDelete]

        public async Task<BaseResponse> Delete(string matrieuchung)
        {
            try
            {
                var result = _TrieuChungService.Delete(matrieuchung);
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
