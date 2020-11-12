using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.BienThe;
using quanlybenh.Services.DTO.Ca;
using quanlybenh.Services.DTO.HinhAnh;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Implementation
{
    public class CaService : BaseService, ICaService
    {

        private IDataRepository<BienThe> _bientheRepository;
        private IDataRepository<Ca> _caRepository;
        private IDataRepository<HinhAnhCa> _hinhanhcaRepository;

        private readonly IMapper _mapper;
        public CaService(
            IDataRepository<BienThe> bientheRepository,
             IDataRepository<Ca> caRepository,
            IDataRepository<HinhAnhCa> hinhanhcaRepository,

           IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _bientheRepository = bientheRepository;
            _caRepository = caRepository;
            _hinhanhcaRepository = hinhanhcaRepository;
            _mapper = mapper;
        }

        public List<CaDTO> GetAll()
        {
            var _lstCas = _caRepository.GetAll().OrderBy(x => x.TenCa).ToList();

            var caDtos = _mapper.Map<List<CaDTO>>(_lstCas);

            var entities = new List<HinhAnhCa>();

            foreach (var item in caDtos)
            {
                //get thông tin biến thể
                var bienthe = _bientheRepository.GetById(item.MaBienThe);
                item.BienThes = _mapper.Map<BienTheDTO>(bienthe);



                // get hinh anh

                //var hinhanhca = _hinhanhcaRepository.GetMany(p => p.MaCa == item.MaCa).ToList();

                //var sql = from ha in hinhanhca
                //          where ha.ChonAvt == true
                //          select ha;

                //entities = sql.OrderByDescending(c => c.TenHinhAnh).ToList();
                //item.Listhacas = _mapper.Map<List<HinhAnhCaDTO>>(entities);
                //item.Mahacas = entities.Where(p => p.MaCa == item.MaCa).Select(p => p.DuongDan).First();
            }

            return caDtos;
        }
    }
}
