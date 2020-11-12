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
    public class ThuocService : BaseService, IThuocService
    {
        private IDataRepository<Thuoc> _thuocRepository;
        private IDataRepository<Benh> _benhRepository;
        private IDataRepository<ThuocDieuTri> _thuocdieutriRepository;
        private IDataRepository<LieuTrinh> _lieutrinhRepository;
        private readonly IMapper _mapper;


        public ThuocService(
              IDataRepository<Benh> benhRepository,
            IDataRepository<Thuoc> thuocRepository,
                  IDataRepository<LieuTrinh> lieutrinhRepository,
              IDataRepository<ThuocDieuTri> thuocdieutriRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork)
        {
            _benhRepository = benhRepository;
            _thuocRepository = thuocRepository;
            _thuocdieutriRepository = thuocdieutriRepository;
            _lieutrinhRepository = lieutrinhRepository;
            _mapper = mapper;

        }

        public bool CheckExistsTenThuoc(string ten)
        {
            var result = _thuocRepository.GetAll().Where(x => x.TenThuoc == ten.Trim());
            if(result.Any())
            {
                return true;
            }
            return false;

        }

        public bool Create(ThuocDTO thuocDto)
        {
           try
            {
                var item = CheckExistsTenThuoc(thuocDto.TenThuoc);
                if (item) return false;
                var thuoc = new Thuoc
                {
                    MaThuoc = Guid.NewGuid(),
                    TenThuoc = thuocDto.TenThuoc,
                    CongDung = thuocDto.CongDung,
                    CachDung = thuocDto.CachDung,
                    HinhAnh = thuocDto.HinhAnh,
                    LuuY = thuocDto.LuuY
                };
                _thuocRepository.Insert(thuoc);
                _unitOfWork.Commit();

                if (thuocDto.MaBenhs != null)
                {
                    foreach (var mabenh in thuocDto.MaBenhs)
                    {
                        var thuocdieutri = new ThuocDieuTri
                        {
                            MaThuoc = thuoc.MaThuoc,
                            MaBenh = new Guid(mabenh)
                        };
                        _thuocdieutriRepository.Insert(thuocdieutri);
                  
                    }
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public List<ThuocDTO> GetAll()
        {
            var _lst = _thuocRepository.GetAll().OrderByDescending(x => x.TenThuoc).ToList();
            var thuocDtos = _mapper.Map<List<ThuocDTO>>(_lst);

            var entities = new List<Benh>();
            foreach (var thuoc in thuocDtos)
            {
                // get danh sach benh

                var _lstThuocDieuTris = _thuocdieutriRepository.GetMany(p => p.MaThuoc == thuoc.MaThuoc).ToList();
                var _lstBenhs = _benhRepository.GetAll().ToList();

                var sql = from benh in _lstBenhs
                          join thuocdieutri in _lstThuocDieuTris on benh.MaBenh equals thuocdieutri.MaBenh
                          select benh;
                entities = sql.OrderByDescending(c => c.TenBenh).ToList();
                thuoc.ListBenhs = _mapper.Map<List<BenhDTO>>(entities);

                thuoc.MaBenhs = _lstThuocDieuTris.Where(p => p.MaThuoc == thuoc.MaThuoc)?.Select(p => p.MaBenh.ToString());

            }


            return thuocDtos;

        }

        public ThuocDTO GetById(string mathuoc)
        {
           try
            {
                var thuoc = _thuocRepository.GetMany(p => p.MaThuoc.ToString().ToLower() == mathuoc.ToLower().Trim()).FirstOrDefault();
                if(thuoc == null)
                {
                    return null;

                }
                var thuocDto = _mapper.Map<ThuocDTO>(thuoc);

                // list bệnh mà thuốc điều trị
                var entities = new List<Benh>();
                var _lstThuocDieuTris = _thuocdieutriRepository.GetMany(p => p.MaThuoc == thuocDto.MaThuoc).ToList();
                var _lstBenhs = _benhRepository.GetAll().ToList();
                var sql = from benh in _lstBenhs
                          join thuocdieutri in _lstThuocDieuTris on benh.MaBenh equals thuocdieutri.MaBenh
                          select benh;
                entities = sql.OrderByDescending(c => c.TenBenh).ToList();
                thuocDto.ListBenhs = _mapper.Map<List<BenhDTO>>(entities);
                thuocDto.MaBenhs = _lstThuocDieuTris.Where(p => p.MaThuoc == thuocDto.MaThuoc)?.Select(p => p.MaBenh.ToString());

                // list liệu trình
                var _lstLieuTrinhs = _lieutrinhRepository.GetMany(p => p.MaThuoc == thuocDto.MaThuoc).ToList();
                thuocDto.ListLieuTrinhs = _mapper.Map<List<LieuTrinhDTO>>(_lstLieuTrinhs);
                thuocDto.MaLieuTrinhs = _lstLieuTrinhs.Where(p => p.MaThuoc == thuocDto.MaThuoc)?.Select(p => p.MaLieuTrinh.ToString());

                return thuocDto;
            }catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(ThuocDTO thuocDto)
        {
            try
            {
                var thuoc = _thuocRepository.GetById(thuocDto.MaThuoc);
                if (thuoc == null)
                {
                    return false;
                }
                // Update chi tiet 
                thuoc.MaThuoc = thuocDto.MaThuoc;
                thuoc.TenThuoc = thuocDto.TenThuoc.ToLower();
                thuoc.CongDung = thuocDto.CongDung;
                thuoc.CachDung = thuocDto.CachDung;
                thuoc.HinhAnh = thuocDto.HinhAnh;
                thuoc.LuuY = thuocDto.LuuY;
                //update thuốc điều trị
                var thuocdieutriOld = _thuocdieutriRepository.GetMany(p => p.MaThuoc == thuoc.MaThuoc).ToList();
                _thuocdieutriRepository.RemoveMultiple(thuocdieutriOld);

                var benhs = _benhRepository.GetMany(r => thuocDto.MaBenhs.Contains(r.MaBenh.ToString()));
                foreach (var benh in benhs)
                {
                    var thuocdieutri = new ThuocDieuTri { MaBenh = benh.MaBenh, MaThuoc = thuoc.MaThuoc };

                    _thuocdieutriRepository.Insert(thuocdieutri);
                }
                // update user
                _thuocRepository.Update(thuoc);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string mathuoc)
        {
            try
            {
                var thuoc = _thuocRepository.GetById(new Guid(mathuoc));
                if (thuoc == null) return false;
              
                var thuocdieutris = _thuocdieutriRepository.GetAll().Where(p => p.MaThuoc == thuoc.MaThuoc).ToList();
                _thuocdieutriRepository.RemoveMultiple(thuocdieutris);
                _unitOfWork.Commit();

                var thuocs = _thuocRepository.GetById(new Guid(mathuoc));
                _thuocRepository.Remove(thuocs);
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
