using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.NhanVien;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Services.Implementation
{
    public class NhanVienService  : BaseService, INhanVienService
    {
    
    
        private IDataRepository<NhanVien> _nhanvienRepository;
        private IDataRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public NhanVienService(
               IDataRepository<NhanVien> nhanvienRepository,
                IDataRepository<User> userRepository,
               IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _nhanvienRepository = nhanvienRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public bool Create(NhanVienDTO nhanvienDto)
        {
            try
            {
                var item = CheckExistsNhanVienByIdentityCardNumber(nhanvienDto.CMND);
                if (item) return false;
                var nhanvien = new NhanVien
                {
                    MaNhanVien = Guid.NewGuid(),
                    HoLot = nhanvienDto.HoLot,
                    TenNhanVien = nhanvienDto.TenNhanVien,
                    CMND = nhanvienDto.CMND,
                    DiaChi = nhanvienDto.DiaChi,
                    NgaySinh = nhanvienDto.NgaySinh,
                    Email = nhanvienDto.Email,
                    Sdt = nhanvienDto.Sdt,
                    GioiTinh = Convert.ToBoolean(nhanvienDto.GioiTinh),
                };

                _nhanvienRepository.Insert(nhanvien);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(NhanVienDTO NhanVienDto)
        {
            try
            {
                var NhanVien = _nhanvienRepository.GetById(NhanVienDto.MaNhanVien);
                // check nhan vien exist
                if (NhanVien == null) return false;

                //Update nhan vien detail
                NhanVien.MaNhanVien = NhanVienDto.MaNhanVien;
                NhanVien.HoLot = NhanVienDto.HoLot;
                NhanVien.TenNhanVien = NhanVienDto.TenNhanVien;
               
                NhanVien.NgaySinh = NhanVienDto.NgaySinh;
                NhanVien.GioiTinh = Convert.ToBoolean(NhanVienDto.GioiTinh);
                NhanVien.CMND = NhanVienDto.CMND;
                NhanVien.Sdt = NhanVienDto.Sdt;
                NhanVien.DiaChi = NhanVienDto.DiaChi;
                NhanVien.Email = NhanVienDto.Email;
            
                // update user
                _nhanvienRepository.Update(NhanVien);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string MaNhanVien)
        {
            try
            {
                var NhanVien = _nhanvienRepository.GetById(new Guid(MaNhanVien));
                if (NhanVien == null) return false;
                _nhanvienRepository.Remove(NhanVien);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public NhanVienDTO GetById(string MaNhanVien)
        {
            try
            {
                var NhanVien = _nhanvienRepository.GetById(new Guid(MaNhanVien));
                if (NhanVien == null) return null;
                var NhanVienObject = _mapper.Map<NhanVienDTO>(NhanVien);

              
                return NhanVienObject;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<NhanVienDTO> GetAll()
        {
            //get tất cả danh sách nhân viên
            var _lstNhanViens = _nhanvienRepository.GetAll().OrderBy(x => x.TenNhanVien).ToList();

            var NhanVienDtos = _mapper.Map<List<NhanVienDTO>>(_lstNhanViens);

            

            return NhanVienDtos;
        }



        public bool CheckExistsNhanVienByIdentityCardNumber(string cmnd)
        {
            var result = _nhanvienRepository.GetAll().Where(x => x.CMND == cmnd.Trim());
            if (result.Any())
            {
                return true;
            }
            return false;
        }

        public List<NhanVienDTO> GetEmployeeNotAccount()
        {
            var _lstEmployees = _nhanvienRepository.GetAll().ToList();
            var _lstUsers = _userRepository.GetMany(p => p.Status != StatusObject.Deleted).ToList();
            var fillteredEmployees = _lstEmployees.Where(a => ! _lstUsers.Select(b => b.MaNhanVien).Contains(a.MaNhanVien));
            var entities = fillteredEmployees.OrderByDescending(c => c.TenNhanVien).ToList();
            var employeeDtos = _mapper.Map<List<NhanVienDTO>>(entities);
            return employeeDtos;
        }
    }

}
