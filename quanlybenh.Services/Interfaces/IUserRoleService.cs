using quanlybenh.Services.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.Interfaces
{
    public interface IUserRoleService
    {
        List<UserRoleDTO> GetListByUserId(string userId);

        RoleDTO GetByRoleId(string roleId);

        List<UserRoleDTO> GetAll();

        bool Add(List<UserRoleDTO> userRoleDtos);

        bool InsertAll(List<UserRoleDTO> userRoleDataPopups);

    }
}
