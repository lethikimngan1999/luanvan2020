namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChungLoai")]
    public partial class ChungLoai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChungLoai()
        {
            BienThes = new HashSet<BienThe>();
        }

        [Key]
        public Guid MaChungLoai { get; set; }

        [StringLength(40)]
        public string TenChungLoai { get; set; }

        public string MauSac { get; set; }

        [StringLength(50)]
        public string MucDoChamSoc { get; set; }

        [StringLength(50)]
        public string CheDoAn { get; set; }

        public string MoTa { get; set; }

        public string TinhCach { get; set; }

        public string DieuKienNuoc { get; set; }

        [StringLength(100)]
        public string KichThuocToiDa { get; set; }

        public string CachChamSoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienThe> BienThes { get; set; }
    }
}
