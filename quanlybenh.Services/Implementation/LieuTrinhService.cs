using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.Benh;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Implementation
{
    public class LieuTrinhService : BaseService, ILieuTrinhService
    {
        private IDataRepository<LieuTrinh> _lieutrinhRepository;
        private IDataRepository<Thuoc> _thuocRepository;

        private readonly IMapper _mapper;
        private IThuocService _thuocService;
        public LieuTrinhService(
            IDataRepository<LieuTrinh> lieutrinhRepository,
            IDataRepository<Thuoc> thuocRepository,
            IThuocService thuocService,
            IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _lieutrinhRepository = lieutrinhRepository;
            _thuocRepository = thuocRepository;
            _thuocService = thuocService;
            _mapper = mapper;

        }
        public bool CheckExistsTenLieuTrinh(string tenlieutrinh)
        {
            var result = _lieutrinhRepository.GetAll().Where(x => x.TenLieuTrinh == tenlieutrinh.Trim());
            if (result.Any())
            {
                return true;
            }
            return false;
        }

        public bool Create(LieuTrinhDTO lieutrinhDto)
        {
            try
            {
                //var item = CheckExistsTenLieuTrinh(lieutrinhDto.TenLieuTrinh);
                //if (item) return false;
                var lieutrinh = new LieuTrinh
                {
                    MaLieuTrinh = Guid.NewGuid(),
                    TenLieuTrinh = lieutrinhDto.TenLieuTrinh,
                    MoTaLieuTrinh = lieutrinhDto.MoTaLieuTrinh,
                    MaThuoc = lieutrinhDto.MaThuoc
                };

                _lieutrinhRepository.Insert(lieutrinh);
                _unitOfWork.Commit();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<LieuTrinhDTO> GetAll()
        {
            var _lstLieuTrinhs = _lieutrinhRepository.GetAll().OrderBy(x => x.TenLieuTrinh).ToList();
            var lieutrinhDtos = _mapper.Map<List<LieuTrinhDTO>>(_lstLieuTrinhs);

            foreach (var item in lieutrinhDtos)
            {
                var thuoc = _thuocRepository.GetById(item.MaThuoc);
                item.Thuoc = _mapper.Map<ThuocDTO>(thuoc);
            }
            return lieutrinhDtos;
        }

        public LieuTrinhDTO GetByMaLieuTrinh(string malieutrinh)
        {
            try
            {
                var lieutrinh = _lieutrinhRepository.GetMany(p => p.MaLieuTrinh.ToString().ToLower() == malieutrinh.ToLower().Trim()).FirstOrDefault();
                if (lieutrinh == null)
                {
                    return null;
                }
                var lieutrinhDto = _mapper.Map<LieuTrinhDTO>(lieutrinh);
                return lieutrinhDto;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Update(LieuTrinhDTO lieutrinhDto)
        {
            try
            {
                var lieutrinh = _lieutrinhRepository.GetById(lieutrinhDto.MaLieuTrinh);
                // check nhan vien exist
                if (lieutrinh == null) return false;

                //Update nhan vien detail
                lieutrinh.MaLieuTrinh = lieutrinhDto.MaLieuTrinh;
                lieutrinh.TenLieuTrinh = lieutrinhDto.TenLieuTrinh;
                lieutrinh.MoTaLieuTrinh = lieutrinhDto.MoTaLieuTrinh;

                lieutrinh.MaThuoc = lieutrinhDto.MaThuoc;
              

                // update user
                _lieutrinhRepository.Update(lieutrinh);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string malieutrinh)
        {
            try
            {
                var lieutrinh = _lieutrinhRepository.GetById(new Guid(malieutrinh));
                if (lieutrinh == null) return false;
                _lieutrinhRepository.Remove(lieutrinh);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
