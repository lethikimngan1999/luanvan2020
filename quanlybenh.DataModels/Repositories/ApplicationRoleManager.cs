using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using quanlybenh.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.DataModels.Repositories
{
    public class RoleStore : RoleStore<Role, Guid, IdentityUserRole<Guid>>, IRoleStore<Role, Guid>
    {
        public RoleStore(AppDbContext context) : base(context)
        {
        }
    }

    public class ApplicationRoleManager : RoleManager<Role, Guid>
    {
        public ApplicationRoleManager(IRoleStore<Role, Guid> roleStore) : base(roleStore)
        {
        }
    }
}
