using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.TaiKhoanKhachHang;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace quanlybenh.Services.Implementation
{
    public class KhachHangService : BaseService, IKhachHangService
    {
        private IDataRepository<KhachHang> _khachhangRepository;
        private IDataRepository<TheoDoiThongTin> _thongtinRepository;
        private readonly IMapper _mapper;

        public KhachHangService(
              IDataRepository<KhachHang> khachhangRepository,
        IDataRepository<TheoDoiThongTin> thongtinRepository,
        
          IUnitOfWork unitOfWork,
          IMapper mapper) : base(unitOfWork)
        {
            _thongtinRepository = thongtinRepository;
            _khachhangRepository = khachhangRepository;
            _mapper = mapper;

        }

        public KhachHangDTO GetById(string makhachhang)
        {
            try
            {
                var khachhang = _khachhangRepository.GetMany(p => p.MaKhachHang.ToString().ToLower() == makhachhang.ToLower().Trim()).FirstOrDefault();
                if (khachhang == null)
                {
                    return null;

                }
                var khachhangDto = _mapper.Map<KhachHangDTO>(khachhang);


                // list thông tin
                var _lstThongTins = _thongtinRepository.GetMany(p => p.MaKhachHang == khachhangDto.MaKhachHang).OrderBy(x => x.ThoiGianDanhThuoc).ToList();
                khachhangDto.ListThongTins = _mapper.Map<List<TheoDoiThongTinDTO>>(_lstThongTins);
                khachhangDto.MaThongTins = _lstThongTins.Where(p => p.MaKhachHang == khachhangDto.MaKhachHang)?.Select(p => p.MaThongTin.ToString());

                return khachhangDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
