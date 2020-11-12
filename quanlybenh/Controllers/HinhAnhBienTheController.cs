using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.HinhAnh;
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
    [RoutePrefix("api/HinhAnhBienThe")]
    public class HinhAnhBienTheController : ApiController
    {
        private IHinhAnhBienTheService _hinhanhService;

        public HinhAnhBienTheController(
               IHinhAnhBienTheService hinhanhService)
        {
            _hinhanhService = hinhanhService;
        }



        //[HttpGet]
        //[Route("GetAll")]

        public async Task<BaseResponse<List<HinhAnhBienTheDTO>>> GetAll()
        {
            try
            {
                var result = _hinhanhService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<HinhAnhBienTheDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<HinhAnhBienTheDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<HinhAnhBienTheDTO>>(Message.GetDataNotSuccess));
            }
        }


        [HttpPost]

        public async Task<BaseResponse> Create(HinhAnhBienTheDTO hinhanhDto)
        {
            try
            {
                var result = _hinhanhService.Create(hinhanhDto);
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
    }
}
