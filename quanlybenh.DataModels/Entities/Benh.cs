namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Benh")]
    public partial class Benh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Benh()
        {
          
            ThuocDieuTris = new HashSet<ThuocDieuTri>();
            TrieuChungBenhs = new HashSet<TrieuChungBenh>();
        }

        [Key]
        public Guid MaBenh { get; set; }

        [StringLength(40)]
        public string TenBenh { get; set; }

        public string NguyenNhan { get; set; }

        public string CachDieuTri { get; set; }

        public string MoTa { get; set; }
        public string HinhAnh { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThuocDieuTri> ThuocDieuTris { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrieuChungBenh> TrieuChungBenhs { get; set; }
    }
}
