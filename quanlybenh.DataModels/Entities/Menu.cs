namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Menu : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MenuId { get; set; }

        [StringLength(128)]
        public string MenuName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(128)]
        public string MenuHref { get; set; }
        [StringLength(128)]
        public string MenuTitle { get; set; }
        public int Level { get; set; }
        [StringLength(20)]
        public string Icon { get; set; }
        public bool? IsOpened { get; set; }
        public bool? IsSelected { get; set; }
        public bool? IsDisable { get; set; }
        public Guid MenuParentId { get; set; }
        public int OrderNumber { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
