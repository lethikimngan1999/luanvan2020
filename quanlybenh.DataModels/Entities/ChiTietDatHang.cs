namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDatHang")]
    public partial class ChiTietDatHang
    {
        [Key]
        [Column(Order = 0)]
        public Guid MaCa { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MaDatHang { get; set; }

        public int? SoLuong { get; set; }

        public double? GiaBan { get; set; }

        public virtual Ca Ca { get; set; }

        public virtual DatHang DatHang { get; set; }
    }
}
