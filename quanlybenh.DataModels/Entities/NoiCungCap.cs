namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NoiCungCap")]
    public partial class NoiCungCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoiCungCap()
        {
            DonNhaps = new HashSet<DonNhap>();
        }

        [Key]
        public Guid MaCungCap { get; set; }

        [StringLength(50)]
        public string TenNoiCungCap { get; set; }

        [StringLength(40)]
        public string TinhTP { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(12)]
        public string Sdt { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonNhap> DonNhaps { get; set; }
    }
}
