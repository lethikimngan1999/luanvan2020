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
    [RoutePrefix("api/ThuocDieuTri")]
    public class ThuocDieuTriController : ApiController
    {
        private IThuocDieuTriService _thuocdieutriService;

        private IThuocService _thuocService;

        public ThuocDieuTriController(IThuocDieuTriService thuocdieutriService, IThuocService thuocService)
        {
            _thuocdieutriService = thuocdieutriService;
            _thuocService = thuocService;
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<ThuocDieuTriBenhDTO>>> GetAll()
        {
            try
            {
                var result = _thuocdieutriService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<ThuocDieuTriBenhDTO>>(result, true)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<List<ThuocDieuTriBenhDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch
            {
                return await Task.FromResult(new BaseResponse<List<ThuocDieuTriBenhDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetThuocOfBenh")]
        public async Task<BaseResponse<List<ThuocDTO>>> GetThuocOfBenh(string mabenh)
        {
            try
            {
                List<ThuocDieuTriBenhDTO> lstThuocdieutris = _thuocdieutriService.GetListByMaBenh(mabenh);
                List<ThuocDTO> thuocdieutriModel = new List<ThuocDTO>();

                foreach (var thuocdieutri in lstThuocdieutris)
                {
                  
                    var thuoc = _thuocService.GetById(thuocdieutri.MaThuoc.ToString());
                    thuocdieutriModel.Add(thuoc);
                }

                return await Task.FromResult(new BaseResponse<List<ThuocDTO>>(thuocdieutriModel, true)).ConfigureAwait(false);
            }
            catch
            {
                return await Task.FromResult(new BaseResponse<List<ThuocDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }


        [HttpPost]
        public async Task<BaseResponse> CreateThuocDieuTri(List<ThuocDieuTriBenhDTO> entity)
        {
            try
            {
                var result = _thuocdieutriService.Add(entity);

                if (result)
                {
                    return await Task.FromResult(new BaseResponse<ThuocDieuTriBenhDTO>(result)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<ThuocDieuTriBenhDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                return await Task.FromResult(new BaseResponse<ThuocDieuTriBenhDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPost]
        [Route("CreateDieuTriBenh")]
        public async Task<BaseResponse> CreateDieuTriBenh(List<ThuocDieuTriBenhDTO> entity)
        {
            try
            {
                var result = _thuocdieutriService.AddDieuTriBenh(entity);

                if (result)
                {
                    return await Task.FromResult(new BaseResponse<ThuocDieuTriBenhDTO>(result)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<ThuocDieuTriBenhDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                return await Task.FromResult(new BaseResponse<ThuocDieuTriBenhDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
        }
    }
}
