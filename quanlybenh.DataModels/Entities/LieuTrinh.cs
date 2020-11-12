namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LieuTrinh")]
    public partial class LieuTrinh
    {
        [Key]
        public Guid MaLieuTrinh { get; set; }

        public Guid MaThuoc { get; set; }

        [StringLength(255)]
        public string TenLieuTrinh { get; set; }

        public string MoTaLieuTrinh { get; set; }

        public virtual Thuoc Thuoc { get; set; }
    }
}
