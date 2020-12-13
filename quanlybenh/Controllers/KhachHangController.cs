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
    [RoutePrefix("api/KhachHang")]
    public class KhachHangController : ApiController
    {
        private IKhachHangService _khachhangService;

        public KhachHangController(
               IKhachHangService khachhangService)
        {
            _khachhangService = khachhangService;
        }

        [HttpGet]
        [Route("GetById")]

        public async Task<BaseResponse<KhachHangDTO>> GetById(string makhachhang)
        {
            try
            {
                var result = _khachhangService.GetById(makhachhang);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<KhachHangDTO>(result));
                }
                return await Task.FromResult(new BaseResponse<KhachHangDTO>(Message.GetDataNotSuccess));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new BaseResponse<KhachHangDTO>(Message.GetDataNotSuccess));
            }
        }
    }
}
