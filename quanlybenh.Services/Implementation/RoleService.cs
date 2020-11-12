using AutoMapper;
using quanlybenh.DataModels.Entities;
using quanlybenh.DataModels.Repositories;
using quanlybenh.DataModels.UnitOfWork;
using quanlybenh.Services.DTO.User;
using quanlybenh.Services.Interfaces;
using quanlybenh.Utilities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Implementation
{
    public class RoleService : BaseService, IRoleService
    {
        private IDataRepository<Menu> _menuRepository;
        private IDataRepository<Role> _roleRepository;
        private ApplicationRoleManager _roleManager;
        private readonly IMapper _mapper;
            
        public RoleService(
            IDataRepository<Menu> menuRepository,
            IDataRepository<Role> roleRepository,
            ApplicationRoleManager roleManager,
            IUnitOfWork unitOfWork, IMapper mapper) :base (unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = menuRepository;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _mapper = mapper;

        }
            
        public bool Add(RoleDTO roleDto)
        {
           try
            {
                var checkNameExits = _roleManager.FindByNameAsync(roleDto.Name);
                if(checkNameExits.Result != null)
                {
                    return false;
                }
                var role = _mapper.Map<Role>(roleDto);
                role.CreatedBy = GetCurrentUserId();
                role.CreatedDate = DateTime.Now;

                _roleRepository.Insert(role);
                _unitOfWork.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Create(RoleDTO entity)
        {
          try
            {
                var item = CheckExistsRoleName(entity.Name);
                if(item)
                {
                    return false;
                }
                var role = _mapper.Map<Role>(entity);
                role.Id = Guid.NewGuid();
                role.CreatedBy = GetCurrentUserId();
                role.CreatedDate = DateTime.Now;
                _roleRepository.Insert(role);
                _unitOfWork.Commit();
                return true;
            }catch(Exception e)
            {
                return false;
            }        
        }

        public bool Delete(Guid roleId)
        {
           
            try
            {
                var role = _roleRepository.GetById(roleId);
                if(role == null)
                {
                    return false;
                }
                role.Status = Constants.StatusObject.Deleted;
                role.UpdatedBy = GetCurrentUserId();
                role.UpdatedDate = DateTime.Now;

                _roleRepository.Update(role);
                _unitOfWork.Commit();
                return true;
            }catch
            {
                return false;
            }
        }

        public List<RoleDTO> GetAll()
        {
            var lstRoles = _roleRepository.GetAll().Where(p => p.Status != Constants.StatusObject.Deleted);
            var roleDtos = _mapper.Map<List<RoleDTO>>(lstRoles);
            return roleDtos;
        }

        public RoleDTO GetById(string roleId)
        {
            var role = _roleRepository.GetById(new Guid(roleId));
            if(role == null)
            {
                return null;
            }
            return _mapper.Map<RoleDTO>(role);
        }

        public bool Update(RoleDTO roleDto)
        {
            try
            {
                var role = _roleRepository.GetById(roleDto.Id);
                if (role == null)
                {
                    return false;
                }
                role.Name = roleDto.Name;
                role.Description = roleDto.Description;
                role.Status = roleDto.Status;
                role.UpdatedBy = GetCurrentUserId();
                role.UpdatedDate = DateTime.Now;
                _roleRepository.Update(role);
                _unitOfWork.Commit();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool CheckExistsRoleName(string roleName)
        {
            var result = _roleRepository.GetAll().Where(x => x.Name.ToLower() == roleName.Trim().ToLower());
            if(result.Any())
            {
                return true;
            }
            return false;
  
        }

        public bool UpdateStatus(Guid roleId, string Status)
        {
            try
            {
                var role = _roleRepository.GetById(roleId);
                role.Status = Status;
                role.UpdatedBy = GetCurrentUserId();
                role.UpdatedDate = DateTime.Now;

                _roleRepository.Update(role);
                _unitOfWork.Commit();
                return true;
            }catch
            {
                return false;
            }
        }
    }
}
