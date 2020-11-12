using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.Ca;
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
                return await Task.FromResult(new BaseResponse<List<CaDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<CaDTO>>(Message.GetDataNotSuccess));
            }
        }
    }
}
