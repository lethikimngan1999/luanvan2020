using quanlybenh.Services.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface IRoleService
    {
        bool Create(RoleDTO roleDto);

        bool Add(RoleDTO roleDto);
        bool Delete(Guid roleId);
        bool Update(RoleDTO roleDto);
        List<RoleDTO> GetAll();
        RoleDTO GetById(string roleId);
        bool UpdateStatus(Guid roleId, string Status);
        bool CheckExistsRoleName(string roleName);
        
    }
}
