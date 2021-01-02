using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.BienThe;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Implementation
{
    public class ChungLoaiService : BaseService, IChungLoaiService
    {

        private IDataRepository<ChungLoai> _chungloaiRepository;
        //private IDataRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public ChungLoaiService(
               IDataRepository<ChungLoai> chungloaiRepository,
                
               IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _chungloaiRepository = chungloaiRepository;
            //_userRepository = userRepository;
            _mapper = mapper;
        }


        public bool Create(ChungLoaiDTO chungloaiDto)
        {
            try
            {
                var item = CheckExistsTenChungLoai(chungloaiDto.TenChungLoai);
                if (item) return false;
                var chungloai = new ChungLoai
                {
                    MaChungLoai = Guid.NewGuid(),
                    TenChungLoai = chungloaiDto.TenChungLoai,
                    MauSac = chungloaiDto.MauSac,
                    MucDoChamSoc = chungloaiDto.MucDoChamSoc,
                    CheDoAn = chungloaiDto.CheDoAn,
                    MoTa = chungloaiDto.MoTa,
                    TinhCach = chungloaiDto.TinhCach,
                    DieuKienNuoc = chungloaiDto.DieuKienNuoc,
                    KichThuocToiDa = chungloaiDto.KichThuocToiDa,
                    CachChamSoc = chungloaiDto.CachChamSoc,
                    
                };

                _chungloaiRepository.Insert(chungloai);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ChungLoaiDTO> GetAll()
        {
            //get tất cả danh sách 
            var _lsts = _chungloaiRepository.GetAll().OrderBy(x => x.TenChungLoai).ToList();

            var ChungLoaiDtos = _mapper.Map<List<ChungLoaiDTO>>(_lsts);



            return ChungLoaiDtos;
        }

        public ChungLoaiDTO GetById(string machungloai)
        {
            try
            {
                var ChungLoai = _chungloaiRepository.GetById(new Guid(machungloai));
                if (ChungLoai == null) return null;
                var Object = _mapper.Map<ChungLoaiDTO>(ChungLoai);


                return Object;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool CheckExistsTenChungLoai(string tenchungloai)
        {
            var result = _chungloaiRepository.GetAll().Where(x => x.TenChungLoai == tenchungloai.Trim());
            if (result.Any())
            {
                return true;
            }
            return false;
        }

        public bool Update(ChungLoaiDTO chungloaiDto)
        {
            try
            {
                var ChungLoai = _chungloaiRepository.GetById(chungloaiDto.MaChungLoai);
                // check nhan vien exist
                if (ChungLoai == null) return false;

                //Update nhan vien detail
                ChungLoai.MaChungLoai = chungloaiDto.MaChungLoai;
                ChungLoai.TenChungLoai = chungloaiDto.TenChungLoai;
                ChungLoai.MauSac = chungloaiDto.MauSac;
                ChungLoai.MucDoChamSoc = chungloaiDto.MucDoChamSoc;
                ChungLoai.CheDoAn = chungloaiDto.CheDoAn;
                ChungLoai.MoTa = chungloaiDto.MoTa;
                ChungLoai.TinhCach = chungloaiDto.TinhCach;
                ChungLoai.DieuKienNuoc = chungloaiDto.DieuKienNuoc;
                ChungLoai.KichThuocToiDa = chungloaiDto.KichThuocToiDa;
                ChungLoai.CachChamSoc = chungloaiDto.CachChamSoc;

                // update user
                _chungloaiRepository.Update(ChungLoai);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string machungloai)
        {
            try
            {
                var chungloai = _chungloaiRepository.GetById(new Guid(machungloai));
                if (chungloai == null) return false;
                _chungloaiRepository.Remove(chungloai);
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
