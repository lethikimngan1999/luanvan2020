using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.BienThe;
using quanlybenh.Services.DTO.HinhAnh;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Services.Implementation
{
    public class BienTheService : BaseService, IBienTheService
    {

        private IDataRepository<BienThe> _bientheRepository;
        private IDataRepository<Giong> _giongRepository;
        private IDataRepository<ChatLuong> _chatluongRepository;
        private IDataRepository<ChungLoai> _chungloaiRepository;
        private IDataRepository<HinhAnhBienThe> _hinhanhRepository;

        private readonly IMapper _mapper;
        public BienTheService(
            IDataRepository<BienThe> bientheRepository,
            IDataRepository<Giong> giongRepository,
            IDataRepository<ChatLuong> chatluongRepository,
            IDataRepository<ChungLoai> chungloaiRepository,
            IDataRepository<HinhAnhBienThe> hinhanhRepository,

           IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _bientheRepository = bientheRepository;
            _giongRepository = giongRepository;
            _chatluongRepository = chatluongRepository;
            _chungloaiRepository = chungloaiRepository;
            _hinhanhRepository = hinhanhRepository;
            _mapper = mapper;
        }
        public List<BienTheDTO> GetAll()
        {
            //get tất cả danh sách
            var _lstBienthes = _bientheRepository.GetAll().OrderBy(x => x.TenBienThe).ToList();

            var bientheDtos = _mapper.Map<List<BienTheDTO>>(_lstBienthes);

            var entities = new List<HinhAnhBienThe>();

            foreach (var item in bientheDtos)
            {
                //get thông tin giong
                var giong = _giongRepository.GetById(item.MaGiong);
                item.Giongs = _mapper.Map<GiongDTO>(giong);

                //get thông tin chung loai
                var chungloai = _chungloaiRepository.GetById(item.MaChungLoai);
                item.ChungLoais = _mapper.Map<ChungLoaiDTO>(chungloai);

                //get thông tin chat luong
                var chatluong = _chatluongRepository.GetById(item.MaChatLuong);
                item.ChatLuongs = _mapper.Map<ChatLuongDTO>(chatluong);

                // get hinh anh
                
                var hinhanh = _hinhanhRepository.GetMany(p => p.MaBienThe == item.MaBienThe).ToList();

                var sql = from ha in hinhanh
                          where ha.ChonAvt == true
                          select ha;

                entities = sql.OrderByDescending(c => c.NgayTao).ToList();

                item.Listhas = _mapper.Map<List<HinhAnhBienTheDTO>>(entities);

              
                 item.Mahas = entities.Where(p => p.MaBienThe == item.MaBienThe).Select(p => p.DuongDan).First();
            }

            return bientheDtos;
        }

        public bool Create(BienTheDTO bientheDto)
        {
            var item = CheckExistsTen(bientheDto.TenBienThe);
            if (item) return false;
            var bienthe = new BienThe
            {
                MaBienThe = Guid.NewGuid(),
                TenBienThe = bientheDto.TenBienThe,
                MaChatLuong = bientheDto.MaChatLuong,
                MaGiong = bientheDto.MaGiong,
                MauSac = bientheDto.MauSac,
                MaChungLoai = bientheDto.MaChungLoai,
            
                TinhTrang = Convert.ToBoolean(bientheDto.TinhTrang),
                MoTa = bientheDto.MoTa
        };
            _bientheRepository.Insert(bienthe);
            _unitOfWork.Commit();  
            return true;
        }

        public bool CheckExistsTen(string ten)
        {
            var result = _bientheRepository.GetAll().Where(x => x.TenBienThe == ten.Trim());
            if (result.Any())
            {
                return true;
            }
            return false;
        }

        public bool Update(BienTheDTO bientheDto)
        {
            try
            {
                var bienthe = _bientheRepository.GetById(bientheDto.MaBienThe);
                if (bienthe == null)
                {
                    return false;
                }
                // Update chi tiet 
                bienthe.MaBienThe = bientheDto.MaBienThe;
                bienthe.TenBienThe = bientheDto.TenBienThe.ToLower();
                bienthe.MaChungLoai = bientheDto.MaChungLoai;
                bienthe.MaChatLuong = bientheDto.MaChatLuong;
                bienthe.MaGiong = bientheDto.MaGiong;
                bienthe.MoTa = bientheDto.MoTa;
                bienthe.MauSac = bientheDto.MauSac;
                bienthe.TinhTrang = bientheDto.TinhTrang;
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
                _bientheRepository.Update(bienthe);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<BienTheDTO> GetListAll()
        {
            //get tất cả danh sách
            var _lstBienthes = _bientheRepository.GetAll().OrderBy(x => x.TenBienThe).ToList();

            var bientheDtos = _mapper.Map<List<BienTheDTO>>(_lstBienthes);

            var entities = new List<HinhAnhBienThe>();

            foreach (var item in bientheDtos)
            {
                //get thông tin giong
                var giong = _giongRepository.GetById(item.MaGiong);
                item.Giongs = _mapper.Map<GiongDTO>(giong);

                //get thông tin chung loai
                var chungloai = _chungloaiRepository.GetById(item.MaChungLoai);
                item.ChungLoais = _mapper.Map<ChungLoaiDTO>(chungloai);

                //get thông tin chat luong
                var chatluong = _chatluongRepository.GetById(item.MaChatLuong);
                item.ChatLuongs = _mapper.Map<ChatLuongDTO>(chatluong);

                // get hinh anh

                var hinhanh = _hinhanhRepository.GetMany(p => p.MaBienThe == item.MaBienThe && p.ChonAvt==true).OrderByDescending(p=>p.NgayTao).ToList();
               
                //var sql = from ha in hinhanh
                //          where ha.ChonAvt == true
                //          select ha;

                //entities = sql.OrderByDescending(c => c.NgayTao).ToList();

               // item.Listhas = _mapper.Map<List<HinhAnhBienTheDTO>>(entities);

                if (hinhanh.Count() >0)
                {
                    foreach(var ha in hinhanh.Take(1))
                    {
                        item.Mahas = ha.DuongDan;
                    }
                 

                }
            }

            return bientheDtos;
        }

        public BienTheDTO GetById(string mabienthe)
        {
            try
            {
                var _lst = _bientheRepository.GetMany(p => p.MaBienThe.ToString().ToLower() == mabienthe.ToLower().Trim()).ToList().FirstOrDefault();
                if (_lst == null)
                {
                    return null;
                }
                var bientheDto = _mapper.Map<BienTheDTO>(_lst);

                var entities = new List<HinhAnhBienThe>();

              
                    //get thông tin giong
                    var giong = _giongRepository.GetById(bientheDto.MaGiong);
                bientheDto.Giongs = _mapper.Map<GiongDTO>(giong);

                    //get thông tin chung loai
                    var chungloai = _chungloaiRepository.GetById(bientheDto.MaChungLoai);
                bientheDto.ChungLoais = _mapper.Map<ChungLoaiDTO>(chungloai);

                    //get thông tin chat luong
                    var chatluong = _chatluongRepository.GetById(bientheDto.MaChatLuong);
                bientheDto.ChatLuongs = _mapper.Map<ChatLuongDTO>(chatluong);

                    // get hinh anh

                    var hinhanh = _hinhanhRepository.GetMany(p => p.MaBienThe == bientheDto.MaBienThe).ToList();

                    var sql = from ha in hinhanh
                              where ha.ChonAvt == true
                              select ha;

                    entities = sql.OrderByDescending(c => c.NgayTao).ToList();

                bientheDto.Listhas = _mapper.Map<List<HinhAnhBienTheDTO>>(entities);


                bientheDto.Mahas = entities.Where(p => p.MaBienThe == bientheDto.MaBienThe).Select(p => p.DuongDan).First();
                

                return bientheDto;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<BienTheDTO> GetListOfChungLoai(string machungloai)
        {
            try
            {
                var _lst = _bientheRepository.GetMany(p => p.MaChungLoai.ToString().ToLower() == machungloai.ToLower().Trim()).ToList();
                if (_lst == null)
                {
                    return null;
                }
                var bientheDto = _mapper.Map< List<BienTheDTO>>(_lst);

                var entities = new List<HinhAnhBienThe>();


                foreach (var item in bientheDto)
                {
                    //get thông tin giong
                    var giong = _giongRepository.GetById(item.MaGiong);
                    item.Giongs = _mapper.Map<GiongDTO>(giong);

                    //get thông tin chung loai
                    var chungloai = _chungloaiRepository.GetById(item.MaChungLoai);
                    item.ChungLoais = _mapper.Map<ChungLoaiDTO>(chungloai);

                    //get thông tin chat luong
                    var chatluong = _chatluongRepository.GetById(item.MaChatLuong);
                    item.ChatLuongs = _mapper.Map<ChatLuongDTO>(chatluong);

                    // get hinh anh

                    var hinhanh = _hinhanhRepository.GetMany(p => p.MaBienThe == item.MaBienThe && p.ChonAvt == true).OrderByDescending(p => p.NgayTao).ToList();

                    //var sql = from ha in hinhanh
                    //          where ha.ChonAvt == true
                    //          select ha;

                    //entities = sql.OrderByDescending(c => c.NgayTao).ToList();

                    // item.Listhas = _mapper.Map<List<HinhAnhBienTheDTO>>(entities);

                    if (hinhanh.Count() > 0)
                    {
                        foreach (var ha in hinhanh.Take(1))
                        {
                            item.Mahas = ha.DuongDan;
                        }
                    }
                }
                return bientheDto;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
