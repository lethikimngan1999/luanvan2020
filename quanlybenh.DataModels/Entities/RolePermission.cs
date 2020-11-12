namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RolePermissions")]
    public partial class RolePermission : BaseEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid RoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MenuId { get; set; }

        public bool? CanRead { get; set; }

        public bool? CanCreate { get; set; }

        public bool? CanUpdate { get; set; }

        public bool? CanDelete { get; set; }

        public bool? CanImport { get; set; }

        public bool? CanExport { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
