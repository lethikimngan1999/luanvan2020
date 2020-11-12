namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DatHangs = new HashSet<DatHang>();
            HoaDonXuats = new HashSet<HoaDonXuat>();
        }

        [Key]
        public Guid MaKhachHang { get; set; }

        [StringLength(60)]
        public string HoLot { get; set; }

        [StringLength(20)]
        public string TenKhachhang { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(40)]
        public string TinhTP { get; set; }

        [StringLength(12)]
        public string Sdt { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        public string GhiChu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonXuat> HoaDonXuats { get; set; }
    }
}
