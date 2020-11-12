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
    public class ThuocDieuTriService : BaseService, IThuocDieuTriService
    {
        IDataRepository<ThuocDieuTri> _thuocdieutriRepository;
        private IDataRepository<Thuoc> _thuocRepository;
        private readonly IThuocService _thuocService;
        private readonly IBenhService _benhService;

        private readonly IMapper _mapper;


        public ThuocDieuTriService(
            IDataRepository<ThuocDieuTri> thuocdieutriRepository,
            IDataRepository<Thuoc> thuocRepository,
            IThuocService thuocService,
            IBenhService benhService,
            IUnitOfWork unitOfWork,
            IMapper mapper): base (unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _thuocRepository = thuocRepository;
            _thuocdieutriRepository = thuocdieutriRepository;
            _thuocService = thuocService;
            _benhService = benhService;
            _mapper = mapper;
        }

        public bool Add(List<ThuocDieuTriBenhDTO> thuocdieutriDtos)
        {
         
            try
            {
                var mabenh = thuocdieutriDtos.FirstOrDefault().MaBenh;
                var thuocdieutris = _thuocdieutriRepository.GetAll().Where(p => p.MaBenh == mabenh).ToList();

                var allThuocdieutri = thuocdieutris.Select(p => new { p.MaThuoc, p.MaBenh }).ToList();
                var allThuocdieutriDto = thuocdieutriDtos.Select(p => new { p.MaThuoc, p.MaBenh });

                var thuocdieutriRemove = allThuocdieutri.Except(allThuocdieutriDto).ToList();
                var thuocdieutriInsert = allThuocdieutriDto.Except(allThuocdieutri).ToList();

                // remove
                foreach( var item in thuocdieutriRemove)
                {
                    var thuocdieutri = thuocdieutris.Where(p => p.MaThuoc == item.MaThuoc).FirstOrDefault();
                    _thuocdieutriRepository.Remove(thuocdieutri);
                }
                // insert

                foreach( var item in thuocdieutriInsert)
                {
                    var thuocdieutriDto = thuocdieutriDtos.Where(p => p.MaThuoc == item.MaThuoc).FirstOrDefault();
                    var thuocdieutri = _mapper.Map<ThuocDieuTri>(thuocdieutriDto);
                    thuocdieutri.LieuDung = thuocdieutriDto.LieuDung;
                    thuocdieutri.MoTa = thuocdieutriDto.MoTa;
                    _thuocdieutriRepository.Insert(thuocdieutri);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<ThuocDieuTriBenhDTO> GetAll()
        {
            var lstThuocDieuTri = _thuocdieutriRepository.GetAll().ToList();
            var lstThuocDieuTriDtos = _mapper.Map<List<ThuocDieuTriBenhDTO>>(lstThuocDieuTri);
            foreach( var item in lstThuocDieuTriDtos)
            {
                var thuoc = _thuocService.GetById(item.MaThuoc.ToString());
                item.Thuoc = _mapper.Map<ThuocDTO>(thuoc);

                var benh = _benhService.GetById(item.MaBenh.ToString());
                item.Benh = _mapper.Map<BenhDTO>(benh);


            }
            return lstThuocDieuTriDtos;
        }

        public List<ThuocDieuTriBenhDTO> GetListByMaBenh(string mabenh)
        {
            var lstThuocDieuTri = _thuocdieutriRepository.GetMany(p => p.MaBenh == new Guid(mabenh)).ToList();
            var thuocdieutriDtos = _mapper.Map<List<ThuocDieuTriBenhDTO>>(lstThuocDieuTri);
            foreach(var item in thuocdieutriDtos)
            {
                var thuoc = _thuocService.GetById(item.MaThuoc.ToString());
                item.Thuoc = _mapper.Map<ThuocDTO>(thuoc);

                var benh = _benhService.GetById(item.MaBenh.ToString());
                item.Benh = _mapper.Map<BenhDTO>(benh);
            }
            return thuocdieutriDtos;
        }

        public bool AddDieuTriBenh(List<ThuocDieuTriBenhDTO> thuocdieutriDtos)
        {

            try
            {
                var mathuoc = thuocdieutriDtos.FirstOrDefault().MaThuoc;
                var thuocdieutris = _thuocdieutriRepository.GetAll().Where(p => p.MaThuoc == mathuoc).ToList();

                var allThuocdieutri = thuocdieutris.Select(p => new { p.MaBenh, p.MaThuoc }).ToList();
                var allThuocdieutriDto = thuocdieutriDtos.Select(p => new { p.MaBenh, p.MaThuoc });

                var thuocdieutriRemove = allThuocdieutri.Except(allThuocdieutriDto).ToList();
                var thuocdieutriInsert = allThuocdieutriDto.Except(allThuocdieutri).ToList();

                // remove
                foreach (var item in thuocdieutriRemove)
                {
                    var thuocdieutri = thuocdieutris.Where(p => p.MaBenh == item.MaBenh).FirstOrDefault();
                    _thuocdieutriRepository.Remove(thuocdieutri);
                }
                // insert

                foreach (var item in thuocdieutriInsert)
                {
                    var thuocdieutriDto = thuocdieutriDtos.Where(p => p.MaBenh == item.MaBenh).FirstOrDefault();
                    var thuocdieutri = _mapper.Map<ThuocDieuTri>(thuocdieutriDto);
                    thuocdieutri.LieuDung = thuocdieutriDto.LieuDung;
                    thuocdieutri.MoTa = thuocdieutriDto.MoTa;
                    _thuocdieutriRepository.Insert(thuocdieutri);
                }
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
