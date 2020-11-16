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
using static quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Services.Implementation
{
    public class BenhService : BaseService, IBenhService
    {
        private IDataRepository<Benh> _benhRepository;
        private IDataRepository<Thuoc> _thuocRepository;
        private IDataRepository<TrieuChung> _trieuchungRepository;
        private IDataRepository<TrieuChungBenh> _trieuchungbenhRepository;
        private IDataRepository<ThuocDieuTri> _thuocdieutriRepository;

        private readonly IMapper _mapper;

        public BenhService(
               IDataRepository<Benh> benhRepository,
               IDataRepository<Thuoc> thuocRepository,
               IDataRepository<TrieuChung> trieuchungRepository,
           IDataRepository<TrieuChungBenh> trieuchungbenhRepository,
               IDataRepository<ThuocDieuTri> thuocdieutriRepository,

               IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _benhRepository = benhRepository;
            _thuocRepository = thuocRepository;
            _trieuchungRepository = trieuchungRepository;
            _trieuchungbenhRepository = trieuchungbenhRepository;
            _thuocdieutriRepository = thuocdieutriRepository;
            _mapper = mapper;
        }

        public bool Create(BenhDTO benhDto)
        {
            try
            {
                var item = CheckExistsTenbenh(benhDto.TenBenh);
                if (item) return false;
                var benh = new Benh
                {
                    MaBenh = Guid.NewGuid(),
                    TenBenh = benhDto.TenBenh,
                    NguyenNhan = benhDto.NguyenNhan,
                    CachDieuTri = benhDto.CachDieuTri,
                    MoTa = benhDto.MoTa,
                    HinhAnh = benhDto.HinhAnh
                };
                _benhRepository.Insert(benh);
                _unitOfWork.Commit();

                if (benhDto.MaThuocs != null)
                {
                    foreach (var mathuoc in benhDto.MaThuocs)
                    {
                        var thuocdieutri = new ThuocDieuTri
                        {
                            MaBenh = benh.MaBenh,
                            MaThuoc = new Guid(mathuoc),

                        };
                        _unitOfWork.Commit();
                    }
                }

                return true;



            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<BenhDTO> GetAll()
        {
            var _lst = _benhRepository.GetAll().OrderByDescending(x => x.TenBenh).ToList();
            var benhDtos = _mapper.Map<List<BenhDTO>>(_lst);

            var entities = new List<Thuoc>();
            foreach (var benh in benhDtos)
            {
                // get danh sach thuoc 

                var _lstThuocDieuTris = _thuocdieutriRepository.GetMany(p => p.MaBenh == benh.MaBenh).ToList();
                var _lstThuocs = _thuocRepository.GetAll().ToList();

                var sql = from thuoc in _lstThuocs
                          join thuocdieutri in _lstThuocDieuTris on thuoc.MaThuoc equals thuocdieutri.MaThuoc
                          select thuoc;
                entities = sql.OrderByDescending(c => c.TenThuoc).ToList();
                benh.ListThuocs = _mapper.Map<List<ThuocDTO>>(entities);

                benh.MaThuocs = _lstThuocDieuTris.Where(p => p.MaBenh == benh.MaBenh)?.Select(p => p.MaThuoc.ToString());


                // list triệu chứng cho bệnh
                var entitiestt = new List<TrieuChung>();

                var _lstTrieuchungbenhs = _trieuchungbenhRepository.GetMany(p => p.MaBenh == benh.MaBenh).ToList();
                var _lstTrieuchungs = _trieuchungRepository.GetAll().ToList();

                var sqltt = from trieuchung in _lstTrieuchungs
                            join trieuchungbenh in _lstTrieuchungbenhs on trieuchung.MaTrieuChung equals trieuchungbenh.MaTrieuChung
                            select trieuchung;
                entitiestt = sqltt.OrderByDescending(c => c.TenTrieuChung).ToList();
                benh.ListTrieuChungs = _mapper.Map<List<TrieuChungDTO>>(entitiestt);

                benh.MaTrieuChungs = _lstTrieuchungbenhs.Where(p => p.MaBenh == benh.MaBenh)?.Select(p => p.MaTrieuChung.ToString());

            }

            return benhDtos;
        }

        public bool CheckExistsTenbenh(string ten)
        {
            var result = _benhRepository.GetAll().Where(x => x.TenBenh == ten.Trim());
            if (result.Any())
            {
                return true;
            }
            return false;
        }

        public BenhDTO GetById(string mabenh)
        {
            try
            {
                var _lst = _benhRepository.GetMany(p => p.MaBenh.ToString().ToLower() == mabenh.ToLower().Trim()).ToList().FirstOrDefault();
                if (_lst == null)
                {
                    return null;
                }
                var benhDto = _mapper.Map<BenhDTO>(_lst);

                // list thuốc điều trị cho bệnh
                var entities = new List<Thuoc>();

                var _lstThuocDieuTris = _thuocdieutriRepository.GetMany(p => p.MaBenh == benhDto.MaBenh).ToList();
                var _lstThuocs = _thuocRepository.GetAll().ToList();

                var sql = from thuoc in _lstThuocs
                          join thuocdieutri in _lstThuocDieuTris on thuoc.MaThuoc equals thuocdieutri.MaThuoc
                          select thuoc;
                entities = sql.OrderByDescending(c => c.TenThuoc).ToList();
                benhDto.ListThuocs = _mapper.Map<List<ThuocDTO>>(entities);

                benhDto.MaThuocs = _lstThuocDieuTris.Where(p => p.MaBenh == benhDto.MaBenh)?.Select(p => p.MaThuoc.ToString());

                //  list triệu chứng
                //var _lstTrieuChungs = _trieuchungRepository.GetMany(p => p.MaBenh == benhDto.MaBenh).ToList();
                // benhDto.ListTrieuChungs = _mapper.Map<List<TrieuChungDTO>>(_lstTrieuChungs);
                // benhDto.MaTrieuChungs = _lstTrieuChungs.Where(p => p.MaBenh == benhDto.MaBenh)?.Select(p => p.MaTrieuChung.ToString());

                // list triệu chứng cho bệnh
                var entitiestt = new List<TrieuChung>();

                var _lstTrieuchungbenhs = _trieuchungbenhRepository.GetMany(p => p.MaBenh == benhDto.MaBenh).ToList();
                var _lstTrieuchungs = _trieuchungRepository.GetAll().ToList();

                var sqltt = from trieuchung in _lstTrieuchungs
                            join trieuchungbenh in _lstTrieuchungbenhs on trieuchung.MaTrieuChung equals trieuchungbenh.MaTrieuChung
                          select trieuchung;
                entitiestt = sqltt.OrderByDescending(c => c.TenTrieuChung).ToList();
                benhDto.ListTrieuChungs = _mapper.Map<List<TrieuChungDTO>>(entitiestt);

                benhDto.MaTrieuChungs = _lstTrieuchungbenhs.Where(p => p.MaBenh == benhDto.MaBenh)?.Select(p => p.MaTrieuChung.ToString());


                return benhDto;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool InsertAll(BenhDTO userRoleDataPopups)
        {

            var item = CheckExistsTenbenh(userRoleDataPopups.TenBenh);
            if (item) return false;
            var benh = new Benh
            {
                MaBenh = Guid.NewGuid(),
                TenBenh = userRoleDataPopups.TenBenh,
                NguyenNhan = userRoleDataPopups.NguyenNhan,
                CachDieuTri = userRoleDataPopups.CachDieuTri,
                MoTa = userRoleDataPopups.MoTa,
                HinhAnh = userRoleDataPopups.HinhAnh

            };
            _benhRepository.Insert(benh);
            _unitOfWork.Commit();

            if (userRoleDataPopups.MaThuocs != null)
            {
                foreach (var mathuoc in userRoleDataPopups.MaThuocs)
                {
                    var thuocdieutri = new ThuocDieuTri { MaBenh = benh.MaBenh, MaThuoc = new Guid(mathuoc) };
                    _thuocdieutriRepository.Insert(thuocdieutri);
                }
                _unitOfWork.Commit();
            }


            if (userRoleDataPopups.MaTrieuChungs != null)
            {
                foreach (var matrieuchung in userRoleDataPopups.MaTrieuChungs)
                {
                    var trieuchungbenh = new TrieuChungBenh { MaBenh = benh.MaBenh, MaTrieuChung = new Guid(matrieuchung) };
                    _trieuchungbenhRepository.Insert(trieuchungbenh);
                }
                _unitOfWork.Commit();
            }

            // liệu trình

            //if (userRoleDataPopups.ListLieuTrinhs.Count <= 0)
            //{
            //    return true;
            //}
            ////insert new record
            //for (int i = 0; i < userRoleDataPopups.ListLieuTrinhs.Count; i++)
            //{
            //    var roleViewModel = userRoleDataPopups.ListLieuTrinhs[i];


            //    //if (LieuTrinhConstant.MaLieuTrinh_Empty.Equals(roleViewModel.MaLieuTrinh.ToString()))
            //    //{
            //        roleViewModel.MaBenh = benh.MaBenh;
            //        roleViewModel.MaLieuTrinh = Guid.NewGuid();
            //    //    continue;
            //    //}
            //    var lstLieuTrinhs = _lieutrinhRepository.GetAll()
            //        .Where(p => p.MaLieuTrinh == roleViewModel.MaLieuTrinh && p.MaBenh == roleViewModel.MaBenh).ToList();
            //    LieuTrinh LieuTrinh = null;
            //    switch (lstLieuTrinhs.Count)
            //    {
            //        case 0:
            //            LieuTrinh = new LieuTrinh
            //            {
            //                MaBenh = roleViewModel.MaBenh,
            //                MaLieuTrinh = roleViewModel.MaLieuTrinh,
            //                TenLieuTrinh = roleViewModel.TenLieuTrinh,
            //                MoTaLieuTrinh = roleViewModel.MoTaLieuTrinh
            //            };
            //            _lieutrinhRepository.Insert(LieuTrinh);
            //            break;
            //        default:
            //            LieuTrinh = lstLieuTrinhs[0];

            //            _lieutrinhRepository.Update(LieuTrinh);
            //            break;
            //    }

            //}

            // delete record

            //  DeleteRecord(userId, lstData);
            //  _unitOfWork.Commit();
            return true;
        }

        public bool Update(BenhDTO benhDto)
        {
            try
            {
                var benh = _benhRepository.GetById(benhDto.MaBenh);
                if (benh == null)
                {
                    return false;
                }
                // Update chi tiet 
                benh.MaBenh = benhDto.MaBenh;
                benh.TenBenh = benhDto.TenBenh.ToLower();       
                benh.NguyenNhan = benhDto.NguyenNhan;
                benh.CachDieuTri = benhDto.CachDieuTri;
                benh.MoTa = benhDto.MoTa;
                benh.HinhAnh = benhDto.HinhAnh;
                //update thuốc điều trị
                var thuocdieutriOld = _thuocdieutriRepository.GetMany(p => p.MaBenh == benh.MaBenh).ToList();
                _thuocdieutriRepository.RemoveMultiple(thuocdieutriOld);

                var thuocs = _thuocRepository.GetMany(r => benhDto.MaThuocs.Contains(r.MaThuoc.ToString()));
                foreach (var thuoc in thuocs)
                {
                    var thuocdieutri = new ThuocDieuTri { MaBenh = benh.MaBenh, MaThuoc = thuoc.MaThuoc };
              
                    _thuocdieutriRepository.Insert(thuocdieutri);
                }           
                // update user
                _benhRepository.Update(benh);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string mabenh)
        {
            try
            {
                var benh = _benhRepository.GetById(new Guid(mabenh));
                if (benh == null) return false;
              //  var lieutrinhs = _lieutrinhRepository.GetAll().Where(p => p.MaBenh == benh.MaBenh).ToList();
              //  var trieuchungs = _trieuchungRepository.GetAll().Where(p => p.MaBenh == benh.MaBenh).ToList();
                var thuocdieutris = _thuocdieutriRepository.GetAll().Where(p => p.MaBenh == benh.MaBenh).ToList();

                //foreach (var item in lieutrinhs)
                //{
                  //  var lieutrinh = lieutrinhs.Where(p => p.MaBenh == item.MaBenh).FirstOrDefault();
                    //_lieutrinhRepository.RemoveMultiple(lieutrinhs);
                    //_unitOfWork.Commit();
                //}

                //foreach (var item in trieuchungs)
                //{
                   // var trieuchung = trieuchungs.Where(p => p.MaBenh == item.MaBenh).FirstOrDefault();
                    //_trieuchungRepository.RemoveMultiple(trieuchungs);
                    //_unitOfWork.Commit();
               // }

     

                //foreach (var item in thuocdieutris)
                //{
                  //  var thuocdieutri = thuocdieutris.Where(p => p.MaBenh == item.MaBenh).ToList();
                    _thuocdieutriRepository.RemoveMultiple(thuocdieutris);
                    _unitOfWork.Commit();
                //}

                var benhs = _benhRepository.GetById(new Guid(mabenh));
                _benhRepository.Remove(benhs);
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

