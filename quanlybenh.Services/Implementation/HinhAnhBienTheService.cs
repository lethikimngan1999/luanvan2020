using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.BienThe;
using quanlybenh.Services.DTO.HinhAnh;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Implementation
{
    public class HinhAnhBienTheService : BaseService, IHinhAnhBienTheService
    {
        private IDataRepository<HinhAnhBienThe> _hinhanhRepository;
        private IDataRepository<BienThe> _bientheRepository;

        private readonly IMapper _mapper;

        public HinhAnhBienTheService(
            IDataRepository<HinhAnhBienThe> hinhanhRepository,
            IDataRepository<BienThe> bientheRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper): base (unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _hinhanhRepository = hinhanhRepository;
            _bientheRepository = bientheRepository;
            _mapper = mapper;
        }

     
        public bool Create(HinhAnhBienTheDTO hinhanhDto)
        {
            try
            {
        
                    var hinhanh = new HinhAnhBienThe
                    {
                        MaHinhAnhBT = Guid.NewGuid(),
                        MaBienThe = hinhanhDto.MaBienThe,
                        TenHinhAnh = hinhanhDto.TenHinhAnh,
                        NgayTao = DateTime.Now,
                        DuongDan = hinhanhDto.DuongDan,
                        ChonAvt = Convert.ToBoolean(hinhanhDto.ChonAvt)
                    };
                    _hinhanhRepository.Insert(hinhanh);
                    _unitOfWork.Commit();

                    return true;
           
              
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public bool CheckExists(string tenhinhanh)
        {
            var result = _hinhanhRepository.GetAll().Where(x => x.TenHinhAnh == tenhinhanh.Trim());
            if (result.Any())
            {
                return true;
            }
            return false;
        }

        public List<HinhAnhBienTheDTO> GetAll()
        {
            var _lsthinhanhs = _hinhanhRepository.GetAll().OrderBy(x => x.TenHinhAnh).ToList();

            var hinhanhDtos = _mapper.Map<List<HinhAnhBienTheDTO>>(_lsthinhanhs);

            foreach (var item in hinhanhDtos)
            {

                var bienthe = _bientheRepository.GetById(item.MaBienThe);
                item.BienThe = _mapper.Map<BienTheDTO>(bienthe);
            }

            return hinhanhDtos;
        }
    }
}
