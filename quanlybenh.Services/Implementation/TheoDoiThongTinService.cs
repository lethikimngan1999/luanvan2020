using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.TaiKhoanKhachHang;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Implementation
{
    public class TheoDoiThongTinService : BaseService, ITheoDoiThongTinService
    {
        private IDataRepository<KhachHang> _khachhangRepository;
        private IDataRepository<TheoDoiThongTin> _thongtinRepository;
        private readonly IMapper _mapper;

        public TheoDoiThongTinService(
           IDataRepository<KhachHang> khachhangRepository,
   IDataRepository<TheoDoiThongTin> thongtinRepository,

        IUnitOfWork unitOfWork,
         IMapper mapper) : base(unitOfWork)
        {
            _khachhangRepository = khachhangRepository;
            _thongtinRepository = thongtinRepository;
            _mapper = mapper;

        }

        public bool Create(TheoDoiThongTinDTO thongtinDto)
        {
            try
            {

                var thongtin = new TheoDoiThongTin
                {
                    MaThongTin = Guid.NewGuid(),
                    MaKhachHang = thongtinDto.MaKhachHang,
                    TenThongTin = thongtinDto.TenThongTin,
                    TenBenh = thongtinDto.TenBenh,
                    TenThuoc = thongtinDto.TenThuoc,

                    ThoiGianDanhThuoc = thongtinDto.ThoiGianDanhThuoc,
                    TrieuChung = thongtinDto.TrieuChung,
                    KetQua = thongtinDto.KetQua,
                    GhiChu = thongtinDto.GhiChu
                };
                _thongtinRepository.Insert(thongtin);
                _unitOfWork.Commit();


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<TheoDoiThongTinDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(TheoDoiThongTinDTO thongtinDto)
        {
            try
            {
                var thongtin = _thongtinRepository.GetById(thongtinDto.MaThongTin);
                // check nhan vien exist
                if (thongtin == null) return false;

                //Update nhan vien detail
                thongtin.MaThongTin = thongtinDto.MaThongTin;
                thongtin.MaKhachHang = thongtinDto.MaKhachHang;
                thongtin.TenThongTin = thongtinDto.TenThongTin;
                thongtin.TenBenh = thongtinDto.TenBenh;
                thongtin.TenThuoc = thongtinDto.TenThuoc;

                thongtin.ThoiGianDanhThuoc = thongtinDto.ThoiGianDanhThuoc;
                thongtin.TrieuChung = thongtinDto.TrieuChung;
                thongtin.KetQua = thongtinDto.KetQua;
                thongtin.GhiChu = thongtinDto.GhiChu;
               
                _thongtinRepository.Update(thongtin);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string mathongtin)
        {
            try
            {
                var thongtin = _thongtinRepository.GetById(new Guid(mathongtin));
                if (thongtin == null) return false;
                _thongtinRepository.Remove(thongtin);
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
