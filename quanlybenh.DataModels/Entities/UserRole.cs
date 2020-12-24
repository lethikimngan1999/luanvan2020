namespace quanlybenh.DataModels.Entities
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserRoles")]
    public partial class UserRole : IdentityUserRole<Guid>, IBaseEntity
    {
        public DateTime? CreatedDate { get; set; }

        [StringLength(128)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual Role Role { get; set; }

       public virtual User User { get; set; }
    }
}
