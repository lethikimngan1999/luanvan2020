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

        public bool Create(CaDTO caDto)
        {
            try
            {
                var ca = new Ca
                {
                    MaCa = Guid.NewGuid(),
                    MaBienThe = caDto.MaBienThe,
                    TenCa = caDto.TenCa,
                    GioiTinh = Convert.ToInt32(caDto.GioiTinh),
                    NgaySinh = caDto.NgaySinh,
                    KichThuoc = caDto.KichThuoc,
                    NgayDo = caDto.NgayDo,
                    DonGia = Convert.ToDouble(caDto.DonGia),
                    Tuoi = caDto.Tuoi,
                    TinhTrang = Convert.ToBoolean(caDto.TinhTrang)
                };
                _caRepository.Insert(ca);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(CaDTO caDto)
        {
            try
            {
                var ca = _caRepository.GetById(caDto.MaCa);
                if (ca == null)
                {
                    return false;
                }
                // Update chi tiet 
                ca.MaCa = caDto.MaCa;
                ca.MaBienThe = caDto.MaBienThe;
                ca.TenCa = caDto.TenCa;
                ca.GioiTinh = Convert.ToInt32(caDto.GioiTinh);
                ca.NgaySinh = caDto.NgaySinh;
                ca.KichThuoc = caDto.KichThuoc;
                ca.NgayDo = caDto.NgayDo;
                ca.DonGia = Convert.ToDouble(caDto.DonGia);
                ca.Tuoi = caDto.Tuoi;
                ca.TinhTrang = Convert.ToBoolean(caDto.TinhTrang);
                //update thuốc điều trị
                //var thuocdieutriOld = _thuocdieutriRepository.GetMany(p => p.MaBenh == benh.MaBenh).ToList();
                //_thuocdieutriRepository.RemoveMultiple(thuocdieutriOld);

                //var thuocs = _thuocRepository.GetMany(r => benhDto.MaThuocs.Contains(r.MaThuoc.ToString()));
                //foreach (var thuoc in thuocs)
                //{
                //    var thuocdieutri = new ThuocDieuTri { MaBenh = benh.MaBenh, MaThuoc = thuoc.MaThuoc };

                //    _thuocdieutriRepository.Insert(thuocdieutri);
                //}
                // update user
                _caRepository.Update(ca);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

