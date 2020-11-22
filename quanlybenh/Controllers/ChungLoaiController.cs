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
    [RoutePrefix("api/ChungLoai")]
    public class ChungLoaiController : ApiController
    {
        private IChungLoaiService _chungloaiService;

        public ChungLoaiController(
               IChungLoaiService chungloaiService)
        {
            _chungloaiService = chungloaiService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<BaseResponse<List<ChungLoaiDTO>>> GetAll()
        {
            try
            {
                var result = _chungloaiService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<ChungLoaiDTO>>(result));
                }
                return await Task.FromResult(new BaseResponse<List<ChungLoaiDTO>>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<List<ChungLoaiDTO>>(Message.GetDataNotSuccess));
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<BaseResponse<ChungLoaiDTO>> GetById(string machungloai)
        {
            try
            {
                var result = _chungloaiService.GetById(machungloai);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<ChungLoaiDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<ChungLoaiDTO>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<ChungLoaiDTO>(Message.GetDataNotSuccess));
            }
        }

        [HttpPost]
        public async Task<BaseResponse> Create(ChungLoaiDTO chungloaiDto)
        {
            try
            {
                var result = _chungloaiService.Create(chungloaiDto);
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
        public async Task<BaseResponse> Update(ChungLoaiDTO chungloaiDto)
        {
            try
            {
                var result = _chungloaiService.Update(chungloaiDto);
                if (result)
                {
                    return await Task.FromResult(new BaseResponse(result));
                }
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse(Message.UpdateNotSuccess));
            }
        }

    }
}
