namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnhCa")]
    public partial class HinhAnhCa
    {
        [Key]
        public Guid MaHinhAnhCa { get; set; }

        public Guid MaCa { get; set; }

        public string DuongDan { get; set; }

        public bool? ChonAvt { get; set; }

        public DateTime? NgayTao { get; set; }

        public string TenHinhAnh { get; set; }

        public virtual Ca Ca { get; set; }
    }
}
