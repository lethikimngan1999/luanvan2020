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
    public class TrieuChungBenhService : BaseService, ITrieuChungBenhService
    {
        IDataRepository<TrieuChungBenh> _trieuchungbenhRepository;
        private IDataRepository<TrieuChung> _trieuchungRepository;
        private IDataRepository<Benh> _benhRepository;
        private readonly ITrieuChungService _trieuchungService;
        private readonly IBenhService _benhService;

        private readonly IMapper _mapper;


        public TrieuChungBenhService(
            IDataRepository<TrieuChungBenh> trieuchungbenhRepository,
            IDataRepository<TrieuChung> trieuchungRepository,
               IDataRepository<Benh> benhRepository,
            ITrieuChungService trieuchungService,
            IBenhService benhService,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _trieuchungbenhRepository = trieuchungbenhRepository;
            _trieuchungRepository = trieuchungRepository;
            _benhRepository = benhRepository;
            _trieuchungService = trieuchungService;
            _benhService = benhService;
            _mapper = mapper;
        }

        public bool Add(List<TrieuChungBenhDTO> trieuchungbenhDtos)
        {

            try
            {
                var mabenh = trieuchungbenhDtos.FirstOrDefault().MaBenh;
                var trieuchungbenhs = _trieuchungbenhRepository.GetAll().Where(p => p.MaBenh == mabenh).ToList();

                var allTrieuchungbenh = trieuchungbenhs.Select(p => new { p.MaTrieuChung, p.MaBenh }).ToList();
                var allTrieuchungbenhDto = trieuchungbenhDtos.Select(p => new { p.MaTrieuChung, p.MaBenh });

                var trieuchungbenhRemove = allTrieuchungbenh.Except(allTrieuchungbenhDto).ToList();
                var trieuchungbenhInsert = allTrieuchungbenhDto.Except(allTrieuchungbenh).ToList();

                // remove
                foreach (var item in trieuchungbenhRemove)
                {
                    var trieuchungbenh = trieuchungbenhs.Where(p => p.MaTrieuChung == item.MaTrieuChung).FirstOrDefault();
                    _trieuchungbenhRepository.Remove(trieuchungbenh);
                }
                // insert

                foreach (var item in trieuchungbenhInsert)
                {
                    var trieuchungbenhDto = trieuchungbenhDtos.Where(p => p.MaTrieuChung == item.MaTrieuChung).FirstOrDefault();
                    var trieuchungbenh = _mapper.Map<TrieuChungBenh>(trieuchungbenhDto);
                    //thuocdieutri.LieuDung = thuocdieutriDto.LieuDung;
                    //thuocdieutri.MoTa = thuocdieutriDto.MoTa;
                    _trieuchungbenhRepository.Insert(trieuchungbenh);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<TrieuChungBenhDTO> GetAll()
        {
            var lstTrieuchungbenh = _trieuchungbenhRepository.GetAll().ToList();
            var lstTrieuchungbenhDtos = _mapper.Map<List<TrieuChungBenhDTO>>(lstTrieuchungbenh);
            foreach (var item in lstTrieuchungbenhDtos)
            {
                var trieuchung = _trieuchungRepository.GetById(item.MaTrieuChung.ToString());
                item.TrieuChung = _mapper.Map<TrieuChungDTO>(trieuchung);

                var benh = _benhService.GetById(item.MaBenh.ToString());
                item.Benh = _mapper.Map<BenhDTO>(benh);


            }
            return lstTrieuchungbenhDtos;
        }

        public List<TrieuChungBenhDTO> GetListByMaBenh(string mabenh)
        {
            var lstTrieuchungbenh = _trieuchungbenhRepository.GetMany(p => p.MaBenh == new Guid(mabenh)).ToList();
            var trieuchungbenhDtos = _mapper.Map<List<TrieuChungBenhDTO>>(lstTrieuchungbenh);
            foreach (var item in trieuchungbenhDtos)
            {
                var trieuchung = _trieuchungRepository.GetById(item.MaTrieuChung.ToString());
                item.TrieuChung = _mapper.Map<TrieuChungDTO>(trieuchung);

                var benh = _benhService.GetById(item.MaBenh.ToString());
                item.Benh = _mapper.Map<BenhDTO>(benh);
            }
            return trieuchungbenhDtos;
        }

        public bool AddTrieuChungBenh(List<TrieuChungBenhDTO> trieuchungbenhDtos)
        {

            try
            {
                var matrieuchung = trieuchungbenhDtos.FirstOrDefault().MaTrieuChung;
                var trieuchungbenhs = _trieuchungbenhRepository.GetAll().Where(p => p.MaTrieuChung == matrieuchung).ToList();

                var allTrieuchungbenh = trieuchungbenhs.Select(p => new { p.MaBenh, p.MaTrieuChung }).ToList();
                var allTrieuchungbenhDto = trieuchungbenhDtos.Select(p => new { p.MaBenh, p.MaTrieuChung });

                var trieuchungbenhRemove = allTrieuchungbenh.Except(allTrieuchungbenhDto).ToList();
                var trieuchungbenhInsert = allTrieuchungbenhDto.Except(allTrieuchungbenh).ToList();

                // remove
                foreach (var item in trieuchungbenhRemove)
                {
                    var trieuchungbenh = trieuchungbenhs.Where(p => p.MaBenh == item.MaBenh).FirstOrDefault();
                    _trieuchungbenhRepository.Remove(trieuchungbenh);
                }
                // insert

                foreach (var item in trieuchungbenhInsert)
                {
                    var trieuchungbenhDto = trieuchungbenhDtos.Where(p => p.MaBenh == item.MaBenh).FirstOrDefault();
                    var trieuchungbenh = _mapper.Map<TrieuChungBenh>(trieuchungbenhDto);
                    //trieuchungbenh.LieuDung = trieuchungbenhDto.LieuDung;
                    //trieuchungbenh.MoTa = thuocdieutriDto.MoTa;
                    _trieuchungbenhRepository.Insert(trieuchungbenh);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<TrieuChungBenhDTO> GetAllTRieuChungBenhByType(List<SearchDTO> searchString)
        {
            // var statuses = new[] { searchString };

            var entities = new List<TrieuChungBenhDTO>();
            //if (!string.IsNullOrEmpty(searchString))
            //{
            if (searchString.Count() > 0)
            {
                var lstTrieuChungs = _trieuchungRepository.GetAll().ToList();
                var lstTrieuChungBenhs = _trieuchungbenhRepository.GetAll().ToList();

                List<TrieuChungBenh> sql = new List<TrieuChungBenh>();
              
                foreach (var a in searchString)
                    {
                    List<TrieuChungBenh> sql1 = new List<TrieuChungBenh>();
                     sql1 = (from trieuchungbenh in lstTrieuChungBenhs
                               where trieuchungbenh.MaTrieuChung == a.search
                               select trieuchungbenh).ToList();

                    if(sql.Count()>0)
                    {
                        foreach (var b in sql1)
                        {
                            if (sql.Where(x => x.MaBenh == b.MaBenh).Count() > 0)
                            {
                                sql = sql.Where(x => x.MaBenh == b.MaBenh).ToList();
                            }
                        }
                    }
                    else
                    {
                        sql = sql1;
                    }


                    //if (sql.Count() > sql1.Count())
                    //{
                    //      sql = sql.Intersect(sql1);
                    //}
                    //else
                    //{
                    //    sql = sql.Intersect(sql1);
                    //}
                }
                //sql = sql1.Intersect(sql);
                foreach (var b in sql)
                    {

                        entities.Add(new TrieuChungBenhDTO { MaBenh = b.MaBenh, MaTrieuChung = b.MaTrieuChung });

                    }
                
               


                //var lstTrieuChungs = _trieuchungRepository.GetAll();
                //var lstTrieuChungBenhs = _trieuchungbenhRepository.GetAll();
                //var lstBenhs = _benhRepository.GetAll();
                //var sql = from
                //               trieuchung in lstTrieuChungs
                //          join trieuchungbenh in lstTrieuChungBenhs on trieuchung.MaTrieuChung equals trieuchungbenh.MaTrieuChung
                //          join benh in lstBenhs on trieuchungbenh.MaBenh equals benh.MaBenh
                //          where statuses.Contains(trieuchung.TenTrieuChung)
                //          select trieuchungbenh;
                //entities = sql.OrderByDescending(c => c.MaBenh).ToList();

            }





            //    var lstTrieuChungs = _trieuchungRepository.GetAll();
            //var lstTrieuChungBenhs = _trieuchungbenhRepository.GetAll();
            //var sql = from trieuchungbenh in lstTrieuChungBenhs
            //          join trieuchung in lstTrieuChungs on trieuchungbenh.MaTrieuChung equals trieuchung.MaTrieuChung
            //          where trieuchung.TenTrieuChung.Contains(searchString)
            //          select trieuchungbenh;
            //entities = sql.OrderByDescending(c => c.MaBenh).ToList();
            // }
            return entities.ToList();

        }

        //public List<TrieuChungBenhDTO> GetAllTrieuChungActive( Guid matrieuchung)
        //{
        //    //var _lstTrieuChungBenhs = _trieuchungbenhRepository.GetMany(p => p.selected == StatusObject.True && p.MaTrieuChung == matrieuchung ).ToList();
        //    //var trieuchungbenhDtos = _mapper.Map<List<TrieuChungBenhDTO>>(_lstTrieuChungBenhs);

        //   // var entities = new List<Role>();

        //    //foreach (var item in trieuchungbenhDtos)
        //    //{
        //    //    var trieuchung = _trieuchungRepository.GetById(item.MaTrieuChung.ToString());
        //    //    item.TrieuChung = _mapper.Map<TrieuChungDTO>(trieuchung);

        //    //    var benh = _benhService.GetById(item.MaBenh.ToString());
        //    //    item.Benh = _mapper.Map<BenhDTO>(benh);
        //    //}
        //    return trieuchungbenhDtos;
        //}
    }
}
