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
    }
}
