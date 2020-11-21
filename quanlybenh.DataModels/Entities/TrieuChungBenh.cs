namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrieuChungBenh")]
    public partial class TrieuChungBenh
    {
        [Key]
        [Column(Order = 0)]
        public Guid MaBenh { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MaTrieuChung { get; set; }

      
        public virtual Benh Benh { get; set; }

        public virtual TrieuChung TrieuChung { get; set; }
    }
}
