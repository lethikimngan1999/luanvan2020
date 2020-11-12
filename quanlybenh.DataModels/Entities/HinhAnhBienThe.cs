namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnhBienThe")]
    public partial class HinhAnhBienThe
    {
        [Key]
        public Guid MaHinhAnhBT { get; set; }

        public Guid MaBienThe { get; set; }

        public string DuongDan { get; set; }

        public bool? ChonAvt { get; set; }
        public DateTime? NgayTao { get; set; }

        public string TenHinhAnh { get; set; }

        public virtual BienThe BienThe { get; set; }
    }
}
