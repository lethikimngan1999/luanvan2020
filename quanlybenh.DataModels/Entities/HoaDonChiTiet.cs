namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonChiTiet")]
    public partial class HoaDonChiTiet
    {
        [Key]
        [Column(Order = 0)]
        public Guid MaCa { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MaHoaDon { get; set; }

        public int? SoLuongXuat { get; set; }

        public double? DonGiaXuat { get; set; }

        public virtual Ca Ca { get; set; }

        public virtual HoaDonXuat HoaDonXuat { get; set; }
    }
}
