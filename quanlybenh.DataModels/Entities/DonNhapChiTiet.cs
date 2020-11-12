namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonNhapChiTiet")]
    public partial class DonNhapChiTiet
    {
        [Key]
        [Column(Order = 0)]
        public Guid MaCa { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MaDonNhap { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGiaNhap { get; set; }

        public double? ChietKhau { get; set; }

        public virtual Ca Ca { get; set; }

        public virtual DonNhap DonNhap { get; set; }
    }
}
