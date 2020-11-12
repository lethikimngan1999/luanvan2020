namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrieuChung")]
    public partial class TrieuChung
    {
        [Key]
        public Guid MaTrieuChung { get; set; }

        public Guid MaBenh { get; set; }

        [StringLength(255)]
        public string TenTrieuChung { get; set; }

        public string MoTaTrieuChung { get; set; }

        public virtual Benh Benh { get; set; }
    }
}
