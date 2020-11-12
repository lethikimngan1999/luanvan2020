namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonNhap")]
    public partial class DonNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonNhap()
        {
            DonNhapChiTiets = new HashSet<DonNhapChiTiet>();
        }

        [Key]
        public Guid MaDonNhap { get; set; }

        public Guid Id { get; set; }

        public Guid MaCungCap { get; set; }

        public DateTime? NgayNhap { get; set; }

        public virtual NoiCungCap NoiCungCap { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonNhapChiTiet> DonNhapChiTiets { get; set; }
    }
}
