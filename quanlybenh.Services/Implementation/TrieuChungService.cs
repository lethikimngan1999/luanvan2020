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
    public class TrieuChungService : BaseService, ITrieuChungService
    {

        private IDataRepository<TrieuChung> _trieuchungRepository;
        private IDataRepository<Benh> _benhRepository;
        private IDataRepository<TrieuChungBenh> _trieuchungbenhRepository;
        private readonly IMapper _mapper;
        private IBenhService _benhService;
        public TrieuChungService(
            IDataRepository<TrieuChung> trieuchungRepository,
            IDataRepository<Benh> benhRepository,
             IDataRepository<TrieuChungBenh> trieuchungbenhRepository,
            IBenhService benhService,
            IUnitOfWork unitOfWork, IMapper mapper): base (unitOfWork)
        {
            _trieuchungRepository = trieuchungRepository;
            _benhRepository = benhRepository;
            _benhService = benhService;
            _trieuchungbenhRepository = trieuchungbenhRepository;
            _mapper = mapper;

        }
        public bool Create(TrieuChungDTO trieuchungDto)
        {
          try
            {
                var item = CheckExistsTenTrieuChung(trieuchungDto.TenTrieuChung);
                if (item) return false;
                var trieuchung = new TrieuChung
                {
                    MaTrieuChung = Guid.NewGuid(),
                    TenTrieuChung = trieuchungDto.TenTrieuChung,
                    MoTaTrieuChung = trieuchungDto.MoTaTrieuChung,
                 //   MaBenh = trieuchungDto.MaBenh
                };

                _trieuchungRepository.Insert(trieuchung);
                _unitOfWork.Commit();

                if (trieuchungDto.MaBenhs != null)
                {
                    foreach (var mabenh in trieuchungDto.MaBenhs)
                    {
                        var trieuchungbenh = new TrieuChungBenh
                        {
                            MaTrieuChung = trieuchung.MaTrieuChung,
                            MaBenh = new Guid(mabenh)
                        };
                        _trieuchungbenhRepository.Insert(trieuchungbenh);

                    }
                    _unitOfWork.Commit();
                }
                return true;

            }catch(Exception ex)
            {
                return false;
            }
        }


        public bool Update(TrieuChungDTO trieuchungDto)
        {
            try
            {
                var trieuchung = _trieuchungRepository.GetById(trieuchungDto.MaTrieuChung);
                // check nhan vien exist
                if (trieuchung == null) return false;

                //Update nhan vien detail
                trieuchung.MaTrieuChung = trieuchungDto.MaTrieuChung;
                trieuchung.TenTrieuChung = trieuchungDto.TenTrieuChung;
                trieuchung.MoTaTrieuChung = trieuchungDto.MoTaTrieuChung;

              //  trieuchung.MaBenh = trieuchungDto.MaBenh;


                // update user
                _trieuchungRepository.Update(trieuchung);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TrieuChungDTO> GetAll()
        {
            var _lstTrieuChungs = _trieuchungRepository.GetAll().OrderBy(x => x.TenTrieuChung).ToList();
            var trieuchungDtos = _mapper.Map<List<TrieuChungDTO>>(_lstTrieuChungs);

            var entities = new List<Benh>();
            foreach (var trieuchung in trieuchungDtos)
            {
                // get danh sach benh

                var _lstTrieuchungbenhs = _trieuchungbenhRepository.GetMany(p => p.MaTrieuChung == trieuchung.MaTrieuChung).ToList();
                var _lstBenhs = _benhRepository.GetAll().ToList();

                var sql = from benh in _lstBenhs
                          join trieuchungbenh in _lstTrieuchungbenhs on benh.MaBenh equals trieuchungbenh.MaBenh
                          select benh;
                entities = sql.OrderByDescending(c => c.TenBenh).ToList();
                trieuchung.ListBenhs = _mapper.Map<List<BenhDTO>>(entities);

                trieuchung.MaBenhs = _lstTrieuchungbenhs.Where(p => p.MaTrieuChung == trieuchung.MaTrieuChung)?.Select(p => p.MaBenh.ToString());

            }
            //foreach(var item in trieuchungDtos)
            //{
            //    var benh = _benhRepository.GetById(item.MaBenh);
            //    item.Benh = _mapper.Map<BenhDTO>(benh);
            //}
            return trieuchungDtos;
        }

        public TrieuChungDTO GetByMaTrieuChung(string matrieuchung)
        {
          try
            {
                var trieuchung = _trieuchungRepository.GetMany(p => p.MaTrieuChung.ToString().ToLower() == matrieuchung.ToLower().Trim()).FirstOrDefault();
                if(trieuchung == null)
                {
                    return null;
                }
                var trieuchungDto = _mapper.Map<TrieuChungDTO>(trieuchung);
                return trieuchungDto;

            }catch(Exception e)
            {
                return null;
            }

        }

        public bool CheckExistsTenTrieuChung(string tentrieuchung)
        {
            var result = _trieuchungRepository.GetAll().Where(x => x.TenTrieuChung == tentrieuchung.Trim());
            if(result.Any())
            {
                return true;
            }
            return false;
        }

        public bool Delete(string matrieuchung)
        {
            try
            {
                var trieuchung = _trieuchungRepository.GetById(new Guid(matrieuchung));
                if (trieuchung == null) return false;
                _trieuchungRepository.Remove(trieuchung);
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
