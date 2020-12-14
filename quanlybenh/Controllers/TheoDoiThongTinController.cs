using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.TaiKhoanKhachHang;
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
    [RoutePrefix("api/TheoDoiThongTin")]
    public class TheoDoiThongTinController : ApiController
    {
        private ITheoDoiThongTinService _thongtinService;

        public TheoDoiThongTinController(
               ITheoDoiThongTinService thongtinService)
        {
            _thongtinService = thongtinService;
        }

        [HttpPost]

        public async Task<BaseResponse> Create(TheoDoiThongTinDTO thongtinDto)
        {
            try
            {
                var result = _thongtinService.Create(thongtinDto);
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

        public async Task<BaseResponse> Update(TheoDoiThongTinDTO thongtinDto)
        {
            try
            {
                var result = _thongtinService.Update(thongtinDto);
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


        [HttpDelete]

        public async Task<BaseResponse> Delete(string mathongtin)
        {
            try
            {
                var result = _thongtinService.Delete(mathongtin);
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
