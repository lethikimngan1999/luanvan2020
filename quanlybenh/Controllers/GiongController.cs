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
    [RoutePrefix("api/Giong")]
    public class GiongController : ApiController
    {
        private IGiongService _giongService;

        public GiongController(
               IGiongService giongService)
        {
            _giongService = giongService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<BaseResponse<List<GiongDTO>>> GetAll()
        {
            try
            {
                var result = _giongService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<GiongDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<GiongDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<GiongDTO>>(Message.GetDataNotSuccess));
            }
        }
    }
}
