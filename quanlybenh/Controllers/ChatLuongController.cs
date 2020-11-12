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
    [RoutePrefix("api/ChatLuong")]
    public class ChatLuongController : ApiController
    {
        private IChatLuongService _chatluongService;

        public ChatLuongController(
               IChatLuongService chatluongService)
        {
            _chatluongService = chatluongService;
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<ChatLuongDTO>>> GetAll()
        {
            try
            {
                var result = _chatluongService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<ChatLuongDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<ChatLuongDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<ChatLuongDTO>>(Message.GetDataNotSuccess));
            }
        }
    }
}
