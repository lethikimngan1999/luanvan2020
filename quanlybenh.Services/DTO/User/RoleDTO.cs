using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.User
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<RolePermissionBaseDTO> ListRolePermissions { get; set; }
        public IEnumerable<RoleDTO> ListRoles { get; set; }
    }
}
