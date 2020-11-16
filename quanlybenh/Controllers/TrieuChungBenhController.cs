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
    [RoutePrefix("api/TrieuChungBenh")]
    public class TrieuChungBenhController : ApiController
    {
        private ITrieuChungBenhService _trieuchungbenhService;

        private ITrieuChungService _trieuchungService;

        public TrieuChungBenhController(ITrieuChungBenhService trieuchungbenhService, ITrieuChungService trieuchungService)
        {
            _trieuchungbenhService = trieuchungbenhService;
            _trieuchungService = trieuchungService;
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<BaseResponse<List<TrieuChungBenhDTO>>> GetAll()
        {
            try
            {
                var result = _trieuchungbenhService.GetAll();
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<TrieuChungBenhDTO>>(result, true)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<List<TrieuChungBenhDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch
            {
                return await Task.FromResult(new BaseResponse<List<TrieuChungBenhDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("GetAllTrieuChung")]

        public async Task<BaseResponse<List<TrieuChungBenhDTO>>> GetAllTrieuChung(string matrieuchung)
        {
            try
            {
                var result = _trieuchungbenhService.GetAllTRieuChungBenhByType(matrieuchung);
                if (result != null)
                {
                    return await Task.FromResult(new BaseResponse<List<TrieuChungBenhDTO>>(result, true)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<List<TrieuChungBenhDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
            catch
            {
                return await Task.FromResult(new BaseResponse<List<TrieuChungBenhDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
            }
        }

        //[HttpGet]
        //[Route("GetTrieuChungOfBenh")]
        //public async Task<BaseResponse<List<ThuocDTO>>> GetTrieuChungOfBenh(string mabenh)
        //{
        //    try
        //    {
        //        List<TrieuChungBenhDTO> lstTrieuchungbenhs = _trieuchungbenhService.GetListByMaBenh(mabenh);
        //        List<TrieuChungDTO> trieuchungbenhModel = new List<TrieuChungDTO>();

        //        foreach (var trieuchungbenh in lstTrieuchungbenhs)
        //        {

        //            var trieuchung = _trieuchungService.GetByMaTrieuChung(trieuchungbenh.MaTrieuChung.ToString());
        //            trieuchungbenhModel.Add(trieuchung);
        //        }

        //        return await Task.FromResult(new BaseResponse<List<TrieuChungDTO>>(trieuchungbenhModel, true)).ConfigureAwait(false);
        //    }
        //    catch
        //    {
        //        return await Task.FromResult(new BaseResponse<List<TrieuChungDTO>>(Message.GetDataNotSuccess, false)).ConfigureAwait(false);
        //    }
        //}


        [HttpPost]
        public async Task<BaseResponse> Create(List<TrieuChungBenhDTO> entity)
        {
            try
            {
                var result = _trieuchungbenhService.Add(entity);

                if (result)
                {
                    return await Task.FromResult(new BaseResponse<TrieuChungBenhDTO>(result)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<TrieuChungBenhDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                return await Task.FromResult(new BaseResponse<TrieuChungBenhDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
        }

        [HttpPost]
        [Route("CreateTrieuChungBenh")]
        public async Task<BaseResponse> CreateTrieuChungBenh(List<TrieuChungBenhDTO> entity)
        {
            try
            {
                var result = _trieuchungbenhService.AddTrieuChungBenh(entity);

                if (result)
                {
                    return await Task.FromResult(new BaseResponse<TrieuChungBenhDTO>(result)).ConfigureAwait(false);
                }

                return await Task.FromResult(new BaseResponse<TrieuChungBenhDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                return await Task.FromResult(new BaseResponse<TrieuChungBenhDTO>(Message.CreateNotSuccess, false)).ConfigureAwait(false);
            }
        }
    }
}

