using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.User;
using quanlybenh.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Implementation
{
    public class UserRoleService : BaseService, IUserRoleService
    {
        private IDataRepository<UserRole> _userRoleRepository;
        private IDataRepository<Role> _roleRepository;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserRoleService(
            IDataRepository<UserRole> userRoleRepository,
            IDataRepository<Role> roleRepository,
            IUnitOfWork unitOfWork,
            IUserService userService,
            IRoleService roleService,
            IMapper mapper) :base (unitOfWork)

        {
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }


        public bool Add(List<UserRoleDTO> userRoleDtos)
        {
           try
            {
                var userId = userRoleDtos.FirstOrDefault().UserId;
                var userRoles = _userRoleRepository.GetAll().Where(p => p.UserId == userId).ToList();
                var allUserRole = userRoles.Select(p => new { p.RoleId, p.UserId }).ToList();
                var allUserRoleDto = userRoleDtos.Select(p => new { p.RoleId, p.UserId });

                var userRoleRemove = allUserRole.Except(allUserRoleDto).ToList();
                var userRoleInsert = allUserRoleDto.Except(allUserRole).ToList();

                // remove role

                foreach (var item in userRoleRemove)
                {
                    var userRole = userRoles.Where(p => p.RoleId == item.RoleId).FirstOrDefault();
                    _userRoleRepository.Remove(userRole);
                }

                // insert role

                foreach (var item in userRoleInsert)
                {
                    var userRoleDto = userRoleDtos.Where(p => p.RoleId == item.RoleId).FirstOrDefault();
                    var userRole = _mapper.Map<UserRole>(userRoleDto);
                    userRole.CreatedBy = GetCurrentUserId();
                    userRole.UpdatedBy = GetCurrentUserId();
                    userRole.CreatedDate = DateTime.Now;
                    userRole.UpdatedDate = DateTime.Now;

                    _userRoleRepository.Insert(userRole);
                }_unitOfWork.Commit();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
           
        }

        public List<UserRoleDTO> GetAll()
        {
            var lstUserRoles = _userRoleRepository.GetAll().ToList();
            var lstUserRoleDtos = _mapper.Map<List<UserRoleDTO>>(lstUserRoles);
            foreach( var item in lstUserRoleDtos)
            {
                var user = _userService.GetById(item.UserId.ToString());
                item.User = _mapper.Map<UserDTO>(user);

                var role = _roleService.GetById(item.RoleId.ToString());
                item.Role = _mapper.Map<RoleDTO>(role);

            }
            return lstUserRoleDtos;
        }

        public RoleDTO GetByRoleId(string roleId)
        {
            var entity = _roleRepository.GetById(new Guid(roleId));
            var result = _mapper.Map<RoleDTO>(entity);
            return result;
        }

        public List<UserRoleDTO> GetListByUserId(string userId)
        {
            var lstUserRoles = _userRoleRepository.GetMany(p => p.UserId == new Guid(userId)).ToList();
            var userRoleDtos = _mapper.Map<List<UserRoleDTO>>(lstUserRoles);
            foreach(var item in userRoleDtos)
            {
                var user = _userService.GetById(item.UserId.ToString());
                item.User = _mapper.Map<UserDTO>(user);

                var role = _roleService.GetById(item.RoleId.ToString());
                item.Role = _mapper.Map<RoleDTO>(role);
            }
            return userRoleDtos;
        }

        public bool InsertAll(List<UserRoleDTO> userRoleDataPopups)
        {
            throw new NotImplementedException();
        }
    }
}
