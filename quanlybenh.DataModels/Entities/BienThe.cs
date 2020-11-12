namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BienThe")]
    public partial class BienThe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BienThe()
        {
            Cas = new HashSet<Ca>();
            HinhAnhBienThes = new HashSet<HinhAnhBienThe>();
        }

        [Key]
        public Guid MaBienThe { get; set; }

        public Guid MaGiong { get; set; }

        public Guid MaChatLuong { get; set; }

        public Guid MaChungLoai { get; set; }

        [StringLength(40)]
        public string TenBienThe { get; set; }

        public string MauSac { get; set; }

        public bool? TinhTrang { get; set; }

        public string MoTa { get; set; }

        public virtual ChatLuong ChatLuong { get; set; }

        public virtual Giong Giong { get; set; }

        public virtual ChungLoai ChungLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ca> Cas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnhBienThe> HinhAnhBienThes { get; set; }
    }
}
