namespace quanlybenh.DataModels.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Thuoc")]
    public partial class Thuoc
    {
        public Thuoc()
        {
            LieuTrinhs = new HashSet<LieuTrinh>();
            ThuocDieuTris = new HashSet<ThuocDieuTri>();
        }

        [Key]
        public Guid MaThuoc { get; set; }

        [StringLength(40)]
        public string TenThuoc { get; set; }

        public string CongDung { get; set; }

        public string CachDung { get; set; }

        public string LuuY { get; set; }
        public string HinhAnh { get; set; }
        public string LieuDung { get; set; }
        
        public virtual ICollection<ThuocDieuTri> ThuocDieuTris { get; set; }
        public virtual ICollection<LieuTrinh> LieuTrinhs { get; set; }
    }
}
