using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.NhanVien;
using quanlybenh.Services.DTO.User;
using quanlybenh.Services.Interfaces;
using quanlybenh.Utilities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static quanlybenh.Utilities.Configurations.Constants;

namespace quanlybenh.Services.Implementation
{
    public class UserService : BaseService, IUserService
    {
        private IDataRepository<User> _userRepository;
        private IDataRepository<Role> _roleRepository;
        private IDataRepository<UserRole> _userRoleRepository;
        private IDataRepository<NhanVien> _nhanvienRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager _userManager;
        private INhanVienService _nhanvienService;

        public UserService(
            ApplicationUserManager userManager,
            IDataRepository<User> userRepository,
                   IDataRepository<NhanVien> nhanvienRepository,
                   IDataRepository<Role> roleRepository,
                   IDataRepository<UserRole> userRoleRepository,
                            INhanVienService nhanvienService,


            IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _userRepository = userRepository;
            _nhanvienRepository = nhanvienRepository;
            _nhanvienService = nhanvienService;
            _userManager = userManager;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public UserDTO GetById(string userId)
        {
            try
            {
                var user = _userRepository.GetById(new Guid(userId));
                if (user == null)
                {
                    return null;
                }
                var userDto = _mapper.Map<UserDTO>(user);
                ////get thong tin nhan vien
                //var nhanvien = _nhanvienRepository.GetById(userDto.MaNhanVien);
                //userDto.Nhanvien = _mapper.Map<NhanVienDTO>(nhanvien);

                //// get danh sach vai tro user

                //var _lstUserRoles = _userRoleRepository.GetMany(p => p.UserId == user.Id).ToList();
                //var _lstRoles = _roleRepository.GetAll().ToList();

                //var sql = from role in _lstRoles
                //          join userRole in _lstUserRoles on role.Id equals userRole.RoleId
                //          select role;

                //var entities = sql.OrderByDescending(c => c.Name).ToList();
                //userDto.ListRoles = _mapper.Map<List<RoleDTO>>(entities);

                //// get userRole
                //userDto.RoleIds = _lstUserRoles.Where(p => p.UserId == user.Id)?.Select(p => p.RoleId.ToString());

                return userDto;

            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public UserDTO GetByMaNhanVien(string manhanvien)
        {
            try
            {
                var user = _userRepository.GetMany(p => p.MaNhanVien.ToString().ToLower() == manhanvien.ToLower().Trim()).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                var userDto = _mapper.Map<UserDTO>(user);
                return userDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserDTO GetByUserName(string username)
        {
            try
            {
                var user = _userRepository.GetMany(p => p.UserName.ToLower() == username.ToLower().Trim()).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                var userDto = _mapper.Map<UserDTO>(user);
                return userDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool CreateNhanVienAccount(UserDTO registerUserDto)
        {
            try
            {
                var item = IsExistedUserByMaNhanVien(registerUserDto.MaNhanVien.ToString());
                if (item)
                {
                    return false;
                }
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = registerUserDto.UserName.ToLower(),
                    PasswordHash = _userManager.PasswordHasher.HashPassword(AppSettings.DefaultPassword),
                    MaNhanVien = new Guid(registerUserDto.MaNhanVien.ToString().ToLower()),
                    CreatedBy = GetCurrentUserId(),
                    CreatedDate = DateTime.Now,
                    UpdatedBy = registerUserDto.UpdatedBy,
                    UpdatedDate = registerUserDto.UpdatedDate,
                    Status = registerUserDto.Status

                };
                _userRepository.Insert(user);
                _unitOfWork.Commit();
                if (registerUserDto.RoleIds != null)
                {
                    foreach (var roleId in registerUserDto.RoleIds)
                    {
                        var userRole = new UserRole { UserId = user.Id, RoleId = new Guid(roleId) };
                        _userRoleRepository.Insert(userRole);
                    }
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public bool Update(UserDTO userDto)
        {
            try
            {
                var user = _userRepository.GetById(userDto.Id);
                if (user == null)
                {
                    return false;
                }
                // Update chi tiet user
                user.Id = userDto.Id;
                user.UserName = userDto.UserName.ToLower();
            //    user.PasswordHash = _userManager.PasswordHasher.HashPassword(userDto.Password);
                user.MaNhanVien = userDto.MaNhanVien;
                user.CreatedBy = user.CreatedBy;
                user.CreatedDate = user.CreatedDate;

                user.UpdatedBy = GetCurrentUserId();
                user.UpdatedDate = DateTime.Now;
                user.Status = userDto.Status;
                //update UserRole
                //var userRoleOld = _userRoleRepository.GetMany(p => p.UserId == user.Id).ToList();
                //_userRoleRepository.RemoveMultiple(userRoleOld);

                //var roles = _roleRepository.GetMany(r => userDto.RoleIds.Contains(r.Id.ToString()));
                //foreach (var role in roles)
                //{
                //    var userRole = new UserRole { UserId = user.Id, RoleId = role.Id };
                //    userRole.UpdatedBy = GetCurrentUserId();
                //    userRole.UpdatedDate = DateTime.Now;
                //    _userRoleRepository.Insert(userRole);
                //}

                // update user
                _userRepository.Update(user);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string userId)
        {
            try
            {
                var user = _userRepository.GetById(new Guid(userId));
                if (user == null)
                {
                    return false;
                }
                // update user table
                user.UpdatedDate = DateTime.Now;
                user.UpdatedBy = GetCurrentUserId();
                user.Status = StatusObject.Deleted;

                _userRepository.Update(user);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool IsExistedUserById(string userId)
        {
            return _userRepository.GetMany(p => p.Id.ToString().Trim().ToLower() == userId.Trim().ToLower()).Any();
        }

        public bool IsExistedUserByMaNhanVien(string manhanvien)
        {
            return _userRepository.GetMany(p => p.MaNhanVien.ToString().Trim().ToLower() == manhanvien.Trim().ToLower()).Any();
        }

        public bool IsExistedUserName(string username)
        {
            return _userRepository.GetMany(p => p.UserName.Trim().ToLower() == username.Trim().ToLower()).Any();
        }

        public bool UpdateStatus(string userId, string status)
        {
            try
            {
                var user = _userRepository.GetById(new Guid(userId));
                if (user != null)
                {
                    user.Status = status;
                    user.UpdatedBy = GetCurrentUserId();
                    user.UpdatedDate = DateTime.Now;
                    _userRepository.Update(user);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<UserDTO> GetAllUser()
        {

            var _lstUsers = _userRepository.GetMany(p => p.Status != StatusObject.Deleted).OrderBy(p => p.CreatedDate).ToList();
            var userDtos = _mapper.Map<List<UserDTO>>(_lstUsers);
            var entities = new List<Role>();

            foreach (var user in userDtos)
            {
                //get thong tin nhan vien
                var nhanvien = _nhanvienRepository.GetById(user.MaNhanVien);
                user.Nhanvien = _mapper.Map<NhanVienDTO>(nhanvien);

                // get danh sach vai tro user

                var _lstUserRoles = _userRoleRepository.GetMany(p => p.UserId == user.Id).ToList();
                var _lstRoles = _roleRepository.GetAll().ToList();

                var sql = from role in _lstRoles
                          join userRole in _lstUserRoles on role.Id equals userRole.RoleId
                          select role;

                entities = sql.OrderByDescending(c => c.Name).ToList();
                user.ListRoles = _mapper.Map<List<RoleDTO>>(entities);

                // get userRole
                user.RoleIds = _lstUserRoles.Where(p => p.UserId == user.Id)?.Select(p => p.RoleId.ToString());
            }
            return userDtos;

            //var userDtos = _userRepository.GetMany(p => p.Status != StatusObject.Deleted).OrderBy(p => p.CreatedDate).ToList();

            //var results = _mapper.Map<List<UserDTO>>(userDtos);
            //return results;
        }

        public async Task<bool> ChangePassword(string userId, string newPassword, string oldPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(oldPassword))
                {
                    return false;
                }
                //Check user exist
                var userObject = _userRepository.GetById(new Guid(userId));
                if (userObject == null)
                {
                    return false;
                }
                // check pass
                var isPasswordCorrect = await _userManager.CheckPasswordAsync(userObject, oldPassword);
                if (isPasswordCorrect)
                {
                    var newPasswordHash = _userManager.PasswordHasher.HashPassword(newPassword);
                    if (newPasswordHash == null)
                    {
                        return false;
                    }
                    userObject.PasswordHash = newPasswordHash;

                    _userRepository.Update(userObject);
                    _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ResetPassword(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return false;
                }
                // check user exist
                var userObject = _userRepository.GetById(new Guid(userId));
                if (userObject == null)
                {
                    return false;
                }
                // check password
                if (userObject != null)
                {
                    var newPassword = AppSettings.DefaultPassword;
                    userObject.PasswordHash = _userManager.PasswordHasher.HashPassword(newPassword);
                    userObject.UpdatedBy = GetCurrentUserId();
                    userObject.UpdatedDate = DateTime.Now;
                    _userRepository.Update(userObject);
                    _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            { return false; }
        }

        public async Task<bool> CheckPassword(string username, string password)
        {
            var user = _userRepository.GetMany(p => p.UserName.ToLower() == username.ToLower().Trim()).FirstOrDefault();
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false);
                return result;
            }
            return false;
        }

        public List<UserDTO> GetAllUserAccountActive()
        {
            var _lstUsers = _userRepository.GetMany(p => p.Status == StatusObject.Active).OrderByDescending(p => p.CreatedDate).ToList();
            var userDtos = _mapper.Map<List<UserDTO>>(_lstUsers);
            var entities = new List<Role>();

            foreach (var user in userDtos)
            {
                //get thong tin nhan vien
                var nhanvien = _nhanvienRepository.GetById(user.MaNhanVien);
                user.Nhanvien = _mapper.Map<NhanVienDTO>(nhanvien);

                // get danh sach vai tro user

                var _lstUserRoles = _userRoleRepository.GetMany(p => p.UserId == user.Id).ToList();
                var _lstRoles = _roleRepository.GetAll().ToList();

                var sql = from role in _lstRoles
                          join userRole in _lstUserRoles on role.Id equals userRole.RoleId
                          select role;

                entities = sql.OrderByDescending(c => c.Name).ToList();
                user.ListRoles = _mapper.Map<List<RoleDTO>>(entities);

                // get userRole
                user.RoleIds = _lstUserRoles.Where(p => p.UserId == user.Id)?.Select(p => p.RoleId.ToString());
            }
            return userDtos;
        }

        public List<UserDTO> GetAllUserAccountLocked()
        {
            var _lstUsers = _userRepository.GetMany(p => p.Status == StatusObject.Locked).OrderByDescending(p => p.CreatedDate).ToList();
            var userDtos = _mapper.Map<List<UserDTO>>(_lstUsers);
            var entities = new List<Role>();

            foreach (var user in userDtos)
            {
                //get thong tin nhan vien
                var employee = _nhanvienRepository.GetById(user.MaNhanVien);
                user.Nhanvien = _mapper.Map<NhanVienDTO>(employee);

                // get danh sach vai tro user

                var _lstUserRoles = _userRoleRepository.GetMany(p => p.UserId == user.Id).ToList();
                var _lstRoles = _roleRepository.GetAll().ToList();

                var sql = from role in _lstRoles
                          join userRole in _lstUserRoles on role.Id equals userRole.RoleId
                          select role;

                entities = sql.OrderByDescending(c => c.Name).ToList();
                user.ListRoles = _mapper.Map<List<RoleDTO>>(entities);

                // get userRole
                user.RoleIds = _lstUserRoles.Where(p => p.UserId == user.Id)?.Select(p => p.RoleId.ToString());
            }
            return userDtos;
        }
    }
    }
