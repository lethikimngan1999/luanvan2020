namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ca")]
    public partial class Ca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ca()
        {
            ChiTietDatHangs = new HashSet<ChiTietDatHang>();
            DonNhapChiTiets = new HashSet<DonNhapChiTiet>();
            HinhAnhCas = new HashSet<HinhAnhCa>();
            HoaDonChiTiets = new HashSet<HoaDonChiTiet>();
        }

        [Key]
        public Guid MaCa { get; set; }

        public Guid MaBienThe { get; set; }

        [StringLength(40)]
        public string TenCa { get; set; }

        public int? GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(20)]
        public string KichThuoc { get; set; }

        public DateTime? NgayDo { get; set; }

        public double? DonGia { get; set; }

        [StringLength(255)]
        public string Tuoi { get; set; }
        public bool? TinhTrang { get; set; }

        public virtual BienThe BienThe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonNhapChiTiet> DonNhapChiTiets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnhCa> HinhAnhCas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}
