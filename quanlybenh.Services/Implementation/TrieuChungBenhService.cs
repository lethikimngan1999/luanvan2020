using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.Benh;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<BenhDTO> GetAllTRieuChungBenhByType(List<SearchDTO> searchString)
        {
            var entities = new List<BenhDTO>();
            var lstTrieuChungs = _trieuchungRepository.GetAll().ToList();
            var lstTrieuChungBenhs = _trieuchungbenhRepository.GetAll().ToList();
            var lstBenh = _benhRepository.GetAll().ToList();
            int res = 0;
            List<BenhDTO> sql2 = new List<BenhDTO>();
            //List<BenhDTO> sql3 = new List<BenhDTO>();
            //List<BenhDTO> sql4 = new List<BenhDTO>();
            //if (!string.IsNullOrEmpty(searchString))
            //{
            if (searchString.Count() > 0)
            {     
                foreach (var a in searchString)
                {
                    List<TrieuChungBenh> sql1 = new List<TrieuChungBenh>();
                    sql1 = (from trieuchungbenh in lstTrieuChungBenhs
                            where trieuchungbenh.MaTrieuChung == a.search
                            select trieuchungbenh).ToList();
                    foreach( var b in sql1)
                    {
                        sql2.Add(new BenhDTO { MaBenh = b.MaBenh});
                    }
                }
            }
            sql2 = sql2.GroupBy(test => test.MaBenh)
                   .Select(grp => grp.First())
                   .ToList();

            //sql3 = sql2.GroupBy(test => test.MaBenh)
            //       .Select(grp => grp.First())
            //       .ToList();
            //for (int i = 0; i < sql3.Count(); i++)
            //{
            //    var list = lstTrieuChungBenhs.Where(x => x.MaBenh == sql3[i].MaBenh).ToList();
            //    res = 0;
            //    for (int j = 0; j < list.Count(); j++)
            //    {
            //        for (int p = 0; p < searchString.Count(); p++)
            //        {
            //            if (list[j].MaTrieuChung != searchString[p].search)
            //            {
            //                res++;
            //                break;
            //            }
            //        }
            //        if (res != searchString.Count())
            //        {
            //            var benh = lstBenh.SingleOrDefault(x => x.MaBenh == sql3[i].MaBenh);
            //            sql4.Add(new BenhDTO
            //            {
            //                MaBenh = benh.MaBenh,
            //                TenBenh = benh.TenBenh,
            //                HinhAnh = benh.HinhAnh,
            //                NguyenNhan = benh.NguyenNhan,
                          
            //            });
            //        }
            //    }
            //}
            //sql4 = sql4.GroupBy(test => test.MaBenh)
            //      .Select(grp => grp.First())
            //      .ToList();
   
            for (int i = 0; i < sql2.Count(); i++)
                {
                    var list = lstTrieuChungBenhs.Where(x => x.MaBenh == sql2[i].MaBenh).ToList();
                    res = 0;
                    for (int j = 0; j <list.Count(); j++)
                    {
                        for(int p = 0; p< searchString.Count(); p++)
                        {
                            if (list[j].MaTrieuChung == searchString[p].search)
                            {
                                res++;
                                break;
                            }
                        }
                       if(res == searchString.Count())
                        {
                        var benh = lstBenh.SingleOrDefault(x => x.MaBenh == sql2[i].MaBenh);
                        entities.Add(new BenhDTO
                        {
                            MaBenh = benh.MaBenh,
                            TenBenh = benh.TenBenh,
                            HinhAnh = benh.HinhAnh,
                            NguyenNhan = benh.NguyenNhan,
                           // ListBenhs = _mapper.Map<List<BenhDTO>>(sql4)
                    });
                        }
                    }
                }
            return entities = entities.GroupBy(test => test.MaBenh)
                 .Select(grp => grp.First())
                 .ToList();
            //}
            //if (sql.Count() > 0)
            //{
            //    foreach (var b in sql)
            //    {

            //        //  entities = _mapper.Map<List<BenhDTO>>(benh);
            //        //entities.Add(benh);

            //        //foreach (var item in entities)
            //        //{
            //        //    var benh = _benhService.GetById(item.MaBenh.ToString());
            //        //    item.Benh = _mapper.Map<BenhDTO>(benh);

            //        //}
            //    }
            //}
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

            //    var lstTrieuChungs = _trieuchungRepository.GetAll();
            //var lstTrieuChungBenhs = _trieuchungbenhRepository.GetAll();
            //var sql = from trieuchungbenh in lstTrieuChungBenhs
            //          join trieuchung in lstTrieuChungs on trieuchungbenh.MaTrieuChung equals trieuchung.MaTrieuChung
            //          where trieuchung.TenTrieuChung.Contains(searchString)
            //          select trieuchungbenh;
            //entities = sql.OrderByDescending(c => c.MaBenh).ToList();
            // }
            //  return entities.Distinct().ToList();

        }

        public List<BenhDTO> GetAllBenhLienQuan(List<SearchDTO> searchString)
        {
            var entities = new List<BenhDTO>();
            var lstTrieuChungs = _trieuchungRepository.GetAll().ToList();
            var lstTrieuChungBenhs = _trieuchungbenhRepository.GetAll().ToList();
            var lstBenh = _benhRepository.GetAll().ToList();
            int res = 0;
            List<BenhDTO> sql2 = new List<BenhDTO>();
            List<BenhDTO> sql3 = new List<BenhDTO>();
            List<BenhDTO> sql4 = new List<BenhDTO>();
        
            if (searchString.Count() > 0)
            {
                foreach (var a in searchString)
                {
                    List<TrieuChungBenh> sql1 = new List<TrieuChungBenh>();
                    sql1 = (from trieuchungbenh in lstTrieuChungBenhs
                            where trieuchungbenh.MaTrieuChung == a.search
                            select trieuchungbenh).ToList();
                    foreach (var b in sql1)
                    {
                        sql2.Add(new BenhDTO { MaBenh = b.MaBenh });
                    }
                }
            }
            sql2 = sql2.GroupBy(test => test.MaBenh)
                   .Select(grp => grp.First())
                   .ToList();

            for (int i = 0; i < sql2.Count(); i++)
            {
                var list = lstTrieuChungBenhs.Where(x => x.MaBenh == sql2[i].MaBenh).ToList();
                res = 0;
                for (int j = 0; j < list.Count(); j++)
                {
                    for (int p = 0; p < searchString.Count(); p++)
                    {
                        if (list[j].MaTrieuChung == searchString[p].search)
                        {
                            res++;
                            break;
                        }
                    }
                    if (res == searchString.Count())
                    {
                        var benh = lstBenh.SingleOrDefault(x => x.MaBenh == sql2[i].MaBenh);
                        entities.Add(new BenhDTO
                        {
                            MaBenh = benh.MaBenh,
                            TenBenh = benh.TenBenh,
                            HinhAnh = benh.HinhAnh,
                            NguyenNhan = benh.NguyenNhan,
                            // ListBenhs = _mapper.Map<List<BenhDTO>>(sql4)
                        });
                    }
                }
            }

            entities = entities.GroupBy(test => test.MaBenh)
                 .Select(grp => grp.First())
                 .ToList();

            sql3 = sql2.GroupBy(test => test.MaBenh)
                   .Select(grp => grp.First())
                   .ToList();
            for (int i = 0; i < sql3.Count(); i++)
            {
                var list = lstTrieuChungBenhs.Where(x => x.MaBenh == sql3[i].MaBenh).ToList();
                res = 0;
                for (int j = 0; j < list.Count(); j++)
                {
                    for (int p = 0; p < searchString.Count(); p++)
                    {
                        if (list[j].MaTrieuChung != searchString[p].search)
                        {
                            res++;
                            break;
                        }
                    }
                    if (res != searchString.Count())
                    {
                        var benh = lstBenh.SingleOrDefault(x => x.MaBenh == sql3[i].MaBenh);
                        sql4.Add(new BenhDTO
                        {
                            MaBenh = benh.MaBenh,
                            TenBenh = benh.TenBenh,
                            HinhAnh = benh.HinhAnh,
                            NguyenNhan = benh.NguyenNhan,
                        });
                    }
                }
            }



            return sql4 = sql4.Except(entities).GroupBy(test => test.MaBenh)
                 .Select(grp => grp.First())
                 .ToList();
        }
    }
}
