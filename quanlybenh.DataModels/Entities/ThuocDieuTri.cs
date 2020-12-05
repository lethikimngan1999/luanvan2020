namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuocDieuTri")]
    public partial class ThuocDieuTri
    {
        [Key]
        [Column(Order = 0)]
        public Guid MaThuoc { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MaBenh { get; set; }

 
        public string MoTa { get; set; }

        public virtual Benh Benh { get; set; }

        public virtual Thuoc Thuoc { get; set; }
    }
}
