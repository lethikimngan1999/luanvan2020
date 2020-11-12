using quanlybenh.Services.DTO.Base;
using quanlybenh.Services.DTO.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.User
{
   public class RolePermissionDTO : BaseTableDTO
    {
        public RolePermissionDTO()
        {
        }
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public Guid MenuId { get; set; }

        public string MenuName { get; set; }

        public bool CanRead { set; get; }

        public bool CanCreate { set; get; }

        public bool CanUpdate { set; get; }

        public bool CanDelete { set; get; }

        public bool CanImport { set; get; }

        public bool CanExport { set; get; }

        public virtual RoleDTO Role { get; set; }

        public virtual MenuDTO Menu { get; set; }

        public List<RolePermissionDTO> ListRolePermissions { get; set; }
    }

    public class RolePermissionBaseDTO
    {
        public Guid MenuId { get; set; }

        public bool CanRead { get; set; }

        public bool CanCreate { get; set; }

        public bool CanUpdate { get; set; }

        public bool CanDelete { get; set; }

        public bool CanImport { get; set; }

        public bool CanExport { get; set; }
    }

}
