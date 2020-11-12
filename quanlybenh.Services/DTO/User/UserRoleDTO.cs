using Microsoft.AspNet.Identity.EntityFramework;
using quanlybenh.Services.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Services.DTO.User
{
    public class UserRoleDTO: IdentityUserRole<Guid>, IBaseDTO
    {
        public UserDTO User { get; set; }

        public RoleDTO Role { get; set; }

        public string Status { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
